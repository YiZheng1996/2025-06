using MainUI.Procedure.DSL.LogicalConfiguration.Forms;
using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using MainUI.Procedure.DSL.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure
{
    /// <summary>
    /// 窗体工厂类
    /// 
    /// 这个类负责创建窗体实例，并自动注入所需的依赖
    /// 优势：
    /// 1. 集中管理窗体创建逻辑
    /// 2. 自动处理依赖注入
    /// 3. 统一的错误处理
    /// 4. 支持窗体生命周期管理
    /// </summary>
    /// <remarks>
    /// 构造函数
    /// </remarks>
    public class FormFactory(IServiceProvider serviceProvider, ILogger<FormFactory> logger)
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        private readonly ILogger<FormFactory> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <summary>
        /// 创建 FrmLogicalConfiguration 窗体
        /// </summary>
        public FrmLogicalConfiguration CreateLogicalConfigurationForm(
            string path, string modelType, string modelName, string processName)
        {
            try
            {
                _logger.LogDebug("创建工作流配置窗体: {ModelType}/{ModelName}/{ProcessName}",
                    modelType, modelName, processName);

                // 从DI容器获取所需的服务
                var workflowState = _serviceProvider.GetRequiredService<IWorkflowStateService>();
                var variableManager = _serviceProvider.GetRequiredService<GlobalVariableManager>();
                var logger = _serviceProvider.GetRequiredService<ILogger<FrmLogicalConfiguration>>();

                // 创建窗体实例
                var form = new FrmLogicalConfiguration(
                    workflowState, variableManager, logger,
                    path, modelType, modelName, processName);

                _logger.LogInformation("工作流配置窗体创建成功");
                return form;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建工作流配置窗体时发生错误");
                throw;
            }
        }

        /// <summary>
        /// 泛型方法：创建任意类型的窗体
        /// </summary>
        public T CreateForm<T>() where T : Form
        {
            try
            {
                _logger.LogDebug("创建窗体: {FormType}", typeof(T).Name);

                var form = _serviceProvider.GetRequiredService<T>();

                _logger.LogInformation("窗体创建成功: {FormType}", typeof(T).Name);
                return form;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建窗体时发生错误: {FormType}", typeof(T).Name);
                throw;
            }
        }

        /// <summary>
        /// 创建带参数的窗体
        /// </summary>
        public T CreateFormWithParameters<T>(params object[] parameters) where T : Form
        {
            try
            {
                _logger.LogDebug("创建带参数的窗体: {FormType}", typeof(T).Name);

                // 使用反射创建实例，结合DI容器提供依赖
                var form = ActivatorUtilities.CreateInstance<T>(_serviceProvider, parameters);

                _logger.LogInformation("带参数的窗体创建成功: {FormType}", typeof(T).Name);
                return form;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建带参数的窗体时发生错误: {FormType}", typeof(T).Name);
                throw;
            }
        }
    }
}
