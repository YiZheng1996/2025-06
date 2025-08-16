using AntdUI;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using RW.Modules;
using System.Text;

namespace MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// PLC点位配置和操作统一管理器
    /// 负责PLC配置管理、读写操作、状态监控等功能
    /// </summary>
    public sealed class PointPLCManager : IniConfig
    {
        private const string MODULE_FILE_PATH = "Modules\\MyModules.ini";
        private static readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 类的延迟初始化单例实例
        /// </summary>
        private static readonly Lazy<PointPLCManager> _instance =
            new(() => new PointPLCManager());

        /// <summary>
        /// 获取PointPLCManager的单例实例
        /// </summary>
        public static PointPLCManager Instance => _instance.Value;

        /// <summary>
        /// 存储所有模块配置内容的字典
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, string>> _dicModelsContent;

        /// <summary>
        /// PLC模块字典 - 延迟初始化
        /// </summary>
        private readonly Lazy<Dictionary<string, BaseModule>> _lazyPLCModules;

        /// <summary>
        /// 获取PLC模块字典
        /// </summary>
        private Dictionary<string, BaseModule> PLCModules => _lazyPLCModules.Value;

        /// <summary>
        /// 获取模块配置内容字典
        /// </summary>
        public IReadOnlyDictionary<string, Dictionary<string, string>> DicModelsContent => _dicModelsContent;

        /// <summary>
        /// 检查PLC模块是否已成功初始化
        /// </summary>
        public bool IsPLCInitialized => _lazyPLCModules.IsValueCreated && PLCModules.Count > 0;

        /// <summary>
        /// 私有构造函数，确保单例模式
        /// </summary>
        private PointPLCManager() : base(AppPath + MODULE_FILE_PATH)
        {
            _dicModelsContent = [];

            // 延迟初始化PLC模块
            _lazyPLCModules = new Lazy<Dictionary<string, BaseModule>>(
                InitializePLCModules,
                LazyThreadSafetyMode.ExecutionAndPublication);

            Initialize();
        }

        /// <summary>
        /// 初始化PLC模块
        /// </summary>
        private Dictionary<string, BaseModule> InitializePLCModules()
        {
            try
            {
                NlogHelper.Default.Info("开始初始化 PLC 模块集合");

                // 确保 ModuleComponent 正确初始化
                ModuleComponent.Instance.Init();

                // 获取模块列表
                var moduleList = ModuleComponent.Instance.GetList();

                if (moduleList == null || moduleList.Count == 0)
                {
                    NlogHelper.Default.Warn("未找到任何 PLC 模块，使用空字典");
                    return [];
                }

                NlogHelper.Default.Info($"成功初始化 {moduleList.Count} 个 PLC 模块: {string.Join(", ", moduleList.Keys)}");
                return moduleList;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"PLC模块初始化失败: {ex.Message}", ex);
                return [];
            }
        }

        /// <summary>
        /// 初始化并加载配置
        /// </summary>
        private void Initialize()
        {
            // 原有的初始化逻辑...
        }

        #region PLC操作统一接口

        /// <summary>
        /// 读取单个PLC点位值
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="address">点位地址</param>
        /// <returns>读取的值</returns>
        public async Task<object> ReadPLCValueAsync(string moduleName, string address)
        {
            try
            {
                // 检查PLC模块是否存在
                if (!PLCModules.TryGetValue(moduleName, out var module))
                {
                    throw new ArgumentException($"未找到指定的PLC模块: {moduleName}");
                }

                // 读取PLC值
                var plcValue = module.Read(address);

                NlogHelper.Default.Info($"PLC读取成功: {moduleName}.{address} = {plcValue}");

                await Task.CompletedTask;
                return plcValue;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"PLC读取失败: {moduleName}.{address}, 错误: {ex.Message}", ex);
                throw new Exception($"读取PLC值失败: {moduleName}.{address}, 错误: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 写入单个PLC点位值
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="address">点位地址</param>
        /// <param name="value">要写入的值</param>
        /// <returns>是否写入成功</returns>
        public async Task<bool> WritePLCValueAsync(string moduleName, string address, object value)
        {
            try
            {
                // 检查PLC模块是否存在
                if (!PLCModules.TryGetValue(moduleName, out var module))
                {
                    NlogHelper.Default.Error($"未找到指定的PLC模块: {moduleName}");
                    return false;
                }

                // 写入PLC值
                module.Write(address, value);

                NlogHelper.Default.Info($"PLC写入成功: {moduleName}.{address} = {value}");

                await Task.CompletedTask;
                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"PLC写入失败: {moduleName}.{address} = {value}, 错误: {ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 批量读取PLC点位值
        /// </summary>
        /// <param name="readItems">读取项列表</param>
        /// <returns>读取结果字典，Key为"模块名.地址"，Value为读取的值</returns>
        public async Task<Dictionary<string, object>> BatchReadPLCAsync(List<PlcReadItem> readItems)
        {
            var results = new Dictionary<string, object>();

            if (readItems == null || readItems.Count == 0)
            {
                return results;
            }

            foreach (var item in readItems)
            {
                try
                {
                    var value = await ReadPLCValueAsync(item.PlcModuleName, item.PlcKeyName);
                    var key = $"{item.PlcModuleName}.{item.PlcKeyName}";
                    results[key] = value;
                }
                catch (Exception ex)
                {
                    NlogHelper.Default.Error($"批量读取PLC失败: {item.PlcModuleName}.{item.PlcKeyName}, 错误: {ex.Message}", ex);
                    // 继续处理其他项
                }
            }

            return results;
        }

        /// <summary>
        /// 批量写入PLC点位值
        /// </summary>
        /// <param name="writeItems">写入项列表</param>
        /// <returns>成功写入的数量</returns>
        public async Task<int> BatchWritePLCAsync(List<PlcWriteItem> writeItems)
        {
            int successCount = 0;

            if (writeItems == null || writeItems.Count == 0)
            {
                return successCount;
            }

            foreach (var item in writeItems)
            {
                try
                {
                    var success = await WritePLCValueAsync(item.PlcModuleName, item.PlcKeyName, item.PlcValue);
                    if (success)
                    {
                        successCount++;
                    }
                }
                catch (Exception ex)
                {
                    NlogHelper.Default.Error($"批量写入PLC失败: {item.PlcModuleName}.{item.PlcKeyName}, 错误: {ex.Message}", ex);
                    // 继续处理其他项
                }
            }

            NlogHelper.Default.Info($"批量PLC写入完成: 成功 {successCount}/{writeItems.Count} 项");
            return successCount;
        }

        /// <summary>
        /// 检测工具专用的PLC读取方法
        /// </summary>
        /// <param name="plcConfig">PLC配置</param>
        /// <returns>读取的值</returns>
        public async Task<object> ReadPLCForDetectionAsync(PlcAddressConfig plcConfig)
        {
            if (!ValidatePlcConfig(plcConfig))
            {
                throw new ArgumentException($"无效的PLC配置: {plcConfig?.ModuleName}.{plcConfig?.Address}");
            }

            return await ReadPLCValueAsync(plcConfig.ModuleName, plcConfig.Address);
        }

        #endregion

        #region PLC配置验证和管理

        /// <summary>
        /// 检查PLC模块和地址是否有效
        /// </summary>
        /// <param name="plcConfig">PLC配置</param>
        /// <returns>是否有效</returns>
        public bool ValidatePlcConfig(PlcAddressConfig plcConfig)
        {
            if (plcConfig == null) return false;

            if (string.IsNullOrWhiteSpace(plcConfig.ModuleName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(plcConfig.Address))
            {
                return false;
            }

            // 检查PLC模块是否存在
            return PLCModules.ContainsKey(plcConfig.ModuleName);
        }

        /// <summary>
        /// 获取所有可用的PLC模块名称
        /// </summary>
        /// <returns>PLC模块名称列表</returns>
        public List<string> GetAvailablePlcModules()
        {
            return [.. PLCModules.Keys];
        }

        /// <summary>
        /// 获取指定PLC模块的所有可用地址
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns>地址列表</returns>
        public List<string> GetAvailablePlcAddresses(string moduleName)
        {
            try
            {
                // 从配置内容获取地址信息
                if (DicModelsContent.TryGetValue(moduleName, out var addresses))
                {
                    return addresses.Keys.Where(key => key != "ServerName").ToList();
                }
                return new List<string>();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"获取PLC地址失败: {ex.Message}", ex);
                return new List<string>();
            }
        }

        /// <summary>
        /// 检查PLC模块是否在线
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns>是否在线</returns>
        public bool IsPLCModuleOnline(string moduleName)
        {
            try
            {
                if (!PLCModules.TryGetValue(moduleName, out var module))
                {
                    return false;
                }

                // 这里可以添加具体的在线检测逻辑
                // 例如读取一个系统状态点位
                return module.Driver != null;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"检查PLC模块在线状态失败: {moduleName}, 错误: {ex.Message}", ex);
                return false;
            }
        }

        #endregion

        #region 统计和监控

        /// <summary>
        /// 获取PLC操作统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        public PLCOperationStats GetOperationStats()
        {
            return new PLCOperationStats
            {
                TotalModules = PLCModules.Count,
                OnlineModules = PLCModules.Keys.Count(IsModuleOnline),
                ConfiguredAddresses = DicModelsContent.Sum(kvp => kvp.Value.Count - 1), // 减去ServerName
                LastUpdateTime = DateTime.Now
            };
        }

        private bool IsModuleOnline(string moduleName)
        {
            try
            {
                return IsPLCModuleOnline(moduleName);
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }

    /// <summary>
    /// PLC操作统计信息
    /// </summary>
    public class PLCOperationStats
    {
        public int TotalModules { get; set; }
        public int OnlineModules { get; set; }
        public int ConfiguredAddresses { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
