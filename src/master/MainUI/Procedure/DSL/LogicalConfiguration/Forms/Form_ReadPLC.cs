using AntdUI;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using Newtonsoft.Json;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    public partial class Form_ReadPLC : UIForm
    {
        private readonly SingletonStatus _status;
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
        /// 初始化变量下拉框数据
        /// </summary>
        private void InitializeVariableComboBox()
        {
            try
            {
                // 清空现有项目
                ColVariable.Items.Clear();

                // 获取所有已定义的变量
                var variables = SingletonStatus.Instance.Obj.OfType<VarItem>().ToList();

                // 添加变量名到下拉框
                foreach (var variable in variables)
                {
                    if (!string.IsNullOrWhiteSpace(variable.VarName))
                    {
                        ColVariable.Items.Add(variable.VarName);
                    }
                }

                // 如果需要显示变量类型信息，可以这样添加：
                // ColVariable.Items.Add($"{variable.VarName} ({variable.VarType})");

                NlogHelper.Default.Info($"已加载 {variables.Count} 个变量到下拉框");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("初始化变量下拉框失败", ex);
                MessageHelper.MessageOK("初始化变量下拉框失败：" + ex.Message, TType.Error);
            }
        }

        /// <summary>
        /// 刷新变量下拉框数据
        /// </summary>
        public void RefreshVariableComboBox()
        {
            InitializeVariableComboBox();
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
                var steps = SingletonStatus.Instance.IempSteps;
                int idx = SingletonStatus.Instance.StepNum;

                if (steps != null && idx >= 0 && idx < steps.Count)
                {
                    var currentStep = steps[idx];
                    List<PlcReadItem> plcItems = [];
                    bool hasEmptyVariable = false;
                    List<string> conflictVariables = []; // 记录冲突的变量

                    foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string plcModelName = row.Cells["ColPCLModelName"].Value?.ToString()?.Trim() ?? "";
                        string plcKeyName = row.Cells["ColPCLKeyName"].Value?.ToString()?.Trim() ?? "";
                        string targetVarName = row.Cells["ColVariable"].Value?.ToString()?.Trim() ?? "";

                        if (string.IsNullOrEmpty(plcModelName)) continue;

                        if (plcItems.Any(p => p.PlcKeyName.Equals($"{plcModelName}.{plcKeyName}", StringComparison.OrdinalIgnoreCase)))
                        {
                            MessageHelper.MessageOK($"PLC名称\"{plcModelName}.{plcKeyName}\"重复。", TType.Warn);
                            return;
                        }

                        if (string.IsNullOrEmpty(targetVarName))
                        {
                            hasEmptyVariable = true;
                        }
                        else
                        {
                            // 检查变量赋值占用冲突
                            var conflictInfo = CheckVariableAssignmentConflict(targetVarName, idx);
                            if (conflictInfo.HasConflict)
                            {
                                conflictVariables.Add($"{targetVarName} (步骤{conflictInfo.ConflictStepNum}: {conflictInfo.ConflictStepName})");
                            }
                        }

                        plcItems.Add(new PlcReadItem
                        {
                            PlcModuleName = plcModelName,
                            PlcKeyName = plcKeyName,
                            TargetVarName = targetVarName
                        });
                    }

                    if (hasEmptyVariable)
                    {
                        MessageHelper.MessageOK("存在变量未选择，请补全所有变量后再保存。", TType.Warn);
                        return;
                    }

                    // 如果有变量赋值冲突，询问用户是否继续
                    if (conflictVariables.Count > 0)
                    {
                        string conflictMessage = $"以下变量已被其他步骤占用赋值：\n{string.Join("\n", conflictVariables)}\n\n继续保存将覆盖这些变量的赋值状态，是否继续？";
                        if (MessageHelper.MessageYes(this, conflictMessage) != DialogResult.OK)
                        {
                            return;
                        }
                    }

                    if (plcItems.Count > 0)
                    {
                        var param = new Parameter_ReadPLC
                        {
                            Items = plcItems
                        };

                        currentStep.StepParameter = JsonConvert.SerializeObject(param);

                        // 更新变量赋值状态
                        UpdateVariableAssignmentStatus(plcItems, idx, currentStep.StepName);

                        MessageHelper.MessageOK("保存成功！PLC操作将在主界面保存时写入配置文件。", TType.Success);
                        Close();
                    }
                    else
                    {
                        MessageHelper.MessageOK("请至少添加一个有效的PLC操作。", TType.Warn);
                    }
                }
                else
                {
                    MessageHelper.MessageOK("当前步骤无效，无法保存PLC数据。", TType.Warn);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"保存PLC读取错误", ex);
                MessageHelper.MessageOK($"保存PLC读取错误：{ex.Message}", TType.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int rowIndex = -1;
            // 优先用 SelectedRows
            if (DataGridViewPLCList.SelectedRows.Count > 0)
            {
                rowIndex = DataGridViewPLCList.SelectedRows[0].Index;
            }
            // 如果没有整行选中，尝试用当前单元格
            else if (DataGridViewPLCList.CurrentCell != null)
            {
                rowIndex = DataGridViewPLCList.CurrentCell.RowIndex;
            }

            if (rowIndex < 0 || rowIndex >= DataGridViewPLCList.Rows.Count || DataGridViewPLCList.Rows[rowIndex].IsNewRow)
            {
                MessageHelper.MessageOK("请选择要删除的PLC行！", TType.Warn);
                return;
            }

            if (MessageHelper.MessageYes(this, "确定要删除选中的PLC行吗？") == DialogResult.OK)
            {
                try
                {
                    // 获取当前步骤
                    var steps = SingletonStatus.Instance.IempSteps;
                    int idx = SingletonStatus.Instance.StepNum;
                    if (steps == null || idx < 0 || idx >= steps.Count)
                    {
                        MessageHelper.MessageOK("当前步骤无效，无法删除。", TType.Warn);
                        return;
                    }
                    var currentStep = steps[idx];

                    // 获取当前参数集合
                    Parameter_ReadPLC param = null;
                    if (currentStep.StepParameter is Parameter_ReadPLC directParam)
                    {
                        param = directParam;
                    }
                    else if (currentStep.StepParameter != null)
                    {
                        try
                        {
                            param = JsonConvert.DeserializeObject<Parameter_ReadPLC>(
                                currentStep.StepParameter is string s ? s :
                                JsonConvert.SerializeObject(currentStep.StepParameter)
                            );
                        }
                        catch
                        {
                            param = new Parameter_ReadPLC();
                        }
                    }
                    if (param == null || param.Items == null)
                    {
                        MessageHelper.MessageOK("集合数据异常，无法删除。", TType.Error);
                        return;
                    }

                    // 获取要删除的PLC名称
                    string plcName = DataGridViewPLCList.Rows[rowIndex].Cells["ColPCLModelName"].Value?.ToString();
                    // 在集合中查找并移除
                    var toRemove = param.Items.FirstOrDefault(x => x.PlcKeyName == plcName);
                    if (toRemove != null)
                    {
                        param.Items.Remove(toRemove);
                        // 更新StepParameter
                        currentStep.StepParameter = JsonConvert.SerializeObject(param);
                        // 删除DataGridView行
                        DataGridViewPLCList.Rows.RemoveAt(rowIndex);
                        MessageHelper.MessageOK("删除成功！", TType.Success);
                    }
                    else
                    {
                        MessageHelper.MessageOK("集合中未找到对应数据。", TType.Warn);
                    }
                }
                catch (Exception ex)
                {
                    NlogHelper.Default.Error($"删除失败", ex);
                    MessageHelper.MessageOK($"删除失败：{ex.Message}", TType.Error);
                }
            }
        }

        /// <summary>
        /// 检查变量赋值冲突信息
        /// </summary>
        private class VariableConflictInfo
        {
            public bool HasConflict { get; set; }
            public int ConflictStepNum { get; set; }
            public string ConflictStepName { get; set; }
        }

        /// <summary>
        /// 检查变量赋值占用冲突
        /// </summary>
        /// <param name="variableName">要检查的变量名</param>
        /// <param name="currentStepIndex">当前步骤索引</param>
        /// <returns>冲突信息</returns>
        private VariableConflictInfo CheckVariableAssignmentConflict(string variableName, int currentStepIndex)
        {
            var conflictInfo = new VariableConflictInfo();

            try
            {
                var steps = SingletonStatus.Instance.IempSteps;
                if (steps == null) return conflictInfo;

                for (int i = 0; i < steps.Count; i++)
                {
                    if (i != currentStepIndex) continue; // 跳过当前步骤

                    var step = steps[i];
                    if (step.StepParameter == null) continue;

                    // 检查变量赋值步骤
                    if (IsVariableAssignmentStep(step.StepParameter, variableName))
                    {
                        conflictInfo.HasConflict = true;
                        conflictInfo.ConflictStepNum = step.StepNum;
                        conflictInfo.ConflictStepName = step.StepName;
                        break;
                    }

                    // 检查其他PLC读取步骤
                    if (IsPlcReadStepConflict(step.StepParameter, variableName))
                    {
                        conflictInfo.HasConflict = true;
                        conflictInfo.ConflictStepNum = step.StepNum;
                        conflictInfo.ConflictStepName = step.StepName;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"检查变量赋值冲突失败: {ex.Message}", ex);
            }

            return conflictInfo;
        }

        /// <summary>
        /// 检查是否为变量赋值步骤且目标变量匹配
        /// </summary>
        private bool IsVariableAssignmentStep(object stepParameter, string variableName)
        {
            try
            {
                if (stepParameter is Parameter_VariableAssignment directParam)
                {
                    return directParam.IsAssignment &&
                           directParam.TargetVarName?.Equals(variableName, StringComparison.OrdinalIgnoreCase) == true;
                }

                if (stepParameter is string jsonStr)
                {
                    var param = JsonConvert.DeserializeObject<Parameter_VariableAssignment>(jsonStr);
                    return param?.IsAssignment == true &&
                           param?.TargetVarName?.Equals(variableName, StringComparison.OrdinalIgnoreCase) == true;
                }
            }
            catch
            {
                // 如果反序列化失败，说明不是变量赋值步骤
            }

            return false;
        }

        /// <summary>
        /// 检查是否为PLC读取步骤且存在变量冲突
        /// </summary>
        private bool IsPlcReadStepConflict(object stepParameter, string variableName)
        {
            try
            {
                if (stepParameter is Parameter_ReadPLC directParam)
                {
                    return directParam.Items?.Any(item =>
                        item.TargetVarName?.Equals(variableName, StringComparison.OrdinalIgnoreCase) == true) == true;
                }

                if (stepParameter is string jsonStr)
                {
                    var param = JsonConvert.DeserializeObject<Parameter_ReadPLC>(jsonStr);
                    return param?.Items?.Any(item =>
                        item.TargetVarName?.Equals(variableName, StringComparison.OrdinalIgnoreCase) == true) == true;
                }
            }
            catch
            {
                // 如果反序列化失败，说明不是PLC读取步骤
            }

            return false;
        }

        /// <summary>
        /// 更新变量赋值状态
        /// </summary>
        /// <param name="plcItems">PLC读取项目</param>
        /// <param name="stepIndex">步骤索引</param>
        /// <param name="stepName">步骤名称</param>
        private void UpdateVariableAssignmentStatus(List<PlcReadItem> plcItems, int stepIndex, string stepName)
        {
            try
            {
                var steps = SingletonStatus.Instance.IempSteps;
                if (steps == null) return;

                foreach (var plcItem in plcItems)
                {
                    if (string.IsNullOrEmpty(plcItem.TargetVarName)) continue;

                    // 清除其他步骤中该变量的赋值状态
                    ClearVariableAssignmentInOtherSteps(plcItem.TargetVarName, stepIndex);

                    // 如果有对应的变量赋值步骤，设置其IsAssignment为true
                    SetVariableAssignmentStatus(plcItem.TargetVarName, true, $"PLC读取步骤: {stepName}");
                }

                NlogHelper.Default.Info($"已更新 {plcItems.Count} 个变量的赋值状态");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"更新变量赋值状态失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 清除其他步骤中指定变量的赋值状态
        /// </summary>
        private void ClearVariableAssignmentInOtherSteps(string variableName, int excludeStepIndex)
        {
            try
            {
                var steps = SingletonStatus.Instance.IempSteps;
                if (steps == null) return;

                for (int i = 0; i < steps.Count; i++)
                {
                    if (i != excludeStepIndex) continue;

                    var step = steps[i];
                    if (step.StepParameter == null) continue;

                    // 清除变量赋值步骤中的状态
                    if (TryUpdateVariableAssignmentParameter(step, variableName, false))
                    {
                        NlogHelper.Default.Info($"已清除步骤 {step.StepName} 中变量 {variableName} 的赋值状态");
                    }

                    // 清除其他PLC读取步骤中的冲突项
                    if (TryRemoveConflictFromPlcReadStep(step, variableName))
                    {
                        NlogHelper.Default.Info($"已从步骤 {step.StepName} 中移除变量 {variableName} 的冲突赋值");
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"清除变量赋值状态失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 尝试更新变量赋值参数中的IsAssignment状态
        /// </summary>
        private bool TryUpdateVariableAssignmentParameter(ChildModel step, string variableName, bool isAssignment)
        {
            try
            {
                if (step.StepParameter is Parameter_VariableAssignment directParam)
                {
                    if (directParam.TargetVarName?.Equals(variableName, StringComparison.OrdinalIgnoreCase) == true)
                    {
                        directParam.IsAssignment = isAssignment;
                        return true;
                    }
                }
                else if (step.StepParameter is string jsonStr)
                {
                    var param = JsonConvert.DeserializeObject<Parameter_VariableAssignment>(jsonStr);
                    if (param?.TargetVarName?.Equals(variableName, StringComparison.OrdinalIgnoreCase) == true)
                    {
                        param.IsAssignment = isAssignment;
                        step.StepParameter = JsonConvert.SerializeObject(param);
                        return true;
                    }
                }
            }
            catch
            {
                // 忽略反序列化失败的情况
            }

            return false;
        }

        /// <summary>
        /// 尝试从PLC读取步骤中移除冲突的变量赋值
        /// </summary>
        private bool TryRemoveConflictFromPlcReadStep(ChildModel step, string variableName)
        {
            try
            {
                if (step.StepParameter is Parameter_ReadPLC directParam)
                {
                    if (directParam.Items?.RemoveAll(item =>
                        item.TargetVarName?.Equals(variableName, StringComparison.OrdinalIgnoreCase) == true) > 0)
                    {
                        return true;
                    }
                }
                else if (step.StepParameter is string jsonStr)
                {
                    var param = JsonConvert.DeserializeObject<Parameter_ReadPLC>(jsonStr);
                    if (param?.Items?.RemoveAll(item =>
                        item.TargetVarName?.Equals(variableName, StringComparison.OrdinalIgnoreCase) == true) > 0)
                    {
                        step.StepParameter = JsonConvert.SerializeObject(param);
                        return true;
                    }
                }
            }
            catch
            {
                // 忽略反序列化失败的情况
            }

            return false;
        }

        /// <summary>
        /// 设置变量赋值状态
        /// </summary>
        private void SetVariableAssignmentStatus(string variableName, bool isAssignment, string assignmentForm)
        {
            try
            {
                var steps = SingletonStatus.Instance.IempSteps;
                if (steps == null) return;

                // 查找对应的变量赋值步骤
                foreach (var step in steps)
                {
                    if (TryUpdateVariableAssignmentParameter(step, variableName, isAssignment))
                    {
                        // 如果是设置为true，同时更新AssignmentForm
                        if (isAssignment && step.StepParameter is Parameter_VariableAssignment param)
                        {
                            param.AssignmentForm = assignmentForm;
                        }
                        else if (isAssignment && step.StepParameter is string jsonStr)
                        {
                            var StrJson = JsonConvert.DeserializeObject<Parameter_VariableAssignment>(jsonStr);
                            if (StrJson != null)
                            {
                                StrJson.AssignmentForm = assignmentForm;
                                step.StepParameter = JsonConvert.SerializeObject(StrJson);
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"设置变量赋值状态失败: {ex.Message}", ex);
            }
        }
    }
}
