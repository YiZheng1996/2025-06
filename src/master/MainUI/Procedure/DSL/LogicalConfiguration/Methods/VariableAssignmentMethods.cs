using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using MainUI.Procedure.DSL.LogicalConfiguration.Methods.Core;
using MainUI.Procedure.DSL.LogicalConfiguration.Services;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Methods
{
    public class VariableAssignmentMethods(
        IWorkflowStateService workflowStateService,
        GlobalVariableManager globalVariableManager
        ) : DSLMethodBase
    {
        public override string Category => "变量赋值";

        public override string Description => "提供全局变量赋值功能";

        private readonly IWorkflowStateService _workflowStateService = workflowStateService;
        private readonly GlobalVariableManager _globalVariableManager = globalVariableManager;



    }
}
