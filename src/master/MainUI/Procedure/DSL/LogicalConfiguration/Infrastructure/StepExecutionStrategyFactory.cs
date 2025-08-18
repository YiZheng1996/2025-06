using MainUI.Procedure.DSL.LogicalConfiguration.Methods;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure
{
    /// <summary>
    /// 步骤执行策略工厂
    /// </summary>
    public static class StepExecutionStrategyFactory
    {
        private static readonly Dictionary<string, Func<IStepExecutionStrategy>> _strategyFactories = [];

        static StepExecutionStrategyFactory()
        {
            RegisterDefaultStrategies();
        }

        /// <summary>
        /// 注册默认策略
        /// </summary>
        private static void RegisterDefaultStrategies()
        {
            // 系统工具
            Register("延时工具", () => new GenericStepExecutionStrategy
                    <Parameter_DelayTime, SystemMethods>("延时工具", "DelayTime"));
            Register("提示工具", () => new GenericStepExecutionStrategy
                    <Parameter_SystemPrompt, SystemMethods>("提示工具", "SystemPrompt"));

            // 变量管理
            Register("变量定义", () => new GenericStepExecutionStrategy
                    <Parameter_DefineVar, VariableMethods>("变量定义", "DefineVar"));
            Register("试验参数", () => new GenericStepExecutionStrategy
                    <Parameter_DefineVar, VariableMethods>("试验参数", "DefineVar"));
            Register("变量赋值", () => new GenericStepExecutionStrategy
                    <Parameter_VariableAssignment, VariableMethods>("变量赋值", "VariableAssignment"));

            // PLC通信
            Register("PLC读取", () => new GenericStepExecutionStrategy
                    <Parameter_ReadPLC, PLCMethods>("PLC读取", "ReadPLC"));
            Register("PLC写入", () => new GenericStepExecutionStrategy
                    <Parameter_WritePLC, PLCMethods>("PLC写入", "WritePLC"));

            // 检测工具
            Register("检测工具", () => new GenericStepExecutionStrategy
                    <Parameter_Detection, DetectionMethods>("检测工具", "Detection"));

            // 流程控制
            Register("条件判断", () => new GenericStepExecutionStrategy
                    <Parameter_Condition, FlowControlMethods>("条件判断", "EvaluateCondition"));

            // 报表工具
            Register("保存报表", () => new GenericStepExecutionStrategy
                    <Parameter_SaveReport, ReportMethods>("保存报表", "SaveReport"));
            Register("读取单元格", () => new GenericStepExecutionStrategy
                    <Parameter_ReadCells, ReportMethods>("读取单元格", "ReadCells"));
            Register("写入单元格", () => new GenericStepExecutionStrategy
                    <Parameter_WriteCells, ReportMethods>("写入单元格", "WriteCells"));
        }

        /// <summary>
        /// 注册策略
        /// </summary>
        public static void Register(string stepName, Func<IStepExecutionStrategy> factory)
        {
            _strategyFactories[stepName] = factory;
        }

        /// <summary>
        /// 创建策略
        /// </summary>
        public static IStepExecutionStrategy CreateStrategy(string stepName)
        {
            if (_strategyFactories.TryGetValue(stepName, out var factory))
            {
                return factory();
            }

            throw new NotSupportedException($"不支持的步骤类型: {stepName}");
        }

        /// <summary>
        /// 获取所有支持的步骤名称
        /// </summary>
        public static IEnumerable<string> GetSupportedStepNames()
        {
            return _strategyFactories.Keys;
        }

        /// <summary>
        /// 检查是否支持指定步骤
        /// </summary>
        public static bool IsSupported(string stepName)
        {
            return _strategyFactories.ContainsKey(stepName);
        }
    }
}
