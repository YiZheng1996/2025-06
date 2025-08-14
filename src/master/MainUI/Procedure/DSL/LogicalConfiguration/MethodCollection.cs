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
                var variables = GetVariables();

                // 检查变量是否已存在
                var existingVar = variables.FirstOrDefault(v => v.VarName == param.VarName);
                if (existingVar != null)
                {
                    // 更新现有变量
                    existingVar.VarName = param.VarName;
                    existingVar.VarType = param.VarType;
                    NlogHelper.Default.Info($"更新变量: {param.VarName} = {param.VarValue} ({param.VarType})");
                }
                else
                {
                    // 添加新变量
                    var newVar = new VarItem
                    {
                        VarName = param.VarName,
                        VarValue = param.VarValue,
                        VarType = param.VarType
                    };
                    singleton.Obj.Add(newVar);
                    NlogHelper.Default.Info($"定义新变量: {param.VarName} = {param.VarValue} ({param.VarType})");
                }

                await Task.CompletedTask; // 保持异步接口一致性
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
                var targetVar = FindVariable(param.TargetVarName);

                if (targetVar == null)
                {
                    NlogHelper.Default.Error($"目标变量 {param.TargetVarName} 不存在");
                    return false;
                }

                // 解析赋值表达式
                object newValue = await EvaluateExpression(param.Expression, GetVariables());

                // 类型转换和赋值
                targetVar.VarValue = ConvertValue(newValue, targetVar.VarType).ToString();

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
                        // 通过ID查找目标变量
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

                        // 直接更新VarItem，包含历史记录
                        targetVariable.UpdateValue(plcValue, $"PLC读取: {plc.PlcModuleName}.{plc.PlcKeyName}");

                        NlogHelper.Default.Info($"PLC读取成功: {plc.PlcModuleName}.{plc.PlcKeyName} = {plcValue} -> {targetVariable.VarName}");
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        NlogHelper.Default.Error($"PLC读取项失败 {plc.PlcModuleName}.{plc.PlcKeyName}: {ex.Message}", ex);
                    }
                }

                NlogHelper.Default.Info($"PLC读取完成: 成功 {successCount}/{param.Items.Count} 项");
                return Task.FromResult(successCount > 0);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"PLC读取异常: {ex.Message}", ex);
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

        #region 循环工具 - 支持简单的循环逻辑

        #endregion

        #region 辅助方法
        /// <summary>
        /// 安全获取变量列表
        /// </summary>
        private static List<VarItem> GetVariables()
        {
            return [.. SingletonStatus.Instance.Obj.OfType<VarItem>()];
        }

        /// <summary>
        /// 根据名称查找变量
        /// </summary>
        private static VarItem FindVariable(string varName)
        {
            return GetVariables().FirstOrDefault(v => v.VarName == varName);
        }

        /// <summary>
        /// 表达式求值 - 支持简单的数学运算和变量引用
        /// </summary>
        private static async Task<object> EvaluateExpression(string expression, List<VarItem> variables)
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
                var variable = FindVariable(varName);
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
            var variables = GetVariables();

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
