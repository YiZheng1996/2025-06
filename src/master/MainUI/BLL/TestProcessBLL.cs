namespace MainUI.BLL
{
    internal class TestProcessBLL
    {
        public List<TestProcessModel> GetTestProcess() => VarHelper.fsql
               .Select<TestProcessModel>()
               .ToList(item => new TestProcessModel
               {
                   ID = item.ID,
                   ProcessName = item.ProcessName,
                   IsVisible = item.IsVisible
               });

        public List<TestProcessModel> GetTestProcess(bool IsVisible) => VarHelper.fsql
              .Select<TestProcessModel>()
              .Where(w => w.IsVisible == IsVisible)
              .ToList(item => new TestProcessModel
              {
                  ID = item.ID,
                  ProcessName = item.ProcessName,
                  IsVisible = item.IsVisible
              });

        public bool AddTestProcess() =>
            VarHelper.fsql.Insert(new TestProcessModel
            {
                ProcessName = "",
                IsVisible = true
            })
            .ExecuteAffrows() > 0;

        public bool DelTestProcess(int ID) =>
            VarHelper.fsql.Delete<TestProcessModel>()
            .Where(a => a.ID == ID)
            .ExecuteAffrows() > 0;

        public bool SaveTestProcess(TestProcessModel model) =>
            VarHelper.fsql.Update<TestProcessModel>()
            .Set(s => s.ProcessName, model.ProcessName)
            .Set(s => s.IsVisible, model.Enable)
            .Where(w => w.ID == model.ID)
            .ExecuteAffrows() > 0;
    }
}
