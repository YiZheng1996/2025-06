using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Services
{
    /// <summary>
    /// PLC服务的依赖注入扩展方法
    /// 
    /// 提供统一的服务注册入口，便于管理和配置
    /// </summary>
    public static class PLCServiceExtensions
    {
        /// <summary>
        /// 注册所有PLC相关服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置对象</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddPLCServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 注册配置选项
            services.Configure<PLCConfigurationOptions>(configuration.GetSection("PLCConfiguration"));
            services.Configure<PLCManagerOptions>(configuration.GetSection("PLCManager"));

            // 注册核心服务接口和实现
            services.AddSingleton<IPLCConfigurationService, PLCConfigurationService>();
            services.AddSingleton<IPLCModuleProvider, PLCModuleProvider>();
            services.AddSingleton<IPLCManager, PLCManager>();

            return services;
        }

        /// <summary>
        /// 注册PLC服务的重载方法，支持自定义配置
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configurePLCOptions">PLC管理器配置委托</param>
        /// <param name="configureConfigOptions">配置服务配置委托</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddPLCServices(
            this IServiceCollection services,
            Action<PLCManagerOptions> configurePLCOptions = null,
            Action<PLCConfigurationOptions> configureConfigOptions = null)
        {
            // 配置选项
            if (configurePLCOptions != null)
            {
                services.Configure(configurePLCOptions);
            }

            if (configureConfigOptions != null)
            {
                services.Configure(configureConfigOptions);
            }

            // 注册服务
            services.AddSingleton<IPLCConfigurationService, PLCConfigurationService>();
            services.AddSingleton<IPLCModuleProvider, PLCModuleProvider>();
            services.AddSingleton<IPLCManager, PLCManager>();

            return services;
        }
    }
}
