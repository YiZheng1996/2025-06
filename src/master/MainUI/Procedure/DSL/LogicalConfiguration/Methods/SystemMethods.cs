using MainUI.Procedure.DSL.LogicalConfiguration.Methods.Core;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Methods
{
    /// <summary>
    /// 系统工具方法集合
    /// </summary>
    public class SystemMethods : DSLMethodBase
    {
        public override string Category => "系统工具";
        public override string Description => "提供延时、提示等系统级工具方法";

        /// <summary>
        /// 延时方法
        /// </summary>
        public async Task<bool> DelayTime(Parameter_DelayTime param)
        {
            try
            {
                LogMethodStart(nameof(DelayTime), param);

                await Task.Delay(TimeSpan.FromMilliseconds(param.T));

                LogMethodSuccess(nameof(DelayTime), $"延时 {param.T} 毫秒");
                return true;
            }
            catch (Exception ex)
            {
                LogMethodError(nameof(DelayTime), ex);
                return false;
            }
        }

        /// <summary>
        /// 系统提示方法
        /// </summary>
        public async Task<bool> SystemPrompt(Parameter_SystemPrompt param)
        {
            try
            {
                LogMethodStart(nameof(SystemPrompt), param);

                // 解析提示内容中的变量引用
                string resolvedMessage = await ResolveVariablesInText(param.Message);

                // 显示提示信息
                var result = MessageHelper.MessageYes(resolvedMessage);

                // 如果需要等待用户响应
                if (param.WaitForResponse)
                {
                    bool success = result == DialogResult.OK;
                    LogMethodSuccess(nameof(SystemPrompt), $"用户响应: {success}");
                    return success;
                }

                LogMethodSuccess(nameof(SystemPrompt), "提示显示成功");
                return true;
            }
            catch (Exception ex)
            {
                LogMethodError(nameof(SystemPrompt), ex);
                return false;
            }
        }

        /// <summary>
        /// 解析文本中的变量引用
        /// </summary>
        private async Task<string> ResolveVariablesInText(string text)
        {
            // 实现变量解析逻辑
            await Task.CompletedTask;
            return text; // 简化实现
        }
    }
}
