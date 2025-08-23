using MainUI.Procedure.DSL.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// 全局变量管理器
    /// 同时支持静态方法（兼容性）和实例方法（推荐）
    /// </summary>
    public class GlobalVariableManager(IWorkflowStateService workflowState)
    {
        private readonly IWorkflowStateService _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));


        #region 实例方法（推荐使用）
        /// <summary>
        /// 获取所有变量
        /// </summary>
        public List<VarItem_Enhanced> GetAllVariables()
        {
            return _workflowState.GetAllVariables();
        }

        /// <summary>
        /// 通过名称查找变量
        /// </summary>
        public VarItem_Enhanced FindVariableByName(string varName)
        {
            if (string.IsNullOrEmpty(varName))
                return null;

            return _workflowState.FindVariableByName(varName);
        }

        /// <summary>
        /// 安全查找变量（不抛异常）
        /// </summary>
        public VarItem_Enhanced TryFindVariableByName(string varName)
        {
            try
            {
                return FindVariableByName(varName);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 添加或更新变量
        /// </summary>
        public void AddOrUpdateVariable(VarItem_Enhanced variable)
        {
            ArgumentNullException.ThrowIfNull(variable);

            var existing = TryFindVariableByName(variable.VarName);
            if (existing != null)
            {
                // 更新现有变量
                existing.VarType = variable.VarType;
                existing.VarValue = variable.VarValue;
                existing.VarText = variable.VarText;
                existing.LastUpdated = DateTime.Now;
            }
            else
            {
                // 添加新变量
                _workflowState.AddVariable(variable);
            }
        }

        /// <summary>
        /// 删除变量
        /// </summary>
        public bool RemoveVariable(string varName)
        {
            var variable = TryFindVariableByName(varName);
            if (variable != null)
            {
                return _workflowState.RemoveVariable(variable);
            }
            return false;
        }

        /// <summary>
        /// 检查变量冲突
        /// </summary>
        public VariableConflictInfo CheckVariableConflict(string varname, int excludeStepIndex)
        {
            var variable = TryFindVariableByName(varname);
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
                    ConflictStepInfo = variable.AssignedByStepInfo ?? "",
                    AssignmentType = (VariableAssignmentType)variable.AssignmentType
                };
            }

            return new VariableConflictInfo { HasConflict = false };
        }

        /// <summary>
        /// 验证步骤索引
        /// </summary>
        public bool ValidateStepIndex(int stepIndex)
        {
            return _workflowState.ValidateStepIndex(stepIndex);
        }

        /// <summary>
        /// 获取未被赋值的变量
        /// </summary>
        public List<VarItem_Enhanced> GetUnassignedVariables()
        {
            return GetAllVariables().Where(v => !v.IsAssignedByStep).ToList();
        }

        /// <summary>
        /// 获取被赋值的变量
        /// </summary>
        public List<VarItem_Enhanced> GetAssignedVariables()
        {
            return GetAllVariables().Where(v => v.IsAssignedByStep).ToList();
        }

        /// <summary>
        /// 清空所有变量
        /// </summary>
        public void ClearAllVariables()
        {
            _workflowState.ClearVariables();
        }

        #endregion

        #region 辅助数据类

        /// <summary>
        /// 变量赋值信息
        /// </summary>
        public class VariableAssignment
        {
            /// <summary>
            /// 变量名称
            /// </summary>
            public string VariableName { get; set; }

            /// <summary>
            /// 赋值描述（如"PLC读取(Module1.Tag1)"）
            /// </summary>
            public string AssignmentDescription { get; set; }

            /// <summary>
            /// 额外信息（可选）
            /// </summary>
            public string ExtraInfo { get; set; }
        }

        // 最简单的 VariableConflictInfo 实现
        public class VariableConflictInfo
        {
            public bool HasConflict { get; set; } = false;
            public int ConflictStepIndex { get; set; } = -1;
            public string ConflictStepInfo { get; set; } = "";
            public VariableAssignmentType AssignmentType { get; set; } = VariableAssignmentType.None;
        }

        public enum VariableAssignmentType
        {
            None = 0,
            PLCRead = 1,
            Expression = 2
        }

        /// <summary>
        /// 当前步骤信息
        /// </summary>
        public class CurrentStepInfo
        {
            /// <summary>
            /// 是否有效
            /// </summary>
            public bool IsValid { get; set; }

            /// <summary>
            /// 步骤索引
            /// </summary>
            public int StepIndex { get; set; }

            /// <summary>
            /// 步骤对象
            /// </summary>
            public ChildModel Step { get; set; }

            /// <summary>
            /// 步骤名称
            /// </summary>
            public string StepName { get; set; }
        }

        #endregion

    }
}


