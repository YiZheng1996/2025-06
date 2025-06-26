using AntdUI;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    public partial class frmLogicalConfiguration : UIForm
    {
        private SingletonStatus _singletonStatus;
        private readonly JsonManager _jsonManager = new();
        private readonly DataGridViewManager _gridManager;

        #region 构造函数
        public frmLogicalConfiguration(string path, string modelType, string modelName, string processName)
        {
            InitializeComponent();

            // 初始化配置
            InitializeConfiguration(path, modelType, modelName, processName);

            // 加载工具箱
            InitializeToolbox();

            // 初始化DataGridView管理器
            _gridManager = new(ProcessDataGridView);

            // 设置事件处理程序
            RegisterEventHandlers();
        }

        private void InitializeConfiguration(string path, string modelType, string modelName, string processName)
        {
            // 设置JSON文件路径
            JsonManager.FilePath = path;

            // 更新窗体标题
            Text = $"产品类型：{modelType}，产品型号：{modelName}，项点名称：{processName}";

            // 创建配置文件
            CreateJsonFileAsync(modelType, modelName, processName).Wait();

            // 初始化变量
            InitializeVariables();

            // 初始化实验步骤
            LoadStepsToGrid();

            _singletonStatus = SingletonStatus.Instance;
        }

        // 注册事件处理程序
        private void RegisterEventHandlers()
        {
            // 拖放事件
            ToolTreeView.ItemDrag += ToolTreeView_ItemDrag;
            ProcessDataGridView.DragEnter += ProcessDataGridView_DragEnter;
            ProcessDataGridView.DragDrop += ProcessDataGridView_DragDrop;
            ProcessDataGridView.CellDoubleClick += ProcessDataGridView_CellDoubleClick;
        }

        // 加载已存在的Json数据到DataGridView控件中
        private async void LoadStepsToGrid()
        {
            var config = await JsonManager.GetOrCreateConfigAsync();
            // 找到当前项点的 Parent
            var parent = config.Form.FirstOrDefault(p =>
                p.ModelTypeName == _singletonStatus.ModelTypeName &&
                p.ModelName == _singletonStatus.ModelName &&
                p.ItemName == _singletonStatus.ItemName);

            if (parent?.ChildSteps != null)
            {
                ProcessDataGridView.Rows.Clear();
                foreach (var step in parent.ChildSteps)
                {
                    // 假设DataGridView列顺序：步骤名称、步骤号、参数
                    ProcessDataGridView.Rows.Add(step.StepName, step.StepNum);
                }
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
                BuildReportToolNode(),
                BuildSystemToolNode(),
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
            return new TreeNode("系统设置",
            [
                new("延时工具"),
                new("系统提示")
            ]);
        }

        /// <summary>
        /// 创建计算工具节点
        /// </summary>
        /// <returns></returns>
        private static TreeNode BuildCalculationToolNode()
        {
            return new TreeNode("计算工具",
            [
                new("变量定义"),
                new("条件判断"),
                new("变量赋值"),
                new("循环控制"),
                new("变量计算")
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
                // 如果文件不存在，创建默认配置
                var config = BuildDefaultConfig(modelType, modelName, processName);
                JsonManager.FilePath = jsonPath;
                // 保存默认配置到JSON文件
                await JsonManager.UpdateConfigAsync(async c =>
                {
                    c.System = config.System;
                    c.Form = config.Form;
                    c.Variable = config.Variable;
                    await Task.CompletedTask;
                });
            }
            // 更新SingletonStatus实例状态
            UpdateSingletonStatus(modelType, modelName, processName);
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
                    ProjectName = "深蓝模板1920"
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
                // 初始化默认变量列表
                Variable =
                [
                    new VarItem { VarName = "a", VarType = "int", VarText = "变量a" },
                    new VarItem { VarName = "b", VarType = "double", VarText = "变量b" },
                    new VarItem { VarName = "c", VarType = "int", VarText = "变量c" }
                ]
            };
        }

        // 更新SingletonStatus实例的状态
        private static void UpdateSingletonStatus(string modelType, string modelName, string processName)
        {
            var status = SingletonStatus.Instance;
            status.ModelTypeName = modelType;
            status.ModelName = modelName;
            status.ItemName = processName;
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
                SingletonStatus.Instance.Obj = [.. VarItems];
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("初始化变量失败", ex);
                MessageHelper.MessageOK("初始化变量失败：" + ex.Message, TType.Error);
            }
        }
        #endregion

        #region 拖放操作
        // 工具箱拖放事件处理
        private void ToolTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode node && node.Parent != null)
            {
                ToolTreeView.DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }

        // DataGridView拖放事件处理
        private async void ProcessDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    var node = (TreeNode)e.Data.GetData(typeof(TreeNode));
                    if (node?.Parent != null)
                    {
                        _gridManager.AddRow(node.Text);
                        await AddStepToForm(node.Text, ProcessDataGridView.Rows.Count);
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
        #endregion

        #region 步骤操作

        // 将步骤临时添加到表单中
        private async Task AddStepToForm(string stepName, int stepNumber)
        {
            try
            {
                await JsonManager.UpdateConfigAsync(async config =>
                {
                    // 确保 Form 列表包含足够的元素
                    if (config.Form.Count == 0)
                    {
                        config.Form.Add(new Parent
                        {
                            ModelTypeName = _singletonStatus.ModelTypeName,
                            ModelName = _singletonStatus.ModelName,
                            ItemName = _singletonStatus.ItemName,
                            ChildSteps = []
                        });
                    }

                    // 添加新的步骤
                    config.Form[0].ChildSteps.Add(new ChildModel
                    {
                        StepName = stepName,
                        Status = 1,
                        StepNum = stepNumber,
                        StepParameter = 0
                    });
                    await Task.CompletedTask;
                });
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("添加步骤到配置文件错误", ex);
                MessageHelper.MessageOK($"添加步骤到配置文件错误：{ex.Message}", TType.Error);
            }
        }

        // 双击DataGridView单元格打开步骤配置界面
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
                        SingletonStatus.Instance.StepNum = e.RowIndex;
                        SingletonStatus.Instance.StepName = stepName;
                        FormSet.OpenFormByName(stepName);
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("打开步骤参数配置界面错误", ex);
                MessageHelper.MessageOK($"打开步骤参数配置界面错误：{ex.Message}", TType.Error);
            }
        }

        // 添加步骤按钮点击事件处理
        private void toolDeleteStep_Click(object sender, EventArgs e)
        {
            _gridManager.DeleteSelectedRow();
        }
        #endregion

        // 退出界面
        private void BtnClose_Click(object sender, EventArgs e) => Close();

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                await JsonManager.UpdateConfigAsync(async config =>
                {
                    // 清空原有步骤
                    if (config.Form.Count == 0)
                    {
                        config.Form.Add(new Parent
                        {
                            ModelTypeName = _singletonStatus.ModelTypeName,
                            ModelName = _singletonStatus.ModelName,
                            ItemName = _singletonStatus.ItemName,
                            ChildSteps = []
                        });
                    }
                    else
                    {
                        config.Form[0].ChildSteps.Clear();
                    }

                    // 遍历DataGridView，将所有行写入ChildSteps
                    foreach (DataGridViewRow row in ProcessDataGridView.Rows)
                    {
                        if (row.IsNewRow) continue;
                        var stepName = row.Cells[0].Value?.ToString();
                        var stepNum = row.Index + 1;
                        if (!string.IsNullOrEmpty(stepName))
                        {
                            config.Form[0].ChildSteps.Add(new ChildModel
                            {
                                StepName = stepName,
                                Status = 1,
                                StepNum = stepNum,
                                StepParameter = 0
                            });
                        }
                    }
                    await Task.CompletedTask;
                });
                MessageHelper.MessageOK("保存成功！", TType.Success);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存步骤到配置文件错误", ex);
                MessageHelper.MessageOK($"保存步骤到配置文件错误：{ex.Message}", TType.Error);
            }
        }

    }

    /// <summary>
    /// DataGridView管理器类
    /// </summary>
    internal class DataGridViewManager(DataGridView grid)
    {
        /// <summary>
        /// 添加行数据到DataGridView
        /// </summary>
        /// <param name="stepName"></param>
        public void AddRow(string stepName)
        {
            grid.Rows.Add(stepName, grid.Rows.Count + 1);
        }

        /// <summary>
        /// 删除选中的行数据
        /// </summary>
        public void DeleteSelectedRow()
        {
            if (grid.SelectedRows.Count > 0)
            {
                var selectedRow = grid.SelectedRows[0];
                grid.Rows.Remove(selectedRow);
                ReorderRows();
            }
        }

        /// <summary>
        /// 重新排序行
        /// </summary>
        private void ReorderRows()
        {
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                if (grid.Rows[i].Cells["ColStepNum"] != null)
                {
                    grid.Rows[i].Cells["ColStepNum"].Value = i + 1;
                }
            }
        }
    }
}
