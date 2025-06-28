using MainUI.Procedure.DSL.LogicalConfiguration;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    /// <summary>
    /// ����ִ�й������࣬��������ִ�в���
    /// </summary>
    public class StepExecutionManager(List<ChildModel> steps)
    {
        private int _currentStepIndex = -1;
        private bool _isRunning;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public event Action<ChildModel, int> StepStatusChanged; // ����״̬�ı��¼�

        /// <summary>
        /// ��ʼִ�����в���
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

                    // ����״̬Ϊִ����
                    step.Status = 1; // 1=ִ����
                    StepStatusChanged?.Invoke(step, _currentStepIndex);

                    // ִ�в���
                    bool success = await ExecuteStepAsync(step);

                    // ����ִ�н��
                    step.Status = success ? 2 : 3; // 2=�ɹ���3=ʧ��
                    StepStatusChanged?.Invoke(step, _currentStepIndex);

                    if (!success)
                    {
                        // ִ��ʧ��ʱֹͣ
                        Stop();
                        break;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // ����ȡ������
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("����ִ�д���", ex);
                MessageHelper.MessageOK($"����ִ�д���{ex.Message}", AntdUI.TType.Error);
            }
            finally
            {
                _isRunning = false;
            }
        }

        /// <summary>
        /// ִ�е�������
        /// </summary>
        private async Task<bool> ExecuteStepAsync(ChildModel step)
        {
            try
            {
                switch (step.StepName)
                {
                    case "��ʱ����":
                        return await ExecuteDelayStepAsync(step);

                    case "��������":
                        return await ExecuteDefineVarStepAsync(step);

                    case "�����ж�":
                        return await ExecuteConditionStepAsync(step);

                    //case "������ֵ":
                    //    return await ExecuteAssignmentStepAsync(step);

                    //case "ѭ������":
                    //    return await ExecuteLoopStepAsync(step);

                    case "��ʾ����":
                        return await ExecutePromptStepAsync(step);

                    // ... �����������͵Ĵ���

                    default:
                        throw new NotSupportedException($"��֧�ֵĲ������ͣ�{step.StepName}");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"ִ�в��� {step.StepName} ʧ��", ex);
                return false;
            }
        }

        /// <summary>
        /// ִ����ʱ����
        /// </summary>
        private async Task<bool> ExecuteDelayStepAsync(ChildModel step)
        {
            if (step.StepParameter is Parameter_DelayTime delayParam)
            {
                await Task.Delay(TimeSpan.FromSeconds(delayParam.T), _cancellationTokenSource.Token);
                return true;
            }
            // ����JObject
            if (step.StepParameter is JObject jObj)
            {
                var param = jObj.ToObject<Parameter_DelayTime>();
                if (param != null)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(param.T), _cancellationTokenSource.Token);
                    // �������ͣ������´λ�Ҫת��
                    step.StepParameter = param;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ��������
        /// </summary>
        private async Task<bool> ExecuteDefineVarStepAsync(ChildModel step)
        {
            return false;
        }

        /// <summary>
        /// �����ж�
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        private async Task<bool> ExecuteConditionStepAsync(ChildModel step)
        {
            var param = step.StepParameter as Parameter_Condition;
            if (param == null) return false;

            // ��ȡ����ֵ
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

            // ��ת��ָ������
            _currentStepIndex = result ? param.TrueStepIndex - 1 : param.FalseStepIndex - 1;
            return true;

            return false;
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ExecutePromptStepAsync(ChildModel step)
        {
            return false;
        }


        /// <summary>
        /// ִֹͣ��
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
            _cancellationTokenSource.Cancel();
        }
    }
}