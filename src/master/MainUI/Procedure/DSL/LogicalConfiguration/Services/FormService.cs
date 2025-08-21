using MainUI.Procedure.DSL.LogicalConfiguration.Forms;
using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Services
{
    /// <summary>
    /// 窗体服务实现，提供更清晰的窗体管理机制
    /// </summary>
    public class FormService(IServiceProvider serviceProvider, ILogger<FormService> logger) : IFormService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        private readonly ILogger<FormService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <summary>
        /// 根据名称打开窗体
        /// </summary>
        public void OpenFormByName(string formName, Form parent = null)
        {
            try
            {
                _logger.LogDebug("打开窗体: {FormName}", formName);

                Form form = null;

                // 根据窗体名称创建对应的窗体
                switch (formName.ToLowerInvariant())
                {
                    case "变量定义":
                        form = CreateForm<Form_DefineVar>();
                        break;
                    case "plc读取":
                        form = CreateForm<Form_ReadPLC>();
                        break;
                    case "plc写入":
                        form = CreateForm<Form_WritePLC>();
                        break;
                    case "延时工具":
                        form = CreateForm<Form_DelayTime>();
                        break;
                    case "读取单元格":
                        form = CreateForm<Form_ReadCells>();
                        break;
                    case "写入单元格":
                        form = CreateForm<Form_WriteCells>();
                        break;
                    case "保存报表":
                        form = CreateForm<Form_SaveReport>();
                        break;
                    case "系统提示":
                        form = CreateForm<Form_SystemPrompt>();
                        break;
                    default:
                        _logger.LogWarning("未知的窗体类型: {FormName}", formName);
                        MessageBox.Show($"未知的窗体类型: {formName}", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                if (form != null)
                {
                    // 设置父窗体关系
                    if (parent != null && !parent.IsDisposed)
                    {
                        form.Owner = parent;
                        form.StartPosition = FormStartPosition.CenterParent;
                    }

                    // 显示窗体
                    form.Show();
                    _logger.LogInformation("窗体打开成功: {FormName}", formName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "打开窗体时发生错误: {FormName}", formName);
                MessageBox.Show($"打开窗体时发生错误: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 创建指定类型的窗体
        /// 手动管理依赖注入，不依赖DI容器管理窗体生命周期
        /// </summary>
        public T CreateForm<T>() where T : Form
        {
            try
            {
                _logger.LogDebug("创建窗体: {FormType}", typeof(T).Name);

                // 根据窗体类型手动创建，注入所需依赖
                if (typeof(T) == typeof(Form_DefineVar))
                {
                    var variableManager = _serviceProvider.GetRequiredService<GlobalVariableManager>();

                    return (T)(object)new Form_DefineVar(variableManager);
                }
                else if (typeof(T) == typeof(Form_ReadPLC))
                {
                    var workflowState = _serviceProvider.GetRequiredService<IWorkflowStateService>();
                    var variableManager = _serviceProvider.GetRequiredService<GlobalVariableManager>();
                    var logger = _serviceProvider.GetRequiredService<ILogger<Form_ReadPLC>>();

                    return (T)(object)new Form_ReadPLC(workflowState, variableManager, logger);
                }
                else if (typeof(T) == typeof(Form_WritePLC))
                {
                    var workflowState = _serviceProvider.GetRequiredService<IWorkflowStateService>();
                    var logger = _serviceProvider.GetRequiredService<ILogger<Form_WritePLC>>();

                    return (T)(object)new Form_WritePLC(workflowState, logger);
                }
                else if (typeof(T) == typeof(Form_DelayTime))
                {
                    var workflowState = _serviceProvider.GetRequiredService<IWorkflowStateService>();
                    var logger = _serviceProvider.GetRequiredService<ILogger<Form_DelayTime>>();

                    return (T)(object)new Form_DelayTime(/*workflowState, logger*/);
                }
                else if (typeof(T) == typeof(Form_ReadCells))
                {
                    var workflowState = _serviceProvider.GetRequiredService<IWorkflowStateService>();
                    var logger = _serviceProvider.GetRequiredService<ILogger<Form_ReadCells>>();

                    return (T)(object)new Form_ReadCells(/*workflowState, logger*/);
                }
                else if (typeof(T) == typeof(Form_WriteCells))
                {
                    var workflowState = _serviceProvider.GetRequiredService<IWorkflowStateService>();
                    var logger = _serviceProvider.GetRequiredService<ILogger<Form_WriteCells>>();

                    return (T)(object)new Form_WriteCells(/*workflowState, logger*/);
                }
                else if (typeof(T) == typeof(Form_SaveReport))
                {
                    var workflowState = _serviceProvider.GetRequiredService<IWorkflowStateService>();
                    var logger = _serviceProvider.GetRequiredService<ILogger<Form_SaveReport>>();

                    return (T)(object)new Form_SaveReport(workflowState, logger);
                }
                else if (typeof(T) == typeof(Form_SystemPrompt))
                {
                    var workflowState = _serviceProvider.GetRequiredService<IWorkflowStateService>();
                    var logger = _serviceProvider.GetRequiredService<ILogger<Form_SystemPrompt>>();

                    return (T)(object)new Form_SystemPrompt(workflowState, logger);
                }

                throw new NotSupportedException($"不支持的窗体类型: {typeof(T).Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建窗体时发生错误: {FormType}", typeof(T).Name);
                throw;
            }
        }

        /// <summary>
        /// 创建逻辑配置窗体
        /// </summary>
        public FrmLogicalConfiguration CreateLogicalConfigurationForm(
            string path, string modelType, string modelName, string processName)
        {
            try
            {
                _logger.LogDebug("创建逻辑配置窗体: {ModelType}/{ModelName}/{ProcessName}",
                    modelType, modelName, processName);

                var workflowState = _serviceProvider.GetRequiredService<IWorkflowStateService>();
                var variableManager = _serviceProvider.GetRequiredService<GlobalVariableManager>();
                var logger = _serviceProvider.GetRequiredService<ILogger<FrmLogicalConfiguration>>();
                var formService = _serviceProvider.GetRequiredService<IFormService>();
                var form = new FrmLogicalConfiguration(
                    workflowState, variableManager, logger, formService,
                    path, modelType, modelName, processName);

                _logger.LogInformation("逻辑配置窗体创建成功");
                return form;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建逻辑配置窗体时发生错误");
                throw;
            }
        }
    }
}
