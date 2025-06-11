namespace MainUI.Procedure.Test
{
    public class AirTightnessTest : BaseTest
    {
        public override async Task<bool> Execute(CancellationToken cancellationToken)
        {
            TestStatus(true);
            TxtTips("开始测试");

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
