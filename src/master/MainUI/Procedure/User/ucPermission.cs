using AntdUI;

namespace MainUI.Procedure.User
{
    public partial class ucPermission : UserControl
    {
        private PermissionModel PermissionModel = new();
        private readonly PermissionBLL permissionBLL = new();
        public ucPermission()
        {
            InitializeComponent();
        }


        private void LoadData()
        {
            Tables.Columns = [
                new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
                new Column("PermissionName","权限名称"){ Align = ColumnAlign.Center , Width="auto" },
                new Column("PermissionCode","权限代码"){ Align = ColumnAlign.Center , Width="auto" , Visible = false},
                new Column("ControlName","控件名称"){ Align = ColumnAlign.Center , Width="auto" },
               
                new Column("PermissionNotes","备注"){ Align = ColumnAlign.Center , Width="auto" },
           ];
            Tables.DataSource = permissionBLL.GetPermissions();
        }

        private void LoadData(PermissionModel model)
        {
            using frmPermissionEdit edit = new(model);
            edit.ShowDialog();
            LoadData();
        }

        private void ucPermission_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadData(null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var DialogResult = MessageHelper.MessageYes("是否删除选中记录？", TType.Warn);
            if (DialogResult == DialogResult.OK)
            {
                if (permissionBLL.DelPermission(PermissionModel.ID))
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            LoadData(PermissionModel);
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is PermissionModel model)
            {
                PermissionModel = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            LoadData(PermissionModel);
        }
    }
}
