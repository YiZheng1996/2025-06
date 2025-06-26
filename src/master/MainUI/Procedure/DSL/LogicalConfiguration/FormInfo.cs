namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    internal class FormInfo
    {
        private static readonly List<FormStr> infoList =
        [
            new("延时工具","Form_DelayTime","MethodCollection.Method_DelayTime","Parameter_DelayTime"),
            new("变量定义","Form_DefineVar","MethodCollection.Method_DefineVar","Parameter_DefineVar"),
            new("PLC读取","Form_ReadPLC","MethodCollection.Method_ReadPLC","Parameter_ReadPLC"),
            new("PLC写入","Form_WritePLC","MethodCollection.Method_WritePLC","Parameter_WritePLC"),
            new("变量赋值","Form_VariableAssignment","MethodCollection.Mathod_VariableAssignment"
                ,"Parameter_VariableAssignment")
        ];

        public static ReadOnlyCollection<FormStr> readOnlyList = new(infoList);
    }
}
