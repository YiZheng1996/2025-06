namespace MainUI.Procedure.Controls
{
    public partial class UcRainyManual : UserControl
    {
        public UcRainyManual()
        {
            InitializeComponent();
            switchPictureBox.Click += SwitchPictureBox_Click;
        }

        public event EventHandler OpenClick;
        private void SwitchPictureBox_Click(object sender, EventArgs e)
        {
            OpenClick?.Invoke(this, e);
        }

        private string _titleName;
        /// <summary>
        /// 标题名称
        /// </summary>
        public string TitleName
        {
            get { return _titleName; }
            set
            {
                _titleName = value;
                TitlePanel.Text = _titleName;
            }
        }

        private bool _isopen;
        /// <summary>
        /// 电磁阀开关状态
        /// </summary>
        public bool IsOpen
        {
            get { return _isopen; }
            set
            {
                _isopen = value;
                switchPictureBox.Switch = _isopen;
            }
        }

        private string _solenoidname;
        /// <summary>
        /// 电磁阀名称
        /// </summary>
        public string SolenoidName
        {
            get { return _solenoidname; }
            set
            {
                _solenoidname = value;
                switchPictureBox.Text = _solenoidname;
            }
        }

        private double _pressure;
        /// <summary>
        /// 气压传感器值
        /// </summary>
        public double Pressure
        {
            get { return _pressure; }
            set
            {
                _pressure = value;
                LabPressure.Value = _pressure;
            }
        }

        private string _pressurename;
        /// <summary>
        /// 气压传感器值名称
        /// </summary>
        public string PressureName
        {
            get { return _pressurename; }
            set
            {
                _pressurename = value;
                LabPressureName.Text = _pressurename;
            }
        }

        private double _internettraffic;
        /// <summary>
        /// 流量传感器值
        /// </summary>
        public double InternetTraffic
        {
            get { return _internettraffic; }
            set
            {
                _internettraffic = value;
                LabInternetTraffic.Value = _internettraffic;
            }
        }

        private string _internettrafficname;
        /// <summary>
        /// 流量传感器值
        /// </summary>
        public string InternetTrafficName
        {
            get { return _internettrafficname; }
            set
            {
                _internettrafficname = value;
                LabInternetTrafficName.Text = _internettrafficname;
            }
        }


    }
}
