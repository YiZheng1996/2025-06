namespace MainUI.Procedure.Curve
{
    public partial class frmCurveDatas : UIForm
    {
        public string Dickey { get; set; }
        public frmCurveDatas(Dictionary<string, DataTable> datas, string model)
        {
            InitializeComponent();
            for (int i = 0; i < datas.Count; i++)
            {
                var key = datas.ToList()[i].Key;
                var newPath = IPSubstring(key, model);
                var (Date, Time) = GetDateTime(newPath);
                dataGridView1.Rows.Add(Date, Time, key);
            }
        }

        // 获取型号后路径
        private string IPSubstring(string text, string model)
        {
            try
            {
                int index = text.IndexOf(model);
                if (index >= 0)
                {
                    string result = text[index..];
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("获取型号后路径错误：", ex);
                MessageHelper.MessageOK("获取型号后路径错误：" + ex.Message);
                return null;
            }
        }

        // 获取路径中 日期，时间
        private (string Date, string Time) GetDateTime(string path)
        {
            try
            {
                var Datas = path.Split('\\');
                return (Datas[^2], Datas[^1]);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("获取路径日期，时间错误：", ex);
                MessageHelper.MessageOK("获取路径日期，时间错误：" + ex.Message);
                return (null, null);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Dickey = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ColKey"].Value.ToString();
                if (!Dickey.IsNullOrEmpty()) DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("双击选择数据错误：", ex);
                MessageHelper.MessageOK("双击选择数据错误：" + ex.Message);
            }
        }
    }
}
