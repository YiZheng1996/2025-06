﻿
namespace MainUI.Procedure.DSL.LogicalConfiguration.Parameter
{
    public class Parameter_Condition
    {
        /// <summary>
        /// 变量名
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// 比较操作符（如 ==, >, <, >=, <=, !=）
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 比较值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 条件为真时跳转的步骤号（或索引）
        /// </summary>
        public int TrueStepIndex { get; set; }

        /// <summary>
        /// 条件为假时跳转的步骤号（或索引）
        /// </summary>
        public int FalseStepIndex { get; set; }
    }
}
