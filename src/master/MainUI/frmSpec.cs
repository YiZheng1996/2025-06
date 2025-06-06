using MainUI.Model;

namespace MainUI
{
    public partial class frmSpec : UIForm
    {
        public frmSpec()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSpec_Load(object sender, EventArgs e)
        {
            uiDataGridView1.AutoGenerateColumns = false;
            BindModels();
        }
        ModelBLL pbll = new();
        /// <summary>
        /// 获取被试品类别列表
        /// </summary>
        private void BindModels()
        {
            try
            {
                ModelBLL bModelType = new();
                cboType.DisplayMember = "ModelType";
                cboType.ValueMember = "ID";
                cboType.DataSource = bModelType.GetModels();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"数据加载错误：{ex.Message}");
            }
        }

        private void LoadData()
        {
            uiDataGridView1.DataSource = pbll.GetNewModels(cboType.SelectedValue.ToInt32());
        }


        /// <summary>
        /// 上翻
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (uiDataGridView1.Rows.Count > 0)
            {
                if (this.uiDataGridView1.CurrentRow.Index > 0)
                {
                    int i = uiDataGridView1.Rows.GetPreviousRow(uiDataGridView1.CurrentRow.Index, DataGridViewElementStates.None);//获取原选定上一行索引
                    uiDataGridView1.Rows[i].Selected = true; //选中整行
                    uiDataGridView1.CurrentCell = uiDataGridView1[1, i];//指针上移
                }
            }
        }
        /// <summary>
        /// 下翻
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (uiDataGridView1.Rows.Count > 0)
            {
                if (this.uiDataGridView1.CurrentRow.Index < this.uiDataGridView1.Rows.Count - 1)
                {
                    int i = uiDataGridView1.Rows.GetNextRow(uiDataGridView1.CurrentRow.Index, DataGridViewElementStates.None);//获取原选定下一行索引
                    uiDataGridView1.Rows[i].Selected = true; //选中整行
                    uiDataGridView1.CurrentCell = uiDataGridView1[1, i];//指针下移
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetModel();
        }

        public void GetModel()
        {
            try
            {
                VarHelper.mTestViewModel.ModelID = Convert.ToInt32(uiDataGridView1.Rows[uiDataGridView1.CurrentRow.Index].Cells["colID"].Value);//得到当前选择的型号ID
                VarHelper.mTestViewModel.TypeID = cboType.SelectedValue.ToInt32();
                VarHelper.mTestViewModel.TypeName = uiDataGridView1.Rows[uiDataGridView1.CurrentRow.Index].Cells["TypeName"].Value.ToString();//型号类别名称
                VarHelper.mTestViewModel.ModelName = uiDataGridView1.Rows[uiDataGridView1.CurrentRow.Index].Cells["colUsername"].Value.ToString();//型号名称
                VarHelper.mTestViewModel.Mark = uiDataGridView1.Rows[uiDataGridView1.CurrentRow.Index].Cells["colMark"].Value.ToString();//得到当前选择的备注
                Close();
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"选择型号数据错误：{ex.Message}");
            }
        }

        private void dataGridView_Spec_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                GetModel();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error(ex.Message);
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModelBLL bModelType = new();
            cboModel.ValueMember = "ID";
            cboModel.DisplayMember = "Name";
            cboModel.DataSource = bModelType.GetNewModels(cboType.SelectedValue.ToInt32());
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}
