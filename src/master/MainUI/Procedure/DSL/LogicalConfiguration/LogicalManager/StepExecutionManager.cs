using MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure;
using MainUI.Procedure.DSL.LogicalConfiguration.Methods;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// 构造函数 - 通过依赖注入获取所有方法实例
    /// </summary>
    public class StepExecutionManager(List<ChildModel> steps)
    {
        private readonly List<ChildModel> _steps = steps ?? throw new ArgumentNullException(nameof(steps));
        private bool _isExecuting;
        private int _currentStepIndex;

        // 使用服务容器获取实例
        private readonly SystemMethods _systemMethods = DSLServiceContainer.GetService<SystemMethods>();
        private readonly VariableMethods _variableMethods = DSLServiceContainer.GetService<VariableMethods>();
        private readonly PLCMethods _plcMethods = DSLServiceContainer.GetService<PLCMethods>();
        private readonly DetectionMethods _detectionMethods = DSLServiceContainer.GetService<DetectionMethods>();
        private readonly FlowControlMethods _flowControlMethods = DSLServiceContainer.GetService<FlowControlMethods>();
        private readonly ReportMethods _reportMethods = DSLServiceContainer.GetService<ReportMethods>();

        public event Action<ChildModel, int> StepStatusChanged;

        /// <summary>
        /// 静态工厂方法 - 通过服务容器创建实例
        /// </summary>
        public static StepExecutionManager Create(List<ChildModel> steps)
        {
            return new StepExecutionManager(steps);
        }

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
