namespace MainUI.BLL
{
    internal class TestRecordNewBLL
    {
        // 更新测试记录
        public bool UpdateTestRecord(TestRecordModel model) => VarHelper.fsql
                   .Update<TestRecordModel>()
                   .Set(a => a.KindID, model.KindID)
                   .Set(a => a.ModelID, model.ModelID)
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
        public List<TestRecordModelDto> GetTestRecord(TestRecordModel model, DateTime toTime) => VarHelper.fsql
            .Select<TestRecordModel, ModelsType, Models>()
            .LeftJoin((t, mt, m) => t.KindID == mt.ID)
            .LeftJoin((t, mt, m) => t.ModelID == m.ID)
            .WhereIf(model.KindID != -1, (t, mt, m) => t.KindID == model.KindID)
            .WhereIf(model.ModelID != -1, (t, mt, m) => t.ModelID == model.ModelID)
            .WhereIf(!string.IsNullOrEmpty(model.TestID)
            , (t, mt, m) => t.TestID.Contains(model.TestID))  
            .WhereIf(!string.IsNullOrEmpty(model.Tester)
            , (t, mt, m) => t.Tester == model.Tester)  
            .Where((t, mt, m) => t.TestTime.Between(model.TestTime, toTime))
            .ToList<TestRecordModelDto>();
    }
}
