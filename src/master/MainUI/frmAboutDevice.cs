using AntdUI;
using MainUI.BLL;

namespace MainUI
{
    public partial class frmAboutDevice : UIForm
    {
        public frmAboutDevice() => InitializeComponent();

        AboutDeviceBLL deviceBLL = new();
        private void frmAboutDevice_Load(object sender, EventArgs e)
        {
            LoadData();
            dtpStart.Value = DateTime.Now.AddDays(-7);
            dtpEnd.Value = DateTime.Now;
        }

        private void LoadData()
        {
            Tables.Columns = [
                new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
                new Column("OnTime","设备开机时间"){ Align = ColumnAlign.Center , Width="auto" },
                new Column("EveryDay","每天使用时长(H)"){ Align = ColumnAlign.Center , Width="auto" },
                new Column("Monthly","每月使用时长(H)"){ Align = ColumnAlign.Center , Width="auto" },
                new Column("UsageTime","设备使用总时间"){ Align = ColumnAlign.Center , Width="auto" , Visible = false },
           ];
            Tables.DataSource = deviceBLL.GetAboutDevice(dtpStart.Value, dtpEnd.Value);
        }

        private void DtpStart_ValueChanged(object sender, DateTime value) => LoadData();
    }
}
