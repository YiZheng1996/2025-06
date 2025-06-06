namespace MainUI
{
    partial class frmDataManager
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            panel2 = new Panel();
            btnExit = new UIButton();
            btnRemove = new UIButton();
            btnView = new UIButton();
            cboType = new UIComboBox();
            grpDI = new UIGroupBox();
            dtpStartBig = new UIDatePicker();
            cboModel = new UIComboBox();
            uiLabel5 = new UILabel();
            dtpStartEnd = new UIDatePicker();
            uiLabel4 = new UILabel();
            txtNumber = new UITextBox();
            btnSearch = new UIButton();
            uiLabel3 = new UILabel();
            uiLabel2 = new UILabel();
            uiLabel1 = new UILabel();
            uiDataGridView1 = new UIDataGridView();
            colid = new DataGridViewTextBoxColumn();
            colmodeltype = new DataGridViewTextBoxColumn();
            colModel = new DataGridViewTextBoxColumn();
            colTestID = new DataGridViewTextBoxColumn();
            colTester = new DataGridViewTextBoxColumn();
            colTestTime = new DataGridViewTextBoxColumn();
            colReportPath = new DataGridViewTextBoxColumn();
            panel2.SuspendLayout();
            grpDI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)uiDataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(btnExit);
            panel2.Controls.Add(btnRemove);
            panel2.Controls.Add(btnView);
            panel2.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panel2.Location = new Point(0, 650);
            panel2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            panel2.Name = "panel2";
            panel2.Size = new Size(1015, 62);
            panel2.TabIndex = 3;
            // 
            // btnExit
            // 
            btnExit.Cursor = Cursors.Hand;
            btnExit.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnExit.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnExit.Location = new Point(868, 12);
            btnExit.MinimumSize = new Size(1, 1);
            btnExit.Name = "btnExit";
            btnExit.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnExit.Size = new Size(120, 40);
            btnExit.TabIndex = 390;
            btnExit.Text = "退出";
            btnExit.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnExit.TipsText = "1";
            btnExit.Click += btnExit_Click;
            // 
            // btnRemove
            // 
            btnRemove.Cursor = Cursors.Hand;
            btnRemove.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnRemove.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnRemove.Location = new Point(430, 12);
            btnRemove.MinimumSize = new Size(1, 1);
            btnRemove.Name = "btnRemove";
            btnRemove.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnRemove.Size = new Size(120, 40);
            btnRemove.TabIndex = 389;
            btnRemove.Text = "删除";
            btnRemove.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnRemove.TipsText = "1";
            btnRemove.Click += btnRemove_Click;
            // 
            // btnView
            // 
            btnView.Cursor = Cursors.Hand;
            btnView.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnView.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnView.Location = new Point(29, 12);
            btnView.MinimumSize = new Size(1, 1);
            btnView.Name = "btnView";
            btnView.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnView.Size = new Size(120, 40);
            btnView.TabIndex = 388;
            btnView.Text = "查看报表";
            btnView.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnView.TipsText = "1";
            btnView.Click += btnView_Click;
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
            cboType.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            cboType.ForeColor = Color.FromArgb(46, 46, 46);
            cboType.ForeDisableColor = Color.FromArgb(46, 46, 46);
            cboType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboType.Location = new Point(107, 28);
            cboType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cboType.MinimumSize = new Size(63, 0);
            cboType.Name = "cboType";
            cboType.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cboType.Radius = 10;
            cboType.RectColor = Color.Gray;
            cboType.RectDisableColor = Color.Gray;
            cboType.RectSides = ToolStripStatusLabelBorderSides.Bottom;
            cboType.Size = new Size(165, 29);
            cboType.SymbolSize = 24;
            cboType.TabIndex = 71;
            cboType.TextAlignment = ContentAlignment.MiddleLeft;
            cboType.Watermark = "请选择";
            cboType.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            cboType.WatermarkColor = Color.FromArgb(46, 46, 46);
            cboType.SelectedIndexChanged += cboType_SelectedIndexChanged;
            // 
            // grpDI
            // 
            grpDI.BackColor = Color.Transparent;
            grpDI.Controls.Add(cboType);
            grpDI.Controls.Add(dtpStartBig);
            grpDI.Controls.Add(cboModel);
            grpDI.Controls.Add(uiLabel5);
            grpDI.Controls.Add(dtpStartEnd);
            grpDI.Controls.Add(uiLabel4);
            grpDI.Controls.Add(txtNumber);
            grpDI.Controls.Add(btnSearch);
            grpDI.Controls.Add(uiLabel3);
            grpDI.Controls.Add(uiLabel2);
            grpDI.Controls.Add(uiLabel1);
            grpDI.FillColor = Color.White;
            grpDI.FillColor2 = Color.White;
            grpDI.FillDisableColor = Color.White;
            grpDI.Font = new Font("微软雅黑", 11F);
            grpDI.ForeColor = Color.White;
            grpDI.ForeDisableColor = Color.White;
            grpDI.Location = new Point(16, 60);
            grpDI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            grpDI.MinimumSize = new Size(1, 1);
            grpDI.Name = "grpDI";
            grpDI.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            grpDI.Radius = 10;
            grpDI.RectColor = Color.White;
            grpDI.RectDisableColor = Color.White;
            grpDI.Size = new Size(985, 127);
            grpDI.TabIndex = 390;
            grpDI.Text = null;
            grpDI.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // dtpStartBig
            // 
            dtpStartBig.BackColor = Color.Transparent;
            dtpStartBig.FillColor = Color.FromArgb(218, 220, 230);
            dtpStartBig.FillColor2 = Color.FromArgb(218, 220, 230);
            dtpStartBig.FillDisableColor = Color.FromArgb(43, 46, 57);
            dtpStartBig.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            dtpStartBig.ForeColor = Color.FromArgb(46, 46, 46);
            dtpStartBig.ForeDisableColor = Color.FromArgb(235, 227, 221);
            dtpStartBig.Location = new Point(107, 76);
            dtpStartBig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            dtpStartBig.MaxLength = 10;
            dtpStartBig.MinimumSize = new Size(63, 0);
            dtpStartBig.Name = "dtpStartBig";
            dtpStartBig.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            dtpStartBig.Radius = 10;
            dtpStartBig.RectColor = Color.Gray;
            dtpStartBig.RectDisableColor = Color.Gray;
            dtpStartBig.Size = new Size(165, 29);
            dtpStartBig.SymbolDropDown = 61555;
            dtpStartBig.SymbolNormal = 61555;
            dtpStartBig.SymbolSize = 24;
            dtpStartBig.TabIndex = 390;
            dtpStartBig.Text = "2023-02-01";
            dtpStartBig.TextAlignment = ContentAlignment.MiddleLeft;
            dtpStartBig.Value = new DateTime(2023, 2, 1, 16, 20, 20, 721);
            dtpStartBig.Watermark = "";
            dtpStartBig.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            dtpStartBig.WatermarkColor = Color.FromArgb(46, 46, 46);
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
            cboModel.Location = new Point(369, 28);
            cboModel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cboModel.MinimumSize = new Size(63, 0);
            cboModel.Name = "cboModel";
            cboModel.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cboModel.Radius = 10;
            cboModel.RectColor = Color.Gray;
            cboModel.RectDisableColor = Color.Gray;
            cboModel.RectSides = ToolStripStatusLabelBorderSides.Bottom;
            cboModel.Size = new Size(165, 29);
            cboModel.SymbolSize = 24;
            cboModel.TabIndex = 73;
            cboModel.TextAlignment = ContentAlignment.MiddleLeft;
            cboModel.Watermark = "请选择";
            cboModel.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            cboModel.WatermarkColor = Color.FromArgb(46, 46, 46);
            // 
            // uiLabel5
            // 
            uiLabel5.BackColor = Color.Transparent;
            uiLabel5.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel5.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel5.Location = new Point(304, 79);
            uiLabel5.Name = "uiLabel5";
            uiLabel5.Size = new Size(25, 23);
            uiLabel5.TabIndex = 392;
            uiLabel5.Text = "至";
            uiLabel5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpStartEnd
            // 
            dtpStartEnd.BackColor = Color.Transparent;
            dtpStartEnd.FillColor = Color.FromArgb(218, 220, 230);
            dtpStartEnd.FillColor2 = Color.FromArgb(218, 220, 230);
            dtpStartEnd.FillDisableColor = Color.FromArgb(43, 46, 57);
            dtpStartEnd.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            dtpStartEnd.ForeColor = Color.FromArgb(46, 46, 46);
            dtpStartEnd.ForeDisableColor = Color.FromArgb(235, 227, 221);
            dtpStartEnd.Location = new Point(369, 76);
            dtpStartEnd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            dtpStartEnd.MaxLength = 10;
            dtpStartEnd.MinimumSize = new Size(63, 0);
            dtpStartEnd.Name = "dtpStartEnd";
            dtpStartEnd.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            dtpStartEnd.Radius = 10;
            dtpStartEnd.RectColor = Color.Gray;
            dtpStartEnd.RectDisableColor = Color.Gray;
            dtpStartEnd.Size = new Size(165, 29);
            dtpStartEnd.SymbolDropDown = 61555;
            dtpStartEnd.SymbolNormal = 61555;
            dtpStartEnd.SymbolSize = 24;
            dtpStartEnd.TabIndex = 391;
            dtpStartEnd.Text = "2023-02-01";
            dtpStartEnd.TextAlignment = ContentAlignment.MiddleLeft;
            dtpStartEnd.Value = new DateTime(2023, 2, 1, 16, 20, 20, 721);
            dtpStartEnd.Watermark = "";
            dtpStartEnd.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            dtpStartEnd.WatermarkColor = Color.FromArgb(46, 46, 46);
            // 
            // uiLabel4
            // 
            uiLabel4.BackColor = Color.Transparent;
            uiLabel4.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel4.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel4.Location = new Point(25, 79);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(84, 23);
            uiLabel4.TabIndex = 389;
            uiLabel4.Text = "测试时间";
            uiLabel4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtNumber
            // 
            txtNumber.BackColor = Color.Transparent;
            txtNumber.Cursor = Cursors.IBeam;
            txtNumber.FillColor = Color.FromArgb(218, 220, 230);
            txtNumber.FillColor2 = Color.FromArgb(218, 220, 230);
            txtNumber.FillDisableColor = Color.FromArgb(43, 46, 57);
            txtNumber.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtNumber.ForeColor = Color.FromArgb(46, 46, 46);
            txtNumber.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtNumber.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtNumber.Location = new Point(637, 28);
            txtNumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtNumber.MinimumSize = new Size(1, 16);
            txtNumber.Name = "txtNumber";
            txtNumber.Padding = new System.Windows.Forms.Padding(5);
            txtNumber.Radius = 10;
            txtNumber.RectColor = Color.FromArgb(218, 220, 230);
            txtNumber.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtNumber.ShowText = false;
            txtNumber.Size = new Size(169, 29);
            txtNumber.TabIndex = 388;
            txtNumber.TextAlignment = ContentAlignment.MiddleLeft;
            txtNumber.Watermark = "请输入";
            txtNumber.WatermarkActiveColor = Color.FromArgb(46, 46, 46);
            txtNumber.WatermarkColor = Color.FromArgb(46, 46, 46);
            // 
            // btnSearch
            // 
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnSearch.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnSearch.ForeDisableColor = Color.White;
            btnSearch.Location = new Point(840, 35);
            btnSearch.MinimumSize = new Size(1, 1);
            btnSearch.Name = "btnSearch";
            btnSearch.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSearch.Size = new Size(120, 40);
            btnSearch.TabIndex = 387;
            btnSearch.Text = "搜索";
            btnSearch.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSearch.TipsText = "1";
            btnSearch.Click += btnSearch_Click;
            // 
            // uiLabel3
            // 
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel3.Location = new Point(549, 31);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(93, 23);
            uiLabel3.TabIndex = 75;
            uiLabel3.Text = "车型车号";
            uiLabel3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel2.Location = new Point(279, 31);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(92, 23);
            uiLabel2.TabIndex = 74;
            uiLabel2.Text = "产品型号";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel1
            // 
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel1.Location = new Point(25, 31);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(84, 23);
            uiLabel1.TabIndex = 72;
            uiLabel1.Text = "产品类型";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiDataGridView1
            // 
            uiDataGridView1.AllowUserToAddRows = false;
            uiDataGridView1.AllowUserToDeleteRows = false;
            uiDataGridView1.AllowUserToResizeColumns = false;
            uiDataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.WhiteSmoke;
            uiDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            uiDataGridView1.BackgroundColor = Color.White;
            uiDataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            uiDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            uiDataGridView1.ColumnHeadersHeight = 32;
            uiDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            uiDataGridView1.Columns.AddRange(new DataGridViewColumn[] { colid, colmodeltype, colModel, colTestID, colTester, colTestTime, colReportPath });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            uiDataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            uiDataGridView1.EnableHeadersVisualStyles = false;
            uiDataGridView1.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiDataGridView1.GridColor = Color.FromArgb(42, 47, 55);
            uiDataGridView1.Location = new Point(17, 183);
            uiDataGridView1.Name = "uiDataGridView1";
            uiDataGridView1.ReadOnly = true;
            uiDataGridView1.RectColor = Color.FromArgb(42, 47, 55);
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle4.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            uiDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle5.SelectionForeColor = Color.White;
            uiDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            uiDataGridView1.RowTemplate.Height = 33;
            uiDataGridView1.ScrollBarRectColor = Color.FromArgb(42, 47, 55);
            uiDataGridView1.ScrollBarStyleInherited = false;
            uiDataGridView1.SelectedIndex = -1;
            uiDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            uiDataGridView1.Size = new Size(985, 464);
            uiDataGridView1.StripeOddColor = Color.WhiteSmoke;
            uiDataGridView1.TabIndex = 391;
            uiDataGridView1.CellMouseDoubleClick += dataGridView1_CellMouseDoubleClick;
            // 
            // colid
            // 
            colid.DataPropertyName = "id";
            colid.HeaderText = "ID";
            colid.Name = "colid";
            colid.ReadOnly = true;
            colid.Visible = false;
            // 
            // colmodeltype
            // 
            colmodeltype.DataPropertyName = "Kind";
            colmodeltype.HeaderText = "产品类型";
            colmodeltype.Name = "colmodeltype";
            colmodeltype.ReadOnly = true;
            colmodeltype.Width = 160;
            // 
            // colModel
            // 
            colModel.DataPropertyName = "Model";
            colModel.HeaderText = "产品型号";
            colModel.Name = "colModel";
            colModel.ReadOnly = true;
            colModel.Width = 160;
            // 
            // colTestID
            // 
            colTestID.DataPropertyName = "TestID";
            colTestID.HeaderText = "车型车号";
            colTestID.Name = "colTestID";
            colTestID.ReadOnly = true;
            colTestID.Width = 200;
            // 
            // colTester
            // 
            colTester.DataPropertyName = "Tester";
            colTester.HeaderText = "测试人员";
            colTester.Name = "colTester";
            colTester.ReadOnly = true;
            colTester.Width = 150;
            // 
            // colTestTime
            // 
            colTestTime.DataPropertyName = "TestTime";
            colTestTime.HeaderText = "测试时间";
            colTestTime.Name = "colTestTime";
            colTestTime.ReadOnly = true;
            colTestTime.Width = 270;
            // 
            // colReportPath
            // 
            colReportPath.DataPropertyName = "ReportPath";
            colReportPath.HeaderText = "报表路径";
            colReportPath.Name = "colReportPath";
            colReportPath.ReadOnly = true;
            colReportPath.Visible = false;
            colReportPath.Width = 192;
            // 
            // frmDataManager
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(1015, 718);
            Controls.Add(uiDataGridView1);
            Controls.Add(grpDI);
            Controls.Add(panel2);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            ForeColor = Color.White;
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmDataManager";
            RectColor = Color.FromArgb(47, 55, 64);
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "试验报表管理";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("思源黑体 CN Heavy", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 1015, 646);
            Load += frmDataManager_Load;
            panel2.ResumeLayout(false);
            grpDI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)uiDataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private Sunny.UI.UIComboBox cboType;
        private Sunny.UI.UIGroupBox grpDI;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIComboBox cboModel;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIButton btnSearch;
        private Sunny.UI.UITextBox txtNumber;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIDatePicker dtpStartBig;
        private Sunny.UI.UIDatePicker dtpStartEnd;
        private Sunny.UI.UIButton btnExit;
        private Sunny.UI.UIButton btnRemove;
        private Sunny.UI.UIButton btnView;
        private Sunny.UI.UIDataGridView uiDataGridView1;
        private Sunny.UI.UILabel uiLabel5;
        private DataGridViewTextBoxColumn colid;
        private DataGridViewTextBoxColumn colmodeltype;
        private DataGridViewTextBoxColumn colModel;
        private DataGridViewTextBoxColumn colTestID;
        private DataGridViewTextBoxColumn colTester;
        private DataGridViewTextBoxColumn colTestTime;
        private DataGridViewTextBoxColumn colReportPath;
    }
}