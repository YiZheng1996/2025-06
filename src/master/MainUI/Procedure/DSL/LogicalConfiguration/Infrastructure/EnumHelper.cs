using System.ComponentModel;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举的Description特性值
        /// </summary>
        public static string GetDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var descriptionAttribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
            return descriptionAttribute?.Description ?? enumValue.ToString();
        }

        /// <summary>
        /// 获取枚举的所有显示项
        /// </summary>
        public static List<EnumDisplayItem> GetDisplayItems<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new EnumDisplayItem
                {
                    Value = e,
                    DisplayName = e.GetDescription()
                })
                .ToList();
        }
    }

    /// <summary>
    /// 枚举显示项
    /// </summary>
    public class EnumDisplayItem
    {
        public object Value { get; set; }
        public string DisplayName { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
