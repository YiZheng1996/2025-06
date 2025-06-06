using MainUI.Config;

namespace MainUI.Procedure
{
    public partial class ucTestParams : ucBaseManagerUI
    {
        private string ModelName => $"{txtType.Text}_{txtModel.Text}";
        ParaConfig paraconfig = new();
        public ucTestParams()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void LoadConfig()
        {
            try
            {
                paraconfig = new ParaConfig();
                paraconfig.SetSectionName(ModelName);
                paraconfig.Load();
                txtRpt.Text = paraconfig.RptFile;
                txtSprayTime.Text = paraconfig.SprayTime;
                txtSprayKpa.Text = paraconfig.SprayKpa;
                txtApplyPressure.Text = paraconfig.ApplyPressure;
                txtPressureRelief.Text = paraconfig.PressureRelief;
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK(ex.Message);
            }
        }
        /// <summary>
        /// 确定
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtModel.Text))
                {
                    MessageHelper.MessageOK($"型号未选择！");
                    return;
                }
                paraconfig.SetSectionName(ModelName);
                paraconfig.RptFile = txtRpt.Text;
                paraconfig.SprayTime = txtSprayTime.Text;
                paraconfig.SprayKpa = txtSprayKpa.Text;
                paraconfig.ApplyPressure = txtApplyPressure.Text;
                paraconfig.PressureRelief = txtPressureRelief.Text;
                paraconfig.Save();
                MessageHelper.MessageOK("保存成功。");
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK("保存失败。" + ex.Message);
            }
        }
        /// <summary>
        /// 重置
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtModel.Text))
                LoadConfig();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new()
            {
                InitialDirectory = Application.StartupPath + "reports\\",
                Filter = "Excel2007+|*.xlsx"
            };
            openFile.ShowDialog();
            if (string.IsNullOrEmpty(openFile.FileName) == false)
            {
                string path = openFile.FileName;
                string[] str = path.Split('\\');
                txtRpt.Text = str[^1];
            }
        }

        //产品选择
        private void btnProductSelection_Click(object sender, EventArgs e)
        {
            frmSpec fs = new();
            fs.ShowDialog();
            if (fs.DialogResult == DialogResult.OK)
            {
                txtType.Text = VarHelper.mTestViewModel.TypeName;
                txtModel.Text = VarHelper.mTestViewModel.ModelName;
                LoadConfig();
            }
        }

        private void btnParameter_Click(object sender, EventArgs e)
        {
            tabs1.SelectedIndex = 0;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            tabs1.SelectedIndex = 1;
        }
    }
}
