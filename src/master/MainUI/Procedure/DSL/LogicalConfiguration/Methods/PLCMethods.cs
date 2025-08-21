using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using MainUI.Procedure.DSL.LogicalConfiguration.Methods.Core;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using MainUI.Procedure.DSL.LogicalConfiguration.Services;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Methods
{
    /// <summary>
    /// PLC通信方法集合
    /// </summary>
    public class PLCMethods(IWorkflowStateService _workflowStateService) : DSLMethodBase
    {
        public override string Category => "PLC通信";
        public override string Description => "提供PLC读写等硬件交互功能";

        private readonly IWorkflowStateService _workflowStateService = _workflowStateService;

        /// <summary>
        /// PLC读取方法 - 使用新的统一错误处理
        /// </summary>
        public async Task<bool> ReadPLC(Parameter_ReadPLC param)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                if (param?.Items == null || param.Items.Count == 0)
                {
                    throw new ArgumentException("PLC读取参数为空");
                }

                var variables = _workflowStateService.GetVariables<VarItem_Enhanced>().ToList();
                int successCount = 0;

                foreach (var plc in param.Items)
                {
                    try
                    {
                        var targetVariable = variables.FirstOrDefault(v => v.VarName == plc.TargetVarName);
                        if (targetVariable == null)
                        {
                            NlogHelper.Default.Error($"目标变量不存在: {plc.TargetVarName}");
                            continue;
                        }

                        var plcValue = await PointPLCManager.Instance.ReadPLCValueAsync(
                            plc.PlcModuleName, plc.PlcKeyName);

                        targetVariable.UpdateValue(plcValue, $"PLC读取: {plc.PlcModuleName}.{plc.PlcKeyName}");
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        NlogHelper.Default.Error($"PLC读取项失败 {plc.PlcModuleName}.{plc.PlcKeyName}: {ex.Message}", ex);
                    }
                }

                // 只要有一个成功就算成功
                return successCount > 0;
            }, false);
        }

        /// <summary>
        /// PLC写入方法
        /// </summary>
        public async Task<bool> WritePLC(Parameter_WritePLC param)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                if (param?.Items == null || param.Items.Count == 0)
                {
                    throw new ArgumentException("PLC写入参数为空");
                }

                int successCount = 0;

                foreach (var plc in param.Items)
                {
                    try
                    {
                        // 解析写入值
                        var writeValue = await ResolveWriteValue(plc.PlcValue);

                        // 执行PLC写入
                        await PointPLCManager.Instance.WritePLCValueAsync(
                            plc.PlcModuleName, plc.PlcKeyName, writeValue);

                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        NlogHelper.Default.Error($"PLC写入项失败 {plc.PlcModuleName}.{plc.PlcKeyName}: {ex.Message}", ex);
                    }
                }

                return successCount > 0;
            }, false);
        }

        /// <summary>
        /// 解析写入值
        /// </summary>
        private async Task<object> ResolveWriteValue(string value)
        {
            // 实现值解析逻辑
            await Task.CompletedTask;
            return value;
        }
    }
}
