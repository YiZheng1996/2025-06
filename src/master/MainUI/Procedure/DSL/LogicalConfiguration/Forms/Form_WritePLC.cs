using AntdUI;
using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using MainUI.Procedure.DSL.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    public partial class Form_WritePLC : UIForm
    {
        private readonly IWorkflowStateService _workflowStateService;
        private readonly ILogger<Form_WritePLC> _logger;

        public Form_WritePLC(IWorkflowStateService workflowStateService, ILogger<Form_WritePLC> logger)
        {
            InitializeComponent();
            _workflowStateService = workflowStateService;
            _logger = logger;
            LoadWritePLCParameters();
            InitializePointLocationPLC();
        }

        /// <summary>
        /// 加载写入PLC参数
        /// </summary>
        private void LoadWritePLCParameters()
        {
            try
            {
                var steps = _workflowStateService.GetSteps() ;
                int idx = _workflowStateService.StepNum;
                if (steps != null && idx >= 0 && idx < steps.Count)
                {
                    var paramObj = steps[idx].StepParameter;
                    Parameter_WritePLC param = null;

                    if (paramObj is Parameter_WritePLC directParam)
                    {
                        param = directParam;
                    }
                    else if (paramObj != null)
                    {
                        try
                        {
                            param = JsonConvert.DeserializeObject<Parameter_WritePLC>(
                                paramObj is string s ? s : JsonConvert.SerializeObject(paramObj)
                            );
                        }
                        catch (Exception ex)
                        {
                            NlogHelper.Default.Error("参数反序列化失败", ex);
                            param = new Parameter_WritePLC();
                        }
                    }

                    // 加载所有行
                    if (param != null && param.Items != null)
                    {
                        foreach (var item in param.Items)
                        {
                            if (!string.IsNullOrWhiteSpace(item.PlcKeyName))
                            {
                                DataGridViewPLCList.Rows.Add(item.PlcModuleName, item.PlcKeyName, item.PlcValue);
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
        /// 加载全部PLC点位
        /// </summary>
        private void InitializePointLocationPLC()
        {
            try
            {
                TreeViewPLC.Nodes.Clear();
                //foreach (var kvp in PointPLCManager.Instance.DicModelsContent)
                //{
                //    // 创建主节点(Key)
                //    TreeNode parentNode = new(kvp.Key);

                //    // 添加子节点(Value)
                //    foreach (var value in kvp.Value)
                //    {
                //        // 如果Key是"ServerName"，则不添加到TreeView中
                //        if (value.Key != "ServerName")
                //            parentNode.Nodes.Add(value.Key);
                //    }
                //    TreeViewPLC.Nodes.Add(parentNode);
                //}
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
                var steps = _workflowStateService.GetSteps();
                int idx = _workflowStateService.StepNum;

                if (steps != null && idx >= 0 && idx < steps.Count)
                {
                    var currentStep = steps[idx];
                    var plcItems = new List<PlcWriteItem>();
                    bool hasEmptyConstant = false;

                    foreach (DataGridViewRow row in DataGridViewPLCList.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string plcModelName = row.Cells["ColPCLModelName"].Value?.ToString()?.Trim() ?? "";
                        string plcKeyName = row.Cells["ColPCLKeyName"].Value?.ToString()?.Trim() ?? "";
                        string plcValue = row.Cells["ColConstant"].Value?.ToString()?.Trim() ?? "";

                        if (string.IsNullOrEmpty(plcModelName)) continue;

                        if (plcItems.Any(p => p.PlcKeyName.Equals(plcModelName, StringComparison.OrdinalIgnoreCase)))
                        {
                            MessageHelper.MessageOK($"PLC名称\"{plcModelName}.{plcKeyName}\"重复。", TType.Warn);
                            return;
                        }

                        if (string.IsNullOrEmpty(plcValue))
                        {
                            hasEmptyConstant = true;
                        }

                        plcItems.Add(new PlcWriteItem
                        {
                            PlcModuleName = plcModelName,
                            PlcKeyName = plcKeyName,
                            PlcValue = plcValue
                        });
                    }

                    if (hasEmptyConstant)
                    {
                        MessageHelper.MessageOK("存在常数未填写，请补全所有常数值后再保存。", TType.Warn);
                        return;
                    }

                    if (plcItems.Count > 0)
                    {
                        var param = new Parameter_WritePLC
                        {
                            Items = plcItems
                        };

                        currentStep.StepParameter = JsonConvert.SerializeObject(param);

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
                NlogHelper.Default.Error($"保存PLC写入错误", ex);
                MessageHelper.MessageOK($"保存PLC写入错误：{ex.Message}", TType.Error);
            }
        }

        // 删除选中的PLC行
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
                    var steps = _workflowStateService.GetSteps();
                    int idx = _workflowStateService.StepNum;
                    if (steps == null || idx < 0 || idx >= steps.Count)
                    {
                        MessageHelper.MessageOK("当前步骤无效，无法删除。", TType.Warn);
                        return;
                    }
                    var currentStep = steps[idx];

                    // 获取当前参数集合
                    Parameter_WritePLC param = null;
                    if (currentStep.StepParameter is Parameter_WritePLC directParam)
                    {
                        param = directParam;
                    }
                    else if (currentStep.StepParameter != null)
                    {
                        try
                        {
                            param = JsonConvert.DeserializeObject<Parameter_WritePLC>(
                                currentStep.StepParameter is string s ? s :
                                JsonConvert.SerializeObject(currentStep.StepParameter)
                            );
                        }
                        catch
                        {
                            param = new Parameter_WritePLC();
                        }
                    }
                    if (param == null || param.Items == null)
                    {
                        MessageHelper.MessageOK("集合数据异常，无法删除。", TType.Error);
                        return;
                    }

                    // 获取要删除的PLC名称
                    string plcModelName = DataGridViewPLCList.Rows[rowIndex].Cells["ColPCLModelName"].Value?.ToString();
                    string plcKeyName = DataGridViewPLCList.Rows[rowIndex].Cells["ColPCLKeyName"].Value?.ToString();
                    // 在集合中查找并移除
                    var toRemove = param.Items.FirstOrDefault(x => $"{x.PlcModuleName}.{x.PlcKeyName}" == $"{plcModelName}.{plcKeyName}");
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
    }
}
