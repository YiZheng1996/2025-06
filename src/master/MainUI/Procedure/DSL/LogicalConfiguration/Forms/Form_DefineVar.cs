using AntdUI;
using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    public partial class Form_DefineVar : UIForm
    {
        public Form_DefineVar()
        {
            InitializeComponent();
            LoadVariables();
        }

        public Form_DefineVar(SingletonStatus singleton)
        {
            InitializeComponent();
            LoadVariables();
        }

        // 加载变量列表
        private void LoadVariables()
        {
            DataGridViewDefineVar.Rows.Clear();
            if (SingletonStatus.Instance.Obj != null)
            {
                foreach (var obj in SingletonStatus.Instance.Obj)
                {
                    if (obj is VarItem v)
                    {
                        DataGridViewDefineVar.Rows.Add(v.VarName, v.VarType, v.VarText);
                    }
                }
            }
        }

        private void Clean_Click(object sender, EventArgs e)
        {
            int rowIndex = -1;
            // 优先用 SelectedRows
            if (DataGridViewDefineVar.SelectedRows.Count > 0)
            {
                rowIndex = DataGridViewDefineVar.SelectedRows[0].Index;
            }
            // 如果没有整行选中，尝试用当前单元格
            else if (DataGridViewDefineVar.CurrentCell != null)
            {
                rowIndex = DataGridViewDefineVar.CurrentCell.RowIndex;
            }

            if (rowIndex < 0 || rowIndex >= DataGridViewDefineVar.Rows.Count || DataGridViewDefineVar.Rows[rowIndex].IsNewRow)
            {
                MessageHelper.MessageOK("请选择要删除的变量！", TType.Warn);
                return;
            }

            if (MessageHelper.MessageYes(this, "确定要删除选中的变量吗？") == DialogResult.OK)
            {
                try
                {
                    string varName = DataGridViewDefineVar.Rows[rowIndex].Cells[0].Value?.ToString();
                    var toRemove = SingletonStatus.Instance.Obj
                        .OfType<VarItem>()
                        .FirstOrDefault(x => x.VarName == varName);
                    if (toRemove != null)
                    {
                        SingletonStatus.Instance.Obj.Remove(toRemove);
                        LoadVariables();
                        MessageHelper.MessageOK("删除成功！", TType.Success);
                    }
                }
                catch (Exception ex)
                {
                    NlogHelper.Default.Error($"删除失败", ex);
                    MessageHelper.MessageOK($"删除失败：{ex.Message}", TType.Error);
                }
            }
        }

        private async void Save_Click(object sender, EventArgs e)
        {
            try
            {
                // 先清空临时变量列表
                SingletonStatus.Instance.Obj.Clear();

                // 遍历DataGridView所有有效行
                foreach (DataGridViewRow row in DataGridViewDefineVar.Rows)
                {
                    // 跳过新行或空行
                    if (row.IsNewRow) continue;

                    string varName = row.Cells["ColVarName"].Value?.ToString()?.Trim();
                    string varType = row.Cells["ColVarType"].Value?.ToString()?.Trim();
                    string varText = row.Cells["ColVarText"].Value?.ToString()?.Trim();

                    // 跳过变量名为空的行
                    if (string.IsNullOrEmpty(varName)) continue;

                    // 检查变量名是否重复（只在本次循环内）
                    if (SingletonStatus.Instance.Obj.OfType<VarItem>().Any(v => v.VarName.Equals(varName, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageHelper.MessageOK($"变量名称“{varName}”重复，无法保存。", TType.Warn);
                        return;
                    }

                    SingletonStatus.Instance.Obj.Add(new VarItem
                    {
                        VarName = varName,
                        VarType = varType,
                        VarText = varText
                    });
                    await JsonManager.UpdateConfigAsync(config =>
                    {
                        // 清空并写入自定义参数
                        config.Variable.Clear();
                        config.Variable.AddRange(SingletonStatus.Instance.Obj.OfType<VarItem>());
                        return Task.CompletedTask;
                    });
                }

                LoadVariables();
                MessageHelper.MessageOK("保存成功！", TType.Success);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"添加变量失败", ex);
                MessageHelper.MessageOK($"添加变量失败：{ex.Message}", TType.Error);
            }
        }

    }
}
