namespace MainUI.Config
{
    public class ModuleConfig : IniConfig
    {
        public ModuleConfig()
        : base(Application.StartupPath + "config\\ModuleConfig.ini")
        {
            Load();
        }
        public ModuleConfig(string sectionName)
            : base(Application.StartupPath + "config\\ModuleConfig.ini")
        {
            SetSectionName(sectionName);
            Load();
        }

        /// <summary>
        /// 数据集
        /// </summary>
        [IniKeyName("数据集")]
        public List<string> ListJson { get; set; }
    }
}
