namespace MainUI.Config
{
    public class ServoControlConfig : IniConfig
    {
        public ServoControlConfig()
          : base(Application.StartupPath + "config\\ServoControl.ini")
        {
        }
        public ServoControlConfig(string sectionName)
            : base(Application.StartupPath + "config\\ServoControl.ini")
        {
            this.SetSectionName(sectionName);
            Load();
        }

        /// <summary>
        /// 定位位置
        /// </summary>
        [IniKeyName("定位位置")]
        public string Positioning { get; set; }

        /// <summary>
        /// 速度(转速)
        /// </summary>
        [IniKeyName("速度(转速)")]
        public string Speed { get; set; }

        /// <summary>
        /// 点动速度
        /// </summary>
        [IniKeyName("点动速度")]
        public string ElectricSpeed { get; set; }
    }
}
