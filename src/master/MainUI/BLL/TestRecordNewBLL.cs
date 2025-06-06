namespace MainUI.BLL
{
    internal class TestRecordNewBLL
    {
        // 更新测试记录
        public bool UpdateTestRecord(TestRecordModel model) => VarHelper.fsql
                   .Update<TestRecordModel>()
                   .Set(a => a.Kind, model.Kind)
                   .Set(a => a.Model, model.Model)
                   .Set(a => a.Driver, model.Driver)
                   .Set(a => a.TestID, model.TestID)
                   .Set(a => a.Tester, model.Tester)
                   .Set(a => a.TestTime, model.TestTime)
                   .Set(a => a.ReportPath, model.ReportPath)
                   .Where(a => a.ID == model.ID)
                   .ExecuteAffrows() > 0;

        // 添加测试记录
        public bool SaveTestRecord(TestRecordModel model) => VarHelper.fsql.
            Insert(model).ExecuteAffrows() > 0;

        // 删除测试记录
        public bool DeleteTestRecord(int id) => VarHelper.fsql
            .Delete<TestRecordModel>()
            .Where(a => a.ID == id)
            .ExecuteAffrows() > 0;

        // 获取测试记录
        public List<TestRecordModel> GetTestRecord(TestRecordModel model, DateTime toTime) => VarHelper.fsql
            .Select<TestRecordModel>()
            .WhereIf(!string.IsNullOrEmpty(model.Kind), t => t.Kind == model.Kind)
            .WhereIf(!string.IsNullOrEmpty(model.Model), t => t.Model == model.Model)
            .WhereIf(!string.IsNullOrEmpty(model.TestID), t => t.TestID.Contains(model.TestID))
            .WhereIf(!string.IsNullOrEmpty(model.Tester), t => t.Tester == model.Tester)
            .Where( t => t.TestTime.Between(model.TestTime, toTime))
            .ToList();
    }
}
