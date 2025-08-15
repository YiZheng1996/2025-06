namespace MainUI.Procedure.DSL.LogicalConfiguration.Parameter
{
    /// <summary>
    /// PLC读取参数
    /// </summary>
    public class Parameter_ReadPLC
    {
        public List<PlcReadItem> Items { get; set; } = [];
    }

    /// <summary>
    /// PLC读取项
    /// </summary>
    public class PlcReadItem
    {
        public string PlcModuleName { get; set; }   // PLC模块名称
        public string PlcKeyName { get; set; }      // PLC键名称

        // 运行时解析的变量引用（不序列化）
        [Newtonsoft.Json.JsonIgnore]
        public VarItem_Enhanced TargetVariable { get; set; }

        // 为了向后兼容，保留名称属性
        public string TargetVarName
        {
            get => TargetVariable?.VarName ?? "";
            set
            {
                // 通过名称查找变量并设置引用
                if (!string.IsNullOrEmpty(value))
                {
                    var variables = SingletonStatus.Instance.Obj.OfType<VarItem_Enhanced>().ToList();
                    TargetVariable = variables.FirstOrDefault(v => v.VarName == value);
                }
            }
        }
    }
}