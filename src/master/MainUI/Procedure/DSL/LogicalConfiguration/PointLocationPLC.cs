using AntdUI;
using System.Text;

namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    /// <summary>
    /// PLC点位配置管理，默认地址为 Bin\Modules\MyModules.ini
    /// </summary>
    public sealed class PointLocationPLC : IniConfig
    {
        private const string MODULE_FILE_PATH = "Modules\\MyModules.ini";
        private static readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 类的延迟初始化单例实例
        /// </summary>
        /// <remarks>实例仅在首次访问时创建，以确保高效的资源使用，避免不必要的开销。
        ///</remarks>
        private static readonly Lazy<PointLocationPLC> _instance =
            new(() => new PointLocationPLC());

        /// <summary>
        /// 获取PointLocationPLC的单例实例
        /// </summary>
        public static PointLocationPLC Instance => _instance.Value;

        /// <summary>
        /// 存储所有模块配置内容的字典
        /// Key: 区段名称, Value: 该区段下的所有配置项
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, string>> _dicModelsContent;

        /// <summary>
        /// 获取模块配置内容字典
        /// </summary>
        public IReadOnlyDictionary<string, Dictionary<string, string>> DicModelsContent => _dicModelsContent;

        /// <summary>
        /// 私有构造函数，确保单例模式
        /// </summary>
        private PointLocationPLC() : base(AppPath + MODULE_FILE_PATH)
        {
            _dicModelsContent = [];
            Initialize();
        }

        /// <summary>
        /// 初始化并加载配置
        /// </summary>
        private void Initialize()
        {
            try
            {
                // 允许使用GB2312编码格式
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                LoadModelsContent();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("初始化PLC点位配置时发生错误", ex);
                MessageHelper.MessageOK($"初始化PLC点位配置时发生错误：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 获取所有配置区段
        /// </summary>
        public string[] GetSections() => Config.GetSections();

        /// <summary>
        /// 加载并更新模块配置内容
        /// </summary>
        public void LoadModelsContent()
        {
            try
            {
                // 清空之前的内容
                _dicModelsContent.Clear();

                // 获取所有配置区段
                var sections = GetSections();

                // 如果没有找到任何区段，则抛出异常
                if (sections == null || sections.Length == 0)
                {
                    throw new InvalidOperationException("没有找到任何配置区段。请检查配置文件是否正确。");
                }

                // 遍历所有区段并获取其键值对
                foreach (string section in sections)
                {
                    // 跳过空区段
                    if (string.IsNullOrWhiteSpace(section)) continue;

                    // 获取当前区段的所有键
                    var sectionKeys = Config.GetKeys(section);

                    // 跳过没有键的区段
                    if (sectionKeys == null || sectionKeys.Length == 0) continue;

                    // 创建一个字典来存储当前区段的键值对
                    var sectionContent = new Dictionary<string, string>(sectionKeys.Length);

                    // 遍历当前区段的所有键，并获取对应的值
                    foreach (var key in sectionKeys)
                    {
                        if (!string.IsNullOrWhiteSpace(key))
                        {
                            // 获取键对应的值，并添加到当前区段的字典中
                            var value = Config.GetString(section, key);
                            sectionContent[key] = value;
                        }
                    }

                    // 如果当前区段有内容，则添加到总字典中
                    if (sectionContent.Count > 0)
                    {
                        // 确保区段名称不为空
                        _dicModelsContent[section] = sectionContent;
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载PLC模块配置内容时发生错误", ex);
                MessageHelper.MessageOK($"加载PLC模块配置内容时发生错误：{ex.Message}", TType.Error);
                throw; // 重新抛出异常，让调用者知道发生了错误
            }
        }

        /// <summary>
        /// 重新加载配置内容
        /// </summary>
        public void Reload()
        {
            LoadModelsContent();
        }

        /// <summary>
        /// 获取指定区段的配置内容
        /// </summary>
        /// <param name="section">区段名称</param>
        /// <returns>区段配置字典</returns>
        public Dictionary<string, string>? GetSectionContent(string section)
        {
            return _dicModelsContent.TryGetValue(section, out var content) ? content : null;
        }

        /// <summary>
        /// 获取指定区段和键的值
        /// </summary>
        /// <param name="section">区段名称</param>
        /// <param name="key">键名</param>
        /// <returns>配置值</returns>
        public string GetValue(string section, string key)
        {
            if (_dicModelsContent.TryGetValue(section, out var sectionContent))
            {
                return sectionContent.TryGetValue(key, out var value) ? value : null;
            }
            return null;
        }
    }
}
