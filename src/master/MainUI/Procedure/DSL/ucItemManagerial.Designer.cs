using Padding = System.Windows.Forms.Padding;

namespace MainUI.Procedure.DSL
{
    partial class ucItemManagerial
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
            TableTestProcess = new AntdUI.Table();
            btnAdd = new UIButton();
            btnDeleteAll = new UIButton();
            SuspendLayout();
            // 
            // TableTestProcess
            // 
            TableTestProcess.AutoSizeColumnsMode = AntdUI.ColumnsMode.Fill;
            TableTestProcess.BackColor = Color.White;
            TableTestProcess.BackgroundImageLayout = ImageLayout.None;
            TableTestProcess.BorderColor = Color.Black;
            TableTestProcess.CheckSize = 20;
            TableTestProcess.ClipboardCopy = false;
            TableTestProcess.ColumnBack = Color.FromArgb(44, 62, 80);
            TableTestProcess.ColumnFont = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            TableTestProcess.ColumnFore = Color.White;
            TableTestProcess.DefaultExpand = true;
            TableTestProcess.Dock = DockStyle.Bottom;
            TableTestProcess.EditMode = AntdUI.TEditMode.DoubleClick;
            TableTestProcess.Font = new Font("微软雅黑", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            TableTestProcess.ImeMode = ImeMode.NoControl;
            TableTestProcess.Location = new Point(0, 50);
            TableTestProcess.Name = "TableTestProcess";
            TableTestProcess.RightToLeft = RightToLeft.No;
            TableTestProcess.RowHeight = 50;
            TableTestProcess.RowHeightHeader = 40;
            TableTestProcess.RowSelectedBg = Color.FromArgb(80, 160, 255);
            TableTestProcess.RowSelectedFore = Color.White;
            TableTestProcess.Size = new Size(665, 610);
            TableTestProcess.SwitchSize = 25;
            TableTestProcess.TabIndex = 399;
            TableTestProcess.CellClick += TableTestProcess_CellClick;
            TableTestProcess.CellButtonClick += TableTestProcess_CellButtonClick;
            // 
            // btnAdd
            // 
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FillColor = Color.FromArgb(90, 124, 236);
            btnAdd.FillColor2 = Color.FromArgb(90, 124, 236);
            btnAdd.FillDisableColor = Color.FromArgb(153, 153, 161);
            btnAdd.FillHoverColor = Color.FromArgb(90, 124, 236);
            btnAdd.FillPressColor = Color.FromArgb(90, 124, 236);
            btnAdd.FillSelectedColor = Color.FromArgb(90, 124, 236);
            btnAdd.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            btnAdd.ForeColor = Color.FromArgb(235, 227, 221);
            btnAdd.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnAdd.LightColor = Color.FromArgb(253, 243, 243);
            btnAdd.Location = new Point(3, 5);
            btnAdd.MinimumSize = new Size(1, 1);
            btnAdd.Name = "btnAdd";
            btnAdd.Radius = 7;
            btnAdd.RectColor = Color.FromArgb(90, 124, 236);
            btnAdd.RectDisableColor = Color.FromArgb(153, 153, 161);
            btnAdd.RectHoverColor = Color.FromArgb(90, 124, 236);
            btnAdd.RectPressColor = Color.FromArgb(90, 124, 236);
            btnAdd.RectSelectedColor = Color.FromArgb(90, 124, 236);
            btnAdd.Size = new Size(147, 37);
            btnAdd.Style = UIStyle.Custom;
            btnAdd.StyleCustomMode = true;
            btnAdd.TabIndex = 401;
            btnAdd.Text = "添加行";
            btnAdd.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDeleteAll
            // 
            btnDeleteAll.Cursor = Cursors.Hand;
            btnDeleteAll.FillColor = Color.FromArgb(230, 83, 100);
            btnDeleteAll.FillColor2 = Color.FromArgb(230, 83, 100);
            btnDeleteAll.FillDisableColor = Color.FromArgb(153, 153, 161);
            btnDeleteAll.FillHoverColor = Color.FromArgb(235, 115, 115);
            btnDeleteAll.FillPressColor = Color.FromArgb(184, 64, 64);
            btnDeleteAll.FillSelectedColor = Color.FromArgb(184, 64, 64);
            btnDeleteAll.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            btnDeleteAll.ForeColor = Color.FromArgb(235, 227, 221);
            btnDeleteAll.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnDeleteAll.LightColor = Color.FromArgb(253, 243, 243);
            btnDeleteAll.Location = new Point(158, 5);
            btnDeleteAll.MinimumSize = new Size(1, 1);
            btnDeleteAll.Name = "btnDeleteAll";
            btnDeleteAll.Radius = 7;
            btnDeleteAll.RectColor = Color.FromArgb(230, 83, 100);
            btnDeleteAll.RectDisableColor = Color.FromArgb(153, 153, 161);
            btnDeleteAll.RectHoverColor = Color.FromArgb(235, 115, 115);
            btnDeleteAll.RectPressColor = Color.FromArgb(184, 64, 64);
            btnDeleteAll.RectSelectedColor = Color.FromArgb(184, 64, 64);
            btnDeleteAll.Size = new Size(147, 37);
            btnDeleteAll.Style = UIStyle.Custom;
            btnDeleteAll.StyleCustomMode = true;
            btnDeleteAll.TabIndex = 402;
            btnDeleteAll.Text = "批量删除";
            btnDeleteAll.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDeleteAll.Click += btnDeleteAll_Click;
            // 
            // ucItemManagerial
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 236, 236);
            Controls.Add(btnDeleteAll);
            Controls.Add(btnAdd);
            Controls.Add(TableTestProcess);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Margin = new Padding(3, 6, 3, 6);
            Name = "ucItemManagerial";
            Size = new Size(665, 660);
            ResumeLayout(false);
        }

        #endregion
        private AntdUI.Table TableTestProcess;
        private UIButton btnAdd;
        private UIButton btnDeleteAll;
    }
}
