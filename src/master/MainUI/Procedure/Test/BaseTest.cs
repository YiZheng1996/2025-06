namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 测试基础类，提供测试流程控制和事件通知功能
    /// </summary>
    public class BaseTest
    {
        #region 事件定义
        public static event Action<bool> TestStateChanged;
        public static event EventHandler<object> TipsChanged;
        public static event EventHandler<int> TimingChanged;
        public static event Action<int> WaitTick;
        public static event Action<int, string> WaitStepTick;
        public static event Action<int, bool> ExcuteEnd;
        public static event Action<int, bool> Excuteing;
        
        protected virtual void OnTestStateChanged(bool state) => 
            TestStateChanged?.Invoke(state);
        
        protected virtual void OnTipsChanged(object info) => 
            TipsChanged?.Invoke(this, info);

        protected virtual void OnTimingChanged(int seconds) =>
            TimingChanged?.Invoke(this, seconds);
            
        protected virtual void OnWaitTick(int tick) =>
            WaitTick?.Invoke(tick);
            
        protected virtual void OnWaitStepTick(int tick, string stepName) =>
            WaitStepTick?.Invoke(tick, stepName);
            
        protected virtual void OnExcuteEnd(int index, bool state) =>
            ExcuteEnd?.Invoke(index, state);
            
        protected virtual void OnExcuteing(int index, bool state) =>
            Excuteing?.Invoke(index, state);
        #endregion

        #region 属性
        public static ParaConfig para { get; set; } = new();
        public static Report.UcGrid ucGrid { get; set; }
        public frmMainMenu frm { get; set; }
        public UcHMI Hmi { get; set; }
        public bool IsTesting { get; private set; }
        #endregion

        #region 延时和等待操作
        /// <summary>
        /// 带条件的超时等待操作
        /// </summary>
        /// <param name="timeout">超时时间(秒)</param>
        /// <param name="breakTime">检查间隔(毫秒)</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <param name="conditions">退出条件(任一为true时退出)</param>
        public static void Delay(int timeout, int breakTime, CancellationToken cancellationToken, params Func<bool>[] conditions)
        {
            Stopwatch sw = Stopwatch.StartNew();
            conditions ??= [];

            while (sw.Elapsed.TotalSeconds < timeout && !cancellationToken.IsCancellationRequested)
            {
                Task.Delay(breakTime).Wait();
                if (conditions.Any(condition => condition()))
                {
                    return;
                }
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        /// <summary>
        /// 简单延时
        /// </summary>
        public void Delay(double seconds, CancellationToken cancellationToken) => 
            Delay(seconds, 100, () => true, cancellationToken);

        /// <summary>
        /// 带回调的延时,返回是否超时
        /// </summary>
        public bool Delay(double seconds, int interval, Func<bool> wait, CancellationToken cancellationToken)
        {
            var elapsed = 0;
            var timeout = seconds * 1000;

            while (elapsed <= timeout && wait() && !cancellationToken.IsCancellationRequested)
            {
                Thread.Sleep(interval);
                elapsed += interval;
                OnWaitTick(elapsed);
                cancellationToken.ThrowIfCancellationRequested();
            }

            return elapsed > timeout;
        }

        /// <summary>
        /// 带步骤名称的延时
        /// </summary>
        public void Delay(int seconds, string waitName) => 
            Delay(seconds, 100, () => true, waitName);

        /// <summary>
        /// 带步骤名称和回调的延时,返回是否完成
        /// </summary>
        public bool Delay(int seconds, int interval, Func<bool> wait, string waitName)
        {
            var remaining = seconds * 1000;
            
            while (remaining > 0 && wait() && IsTesting)
            {
                Thread.Sleep(interval);
                remaining -= interval;
                OnWaitStepTick(remaining, waitName);
            }
            return remaining <= 0;
        }
        #endregion

        #region 状态更新方法
        /// <summary>
        /// 更新已执行时间
        /// </summary>
        public void LblTime(int time, string waitName) => 
            OnWaitStepTick(time, waitName);

        /// <summary>
        /// 显示提示信息
        /// </summary>
        public void TxtTips(object message) => 
            OnTipsChanged(message);

        /// <summary>
        /// 更新计时
        /// </summary>
        public void TxtTiming(int seconds) => 
            OnTimingChanged(seconds);

        /// <summary>
        /// 更新测试项执行完成状态
        /// </summary>
        public void Testend(int index, bool state) => 
            OnExcuteEnd(index, state);

        /// <summary>
        /// 更新测试项执行中状态
        /// </summary>
        public void Testing(int index, bool state) => 
            OnExcuteing(index, state);

        /// <summary>
        /// 更新测试状态
        /// </summary>
        public void TestStatus(bool isTest)
        {
            IsTesting = isTest;
            OnTestStateChanged(isTest);
        }
        #endregion

        #region 虚方法
        /// <summary>
        /// 在子类中执行测试过程
        /// </summary>
        public virtual Task<bool> Execute
            (CancellationToken cancellationToken) => 
            Task.FromResult(true);

        /// <summary>
        /// 测试项初始化
        /// </summary>
        public virtual void Init() { }
        #endregion
    }
}
