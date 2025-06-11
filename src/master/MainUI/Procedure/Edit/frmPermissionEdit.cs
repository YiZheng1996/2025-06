
namespace MainUI
{
    public partial class frmPermissionEdit : UIForm
    {
        public frmPermissionEdit() => InitializeComponent();

        private readonly PermissionBLL permissionBLL = new();
        private readonly PermissionModel permissionModel;
        public frmPermissionEdit(PermissionModel model)
        {
            InitializeComponent();
            permissionModel = model;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string PermissionName = txtPermissionName.Text.Trim();
                string PermissionNotes = txtPermissionNotes.Text.Trim();
                string PermissionCode = txtPermissionCode.Text.Trim();
                string ControlName = txtControlName.Text.Trim();

                if (string.IsNullOrEmpty(PermissionName))
                {
                    MessageHelper.MessageOK(this, "未输入名称！");
                    txtPermissionName.Focus();
                    return;
                }

                bool result = false;
                if (permissionModel == null)
                {
                    var newModel = new PermissionModel
                    {
                        PermissionName = PermissionName,
                        PermissionNotes = PermissionNotes,
                        PermissionCode = PermissionCode,
                        ControlName = ControlName,
                    };
                    result = permissionBLL.AddPermission(newModel);
                }
                else
                {
                    permissionModel.PermissionName = PermissionName;
                    permissionModel.ControlName = ControlName;
                    permissionModel.PermissionCode = PermissionCode;
                    permissionModel.PermissionNotes = PermissionNotes;
                    result = permissionBLL.UpdatePermission(permissionModel);
                }

                if (result)
                {
                    MessageHelper.MessageOK(this, "保存成功！");
                    Close();
                }
                else
                {
                    MessageHelper.MessageOK(this, "保存失败！");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存失败：", ex);
                MessageHelper.MessageOK(this, "保存失败：" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMeteringEdit_Load(object sender, EventArgs e)
        {
            if (permissionModel != null)
            {
                txtPermissionName.Text = permissionModel.PermissionName;
                txtPermissionNotes.Text = permissionModel.PermissionNotes;
                txtPermissionCode.Text = permissionModel.PermissionCode;
                txtControlName.Text = permissionModel.ControlName;
            }
        }


    }
}