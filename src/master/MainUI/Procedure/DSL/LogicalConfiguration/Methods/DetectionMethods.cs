using MainUI.Procedure.DSL.LogicalConfiguration;
using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using MainUI.Procedure.DSL.LogicalConfiguration.Methods.Core;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Methods
{
    /// <summary>
    /// 检测工具方法集合
    /// </summary>
    public class DetectionMethods : DSLMethodBase
    {
        public override string Category => "检测工具";
        public override string Description => "提供各种检测和验证功能";

        /// <summary>
        /// 检测工具执行方法
        /// </summary>
        public async Task<bool> Detection(Parameter_Detection param)
        {
            try
            {
                LogMethodStart(nameof(Detection), param);

                DetectionResult result = new()
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

                        // 如果检测成功，退出重试循环
                        if (result.IsSuccess)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        NlogHelper.Default.Error($"检测执行失败 (尝试 {attempt + 1}): {ex.Message}", ex);
                        result.ErrorMessage = ex.Message;

                        // 如果是最后一次尝试，记录最终失败
                        if (attempt == param.RetryCount)
                        {
                            result.IsSuccess = false;
                        }
                    }
                }

                result.EndTime = DateTime.Now;

                // 处理检测结果
                await ProcessDetectionResult(result, param);

                LogMethodSuccess(nameof(Detection), $"检测 {param.DetectionName}: {(result.IsSuccess ? "通过" : "失败")}");
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                LogMethodError(nameof(Detection), ex);
                return false;
            }
        }

        /// <summary>
        /// 获取检测数据值
        /// </summary>
        private static async Task<object> GetDetectionValue(DataSourceConfig dataSource)
        {
            return dataSource.SourceType switch
            {
                DataSourceType.Variable => await GetVariableValue(dataSource.VariableName),
                DataSourceType.PLC => await PointPLCManager.Instance.ReadPLCForDetectionAsync(dataSource.PlcConfig),
                //DataSourceType.Expression => await EvaluateExpression(dataSource.Expression,
                //                        GlobalVariableManager.GetAllVariables()),
                _ => throw new NotSupportedException($"不支持的数据源类型: {dataSource.SourceType}"),
            };
        }

        /// <summary>
        /// 获取变量值
        /// </summary>
        private static async Task<object> GetVariableValue(string variableName)
        {
            var singleton = SingletonStatus.Instance;
            var variables = singleton.Obj.OfType<VarItem>().ToList();
            var variable = variables.FirstOrDefault(v => v.VarName == variableName) ??
                throw new ArgumentException($"变量 {variableName} 不存在");
            await Task.CompletedTask;
            return variable.VarValue;
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

    }
}
