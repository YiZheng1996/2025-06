namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 气密性试验
    /// </summary>
    /// <param name="cancellationToken"></param>
    public class AirTightnessTest() : BaseTest
    {
        public override Task<bool> Execute(CancellationToken cancellationToken)
        {
            TestStatus(true);
            TxtTips("试验开始");
            OPCHelper.TestCongrp[69] = 20;
            Delay(90, 100, cancellationToken, () => OPCHelper.TestCongrp[123].ToString() == "21");
            string PressureTime01 = OPCHelper.TestCongrp[124].ToString();
            string PressureTime02 = OPCHelper.TestCongrp[125].ToString();
            string PressureTime03 = OPCHelper.TestCongrp[126].ToString();
            TxtTips("试验结束");
            TestStatus(false);
            return Task.FromResult(false);
        }
    }
}
