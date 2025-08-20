using MainUI.Procedure.DSL.LogicalConfiguration.Forms;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure
{
    /// <summary>
    /// 窗体集合管理类
    /// 现在使用窗体工厂和依赖注入
    /// </summary>
    public static class FormSet
    {
        private static FormFactory _formFactory;

        /// <summary>
        /// 初始化窗体工厂
        /// 在应用程序启动时调用
        /// </summary>
        public static void Initialize(FormFactory formFactory)
        {
            _formFactory = formFactory ?? throw new ArgumentNullException(nameof(formFactory));
        }

        /// <summary>
        /// 根据名称打开窗体 - 兼容性方法
        /// </summary>
        public static void OpenFormByName(string formName, Form parent)
        {
            try
            {
                if (_formFactory == null)
                {
                    throw new InvalidOperationException("FormSet 未初始化，请在应用程序启动时调用 Initialize 方法");
                }

                Form form = null;

                // 根据窗体名称创建对应的窗体
                switch (formName.ToLowerInvariant())
                {
                    case "变量定义":
                        form = _formFactory.CreateForm<Form_DefineVar>();
                        break;
                    case "PLC读取":
                        form = _formFactory.CreateForm<Form_ReadPLC>();
                        break;
                    case "PLC写入":
                        form = _formFactory.CreateForm<Form_WritePLC>();
                        break;
                    case "延时工具":
                        form = _formFactory.CreateForm<Form_DelayTime>();
                        break;

                    case "读取单元格":
                        form = _formFactory.CreateForm<Form_ReadCells>();
                        break;
                    case "写入单元格":
                        form = _formFactory.CreateForm<Form_WriteCells>();
                        break;
                    case "保存报表":
                        form = _formFactory.CreateForm<Form_SaveReport>();
                        break;

                    default:
                        MessageBox.Show($"未知的窗体类型: {formName}", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                if (form != null)
                {
                    // 设置父窗体（如果需要）
                    if (parent != null && !parent.IsDisposed)
                    {
                        form.Owner = parent;
                        form.StartPosition = FormStartPosition.CenterParent;
                    }

                    // 显示窗体
                    form.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开窗体时发生错误: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 新方法：使用泛型创建窗体
        /// </summary>
        public static T CreateForm<T>() where T : Form
        {
            if (_formFactory == null)
            {
                throw new InvalidOperationException("FormSet 未初始化");
            }

            return _formFactory.CreateForm<T>();
        }
    }
}
