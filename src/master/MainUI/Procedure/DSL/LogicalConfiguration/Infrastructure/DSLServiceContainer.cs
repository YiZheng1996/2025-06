using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using MainUI.Procedure.DSL.LogicalConfiguration.Methods;
using Microsoft.Extensions.DependencyInjection;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure
{
    /// <summary>
    /// DSL服务容器 - 统一管理所有DSL方法类实例
    /// </summary>
    public static class DSLServiceContainer
    {
        private static readonly Lazy<IServiceProvider> _serviceProvider = new(BuildServiceProvider);

        /// <summary>
        /// 获取服务实例
        /// </summary>
        public static T GetService<T>() where T : class
        {
            return _serviceProvider.Value.GetRequiredService<T>();
        }

        /// <summary>
        /// 尝试获取服务实例
        /// </summary>
        public static T GetService<T>(string serviceName) where T : class
        {
            return _serviceProvider.Value.GetService<T>();
        }

        /// <summary>
        /// 构建服务提供者
        /// </summary>
        private static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            // 注册DSL方法类为单例
            RegisterDSLMethods(services);

            // 注册管理器类
            RegisterManagers(services);

            // 注册其他服务
            RegisterOtherServices(services);

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// 注册DSL方法类
        /// </summary>
        private static void RegisterDSLMethods(IServiceCollection services)
        {
            services.AddSingleton<SystemMethods>();
            services.AddSingleton<VariableMethods>();
            services.AddSingleton<PLCMethods>();
            services.AddSingleton<DetectionMethods>();
            services.AddSingleton<FlowControlMethods>();
            services.AddSingleton<ReportMethods>();
        }

        /// <summary>
        /// 注册管理器类
        /// </summary>
        private static void RegisterManagers(IServiceCollection services)
        {
            // StepExecutionManager每次使用时创建新实例
            services.AddTransient<StepExecutionManager>();

            // GlobalVariableManager作为单例
            services.AddSingleton<GlobalVariableManager>();

            // DataGridViewManager每次创建新实例
            services.AddTransient<DataGridViewManager>();
        }

        /// <summary>
        /// 注册其他服务
        /// </summary>
        private static void RegisterOtherServices(IServiceCollection services)
        {
            // 可以在这里注册其他需要的服务
            // 例如：配置服务、缓存服务等
        }

        /// <summary>
        /// 重新初始化容器（用于测试或配置更改）
        /// </summary>
        public static void Reset()
        {
            if (_serviceProvider.IsValueCreated)
            {
                _serviceProvider.Value?.GetService<IServiceProvider>()?.GetService<IDisposable>()?.Dispose();
            }
        }
    }
}
