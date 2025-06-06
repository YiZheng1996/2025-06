using RW.Modules;
using System.IO.Ports;

namespace MainUI.Devices
{
    public class BaseModuleNew : BaseModule
    {
        public BaseModuleNew() { }

        public BaseModuleNew(string name)
        {
            _moduleName = name;
            _className = GetType().Name;
        }

        private SerialPort _serialPort;

        public SerialPort SerialPort
        {
            get { return _serialPort; }
            set { _serialPort = value; }
        }

        private string _ip;
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }

        private string _ipPort;
        public string IPPort
        {
            get { return _ipPort; }
            set { _ipPort = value; }
        }

        private string _moduleName;
        public new string ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }

        private string _className;
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
    }
}
