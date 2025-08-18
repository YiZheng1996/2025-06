namespace MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure
{
    /// <summary>
    /// 步骤执行管理器 - 使用策略模式
    /// </summary>
    /// <remarks>
    /// 构造函数
    /// </remarks>
    public class StepExecutionManager(List<ChildModel> steps)
    {
        private readonly List<ChildModel> _steps = steps ?? throw new ArgumentNullException(nameof(steps));
        private bool _isExecuting;
        private int _currentStepIndex;

        public event Action<ChildModel, int> StepStatusChanged;

        /// <summary>
        /// 静态工厂方法
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
                        // 使用策略模式执行步骤，消除大量重复的Execute方法
                        var result = await ExecuteStepAsync(step);

                        if (result.Success)
                        {
                            step.Status = 2; // 成功

                            // 检查是否需要跳转
                            if (result.NextStepIndex.HasValue)
                            {
                                _currentStepIndex = result.NextStepIndex.Value;
                                continue;
                            }
                        }
                        else
                        {
                            step.Status = 3; // 失败
                            NlogHelper.Default.Error($"步骤执行失败: {step.StepName}, 原因: {result.Message}");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        step.Status = 3; // 失败
                        NlogHelper.Default.Error($"步骤执行异常: {step.StepName}", ex);
                        break;
                    }

                    _currentStepIndex++;
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
        public void StopExecution()
        {
            _isExecuting = false;
        }

        /// <summary>
        /// 执行单个步骤，统一执行方法
        /// </summary>
        private async Task<ExecutionResult> ExecuteStepAsync(ChildModel step)
        {
            try
            {
                // 使用策略工厂创建执行策略，替代原来的switch语句
                var strategy = StepExecutionStrategyFactory.CreateStrategy(step.StepName);
                return await strategy.ExecuteAsync(step);
            }
            catch (NotSupportedException ex)
            {
                NlogHelper.Default.Error($"不支持的步骤类型: {step.StepName}", ex);
                return ExecutionResult.Failed($"不支持的步骤类型: {step.StepName}");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"步骤执行异常: {step.StepName}", ex);
                return ExecutionResult.Failed($"执行异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取当前步骤索引
        /// </summary>
        public int CurrentStepIndex => _currentStepIndex;

        /// <summary>
        /// 获取是否正在执行
        /// </summary>
        public bool IsExecuting => _isExecuting;

        /// <summary>
        /// 获取总步骤数
        /// </summary>
        public int TotalSteps => _steps.Count;
    }
}