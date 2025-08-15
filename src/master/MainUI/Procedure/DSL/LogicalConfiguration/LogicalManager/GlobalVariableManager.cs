using Newtonsoft.Json;
using static MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager.GlobalVariableManager;

namespace MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// 全局变量管理器 - 提供统一的变量操作接口
    /// </summary>
    public static class GlobalVariableManager
    {
        #region 通用变量数据集
        /// <summary>
        /// 获取所有变量，从SingletonStatus读取
        /// </summary>
        public static List<VarItem_Enhanced> GetAllVariables()
        {
            return SingletonStatus.Instance.Obj.OfType<VarItem_Enhanced>().ToList();
        }

        /// <summary>
        /// 通过名称查找变量
        /// </summary>
        public static VarItem_Enhanced FindVariableByName(string varName)
        {
            return GetAllVariables().FirstOrDefault(v => v.VarName == varName);
        }

        /// <summary>
        /// 添加或更新变量到临时状态
        /// </summary>
        public static void AddOrUpdateVariable(VarItem_Enhanced variable)
        {
            var existing = FindVariableByName(variable.VarName);
            if (existing != null)
            {
                // 更新现有变量
                existing.VarType = variable.VarType;
                existing.VarValue = variable.VarValue;
                existing.UpdateValue(variable.VarValue, "手动更新");
            }
            else
            {
                // 添加新变量
                SingletonStatus.Instance.Obj.Add(variable);
            }
        }

        /// <summary>
        /// 从临时状态删除变量
        /// </summary>
        public static bool RemoveVariable(string varName)
        {
            var variable = FindVariableByName(varName);
            if (variable != null)
            {
                return SingletonStatus.Instance.Obj.Remove(variable);
            }
            return false;
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
        #endregion

        #region 通用变量状态管理功能

        #region 核心状态管理方法
        /// <summary>
        /// 设置步骤的变量赋值状态
        /// </summary>
        /// <param name="stepIndex">步骤索引</param>
        /// <param name="stepName">步骤名称</param>
        /// <param name="variableAssignments">变量赋值列表</param>
        /// <param name="assignmentType">赋值类型</param>
        /// <param name="clearExisting">是否先清除现有状态</param>
        public static void SetStepVariableAssignments(
            int stepIndex,
            string stepName,
            List<VariableAssignment> variableAssignments,
            VariableAssignmentType assignmentType,
            bool clearExisting = true)
        {
            try
            {
                // 先清除当前步骤的所有赋值状态
                if (clearExisting)
                {
                    ClearStepAssignments(stepIndex);
                }

                // 设置新的赋值状态
                foreach (var assignment in variableAssignments)
                {
                    if (!string.IsNullOrEmpty(assignment.VariableName))
                    {
                        var variable = FindVariableByName(assignment.VariableName);
                        if (variable != null)
                        {
                            variable.SetAssignmentStatus(
                                stepIndex,
                                $"{assignment.AssignmentDescription}:{stepName}",
                                assignmentType
                            );

                            NlogHelper.Default.Info($"设置变量赋值状态: {assignment.VariableName} -> 步骤{stepIndex}({stepName})");
                        }
                        else
                        {
                            NlogHelper.Default.Warn($"变量 {assignment.VariableName} 不存在，无法设置赋值状态");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"设置步骤变量赋值状态失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 清除步骤的变量赋值状态
        /// </summary>
        /// <param name="stepIndex">步骤索引</param>
        public static void ClearStepVariableAssignments(int stepIndex)
        {
            try
            {
                ClearStepAssignments(stepIndex);
                NlogHelper.Default.Info($"已清除步骤{stepIndex}的所有变量赋值状态");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"清除步骤变量赋值状态失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 清除特定变量的赋值状态（仅当它是由指定步骤赋值时）
        /// </summary>
        /// <param name="variableName">变量名</param>
        /// <param name="stepIndex">步骤索引</param>
        public static void ClearSpecificVariableAssignment(string variableName, int stepIndex)
        {
            try
            {
                var variable = FindVariableByName(variableName);
                if (variable != null && variable.IsAssignedByStep && variable.AssignedByStepIndex == stepIndex)
                {
                    variable.ClearAssignmentStatus();
                    NlogHelper.Default.Info($"清除变量赋值状态: {variableName} (来自步骤{stepIndex})");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"清除变量赋值状态失败: {ex.Message}", ex);
                throw;
            }
        }

        #endregion

        #region 冲突检查和验证

        /// <summary>
        /// 批量检查变量冲突
        /// </summary>
        /// <param name="variableNames">要检查的变量名列表</param>
        /// <param name="excludeStepIndex">排除的步骤索引</param>
        /// <returns>冲突信息列表</returns>
        public static List<VariableConflictResult> CheckVariableConflicts(
            List<string> variableNames,
            int excludeStepIndex)
        {
            var conflicts = new List<VariableConflictResult>();

            try
            {
                foreach (var varName in variableNames.Where(v => !string.IsNullOrEmpty(v)))
                {
                    var conflictInfo = CheckVariableConflict(varName, excludeStepIndex);
                    if (conflictInfo.HasConflict)
                    {
                        conflicts.Add(new VariableConflictResult
                        {
                            VariableName = varName,
                            ConflictInfo = conflictInfo,
                            ConflictMessage = $"变量 {varName} 已被步骤 {conflictInfo.ConflictStepInfo} 赋值"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"检查变量冲突失败: {ex.Message}", ex);
            }

            return conflicts;
        }

        /// <summary>
        /// 显示冲突警告对话框
        /// </summary>
        /// <param name="conflicts">冲突列表</param>
        /// <param name="parentForm">父窗体</param>
        /// <returns>用户是否选择继续</returns>
        public static bool ShowConflictWarning(List<VariableConflictResult> conflicts, Form parentForm = null)
        {
            if (conflicts.Count == 0) return true;

            try
            {
                string warningMessage = string.Join("\n", conflicts.Select(c => c.ConflictMessage));
                warningMessage += "\n\n是否继续保存？";

                var result = parentForm != null
                    ? MessageHelper.MessageYes(parentForm, warningMessage)
                    : MessageHelper.MessageYes(warningMessage);

                return result == DialogResult.OK;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"显示冲突警告失败: {ex.Message}", ex);
                return false;
            }
        }

        #endregion

        #region 参数处理助手

        /// <summary>
        /// 从步骤参数中提取变量赋值信息
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="stepParameter">步骤参数</param>
        /// <param name="extractorFunc">提取函数</param>
        /// <returns>变量赋值列表</returns>
        public static List<VariableAssignment> ExtractVariableAssignments<T>(
            object stepParameter,
            Func<T, List<VariableAssignment>> extractorFunc) where T : class
        {
            try
            {
                if (TryGetParameter<T>(stepParameter, out var param))
                {
                    return extractorFunc(param);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"提取变量赋值信息失败: {ex.Message}", ex);
            }

            return [];
        }

        /// <summary>
        /// 尝试从步骤参数中获取指定类型的参数对象
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="stepParameter">步骤参数对象</param>
        /// <param name="parameter">输出的参数对象</param>
        /// <returns>是否成功获取</returns>
        public static bool TryGetParameter<T>(object stepParameter, out T parameter) where T : class
        {
            parameter = null;

            if (stepParameter == null)
                return false;

            try
            {
                // 如果直接是目标类型
                if (stepParameter is T directParam)
                {
                    parameter = directParam;
                    return true;
                }

                // 如果是JSON字符串，尝试反序列化
                if (stepParameter is string jsonStr && !string.IsNullOrEmpty(jsonStr))
                {
                    parameter = JsonConvert.DeserializeObject<T>(jsonStr);
                    return parameter != null;
                }

                // 如果是其他对象，尝试先序列化再反序列化
                if (stepParameter != null)
                {
                    var jsonString = JsonConvert.SerializeObject(stepParameter);
                    parameter = JsonConvert.DeserializeObject<T>(jsonString);
                    return parameter != null;
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Debug($"参数转换失败 {typeof(T).Name}: {ex.Message}");
            }

            return false;
        }

        #endregion

        #region 步骤信息获取

        /// <summary>
        /// 获取当前步骤信息
        /// </summary>
        /// <returns>当前步骤信息</returns>
        public static CurrentStepInfo GetCurrentStepInfo()
        {
            try
            {
                var steps = SingletonStatus.Instance.IempSteps;
                int currentStepIndex = SingletonStatus.Instance.StepNum;

                if (steps != null && currentStepIndex >= 0 && currentStepIndex < steps.Count)
                {
                    var currentStep = steps[currentStepIndex];
                    return new CurrentStepInfo
                    {
                        IsValid = true,
                        StepIndex = currentStepIndex,
                        Step = currentStep,
                        StepName = currentStep.StepName
                    };
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"获取当前步骤信息失败: {ex.Message}", ex);
            }

            return new CurrentStepInfo { IsValid = false };
        }

        /// <summary>
        /// 验证步骤有效性
        /// </summary>
        /// <param name="stepIndex">步骤索引</param>
        /// <returns>是否有效</returns>
        public static bool ValidateStepIndex(int stepIndex)
        {
            var steps = SingletonStatus.Instance.IempSteps;
            return steps != null && stepIndex >= 0 && stepIndex < steps.Count;
        }

        #endregion
    }

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

    /// <summary>
    /// 变量冲突结果
    /// </summary>
    public class VariableConflictResult
    {
        /// <summary>
        /// 变量名称
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// 冲突信息
        /// </summary>
        public VariableConflictInfo ConflictInfo { get; set; }

        /// <summary>
        /// 冲突消息
        /// </summary>
        public string ConflictMessage { get; set; }
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

    #endregion
}


