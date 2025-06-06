namespace MainUI.BLL
{
    public class NlogsBLL
    {
        public List<NlogsModel> FindList(string Level, DateTime from, DateTime to) =>
            VarHelper.fsql.Select<NlogsModel>()
                .WhereIf(!string.IsNullOrEmpty(Level), t => t.Level == Level)
                .Where(t => t.MessTime.Between(from, to))
                .OrderByDescending(t => t.MessTime)
                .ToList();
    }
}
