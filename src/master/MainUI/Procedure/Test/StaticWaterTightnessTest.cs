namespace MainUI.Procedure.Test
{
    public class StaticWaterTightnessTest : BaseTest
    {
        public override async Task<bool> Execute(CancellationToken cancellationToken)
        {
            TestStatus(true);
            TxtTips("试验开始");

            try
            {
                // 执行测试逻辑
                await Task.Delay(1000, cancellationToken);
                return true;
            }
            finally
            {
                TestStatus(false);
            }
        }
    }
}
