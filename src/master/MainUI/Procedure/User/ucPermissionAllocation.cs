namespace MainUI.Procedure.User
{
    public partial class ucPermissionAllocation : UserControl
    {
        private Dictionary<int, UICheckBox> DicChecks = [];
        public ucPermissionAllocation() => InitializeComponent();

        private void ucPermissionAllocation_Load(object sender, EventArgs e)
        {
            try
            {
                GetCboRoleData();
                GetPermissions();
                GetPermissionChecks();
                cboRole.SelectedValueChanged += cboRole_SelectedValueChanged;
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"权限设定界面发生错误：{ex.Message}");
            }
        }

        // 获取角色数据
        private void GetCboRoleData()
        {
            try
            {
                RoleBLL roleBLL = new();
                cboRole.DataSource = roleBLL.GetRoles();
                cboRole.DisplayMember = $"Describe";
                cboRole.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("获取角色数据错误：{0}", ex);
                MessageHelper.MessageOK($"获取角色数据错误：{ex.Message}");
            }
        }

        // 获取权限数据
        private void GetPermissions()
        {
            try
            {
                PermissionBLL permissionBLL = new();
                var permissions = permissionBLL.GetPermissions();
                foreach (var item in permissions)
                {
                    UICheckBox checkBox = new()
                    {
                        Text = item.PermissionName,
                        Tag = item.ID,
                        AutoSize = true,
                        CheckBoxSize = 18,
                        Font = new Font("微软雅黑", 13),
                        Margin = new System.Windows.Forms.Padding(20)
                    };
                    DicChecks.TryAdd(checkBox.Tag.ToInt32(), checkBox);
                    PanelPermissions.Padding = new System.Windows.Forms.Padding(15);
                    PanelPermissions.Controls.Add(checkBox);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载权限列表错误：{0}", ex);
                MessageHelper.MessageOK($"加载权限列表错误：{ex.Message}");
            }
        }

        // 获取权限Check数据
        private void GetPermissionChecks()
        {
            try
            {
                var UserID = cboRole.SelectedValue.ToInt32();
                if (UserID == 0 || DicChecks.Count == 0) return;
                PermissionAllocationBLL rolePermissionBLL = new();
                var Permissions = rolePermissionBLL.GetPermissionChecks(UserID);
                for (int i = 0; i < Permissions.Count; i++)
                {
                    int PermissionID = Permissions[i].Permission_id;
                    if (PermissionID == DicChecks[PermissionID].Tag.ToInt32())
                    {
                        DicChecks[PermissionID].Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("获取权限Check发生错误：{0}", ex);
                MessageHelper.MessageOK($"获取权限Check发生错误：{ex.Message}");
            }
        }

        // 取消所有Check控件选择
        private void ClearChecks()
        {
            foreach (var item in DicChecks)
            {
                item.Value.Checked = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboRole.SelectedValue == null)
                {
                    MessageHelper.MessageOK("请先选择一个角色。");
                    return;
                }

                int roleId = Convert.ToInt32(cboRole.SelectedValue);
                List<int> selectedPermissionIds = [];

                foreach (var pair in DicChecks)
                {
                    UICheckBox checkBox = pair.Value;
                    if (checkBox.Checked)
                    {
                        if (checkBox.Tag is int permissionId)
                        {
                            selectedPermissionIds.Add(permissionId);
                        }
                    }
                }

                if (selectedPermissionIds.Count == 0)
                {
                    MessageHelper.MessageOK("请至少选择一个权限。");
                    return;
                }

                PermissionAllocationBLL rolePermissionBLL = new();
                bool result = rolePermissionBLL.SetRolePermissions(roleId, selectedPermissionIds);

                if (result)
                {
                    MessageHelper.MessageOK("权限分配成功！");
                }
                else
                {
                    MessageHelper.MessageOK("权限分配失败，请重试。");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("权限分配发生错误：{0}", ex);
                MessageHelper.MessageOK($"权限分配发生错误：{ex.Message}");
            }
        }

        private void cboRole_SelectedValueChanged(object sender, EventArgs e)
        {
            ClearChecks();
            GetPermissionChecks();
        }
    }
}
