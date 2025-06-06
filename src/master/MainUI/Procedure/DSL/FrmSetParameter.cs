using System.Configuration;

namespace MainUI.Procedure.DSL
{
    public partial class FrmSetParameter : UIForm
    {
        string LstName;
        public FrmSetParameter(string LstName)
        {
            InitializeComponent();
            InitSerialPortParameters();
            this.LstName = LstName;
            InitSaveParameters();
            Text = $"{LstName}-参数更改";
        }

        private void InitSerialPortParameters()
        {
            try
            {
                LoadingParameters(ConfigurationManager.AppSettings["SerialPort"].Split(','), cboSerialPort);
                LoadingParameters(ConfigurationManager.AppSettings["BaudRate"].Split(','), cboBaudRate);
                LoadingParameters(ConfigurationManager.AppSettings["DataBits"].Split(','), cboDataBits);
                LoadingParameters(ConfigurationManager.AppSettings["Parity"].Split(','), cboParity);
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK("加载串口参数错误：" + ex.Message);
            }
        }

        /// <summary>
        /// 加载单个参数
        /// </summary>
        private void LoadingParameters(string[] array, UIComboBox box)
        {
            try
            {
                foreach (string item in array)
                {
                    box.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK("加载单个参数错误：" + ex.Message);
            }
        }

        /// <summary>
        /// 加载保存参数
        /// </summary>
        private void InitSaveParameters()
        {
            try
            {
                var module = new ModuleParameterConfig(LstName);
                GetParity(module.Parity, cboParity);
                cboSerialPort.Text = module.SerialPort;
                cboBaudRate.Text = module.BaudRate.ToString();
                cboDataBits.Text = module.DataBits.ToString();
                StopBits1.Checked = module.StopBits == 1;
                StopBits2.Checked = module.StopBits == 2;
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK("加载保护参数错误：" + ex.Message);
            }
        }

        private void GetParity(int Parity, UIComboBox uI)
        {
            switch (Parity)
            {
                case 0:
                    uI.Text = ConfigurationManager.AppSettings["Parity"].Split(',')[0];
                    break;
                case 1:
                    uI.Text = ConfigurationManager.AppSettings["Parity"].Split(',')[1];
                    break;
                case 2:
                    uI.Text = ConfigurationManager.AppSettings["Parity"].Split(',')[2];
                    break;
                default:
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var module = new ModuleParameterConfig(LstName)
            {
                SerialPort = cboSerialPort.SelectedText,
                BaudRate = cboBaudRate.SelectedText.ToInt32(),   // 设置波特率
                DataBits = cboDataBits.SelectedText.ToInt32(),      // 设置数据位数
                Parity = cboParity.SelectedIndex,        //设置奇偶校验
                StopBits = StopBits1.Checked ? 1 : 2,      // 设置停止位                 
            };
            module.Save();
            MessageHelper.MessageOK("保存成功！");
        }

        private void btnCance_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
