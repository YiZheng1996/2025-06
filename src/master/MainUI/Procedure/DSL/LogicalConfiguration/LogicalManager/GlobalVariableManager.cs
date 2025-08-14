namespace MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// 全局变量管理器 - 提供统一的变量操作接口
    /// </summary>
    public static class GlobalVariableManager
    {
        /// <summary>
        /// 获取所有变量
        /// </summary>
        public static List<VarItem_Enhanced> GetAllVariables()
        {
            return [.. SingletonStatus.Instance.Obj.OfType<VarItem_Enhanced>()];
        }

        /// <summary>
        /// 通过名称查找变量
        /// </summary>
        public static VarItem_Enhanced FindVariableByName(string varName)
        {
            return GetAllVariables().FirstOrDefault(v => v.VarName == varName);
        }

        /// <summary>
        /// 检查变量是否被其他步骤赋值
        /// </summary>
        public static VariableConflictInfo CheckVariableConflict(string varname, int excludeStepIndex)
        {
            var variable = FindVariableByName(varname);
            if (variable == null)
            {
                return new VariableConflictInfo { HasConflict = false };
            }

            if (variable.IsAssignedByStep && variable.AssignedByStepIndex != excludeStepIndex)
            {
                return new VariableConflictInfo
                {
                    HasConflict = true,
                    ConflictStepIndex = variable.AssignedByStepIndex,
                    ConflictStepInfo = variable.AssignedByStepInfo,
                    AssignmentType = variable.AssignmentType
                };
            }

            return new VariableConflictInfo { HasConflict = false };
        }

        /// <summary>
        /// 获取未被赋值的变量列表
        /// </summary>
        public static List<VarItem_Enhanced> GetUnassignedVariables()
        {
            return GetAllVariables().Where(v => !v.IsAssignedByStep).ToList();
        }

        /// <summary>
        /// 获取被指定步骤赋值的变量列表
        /// </summary>
        public static List<VarItem_Enhanced> GetVariablesAssignedByStep(int stepIndex)
        {
            return GetAllVariables().Where(v => v.IsAssignedByStep && v.AssignedByStepIndex == stepIndex).ToList();
        }

        /// <summary>
        /// 清除指定步骤的所有变量赋值
        /// </summary>
        public static void ClearStepAssignments(int stepIndex)
        {
            var variables = GetVariablesAssignedByStep(stepIndex);
            foreach (var variable in variables)
            {
                variable.ClearAssignmentStatus();
            }
        }
    }

    /// <summary>
    /// 变量冲突信息
    /// </summary>
    public class VariableConflictInfo
    {
        public bool HasConflict { get; set; }
        public int ConflictStepIndex { get; set; }
        public string ConflictStepInfo { get; set; }
        public VariableAssignmentType AssignmentType { get; set; }
    }
}
