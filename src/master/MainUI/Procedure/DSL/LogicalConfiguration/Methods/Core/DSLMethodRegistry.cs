namespace MainUI.Procedure.DSL.LogicalConfiguration.Methods.Core
{
    /// <summary>
    /// DSL方法注册器
    /// </summary>
    public static class DSLMethodRegistry
    {
        private static readonly Dictionary<string, IDSLMethod> _methods = [];

        static DSLMethodRegistry()
        {
            RegisterDefaultMethods();
        }

        /// <summary>
        /// 注册默认方法
        /// </summary>
        private static void RegisterDefaultMethods()
        {
            Register("SystemMethods", new SystemMethods());
            Register("VariableMethods", new VariableMethods());
            Register("PLCMethods", new PLCMethods());
            Register("DetectionMethods", new DetectionMethods());
            Register("FlowControlMethods", new FlowControlMethods());
            Register("ReportMethods", new ReportMethods());
        }

        /// <summary>
        /// 注册方法类
        /// </summary>
        public static void Register(string name, IDSLMethod method)
        {
            _methods[name] = method;
        }

        /// <summary>
        /// 获取方法类
        /// </summary>
        public static T GetMethod<T>() where T : class, IDSLMethod
        {
            return _methods.Values.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// 获取所有方法
        /// </summary>
        public static IReadOnlyDictionary<string, IDSLMethod> GetAllMethods()
        {
            return _methods;
        }
    }
}
