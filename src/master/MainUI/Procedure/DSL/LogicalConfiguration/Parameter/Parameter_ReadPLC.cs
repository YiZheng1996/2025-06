namespace MainUI.Procedure.DSL.LogicalConfiguration.Parameter
{
    /// <summary>
    /// PLC读取参数集合类
    /// </summary>
    public class Parameter_ReadPLC
    {
        public List<PlcReadItem> Items { get; set; } = [];
    }

    public class PlcReadItem
    {
        public string PlcModuleName { get; set; }   // PLC模块名称

        public string PlcKeyName { get; set; }      // PLC键名称

        public string TargetVarName { get; set; }        // PLC值
    }
}