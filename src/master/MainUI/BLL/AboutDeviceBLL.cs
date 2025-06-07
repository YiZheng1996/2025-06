namespace MainUI.BLL
{
    public class AboutDeviceBLL
    {
        public List<AboutDeviceModel> 
            GetAboutDevice(DateTime time, DateTime totime) => VarHelper.fsql
            .Select<AboutDeviceModel>()
            .Where(x => x.OnTime.Between(time, totime))
            .ToList();
    }
}
