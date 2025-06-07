using AntdUI;
using RW;

namespace MainUI
{
    public partial class frmDataManager : UIForm
    {
        private TestRecordModelDto RecordModel = new();
        TestRecordNewBLL testRecord = new();
        public frmDataManager() => InitializeComponent();

        private void frmDataManager_Load(object sender, EventArgs e)
        {
            Init();
            LoadData();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private new void Init()
        {
            try
            {
                ModelTypeBLL bModelType = new();
                dtpStartBig.Value = DateTime.Now.AddDays(-3);
                dtpStartEnd.Value = DateTime.Now;
                cboType.DisplayMember = "ModelTypeName";
                cboType.ValueMember = "ID";
                cboType.DataSource = bModelType.GetModels();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 选择类别时事件
        /// </summary>
        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModelTypeBLL bModelType = new();
            cboModel.ValueMember = "ID";
            cboModel.DisplayMember = "ModelName";
            cboModel.DataSource = bModelType.GetNewModels(cboType.SelectedValue.ToInt32());
        }
        /// <summary>
        /// 查询方法
        /// </summary>
        private void LoadData()
        {
            try
            {
                string TestId = txtNumber.Text;
                int TypeID = cboType.SelectedValue.ToInt32();
                int Model = cboModel.SelectedValue.ToInt32();
                DateTime dateFrom = dtpStartBig.Value;
                DateTime dateTo = dtpStartEnd.Value;
                //TODO:模糊查询TestId字段及时间范围搜索
                var data = testRecord.GetTestRecord(new TestRecordModel
                {
                    KindID = TypeID,
                    ModelID = Model,
                    TestID = TestId,
                    TestTime = dateFrom,
                }, dateTo.AddDays(1));

                Tables.Columns =
                [
                  new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
                  new Column("KindID","类型ID"){ Align = ColumnAlign.Center , Visible = false },
                  new Column("ModelTypeName","类型名称"){ Align = ColumnAlign.Center , Width="auto"  },
                  new Column("ModelID","型号ID"){ Align = ColumnAlign.Center , Visible = false },
                  new Column("ModelName","型号名称"){ Align = ColumnAlign.Center , Width="auto"  },
                  new Column("TestID","车型车号"){ Align = ColumnAlign.Center , Width="auto" },
                  new Column("Tester","操作员"){ Align = ColumnAlign.Center , Width="auto" },
                  new Column("TestTime","保存时间"){ Align = ColumnAlign.Center , Width="auto" },
                  new Column("ReportPath","保存路径"){ Align = ColumnAlign.Center , Visible=false },
                ];
                Tables.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"加载数据出现错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 查看报表方法
        /// </summary>
        private void View()
        {
            try
            {
                if (RecordModel.ReportPath == null)
                {
                    MessageBox.Show("请先选择一条记录。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string filename = RecordModel.ReportPath.ToString();
                string filenameNew = RecordModel.ReportPath + ".xlsx".ToString();
                if (!File.Exists(filenameNew))
                {
                    MessageBox.Show("报表文件不存在或已经删除。", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                frmDispReport report = new(filename);
                report.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 搜索
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        /// <summary>
        /// 查看报表
        /// </summary>
        private void btnView_Click(object sender, EventArgs e)
        {
            View();
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (RecordModel.ReportPath == null)
            {
                MessageBox.Show("请先选择一条记录。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("删除后无法恢复，确定要删除该条记录吗？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                testRecord.DeleteTestRecord(RecordModel.ID);
                LoadData();
            }
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is TestRecordModelDto model)
            {
                RecordModel = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            View();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
