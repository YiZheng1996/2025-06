namespace MainUI.Config
{
    internal class ModuleParameterConfig : IniConfig
    {
        public ModuleParameterConfig(string NameConfig)
        : base($"{Application.StartupPath}MyModules\\{NameConfig}.ini")
        {
            Load();
        }

        /// <summary>
        /// 串口号
        /// </summary>
        [IniKeyName("串口号")]
        public string SerialPort { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        [IniKeyName("波特率")]
        public int BaudRate { get; set; }

        /// <summary>
        /// 数据位 5、6、7、8
        /// </summary>
        [IniKeyName("数据位")]
        public int DataBits { get; set; }

        /// <summary>
        /// 奇、偶校验位 
        /// </summary>
        [IniKeyName("校验位")]
        public int Parity { get; set; }

        /// <summary>
        /// 停止位 1、2
        /// </summary>
        [IniKeyName("停止位")]
        public int StopBits { get; set; }

        /// <summary>
        /// DTR
        /// </summary>
        [IniKeyName("DTR")]
        public bool Dtr { get; set; }

        /// <summary>
        ///  获取或设置写入操作未完成时发生超时之前的毫秒数。
        /// </summary>
        [IniKeyName("读超时(毫秒)")]
        public int ReadTimeout => 5000;
    }
}
