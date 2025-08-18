using MainUI.Procedure.DSL.LogicalConfiguration.Methods.Core;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Reflection;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure
{
    /// <summary>
    /// 使用泛型和反射，通用步骤执行策略
    /// </summary>
    public class GenericStepExecutionStrategy<TParam, TMethod> : IStepExecutionStrategy
        where TMethod : class, IDSLMethod
        where TParam : class
    {
        private readonly TMethod _methodInstance;
        private readonly string _methodName;
        private readonly Func<TMethod, TParam, Task<object>> _methodInvoker;

        public string StepName { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GenericStepExecutionStrategy(string stepName, string methodName)
        {
            StepName = stepName;
            _methodName = methodName;
            _methodInstance = DSLServiceContainer.GetService<TMethod>();
            _methodInvoker = CreateMethodInvoker();
        }

        /// <summary>
        /// 执行步骤
        /// </summary>
        public async Task<ExecutionResult> ExecuteAsync(ChildModel step)
        {
            try
            {
                // 转换参数
                var parameter = ConvertStepParameter<TParam>(step.StepParameter);
                if (parameter == null)
                {
                    return ExecutionResult.Failed($"参数转换失败: {step.StepName}");
                }

                // 执行方法
                var result = await _methodInvoker(_methodInstance, parameter);

                // 处理不同的返回类型
                return ProcessResult(result);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"步骤执行失败: {step.StepName}", ex);
                return ExecutionResult.Failed($"执行异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 创建方法调用器（使用表达式树提高性能）
        /// </summary>
        private Func<TMethod, TParam, Task<object>> CreateMethodInvoker()
        {
            var methodInfo = typeof(TMethod).GetMethod(_methodName) ?? 
                throw new InvalidOperationException($"方法不存在: {typeof(TMethod).Name}.{_methodName}");

            // 创建参数表达式
            var instanceParam = Expression.Parameter(typeof(TMethod), "instance");
            var parameterParam = Expression.Parameter(typeof(TParam), "parameter");

            // 创建方法调用表达式
            var methodCall = Expression.Call(instanceParam, methodInfo, parameterParam);

            // 处理不同的返回类型
            Expression convertedCall;
            if (methodInfo.ReturnType == typeof(Task<bool>))
            {
                // Task<bool> -> Task<object>
                var convertMethod = typeof(GenericStepExecutionStrategy<TParam, TMethod>)
                    .GetMethod(nameof(ConvertBoolTaskToObjectTask), BindingFlags.NonPublic | BindingFlags.Static);
                convertedCall = Expression.Call(convertMethod, methodCall);
            }
            else if (methodInfo.ReturnType == typeof(Task<int>))
            {
                // Task<int> -> Task<object>
                var convertMethod = typeof(GenericStepExecutionStrategy<TParam, TMethod>)
                    .GetMethod(nameof(ConvertIntTaskToObjectTask), BindingFlags.NonPublic | BindingFlags.Static);
                convertedCall = Expression.Call(convertMethod, methodCall);
            }
            else if (methodInfo.ReturnType.IsGenericType &&
                     methodInfo.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                // Task<T> -> Task<object>
                var convertMethod = typeof(GenericStepExecutionStrategy<TParam, TMethod>)
                    .GetMethod(nameof(ConvertGenericTaskToObjectTask), BindingFlags.NonPublic | BindingFlags.Static)
                    .MakeGenericMethod(methodInfo.ReturnType.GetGenericArguments()[0]);
                convertedCall = Expression.Call(convertMethod, methodCall);
            }
            else
            {
                throw new NotSupportedException($"不支持的返回类型: {methodInfo.ReturnType}");
            }

            // 编译表达式
            var lambda = Expression.Lambda<Func<TMethod, TParam, Task<object>>>(
                convertedCall, instanceParam, parameterParam);

            return lambda.Compile();
        }

        /// <summary>
        /// 转换参数
        /// </summary>
        private static T ConvertStepParameter<T>(object stepParameter) where T : class
        {
            if (stepParameter == null) return null;

            if (stepParameter is T directParam)
                return directParam;

            try
            {
                var json = stepParameter.ToString();
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 处理执行结果
        /// </summary>
        private ExecutionResult ProcessResult(object result)
        {
            return result switch
            {
                bool boolResult => boolResult ? ExecutionResult.Succeeded() : ExecutionResult.Failed(),
                int intResult when intResult >= 0 => ExecutionResult.Jump(intResult),
                int intResult when intResult < 0 => ExecutionResult.Failed("跳转索引无效"),
                null => ExecutionResult.Failed("方法返回null"),
                _ => ExecutionResult.Succeeded("", result)
            };
        }

        #region 类型转换辅助方法

        private static async Task<object> ConvertBoolTaskToObjectTask(Task<bool> task)
        {
            return await task;
        }

        private static async Task<object> ConvertIntTaskToObjectTask(Task<int> task)
        {
            return await task;
        }

        private static async Task<object> ConvertGenericTaskToObjectTask<T>(Task<T> task)
        {
            return await task;
        }

        #endregion
    }
}
