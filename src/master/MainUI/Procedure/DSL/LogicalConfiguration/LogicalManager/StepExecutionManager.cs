using MainUI.Procedure.DSL.LogicalConfiguration.Methods;
using MainUI.Procedure.DSL.LogicalConfiguration.Methods.Core;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager
{
    public class StepExecutionManager(List<ChildModel> steps)
    {
        private readonly List<ChildModel> _steps = steps ?? throw new ArgumentNullException(nameof(steps));
        private bool _isExecuting;
        private int _currentStepIndex;

        private readonly SystemMethods _systemMethods = DSLMethodRegistry.GetMethod<SystemMethods>();
        private readonly VariableMethods _variableMethods = DSLMethodRegistry.GetMethod<VariableMethods>();
        private readonly PLCMethods _plcMethods = DSLMethodRegistry.GetMethod<PLCMethods>();
        private readonly DetectionMethods _detectionMethods = DSLMethodRegistry.GetMethod<DetectionMethods>();
        private readonly FlowControlMethods _flowControlMethods = DSLMethodRegistry.GetMethod<FlowControlMethods>();
        private readonly ReportMethods _reportMethods = DSLMethodRegistry.GetMethod<ReportMethods>();

        public event Action<ChildModel, int> StepStatusChanged;

        /// <summary>
        /// 开始执行所有步骤
        /// </summary>
        public async Task StartExecutionAsync()
        {
            _isExecuting = true;
            _currentStepIndex = 0;

            try
            {
                while (_isExecuting && _currentStepIndex < _steps.Count)
                {
                    var step = _steps[_currentStepIndex];

                    // 更新步骤状态为执行中
                    step.Status = 1; // 执行中
                    StepStatusChanged?.Invoke(step, _currentStepIndex);

                    try
                    {
                        // 执行步骤
                        var result = await ExecuteStepAsync(step);

                        if (result.Success)
                        {
                            step.Status = 2; // 成功

                            // 检查是否是条件判断步骤，需要跳转
                            if (result.NextStepIndex.HasValue)
                            {
                                _currentStepIndex = result.NextStepIndex.Value;
                                NlogHelper.Default.Info($"条件跳转到步骤: {_currentStepIndex}");
                            }
                            else
                            {
                                _currentStepIndex++; // 顺序执行下一步
                            }
                        }
                        else
                        {
                            step.Status = 3; // 失败
                            NlogHelper.Default.Info($"步骤执行失败: {step.StepName}");
                            break; // 停止执行
                        }
                    }
                    catch (Exception ex)
                    {
                        step.Status = 3; // 失败
                        NlogHelper.Default.Info($"步骤执行异常: {step.StepName}", ex);
                        break; // 停止执行
                    }
                    finally
                    {
                        StepStatusChanged?.Invoke(step, _currentStepIndex);
                    }

                    // 可以在这里添加步骤间的延时
                    await Task.Delay(100);
                }
            }
            finally
            {
                _isExecuting = false;
            }
        }

        /// <summary>
        /// 停止执行
        /// </summary>
        public void Stop()
        {
            _isExecuting = false;
            NlogHelper.Default.Info("步骤执行已停止");
        }

        /// <summary>
        /// 执行单个步骤
        /// </summary>
        private async Task<ExecutionResult> ExecuteStepAsync(ChildModel step)
        {
            try
            {
                return step.StepName switch
                {
                    // 系统工具
                    "延时工具" => await ExecuteDelayTime(step),
                    "提示工具" => await ExecuteSystemPrompt(step),

                    // 变量管理
                    "变量定义" => await ExecuteDefineVar(step),
                    "试验参数" => await ExecuteDefineVar(step), // 试验参数也使用变量定义
                    "变量赋值" => await ExecuteVariableAssignment(step),

                    // PLC通信
                    "PLC读取" => await ExecuteReadPLC(step),
                    "PLC写入" => await ExecuteWritePLC(step),

                    // 检测工具
                    "检测工具" => await ExecuteDetection(step),

                    // 流程控制
                    "条件判断" => await ExecuteEvaluateCondition(step),

                    // 报表工具
                    "保存报表" => await ExecuteSaveReport(step),
                    "读取单元格" => await ExecuteReadCells(step),
                    "写入单元格" => await ExecuteWriteCells(step),

                    // 未知步骤
                    _ => ExecutionResult.Failed()
                };
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"执行步骤失败: {step.StepName}", ex);
                return ExecutionResult.Failed();
            }
        }

        #region 具体步骤执行方法

        /// <summary>
        /// 执行延时工具
        /// </summary>
        private async Task<ExecutionResult> ExecuteDelayTime(ChildModel step)
        {
            var param = ConvertStepParameter<Parameter_DelayTime>(step.StepParameter);
            var success = await _systemMethods.DelayTime(param);
            return success ? ExecutionResult.Succes() : ExecutionResult.Failed();
        }

        /// <summary>
        /// 执行系统提示
        /// </summary>
        private async Task<ExecutionResult> ExecuteSystemPrompt(ChildModel step)
        {
            var param = ConvertStepParameter<Parameter_SystemPrompt>(step.StepParameter);
            var success = await _systemMethods.SystemPrompt(param);
            return success ? ExecutionResult.Succes() : ExecutionResult.Failed();
        }

        /// <summary>
        /// 执行变量定义
        /// </summary>
        private async Task<ExecutionResult> ExecuteDefineVar(ChildModel step)
        {
            var param = ConvertStepParameter<Parameter_DefineVar>(step.StepParameter);
            var success = await _variableMethods.DefineVar(param);
            return success ? ExecutionResult.Succes() : ExecutionResult.Failed();
        }

        /// <summary>
        /// 执行变量赋值
        /// </summary>
        private async Task<ExecutionResult> ExecuteVariableAssignment(ChildModel step)
        {
            var param = ConvertStepParameter<Parameter_VariableAssignment>(step.StepParameter);
            var success = await _variableMethods.VariableAssignment(param);
            return success ? ExecutionResult.Succes() : ExecutionResult.Failed();
        }

        /// <summary>
        /// 执行PLC读取
        /// </summary>
        private async Task<ExecutionResult> ExecuteReadPLC(ChildModel step)
        {
            var param = ConvertStepParameter<Parameter_ReadPLC>(step.StepParameter);
            var success = await _plcMethods.ReadPLC(param);
            return success ? ExecutionResult.Succes() : ExecutionResult.Failed();
        }

        /// <summary>
        /// 执行PLC写入
        /// </summary>
        private async Task<ExecutionResult> ExecuteWritePLC(ChildModel step)
        {
            var param = ConvertStepParameter<Parameter_WritePLC>(step.StepParameter);
            var success = await _plcMethods.WritePLC(param);
            return success ? ExecutionResult.Succes() : ExecutionResult.Failed();
        }

        /// <summary>
        /// 执行检测工具
        /// </summary>
        private async Task<ExecutionResult> ExecuteDetection(ChildModel step)
        {
            var param = ConvertStepParameter<Parameter_Detection>(step.StepParameter);
            var success = await _detectionMethods.Detection(param);
            return success ? ExecutionResult.Succes() : ExecutionResult.Failed();
        }

        /// <summary>
        /// 执行条件判断
        /// </summary>
        private async Task<ExecutionResult> ExecuteEvaluateCondition(ChildModel step)
        {
            var param = ConvertStepParameter<Parameter_Condition>(step.StepParameter);
            var nextStepIndex = await _flowControlMethods.EvaluateCondition(param);
            return ExecutionResult.Jump(nextStepIndex);
        }

        /// <summary>
        /// 执行保存报表
        /// </summary>
        private async Task<ExecutionResult> ExecuteSaveReport(ChildModel step)
        {
            var param = ConvertStepParameter<Parameter_SaveReport>(step.StepParameter);
            var success = await _reportMethods.SaveReport(param);
            return success ? ExecutionResult.Succes() : ExecutionResult.Failed();
        }

        /// <summary>
        /// 执行读取单元格
        /// </summary>
        private async Task<ExecutionResult> ExecuteReadCells(ChildModel step)
        {
            var param = ConvertStepParameter<Parameter_ReadCells>(step.StepParameter);
            try
            {
                await _reportMethods.ReadCells(param);
                return ExecutionResult.Succes();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"读取单元格失败: {ex.Message}", ex);
                return ExecutionResult.Failed();
            }
        }

        /// <summary>
        /// 执行写入单元格
        /// </summary>
        private async Task<ExecutionResult> ExecuteWriteCells(ChildModel step)
        {
            var param = ConvertStepParameter<Parameter_WriteCells>(step.StepParameter);
            var success = await _reportMethods.WriteCells(param);
            return success ? ExecutionResult.Succes() : ExecutionResult.Failed();
        }

        /// <summary>
        /// 通用参数转换方法
        /// </summary>
        private T ConvertStepParameter<T>(object stepParameter) where T : class, new()
        {
            try
            {
                if (stepParameter == null)
                    return new T();

                if (stepParameter is T directType)
                    return directType;

                if (stepParameter is string strParameter && !string.IsNullOrWhiteSpace(strParameter))
                {
                    return JsonConvert.DeserializeObject<T>(strParameter) ?? new T();
                }

                if (stepParameter is JObject jObject)
                {
                    return jObject.ToObject<T>() ?? new T();
                }

                // 尝试序列化/反序列化
                var jsonString = JsonConvert.SerializeObject(stepParameter);
                return JsonConvert.DeserializeObject<T>(jsonString) ?? new T();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"参数转换失败 {typeof(T).Name}: {ex.Message}", ex);
                return new T();
            }
        }
        #endregion
    }

    /// <summary>
    /// 执行结果类
    /// </summary>
    public class ExecutionResult
    {
        public bool Success { get; set; }
        public int? NextStepIndex { get; set; }

        public static ExecutionResult Succes() => new() { Success = true };
        public static ExecutionResult Failed() => new() { Success = false };
        public static ExecutionResult Jump(int stepIndex) => new() { Success = true, NextStepIndex = stepIndex };
    }
}
