using MainUI.Procedure.DSL.LogicalConfiguration;
using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using MainUI.Procedure.DSL.LogicalConfiguration.Methods.Core;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Methods
{
    /// <summary>
    /// 变量管理方法集合
    /// </summary>
    public class VariableMethods : DSLMethodBase
    {
        public override string Category => "变量管理";
        public override string Description => "提供变量定义、赋值等变量管理功能";

        /// <summary>
        /// 变量定义方法
        /// </summary>
        public async Task<bool> DefineVar(Parameter_DefineVar param)
        {
            try
            {
                LogMethodStart(nameof(DefineVar), param);

                var singleton = SingletonStatus.Instance;
                var variables = GlobalVariableManager.GetAllVariables();

                // 检查变量是否已存在
                var existingVar = variables.FirstOrDefault(v => v.VarName == param.VarName);
                if (existingVar != null)
                {
                    // 更新现有变量
                    existingVar.VarName = param.VarName;
                    existingVar.VarType = param.VarType;
                    existingVar.UpdateValue(param.VarValue, "变量定义更新");

                    LogMethodSuccess(nameof(DefineVar), $"更新变量: {param.VarName}");
                }
                else
                {
                    // 添加新变量
                    var newVar = new VarItem_Enhanced
                    {
                        VarName = param.VarName,
                        VarValue = param.VarValue,
                        VarType = param.VarType,
                        LastUpdated = DateTime.Now,
                        IsAssignedByStep = false,
                        AssignmentType = VariableAssignmentType.None
                    };
                    singleton.Obj.Add(newVar);

                    LogMethodSuccess(nameof(DefineVar), $"新建变量: {param.VarName}");
                }

                await Task.CompletedTask;
                return true;
            }
            catch (Exception ex)
            {
                LogMethodError(nameof(DefineVar), ex);
                return false;
            }
        }

        /// <summary>
        /// 变量赋值方法
        /// </summary>
        public async Task<bool> VariableAssignment(Parameter_VariableAssignment param)
        {
            try
            {
                LogMethodStart(nameof(VariableAssignment), param);

                var targetVar = GlobalVariableManager.FindVariableByName(param.TargetVarName);

                if (targetVar == null)
                {
                    LogMethodError(nameof(VariableAssignment),
                        new ArgumentException($"目标变量 {param.TargetVarName} 不存在"));
                    return false;
                }

                // 解析赋值表达式
                object newValue = await EvaluateExpression(param.Expression,
                    GlobalVariableManager.GetAllVariables());

                // 类型转换和赋值
                targetVar.UpdateValue(ConvertValue(newValue, targetVar.VarType), "变量赋值");

                LogMethodSuccess(nameof(VariableAssignment),
                    $"{param.TargetVarName} = {targetVar.VarValue}");
                return true;
            }
            catch (Exception ex)
            {
                LogMethodError(nameof(VariableAssignment), ex);
                return false;
            }
        }

        /// <summary>
        /// 表达式求值
        /// </summary>
        private async Task<object> EvaluateExpression(string expression, List<VarItem_Enhanced> variables)
        {
            // 实现表达式求值逻辑
            await Task.CompletedTask;
            return expression; // 简化实现
        }

        /// <summary>
        /// 值类型转换
        /// </summary>
        private object ConvertValue(object value, string targetType)
        {
            // 实现类型转换逻辑
            return value;
        }
    }
}
