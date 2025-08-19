using MainUI.Procedure.DSL.LogicalConfiguration;
using MainUI.Procedure.DSL.LogicalConfiguration.Forms;
using MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure;
using MainUI.Procedure.DSL.LogicalConfiguration.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Configuration;

namespace MainUI
{
    static class Program
    {
        // 全局服务提供者，用于整个应用程序
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>  
        /// 应用程序的主入口点。  
        /// </summary>  
        [STAThread]
        static void Main()
        {
            VarHelper.fsql = new FreeSql.FreeSqlBuilder()
                .UseMonitorCommand(cmd => Trace.WriteLine($"Sql：{cmd.CommandText}"))  
                .UseConnectionString(FreeSql.DataType.Sqlite, ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString)
                .Build();
            if (!VarHelper.fsql.Ado.ExecuteConnectTest()) throw new Exception("Sqlite数据库连接失败");

            // 配置依赖注入容器
            var services = new ServiceCollection();
            ConfigureServices(services);

            // 构建服务提供者
            ServiceProvider = services.BuildServiceProvider();

            // 设置兼容性服务定位器（临时解决方案）
            ServiceLocator.SetServiceProvider(ServiceProvider);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmLogin login = new();
            login.lblSoftName.Text = "软件通用平台";
            login.Icon = new Icon("ico.ico");
            var files = Directory.GetFiles(Application.StartupPath, "ico.*");
            var f = files.Where(x => !x.Contains("ico.ico")).FirstOrDefault();
            if (f != null)
            {
                Image image = Image.FromFile(f);//登录界面和主界面的图片  
                login.Logo.Image = image;
            }
            #region 单例模式  
            string softname = Application.ProductName;
            VarHelper.SoftName = softname;
            bool flag = false;
            Mutex mutex = new(true, softname, out flag);
            //第一个参数:true--给调用线程赋予互斥体的初始所属权    
            //第一个参数:互斥体的名称    
            //第三个参数:返回值,如果调用线程已被授予互斥体的初始所属权,则返回true    
            if (!flag)
            {
                MessageBox.Show("只能运行一个程序！", "请确定", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Environment.Exit(0);
            }
            #endregion
            DialogResult dr = login.ShowDialog();
            if (dr == DialogResult.OK)
            {
                try
                {
                    OPCHelper.Connect();
                    ServiceContainerInitializer.Initialize();
                    var main = ServiceProvider.GetRequiredService<frmMainMenu>();
                    //frmMainMenu main = new()
                    //{
                    //    Icon = new Icon("ico.ico")
                    //};
                    //if (f != null)
                    //{
                    //    Image image = Image.FromFile(f);//登录界面和主界面的图片  
                    //    main.Logo.Image = image;
                    //}
                    Application.Run(main);
                }
                catch (Exception ex)
                {
                    NlogHelper.Default.Error("初始化失败：", ex);
                    MessageBox.Show("初始化失败：" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 配置所有服务的依赖注入
        /// </summary>
        private static void ConfigureServices(IServiceCollection services)
        {
            // 注册工作流服务
            services.AddWorkflowServices(options =>
            {
                options.EnableEventLogging = true;      // 开启事件日志
                options.EnablePerformanceMonitoring = false; // 关闭性能监控（开发阶段）
                options.MaxVariableCacheSize = 1000;    // 设置缓存大小
            });

            // 注册日志服务
            services.AddLogging(builder =>
            {
                builder.AddConsole();  // 控制台日志
                builder.AddDebug();    // 调试输出日志
            });

            // 注册你的窗体类（如果需要依赖注入）
            services.AddTransient<frmMainMenu>();
            services.AddTransient<FrmLogicalConfiguration>();
            services.AddTransient<Form_DefineVar>();

            // 注册其他你需要的服务
            // services.AddScoped<IDataService, DataService>();
            // services.AddSingleton<IConfigurationService, ConfigurationService>();
        }
    }
}
