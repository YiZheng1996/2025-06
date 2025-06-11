namespace MainUI.Service
{
    public class CountdownService(UIPanel timeLabel)
    {
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// 启动倒计时
        /// </summary>
        public async Task StartCountdown(int totalMinutes, CancellationToken externalToken)
        {
            try
            {
                // 1. 创建新的 CancellationTokenSource
                _cancellationTokenSource = new CancellationTokenSource();

                // 2. 将外部token和内部token链接起来
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                    externalToken,
                    _cancellationTokenSource.Token
                );

                TimeSpan remainingTime = TimeSpan.FromMinutes(totalMinutes);

                await Task.Run(async () =>
                {
                    // 3. 使用链接后的token
                    while (!linkedCts.Token.IsCancellationRequested)
                    {
                        timeLabel.Invoke(() =>
                        {
                            timeLabel.Text = $"{remainingTime.Hours:D2}:{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";
                        });

                        // 4. 使用链接后的token
                        await Task.Delay(1000, linkedCts.Token);
                        remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
                    }
                }, linkedCts.Token); // 5. 使用链接后的token
            }
            catch (OperationCanceledException)
            {
                // 正常的取消操作
                timeLabel.Invoke(() =>
                {
                    timeLabel.Text = "00:00:00";
                });
            }
            finally
            {
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }
        }

        /// <summary>
        /// 停止倒计时
        /// </summary>
        public void StopCountdown()
        {
            try
            {
                _cancellationTokenSource?.Cancel();
            }
            catch (ObjectDisposedException)
            {
                // 忽略已释放的对象
            }
        }
    }
}
