namespace MainUI
{
    public partial class frmModelEdit : UIForm
    {
        public frmModelEdit() => InitializeComponent();

        Models model = new();
        ModelBLL modelBLL = new();
        public frmModelEdit(Models model)
        {
            InitializeComponent();
            this.model = model;
            GetModelType();
        }

        public void GetModelType()
        {
            ModelTypeBLL bModelType = new();
            cboModelType.DisplayMember = "ModelTypeName";
            cboModelType.ValueMember = "ID";
            cboModelType.DataSource = bModelType.GetModels();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string ModelName = txtModelType.Text.Trim();
                string Mark = txtMark.Text.Trim();
                int TypeID = cboModelType.SelectedValue.ToInt32();
                if (string.IsNullOrEmpty(ModelName))
                {
                    MessageHelper.MessageOK(this, "未输入型号名称！");
                    txtModelType.Focus();
                    return;
                }

                bool result = false;
                if (model == null)
                {
                    var newModel = new Models
                    {
                        TypeID = TypeID,
                        ModelName = ModelName,
                        Mark = Mark,
                    };
                    result = modelBLL.Add(newModel);
                }
                else
                {
                    model.TypeID = TypeID;
                    model.ModelName = ModelName;
                    model.Mark = Mark;
                    result = modelBLL.Update(model);
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
            if (model != null)
            {
                cboModelType.SelectedValue = model.TypeID;
                txtModelType.Text = model.ModelName;
                txtMark.Text = model.Mark;
            }
        }
    }
}