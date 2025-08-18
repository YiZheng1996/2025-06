namespace MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure
{
    /// <summary>
    /// 步骤执行策略接口
    /// </summary>
    public interface IStepExecutionStrategy
    {
        /// <summary>
        /// 步骤名称
        /// </summary>
        string StepName { get; }

        /// <summary>
        /// 执行步骤
        /// </summary>
        Task<ExecutionResult> ExecuteAsync(ChildModel step);
    }

    /// <summary>
    /// 执行结果
    /// </summary>
    public class ExecutionResult
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// 下一步索引
        /// </summary>
        public int? NextStepIndex { get; private set; }

        /// <summary>
        /// 执行消息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 执行数据
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// 执行结果构造函数
        /// </summary>
        private ExecutionResult(bool success, int? nextStepIndex = null, string message = "", object data = null)
        {
            Success = success;
            NextStepIndex = nextStepIndex;
            Message = message ?? "";
            Data = data;
        }

        /// <summary>
        /// 成功执行结果
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ExecutionResult Succeeded(string message = "", object data = null)
            => new(true, null, message, data);

        /// <summary>
        /// 失败执行结果
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ExecutionResult Failed(string message = "", object data = null)
            => new(false, null, message, data);

        /// <summary>
        /// 执行结果跳转到指定步骤
        /// </summary>
        public static ExecutionResult Jump(int nextStepIndex, string message = "", object data = null)
            => new(true, nextStepIndex, message, data);
    }
}
