using AntdUI;

namespace MainUI.Procedure
{
    public partial class ucModelManage : ucBaseManagerUI
    {
        Models model = new();
        ModelBLL modelBLL = new();
        ModelTypeBLL bModelType = new();
        public ucModelManage() => InitializeComponent();

        private void ucModelManage_Load(object sender, EventArgs e)
        {
            GetModelType();
            LoadData();
        }

        public void GetModelType()
        {
            cboModelType.DisplayMember = "ModelTypeName";
            cboModelType.ValueMember = "ID";
            cboModelType.DataSource = bModelType.GetModels();
            cboModelType.SelectedValueChanged += CboModelType_SelectedValueChanged;
        }

        private void CboModelType_SelectedValueChanged(object sender, EventArgs e)
        {
            Tables.DataSource = bModelType
                .GetNewModels(cboModelType.SelectedValue.ToInt32());
        }

        private void LoadData()
        {
            Tables.Columns = [
                new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
                new Column("TypeID","类型ID"){ Align = ColumnAlign.Center , Width="auto", Visible = false },
                new Column("ModelName","型号名称"){ Align = ColumnAlign.Center , Width="auto" },
                new Column("DrawingNo","产品图号"){ Align = ColumnAlign.Center , Width="auto" },
                new Column("ReleaseTime","发布时间"){ Align = ColumnAlign.Center , Width="auto" },
                new Column("Mark","型号描述"){ Align = ColumnAlign.Center , Width="auto" },
                new Column("Buttns","操作",ColumnAlign.Center){ Width = "70"}
           ];
            Tables.DataSource = bModelType
                .GetNewModels(cboModelType.SelectedValue.ToInt32());
        }

        private void LoadData(Models model)
        {
            using frmModelEdit edit = new(model);
            edit.ShowDialog();
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
                if (modelBLL.Delete(model.ID))
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
            LoadData(model);
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is Models model)
            {
                this.model = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            LoadData(model);
        }

        private void Tables_CellButtonClick(object sender, TableButtonEventArgs e)
        {
            try
            {
                if (e.Record is NewModels data)
                {
                    if (e.Btn.Id == "Release")
                    {
                        if (DialogResult.OK == MessageHelper.MessageYes($"确认发布型号{data.ModelName}吗？"))
                        {
                            if (modelBLL.IsRelease(data))
                            {
                                LoadData();
                                MessageHelper.MessageOK($"发布成功！");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK("发布错误：" + ex.Message);
            }
        }
    }
}
