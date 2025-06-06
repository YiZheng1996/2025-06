using MainUI.CurrencyHelper;
namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 静态水密性试验
    /// </summary>
    /// <param name="cancellationToken"></param>
    public class StaticWaterTightnessTest() : BaseTest
    {
        public override Task<bool> Execute(CancellationToken cancellationToken)
        {
            TestStatus(true);
            TxtTips("试验开始");
            var TaskState = cancellationToken.IsCancellationRequested;
            OPCHelper.TestCongrp[69] = 10;
            Delay(90, 100, cancellationToken, () => OPCHelper.TestCongrp[123].ToString() == "10");
            TxtTips("试验完成");
            TestStatus(false);
            return Task.FromResult(false);
        }
    }
}
