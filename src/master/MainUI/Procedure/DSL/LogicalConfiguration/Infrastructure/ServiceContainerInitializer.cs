using MainUI.Procedure.DSL.LogicalConfiguration.Methods;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure
{
    /// <summary>
    /// 服务容器初始化器 - 在应用程序启动时调用
    /// </summary>
    public static class ServiceContainerInitializer
    {
        private static bool _isInitialized = false;

        /// <summary>
        /// 初始化服务容器
        /// </summary>
        public static void Initialize()
        {
            if (_isInitialized) return;

            try
            {
                // 预热服务容器，确保所有服务都能正常创建
                var systemMethods = DSLServiceContainer.GetService<SystemMethods>();
                var variableMethods = DSLServiceContainer.GetService<VariableMethods>();
                var plcMethods = DSLServiceContainer.GetService<PLCMethods>();
                var detectionMethods = DSLServiceContainer.GetService<DetectionMethods>();
                var flowControlMethods = DSLServiceContainer.GetService<FlowControlMethods>();
                var reportMethods = DSLServiceContainer.GetService<ReportMethods>();

                NlogHelper.Default.Info("DSL服务容器初始化成功");
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("DSL服务容器初始化失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取初始化状态
        /// </summary>
        public static bool IsInitialized => _isInitialized;
    }
}
