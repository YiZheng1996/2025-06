using MainUI.CurrencyHelper;

namespace MainUI.BLL
{
    public class TestStepBLL
    {
        public List<TestStepModel> GetTestSteps(TestStep Step) =>
            VarHelper.fsql.Select<TestStepModel>()
            .Where(x => x.ModelID == Step.ModelID)
            .ToList();

        public bool DeleTestStep(int ModelID) =>
            VarHelper.fsql.Delete<TestStepModel>()
            .Where(x => x.ModelID == ModelID)
            .ExecuteAffrows() > 0;

        public bool InsertTestStep(List<TestStepModel> Steps, int ModelID)
        {
            DeleTestStep(ModelID);
            return VarHelper.fsql.Insert(Steps)
                .ExecuteAffrows() > 0;
        }

        public List<TestStepNewModel> GetTestItems(int ModelID) => VarHelper.fsql
           .Select<TestStepModel, TestProcessModel>()
           .LeftJoin((s, p) => s.TestProcessID == p.ID)
           .Where((s, p) => p.IsVisible == true && s.ModelID == ModelID)
           .ToList((s, p) => new TestStepNewModel()
           {
               ID = s.ID,
               Step = s.Step,
               ProcessName = s.ProcessName,
               ModelID = s.ModelID,
               TestProcessID = p.ID,
               TestProcessName = p.ProcessName,
               IsVisible = p.IsVisible,
           });
    }
}
