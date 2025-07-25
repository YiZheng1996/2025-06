﻿namespace MainUI.Procedure.DSL.LogicalConfiguration.Parameter
{
    public class Parameter_WritePLC
    {
        public List<PlcWriteItem> Items { get; set; } = new();
    }

    public class PlcWriteItem
    {
        public string PlcName { get; set; }
        public string PlcValue { get; set; }

        //public string PLCAddress { get; set; }      // PLC地址
        //public string RegisterAddress { get; set; } // 寄存器地址
        //public string WriteValue { get; set; }      // 写入值
        //public string DataType { get; set; }        // 数据类型
    }
}

