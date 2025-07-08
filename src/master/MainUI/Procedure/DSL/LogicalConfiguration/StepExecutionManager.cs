namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    /// <summary>
    /// 步骤执行管理器类，负责管理和执行步骤
    /// </summary>
    public class StepExecutionManager(List<ChildModel> steps)
    {
        private int _currentStepIndex = -1;
        private bool _isRunning;
        private CancellationTokenSource _cancellationTokenSource = new();

        public event Action<ChildModel, int> StepStatusChanged; // 步骤状态改变事件

        /// <summary>
        /// 开始执行所有步骤
        /// </summary>
        public async Task StartExecutionAsync()
        {
            if (_isRunning) return;

            _isRunning = true;
            _currentStepIndex = -1;
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                while (_currentStepIndex < steps.Count - 1 && _isRunning)
                {
                    _currentStepIndex++;
                    var step = steps[_currentStepIndex];

                    // 更新状态为执行中
                    step.Status = 1; // 1=执行中
                    StepStatusChanged?.Invoke(step, _currentStepIndex);

                    // 执行步骤
                    bool success = await ExecuteStepAsync(step);

                    // 更新执行结果
                    step.Status = success ? 2 : 3; // 2=成功，3=失败
                    StepStatusChanged?.Invoke(step, _currentStepIndex);

                    if (!success)
                    {
                        // 执行失败时停止
                        Stop();
                        break;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 处理取消操作
                Debug.WriteLine($"处理取消操作");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("步骤执行错误", ex);
                MessageHelper.MessageOK($"步骤执行错误：{ex.Message}", AntdUI.TType.Error);
            }
            finally
            {
                _isRunning = false;
            }
        }

        /// <summary>
        /// 执行单个步骤
        /// </summary>
        private async Task<bool> ExecuteStepAsync(ChildModel step)
        {
            try
            {
                var formInfo = FormInfo.readOnlyList.FirstOrDefault(f => f.FormText == step.StepName);
                if (formInfo == null) return false;

                // 通过反射调用对应的方法
                var methodName = formInfo.FormMethod;
                var method = typeof(MethodCollection).GetMethod(methodName.Split('.').Last());

                if (method != null)
                {
                    var result = await (Task<bool>)method.Invoke(null, [step.StepParameter]);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"执行步骤 {step.StepName} 失败", ex);
                return false;
            }
        }


        /// <summary>
        /// 停止执行
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
            _cancellationTokenSource.Cancel();
        }
    }
}