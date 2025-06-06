namespace MainUI.Config
{
    public class InverterControlConfig : IniConfig
    {
        public InverterControlConfig()
          : base(Application.StartupPath + "config\\InverterControl.ini")
        {
        }
        public InverterControlConfig(string sectionName)
            : base(Application.StartupPath + "config\\InverterControl.ini")
        {
            SetSectionName(sectionName);
            Load();
        }

        /// <summary>
        /// 控制命令
        /// </summary>
        [IniKeyName("控制命令")]
        public string ControlCommand { get; set; }

        /// <summary>
        /// 频率设定
        /// </summary>
        [IniKeyName("频率设定")]
        public string FrequencySetting { get; set; }

        /// <summary>
        /// 电压设定
        /// </summary>
        [IniKeyName("电压设定")]
        public string VoltageSetting { get; set; }

        /// <summary>
        /// 运行指令选择
        /// </summary>
        [IniKeyName("运行指令选择")]
        public string RunningInstructions { get; set; }

        /// <summary>
        /// 主频率指令输入选择
        /// </summary>
        [IniKeyName("主频率指令输入选择")]
        public string MainFrequency { get; set; }

        /// <summary>
        /// V/F分离的电压源
        /// </summary>
        [IniKeyName("V/F分离的电压源")]
        public string SeparateVoltage { get; set; }
    }
}
