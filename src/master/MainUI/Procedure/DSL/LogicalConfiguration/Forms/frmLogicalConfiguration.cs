using AntdUI;
using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using MainUI.Procedure.DSL.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// 使用依赖注入的构造函数
    /// 
    /// 优势：
    /// 1. 明确的依赖关系
    /// 2. 便于单元测试
    /// 3. 更好的错误处理
    /// 4. 支持配置和日志
    /// </summary>
    public partial class FrmLogicalConfiguration : UIForm
    {
        // 通过依赖注入获取的服务
        private readonly IWorkflowStateService _workflowState;
        private readonly GlobalVariableManager _variableManager;
        private readonly ILogger<FrmLogicalConfiguration> _logger;
        private readonly IFormService _formService;

        // 原有的私有字段
        private readonly DataGridViewManager _gridManager;
        private StepExecutionManager _executionManager;
        private bool _isExecuting;

        #region 构造函数
        public FrmLogicalConfiguration(
            IWorkflowStateService workflowState,
            GlobalVariableManager variableManager,
            ILogger<FrmLogicalConfiguration> logger,
            IFormService formService,
            string path,
            string modelType,
            string modelName,
            string processName)
        {
            // 依赖验证
            _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));

            try
            {
                _logger.LogInformation("正在初始化工作流配置窗体");

                InitializeComponent();

                // 初始化配置
                InitializeConfiguration(path, modelType, modelName, processName);

                // 加载工具箱
                InitializeToolbox();

                // 使用服务创建DataGridView管理器
                var steps = _workflowState.GetSteps();
                _gridManager = new DataGridViewManager(ProcessDataGridView, steps);

                // 设置事件处理程序
                RegisterEventHandlers();

                _logger.LogInformation("工作流配置窗体初始化完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "初始化工作流配置窗体时发生错误");
                MessageHelper.MessageOK($"构造函数加载数据错误：{ex.Message}", TType.Error);
                throw; // 重新抛出异常，让调用方处理
            }
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化配置 - 使用新的服务
        /// </summary>
        private void InitializeConfiguration(string path, string modelType, string modelName, string processName)
        {
            try
            {
                _logger.LogDebug("开始初始化配置: {ModelType}/{ModelName}/{ProcessName}",
                    modelType, modelName, processName);

                // 设置JSON文件路径
                JsonManager.FilePath = path;

                // 更新窗体标题
                Text = $"产品类型：{modelType}，产品型号：{modelName}，项点名称：{processName}";

                // 创建配置文件
                CreateJsonFileAsync(modelType, modelName, processName).Wait();

                // 使用新服务更新配置
                _workflowState.UpdateConfiguration(modelType, modelName, processName);

                // 初始化变量
                InitializeVariables();

                // 加载已保存的步骤到DataGridView
                LoadStepsToGrid();

                _logger.LogDebug("配置初始化完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "初始化配置时发生错误");
                throw;
            }
        }

        /// <summary>
        /// 注册事件处理程序
        /// </summary>
        private void RegisterEventHandlers()
        {
            try
            {
                // 订阅工作流状态变更事件
                _workflowState.StepNumChanged += OnStepNumChanged;
                _workflowState.VariableAdded += OnVariableAdded;
                _workflowState.VariableRemoved += OnVariableRemoved;
                _workflowState.StepAdded += OnStepAdded;
                _workflowState.StepRemoved += OnStepRemoved;

                // 订阅ToolTreeView的事件
                ToolTreeView.ItemDrag += ToolTreeView_ItemDrag;
                ToolTreeView.NodeMouseDoubleClick += ToolTreeView_NodeMouseDoubleClick;

                // 订阅DataGridView的事件
                ProcessDataGridView.DragDrop += ProcessDataGridView_DragDrop;
                ProcessDataGridView.DragEnter += ProcessDataGridView_DragEnter;
                ProcessDataGridView.CellDoubleClick += ProcessDataGridView_CellDoubleClick;

                _logger.LogDebug("事件处理程序注册完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "注册事件处理程序时发生错误");
                throw;
            }
        }

        // 加载已存在的Json数据到DataGridView控件中
        private async void LoadStepsToGrid()
        {
            try
            {
                var config = await JsonManager.GetOrCreateConfigAsync();
                // 找到当前项点的 Parent
                var parent = config.Form.FirstOrDefault(p =>
                    p.ModelTypeName == _workflowState.ModelTypeName &&
                    p.ModelName == _workflowState.ModelName &&
                    p.ItemName == _workflowState.ItemName);

                if (parent?.ChildSteps != null)
                {
                    // 清空临时数据和网格
                    _workflowState.ClearSteps();
                    ProcessDataGridView.Rows.Clear();

                    // 加载数据到临时存储和网格
                    foreach (var step in parent.ChildSteps)
                    {
                        ProcessDataGridView.Rows.Add(step.StepName, step.StepNum);
                        _workflowState.AddStep(new ChildModel
                        {
                            StepName = step.StepName,
                            Status = step.Status,
                            StepNum = step.StepNum,
                            StepParameter = step.StepParameter
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载步骤数据错误", ex);
                MessageHelper.MessageOK($"加载步骤数据错误：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 事件处理方法

        /// <summary>
        /// 步骤序号变更事件处理
        /// </summary>
        private void OnStepNumChanged(int newStepNum)
        {
            try
            {
                _logger.LogDebug("步骤序号变更为: {StepNum}", newStepNum);

                // 在UI线程上更新界面
                if (InvokeRequired)
                {
                    Invoke(new Action<int>(OnStepNumChanged), newStepNum);
                    return;
                }

                // 更新界面显示
                UpdateStepDisplay(newStepNum);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理步骤序号变更事件时发生错误");
            }
        }

        /// <summary>
        /// 变量添加事件处理
        /// </summary>
        private void OnVariableAdded(object variable)
        {
            try
            {
                if (variable is VarItem_Enhanced varItem)
                {
                    _logger.LogDebug("变量已添加: {VarName}", varItem.VarName);
                    // 可以在这里更新相关UI
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理变量添加事件时发生错误");
            }
        }

        /// <summary>
        /// 变量移除事件处理
        /// </summary>
        private void OnVariableRemoved(object variable)
        {
            try
            {
                if (variable is VarItem_Enhanced varItem)
                {
                    _logger.LogDebug("变量已移除: {VarName}", varItem.VarName);
                    // 可以在这里更新相关UI
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理变量移除事件时发生错误");
            }
        }

        /// <summary>
        /// 步骤添加事件处理
        /// </summary>
        private void OnStepAdded(ChildModel step)
        {
            try
            {
                _logger.LogDebug("步骤已添加: {StepName}", step.StepName);

                // 在UI线程上更新DataGridView
                if (InvokeRequired)
                {
                    Invoke(new Action<ChildModel>(OnStepAdded), step);
                    return;
                }

                _gridManager.AddRow(step.StepName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理步骤添加事件时发生错误");
            }
        }

        /// <summary>
        /// 步骤移除事件处理
        /// </summary>
        private void OnStepRemoved(ChildModel step)
        {
            try
            {
                _logger.LogDebug("步骤已移除: {StepName}", step.StepName);

                // 在UI线程上更新DataGridView
                if (InvokeRequired)
                {
                    Invoke(new Action<ChildModel>(OnStepRemoved), step);
                    return;
                }

                // 更新网格显示
                RefreshStepGrid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理步骤移除事件时发生错误");
            }
        }

        // 工具箱拖放事件处理
        private void ToolTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode node && node.Parent != null)
            {
                ToolTreeView.DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }

        // 双击工具箱节点打开对应表单
        private void ToolTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null) // 确保节点非空
            {
                _formService.OpenFormByName(e.Node.Text, this);
            }
        }

        // DataGridView拖放事件处理
        private void ProcessDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    var node = (TreeNode)e.Data.GetData(typeof(TreeNode));
                    if (node?.Parent != null)
                    {
                        AddStepToForm(node.Text, ProcessDataGridView.Rows.Count + 1);
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("拖拽步骤错误", ex);
                MessageHelper.MessageOK($"拖拽步骤错误：{ex.Message}", TType.Error);
            }
        }

        // DataGridView拖放进入事件处理
        private void ProcessDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(TreeNode)) ?
                DragDropEffects.Copy : DragDropEffects.None;
        }

        /// <summary>
        /// 双击DataGridView单元格打开步骤配置界面
        /// </summary>
        private void ProcessDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var row = ProcessDataGridView.Rows[e.RowIndex];
                    string stepName = row.Cells[0].Value?.ToString();

                    if (!string.IsNullOrEmpty(stepName))
                    {
                        _logger.LogDebug("打开步骤配置: {StepName}, 行索引: {RowIndex}", stepName, e.RowIndex);

                        // 使用新的线程安全属性设置
                        _workflowState.StepNum = e.RowIndex;
                        _workflowState.StepName = stepName;

                        _formService.OpenFormByName(stepName, this);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "打开步骤参数配置界面时发生错误");
                MessageHelper.MessageOK($"打开步骤参数配置界面错误：{ex.Message}", TType.Error);
            }
        }
        #endregion

        #region 工具箱初始化
        private void InitializeToolbox()
        {
            var toolNodes = BuildToolboxNodes();
            ToolTreeView.Nodes.AddRange(toolNodes);
            //默认展开所有节点
            ToolTreeView.ExpandAll();
        }

        /// <summary>
        /// 创建工具箱节点
        /// </summary>
        /// <returns></returns>
        private static TreeNode[] BuildToolboxNodes() =>
            [
                BuildSystemToolNode(),
                BuildReportToolNode(),
                BuildCalculationToolNode()
            ];

        /// <summary>
        /// 创建报表工具节点
        /// </summary>
        /// <returns></returns>
        private static TreeNode BuildReportToolNode()
        {
            return new TreeNode("报表工具",
            [
                new("读取单元格"),
                new("写入单元格"),
                new("保存报表"),
                new("打印报表")
            ]);
        }

        /// <summary>
        /// 创建系统设置工具节点
        /// </summary>
        /// <returns></returns>
        private static TreeNode BuildSystemToolNode()
        {
            return new TreeNode("通用参数(双击查看参数)",
            [
                new("变量定义"),
                new("试验参数"),
            ]);
        }

        /// <summary>
        /// 创建常用工具节点
        /// </summary>
        /// <returns></returns>
        private static TreeNode BuildCalculationToolNode()
        {
            return new TreeNode("常用工具",
            [
                new("条件判断"),
                new("变量赋值"),
                new("循环开始"),
                new("循环结束"),
                new("延时工具"),
                new("提示工具"),
                new("PLC读取"),
                new("PLC写入"),
                new("变量计算"),
                new("检测工具"),
            ]);
        }
        #endregion

        #region JSON文件处理
        // 创建JSON文件，如果不存在则创建并写入默认结构
        private static async Task CreateJsonFileAsync(string modelType, string modelName, string processName)
        {
            // 根据产品类型、产品型号中的试验项点生成存放JSON数据的路径
            string modelPath = Path.Combine(Application.StartupPath, "Procedure", modelType, modelName);
            string jsonPath = Path.Combine(modelPath, $"{processName}.json");

            if (!Directory.Exists(modelPath))
                Directory.CreateDirectory(modelPath);

            if (!File.Exists(jsonPath))
            {
                // 如果文件不存在，创建默认配置及格式
                var config = BuildDefaultConfig(modelType, modelName, processName);

                // 保存默认配置到JSON文件
                await JsonManager.UpdateConfigAsync(async c =>
                {
                    c.System = config.System;
                    c.Form = config.Form;
                    c.Variable = config.Variable;
                    await Task.CompletedTask;
                });
            }
        }

        /// <summary>
        /// 生成默认JSON配置结构
        /// </summary>
        /// <param name="modelType">产品类型</param>
        /// <param name="modelName">产品型号</param>
        /// <param name="processName">试验项点</param>
        /// <returns></returns>
        private static JsonManager.JsonConfig BuildDefaultConfig(string modelType, string modelName, string processName)
        {
            return new JsonManager.JsonConfig
            {
                // 初始化系统信息
                System = new JsonManager.JsonConfig.SystemInfo
                {
                    CreateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ProjectName = "软件通用平台"
                },
                // 初始化默认表单结构
                Form =
                [
                    new()
                     {
                         ModelTypeName = modelType,
                         ModelName = modelName,
                         ItemName = processName,
                         ChildSteps = []
                     }
                ],
                // 初始化默认变量列表（使用VarItem_Enhanced）
                Variable =
                [
                    new VarItem { VarName = "a", VarType = "int", VarText = "变量a" },
                    new VarItem { VarName = "b", VarType = "double", VarText = "变量b" },
                    new VarItem { VarName = "c", VarType = "int", VarText = "变量c" }
                ]
            };
        }

        #endregion

        #region 变量初始化
        /// <summary>
        /// 初始化变量
        /// </summary>
        private async void InitializeVariables()
        {
            try
            {
                // 读取JSON文件中的变量项
                var VarItems = await JsonManager.ReadVarItemsAsync();

                // 将VarItem转换为VarItem_Enhanced
                var enhancedVarItems = VarItems.Select(v => new VarItem_Enhanced
                {
                    VarName = v.VarName,
                    VarType = v.VarType,
                    VarValue = v.VarValue,
                    VarText = v.VarText,
                    LastUpdated = DateTime.Now,
                    IsAssignedByStep = false,
                    AssignmentType = VariableAssignmentType.None
                }).Cast<object>().ToList();

                // 清空现有变量并添加新变量
                _workflowState.ClearVariables();
                foreach (var variable in enhancedVarItems)
                {
                    _workflowState.AddVariable(variable);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("初始化变量失败", ex);
                MessageHelper.MessageOK("初始化变量失败：" + ex.Message, TType.Error);
            }
        }
        #endregion

        #region 步骤操作

        /// <summary>
        /// 添加步骤到表单 - 新版本
        /// </summary>
        private void AddStepToForm(string stepName, int stepNumber)
        {
            try
            {
                _logger.LogDebug("添加步骤: {StepName}, 序号: {StepNumber}", stepName, stepNumber);

                var newStep = new ChildModel
                {
                    StepName = stepName,
                    Status = 0,
                    StepNum = stepNumber,
                    StepParameter = 0
                };

                // 添加步骤
                _workflowState.AddStep(newStep);

                _logger.LogInformation("步骤添加成功: {StepName}", stepName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加步骤时发生错误: {StepName}", stepName);
                MessageHelper.MessageOK($"添加步骤错误：{ex.Message}", TType.Error);
            }
        }


        // 添加步骤按钮点击事件处理
        private void toolDeleteStep_Click(object sender, EventArgs e)
        {
            _gridManager.DeleteSelectedRow();
        }
        #endregion

        #region 按钮操作 - 使用新服务

        /// <summary>
        /// 保存按钮点击事件 - 新版本
        /// </summary>
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _logger.LogInformation("开始保存工作流配置");

                await JsonManager.UpdateConfigAsync(async config =>
                {
                    // 确保配置中有表单
                    if (config.Form.Count == 0)
                    {
                        config.Form.Add(new Parent
                        {
                            ModelTypeName = _workflowState.ModelTypeName,
                            ModelName = _workflowState.ModelName,
                            ItemName = _workflowState.ItemName,
                            ChildSteps = []
                        });
                    }

                    // 使用新的线程安全方法获取变量
                    config.Variable.Clear();
                    var variables = _variableManager.GetAllVariables();
                    config.Variable.AddRange(variables.Cast<VarItem_Enhanced>());

                    // 使用新的线程安全方法获取步骤
                    config.Form[0].ChildSteps.Clear();
                    var steps = _workflowState.GetSteps();
                    config.Form[0].ChildSteps.AddRange(steps);

                    await Task.CompletedTask;
                });

                _logger.LogInformation("工作流配置保存成功");
                MessageHelper.MessageOK("保存成功！", TType.Success);
                Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存工作流配置时发生错误");
                MessageHelper.MessageOK($"保存错误：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 执行按钮点击事件 - 新版本
        /// </summary>
        private async void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isExecuting)
                {
                    // 停止执行
                    _executionManager?.Stop();
                    return;
                }

                _isExecuting = true;
                btnExecute.Text = "停止";
                btnExecute.Symbol = 61516;

                try
                {
                    // 取消选择
                    ProcessDataGridView.ClearSelection();

                    // 使用新的线程安全方法获取步骤
                    var steps = _workflowState.GetSteps();
                    var stepCount = _workflowState.GetStepCount();

                    _logger.LogInformation("开始执行步骤序列，共 {StepCount} 个步骤", stepCount);

                    var factory = Program.ServiceProvider.GetRequiredService<Func<List<ChildModel>, StepExecutionManager>>();
                    _executionManager = factory(steps);

                    _executionManager.StepStatusChanged += UpdateStepStatus;

                    // 开始执行
                    await _executionManager.StartExecutionAsync();

                    _logger.LogInformation("步骤序列执行完成");
                }
                finally
                {
                    _isExecuting = false;
                    btnExecute.Text = "执行";
                    btnExecute.Symbol = 61515;

                    if (_executionManager != null)
                    {
                        _executionManager.StepStatusChanged -= UpdateStepStatus;
                        _executionManager = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "执行步骤序列时发生错误");
                MessageHelper.MessageOK($"执行错误：{ex.Message}", TType.Error);
            }
        }

        // 退出
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region 执行和停止操作

        /// <summary>
        /// 更新步骤状态显示
        /// </summary>
        private void UpdateStepStatus(ChildModel step, int index)
        {
            try
            {
                if (ProcessDataGridView.InvokeRequired)
                {
                    ProcessDataGridView.Invoke(() => UpdateStepStatus(step, index));
                    return;
                }

                if (index >= ProcessDataGridView.Rows.Count) return;

                var row = ProcessDataGridView.Rows[index];

                // 更新状态显示（不同颜色表示不同状态）
                switch (step.Status)
                {
                    case 0: // 未执行
                        row.DefaultCellStyle.BackColor = Color.White;
                        break;
                    case 1: // 执行中
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                        break;
                    case 2: // 成功
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        break;
                    case 3: // 失败
                        row.DefaultCellStyle.BackColor = Color.LightPink;
                        break;
                }

                // 刷新显示
                ProcessDataGridView.Refresh();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("更新步骤状态显示错误", ex);
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 更新步骤显示
        /// </summary>
        private void UpdateStepDisplay(int stepNum)
        {
            try
            {
                // 更新界面显示当前步骤
                if (stepNum >= 0 && stepNum < ProcessDataGridView.Rows.Count)
                {
                    ProcessDataGridView.ClearSelection();
                    ProcessDataGridView.Rows[stepNum].Selected = true;
                    ProcessDataGridView.CurrentCell = ProcessDataGridView.Rows[stepNum].Cells[0];
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新步骤显示时发生错误");
            }
        }

        /// <summary>
        /// 刷新步骤网格
        /// </summary>
        private void RefreshStepGrid()
        {
            try
            {
                // 重新加载步骤到网格
                var steps = _workflowState.GetSteps();
                //_gridManager.RefreshData(steps);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "刷新步骤网格时发生错误");
            }
        }

        #endregion

        #region 资源释放

        /// <summary>
        /// 窗体关闭时的清理工作
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                // 取消事件订阅
                if (_workflowState != null)
                {
                    _workflowState.StepNumChanged -= OnStepNumChanged;
                    _workflowState.VariableAdded -= OnVariableAdded;
                    _workflowState.VariableRemoved -= OnVariableRemoved;
                    _workflowState.StepAdded -= OnStepAdded;
                    _workflowState.StepRemoved -= OnStepRemoved;
                }

                _logger.LogInformation("工作流配置窗体已关闭");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "关闭窗体时发生错误");
            }
            finally
            {
                base.OnFormClosed(e);
            }
        }

        #endregion
    }
}