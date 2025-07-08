using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Parameter
{
    public class Parameter_ReadPLC
    {
        public string PLCAddress { get; set; }      // PLC地址
        public string RegisterAddress { get; set; } // 寄存器地址
        public string DataType { get; set; }        // 数据类型
        public string SaveToVariable { get; set; }  // 保存到变量名
    }
}
