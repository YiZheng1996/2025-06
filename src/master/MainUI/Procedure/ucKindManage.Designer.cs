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
            btnExpandAll = new UIButton();
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
            grpUser.Controls.Add(btnExpandAll);
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
            grpUser.Text = "产品类型与型号管理";
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
            Tables.DefaultExpand = false;
            Tables.Dock = DockStyle.Top;
            Tables.Font = new Font("微软雅黑", 14.25F);
            Tables.ImeMode = ImeMode.NoControl;
            Tables.Location = new Point(0, 32);
            Tables.Name = "Tables";
            Tables.RightToLeft = RightToLeft.No;
            Tables.RowHeight = 50;
            Tables.RowHeightHeader = 40;
            Tables.Size = new Size(665, 520);
            Tables.SwitchSize = 25;
            Tables.TabIndex = 406;
            Tables.TreeButtonSize = 25;
            Tables.CellClick += Tables_CellClick;
            Tables.CellDoubleClick += Tables_CellDoubleClick;
            //Tables.CellFormatting += Tables_CellFormatting;
            // 
            // btnRelease
            // 
            //btnRelease.Cursor = Cursors.Hand;
            //btnRelease.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            //btnRelease.ForeDisableColor = Color.White;
            //btnRelease.Location = new Point(2, 565);
            //btnRelease.MinimumSize = new Size(1, 1);
            //btnRelease.Name = "btnRelease";
            //btnRelease.RectDisableColor = Color.FromArgb(255, 165, 0);
            //btnRelease.RectColor = Color.FromArgb(255, 165, 0);
            //btnRelease.Size = new Size(100, 37);
            //btnRelease.TabIndex = 397;
            //btnRelease.Text = "发布型号";
            //btnRelease.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            //btnRelease.TipsText = "发布选中的产品型号";
            //btnRelease.Click += btnRelease_Click;
            // 
            // btnExpandAll
            // 
            btnExpandAll.Cursor = Cursors.Hand;
            btnExpandAll.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            btnExpandAll.ForeDisableColor = Color.White;
            btnExpandAll.Location = new Point(565, 565);
            btnExpandAll.MinimumSize = new Size(1, 1);
            btnExpandAll.Name = "btnExpandAll";
            btnExpandAll.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnExpandAll.Size = new Size(100, 37);
            btnExpandAll.TabIndex = 396;
            btnExpandAll.Text = "展开/折叠";
            btnExpandAll.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnExpandAll.TipsText = "展开或折叠所有节点";
            btnExpandAll.Click += btnExpandAll_Click;
            // 
            // btnEdit
            // 
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            btnEdit.ForeDisableColor = Color.White;
            btnEdit.Location = new Point(345, 565);
            btnEdit.MinimumSize = new Size(1, 1);
            btnEdit.Name = "btnEdit";
            btnEdit.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnEdit.Size = new Size(100, 37);
            btnEdit.TabIndex = 395;
            btnEdit.Text = "编辑";
            btnEdit.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEdit.TipsText = "编辑选中的记录";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            btnDelete.ForeDisableColor = Color.White;
            btnDelete.Location = new Point(235, 565);
            btnDelete.MinimumSize = new Size(1, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.RectDisableColor = Color.FromArgb(220, 53, 69);
            btnDelete.RectColor = Color.FromArgb(220, 53, 69);
            btnDelete.Size = new Size(100, 37);
            btnDelete.TabIndex = 394;
            btnDelete.Text = "删除";
            btnDelete.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDelete.TipsText = "删除选中的记录";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAdd
            // 
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            btnAdd.ForeDisableColor = Color.White;
            btnAdd.Location = new Point(125, 565);
            btnAdd.MinimumSize = new Size(1, 1);
            btnAdd.Name = "btnAdd";
            btnAdd.RectDisableColor = Color.FromArgb(40, 167, 69);
            btnAdd.RectColor = Color.FromArgb(40, 167, 69);
            btnAdd.Size = new Size(100, 37);
            btnAdd.TabIndex = 393;
            btnAdd.Text = "添加";
            btnAdd.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAdd.TipsText = "添加新记录";
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
        private UIButton btnExpandAll;
        private UIButton btnEdit;
        private UIButton btnDelete;
        private UIButton btnAdd;
    }
}