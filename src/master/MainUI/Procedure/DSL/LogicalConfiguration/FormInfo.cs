namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    internal class FormInfo
    {
        // 窗体名称, 窗体类名, 方法名, 参数类名
        private static readonly List<FormStr> infoList =
        [
            // 系统工具
            new("变量定义","Form_DefineVar","MethodCollection.Method_DefineVar","Parameter_DefineVar"),
            new("试验参数","Form_DefineVar","MethodCollection.Method_DefineVar","Parameter_DefineVar"),

            // 报表工具
            new("保存报表","Form_SaveReport","MethodCollection.","Parameter_SaveReport"),
            new("读取单元格","Form_ReadCells","MethodCollection.","Parameter_ReadCells"),
            new("写入单元格","Form_WriteCells","MethodCollection.","Parameter_WriteCells"),

            // 通用工具
            new("提示窗体","Form_SystemPrompt","MethodCollection.","Parameter_SystemPrompt"),
            new("延时工具","Form_DelayTime","MethodCollection.","Parameter_DelayTime"),
            new("PLC读取","Form_ReadPLC","MethodCollection.Method_ReadPLC","Parameter_ReadPLC"),
            new("PLC写入","Form_WritePLC","MethodCollection.Method_WritePLC","Parameter_WritePLC"),
            new("变量赋值","Form_VariableAssignment","MethodCollection.Mathod_VariableAssignment"
                ,"Parameter_VariableAssignment")
        ];

        public static ReadOnlyCollection<FormStr> readOnlyList = new(infoList);
    }
}
