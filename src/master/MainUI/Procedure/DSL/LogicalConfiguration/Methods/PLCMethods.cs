using MainUI.Procedure.DSL.LogicalConfiguration;
using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using MainUI.Procedure.DSL.LogicalConfiguration.Methods.Core;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Methods
{
    /// <summary>
    /// PLC通信方法集合
    /// </summary>
    public class PLCMethods : DSLMethodBase
    {
        public override string Category => "PLC通信";
        public override string Description => "提供PLC读写等硬件交互功能";

        /// <summary>
        /// PLC读取方法
        /// </summary>
        public async Task<bool> ReadPLC(Parameter_ReadPLC param)
        {
            try
            {
                LogMethodStart(nameof(ReadPLC), param);

                if (param?.Items == null || param.Items.Count == 0)
                {
                    LogMethodError(nameof(ReadPLC),
                        new ArgumentException("PLC读取参数为空"));
                    return false;
                }

                var variables = SingletonStatus.Instance.Obj.OfType<VarItem_Enhanced>().ToList();
                int successCount = 0;

                foreach (var plc in param.Items)
                {
                    try
                    {
                        // 通过名称查找目标变量
                        var targetVariable = variables.FirstOrDefault(v => v.VarName == plc.TargetVarName);
                        if (targetVariable == null)
                        {
                            NlogHelper.Default.Error($"目标变量不存在: {plc.TargetVarName}");
                            continue;
                        }

                        // 使用统一的PLC管理器读取值
                        var plcValue = await PointPLCManager.Instance.ReadPLCValueAsync(
                            plc.PlcModuleName, plc.PlcKeyName);

                        // 更新变量值
                        targetVariable.UpdateValue(plcValue,
                            $"PLC读取: {plc.PlcModuleName}.{plc.PlcKeyName}");

                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        NlogHelper.Default.Error($"PLC读取项失败 {plc.PlcModuleName}.{plc.PlcKeyName}: {ex.Message}", ex);
                    }
                }

                bool success = successCount > 0;
                LogMethodSuccess(nameof(ReadPLC), $"成功读取 {successCount}/{param.Items.Count} 项");
                return success;
            }
            catch (Exception ex)
            {
                LogMethodError(nameof(ReadPLC), ex);
                return false;
            }
        }

        /// <summary>
        /// PLC写入方法
        /// </summary>
        public async Task<bool> WritePLC(Parameter_WritePLC param)
        {
            try
            {
                LogMethodStart(nameof(WritePLC), param);

                if (param?.Items == null || param.Items.Count == 0)
                {
                    LogMethodError(nameof(WritePLC),
                        new ArgumentException("PLC写入参数为空"));
                    return false;
                }

                // 使用统一的PLC管理器进行批量写入
                var successCount = await PointPLCManager.Instance.BatchWritePLCAsync(param.Items);

                bool success = successCount > 0;
                LogMethodSuccess(nameof(WritePLC), $"成功写入 {successCount}/{param.Items.Count} 项");
                return success;
            }
            catch (Exception ex)
            {
                LogMethodError(nameof(WritePLC), ex);
                return false;
            }
        }
    }
}
