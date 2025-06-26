using System.Reflection;

namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    internal class FormSet
    {
        public static void OpenFormByName(string formText)
        {
            try
            {
                // 1. 从FormInfo配置中获取窗体信息
                var formInfo = FormInfo.readOnlyList.FirstOrDefault(f => f.FormText == formText);
                if (formInfo == null)
                {
                    MessageHelper.MessageOK($"未找到窗体配置: {formText}");
                    return;
                }

                // 2. 获取当前执行环境信息
                var singleton = SingletonStatus.Instance;

                // 3. 构建完整的类型名称
                string fullTypeName = $"MainUI.Procedure.DSL.LogicalConfiguration.Forms.{formInfo.FormName}";
                string parameterTypeName = $"MainUI.Procedure.DSL.LogicalConfiguration.Parameter.{formInfo.FormParameter}";

                var assembly = Assembly.GetExecutingAssembly();

                // 4. 获取窗体类型
                Type formType = assembly.GetType(fullTypeName);
                if (formType == null)
                {
                    NlogHelper.Default.Error($"未找到窗体类型: {fullTypeName}");
                    MessageHelper.MessageOK($"未找到窗体类型: {fullTypeName}");
                    return;
                }

                // 5. 查找构造函数并创建实例
                try
                {
                    Form form;

                    // 获取所有公共实例构造函数
                    var constructors = formType.GetConstructors();

                    // 如果只有无参构造函数
                    if (constructors.Length == 1 && constructors[0].GetParameters().Length == 0)
                    {
                        form = (Form)Activator.CreateInstance(formType);
                    }
                    // 如果有带参数的构造函数
                    else
                    {
                        // 创建构造函数所需的参数
                        var parameters = PrepareConstructorParameters(formType, singleton);
                        form = (Form)Activator.CreateInstance(formType, parameters);
                    }

                    if (form != null)
                    {
                        form.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    string detailError = ex.InnerException?.Message ?? ex.Message;
                    NlogHelper.Default.Error($"创建窗体实例失败: {detailError}", ex);
                    MessageHelper.MessageOK($"创建窗体实例失败: {detailError}");
                }
            }
            catch (Exception ex)
            {
                string err = ex.InnerException?.Message ?? ex.Message;
                NlogHelper.Default.Error($"打开窗体失败: {err}", ex);
                MessageHelper.MessageOK($"打开窗体失败: {err}");
            }
        }

        /// <summary>
        /// 准备构造函数参数
        /// </summary>
        private static object[] PrepareConstructorParameters(Type formType, SingletonStatus singleton)
        {
            var constructor = formType.GetConstructors().First();
            var parameters = constructor.GetParameters();
            var paramValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                paramValues[i] = param.ParameterType.Name switch
                {
                    nameof(String) => string.Empty, // 字符串参数默认空字符串
                    nameof(Int32) => 0,            // 整数参数默认0
                    nameof(Boolean) => false,      // 布尔参数默认false
                    nameof(SingletonStatus) => singleton, // SingletonStatus参数传入单例
                    _ => null                      // 其他类型参数默认null
                };
            }

            return paramValues;
        }
    }

    /// <summary>
    /// lass FormStr 用于存储窗体的相关信息
    /// </summary>
    /// <param name="text">窗体显示文本</param>
    /// <param name="name">窗体名称</param>
    /// <param name="method">窗体方法</param>
    /// <param name="parameter">窗体参数</param>
    internal class FormStr(string text, string name, string method, string parameter)
    {
        /// <summary>
        /// 窗体显示文本
        /// </summary>
        public string FormText { get; set; } = text;

        /// <summary>
        /// 窗体名称
        /// </summary>
        public string FormName { get; set; } = name;

        /// <summary>
        /// 窗体方法
        /// </summary>
        public string FormMethod { get; set; } = method;

        /// <summary>
        /// 窗体参数
        /// </summary>
        public string FormParameter { get; set; } = parameter;
    }
}