using System.Text;

namespace MainUI.Procedure.DSL
{
    public partial class FrmStepEdit : UIForm
    {
        private readonly string Path;
        public FrmStepEdit(string Path, string ModelType, string ModelName, string ProcessName)
        {
            InitializeComponent();
            this.Path = Path;
            Text = $"产品类型：{ModelType}，产品型号：{ModelName}，项点名称：{ProcessName}";
            CreateFile(ModelType, ModelName, ProcessName);
        }

        private void FrmStepEdit_Load(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new(Path, FileMode.Open, FileAccess.Read);
                StreamReader sr = new(fs, Encoding.UTF8);
                txtDSL.Text = sr.ReadToEnd();
                sr.Dispose();
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"DSL逻辑加载错误：{ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new(Path, FileMode.Create, FileAccess.Write);
                fs.Dispose();
                using (StreamWriter sw = new(Path, true, Encoding.UTF8))
                {
                    sw.Write($"{txtDSL.Text}");
                }
                MessageHelper.MessageOK("保存成功!");
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"保存DSL逻辑失败：{ex.Message}");
            }
        }

        private void btnCanl_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        /// <summary>
        /// 根据数据库配置自动生成 项点rw1文件
        /// </summary>
        /// <param name="ModelName">型号</param>
        /// <param name="ProcessName">试验项点名称</param>
        private static void CreateFile(string ModelType, string ModelName, string ProcessName)
        {
            try
            {
                string ModelNamePath = $"{Application.StartupPath}Procedure\\{ModelType}\\{ModelName}";
                string DSLNamePath = $"{ModelNamePath}\\{ProcessName}.rw1";
                if (!Directory.Exists(ModelNamePath))
                    Directory.CreateDirectory(ModelNamePath);
                if (!File.Exists(DSLNamePath))
                {
                    File.Create(DSLNamePath).Dispose();
                    using (new StreamWriter(DSLNamePath, true, Encoding.GetEncoding("UTF-8"))) { }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"自动创建试验项点错误：{ex.Message}");
            }
        }
    }
}
