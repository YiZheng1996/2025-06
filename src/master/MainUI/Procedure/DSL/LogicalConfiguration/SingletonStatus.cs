namespace MainUI.Procedure.DSL.LogicalConfiguration
{
    /// <summary>
    /// 表示一个线程安全的单例类，用于维护操作的状态和元数据。
    /// 该类提供了完整的线程安全保证，包括属性的读写操作。
    /// </summary>
    public sealed class SingletonStatus : IDisposable
    {
        #region 单例模式实现

        /// <summary>
        /// 私有静态实例，使用Lazy<T>确保线程安全的延迟初始化
        /// </summary>
        private static readonly Lazy<SingletonStatus> _lazyInstance =
            new(() => new SingletonStatus(), LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// 获取单例实例 - 线程安全的访问方式
        /// </summary>
        public static SingletonStatus Instance => _lazyInstance.Value;

        /// <summary>
        /// 私有构造函数，防止外部实例化
        /// 初始化所有必要的集合和属性
        /// </summary>
        private SingletonStatus()
        {
            // 使用线程安全的集合类型，避免多线程访问时的数据竞争
            Obj = new List<object>(); // 如果需要线程安全，可以考虑使用 ConcurrentBag<object>
            IempSteps = new List<ChildModel>(); // 同上

            // 初始化读写锁，用于保护非线程安全的集合操作
            _objLock = new ReaderWriterLockSlim();
            _stepsLock = new ReaderWriterLockSlim();
        }

        #endregion

        #region 线程安全的字段和属性

        /// <summary>
        /// 用于保护 Obj 集合的读写锁
        /// </summary>
        private readonly ReaderWriterLockSlim _objLock;

        /// <summary>
        /// 用于保护 IempSteps 集合的读写锁
        /// </summary>
        private readonly ReaderWriterLockSlim _stepsLock;

        /// <summary>
        /// 步骤序号的私有字段，使用volatile确保可见性
        /// </summary>
        private volatile int _stepNum;

        /// <summary>
        /// 当前操作状态的私有字段，使用volatile确保可见性
        /// </summary>
        private volatile bool _status;

        /// <summary>
        /// 通用数据字段，使用volatile确保可见性
        /// </summary>
        private volatile int _t;

        /// <summary>
        /// 模型类型名称的私有字段
        /// 字符串引用的赋值在.NET中是原子操作，但为了一致性，也使用同步
        /// </summary>
        private string _modelTypeName;

        /// <summary>
        /// 模型名称的私有字段
        /// </summary>
        private string _modelName;

        /// <summary>
        /// 项目名称的私有字段
        /// </summary>
        private string _itemName;

        /// <summary>
        /// 步骤名称的私有字段
        /// </summary>
        private string _stepName;

        #endregion

        #region 公共属性（线程安全访问）

        /// <summary>
        /// 通用数据属性
        /// 使用Interlocked确保原子操作
        /// </summary>
        public int T
        {
            get => _t;
            set => Interlocked.Exchange(ref _t, value);
        }

        /// <summary>
        /// 当前操作的状态，true表示成功，false表示失败
        /// 布尔值的读写在.NET中是原子的，但使用volatile确保可见性
        /// </summary>
        public bool Status
        {
            get => _status;
            set => _status = value;
        }

        /// <summary>
        /// 步骤序号，用于标识当前操作的步骤
        /// 设置时会触发事件通知，使用锁保护事件触发过程
        /// </summary>
        public int StepNum
        {
            get => _stepNum;
            set
            {
                var oldValue = Interlocked.Exchange(ref _stepNum, value);
                if (oldValue != value)
                {
                    // 在锁保护下触发事件，避免事件处理过程中的竞态条件
                    OnStepNumChanged(value);
                }
            }
        }

        /// <summary>
        /// 产品类型名称，用于区分不同的产品或模型
        /// 使用锁保护字符串字段的读写操作
        /// </summary>
        public string ModelTypeName
        {
            get
            {
                lock (this)
                {
                    return _modelTypeName;
                }
            }
            set
            {
                lock (this)
                {
                    _modelTypeName = value;
                }
            }
        }

        /// <summary>
        /// 产品型号名称，用于具体标识某一型号的产品
        /// </summary>
        public string ModelName
        {
            get
            {
                lock (this)
                {
                    return _modelName;
                }
            }
            set
            {
                lock (this)
                {
                    _modelName = value;
                }
            }
        }

        /// <summary>
        /// 项点名称，用于标识具体的试验或操作步骤
        /// </summary>
        public string ItemName
        {
            get
            {
                lock (this)
                {
                    return _itemName;
                }
            }
            set
            {
                lock (this)
                {
                    _itemName = value;
                }
            }
        }

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName
        {
            get
            {
                lock (this)
                {
                    return _stepName;
                }
            }
            set
            {
                lock (this)
                {
                    _stepName = value;
                }
            }
        }

        #endregion

        #region 集合属性（线程安全访问）

        /// <summary>
        /// 自定义变量列表的私有字段
        /// 注意：List<T>不是线程安全的，所有访问都需要通过锁保护
        /// </summary>
        private readonly List<object> _obj = new();

        /// <summary>
        /// 自定义变量列表
        /// 为了保持向后兼容性，仍然返回List<object>，但建议使用专门的线程安全方法
        /// </summary>
        public List<object> Obj
        {
            get
            {
                _objLock.EnterReadLock();
                try
                {
                    // 返回副本以避免外部直接修改集合
                    return new List<object>(_obj);
                }
                finally
                {
                    _objLock.ExitReadLock();
                }
            }
            set
            {
                _objLock.EnterWriteLock();
                try
                {
                    _obj.Clear();
                    if (value != null)
                    {
                        _obj.AddRange(value);
                    }
                }
                finally
                {
                    _objLock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Json序列化的步骤参数的私有字段
        /// </summary>
        private readonly List<ChildModel> _iempSteps = new();

        /// <summary>
        /// Json序列化的步骤参数
        /// 返回副本以保护内部集合不被外部修改
        /// </summary>
        public List<ChildModel> IempSteps
        {
            get
            {
                _stepsLock.EnterReadLock();
                try
                {
                    return new List<ChildModel>(_iempSteps);
                }
                finally
                {
                    _stepsLock.ExitReadLock();
                }
            }
            private set
            {
                _stepsLock.EnterWriteLock();
                try
                {
                    _iempSteps.Clear();
                    if (value != null)
                    {
                        _iempSteps.AddRange(value);
                    }
                }
                finally
                {
                    _stepsLock.ExitWriteLock();
                }
            }
        }

        #endregion

        #region 线程安全的集合操作方法

        /// <summary>
        /// 线程安全地添加对象到Obj集合
        /// </summary>
        /// <param name="item">要添加的对象</param>
        public void AddObj(object item)
        {
            if (item == null) return;

            _objLock.EnterWriteLock();
            try
            {
                _obj.Add(item);
            }
            finally
            {
                _objLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 线程安全地从Obj集合移除对象
        /// </summary>
        /// <param name="item">要移除的对象</param>
        /// <returns>是否成功移除</returns>
        public bool RemoveObj(object item)
        {
            if (item == null) return false;

            _objLock.EnterWriteLock();
            try
            {
                return _obj.Remove(item);
            }
            finally
            {
                _objLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 线程安全地清空Obj集合
        /// </summary>
        public void ClearObj()
        {
            _objLock.EnterWriteLock();
            try
            {
                _obj.Clear();
            }
            finally
            {
                _objLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 线程安全地获取Obj集合中特定类型的对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>指定类型的对象集合</returns>
        public List<T> GetObjOfType<T>()
        {
            _objLock.EnterReadLock();
            try
            {
                return _obj.OfType<T>().ToList();
            }
            finally
            {
                _objLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 线程安全地添加步骤到IempSteps集合
        /// </summary>
        /// <param name="step">要添加的步骤</param>
        public void AddStep(ChildModel step)
        {
            if (step == null) return;

            _stepsLock.EnterWriteLock();
            try
            {
                _iempSteps.Add(step);
            }
            finally
            {
                _stepsLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 线程安全地从IempSteps集合移除步骤
        /// </summary>
        /// <param name="step">要移除的步骤</param>
        /// <returns>是否成功移除</returns>
        public bool RemoveStep(ChildModel step)
        {
            if (step == null) return false;

            _stepsLock.EnterWriteLock();
            try
            {
                return _iempSteps.Remove(step);
            }
            finally
            {
                _stepsLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 线程安全地清空IempSteps集合
        /// </summary>
        public void ClearSteps()
        {
            _stepsLock.EnterWriteLock();
            try
            {
                _iempSteps.Clear();
            }
            finally
            {
                _stepsLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 线程安全地获取步骤数量
        /// </summary>
        public int GetStepCount()
        {
            _stepsLock.EnterReadLock();
            try
            {
                return _iempSteps.Count;
            }
            finally
            {
                _stepsLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 线程安全地获取指定索引的步骤
        /// </summary>
        /// <param name="index">步骤索引</param>
        /// <returns>步骤对象，如果索引无效则返回null</returns>
        public ChildModel GetStep(int index)
        {
            _stepsLock.EnterReadLock();
            try
            {
                if (index >= 0 && index < _iempSteps.Count)
                {
                    return _iempSteps[index];
                }
                return null;
            }
            finally
            {
                _stepsLock.ExitReadLock();
            }
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 步骤编号变更事件
        /// 使用线程安全的事件模式
        /// </summary>
        public event Action<int> StepNumChanged;

        /// <summary>
        /// 触发步骤编号变更事件
        /// 使用锁保护事件触发过程，避免在事件处理过程中发生竞态条件
        /// </summary>
        /// <param name="newStepNum">新的步骤编号</param>
        private void OnStepNumChanged(int newStepNum)
        {
            lock (this)
            {
                try
                {
                    StepNumChanged?.Invoke(newStepNum);
                }
                catch (Exception ex)
                {
                    // 记录事件处理异常，但不抛出，避免影响主流程
                    System.Diagnostics.Debug.WriteLine($"StepNumChanged事件处理异常: {ex.Message}");
                }
            }
        }

        #endregion

        #region 批量操作方法

        /// <summary>
        /// 线程安全地批量更新配置信息
        /// 使用锁确保整个更新过程的原子性
        /// </summary>
        /// <param name="modelType">产品类型</param>
        /// <param name="modelName">产品型号</param>
        /// <param name="itemName">项目名称</param>
        public void UpdateConfiguration(string modelType, string modelName, string itemName)
        {
            lock (this)
            {
                _modelTypeName = modelType;
                _modelName = modelName;
                _itemName = itemName;
            }
        }

        /// <summary>
        /// 线程安全地重置所有数据
        /// 用于清理和重新初始化
        /// </summary>
        public void Reset()
        {
            // 按照加锁顺序获取所有锁，避免死锁
            lock (this)
            {
                _objLock.EnterWriteLock();
                try
                {
                    _stepsLock.EnterWriteLock();
                    try
                    {
                        // 重置所有字段
                        _stepNum = 0;
                        _status = false;
                        _t = 0;
                        _modelTypeName = null;
                        _modelName = null;
                        _itemName = null;
                        _stepName = null;

                        // 清空集合
                        _obj.Clear();
                        _iempSteps.Clear();
                    }
                    finally
                    {
                        _stepsLock.ExitWriteLock();
                    }
                }
                finally
                {
                    _objLock.ExitWriteLock();
                }
            }
        }

        #endregion

        #region IDisposable 实现

        private bool _disposed = false;

        /// <summary>
        /// 释放资源
        /// 实现IDisposable接口，确保锁资源得到正确释放
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 受保护的释放方法
        /// </summary>
        /// <param name="disposing">是否正在释放托管资源</param>
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // 释放托管资源
                    _objLock?.Dispose();
                    _stepsLock?.Dispose();
                }
                _disposed = true;
            }
        }

        #endregion

        #region 调试和诊断方法

        /// <summary>
        /// 获取当前实例的诊断信息
        /// 用于调试和监控
        /// </summary>
        /// <returns>诊断信息字符串</returns>
        public string GetDiagnosticInfo()
        {
            lock (this)
            {
                _objLock.EnterReadLock();
                try
                {
                    _stepsLock.EnterReadLock();
                    try
                    {
                        return $"SingletonStatus诊断信息:\n" +
                               $"- 步骤编号: {_stepNum}\n" +
                               $"- 状态: {_status}\n" +
                               $"- T值: {_t}\n" +
                               $"- 模型类型: {_modelTypeName ?? "未设置"}\n" +
                               $"- 模型名称: {_modelName ?? "未设置"}\n" +
                               $"- 项目名称: {_itemName ?? "未设置"}\n" +
                               $"- 步骤名称: {_stepName ?? "未设置"}\n" +
                               $"- Obj集合大小: {_obj.Count}\n" +
                               $"- Steps集合大小: {_iempSteps.Count}\n" +
                               $"- 是否已释放: {_disposed}";
                    }
                    finally
                    {
                        _stepsLock.ExitReadLock();
                    }
                }
                finally
                {
                    _objLock.ExitReadLock();
                }
            }
        }

        #endregion
    }
}