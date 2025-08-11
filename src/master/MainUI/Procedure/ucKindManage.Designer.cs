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
            grpUser = new UIGroupBox();
            Tables = new AntdUI.Table();
            btnEdit = new UIButton();
            btnDelete = new UIButton();
            btnAdd = new UIButton();
            grpUser.SuspendLayout();
            SuspendLayout();
            // 
            // grpUser
            // 
            grpUser.BackColor = Color.FromArgb(236, 236, 237);
            grpUser.Controls.Add(Tables);
            grpUser.Controls.Add(btnEdit);
            grpUser.Controls.Add(btnDelete);
            grpUser.Controls.Add(btnAdd);
            grpUser.FillColor = Color.FromArgb(236, 236, 237);
            grpUser.FillColor2 = Color.FromArgb(236, 236, 237);
            grpUser.FillDisableColor = Color.FromArgb(236, 236, 237);
            grpUser.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            grpUser.ForeColor = Color.Black;
            grpUser.ForeDisableColor = Color.Black;
            grpUser.Location = new Point(0, -3);
            grpUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            grpUser.MinimumSize = new Size(1, 1);
            grpUser.Name = "grpUser";
            grpUser.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            grpUser.RectColor = Color.FromArgb(236, 236, 237);
            grpUser.RectDisableColor = Color.FromArgb(236, 236, 237);
            grpUser.Size = new Size(665, 660);
            grpUser.TabIndex = 399;
            grpUser.Text = "产品类型列表";
            grpUser.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // Tables
            // 
            Tables.AutoSizeColumnsMode = AntdUI.ColumnsMode.Fill;
            Tables.BackColor = Color.White;
            Tables.BackgroundImageLayout = ImageLayout.None;
            Tables.BorderColor = Color.Black;
            Tables.Bordered = true;
            Tables.CheckSize = 20;
            Tables.ClipboardCopy = false;
            Tables.ColumnBack = Color.FromArgb(44, 62, 80);
            Tables.ColumnFont = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            Tables.ColumnFore = Color.White;
            Tables.DefaultExpand = true;
            Tables.Dock = DockStyle.Top;
            Tables.Font = new Font("微软雅黑", 14.25F);
            Tables.ImeMode = ImeMode.NoControl;
            Tables.Location = new Point(0, 32);
            Tables.Name = "Tables";
            Tables.RightToLeft = RightToLeft.No;
            Tables.RowHeight = 50;
            Tables.RowHeightHeader = 40;
            Tables.Size = new Size(665, 570);
            Tables.SwitchSize = 25;
            Tables.TabIndex = 406;
            Tables.TreeButtonSize = 25;
            Tables.CellClick += Tables_CellClick;
            Tables.CellDoubleClick += Tables_CellDoubleClick;
            // 
            // btnEdit
            // 
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            btnEdit.ForeDisableColor = Color.White;
            btnEdit.Location = new Point(364, 615);
            btnEdit.MinimumSize = new Size(1, 1);
            btnEdit.Name = "btnEdit";
            btnEdit.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnEdit.Size = new Size(147, 37);
            btnEdit.TabIndex = 395;
            btnEdit.Text = "更改";
            btnEdit.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEdit.TipsText = "1";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            btnDelete.ForeDisableColor = Color.White;
            btnDelete.Location = new Point(183, 615);
            btnDelete.MinimumSize = new Size(1, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnDelete.Size = new Size(147, 37);
            btnDelete.TabIndex = 394;
            btnDelete.Text = "删除";
            btnDelete.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDelete.TipsText = "1";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAdd
            // 
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            btnAdd.ForeDisableColor = Color.White;
            btnAdd.Location = new Point(2, 615);
            btnAdd.MinimumSize = new Size(1, 1);
            btnAdd.Name = "btnAdd";
            btnAdd.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnAdd.Size = new Size(147, 37);
            btnAdd.TabIndex = 393;
            btnAdd.Text = "添加";
            btnAdd.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAdd.TipsText = "1";
            btnAdd.Click += btnAdd_Click;
            // 
            // ucKindManage
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 236, 237);
            Controls.Add(grpUser);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            Name = "ucKindManage";
            Size = new Size(665, 655);
            Load += ucModelManage_Load;
            grpUser.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private UIGroupBox grpUser;
        private AntdUI.Table Tables;
        private UIButton btnEdit;
        private UIButton btnDelete;
        private UIButton btnAdd;
    }
}
