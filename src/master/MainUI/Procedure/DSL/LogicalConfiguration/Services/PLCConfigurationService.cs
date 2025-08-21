using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Services
{
    /// <summary>
    /// PLC配置服务实现
    /// 
    /// 设计特点：
    /// 1. 使用ILogger进行日志记录
    /// 2. 支持选项模式配置
    /// 3. 异步文件操作
    /// 4. 线程安全的缓存管理
    /// </summary>
    public class PLCConfigurationService : IPLCConfigurationService, IDisposable
    {
        private readonly ILogger<PLCConfigurationService> _logger;
        private readonly PLCConfigurationOptions _options;
        private readonly ReaderWriterLockSlim _configLock = new();
        private readonly Dictionary<string, Dictionary<string, string>> _configuration = new();
        private readonly FileSystemWatcher _fileWatcher;
        private bool _disposed = false;

        public PLCConfigurationService(
            ILogger<PLCConfigurationService> logger,
            IOptions<PLCConfigurationOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

            // 设置文件监控（可选）
            if (_options.EnableFileWatching && !string.IsNullOrEmpty(_options.ConfigurationPath))
            {
                _fileWatcher = CreateFileWatcher(_options.ConfigurationPath);
            }
        }

        /// <summary>
        /// 获取配置内容（只读视图）
        /// </summary>
        public IReadOnlyDictionary<string, Dictionary<string, string>> Configuration
        {
            get
            {
                _configLock.EnterReadLock();
                try
                {
                    // 返回深拷贝以确保不可变性
                    return _configuration.ToDictionary(
                        kvp => kvp.Key,
                        kvp => new Dictionary<string, string>(kvp.Value)
                    );
                }
                finally
                {
                    _configLock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// 配置变更事件
        /// </summary>
        public event EventHandler<ConfigurationChangedEventArgs>? ConfigurationChanged;

        /// <summary>
        /// 异步加载配置文件
        /// </summary>
        public async Task<Dictionary<string, Dictionary<string, string>>> LoadConfigurationAsync(
            string configPath,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(configPath))
                throw new ArgumentException("配置文件路径不能为空", nameof(configPath));

            try
            {
                _logger.LogInformation("开始加载PLC配置文件: {ConfigPath}", configPath);

                // 检查文件是否存在
                if (!File.Exists(configPath))
                {
                    _logger.LogWarning("配置文件不存在: {ConfigPath}", configPath);
                    return new Dictionary<string, Dictionary<string, string>>();
                }

                // 异步读取文件内容
                var iniConfig = new IniConfig(configPath);
                var newConfiguration = new Dictionary<string, Dictionary<string, string>>();

                // 使用IniConfig的现有方法加载配置
                // 这里需要根据原有的IniConfig实现来适配
                await Task.Run(() =>
                {
                    // 模拟原有的配置加载逻辑
                    // 您需要根据实际的IniConfig实现来调整这部分代码
                    LoadIniConfiguration(iniConfig, newConfiguration);
                }, cancellationToken);

                // 更新缓存
                _configLock.EnterWriteLock();
                try
                {
                    _configuration.Clear();
                    foreach (var kvp in newConfiguration)
                    {
                        _configuration[kvp.Key] = kvp.Value;
                    }
                }
                finally
                {
                    _configLock.ExitWriteLock();
                }

                _logger.LogInformation("成功加载PLC配置，共 {Count} 个模块", newConfiguration.Count);
                return newConfiguration;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载PLC配置文件失败: {ConfigPath}", configPath);
                throw new InvalidOperationException($"加载PLC配置失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 刷新配置
        /// </summary>
        public async Task RefreshConfigurationAsync(CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(_options.ConfigurationPath))
            {
                _logger.LogWarning("未配置配置文件路径，无法刷新");
                return;
            }

            await LoadConfigurationAsync(_options.ConfigurationPath, cancellationToken);
        }

        /// <summary>
        /// 加载INI配置的具体实现
        /// </summary>
        private static void LoadIniConfiguration(IniConfig iniConfig, Dictionary<string, Dictionary<string, string>> configuration)
        {
            // 这里需要根据您的IniConfig实际实现来编写
            // 示例实现（需要根据实际情况调整）

            // 假设IniConfig有GetSections()和GetKeys()方法
            // 您需要根据实际的IniConfig API来实现这部分

            /*
            var sections = iniConfig.GetSections();
            foreach (var section in sections)
            {
                var sectionDict = new Dictionary<string, string>();
                var keys = iniConfig.GetKeys(section);
                foreach (var key in keys)
                {
                    var value = iniConfig.ReadString(section, key, "");
                    sectionDict[key] = value;
                }
                configuration[section] = sectionDict;
            }
            */
        }

        /// <summary>
        /// 创建文件监控器
        /// </summary>
        private FileSystemWatcher CreateFileWatcher(string configPath)
        {
            var directory = Path.GetDirectoryName(configPath);
            var fileName = Path.GetFileName(configPath);

            if (string.IsNullOrEmpty(directory) || string.IsNullOrEmpty(fileName))
                return null!;

            var watcher = new FileSystemWatcher(directory, fileName)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
                EnableRaisingEvents = true
            };

            watcher.Changed += async (sender, e) =>
            {
                try
                {
                    // 防止重复触发
                    await Task.Delay(100);
                    await RefreshConfigurationAsync();

                    _logger.LogInformation("配置文件已自动刷新: {Path}", e.FullPath);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "自动刷新配置失败");
                }
            };

            return watcher;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _fileWatcher?.Dispose();
                _configLock.Dispose();
                _disposed = true;
            }
        }
    }

    /// <summary>
    /// PLC配置选项
    /// </summary>
    public class PLCConfigurationOptions
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string ConfigurationPath { get; set; } = "Modules\\MyModules.ini";

        /// <summary>
        /// 是否启用文件监控
        /// </summary>
        public bool EnableFileWatching { get; set; } = true;

        /// <summary>
        /// 配置缓存超时时间（分钟）
        /// </summary>
        public int CacheTimeoutMinutes { get; set; } = 30;
    }
}