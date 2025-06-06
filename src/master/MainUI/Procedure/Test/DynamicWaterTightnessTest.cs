using MainUI.CurrencyHelper;

namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 动态水密性试验
    /// </summary>
    /// <param name="cancellationToken"></param>
    public class DynamicWaterTightnessTest() : BaseTest
    {
        public override Task<bool> Execute(CancellationToken cancellationToken)
        {
            TestStatus(true);
            var TaskState = cancellationToken.IsCancellationRequested;
            OPCHelper.TestCongrp[69] = 30;
            Delay(90, 100, cancellationToken, () => OPCHelper.TestCongrp[123].ToString() == "31");
            TestStatus(false);
            return Task.FromResult(false);
        }
    }
}
