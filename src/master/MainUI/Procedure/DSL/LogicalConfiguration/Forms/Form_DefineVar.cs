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
        // 修改LoadVariables方法
        private void LoadVariables()
        {
            try
            {
                DataGridViewDefineVar.Rows.Clear();
                var variables = SingletonStatus.Instance.Obj.OfType<VarItem_Enhanced>().ToList(); 
                foreach (var variable in variables)
                {
                    DataGridViewDefineVar.Rows.Add(variable.VarName, variable.VarType, variable.VarText);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载变量失败", ex);
                MessageHelper.MessageOK("加载变量失败：" + ex.Message, TType.Error);
            }
        }

        private void Clean_Click(object sender, EventArgs e)
        {
            int rowIndex = DataGridViewDefineVar.CurrentCell?.RowIndex ?? -1;
            if (rowIndex < 0 || DataGridViewDefineVar.Rows[rowIndex].IsNewRow)
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
                        .OfType<VarItem_Enhanced>()
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

                    // 检查变量名是否重复
                    if (SingletonStatus.Instance.Obj.OfType<VarItem_Enhanced>().Any(v => v.VarName.Equals(varName, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageHelper.MessageOK($"变量名称\"{varName}\"重复，无法保存。", TType.Warn);
                        return;
                    }

                    // 使用VarItem_Enhanced
                    SingletonStatus.Instance.Obj.Add(new VarItem_Enhanced
                    {
                        VarName = varName,
                        VarType = varType,
                        VarText = varText,
                        VarValue = "", // 默认值
                        LastUpdated = DateTime.Now,
                        IsAssignedByStep = false,
                        AssignedByStepIndex = -1,
                        AssignmentType = VariableAssignmentType.None
                    });

                    await JsonManager.UpdateConfigAsync(config =>
                    {
                        // 清空并写入自定义参数 - 转换为VarItem用于序列化
                        config.Variable.Clear();
                        config.Variable.AddRange(SingletonStatus.Instance.Obj.OfType<VarItem_Enhanced>().Cast<VarItem>());
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
