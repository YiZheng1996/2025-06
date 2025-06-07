using AntdUI;
namespace MainUI.Procedure
{
    public partial class ucKindManage : ucBaseManagerUI
    {
        private ModelsType modelsType = new();
        private readonly ModelTypeBLL modelBLL = new();
        public ucKindManage() => InitializeComponent();

        private void ucModelManage_Load(object sender, EventArgs e) => LoadData();

        private void LoadData()
        {
            Tables.Columns = [
                new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
                new Column("ModelTypeName","类型名称"){ Align = ColumnAlign.Center , Width="auto" },
                new Column("Mark","类型备注"){ Align = ColumnAlign.Center , Width="auto" },
           ];
            Tables.DataSource = modelBLL.GetAllModelType();
        }

        private void LoadData(ModelsType model)
        {
            using frmModelTypeEdit edit = new(model);
            edit.ShowDialog();
            LoadData();
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is ModelsType model)
            {
                modelsType = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            LoadData(modelsType);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadData(null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            LoadData(modelsType);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var DialogResult = MessageHelper.MessageYes("是否删除选中记录？", TType.Warn);
            if (DialogResult == DialogResult.OK)
            {
                if (modelBLL.Delete(modelsType.ID))
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
    }
}
