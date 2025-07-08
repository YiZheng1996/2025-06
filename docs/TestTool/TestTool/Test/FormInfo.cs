using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTool.Forms;

namespace TestTool.Test
{
    internal class FormInfo
    {
        private static List<FormStr> infoList = new List<FormStr>()
        {
            new FormStr("变量定义","Form_DefineVar","MethodCollection.Method_DefineVar","Parameter_DefineVar"),
            new FormStr("PLC读取","Form_ReadPLC","MethodCollection.Method_ReadPLC","Parameter_ReadPLC"),
            new FormStr("PLC写入","Form_WritePLC","MethodCollection.Method_WritePLC","Parameter_WritePLC"),
            new FormStr("延时工具","Form_DelayTime","MethodCollection.Method_DelayTime","Parameter_DelayTime"),
            new FormStr("变量赋值","Form_VariableAssignment","MethodCollection.Mathod_VariableAssignment","Parameter_VariableAssignment")

        };
        public static ReadOnlyCollection<FormStr> readOnlyList = new ReadOnlyCollection<FormStr>(infoList);

    }
}
