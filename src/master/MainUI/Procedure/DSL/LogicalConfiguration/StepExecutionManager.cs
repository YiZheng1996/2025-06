namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    /// <summary>
    /// ����ִ�й������࣬��������ִ�в���
    /// </summary>
    public class StepExecutionManager(List<ChildModel> steps)
    {
        private int _currentStepIndex = -1;
        private bool _isRunning;
        private CancellationTokenSource _cancellationTokenSource = new();

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
                Debug.WriteLine($"����ȡ������");
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
                var formInfo = FormInfo.readOnlyList.FirstOrDefault(f => f.FormText == step.StepName);
                if (formInfo == null) return false;

                // ͨ��������ö�Ӧ�ķ���
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
                NlogHelper.Default.Error($"ִ�в��� {step.StepName} ʧ��", ex);
                return false;
            }
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