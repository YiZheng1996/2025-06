using MainUI.Devices;
using RW.DSL.Modules;
using System.IO.Ports;
using System.Reflection;


namespace MainUI.CurrencyHelper
{
    public class RWDSLHelper
    {
        public RWDSLHelper() => InitDSL();

        public static Dictionary<string, DSLModule> MyModules =>
            DSLModuleHelper.GetModulesWithFile(Environment.CurrentDirectory + @"\Modules\MyModules.ini");

        public static Dictionary<string, IBaseType> ModuleDic { get; set; } = [];

        public void InitDSL()
        {
            // 获取当前程序集
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            // 获取实现 IBaseType 接口的所有类型
            Type[] types = currentAssembly.GetTypes();
            foreach (Type type in types)
            {
                if (typeof(IBaseType).IsAssignableFrom(type) && !type.IsInterface)
                {
                    // 动态创建类的实例
                    IBaseType instance = (IBaseType)Activator.CreateInstance(type);
                    //ModuleDic.Add(type.Name, instance);
                    ModuleDic.Add((instance as BaseModuleNew).ModuleName, instance);
                }
            }

            foreach (KeyValuePair<string, IBaseType> pair in ModuleDic)
            {
                switch (pair.Value.TypeName)
                {
                    case DeviceType.Modbus:
                        var Ports = pair.Value as BaseModuleNew;
                        Ports.SerialPort = new() //默认值
                        {
                            PortName = "COM1",
                            BaudRate = 9600,
                            DataBits = 8,
                            Parity = Parity.None,
                            StopBits = StopBits.One
                        };
                        break;
                    case DeviceType.TCPIP:
                        var IP = pair.Value as BaseModuleNew;
                        IP.IP = "192.168.0.1";
                        IP.IPPort = "6060";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
