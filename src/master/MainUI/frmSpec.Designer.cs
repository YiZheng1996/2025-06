namespace MainUI
{
    partial class frmSpec
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            uiButton1 = new UIButton();
            uiButton2 = new UIButton();
            uiButton3 = new UIButton();
            uiDataGridView1 = new UIDataGridView();
            colID = new DataGridViewTextBoxColumn();
            TypeName = new DataGridViewTextBoxColumn();
            colUsername = new DataGridViewTextBoxColumn();
            colMark = new DataGridViewTextBoxColumn();
            cboType = new UIComboBox();
            cboModel = new UIComboBox();
            uiLabel2 = new UILabel();
            uiLabel1 = new UILabel();
            btnSearch = new UIButton();
            ((System.ComponentModel.ISupportInitialize)uiDataGridView1).BeginInit();
            SuspendLayout();
            // 
            // uiButton1
            // 
            uiButton1.Cursor = Cursors.Hand;
            uiButton1.FillColor = Color.DodgerBlue;
            uiButton1.FillColor2 = Color.DodgerBlue;
            uiButton1.Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            uiButton1.Location = new Point(18, 675);
            uiButton1.MinimumSize = new Size(1, 1);
            uiButton1.Name = "uiButton1";
            uiButton1.RectColor = Color.DodgerBlue;
            uiButton1.RectDisableColor = Color.DodgerBlue;
            uiButton1.Size = new Size(86, 38);
            uiButton1.TabIndex = 389;
            uiButton1.Text = "▲";
            uiButton1.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiButton1.TipsText = "1";
            uiButton1.Click += button1_Click;
            // 
            // uiButton2
            // 
            uiButton2.Cursor = Cursors.Hand;
            uiButton2.FillColor = Color.DodgerBlue;
            uiButton2.FillColor2 = Color.DodgerBlue;
            uiButton2.Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            uiButton2.Location = new Point(110, 675);
            uiButton2.MinimumSize = new Size(1, 1);
            uiButton2.Name = "uiButton2";
            uiButton2.RectColor = Color.DodgerBlue;
            uiButton2.RectDisableColor = Color.DodgerBlue;
            uiButton2.Size = new Size(86, 38);
            uiButton2.TabIndex = 390;
            uiButton2.Text = "▼";
            uiButton2.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiButton2.TipsText = "1";
            uiButton2.Click += button2_Click;
            // 
            // uiButton3
            // 
            uiButton3.Cursor = Cursors.Hand;
            uiButton3.DialogResult = DialogResult.OK;
            uiButton3.FillColor = Color.DodgerBlue;
            uiButton3.FillColor2 = Color.DodgerBlue;
            uiButton3.Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            uiButton3.Location = new Point(651, 673);
            uiButton3.MinimumSize = new Size(1, 1);
            uiButton3.Name = "uiButton3";
            uiButton3.RectColor = Color.DodgerBlue;
            uiButton3.RectDisableColor = Color.DodgerBlue;
            uiButton3.Size = new Size(120, 40);
            uiButton3.TabIndex = 391;
            uiButton3.Text = "选择实行";
            uiButton3.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiButton3.TipsForeColor = Color.Transparent;
            uiButton3.TipsText = "1";
            uiButton3.Click += button3_Click;
            // 
            // uiDataGridView1
            // 
            uiDataGridView1.AllowUserToAddRows = false;
            uiDataGridView1.AllowUserToDeleteRows = false;
            uiDataGridView1.AllowUserToResizeColumns = false;
            uiDataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = Color.WhiteSmoke;
            uiDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            uiDataGridView1.BackgroundColor = Color.White;
            uiDataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle7.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = Color.White;
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle7.SelectionForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            uiDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            uiDataGridView1.ColumnHeadersHeight = 32;
            uiDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            uiDataGridView1.Columns.AddRange(new DataGridViewColumn[] { colID, TypeName, colUsername, colMark });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(235, 227, 221);
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            uiDataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            uiDataGridView1.EnableHeadersVisualStyles = false;
            uiDataGridView1.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiDataGridView1.GridColor = Color.FromArgb(42, 47, 55);
            uiDataGridView1.Location = new Point(17, 96);
            uiDataGridView1.Name = "uiDataGridView1";
            uiDataGridView1.ReadOnly = true;
            uiDataGridView1.RectColor = Color.FromArgb(49, 54, 64);
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle9.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle9.SelectionForeColor = Color.White;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            uiDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.BackColor = Color.FromArgb(224, 247, 250);
            dataGridViewCellStyle10.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            dataGridViewCellStyle10.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle10.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle10.SelectionForeColor = Color.White;
            uiDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle10;
            uiDataGridView1.RowTemplate.Height = 37;
            uiDataGridView1.ScrollBarColor = Color.FromArgb(42, 47, 55);
            uiDataGridView1.ScrollBarStyleInherited = false;
            uiDataGridView1.SelectedIndex = -1;
            uiDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            uiDataGridView1.Size = new Size(753, 564);
            uiDataGridView1.StripeEvenColor = Color.FromArgb(224, 247, 250);
            uiDataGridView1.StripeOddColor = Color.WhiteSmoke;
            uiDataGridView1.TabIndex = 392;
            uiDataGridView1.CellDoubleClick += dataGridView_Spec_CellDoubleClick;
            // 
            // colID
            // 
            colID.DataPropertyName = "ID";
            colID.HeaderText = "ID";
            colID.Name = "colID";
            colID.ReadOnly = true;
            colID.Visible = false;
            // 
            // TypeName
            // 
            TypeName.DataPropertyName = "modeltype";
            TypeName.HeaderText = "产品类型";
            TypeName.Name = "TypeName";
            TypeName.ReadOnly = true;
            TypeName.Visible = false;
            TypeName.Width = 250;
            // 
            // colUsername
            // 
            colUsername.DataPropertyName = "Name";
            colUsername.HeaderText = "产品型号";
            colUsername.Name = "colUsername";
            colUsername.ReadOnly = true;
            colUsername.Width = 400;
            // 
            // colMark
            // 
            colMark.DataPropertyName = "mark";
            colMark.HeaderText = "备注";
            colMark.Name = "colMark";
            colMark.ReadOnly = true;
            colMark.Width = 310;
            // 
            // cboType
            // 
            cboType.BackColor = Color.Transparent;
            cboType.DataSource = null;
            cboType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboType.FillColor = Color.FromArgb(218, 220, 230);
            cboType.FillColor2 = Color.FromArgb(218, 220, 230);
            cboType.FillDisableColor = Color.FromArgb(218, 220, 230);
            cboType.FilterMaxCount = 50;
            cboType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            cboType.ForeColor = Color.FromArgb(46, 46, 46);
            cboType.ForeDisableColor = Color.FromArgb(46, 46, 46);
            cboType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboType.Location = new Point(319, 50);
            cboType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cboType.MinimumSize = new Size(63, 0);
            cboType.Name = "cboType";
            cboType.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cboType.Radius = 10;
            cboType.RectColor = Color.Gray;
            cboType.RectDisableColor = Color.Gray;
            cboType.RectSides = ToolStripStatusLabelBorderSides.Bottom;
            cboType.Size = new Size(244, 29);
            cboType.SymbolSize = 24;
            cboType.TabIndex = 393;
            cboType.TextAlignment = ContentAlignment.MiddleLeft;
            cboType.Watermark = "请选择";
            cboType.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            cboType.WatermarkColor = Color.FromArgb(46, 46, 46);
            cboType.SelectedIndexChanged += cboType_SelectedIndexChanged;
            // 
            // cboModel
            // 
            cboModel.BackColor = Color.Transparent;
            cboModel.DataSource = null;
            cboModel.DropDownStyle = UIDropDownStyle.DropDownList;
            cboModel.FillColor = Color.FromArgb(218, 220, 230);
            cboModel.FillColor2 = Color.FromArgb(218, 220, 230);
            cboModel.FillDisableColor = Color.FromArgb(218, 220, 230);
            cboModel.FilterMaxCount = 50;
            cboModel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            cboModel.ForeColor = Color.FromArgb(46, 46, 46);
            cboModel.ForeDisableColor = Color.FromArgb(235, 227, 221);
            cboModel.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboModel.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboModel.Location = new Point(301, 679);
            cboModel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cboModel.MinimumSize = new Size(63, 0);
            cboModel.Name = "cboModel";
            cboModel.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cboModel.Radius = 10;
            cboModel.RectColor = Color.FromArgb(218, 220, 230);
            cboModel.RectDisableColor = Color.FromArgb(218, 220, 230);
            cboModel.RectSides = ToolStripStatusLabelBorderSides.Bottom;
            cboModel.Size = new Size(172, 29);
            cboModel.SymbolSize = 24;
            cboModel.TabIndex = 395;
            cboModel.TextAlignment = ContentAlignment.MiddleLeft;
            cboModel.Visible = false;
            cboModel.Watermark = "请选择";
            cboModel.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            cboModel.WatermarkColor = Color.FromArgb(46, 46, 46);
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel2.Location = new Point(211, 681);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(102, 26);
            uiLabel2.TabIndex = 396;
            uiLabel2.Text = "产品型号：";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            uiLabel2.Visible = false;
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel1.Location = new Point(223, 52);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(102, 26);
            uiLabel1.TabIndex = 394;
            uiLabel1.Text = "产品类型：";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSearch
            // 
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnSearch.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnSearch.ForeDisableColor = Color.White;
            btnSearch.Location = new Point(480, 672);
            btnSearch.MinimumSize = new Size(1, 1);
            btnSearch.Name = "btnSearch";
            btnSearch.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSearch.Size = new Size(120, 40);
            btnSearch.TabIndex = 397;
            btnSearch.Text = "搜索";
            btnSearch.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSearch.TipsText = "1";
            btnSearch.Visible = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // frmSpec
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(786, 722);
            Controls.Add(btnSearch);
            Controls.Add(cboType);
            Controls.Add(cboModel);
            Controls.Add(uiLabel2);
            Controls.Add(uiLabel1);
            Controls.Add(uiDataGridView1);
            Controls.Add(uiButton3);
            Controls.Add(uiButton2);
            Controls.Add(uiButton1);
            Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            ForeColor = Color.FromArgb(235, 227, 221);
            Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSpec";
            RectColor = Color.FromArgb(49, 54, 64);
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "车型选择";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("思源黑体 CN Heavy", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 786, 678);
            Load += frmSpec_Load;
            ((System.ComponentModel.ISupportInitialize)uiDataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Sunny.UI.UIButton uiButton1;
        private Sunny.UI.UIButton uiButton2;
        private Sunny.UI.UIButton uiButton3;
        private Sunny.UI.UIDataGridView uiDataGridView1;
        private UIComboBox cboType;
        private UIComboBox cboModel;
        private UILabel uiLabel2;
        private UILabel uiLabel1;
        private UIButton btnSearch;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn TypeName;
        private DataGridViewTextBoxColumn colUsername;
        private DataGridViewTextBoxColumn colMark;
    }
}