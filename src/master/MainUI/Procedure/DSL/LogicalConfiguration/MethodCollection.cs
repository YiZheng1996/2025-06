using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using RW.Modules;

namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    /// <summary>
    /// DSL执行引擎的核心方法集合
    /// </summary>
    public static class MethodCollection
    {
        #region 初始化的PLC模块字典
        /// <summary>
        /// 延迟初始化的PLC模块字典
        /// </summary>
        private static readonly Lazy<Dictionary<string, BaseModule>> _lazyDicPLC =
            new(InitializePLCModules, LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// 获取PLC模块字典的属性
        /// </summary>
        private static Dictionary<string, BaseModule> _dicPLC => _lazyDicPLC.Value;

        /// <summary>
        /// 初始化PLC模块的方法
        /// </summary>
        private static Dictionary<string, BaseModule> InitializePLCModules()
        {
            try
            {
                NlogHelper.Default.Info("开始延迟初始化 PLC 模块集合");

                // 确保 ModuleComponent 正确初始化
                ModuleComponent.Instance.Init();

                // 获取模块列表
                var moduleList = ModuleComponent.Instance.GetList();

                if (moduleList == null || moduleList.Count == 0)
                {
                    NlogHelper.Default.Warn("未找到任何 PLC 模块，使用空字典");
                    return [];
                }

                NlogHelper.Default.Info($"成功初始化 {moduleList.Count} 个 PLC 模块: {string.Join(", ", moduleList.Keys)}");
                return moduleList;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Info($"PLC模块延迟初始化失败: {ex.Message}", ex);
                // 返回空字典而不是抛出异常，确保系统继续运行
                return [];
            }
        }

        /// <summary>
        /// 检查PLC模块是否已成功初始化
        /// </summary>
        public static bool IsPLCInitialized => _lazyDicPLC.IsValueCreated && _dicPLC.Count > 0;

        #endregion

        #region 1. 延时工具 - 最简单的实现
        /// <summary>
        /// 延时方法 - 用于验证基础架构
        /// </summary>
        public static async Task<bool> Method_DelayTime(Parameter_DelayTime param)
        {
            try
            {
                // 记录开始日志
                NlogHelper.Default.Info($"开始延时 {param.T} 秒");

                // 执行延时
                await Task.Delay(TimeSpan.FromMilliseconds(param.T));

                // 记录完成日志
                NlogHelper.Default.Info($"延时 {param.T} 秒完成");
                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"延时执行失败: {ex.Message}", ex);
                return false;
            }
        }
        #endregion

        #region 2. 变量管理 - 涉及状态管理
        /// <summary>
        /// 变量定义方法
        /// </summary>
        public static async Task<bool> Method_DefineVar(Parameter_DefineVar param)
        {
            try
            {
                var singleton = SingletonStatus.Instance;
                var variables = GlobalVariableManager.GetAllVariables();

                // 检查变量是否已存在
                var existingVar = variables.FirstOrDefault(v => v.VarName == param.VarName);
                if (existingVar != null)
                {
                    // 更新现有变量
                    existingVar.VarName = param.VarName;
                    existingVar.VarType = param.VarType;
                    existingVar.UpdateValue(param.VarValue, "变量定义更新");
                    NlogHelper.Default.Info($"更新变量: {param.VarName} = {param.VarValue} ({param.VarType})");
                }
                else
                {
                    // 添加新变量（使用VarItem_Enhanced）
                    var newVar = new VarItem_Enhanced
                    {
                        VarName = param.VarName,
                        VarValue = param.VarValue,
                        VarType = param.VarType,
                        LastUpdated = DateTime.Now,
                        IsAssignedByStep = false,
                        AssignmentType = VariableAssignmentType.None
                    };
                    singleton.Obj.Add(newVar);
                    NlogHelper.Default.Info($"定义新变量: {param.VarName} = {param.VarValue} ({param.VarType})");
                }

                await Task.CompletedTask;
                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"变量定义失败: {ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 变量赋值方法
        /// </summary>
        public static async Task<bool> Method_VariableAssignment(Parameter_VariableAssignment param)
        {
            try
            {
                var targetVar = GlobalVariableManager.FindVariableByName(param.TargetVarName);

                if (targetVar == null)
                {
                    NlogHelper.Default.Error($"目标变量 {param.TargetVarName} 不存在");
                    return false;
                }

                // 解析赋值表达式
                object newValue = await EvaluateExpression(param.Expression, GlobalVariableManager.GetAllVariables());

                // 类型转换和赋值
                targetVar.UpdateValue(ConvertValue(newValue, targetVar.VarType), "变量赋值");

                NlogHelper.Default.Info($"变量赋值: {param.TargetVarName} = {targetVar.VarValue}");
                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"变量赋值失败: {ex.Message}", ex);
                return false;
            }
        }
        #endregion

        #region 3. PLC通信 - 硬件交互
        /// <summary>
        /// PLC读取方法
        /// </summary>
        public static Task<bool> Method_ReadPLC(Parameter_ReadPLC param)
        {
            try
            {
                if (param?.Items == null || param.Items.Count == 0)
                {
                    NlogHelper.Default.Error("PLC读取参数为空");
                    return Task.FromResult(false);
                }

                var variables = SingletonStatus.Instance.Obj.OfType<VarItem_Enhanced>().ToList();
                int successCount = 0;

                foreach (var plc in param.Items)
                {
                    try
                    {
                        // 通过名称查找目标变量
                        var targetVariable = variables.FirstOrDefault(v => v.VarName == plc.TargetVarName);
                        if (targetVariable == null)
                        {
                            NlogHelper.Default.Error($"目标变量不存在: {plc.TargetVarName}");
                            continue;
                        }

                        // 检查PLC模块
                        if (!_dicPLC.TryGetValue(plc.PlcModuleName, out var module))
                        {
                            NlogHelper.Default.Error($"PLC模块不存在: {plc.PlcModuleName}");
                            continue;
                        }

                        // 读取PLC值
                        var plcValue = module.Read(plc.PlcKeyName);

                        // 更改变量状态
                        targetVariable.UpdateValue(plcValue, $"PLC读取: {plc.PlcModuleName}.{plc.PlcKeyName}");

                        NlogHelper.Default.Info($"PLC读取成功: {plc.PlcModuleName}.{plc.PlcKeyName} = {plcValue} -> {targetVariable.VarName}");
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        NlogHelper.Default.Error($"PLC读取项失败 {plc.PlcModuleName}.{plc.PlcKeyName}: {ex.Message}", ex);
                    }
                }

                return Task.FromResult(successCount > 0);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"PLC读取方法失败: {ex.Message}", ex);
                return Task.FromResult(false);
            }
        }

        /// <summary>
        /// PLC写入方法
        /// </summary>
        public static async Task<bool> Method_WritePLC(Parameter_WritePLC param)
        {
            try
            {
                // 验证参数
                if (param?.Items == null || param.Items.Count == 0)
                {
                    NlogHelper.Default.Error("PLC写入参数为空或未指定PLC项");
                    return false;
                }

                int successCount = 0;
                int totalCount = param.Items.Count;

                // 逐个写入PLC值
                foreach (var plc in param.Items)
                {
                    try
                    {
                        // 验证参数完整性
                        if (string.IsNullOrEmpty(plc.PlcModuleName) ||
                            string.IsNullOrEmpty(plc.PlcKeyName))
                        {
                            NlogHelper.Default.Error($"PLC写入项参数不完整: {plc.PlcModuleName}.{plc.PlcKeyName}");
                            continue;
                        }

                        // 检查PLC模块是否存在
                        if (!_dicPLC.TryGetValue(plc.PlcModuleName, out var module))
                        {
                            NlogHelper.Default.Error($"未找到指定的PLC模块: {plc.PlcModuleName}");
                            continue;
                        }

                        // 先解析写入值
                        object writeValue = await ResolveValue(plc.PlcValue);
                        module.Write(plc.PlcKeyName, writeValue);
                        NlogHelper.Default.Info($"PLC写入成功: {plc.PlcModuleName}.{plc.PlcKeyName} = {writeValue}");
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        NlogHelper.Default.Error($"PLC写入项失败 {plc.PlcModuleName}.{plc.PlcKeyName}: {ex.Message}", ex);
                    }
                }

                // 根据成功率判断整体结果
                bool overallSuccess = successCount > 0;
                NlogHelper.Default.Info($"PLC写入完成: 成功 {successCount}/{totalCount} 项");
                return overallSuccess;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"PLC写入异常: {ex.Message}", ex);
                return false;
            }
        }
        #endregion

        #region 4. 条件判断 - 流程控制核心
        /// <summary>
        /// 条件评估方法 - 返回下一步骤索引
        /// </summary>
        public static async Task<int> Method_EvaluateCondition(Parameter_Condition param)
        {
            try
            {
                // 获取变量值
                var singleton = SingletonStatus.Instance;
                var variables = singleton.Obj.OfType<VarItem>().ToList();
                var variable = variables.FirstOrDefault(v => v.VarName == param.VarName);

                if (variable == null)
                {
                    NlogHelper.Default.Error($"条件判断失败: 变量 {param.VarName} 不存在");
                    return param.FalseStepIndex; // 变量不存在时走False分支
                }

                // 执行条件比较
                bool conditionResult = await EvaluateCondition(variable.VarValue, param.Operator, param.Value);

                int nextStep = conditionResult ? param.TrueStepIndex : param.FalseStepIndex;

                NlogHelper.Default.Info($"条件判断: {param.VarName}({variable.VarValue}) {param.Operator} {param.Value} = {conditionResult}, 下一步: {nextStep}");

                return nextStep;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"条件判断异常: {ex.Message}", ex);
                return param.FalseStepIndex; // 异常时走False分支
            }
        }
        #endregion

        #region 5. 系统提示
        /// <summary>
        /// 提示窗体方法
        /// </summary>
        public static async Task<bool> Method_SystemPrompt(Parameter_SystemPrompt param)
        {
            try
            {
                // 解析提示内容中的变量引用
                string resolvedMessage = await ResolveVariablesInText(param.Message);

                // 显示提示信息
                var result = MessageHelper.MessageYes(resolvedMessage);

                NlogHelper.Default.Info($"系统提示显示: {param.Title} - {resolvedMessage}");

                // 如果需要等待用户响应
                if (param.WaitForResponse)
                {
                    return result == DialogResult.OK;
                }

                await Task.CompletedTask;
                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"系统提示失败: {ex.Message}", ex);
                return false;
            }
        }
        #endregion

        #region 6. 检测工具
        /// <summary>
        /// 检测工具执行方法
        /// </summary>
        /// <param name="param">检测参数</param>
        /// <returns>检测结果</returns>
        public static async Task<bool> Method_Detection(Parameter_Detection param)
        {
            try
            {
                NlogHelper.Default.Info($"开始执行检测: {param.DetectionName}");

                DetectionResult result = new DetectionResult
                {
                    DetectionName = param.DetectionName,
                    StartTime = DateTime.Now
                };

                // 执行检测逻辑（支持重试机制）
                for (int attempt = 0; attempt <= param.RetryCount; attempt++)
                {
                    try
                    {
                        if (attempt > 0)
                        {
                            NlogHelper.Default.Info($"检测重试 {attempt}/{param.RetryCount}: {param.DetectionName}");
                            await Task.Delay(param.RetryIntervalMs);
                        }

                        // 1. 获取检测数据
                        object detectionValue = await GetDetectionValue(param.DataSource);
                        result.DetectedValue = detectionValue;

                        // 2. 执行检测判断
                        result.IsSuccess = await EvaluateDetection(detectionValue, param);
                        result.EndTime = DateTime.Now;

                        if (result.IsSuccess || attempt == param.RetryCount)
                        {
                            break; // 成功或达到最大重试次数
                        }
                    }
                    catch (Exception ex)
                    {
                        result.ErrorMessage = ex.Message;
                        NlogHelper.Default.Error($"检测执行异常 (尝试 {attempt + 1}): {ex.Message}", ex);

                        if (attempt == param.RetryCount)
                        {
                            result.IsSuccess = false;
                            break;
                        }
                    }
                }

                // 3. 处理检测结果
                await ProcessDetectionResult(result, param);

                NlogHelper.Default.Info($"检测完成: {param.DetectionName}, 结果: {(result.IsSuccess ? "通过" : "失败")}");

                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"检测工具执行失败: {ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 获取检测数据值
        /// </summary>
        private static async Task<object> GetDetectionValue(DataSourceConfig dataSource)
        {
            switch (dataSource.SourceType)
            {
                case DataSourceType.Variable:
                    return await GetVariableValue(dataSource.VariableName);

                case DataSourceType.PLC:
                    return await GetPlcValue(dataSource.PlcConfig);

                case DataSourceType.Expression:
                    return await EvaluateExpression(dataSource.Expression, 
                        GlobalVariableManager.GetAllVariables());

                default:
                    throw new NotSupportedException($"不支持的数据源类型: {dataSource.SourceType}");
            }
        }

        /// <summary>
        /// 获取变量值
        /// </summary>
        private static async Task<object> GetVariableValue(string variableName)
        {
            var singleton = SingletonStatus.Instance;
            var variables = singleton.Obj.OfType<VarItem>().ToList();
            var variable = variables.FirstOrDefault(v => v.VarName == variableName);

            if (variable == null)
            {
                throw new ArgumentException($"变量 {variableName} 不存在");
            }

            await Task.CompletedTask;
            return variable.VarValue;
        }

        /// <summary>
        /// 获取PLC值
        /// </summary>
        private static async Task<object> GetPlcValue(PlcAddressConfig plcConfig)
        {
            try
            {
                // 检查PLC模块是否存在
                if (!_dicPLC.TryGetValue(plcConfig.ModuleName, out var module))
                {
                    throw new ArgumentException($"未找到指定的PLC模块: {plcConfig.ModuleName}");
                }

                // 读取PLC值
                var plcValue = module.Read(plcConfig.Address);

                NlogHelper.Default.Info($"PLC读取成功: {plcConfig.ModuleName}.{plcConfig.Address} = {plcValue}");

                await Task.CompletedTask;
                return plcValue;
            }
            catch (Exception ex)
            {
                throw new Exception($"读取PLC值失败: {plcConfig.ModuleName}.{plcConfig.Address}, 错误: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 读取PLC值的异步方法 - 修正版
        /// </summary>
        private static async Task<object> ReadPlcValueAsync(PlcAddressConfig plcConfig)
        {
            // 这里需要实现实际的PLC读取逻辑
            // 示例代码，需要根据实际情况调整
            await Task.Delay(100); // 模拟异步读取
            return await GetPlcValue(plcConfig);
        }

        /// <summary>
        /// 检查PLC模块和地址是否有效
        /// </summary>
        /// <param name="plcConfig">PLC配置</param>
        /// <returns>是否有效</returns>
        public static bool ValidatePlcConfig(PlcAddressConfig plcConfig)
        {
            if (plcConfig == null) return false;

            if (string.IsNullOrWhiteSpace(plcConfig.ModuleName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(plcConfig.Address))
            {
                return false;
            }

            // 检查PLC模块是否存在
            return _dicPLC.ContainsKey(plcConfig.ModuleName);
        }

        /// <summary>
        /// 获取所有可用的PLC模块名称
        /// </summary>
        /// <returns>PLC模块名称列表</returns>
        public static List<string> GetAvailablePlcModules()
        {
            return [.. _dicPLC.Keys];
        }

        /// <summary>
        /// 获取指定PLC模块的所有可用地址
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns>地址列表</returns>
        public static List<string> GetAvailablePlcAddresses(string moduleName)
        {
            try
            {
                // 从 PointLocationPLC 获取地址信息
                if (PointPLCManager.Instance.DicModelsContent.TryGetValue(moduleName, out var addresses))
                {
                    return addresses.Keys.Where(key => key != "ServerName").ToList();
                }
                return new List<string>();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"获取PLC地址失败: {ex.Message}", ex);
                return new List<string>();
            }
        }

        /// <summary>
        /// 执行检测判断
        /// </summary>
        private static async Task<bool> EvaluateDetection(object value, Parameter_Detection param)
        {
            try
            {
                switch (param.Type)
                {
                    case DetectionType.ValueRange:
                        return await EvaluateRangeDetection(value, param.Condition);

                    case DetectionType.Equality:
                        return await EvaluateEqualityDetection(value, param.Condition);

                    case DetectionType.Threshold:
                        return await EvaluateThresholdDetection(value, param.Condition);

                    case DetectionType.Status:
                        return await EvaluateStatusDetection(value, param.Condition);

                    case DetectionType.ChangeRate:
                        return await EvaluateChangeRateDetection(value, param.Condition);

                    case DetectionType.CustomExpression:
                        return await EvaluateCustomExpression(value, param.Condition);

                    default:
                        throw new NotSupportedException($"不支持的检测类型: {param.Type}");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"检测判断失败: {ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 范围检测
        /// </summary>
        private static async Task<bool> EvaluateRangeDetection(object value, DetectionCondition condition)
        {
            if (!double.TryParse(value?.ToString(), out double numValue))
            {
                return false;
            }

            await Task.CompletedTask;
            return numValue >= condition.MinValue && numValue <= condition.MaxValue;
        }

        /// <summary>
        /// 相等性检测
        /// </summary>
        private static async Task<bool> EvaluateEqualityDetection(object value, DetectionCondition condition)
        {
            await Task.CompletedTask;

            if (double.TryParse(value?.ToString(), out double numValue) &&
                double.TryParse(condition.TargetValue, out double targetNum))
            {
                return Math.Abs(numValue - targetNum) <= condition.Tolerance;
            }

            return string.Equals(value?.ToString(), condition.TargetValue, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 阈值检测
        /// </summary>
        private static async Task<bool> EvaluateThresholdDetection(object value, DetectionCondition condition)
        {
            if (!double.TryParse(value?.ToString(), out double numValue))
            {
                return false;
            }

            await Task.CompletedTask;

            return condition.Operator switch
            {
                ComparisonOperator.GreaterThan => numValue > condition.ThresholdValue,
                ComparisonOperator.GreaterThanOrEqual => numValue >= condition.ThresholdValue,
                ComparisonOperator.LessThan => numValue < condition.ThresholdValue,
                ComparisonOperator.LessThanOrEqual => numValue <= condition.ThresholdValue,
                ComparisonOperator.Equal => Math.Abs(numValue - condition.ThresholdValue) <= condition.Tolerance,
                ComparisonOperator.NotEqual => Math.Abs(numValue - condition.ThresholdValue) > condition.Tolerance,
                _ => false
            };
        }

        /// <summary>
        /// 状态检测
        /// </summary>
        private static async Task<bool> EvaluateStatusDetection(object value, DetectionCondition condition)
        {
            await Task.CompletedTask;

            // 状态检测通常用于布尔值或状态字符串
            if (value is bool boolValue)
            {
                return bool.TryParse(condition.TargetValue, out bool targetBool) && boolValue == targetBool;
            }

            return string.Equals(value?.ToString(), condition.TargetValue, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 变化率检测
        /// </summary>
        private static async Task<bool> EvaluateChangeRateDetection(object value, DetectionCondition condition)
        {
            // 变化率检测需要历史数据，这里是简化实现
            // 实际应用中需要维护历史值队列
            await Task.CompletedTask;
            return true; // 简化实现
        }

        /// <summary>
        /// 自定义表达式检测
        /// </summary>
        private static async Task<bool> EvaluateCustomExpression(object value, DetectionCondition condition)
        {
            try
            {
                // 替换表达式中的变量引用
                string expression = condition.CustomExpression.Replace("{value}", value?.ToString() ?? "0");

                // 这里可以使用表达式求值库，比如 NCalc 或自定义解析器
                // 简化实现，只支持基本的比较
                await Task.CompletedTask;
                return true; // 简化实现
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"自定义表达式求值失败: {ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 处理检测结果
        /// </summary>
        private static async Task ProcessDetectionResult(DetectionResult result, Parameter_Detection param)
        {
            try
            {
                var singleton = SingletonStatus.Instance;
                var variables = singleton.Obj.OfType<VarItem>().ToList();

                // 保存检测结果到变量
                if (param.ResultHandling.SaveToVariable && !string.IsNullOrEmpty(param.ResultHandling.ResultVariableName))
                {
                    await SaveOrUpdateVariable(param.ResultHandling.ResultVariableName, result.IsSuccess);
                }

                // 保存检测值到变量
                if (param.ResultHandling.SaveValueToVariable && !string.IsNullOrEmpty(param.ResultHandling.ValueVariableName))
                {
                    await SaveOrUpdateVariable(param.ResultHandling.ValueVariableName, result.DetectedValue);
                }

                // 显示检测结果
                if (param.ResultHandling.ShowResult)
                {
                    string message = param.ResultHandling.MessageTemplate
                        .Replace("{DetectionName}", param.DetectionName)
                        .Replace("{Result}", result.IsSuccess ? "通过" : "失败")
                        .Replace("{Value}", result.DetectedValue?.ToString() ?? "无")
                        .Replace("{Duration}", $"{result.Duration.TotalMilliseconds:F0}ms");

                    NlogHelper.Default.Info(message);
                }

                // 处理失败情况
                if (!result.IsSuccess)
                {
                    await HandleDetectionFailure(param.ResultHandling, result);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"处理检测结果失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 处理检测失败
        /// </summary>
        private static async Task HandleDetectionFailure(ResultHandling resultHandling, DetectionResult result)
        {
            switch (resultHandling.OnFailure)
            {
                case FailureAction.Continue:
                    // 继续执行，无需特殊处理
                    break;

                case FailureAction.Stop:
                    // 停止流程执行
                    throw new InvalidOperationException($"检测失败，流程已停止: {result.DetectionName}");

                case FailureAction.Jump:
                    // 跳转逻辑需要在流程执行器中处理
                    break;

                case FailureAction.Confirm:
                    // 显示确认对话框
                    var confirmResult = MessageHelper.MessageYes(
                        $"检测失败: {result.DetectionName}\n检测值: {result.DetectedValue}\n是否继续执行流程？");
                    if (confirmResult != DialogResult.Yes)
                    {
                        throw new OperationCanceledException("用户取消流程执行");
                    }
                    break;
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 保存或更新变量
        /// </summary>
        private static async Task SaveOrUpdateVariable(string variableName, object value)
        {
            var singleton = SingletonStatus.Instance;
            var variables = singleton.Obj.OfType<VarItem>().ToList();
            var existingVar = variables.FirstOrDefault(v => v.VarName == variableName);

            if (existingVar != null)
            {
                existingVar.VarValue = (string)value;
            }
            else
            {
                var newVar = new VarItem
                {
                    VarName = variableName,
                    VarValue = (string)value,
                    VarType = value?.GetType().Name ?? "String"
                };
                singleton.Obj.Add(newVar);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 检测结果类
        /// </summary>
        public class DetectionResult
        {
            /// <summary>
            /// 检测项名称
            /// </summary>
            public string DetectionName { get; set; }

            /// <summary>
            /// 检测是否成功
            /// </summary>
            public bool IsSuccess { get; set; }

            /// <summary>
            /// 检测到的值
            /// </summary>
            public object DetectedValue { get; set; }

            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime StartTime { get; set; }

            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime EndTime { get; set; }

            /// <summary>
            /// 检测耗时
            /// </summary>
            public TimeSpan Duration => EndTime - StartTime;

            /// <summary>
            /// 错误消息
            /// </summary>
            public string ErrorMessage { get; set; }
        }
        #endregion

        #region 循环工具 - 支持简单的循环逻辑

        #endregion

        #region 辅助方法
        /// <summary>
        /// 表达式求值 - 支持简单的数学运算和变量引用
        /// </summary>
        private static async Task<object> EvaluateExpression(string expression, List<VarItem_Enhanced> variables)
        {
            // 简单实现 - 可以扩展支持更复杂的表达式
            expression = expression.Trim();

            // 如果是变量引用 (格式: {变量名})
            if (expression.StartsWith("{") && expression.EndsWith("}"))
            {
                string varName = expression.Substring(1, expression.Length - 2);
                var variable = variables.FirstOrDefault(v => v.VarName == varName);
                return variable?.VarValue ?? throw new ArgumentException($"变量 {varName} 不存在");
            }

            // 如果是直接值
            if (double.TryParse(expression, out double numValue))
                return numValue;

            // 字符串值
            return expression;
        }

        /// <summary>
        /// 条件比较评估
        /// </summary>
        private static async Task<bool> EvaluateCondition(object leftValue, string operatorStr, string rightValue)
        {
            // 类型转换
            double leftNum = Convert.ToDouble(leftValue);
            double rightNum = Convert.ToDouble(rightValue);

            return operatorStr switch
            {
                "==" => Math.Abs(leftNum - rightNum) < 0.0001,
                "!=" => Math.Abs(leftNum - rightNum) >= 0.0001,
                ">" => leftNum > rightNum,
                "<" => leftNum < rightNum,
                ">=" => leftNum >= rightNum,
                "<=" => leftNum <= rightNum,
                _ => throw new ArgumentException($"不支持的操作符: {operatorStr}")
            };
        }

        /// <summary>
        /// 解析值 - 支持变量引用和直接值
        /// </summary>
        private static async Task<object> ResolveValue(string valueExpression)
        {
            if (string.IsNullOrEmpty(valueExpression))
                return null;

            // 变量引用
            if (valueExpression.StartsWith("{") && valueExpression.EndsWith("}"))
            {
                string varName = valueExpression.Substring(1, valueExpression.Length - 2);
                var variable = GlobalVariableManager.FindVariableByName(varName);
                return variable?.VarValue;
            }

            // 直接值
            return valueExpression;
        }

        /// <summary>
        /// 解析文本中的变量引用
        /// </summary>
        private static async Task<string> ResolveVariablesInText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            string result = text;
            var variables = GlobalVariableManager.GetAllVariables();

            foreach (var variable in variables)
            {
                string placeholder = $"{{{variable.VarName}}}";
                if (result.Contains(placeholder))
                {
                    result = result.Replace(placeholder, variable.VarValue?.ToString() ?? "null");
                }
            }

            return result;
        }

        /// <summary>
        /// 值类型转换
        /// </summary>
        private static object ConvertValue(object value, string targetType)
        {
            return targetType?.ToLower() switch
            {
                "int" => Convert.ToInt32(value),
                "double" => Convert.ToDouble(value),
                "bool" => Convert.ToBoolean(value),
                "string" => value?.ToString(),
                _ => value
            };
        }
        #endregion
    }
}
