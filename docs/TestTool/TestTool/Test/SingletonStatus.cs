using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTool.Test
{
    internal class SingletonStatus
    {
        // 私有静态实例，用于存储类的唯一实例
        private static SingletonStatus _instance;

        // 私有静态锁，用于线程安全
        private static readonly object _lock = new object();

        // 公共属性，用于存储需要传递的数据
        public int t { get; set; }
        private int _stepNum;
        public bool status { get; set; }

        //public List<bool> opc_status { get; set; }
        public int stepNum
        {
            get { return _stepNum; }
            set
            {
                if (_stepNum != value)
                {
                    _stepNum = value;
                    OnMyStaticPropertyChanged();
                }
            }
        }
        private void OnMyStaticPropertyChanged()
        {
            MyStaticPropertyChanged?.Invoke(stepNum);
        }
        public event Action<int> MyStaticPropertyChanged;
        public string childName { get; set; }
        public string parentName { get; set; }
        public string stepName { get; set; }
        public List<object> obj { get; set; }
        public bool cycle { get; set; }
        // 私有构造函数，防止外部类实例化
        private SingletonStatus()
        {
            // 可以在这里进行任何需要的初始化
        }
        
        // 公共静态方法，用于返回类的唯一实例（线程安全）
        public static SingletonStatus Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SingletonStatus();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
