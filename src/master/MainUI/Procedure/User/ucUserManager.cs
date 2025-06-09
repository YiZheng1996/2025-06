using AntdUI;

namespace MainUI.Procedure.User
{
    public partial class ucUserManager : UserControl
    {
        private OperateUserBLL bLL = new();
        private OperateUserNewModel OperateUserModel = new();
        public ucUserManager() => InitializeComponent();

        private void ucUserManager_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        //数据绑定
        private void LoadData()
        {
            Tables.Columns =
           [
               new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
               new Column("Username","用户名"){ Align = ColumnAlign.Center , Width="auto" },
               new Column("Role_ID","权限ID"){ Align = ColumnAlign.Center , Width="auto" },
               new Column("Describe","权限"){ Align = ColumnAlign.Center , Width="auto" },
           ];
            Tables.DataSource = bLL.GetUsers();
        }

        private void LoadData(OperateUserNewModel model)
        {
            using frmUserEdit edit = new(model);
            edit.ShowDialog();
            LoadData();
        }

        //添加用户按钮
        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadData(null);
        }

        //点击编辑用户按钮
        private void btnEdit_Click(object sender, EventArgs e)
        {
            LoadData(OperateUserModel);
        }


        //删除用户按钮
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OperateUserModel.ID == 1)
            {
                MessageHelper.MessageOK("超级管理员不能删除！", TType.Error);
                return;
            }

            var DialogResult = MessageHelper.MessageYes("是否删除选中记录？", TType.Warn);
            if (DialogResult == DialogResult.OK)
            {
                if (bLL.RemoveByUserJob(OperateUserModel) > 0)
                {
                    MessageHelper.MessageOK("删除成功！");
                }
                else
                {
                    MessageHelper.MessageOK("删除失败！");
                }
                LoadData();
            }
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is OperateUserNewModel model)
            {
                OperateUserModel = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            LoadData(OperateUserModel);
        }
    }
}
