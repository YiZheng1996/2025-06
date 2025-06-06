namespace MainUI.Procedure
{
    partial class ucKindManage
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            groupBox1 = new UIGroupBox();
            dataGridView1 = new UIDataGridView();
            name = new DataGridViewTextBoxColumn();
            ModelType = new DataGridViewTextBoxColumn();
            mark = new DataGridViewTextBoxColumn();
            txtChexing = new UITextBox();
            uiLabel4 = new UILabel();
            txtModelName = new UITextBox();
            uiLabel1 = new UILabel();
            btnEdit = new UIButton();
            btnDelete = new UIButton();
            btnAdd = new UIButton();
            uiPanel1 = new UIPanel();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.FillColor = Color.White;
            groupBox1.FillColor2 = Color.White;
            groupBox1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(46, 46, 46);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            groupBox1.MinimumSize = new Size(1, 1);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(0, 45, 0, 0);
            groupBox1.RectColor = Color.White;
            groupBox1.RectDisableColor = Color.White;
            groupBox1.Size = new Size(454, 606);
            groupBox1.TabIndex = 399;
            groupBox1.Text = "被试品类型";
            groupBox1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.WhiteSmoke;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeight = 32;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { name, ModelType, mark });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("微软雅黑", 12F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(220, 236, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Dock = DockStyle.Bottom;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Font = new Font("微软雅黑", 12F);
            dataGridView1.GridColor = Color.FromArgb(42, 47, 55);
            dataGridView1.Location = new Point(0, 30);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RectColor = Color.White;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(243, 249, 255);
            dataGridViewCellStyle4.Font = new Font("微软雅黑", 12F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.FromArgb(46, 46, 46);
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle5.SelectionForeColor = Color.White;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.SelectedIndex = -1;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(454, 576);
            dataGridView1.StripeOddColor = Color.WhiteSmoke;
            dataGridView1.TabIndex = 407;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // name
            // 
            name.DataPropertyName = "ID";
            name.HeaderText = "ID";
            name.Name = "name";
            name.ReadOnly = true;
            name.Visible = false;
            name.Width = 170;
            // 
            // ModelType
            // 
            ModelType.DataPropertyName = "ModelType";
            ModelType.HeaderText = "类型";
            ModelType.Name = "ModelType";
            ModelType.ReadOnly = true;
            ModelType.Width = 210;
            // 
            // mark
            // 
            mark.DataPropertyName = "mark";
            mark.HeaderText = "备注";
            mark.Name = "mark";
            mark.ReadOnly = true;
            mark.Width = 200;
            // 
            // txtChexing
            // 
            txtChexing.Cursor = Cursors.IBeam;
            txtChexing.FillColor = Color.FromArgb(218, 220, 230);
            txtChexing.FillColor2 = Color.FromArgb(218, 220, 230);
            txtChexing.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            txtChexing.ForeColor = Color.FromArgb(46, 46, 46);
            txtChexing.Location = new Point(9, 116);
            txtChexing.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtChexing.MinimumSize = new Size(1, 16);
            txtChexing.Name = "txtChexing";
            txtChexing.Padding = new System.Windows.Forms.Padding(5);
            txtChexing.RectColor = Color.FromArgb(218, 220, 230);
            txtChexing.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtChexing.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtChexing.ShowText = false;
            txtChexing.Size = new Size(177, 29);
            txtChexing.TabIndex = 406;
            txtChexing.TextAlignment = ContentAlignment.MiddleLeft;
            txtChexing.Watermark = "请输入";
            txtChexing.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            txtChexing.WatermarkColor = Color.FromArgb(46, 46, 46);
            // 
            // uiLabel4
            // 
            uiLabel4.BackColor = Color.Transparent;
            uiLabel4.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            uiLabel4.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel4.Location = new Point(9, 89);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(95, 23);
            uiLabel4.TabIndex = 405;
            uiLabel4.Text = "备注";
            uiLabel4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtModelName
            // 
            txtModelName.Cursor = Cursors.IBeam;
            txtModelName.FillColor = Color.FromArgb(218, 220, 230);
            txtModelName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtModelName.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            txtModelName.ForeColor = Color.FromArgb(46, 46, 46);
            txtModelName.Location = new Point(9, 39);
            txtModelName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtModelName.MinimumSize = new Size(1, 16);
            txtModelName.Name = "txtModelName";
            txtModelName.Padding = new System.Windows.Forms.Padding(5);
            txtModelName.RectColor = Color.FromArgb(218, 220, 230);
            txtModelName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtModelName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtModelName.ShowText = false;
            txtModelName.Size = new Size(177, 29);
            txtModelName.TabIndex = 404;
            txtModelName.TextAlignment = ContentAlignment.MiddleLeft;
            txtModelName.Watermark = "请输入";
            txtModelName.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            txtModelName.WatermarkColor = Color.FromArgb(46, 46, 46);
            // 
            // uiLabel1
            // 
            uiLabel1.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel1.Location = new Point(8, 11);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(95, 23);
            uiLabel1.TabIndex = 400;
            uiLabel1.Text = "产品类型";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnEdit
            // 
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            btnEdit.ForeDisableColor = Color.White;
            btnEdit.Location = new Point(361, 616);
            btnEdit.MinimumSize = new Size(1, 1);
            btnEdit.Name = "btnEdit";
            btnEdit.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnEdit.Size = new Size(147, 37);
            btnEdit.TabIndex = 395;
            btnEdit.Text = "更 改";
            btnEdit.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEdit.TipsText = "1";
            btnEdit.Click += btnChange_Click;
            // 
            // btnDelete
            // 
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            btnDelete.ForeDisableColor = Color.White;
            btnDelete.Location = new Point(188, 616);
            btnDelete.MinimumSize = new Size(1, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnDelete.Size = new Size(147, 37);
            btnDelete.TabIndex = 394;
            btnDelete.Text = "删 除";
            btnDelete.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDelete.TipsText = "1";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAdd
            // 
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            btnAdd.ForeDisableColor = Color.White;
            btnAdd.Location = new Point(15, 616);
            btnAdd.MinimumSize = new Size(1, 1);
            btnAdd.Name = "btnAdd";
            btnAdd.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnAdd.Size = new Size(147, 37);
            btnAdd.TabIndex = 393;
            btnAdd.Text = "新 增";
            btnAdd.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAdd.TipsText = "1";
            btnAdd.Click += btnAdd_Click;
            // 
            // uiPanel1
            // 
            uiPanel1.BackColor = Color.Transparent;
            uiPanel1.Controls.Add(txtChexing);
            uiPanel1.Controls.Add(uiLabel4);
            uiPanel1.Controls.Add(txtModelName);
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.FillDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiPanel1.ForeColor = Color.FromArgb(49, 54, 64);
            uiPanel1.ForeDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Location = new Point(461, 0);
            uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 15;
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.Size = new Size(200, 606);
            uiPanel1.TabIndex = 408;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // ucKindManage
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            Controls.Add(uiPanel1);
            Controls.Add(groupBox1);
            Controls.Add(btnAdd);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            Name = "ucKindManage";
            Size = new Size(665, 660);
            Load += ucModelManage_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            uiPanel1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIGroupBox groupBox1;
        private Sunny.UI.UIButton btnEdit;
        private Sunny.UI.UIButton btnDelete;
        private Sunny.UI.UIButton btnAdd;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox txtModelName;
        private Sunny.UI.UITextBox txtChexing;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIDataGridView dataGridView1;
        private UIPanel uiPanel1;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn ModelType;
        private DataGridViewTextBoxColumn mark;
    }
}
