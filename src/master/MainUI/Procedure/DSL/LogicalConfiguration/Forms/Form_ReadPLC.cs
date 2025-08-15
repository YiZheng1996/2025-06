using AntdUI;
using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using Newtonsoft.Json;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    public partial class Form_ReadPLC : UIForm
    {
        public Form_ReadPLC()
        {
            InitializeComponent();
            LoadWritePLCParameters();
            InitializePointLocationPLC();
            InitializeVariableComboBox();
        }

        /// <summary>
        /// 加载写入PLC参数
        /// </summary>
        private void LoadWritePLCParameters()
        {
            try
            {
                var steps = SingletonStatus.Instance.IempSteps;
                int idx = SingletonStatus.Instance.StepNum;
                if (steps != null && idx >= 0 && idx < steps.Count)
                {
                    var paramObj = steps[idx].StepParameter;
                    Parameter_ReadPLC param = null;

                    if (paramObj is Parameter_ReadPLC directParam)
                    {
                        param = directParam;
                    }
                    else if (paramObj != null)
                    {
                        try
                        {
                            param = JsonConvert.DeserializeObject<Parameter_ReadPLC>(
                                paramObj is string s ? s : JsonConvert.SerializeObject(paramObj)
                            );
                        }
                        catch (Exception ex)
                        {
                            NlogHelper.Default.Error("参数反序列化失败", ex);
                            param = new Parameter_ReadPLC();
                        }
                    }

                    // 加载所有行
                    if (param != null && param.Items != null)
                    {
                        foreach (var item in param.Items)
                        {
                            if (!string.IsNullOrWhiteSpace(item.PlcKeyName))
                            {
                                DataGridViewPLCList.Rows.Add(item.PlcModuleName, item.PlcKeyName, item.TargetVarName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载PLC参数错误", ex);
                MessageHelper.MessageOK("加载PLC参数错误：" + ex.Message, TType.Error);
            }
        }

        /// <summary>
        /// 初始化变量下拉框
        /// </summary>
        private void InitializeVariableComboBox()
        {
            try
            {
                try
                {
                    var variables = GlobalVariableManager.GetAllVariables();

                    // 清空并重新加载
                    ColVariable.Items.Clear();

                    // 添加空选项
                    ColVariable.Items.Add("");

                    foreach (var variable in variables)
                    {
                        ColVariable.Items.Add(variable.VarName);
                    }
                }
                catch (Exception ex)
                {
                    NlogHelper.Default.Error("初始化变量下拉框失败", ex);
                    MessageHelper.MessageOK("初始化变量下拉框失败：" + ex.Message, TType.Error);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("初始化变量下拉框失败", ex);
            }
        }

        /// <summary>
        /// 加载全部PLC点位
        /// </summary>
        private void InitializePointLocationPLC()
        {
            try
            {
                TreeViewPLC.Nodes.Clear();
                foreach (var kvp in PointLocationPLC.Instance.DicModelsContent)
                {
                    // 创建主节点(Key)
                    TreeNode parentNode = new(kvp.Key);

                    // 添加子节点(Value)
                    foreach (var value in kvp.Value)
                    {
                        // 如果Key是"ServerName"，则不添加到TreeView中
                        if (value.Key != "ServerName")
                            parentNode.Nodes.Add(value.Key);
                    }
                    TreeViewPLC.Nodes.Add(parentNode);
                }
                // 默认全部展开
                TreeViewPLC.ExpandAll();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载全部PLC点位错误", ex);
                MessageHelper.MessageOK($"加载全部PLC点位错误：{ex.Message}", TType.Error);
            }
        }

        private void TreeViewPLC_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeViewPLC.DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        // 拖拽添加操作
        private void DataGridViewPLCList_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    var node = (TreeNode)e.Data.GetData(typeof(TreeNode));
                    if (node?.Parent != null)
                    {
                        DataGridViewPLCList.Rows.Add($"{node?.Parent.Text}", $"{node.Text}");
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("拖拽步骤错误", ex);
                MessageHelper.MessageOK($"拖拽步骤错误：{ex.Message}", TType.Error);
            }
        }

        // 拖拽完成
        private void DataGridViewPLCList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(TreeNode)) ?
               DragDropEffects.Copy : DragDropEffects.None;
        }

        // 保存数据到当前步骤
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取当前步骤信息
                var stepInfo = GlobalVariableManager.GetCurrentStepInfo();
                if (!stepInfo.IsValid)
                {
                    MessageHelper.MessageOK("当前步骤无效，无法保存。", TType.Error);
                    return;
                }

                // 检查是否有重复的变量名
                if (DataGridViewManager.HasDuplicateValuesInColumn(DataGridViewPLCList, "ColVariable"))
                {
                    MessageHelper.MessageOK("变量赋值名称被重复使用！！！", TType.Error);
                    return;
                }

                // 1. 收集变量赋值信息
                var variableAssignments = CollectVariableAssignments();
                var plcItems = CollectPlcItems();

                // 2. 检查变量冲突
                var conflicts = GlobalVariableManager.CheckVariableConflicts(
                    [.. variableAssignments.Select(v => v.VariableName)],
                    stepInfo.StepIndex
                );

                // 3. 显示冲突警告
                if (!GlobalVariableManager.ShowConflictWarning(conflicts, this))
                {
                    return;
                }

                if (plcItems.Count > 0)
                {
                    // 4. 保存参数
                    var param = new Parameter_ReadPLC { Items = plcItems };
                    stepInfo.Step.StepParameter = JsonConvert.SerializeObject(param);

                    // 5. 使用通用管理器设置变量状态
                    GlobalVariableManager.SetStepVariableAssignments(
                        stepInfo.StepIndex,
                        stepInfo.StepName,
                        variableAssignments,
                        VariableAssignmentType.PLCRead
                    );

                    MessageHelper.MessageOK("保存成功！请在主界面点击保存以写入文件。", TType.Success);
                    Close();
                }
                else
                {
                    MessageHelper.MessageOK("请至少添加一个有效的PLC读取项", TType.Warn);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存PLC读取配置失败", ex);
                MessageHelper.MessageOK($"保存失败：{ex.Message}", TType.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // 获取要删除的行
            int rowIndex = GetSelectedRowIndex();
            if (rowIndex < 0)
            {
                MessageHelper.MessageOK("请选择要删除的PLC行！", TType.Warn);
                return;
            }

            if (MessageHelper.MessageYes(this, "确定要删除选中的PLC行吗？") == DialogResult.OK)
            {
                try
                {
                    var stepInfo = GlobalVariableManager.GetCurrentStepInfo();
                    if (!stepInfo.IsValid)
                    {
                        MessageHelper.MessageOK("当前步骤无效，无法删除。", TType.Warn);
                        return;
                    }

                    // 1. 获取要删除的变量名
                    string targetVarName = DataGridViewPLCList.Rows[rowIndex].Cells["ColVariable"].Value?.ToString();

                    // 2. 清除该变量的赋值状态
                    if (!string.IsNullOrEmpty(targetVarName))
                    {
                        GlobalVariableManager.ClearSpecificVariableAssignment(targetVarName, stepInfo.StepIndex);
                    }

                    // 3. 从参数和UI中移除
                    RemoveItemFromParameter(stepInfo, rowIndex);
                    DataGridViewPLCList.Rows.RemoveAt(rowIndex);

                    MessageHelper.MessageOK("删除成功！", TType.Success);
                }
                catch (Exception ex)
                {
                    NlogHelper.Default.Error("删除PLC行失败", ex);
                    MessageHelper.MessageOK($"删除失败：{ex.Message}", TType.Error);
                }
            }
        }

        // 获取行索引
        private int GetSelectedRowIndex()
        {
            if (DataGridViewPLCList.SelectedRows.Count > 0)
                return DataGridViewPLCList.SelectedRows[0].Index;
            else if (DataGridViewPLCList.CurrentCell != null)
                return DataGridViewPLCList.CurrentCell.RowIndex;
            return -1;
        }

        private void RemoveItemFromParameter(CurrentStepInfo stepInfo, int rowIndex)
        {
            string plcModuleName = DataGridViewPLCList.Rows[rowIndex].Cells["ColPCLModelName"].Value?.ToString();
            string plcKeyName = DataGridViewPLCList.Rows[rowIndex].Cells["ColPCLKeyName"].Value?.ToString();

            if (GlobalVariableManager.TryGetParameter<Parameter_ReadPLC>(stepInfo.Step.StepParameter, out var param))
            {
                var toRemove = param.Items.FirstOrDefault(x =>
                    x.PlcModuleName == plcModuleName && x.PlcKeyName == plcKeyName);

                if (toRemove != null)
                {
                    param.Items.Remove(toRemove);
                    stepInfo.Step.StepParameter = JsonConvert.SerializeObject(param);
                }
            }
        }

        /// <summary>
        /// 收集变量赋值信息
        /// </summary>
        private List<VariableAssignment> CollectVariableAssignments()
        {
            var assignments = new List<VariableAssignment>();

            foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
            {
                if (row.IsNewRow) continue;

                string plcModuleName = row.Cells["ColPCLModelName"].Value?.ToString();
                string plcKeyName = row.Cells["ColPCLKeyName"].Value?.ToString();
                string varName = row.Cells["ColVariable"].Value?.ToString();

                if (!string.IsNullOrEmpty(varName) && !string.IsNullOrEmpty(plcModuleName) && !string.IsNullOrEmpty(plcKeyName))
                {
                    assignments.Add(new VariableAssignment
                    {
                        VariableName = varName,
                        AssignmentDescription = $"PLC读取({plcModuleName}.{plcKeyName})"
                    });
                }
            }

            return assignments;
        }

        /// <summary>
        /// 收集PLC项
        /// </summary>
        private List<PlcReadItem> CollectPlcItems()
        {
            var plcItems = new List<PlcReadItem>();

            foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
            {
                if (row.IsNewRow) continue;

                string plcModuleName = row.Cells["ColPCLModelName"].Value?.ToString();
                string plcKeyName = row.Cells["ColPCLKeyName"].Value?.ToString();
                string varName = row.Cells["ColVariable"].Value?.ToString();

                if (!string.IsNullOrEmpty(plcModuleName) && !string.IsNullOrEmpty(plcKeyName))
                {
                    plcItems.Add(new PlcReadItem
                    {
                        PlcModuleName = plcModuleName,
                        PlcKeyName = plcKeyName,
                        TargetVarName = varName
                    });
                }
            }

            return plcItems;
        }
    }
}
