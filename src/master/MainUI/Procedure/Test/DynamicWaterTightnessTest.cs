namespace MainUI.Procedure.Test
{
    public class DynamicWaterTightnessTest : BaseTest
    {
        public override async Task<bool> Execute(CancellationToken cancellationToken)
        {
            TestStatus(true);
            TxtTips("开始测试");

            try
            {
                // 执行测试逻辑
                await Task.Delay(1000, cancellationToken);
                Delay(90, 100, cancellationToken, 
                    () => OPCHelper.TestCongrp[123].ToString() == "10");
                return true;
            }
            finally
            {
                TestStatus(false);
            }
        }
    }
}
