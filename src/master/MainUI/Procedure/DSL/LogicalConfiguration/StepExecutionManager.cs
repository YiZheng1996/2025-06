using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    public class StepExecutionManager(List<ChildModel> steps)
    {
        private readonly List<ChildModel> _steps = 
            steps ?? throw new ArgumentNullException(nameof(steps));
        private bool _isExecuting;
        private int _currentStepIndex;

        public event Action<ChildModel, int> StepStatusChanged;

        /// <summary>
        /// ��ʼִ�����в���
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

                    // ���²���״̬Ϊִ����
                    step.Status = 1; // ִ����
                    StepStatusChanged?.Invoke(step, _currentStepIndex);

                    try
                    {
                        // ִ�в���
                        var result = await ExecuteStepAsync(step);

                        if (result.Success)
                        {
                            step.Status = 2; // �ɹ�

                            // ����Ƿ��������жϲ��裬��Ҫ��ת
                            if (result.NextStepIndex.HasValue)
                            {
                                _currentStepIndex = result.NextStepIndex.Value;
                                NlogHelper.Default.Info($"������ת������: {_currentStepIndex}");
                            }
                            else
                            {
                                _currentStepIndex++; // ˳��ִ����һ��
                            }
                        }
                        else
                        {
                            step.Status = 3; // ʧ��
                            NlogHelper.Default.Info($"����ִ��ʧ��: {step.StepName}");
                            break; // ִֹͣ��
                        }
                    }
                    catch (Exception ex)
                    {
                        step.Status = 3; // ʧ��
                        NlogHelper.Default.Info($"����ִ���쳣: {step.StepName}", ex);
                        break; // ִֹͣ��
                    }
                    finally
                    {
                        StepStatusChanged?.Invoke(step, _currentStepIndex);
                    }

                    // ������������Ӳ�������ʱ
                    await Task.Delay(100);
                }
            }
            finally
            {
                _isExecuting = false;
            }
        }

        /// <summary>
        /// ִ�е�������
        /// </summary>
        private async Task<ExecutionResult> ExecuteStepAsync(ChildModel step)
        {
            try
            {
                // 1. ��ȡ�����Ӧ�Ĵ�����Ϣ
                var formInfo = FormInfo.readOnlyList.FirstOrDefault(f => f.FormText == step.StepName);
                if (formInfo == null)
                {
                    NlogHelper.Default.Info($"δ�ҵ���������: {step.StepName}");
                    return ExecutionResult.Failed();
                }

                // 2. ����������
                var methodName = formInfo.FormMethod;
                if (string.IsNullOrEmpty(methodName))
                {
                    NlogHelper.Default.Info($"���� {step.StepName} δ����ִ�з���");
                    return ExecutionResult.Failed();
                }

                // ��ȡʵ�ʵķ�������ȥ�� "MethodCollection." ǰ׺��
                string actualMethodName = methodName.Contains('.')
                    ? methodName.Split('.').Last()
                    : methodName;

                // 3. ��ȡ����������Ϣ
                var method = typeof(MethodCollection).GetMethod(actualMethodName);
                if (method == null)
                {
                    NlogHelper.Default.Info($"δ�ҵ�����: {actualMethodName}");
                    return ExecutionResult.Failed();
                }

                // 4. ת����������
                object parameter = ConvertStepParameter(step.StepParameter, formInfo.FormParameter);
                if (parameter == null)
                {
                    NlogHelper.Default.Info($"����ת��ʧ��: {step.StepName}");
                    return ExecutionResult.Failed();
                }

                // 5. ���÷���
                NlogHelper.Default.Info($"ִ�в���: {step.StepName}");

                var result = method.Invoke(null, [parameter]);

                // 6. ������ֵ
                return await HandleMethodResult(result, step.StepName);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Info($"ִ�в���ʧ��: {step.StepName}", ex);
                return ExecutionResult.Failed();
            }
        }

        /// <summary>
        /// ת������������� - ���JObjectת������
        /// </summary>
        private object ConvertStepParameter(object stepParameter, string parameterTypeName)
        {
            try
            {
                // �������Ϊ�գ�����Ĭ��ʵ��
                if (stepParameter == null)
                {
                    return CreateDefaultParameter(parameterTypeName);
                }

                // �����JObject����Ҫת��Ϊ��������
                if (stepParameter is JObject jObject)
                {
                    // ������������������
                    string fullTypeName = $"MainUI.Procedure.DSL.LogicalConfiguration.Parameter.{parameterTypeName}";

                    // ��ȡ��������
                    var parameterType = Assembly.GetExecutingAssembly().GetType(fullTypeName);
                    if (parameterType == null)
                    {
                        NlogHelper.Default.Info($"δ�ҵ���������: {fullTypeName}");
                        return null;
                    }

                    // ��JObjectת��Ϊ��������
                    var convertedParameter = jObject.ToObject(parameterType);

                    NlogHelper.Default.Info($"����ת���ɹ�: {parameterTypeName}");
                    return convertedParameter;
                }

                // ����Ѿ�����ȷ���ͣ�ֱ�ӷ���
                return stepParameter;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Info($"����ת���쳣: {parameterTypeName}", ex);
                return null;
            }
        }

        /// <summary>
        /// ����Ĭ�ϲ���ʵ��
        /// </summary>
        private object CreateDefaultParameter(string parameterTypeName)
        {
            try
            {
                string fullTypeName = $"MainUI.Procedure.DSL.LogicalConfiguration.Parameter.{parameterTypeName}";
                var parameterType = Assembly.GetExecutingAssembly().GetType(fullTypeName);

                if (parameterType != null)
                {
                    return Activator.CreateInstance(parameterType);
                }

                // ����Ҳ����������ͣ�����һЩĬ��ֵ
                return parameterTypeName switch
                {
                    "Parameter_DelayTime" => new { T = 1.0 },
                    "Parameter_DefineVar" => new { VarName = "", VarValue = "", VarType = "string" },
                    _ => new { }
                };
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Info($"����Ĭ�ϲ���ʧ��: {parameterTypeName}", ex);
                return new { };
            }
        }

        /// <summary>
        /// ����������ֵ
        /// </summary>
        private async Task<ExecutionResult> HandleMethodResult(object result, string stepName)
        {
            try
            {
                if (result is Task<bool> boolTask)
                {
                    bool success = await boolTask;
                    return success ? ExecutionResult.Succes() : ExecutionResult.Failed();
                }
                else if (result is Task<int> intTask)
                {
                    // �����жϷ���������һ������
                    int nextStepIndex = await intTask;
                    return ExecutionResult.Jump(nextStepIndex);
                }
                else if (result is bool directBool)
                {
                    return directBool ? ExecutionResult.Succes() : ExecutionResult.Failed();
                }
                else if (result is int directInt)
                {
                    return ExecutionResult.Jump(directInt);
                }
                else
                {
                    NlogHelper.Default.Info($"δʶ��ķ���ֵ����: {result?.GetType().Name}");
                    return ExecutionResult.Succes(); // Ĭ�ϳɹ�
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Info($"����������ֵʧ��: {stepName}", ex);
                return ExecutionResult.Failed();
            }
        }

        /// <summary>
        /// ִֹͣ��
        /// </summary>
        public void Stop()
        {
            _isExecuting = false;
            NlogHelper.Default.Info("����ִ����ֹͣ");
        }
    }

    /// <summary>
    /// ִ�н����
    /// </summary>
    public class ExecutionResult
    {
        public bool Success { get; set; }
        public int? NextStepIndex { get; set; }

        public static ExecutionResult Succes() => new() { Success = true };
        public static ExecutionResult Failed() => new() { Success = false };
        public static ExecutionResult Jump(int stepIndex) => new() { Success = true, NextStepIndex = stepIndex };
    }

    // ���䣺�������Ҫ��ǿ���͵Ĳ�������������������չ����
    public static class ParameterExtensions
    {
        /// <summary>
        /// ��ȫת��JObjectΪָ������
        /// </summary>
        public static T ToParameterType<T>(this object obj) where T : class, new()
        {
            if (obj == null) return new T();

            if (obj is JObject jObj)
            {
                return jObj.ToObject<T>() ?? new T();
            }

            if (obj is T directType)
            {
                return directType;
            }

            // �����JSON���л�/�����л�
            try
            {
                string json = JsonConvert.SerializeObject(obj);
                return JsonConvert.DeserializeObject<T>(json) ?? new T();
            }
            catch
            {
                return new T();
            }
        }
    }
}