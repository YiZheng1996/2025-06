namespace MainUI.Config
{
    public class AAConfig : IniConfig
    {
        public AAConfig()
          : base(Application.StartupPath + "\\config\\Version.ini")
        {
        }
        public AAConfig(string sectionName)
            : base(Application.StartupPath + "\\config\\Version.ini")
        {
            this.SetSectionName(sectionName);
            Load();
        }
        /// <summary>
        /// 版本
        /// </summary>
        [IniKeyName("版本")]
        public string Version { get; set; }



    }
}
