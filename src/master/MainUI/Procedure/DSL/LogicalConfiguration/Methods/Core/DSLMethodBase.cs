using MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure;
using System.Runtime.CompilerServices;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Methods.Core
{
    /// <summary>
    /// DSL方法基类 - 使用新的统一错误处理
    /// </summary>
    public abstract class DSLMethodBase : IDSLMethod
    {
        public abstract string Category { get; }
        public abstract string Description { get; }

        /// <summary>
        /// 执行方法并记录日志（带返回值）
        /// </summary>
        protected async Task<T> ExecuteWithLogging<T>(
            object parameter,
            Func<Task<T>> action,
            T defaultValue = default,
            [CallerMemberName] string methodName = "")
        {
            return await MethodExecutor.ExecuteAsync(methodName, parameter, action, defaultValue);
        }

        /// <summary>
        /// 执行方法并记录日志（无返回值）
        /// </summary>
        protected async Task ExecuteWithLogging(
            object parameter,
            Func<Task> action,
            [CallerMemberName] string methodName = "")
        {
            await MethodExecutor.ExecuteAsync(methodName, parameter, action);
        }

        // 保留原有方法以向后兼容（标记为过时）
        [Obsolete("请使用 ExecuteWithLogging 方法")]
        protected void LogMethodStart(string methodName, object parameter)
        {
            NlogHelper.Default.Info($"开始执行 {methodName}，参数: {parameter?.ToString() ?? "null"}");
        }

        [Obsolete("请使用 ExecuteWithLogging 方法")]
        protected void LogMethodSuccess(string methodName, object result = null)
        {
            NlogHelper.Default.Info($"{methodName} 执行成功，结果: {result?.ToString() ?? "无返回值"}");
        }

        [Obsolete("请使用 ExecuteWithLogging 方法")]
        protected void LogMethodError(string methodName, Exception ex)
        {
            NlogHelper.Default.Error($"{methodName} 执行失败: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// DSL方法接口
    /// </summary>
    public interface IDSLMethod
    {
        /// <summary>
        /// 方法类别
        /// </summary>
        string Category { get; }

        /// <summary>
        /// 方法描述
        /// </summary>
        string Description { get; }
    }

}
