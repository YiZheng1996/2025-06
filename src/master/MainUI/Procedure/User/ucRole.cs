﻿using AntdUI;

namespace MainUI.Procedure.User
{
    public partial class ucRole : UserControl
    {
        private RoleModel roleModel = new();
        private readonly RoleBLL roleBLL = new();
        public ucRole() => InitializeComponent();

        private void LoadData()
        {
            Tables.Columns = [
                new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
                new Column("RoleName","角色名称"){ Align = ColumnAlign.Center , Width="auto" },
                new Column("Describe","角色描述"){ Align = ColumnAlign.Center , Width="auto" },
           ];
            Tables.DataSource = roleBLL.GetRoles();
        }

        private void LoadData(RoleModel model)
        {
            using frmRoleEdit edit = new(model);
            edit.ShowDialog();
            LoadData();
        }

        private void ucRole_Load(object sender, EventArgs e)
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
                if (roleBLL.DelRole(roleModel.ID))
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
            LoadData(roleModel);
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is RoleModel model)
            {
                roleModel = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            LoadData(roleModel);
        }
    }
}
