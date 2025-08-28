namespace MainUI.Procedure.DSL.LogicalConfiguration.Parameter
{
    /// <summary>
    /// 项点变量赋值参数
    /// </summary>
    public class Parameter_VariableAssignment
    {
        /// <summary>
        /// 变量名称
        /// </summary>
        public string TargetVarName { get; set; }

        /// <summary>
        /// 赋值表达式
        /// </summary>
        public string Expression { get; set; } 

        /// <summary>
        /// 是否赋值
        /// </summary>
        public bool IsAssignment { get; set; }

        /// <summary>
        /// 赋值表单名称
        /// </summary>
        public string AssignmentForm { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
