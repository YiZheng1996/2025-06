namespace MainUI.Procedure.DSL.LogicalConfiguration.Parameter
{
    internal class Parameter_WritePLC
    {
        public List<PlcWriteItem> Items { get; set; } = new();
    }

    public class PlcWriteItem
    {
        public string PlcName { get; set; }
        public string PlcValue { get; set; }
    }
}

