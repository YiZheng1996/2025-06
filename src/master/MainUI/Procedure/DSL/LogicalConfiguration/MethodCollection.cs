using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using RW.Modules;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    /// <summary>
    /// DSL执行引擎的核心方法集合
    /// </summary>
    public static class MethodCollection
    {
        #region 1. 延时工具 - 最简单的实现
        /// <summary>
        /// 延时方法 - 用于验证基础架构
        /// </summary>
        public static async Task<bool> Method_DelayTime(Parameter_DelayTime param)
        {
            try
            {
                // 记录开始日志
                NlogHelper.Default.Info($"开始延时 {param.T} 秒");

                // 执行延时
                await Task.Delay(TimeSpan.FromMilliseconds(param.T));

                // 记录完成日志
                NlogHelper.Default.Info($"延时 {param.T} 秒完成");
                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"延时执行失败: {ex.Message}", ex);
                return false;
            }
        }
        #endregion

        #region 2. 变量管理 - 涉及状态管理
        /// <summary>
        /// 变量定义方法
        /// </summary>
        public static async Task<bool> Method_DefineVar(Parameter_DefineVar param)
        {
            try
            {
                var singleton = SingletonStatus.Instance;
                var variables = GetVariables();

                // 检查变量是否已存在
                var existingVar = variables.FirstOrDefault(v => v.VarName == param.VarName);
                if (existingVar != null)
                {
                    // 更新现有变量
                    existingVar.VarName = param.VarName;
                    existingVar.VarType = param.VarType;
                    NlogHelper.Default.Info($"更新变量: {param.VarName} = {param.VarValue} ({param.VarType})");
                }
                else
                {
                    // 添加新变量
                    var newVar = new VarItem
                    {
                        VarName = param.VarName,
                        VarValue = param.VarValue,
                        VarType = param.VarType
                    };
                    singleton.Obj.Add(newVar);
                    NlogHelper.Default.Info($"定义新变量: {param.VarName} = {param.VarValue} ({param.VarType})");
                }

                await Task.CompletedTask; // 保持异步接口一致性
                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"变量定义失败: {ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 变量赋值方法
        /// </summary>
        public static async Task<bool> Method_VariableAssignment(Parameter_VariableAssignment param)
        {
            try
            {
                var targetVar = FindVariable(param.TargetVarName);

                if (targetVar == null)
                {
                    NlogHelper.Default.Error($"目标变量 {param.TargetVarName} 不存在");
                    return false;
                }

                // 解析赋值表达式
                object newValue = await EvaluateExpression(param.Expression, GetVariables());

                // 类型转换和赋值
                targetVar.VarValue = ConvertValue(newValue, targetVar.VarType).ToString();

                NlogHelper.Default.Info($"变量赋值: {param.TargetVarName} = {targetVar.VarValue}");
                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"变量赋值失败: {ex.Message}", ex);
                return false;
            }
        }
        #endregion

        #region 3. PLC通信 - 硬件交互
        /// <summary>
        /// PLC读取方法
        /// </summary>
        //public static Task<bool> Method_ReadPLC(Parameter_ReadPLC param)
        //{
        //    try
        //    {
        //        //// 获取PLC通信客户端 (需要根据您的实际PLC库调整)
        //        //var plcClient = PLCManager.GetClient(param.PLCAddress);
        //        //if (plcClient == null)
        //        //{
        //        //    NlogHelper.Default.Error($"无法连接到PLC: {param.PLCAddress}");
        //        //    return false;
        //        //}

        //        //// 读取PLC数据
        //        //var result = await plcClient.ReadAsync(param.RegisterAddress, param.DataType);
        //        //if (!result.IsSuccess)
        //        //{
        //        //    NlogHelper.Default.Error($"PLC读取失败: {result.ErrorMessage}");
        //        //    return false;
        //        //}

        //        //// 将读取的值保存到变量中
        //        //if (!string.IsNullOrEmpty(param.SaveToVariable))
        //        //{
        //        //    var singleton = SingletonStatus.Instance;
        //        //    var variables = singleton.Obj.OfType<VarItem>().ToList();
        //        //    var targetVar = variables.FirstOrDefault(v => v.VarName == param.SaveToVariable);
        //        //    if (targetVar != null)
        //        //    {
        //        //        targetVar.VarValue = result.Value;
        //        //        NlogHelper.Default.Info($"PLC读取成功: {param.RegisterAddress} = {result.Value}, 保存到变量: {param.SaveToVariable}");
        //        //    }
        //        //    else
        //        //    {
        //        //        NlogHelper.Default.Error($"目标变量 {param.SaveToVariable} 不存在，无法保存PLC读取值");
        //        //    }
        //        //}

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        NlogHelper.Default.Error($"PLC读取异常: {ex.Message}", ex);
        //        return false;
        //    }
        //}

        /// <summary>
        /// PLC写入方法
        /// </summary>
        public static Task<bool> Method_WritePLC(Parameter_WritePLC param)
        {
            try
            {
                ModuleComponent.Instance.Init();
                Dictionary<string, BaseModule> DicPLC = ModuleComponent.Instance.GetList();

                // 解析写入值 (支持变量引用或直接值)
                var plcClient = param.Items;
                if (plcClient == null || plcClient.Count == 0)
                {
                    NlogHelper.Default.Error("PLC写入参数为空或未指定PLC项");
                    return Task.FromResult(false);
                }

                // 写入PLC
                foreach (var plc in plcClient)
                {
                    if (DicPLC.TryGetValue(plc.PlcModuleName, out var module))
                    {
                        DicPLC[plc.PlcModuleName].Write(plc.PlcKeyName, plc.PlcValue);
                        var writeValue = ResolveValue(plc.PlcValue).Result;
                        NlogHelper.Default.Info($"PLC写入成功: {plc.PlcModuleName}.{plc.PlcKeyName} = {writeValue}");
                    }
                    else
                    {
                        NlogHelper.Default.Error($"未找到指定的PLC模块: {plc.PlcModuleName}");
                        return Task.FromResult(false);
                    }
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"PLC写入异常: {ex.Message}", ex);
                return Task.FromResult(false);
            }
        }
        #endregion

        #region 4. 条件判断 - 流程控制核心
        /// <summary>
        /// 条件评估方法 - 返回下一步骤索引
        /// </summary>
        public static async Task<int> Method_EvaluateCondition(Parameter_Condition param)
        {
            try
            {
                // 获取变量值
                var singleton = SingletonStatus.Instance;
                var variables = singleton.Obj.OfType<VarItem>().ToList();
                var variable = variables.FirstOrDefault(v => v.VarName == param.VarName);

                if (variable == null)
                {
                    NlogHelper.Default.Error($"条件判断失败: 变量 {param.VarName} 不存在");
                    return param.FalseStepIndex; // 变量不存在时走False分支
                }

                // 执行条件比较
                bool conditionResult = await EvaluateCondition(variable.VarValue, param.Operator, param.Value);

                int nextStep = conditionResult ? param.TrueStepIndex : param.FalseStepIndex;

                NlogHelper.Default.Info($"条件判断: {param.VarName}({variable.VarValue}) {param.Operator} {param.Value} = {conditionResult}, 下一步: {nextStep}");

                return nextStep;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"条件判断异常: {ex.Message}", ex);
                return param.FalseStepIndex; // 异常时走False分支
            }
        }
        #endregion

        #region 5. 系统提示
        /// <summary>
        /// 提示窗体方法
        /// </summary>
        public static async Task<bool> Method_SystemPrompt(Parameter_SystemPrompt param)
        {
            try
            {
                // 解析提示内容中的变量引用
                string resolvedMessage = await ResolveVariablesInText(param.Message);

                // 显示提示信息
                var result = MessageHelper.MessageYes(resolvedMessage);

                NlogHelper.Default.Info($"系统提示显示: {param.Title} - {resolvedMessage}");

                // 如果需要等待用户响应
                if (param.WaitForResponse)
                {
                    return result == DialogResult.OK;
                }

                await Task.CompletedTask;
                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"系统提示失败: {ex.Message}", ex);
                return false;
            }
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 安全获取变量列表
        /// </summary>
        private static List<VarItem> GetVariables()
        {
            return [.. SingletonStatus.Instance.Obj.OfType<VarItem>()];
        }

        /// <summary>
        /// 根据名称查找变量
        /// </summary>
        private static VarItem FindVariable(string varName)
        {
            return GetVariables().FirstOrDefault(v => v.VarName == varName);
        }

        /// <summary>
        /// 表达式求值 - 支持简单的数学运算和变量引用
        /// </summary>
        private static async Task<object> EvaluateExpression(string expression, List<VarItem> variables)
        {
            // 简单实现 - 可以扩展支持更复杂的表达式
            expression = expression.Trim();

            // 如果是变量引用 (格式: {变量名})
            if (expression.StartsWith("{") && expression.EndsWith("}"))
            {
                string varName = expression.Substring(1, expression.Length - 2);
                var variable = variables.FirstOrDefault(v => v.VarName == varName);
                return variable?.VarValue ?? throw new ArgumentException($"变量 {varName} 不存在");
            }

            // 如果是直接值
            if (double.TryParse(expression, out double numValue))
                return numValue;

            // 字符串值
            return expression;
        }

        /// <summary>
        /// 条件比较评估
        /// </summary>
        private static async Task<bool> EvaluateCondition(object leftValue, string operatorStr, string rightValue)
        {
            // 类型转换
            double leftNum = Convert.ToDouble(leftValue);
            double rightNum = Convert.ToDouble(rightValue);

            return operatorStr switch
            {
                "==" => Math.Abs(leftNum - rightNum) < 0.0001,
                "!=" => Math.Abs(leftNum - rightNum) >= 0.0001,
                ">" => leftNum > rightNum,
                "<" => leftNum < rightNum,
                ">=" => leftNum >= rightNum,
                "<=" => leftNum <= rightNum,
                _ => throw new ArgumentException($"不支持的操作符: {operatorStr}")
            };
        }

        /// <summary>
        /// 解析值 - 支持变量引用和直接值
        /// </summary>
        private static async Task<object> ResolveValue(string valueExpression)
        {
            if (string.IsNullOrEmpty(valueExpression))
                return null;

            // 变量引用
            if (valueExpression.StartsWith("{") && valueExpression.EndsWith("}"))
            {
                string varName = valueExpression.Substring(1, valueExpression.Length - 2);
                var variable = FindVariable(varName);
                return variable?.VarValue;
            }

            // 直接值
            return valueExpression;
        }

        /// <summary>
        /// 解析文本中的变量引用
        /// </summary>
        private static async Task<string> ResolveVariablesInText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            string result = text;
            var variables = GetVariables();

            foreach (var variable in variables)
            {
                string placeholder = $"{{{variable.VarName}}}";
                if (result.Contains(placeholder))
                {
                    result = result.Replace(placeholder, variable.VarValue?.ToString() ?? "null");
                }
            }

            return result;
        }

        /// <summary>
        /// 值类型转换
        /// </summary>
        private static object ConvertValue(object value, string targetType)
        {
            return targetType?.ToLower() switch
            {
                "int" => Convert.ToInt32(value),
                "double" => Convert.ToDouble(value),
                "bool" => Convert.ToBoolean(value),
                "string" => value?.ToString(),
                _ => value
            };
        }
        #endregion
    }
}
