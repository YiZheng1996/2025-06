using RW.DSL;

namespace MainUI.Service
{
    /// <summary>
    /// 执行测试服务类，负责执行DSL脚本中的测试项目
    /// </summary>
    /// <param name="dslService">DSL服务类</param>
    /// <param name="updateTableColor">项点名称行颜色</param>
    public class TestExecutionService(DSLService dslService, Action<ItemPointModel, int> updateTableColor)
    {
        private CancellationTokenSource _cancellationTokenSource = new();

        /// <summary>
        /// 开始执行测试
        /// </summary>
        /// <param name="testPoints"></param>
        /// <returns></returns>
        public async Task StartTest(List<ItemPointModel> testPoints)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await Task.Factory.StartNew(() => ExecuteTests(testPoints),
                _cancellationTokenSource.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        /// <summary>
        /// 对提供的测试点集合执行一系列测试。
        /// </summary>
        /// <param name="testPoints">试验项点表单，状态类集合</param>
        private void ExecuteTests(List<ItemPointModel> testPoints)
        {
            // 重置所有测试点的颜色状态为0（未开始状态）
            testPoints.ForEach(i => i.ColorState = 0);

            foreach (var point in testPoints)
            {
                if (!point.Check) continue;

                if (dslService.HasProcedure(point.ItemName))
                {
                    ExecuteSingleTest(point);
                }
                else
                {
                    throw new Exception("未找到自动试验文件，试验开始失败！");
                }
            }
        }

        /// <summary>
        /// 执行单次测试
        /// </summary>
        /// <param name="point">试验项点表单，单次状态类</param>
        private void ExecuteSingleTest(ItemPointModel point)
        {
            updateTableColor(point, 1);
            try
            {
                var procedure = dslService.GetProcedure(point.ItemName);
                procedure?.Execute();
            }
            catch (Exception ex)
            {
                HandleTestException(ex);
            }
            finally
            {
                updateTableColor(point, 2);
            }
        }

        /// <summary>
        /// 记录错误详细信息
        /// </summary>
        /// <param name="ex"></param>
        private static void HandleTestException(Exception ex)
        {
            switch (ex)
            {
                case RWDSLException dslex:
                    NlogHelper.Default.Debug($"执行DSL错误：RW.DSL.RWDSLException{dslex.Message}");
                    break;
                case NullReferenceException nullex:
                    NlogHelper.Default.Debug($"执行DSL错误：NullReferenceException{nullex.Message}");
                    break;
                default:
                    NlogHelper.Default.Error($"执行DSL错误：Exception{ex.Message}");
                    MessageHelper.MessageOK(ex.Message);
                    break;
            }
        }

        /// <summary>
        /// 停止测试
        /// </summary>
        public void StopTest()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}