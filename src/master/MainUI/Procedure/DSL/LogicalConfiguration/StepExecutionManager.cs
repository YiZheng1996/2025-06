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
        /// 执行单个步骤
        /// </summary>
        private async Task<ExecutionResult> ExecuteStepAsync(ChildModel step)
        {
            try
            {
                // 1. 获取步骤对应的窗体信息
                var formInfo = FormInfo.readOnlyList.FirstOrDefault(f => f.FormText == step.StepName);
                if (formInfo == null)
                {
                    NlogHelper.Default.Info($"未找到步骤配置: {step.StepName}");
                    return ExecutionResult.Failed();
                }

                // 2. 解析方法名
                var methodName = formInfo.FormMethod;
                if (string.IsNullOrEmpty(methodName))
                {
                    NlogHelper.Default.Info($"步骤 {step.StepName} 未配置执行方法");
                    return ExecutionResult.Failed();
                }

                // 提取实际的方法名（去掉 "MethodCollection." 前缀）
                string actualMethodName = methodName.Contains('.')
                    ? methodName.Split('.').Last()
                    : methodName;

                // 3. 获取方法反射信息
                var method = typeof(MethodCollection).GetMethod(actualMethodName);
                if (method == null)
                {
                    NlogHelper.Default.Info($"未找到方法: {actualMethodName}");
                    return ExecutionResult.Failed();
                }

                // 4. 转换参数类型
                object parameter = ConvertStepParameter(step.StepParameter, formInfo.FormParameter);
                if (parameter == null)
                {
                    NlogHelper.Default.Info($"参数转换失败: {step.StepName}");
                    return ExecutionResult.Failed();
                }

                // 5. 调用方法
                NlogHelper.Default.Info($"执行步骤: {step.StepName}");

                var result = method.Invoke(null, [parameter]);

                // 6. 处理返回值
                return await HandleMethodResult(result, step.StepName);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Info($"执行步骤失败: {step.StepName}", ex);
                return ExecutionResult.Failed();
            }
        }

        /// <summary>
        /// 转换步骤参数类型 - 解决JObject转换问题
        /// </summary>
        private object ConvertStepParameter(object stepParameter, string parameterTypeName)
        {
            try
            {
                // 如果参数为空，创建默认实例
                if (stepParameter == null)
                {
                    return CreateDefaultParameter(parameterTypeName);
                }

                // 如果是JObject，需要转换为具体类型
                if (stepParameter is JObject jObject)
                {
                    // 构建完整的类型名称
                    string fullTypeName = $"MainUI.Procedure.DSL.LogicalConfiguration.Parameter.{parameterTypeName}";

                    // 获取参数类型
                    var parameterType = Assembly.GetExecutingAssembly().GetType(fullTypeName);
                    if (parameterType == null)
                    {
                        NlogHelper.Default.Info($"未找到参数类型: {fullTypeName}");
                        return null;
                    }

                    // 将JObject转换为具体类型
                    var convertedParameter = jObject.ToObject(parameterType);

                    NlogHelper.Default.Info($"参数转换成功: {parameterTypeName}");
                    return convertedParameter;
                }

                // 如果已经是正确类型，直接返回
                return stepParameter;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Info($"参数转换异常: {parameterTypeName}", ex);
                return null;
            }
        }

        /// <summary>
        /// 创建默认参数实例
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

                // 如果找不到具体类型，创建一些默认值
                return parameterTypeName switch
                {
                    "Parameter_DelayTime" => new { T = 1.0 },
                    "Parameter_DefineVar" => new { VarName = "", VarValue = "", VarType = "string" },
                    _ => new { }
                };
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Info($"创建默认参数失败: {parameterTypeName}", ex);
                return new { };
            }
        }

        /// <summary>
        /// 处理方法返回值
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
                    // 条件判断方法返回下一步索引
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
                    NlogHelper.Default.Info($"未识别的返回值类型: {result?.GetType().Name}");
                    return ExecutionResult.Succes(); // 默认成功
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Info($"处理方法返回值失败: {stepName}", ex);
                return ExecutionResult.Failed();
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

    // 补充：如果您需要更强类型的参数处理，可以添加这个扩展方法
    public static class ParameterExtensions
    {
        /// <summary>
        /// 安全转换JObject为指定类型
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

            // 最后尝试JSON序列化/反序列化
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