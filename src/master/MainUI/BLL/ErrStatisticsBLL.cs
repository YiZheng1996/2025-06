namespace MainUI.BLL
{
    public class ErrStatisticsBLL
    {
        public List<ErrStatisticsModel> GetErrStatistics() =>
            VarHelper.fsql.Select<ErrStatisticsModel>()
            .Where(m => m.IsDelete == 0)
            .ToList();

        public bool DelErrStatistics(int ID) =>
            VarHelper.fsql.Update<ErrStatisticsModel>()
            .Set(m => m.IsDelete, 1)
            .Set(m => m.DeleteTime, DateTime.Now)
            .Where(m => m.ID == ID)
            .ExecuteAffrows() > 0;

        public bool UpdateErrStatistics(ErrStatisticsModel model) =>
            VarHelper.fsql.Update<ErrStatisticsModel>()
            .SetSource(model)
            .Where(m => m.ID == model.ID)
            .ExecuteAffrows() > 0;

        public bool AddErrStatistics(ErrStatisticsModel model) =>
            VarHelper.fsql.Insert(model)
            .ExecuteAffrows() > 0;
    }
}
