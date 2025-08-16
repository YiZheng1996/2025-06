namespace MainUI.Procedure.DSL.LogicalConfiguration.Methods.Core
{
    /// <summary>
    /// DSL方法基类
    /// </summary>
    public abstract class DSLMethodBase : IDSLMethod
    {
        public abstract string Category { get; }

        public abstract string Description { get; }

        /// <summary>
        /// 记录方法执行日志
        /// </summary>
        protected void LogMethodStart(string methodName, object parameter)
        {
            NlogHelper.Default.Info($"开始执行 {methodName}，参数: {parameter?.ToString() ?? "null"}");
        }

        /// <summary>
        /// 记录方法成功日志
        /// </summary>
        protected void LogMethodSuccess(string methodName, object result = null)
        {
            NlogHelper.Default.Info($"{methodName} 执行成功，结果: {result?.ToString() ?? "无返回值"}");
        }

        /// <summary>
        /// 记录方法失败日志
        /// </summary>
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
