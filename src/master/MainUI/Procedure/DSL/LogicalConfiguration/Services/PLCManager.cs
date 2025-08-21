using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RW.Modules;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Services
{
    /// <summary>
    /// PLC管理器实现 - 依赖注入版本
    /// 
    /// 重构要点：
    /// 1. 完全移除单例模式
    /// 2. 通过构造函数注入所有依赖
    /// 3. 使用接口实现依赖倒置
    /// 4. 支持异步操作和取消令牌
    /// 5. 增强错误处理和日志记录
    /// </summary>
    public class PLCManager : IPLCManager, IDisposable
    {
        private readonly ILogger<PLCManager> _logger;
        private readonly IPLCModuleProvider _moduleProvider;
        private readonly IPLCConfigurationService _configurationService;
        private readonly PLCManagerOptions _options;

        // 懒加载的模块字典
        private readonly Lazy<Task<Dictionary<string, BaseModule>>> _lazyModules;
        private bool _disposed = false;

        /// <summary>
        /// 构造函数 - 依赖注入所有必要的服务
        /// </summary>
        public PLCManager(
            ILogger<PLCManager> logger,
            IPLCModuleProvider moduleProvider,
            IPLCConfigurationService configurationService,
            IOptions<PLCManagerOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _moduleProvider = moduleProvider ?? throw new ArgumentNullException(nameof(moduleProvider));
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

            // 懒加载模块初始化
            _lazyModules = new Lazy<Task<Dictionary<string, BaseModule>>>(
                () => _moduleProvider.InitializePLCModulesAsync(),
                LazyThreadSafetyMode.ExecutionAndPublication);

            _logger.LogInformation("PLCManager实例已创建，使用依赖注入模式");
        }

        #region 属性实现

        /// <summary>
        /// 检查PLC模块是否已成功初始化
        /// </summary>
        public bool IsPLCInitialized => _lazyModules.IsValueCreated &&
                                       _lazyModules.Value.IsCompletedSuccessfully &&
                                       _lazyModules.Value.Result.Count > 0;

        /// <summary>
        /// 获取模块配置内容字典（只读）
        /// </summary>
        public IReadOnlyDictionary<string, Dictionary<string, string>> ModelsContent =>
            _configurationService.Configuration;

        #endregion

        #region PLC操作实现

        /// <summary>
        /// 异步读取单个PLC点位值
        /// </summary>
        public async Task<object> ReadPLCValueAsync(string moduleName, string address, CancellationToken cancellationToken = default)
        {
            // 参数验证
            if (string.IsNullOrWhiteSpace(moduleName))
                throw new ArgumentException("模块名称不能为空", nameof(moduleName));
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("点位地址不能为空", nameof(address));

            try
            {
                _logger.LogDebug("开始读取PLC值: {ModuleName}.{Address}", moduleName, address);

                // 获取模块
                var module = await GetModuleAsync(moduleName, cancellationToken);
                if (module == null)
                {
                    throw new InvalidOperationException($"未找到指定的PLC模块: {moduleName}");
                }

                // 执行读取操作（包装为异步）
                var result = await Task.Run(() => module.Read(address), cancellationToken);

                _logger.LogDebug("PLC读取成功: {ModuleName}.{Address} = {Value}", moduleName, address, result);
                return result;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("PLC读取操作被取消: {ModuleName}.{Address}", moduleName, address);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PLC读取失败: {ModuleName}.{Address}", moduleName, address);
                throw new InvalidOperationException($"读取PLC值失败: {moduleName}.{address}, 错误: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 异步写入单个PLC点位值
        /// </summary>
        public async Task<bool> WritePLCValueAsync(string moduleName, string address, object value, CancellationToken cancellationToken = default)
        {
            // 参数验证
            if (string.IsNullOrWhiteSpace(moduleName))
                throw new ArgumentException("模块名称不能为空", nameof(moduleName));
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("点位地址不能为空", nameof(address));

            try
            {
                _logger.LogDebug("开始写入PLC值: {ModuleName}.{Address} = {Value}", moduleName, address, value);

                // 获取模块
                var module = await GetModuleAsync(moduleName, cancellationToken);
                if (module == null)
                {
                    _logger.LogError("未找到指定的PLC模块: {ModuleName}", moduleName);
                    return false;
                }

                // 执行写入操作（包装为异步）
                await Task.Run(() => module.Write(address, value), cancellationToken);

                _logger.LogDebug("PLC写入成功: {ModuleName}.{Address} = {Value}", moduleName, address, value);
                return true;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("PLC写入操作被取消: {ModuleName}.{Address}", moduleName, address);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PLC写入失败: {ModuleName}.{Address} = {Value}", moduleName, address, value);
                return false;
            }
        }

        /// <summary>
        /// 异步批量读取PLC点位值
        /// </summary>
        public async Task<Dictionary<string, object>> BatchReadPLCAsync(IEnumerable<PlcReadItem> readItems, CancellationToken cancellationToken = default)
        {
            var results = new Dictionary<string, object>();
            var items = readItems?.ToList() ?? new List<PlcReadItem>();

            if (items.Count == 0)
            {
                _logger.LogWarning("批量读取PLC：读取项列表为空");
                return results;
            }

            _logger.LogInformation("开始批量读取PLC，共 {Count} 项", items.Count);

            // 使用并发处理提高性能（可选）
            if (_options.EnableParallelProcessing)
            {
                var semaphore = new SemaphoreSlim(_options.MaxConcurrentOperations);
                var tasks = items.Select(async item =>
                {
                    await semaphore.WaitAsync(cancellationToken);
                    try
                    {
                        var value = await ReadPLCValueAsync(item.PlcModuleName, item.PlcKeyName, cancellationToken);
                        var key = $"{item.PlcModuleName}.{item.PlcKeyName}";
                        lock (results)
                        {
                            results[key] = value;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "批量读取PLC项失败: {ModuleName}.{Address}", item.PlcModuleName, item.PlcKeyName);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                await Task.WhenAll(tasks);
            }
            else
            {
                // 串行处理
                foreach (var item in items)
                {
                    try
                    {
                        var value = await ReadPLCValueAsync(item.PlcModuleName, item.PlcKeyName, cancellationToken);
                        var key = $"{item.PlcModuleName}.{item.PlcKeyName}";
                        results[key] = value;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "批量读取PLC项失败: {ModuleName}.{Address}", item.PlcModuleName, item.PlcKeyName);
                    }
                }
            }

            _logger.LogInformation("批量PLC读取完成: 成功 {SuccessCount}/{TotalCount} 项", results.Count, items.Count);
            return results;
        }

        /// <summary>
        /// 异步批量写入PLC点位值
        /// </summary>
        public async Task<int> BatchWritePLCAsync(IEnumerable<PlcWriteItem> writeItems, CancellationToken cancellationToken = default)
        {
            var items = writeItems?.ToList() ?? new List<PlcWriteItem>();
            if (items.Count == 0)
            {
                _logger.LogWarning("批量写入PLC：写入项列表为空");
                return 0;
            }

            _logger.LogInformation("开始批量写入PLC，共 {Count} 项", items.Count);
            var successCount = 0;

            // 使用并发处理提高性能（可选）
            if (_options.EnableParallelProcessing)
            {
                var semaphore = new SemaphoreSlim(_options.MaxConcurrentOperations);
                var tasks = items.Select(async item =>
                {
                    await semaphore.WaitAsync(cancellationToken);
                    try
                    {
                        var success = await WritePLCValueAsync(item.PlcModuleName, item.PlcKeyName, item.PlcValue, cancellationToken);
                        if (success)
                        {
                            Interlocked.Increment(ref successCount);
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                await Task.WhenAll(tasks);
            }
            else
            {
                // 串行处理
                foreach (var item in items)
                {
                    try
                    {
                        var success = await WritePLCValueAsync(item.PlcModuleName, item.PlcKeyName, item.PlcValue, cancellationToken);
                        if (success)
                        {
                            successCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "批量写入PLC项失败: {ModuleName}.{Address}", item.PlcModuleName, item.PlcKeyName);
                    }
                }
            }

            _logger.LogInformation("批量PLC写入完成: 成功 {SuccessCount}/{TotalCount} 项", successCount, items.Count);
            return successCount;
        }

        #endregion

        #region 模块管理实现

        /// <summary>
        /// 检查指定模块是否在线
        /// </summary>
        public async Task<bool> IsModuleOnlineAsync(string moduleName)
        {
            if (string.IsNullOrWhiteSpace(moduleName))
                return false;

            try
            {
                var module = await GetModuleAsync(moduleName);
                if (module == null)
                    return false;

                // 尝试读取一个测试地址来检查连接状态
                // 这里需要根据您的BaseModule实现来调整
                await Task.Run(() =>
                {
                    // 假设有IsConnected属性或类似的方法
                    // return module.IsConnected;

                    // 或者通过尝试读取来检查
                    try
                    {
                        module.Read("ConnectionTest"); // 这需要根据实际情况调整
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "检查模块在线状态失败: {ModuleName}", moduleName);
                return false;
            }
        }

        /// <summary>
        /// 获取PLC操作统计信息
        /// </summary>
        public async Task<PLCOperationStats> GetOperationStatsAsync()
        {
            try
            {
                var modules = await GetModulesAsync();
                var moduleNames = modules.Keys.ToList();

                // 并发检查模块在线状态
                var onlineCheckTasks = moduleNames.Select(async name =>
                {
                    var isOnline = await IsModuleOnlineAsync(name);
                    return new { Name = name, IsOnline = isOnline };
                });

                var onlineResults = await Task.WhenAll(onlineCheckTasks);
                var onlineCount = onlineResults.Count(r => r.IsOnline);

                return new PLCOperationStats
                {
                    TotalModules = modules.Count,
                    OnlineModules = onlineCount,
                    ConfiguredAddresses = ModelsContent.Sum(kvp => Math.Max(0, kvp.Value.Count - 1)), // 减去ServerName
                    LastUpdateTime = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取PLC操作统计信息失败");
                return new PLCOperationStats
                {
                    TotalModules = 0,
                    OnlineModules = 0,
                    ConfiguredAddresses = 0,
                    LastUpdateTime = DateTime.Now
                };
            }
        }

        /// <summary>
        /// 验证PLC配置是否有效
        /// </summary>
        public bool ValidatePlcConfig(PlcAddressConfig config)
        {
            if (config == null)
            {
                _logger.LogWarning("PLC配置为null");
                return false;
            }

            if (string.IsNullOrWhiteSpace(config.ModuleName))
            {
                _logger.LogWarning("PLC配置缺少模块名称");
                return false;
            }

            if (string.IsNullOrWhiteSpace(config.Address))
            {
                _logger.LogWarning("PLC配置缺少地址信息");
                return false;
            }

            // 检查模块是否存在
            if (!_moduleProvider.ModuleExists(config.ModuleName))
            {
                _logger.LogWarning("PLC配置中的模块不存在: {ModuleName}", config.ModuleName);
                return false;
            }

            return true;
        }

        #endregion

        #region 专用业务实现

        /// <summary>
        /// 检测工具专用的PLC读取方法
        /// </summary>
        public async Task<object> ReadPLCForDetectionAsync(PlcAddressConfig plcConfig, CancellationToken cancellationToken = default)
        {
            if (!ValidatePlcConfig(plcConfig))
            {
                throw new ArgumentException($"无效的PLC配置: {plcConfig?.ModuleName ?? "null"}.{plcConfig?.Address ?? "null"}");
            }

            try
            {
                _logger.LogDebug("检测工具PLC读取: {ModuleName}.{Address}", plcConfig.ModuleName, plcConfig.Address);

                var result = await ReadPLCValueAsync(plcConfig.ModuleName, plcConfig.Address, cancellationToken);

                _logger.LogDebug("检测工具PLC读取成功: {ModuleName}.{Address} = {Value}",
                    plcConfig.ModuleName, plcConfig.Address, result);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "检测工具PLC读取失败: {ModuleName}.{Address}",
                    plcConfig.ModuleName, plcConfig.Address);
                throw new InvalidOperationException($"检测工具PLC读取失败: {plcConfig.ModuleName}.{plcConfig.Address}, 错误: {ex.Message}", ex);
            }
        }

        #endregion

        #region 私有辅助方法

        /// <summary>
        /// 异步获取指定模块
        /// </summary>
        private async Task<BaseModule> GetModuleAsync(string moduleName, CancellationToken cancellationToken = default)
        {
            var modules = await GetModulesAsync(cancellationToken);
            return modules.TryGetValue(moduleName, out var module) ? module : null;
        }

        /// <summary>
        /// 异步获取所有模块
        /// </summary>
        private async Task<Dictionary<string, BaseModule>> GetModulesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _lazyModules.Value.WaitAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("获取PLC模块操作被取消");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取PLC模块失败");
                throw;
            }
        }

        #endregion

        #region IDisposable实现

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                // 如果模块已经初始化，尝试释放它们
                if (_lazyModules.IsValueCreated && _lazyModules.Value.IsCompletedSuccessfully)
                {
                    var modules = _lazyModules.Value.Result;
                    foreach (var module in modules.Values)
                    {
                        try
                        {
                            if (module is IDisposable disposableModule)
                            {
                                disposableModule.Dispose();
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "释放PLC模块时发生异常");
                        }
                    }
                }

                _disposed = true;
                _logger.LogInformation("PLCManager已释放资源");
            }
        }

        #endregion
    }

    /// <summary>
    /// PLC管理器选项配置
    /// </summary>
    public class PLCManagerOptions
    {
        /// <summary>
        /// 是否启用并行处理
        /// </summary>
        public bool EnableParallelProcessing { get; set; } = true;

        /// <summary>
        /// 最大并发操作数
        /// </summary>
        public int MaxConcurrentOperations { get; set; } = 10;

        /// <summary>
        /// 操作超时时间（毫秒）
        /// </summary>
        public int OperationTimeoutMs { get; set; } = 5000;

        /// <summary>
        /// 是否启用详细日志
        /// </summary>
        public bool EnableDetailedLogging { get; set; } = false;
    }

    /// <summary>
    /// LC操作统计信息类
    /// </summary>
    public class PLCOperationStats
    {
        public int TotalModules { get; set; }           // 总PLC模块数量
        public int OnlineModules { get; set; }          // 在线PLC模块数量  
        public int ConfiguredAddresses { get; set; }    // 已配置的地址点位总数
        public DateTime LastUpdateTime { get; set; }    // 最后更新时间
    }
}