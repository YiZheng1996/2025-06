using MainUI.Procedure.DSL.LogicalConfiguration;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    /// <summary>
    /// 步骤执行管理器类，负责管理和执行步骤
    /// </summary>
    public class StepExecutionManager(List<ChildModel> steps)
    {
        private int _currentStepIndex = -1;
        private bool _isRunning;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

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
                switch (step.StepName)
                {
                    case "延时工具":
                        return await ExecuteDelayStepAsync(step);

                    case "变量定义":
                        return await ExecuteDefineVarStepAsync(step);

                    case "条件判断":
                        return await ExecuteConditionStepAsync(step);

                    //case "变量赋值":
                    //    return await ExecuteAssignmentStepAsync(step);

                    //case "循环控制":
                    //    return await ExecuteLoopStepAsync(step);

                    case "提示窗体":
                        return await ExecutePromptStepAsync(step);

                    // ... 其他步骤类型的处理

                    default:
                        throw new NotSupportedException($"不支持的步骤类型：{step.StepName}");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"执行步骤 {step.StepName} 失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 执行延时步骤
        /// </summary>
        private async Task<bool> ExecuteDelayStepAsync(ChildModel step)
        {
            if (step.StepParameter is Parameter_DelayTime delayParam)
            {
                await Task.Delay(TimeSpan.FromSeconds(delayParam.T), _cancellationTokenSource.Token);
                return true;
            }
            // 兼容JObject
            if (step.StepParameter is JObject jObj)
            {
                var param = jObj.ToObject<Parameter_DelayTime>();
                if (param != null)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(param.T), _cancellationTokenSource.Token);
                    // 修正类型，避免下次还要转换
                    step.StepParameter = param;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 变量定义
        /// </summary>
        private async Task<bool> ExecuteDefineVarStepAsync(ChildModel step)
        {
            return false;
        }

        /// <summary>
        /// 条件判断
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        private async Task<bool> ExecuteConditionStepAsync(ChildModel step)
        {
            var param = step.StepParameter as Parameter_Condition;
            if (param == null) return false;

            // 获取变量值
            var varItem = SingletonStatus.Instance.Obj.OfType<VarItem>()
                .FirstOrDefault(v => v.VarName == param.VarName);
            if (varItem == null) return false;

            double varValue = double.TryParse(varItem.VarText, out var v) ? v : 0;
            double cmpValue = double.TryParse(param.Value, out var c) ? c : 0;
            bool result = param.Operator switch
            {
                "==" => varValue == cmpValue,
                "!=" => varValue != cmpValue,
                ">" => varValue > cmpValue,
                "<" => varValue < cmpValue,
                ">=" => varValue >= cmpValue,
                "<=" => varValue <= cmpValue,
                _ => false
            };

            // 跳转到指定步骤
            _currentStepIndex = result ? param.TrueStepIndex - 1 : param.FalseStepIndex - 1;
            return true;

            return false;
        }

        /// <summary>
        /// 提示窗体
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ExecutePromptStepAsync(ChildModel step)
        {
            return false;
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