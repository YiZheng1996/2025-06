namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    /// <summary>
    /// 表示一个线程安全的单例类，用于维护操作的状态和元数据。
    /// </summary>
    public class SingletonStatus
    {
        // 私有静态实例，用于存储类的唯一实例
        private static SingletonStatus _instance;

        // 私有静态锁，用于线程安全
        private static readonly object _lock = new();

        // 公共属性，用于存储需要传递的数据
        public int T { get; set; }

        /// <summary>
        /// 当前操作的状态，true表示成功，false表示失败
        /// </summary>
        public bool Status { get; set; }

        private int _stepNum;
        /// <summary>
        /// 步骤序号，用于标识当前操作的步骤
        /// </summary>
        public int StepNum
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

        public event Action<int> MyStaticPropertyChanged;
        /// <summary>
        /// 事件，通知订阅者静态更改
        /// </summary>
        private void OnMyStaticPropertyChanged()
        {
            MyStaticPropertyChanged?.Invoke(StepNum);
        }

        /// <summary>
        /// 产品类型名称，用于区分不同的产品或模型
        /// </summary>
        public string ModelTypeName { get; set; }

        /// <summary>
        /// 产品型号名称，用于具体标识某一型号的产品
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 项点名称，用于标识具体的试验或操作步骤
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName { get; set; }

        public List<object> Obj { get; set; }

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
                        _instance ??= new SingletonStatus();
                    }
                }
                return _instance;
            }
        }
    }
}
