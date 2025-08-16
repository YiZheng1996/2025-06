using MainUI.Procedure.DSL.LogicalConfiguration;
using MainUI.Procedure.DSL.LogicalConfiguration.Methods.Core;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Methods
{
    /// <summary>
    /// 流程控制方法集合
    /// </summary>
    public class FlowControlMethods : DSLMethodBase
    {
        public override string Category => "流程控制";
        public override string Description => "提供条件判断、循环等流程控制功能";

        /// <summary>
        /// 条件评估方法 - 返回下一步骤索引
        /// </summary>
        public async Task<int> EvaluateCondition(Parameter_Condition param)
        {
            try
            {
                LogMethodStart(nameof(EvaluateCondition), param);

                // 获取变量值
                var singleton = SingletonStatus.Instance;
                var variables = singleton.Obj.OfType<VarItem>().ToList();
                var variable = variables.FirstOrDefault(v => v.VarName == param.VarName);

                if (variable == null)
                {
                    LogMethodError(nameof(EvaluateCondition),
                        new ArgumentException($"变量 {param.VarName} 不存在"));
                    return param.FalseStepIndex;
                }

                // 执行条件比较
                bool conditionResult = await EvaluateCondition(variable.VarValue, param.Operator, param.Value);

                int nextStep = conditionResult ? param.TrueStepIndex : param.FalseStepIndex;

                LogMethodSuccess(nameof(EvaluateCondition),
                    $"{param.VarName}({variable.VarValue}) {param.Operator} {param.Value} = {conditionResult}, 下一步: {nextStep}");

                return nextStep;
            }
            catch (Exception ex)
            {
                LogMethodError(nameof(EvaluateCondition), ex);
                return param.FalseStepIndex; // 异常时走False分支
            }
        }

        /// <summary>
        /// 条件比较
        /// </summary>
        private async Task<bool> EvaluateCondition(string value, string operatorStr, string targetValue)
        {
            // 实现条件比较逻辑
            await Task.CompletedTask;
            return true; // 简化实现
        }
    }
}
