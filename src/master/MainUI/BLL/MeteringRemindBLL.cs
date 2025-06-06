namespace MainUI.BLL
{
    internal class MeteringRemindBLL
    {
        public List<MeteringRemindModel> GetMeteringReminds()
            => VarHelper.fsql
            .Select<MeteringRemindModel>()
            .Where(m => m.IsDelete == 0)
            .ToList();

        public bool DelMeteringReminds(int ID)
            => VarHelper.fsql
            .Update<MeteringRemindModel>()
            .Set(m => m.IsDelete, 1)
            .Set(m => m.DeleteTime, DateTime.Now)
            .Where(m => m.ID == ID)
            .ExecuteAffrows() > 0;

        public bool UpdateMeteringReminds(MeteringRemindModel model)
            => VarHelper.fsql
            .Update<MeteringRemindModel>()
            .SetSource(model)
            .Where(m => m.ID == model.ID)
            .ExecuteAffrows() > 0;

        public bool AddMeteringReminds(MeteringRemindModel model)
            => VarHelper.fsql
            .Insert(model)
            .ExecuteAffrows() > 0;
    }
}
