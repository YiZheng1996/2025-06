namespace MainUI.Procedure
{
    partial class ucTestParams
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
            AntdUI.Tabs.StyleCard2 styleCard21 = new AntdUI.Tabs.StyleCard2();
            openFileDialog1 = new OpenFileDialog();
            uiGroupBox1 = new UIGroupBox();
            txtType = new UITextBox();
            uiLabel9 = new UILabel();
            btnGet = new UIButton();
            uiLabel2 = new UILabel();
            txtModel = new UITextBox();
            btnBrowse = new UIButton();
            btnDelete = new UIButton();
            txtRpt = new UITextBox();
            tabs1 = new AntdUI.Tabs();
            tabPage1 = new AntdUI.TabPage();
            uiLabel10 = new UILabel();
            txtPressureRelief = new UITextBox();
            uiLabel11 = new UILabel();
            uiLabel7 = new UILabel();
            txtApplyPressure = new UITextBox();
            uiLabel8 = new UILabel();
            uiLabel5 = new UILabel();
            txtSprayKpa = new UITextBox();
            uiLabel6 = new UILabel();
            uiLabel4 = new UILabel();
            txtSprayTime = new UITextBox();
            uiLabel1 = new UILabel();
            tabPage2 = new AntdUI.TabPage();
            uiLabel3 = new UILabel();
            btnParameter = new AntdUI.Button();
            btnReport = new AntdUI.Button();
            uiGroupBox1.SuspendLayout();
            tabs1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.BackColor = Color.Transparent;
            uiGroupBox1.Controls.Add(txtType);
            uiGroupBox1.Controls.Add(uiLabel9);
            uiGroupBox1.Controls.Add(btnGet);
            uiGroupBox1.Controls.Add(uiLabel2);
            uiGroupBox1.Controls.Add(txtModel);
            uiGroupBox1.FillColor = Color.White;
            uiGroupBox1.FillColor2 = Color.White;
            uiGroupBox1.FillDisableColor = Color.FromArgb(42, 47, 55);
            uiGroupBox1.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            uiGroupBox1.ForeColor = Color.FromArgb(46, 46, 46);
            uiGroupBox1.ForeDisableColor = Color.FromArgb(235, 227, 221);
            uiGroupBox1.Location = new Point(0, 0);
            uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            uiGroupBox1.Radius = 15;
            uiGroupBox1.RectColor = Color.White;
            uiGroupBox1.RectDisableColor = Color.White;
            uiGroupBox1.Size = new Size(665, 93);
            uiGroupBox1.TabIndex = 400;
            uiGroupBox1.Text = "参数设置";
            uiGroupBox1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // txtType
            // 
            txtType.Enabled = false;
            txtType.FillColor = Color.FromArgb(218, 220, 230);
            txtType.FillColor2 = Color.FromArgb(218, 220, 230);
            txtType.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtType.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtType.ForeColor = Color.FromArgb(46, 46, 46);
            txtType.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtType.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtType.Location = new Point(97, 44);
            txtType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtType.MinimumSize = new Size(1, 16);
            txtType.Name = "txtType";
            txtType.Padding = new System.Windows.Forms.Padding(5);
            txtType.Radius = 10;
            txtType.ReadOnly = true;
            txtType.RectColor = Color.Gray;
            txtType.RectDisableColor = Color.Gray;
            txtType.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtType.ShowText = false;
            txtType.Size = new Size(147, 29);
            txtType.TabIndex = 397;
            txtType.TextAlignment = ContentAlignment.MiddleLeft;
            txtType.Watermark = "请选择";
            // 
            // uiLabel9
            // 
            uiLabel9.AutoSize = true;
            uiLabel9.BackColor = Color.Transparent;
            uiLabel9.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            uiLabel9.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel9.Location = new Point(14, 46);
            uiLabel9.Name = "uiLabel9";
            uiLabel9.Size = new Size(90, 23);
            uiLabel9.TabIndex = 396;
            uiLabel9.Text = "产品类型：";
            uiLabel9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnGet
            // 
            btnGet.Cursor = Cursors.Hand;
            btnGet.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnGet.Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            btnGet.ForeDisableColor = Color.White;
            btnGet.Location = new Point(542, 38);
            btnGet.MinimumSize = new Size(1, 1);
            btnGet.Name = "btnGet";
            btnGet.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnGet.Size = new Size(105, 40);
            btnGet.TabIndex = 389;
            btnGet.Text = "产品选择";
            btnGet.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnGet.TipsText = "1";
            btnGet.Click += btnProductSelection_Click;
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel2.Location = new Point(286, 46);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(79, 23);
            uiLabel2.TabIndex = 82;
            uiLabel2.Text = "产品型号:";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtModel
            // 
            txtModel.Enabled = false;
            txtModel.FillColor = Color.FromArgb(218, 220, 230);
            txtModel.FillColor2 = Color.FromArgb(218, 220, 230);
            txtModel.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtModel.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtModel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtModel.ForeColor = Color.FromArgb(46, 46, 46);
            txtModel.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtModel.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtModel.Location = new Point(371, 44);
            txtModel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtModel.MinimumSize = new Size(1, 16);
            txtModel.Name = "txtModel";
            txtModel.Padding = new System.Windows.Forms.Padding(5);
            txtModel.ReadOnly = true;
            txtModel.RectColor = Color.FromArgb(218, 220, 230);
            txtModel.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtModel.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtModel.ShowText = false;
            txtModel.Size = new Size(147, 29);
            txtModel.TabIndex = 390;
            txtModel.TextAlignment = ContentAlignment.MiddleLeft;
            txtModel.Watermark = "请选择";
            // 
            // btnBrowse
            // 
            btnBrowse.Cursor = Cursors.Hand;
            btnBrowse.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnBrowse.Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            btnBrowse.ForeDisableColor = Color.White;
            btnBrowse.Location = new Point(480, 126);
            btnBrowse.MinimumSize = new Size(1, 1);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnBrowse.Size = new Size(82, 29);
            btnBrowse.TabIndex = 394;
            btnBrowse.Text = "浏览";
            btnBrowse.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnBrowse.TipsText = "1";
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Transparent;
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnDelete.Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            btnDelete.ForeDisableColor = Color.White;
            btnDelete.Location = new Point(467, 614);
            btnDelete.MinimumSize = new Size(1, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnDelete.Size = new Size(183, 40);
            btnDelete.TabIndex = 396;
            btnDelete.Text = "保存";
            btnDelete.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDelete.TipsText = "1";
            btnDelete.Click += btnOK_Click;
            // 
            // txtRpt
            // 
            txtRpt.Enabled = false;
            txtRpt.FillColor = Color.FromArgb(218, 220, 230);
            txtRpt.FillColor2 = Color.FromArgb(218, 220, 230);
            txtRpt.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtRpt.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtRpt.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            txtRpt.ForeColor = Color.FromArgb(46, 46, 46);
            txtRpt.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtRpt.ForeReadOnlyColor = Color.FromArgb(235, 227, 221);
            txtRpt.Location = new Point(165, 126);
            txtRpt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtRpt.MinimumSize = new Size(1, 16);
            txtRpt.Name = "txtRpt";
            txtRpt.Padding = new System.Windows.Forms.Padding(5);
            txtRpt.ReadOnly = true;
            txtRpt.RectColor = Color.FromArgb(218, 220, 230);
            txtRpt.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtRpt.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtRpt.ShowText = false;
            txtRpt.Size = new Size(298, 29);
            txtRpt.TabIndex = 393;
            txtRpt.TextAlignment = ContentAlignment.MiddleLeft;
            txtRpt.Watermark = "请选择";
            // 
            // tabs1
            // 
            tabs1.BackColor = Color.White;
            tabs1.Controls.Add(tabPage1);
            tabs1.Controls.Add(tabPage2);
            tabs1.Location = new Point(0, 136);
            tabs1.Name = "tabs1";
            tabs1.Pages.Add(tabPage1);
            tabs1.Pages.Add(tabPage2);
            tabs1.Size = new Size(665, 470);
            styleCard21.Closable = AntdUI.Tabs.StyleCard2.CloseType.none;
            tabs1.Style = styleCard21;
            tabs1.TabIndex = 401;
            tabs1.TabMenuVisible = false;
            tabs1.Text = "tabs1";
            tabs1.Type = AntdUI.TabType.Card2;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.White;
            tabPage1.Controls.Add(uiLabel10);
            tabPage1.Controls.Add(txtPressureRelief);
            tabPage1.Controls.Add(uiLabel11);
            tabPage1.Controls.Add(uiLabel7);
            tabPage1.Controls.Add(txtApplyPressure);
            tabPage1.Controls.Add(uiLabel8);
            tabPage1.Controls.Add(uiLabel5);
            tabPage1.Controls.Add(txtSprayKpa);
            tabPage1.Controls.Add(uiLabel6);
            tabPage1.Controls.Add(uiLabel4);
            tabPage1.Controls.Add(txtSprayTime);
            tabPage1.Controls.Add(uiLabel1);
            tabPage1.Location = new Point(3, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(659, 464);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "试验参数";
            // 
            // uiLabel10
            // 
            uiLabel10.AutoSize = true;
            uiLabel10.BackColor = Color.Transparent;
            uiLabel10.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel10.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel10.Location = new Point(413, 326);
            uiLabel10.Name = "uiLabel10";
            uiLabel10.Size = new Size(35, 26);
            uiLabel10.TabIndex = 409;
            uiLabel10.Text = "pa";
            uiLabel10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPressureRelief
            // 
            txtPressureRelief.FillColor = Color.FromArgb(218, 220, 230);
            txtPressureRelief.FillColor2 = Color.FromArgb(218, 220, 230);
            txtPressureRelief.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtPressureRelief.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPressureRelief.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            txtPressureRelief.ForeColor = Color.FromArgb(46, 46, 46);
            txtPressureRelief.ForeDisableColor = Color.FromArgb(46, 46, 46);
            txtPressureRelief.ForeReadOnlyColor = Color.FromArgb(46, 46, 46);
            txtPressureRelief.Location = new Point(269, 324);
            txtPressureRelief.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtPressureRelief.MinimumSize = new Size(1, 16);
            txtPressureRelief.Name = "txtPressureRelief";
            txtPressureRelief.Padding = new System.Windows.Forms.Padding(5);
            txtPressureRelief.RectColor = Color.FromArgb(218, 220, 230);
            txtPressureRelief.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtPressureRelief.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPressureRelief.ShowText = false;
            txtPressureRelief.Size = new Size(126, 29);
            txtPressureRelief.TabIndex = 407;
            txtPressureRelief.Text = "0";
            txtPressureRelief.TextAlignment = ContentAlignment.MiddleCenter;
            txtPressureRelief.Type = UITextBox.UIEditType.Integer;
            txtPressureRelief.Watermark = "请输入";
            // 
            // uiLabel11
            // 
            uiLabel11.AutoSize = true;
            uiLabel11.BackColor = Color.Transparent;
            uiLabel11.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel11.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel11.Location = new Point(142, 324);
            uiLabel11.Name = "uiLabel11";
            uiLabel11.Size = new Size(120, 26);
            uiLabel11.TabIndex = 408;
            uiLabel11.Text = "泄压压力值：";
            uiLabel11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel7
            // 
            uiLabel7.AutoSize = true;
            uiLabel7.BackColor = Color.Transparent;
            uiLabel7.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel7.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel7.Location = new Point(413, 255);
            uiLabel7.Name = "uiLabel7";
            uiLabel7.Size = new Size(35, 26);
            uiLabel7.TabIndex = 406;
            uiLabel7.Text = "pa";
            uiLabel7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtApplyPressure
            // 
            txtApplyPressure.FillColor = Color.FromArgb(218, 220, 230);
            txtApplyPressure.FillColor2 = Color.FromArgb(218, 220, 230);
            txtApplyPressure.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtApplyPressure.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtApplyPressure.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            txtApplyPressure.ForeColor = Color.FromArgb(46, 46, 46);
            txtApplyPressure.ForeDisableColor = Color.FromArgb(46, 46, 46);
            txtApplyPressure.ForeReadOnlyColor = Color.FromArgb(46, 46, 46);
            txtApplyPressure.Location = new Point(269, 253);
            txtApplyPressure.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtApplyPressure.MinimumSize = new Size(1, 16);
            txtApplyPressure.Name = "txtApplyPressure";
            txtApplyPressure.Padding = new System.Windows.Forms.Padding(5);
            txtApplyPressure.RectColor = Color.FromArgb(218, 220, 230);
            txtApplyPressure.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtApplyPressure.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtApplyPressure.ShowText = false;
            txtApplyPressure.Size = new Size(126, 29);
            txtApplyPressure.TabIndex = 404;
            txtApplyPressure.Text = "0";
            txtApplyPressure.TextAlignment = ContentAlignment.MiddleCenter;
            txtApplyPressure.Type = UITextBox.UIEditType.Integer;
            txtApplyPressure.Watermark = "请输入";
            // 
            // uiLabel8
            // 
            uiLabel8.AutoSize = true;
            uiLabel8.BackColor = Color.Transparent;
            uiLabel8.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel8.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel8.Location = new Point(142, 253);
            uiLabel8.Name = "uiLabel8";
            uiLabel8.Size = new Size(120, 26);
            uiLabel8.TabIndex = 405;
            uiLabel8.Text = "加压压力值：";
            uiLabel8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel5
            // 
            uiLabel5.AutoSize = true;
            uiLabel5.BackColor = Color.Transparent;
            uiLabel5.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel5.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel5.Location = new Point(413, 184);
            uiLabel5.Name = "uiLabel5";
            uiLabel5.Size = new Size(35, 26);
            uiLabel5.TabIndex = 403;
            uiLabel5.Text = "pa";
            uiLabel5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSprayKpa
            // 
            txtSprayKpa.FillColor = Color.FromArgb(218, 220, 230);
            txtSprayKpa.FillColor2 = Color.FromArgb(218, 220, 230);
            txtSprayKpa.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtSprayKpa.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtSprayKpa.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            txtSprayKpa.ForeColor = Color.FromArgb(46, 46, 46);
            txtSprayKpa.ForeDisableColor = Color.FromArgb(46, 46, 46);
            txtSprayKpa.ForeReadOnlyColor = Color.FromArgb(46, 46, 46);
            txtSprayKpa.Location = new Point(269, 182);
            txtSprayKpa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtSprayKpa.MinimumSize = new Size(1, 16);
            txtSprayKpa.Name = "txtSprayKpa";
            txtSprayKpa.Padding = new System.Windows.Forms.Padding(5);
            txtSprayKpa.RectColor = Color.FromArgb(218, 220, 230);
            txtSprayKpa.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtSprayKpa.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtSprayKpa.ShowText = false;
            txtSprayKpa.Size = new Size(126, 29);
            txtSprayKpa.TabIndex = 401;
            txtSprayKpa.Text = "0";
            txtSprayKpa.TextAlignment = ContentAlignment.MiddleCenter;
            txtSprayKpa.Type = UITextBox.UIEditType.Integer;
            txtSprayKpa.Watermark = "请输入";
            // 
            // uiLabel6
            // 
            uiLabel6.AutoSize = true;
            uiLabel6.BackColor = Color.Transparent;
            uiLabel6.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel6.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel6.Location = new Point(160, 182);
            uiLabel6.Name = "uiLabel6";
            uiLabel6.Size = new Size(102, 26);
            uiLabel6.TabIndex = 402;
            uiLabel6.Text = "喷淋压力：";
            uiLabel6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel4
            // 
            uiLabel4.AutoSize = true;
            uiLabel4.BackColor = Color.Transparent;
            uiLabel4.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel4.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel4.Location = new Point(413, 113);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(44, 26);
            uiLabel4.TabIndex = 400;
            uiLabel4.Text = "Min";
            uiLabel4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSprayTime
            // 
            txtSprayTime.FillColor = Color.FromArgb(218, 220, 230);
            txtSprayTime.FillColor2 = Color.FromArgb(218, 220, 230);
            txtSprayTime.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtSprayTime.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtSprayTime.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            txtSprayTime.ForeColor = Color.FromArgb(46, 46, 46);
            txtSprayTime.ForeDisableColor = Color.FromArgb(46, 46, 46);
            txtSprayTime.ForeReadOnlyColor = Color.FromArgb(46, 46, 46);
            txtSprayTime.Location = new Point(269, 111);
            txtSprayTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtSprayTime.MinimumSize = new Size(1, 16);
            txtSprayTime.Name = "txtSprayTime";
            txtSprayTime.Padding = new System.Windows.Forms.Padding(5);
            txtSprayTime.RectColor = Color.FromArgb(218, 220, 230);
            txtSprayTime.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtSprayTime.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtSprayTime.ShowText = false;
            txtSprayTime.Size = new Size(126, 29);
            txtSprayTime.TabIndex = 398;
            txtSprayTime.Text = "0";
            txtSprayTime.TextAlignment = ContentAlignment.MiddleCenter;
            txtSprayTime.Type = UITextBox.UIEditType.Integer;
            txtSprayTime.Watermark = "请输入";
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel1.Location = new Point(160, 111);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(102, 26);
            uiLabel1.TabIndex = 399;
            uiLabel1.Text = "喷淋时间：";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.White;
            tabPage2.Controls.Add(uiLabel3);
            tabPage2.Controls.Add(btnBrowse);
            tabPage2.Controls.Add(txtRpt);
            tabPage2.Location = new Point(-659, -464);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(659, 464);
            tabPage2.TabIndex = 0;
            tabPage2.Text = "报表模板";
            // 
            // uiLabel3
            // 
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel3.Location = new Point(83, 126);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(75, 23);
            uiLabel3.TabIndex = 397;
            uiLabel3.Text = "产品类型";
            uiLabel3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnParameter
            // 
            btnParameter.BackActive = Color.FromArgb(80, 160, 255);
            btnParameter.BackColor = Color.FromArgb(80, 160, 255);
            btnParameter.BorderWidth = 1F;
            btnParameter.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnParameter.ForeColor = Color.White;
            btnParameter.Location = new Point(1, 101);
            btnParameter.Name = "btnParameter";
            btnParameter.Size = new Size(131, 48);
            btnParameter.TabIndex = 404;
            btnParameter.Text = "参数设置";
            btnParameter.Type = AntdUI.TTypeMini.Primary;
            btnParameter.WaveSize = 1;
            btnParameter.Click += btnParameter_Click;
            // 
            // btnReport
            // 
            btnReport.BackActive = Color.FromArgb(80, 160, 255);
            btnReport.BackColor = Color.FromArgb(80, 160, 255);
            btnReport.BorderWidth = 1F;
            btnReport.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnReport.ForeColor = Color.White;
            btnReport.Location = new Point(138, 101);
            btnReport.Name = "btnReport";
            btnReport.Size = new Size(131, 48);
            btnReport.TabIndex = 405;
            btnReport.Text = "报表模板";
            btnReport.Type = AntdUI.TTypeMini.Primary;
            btnReport.WaveSize = 1;
            btnReport.Click += btnReport_Click;
            // 
            // ucTestParams
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            Controls.Add(tabs1);
            Controls.Add(btnDelete);
            Controls.Add(uiGroupBox1);
            Controls.Add(btnParameter);
            Controls.Add(btnReport);
            Name = "ucTestParams";
            Size = new Size(665, 660);
            uiGroupBox1.ResumeLayout(false);
            uiGroupBox1.PerformLayout();
            tabs1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIButton btnGet;
        private Sunny.UI.UITextBox txtModel;
        private Sunny.UI.UIButton btnDelete;
        private Sunny.UI.UIButton btnBrowse;
        private Sunny.UI.UITextBox txtRpt;
        private AntdUI.Tabs tabs1;
        private AntdUI.TabPage tabPage1;
        private AntdUI.TabPage tabPage2;
        private UILabel uiLabel3;
        private AntdUI.Button btnParameter;
        private AntdUI.Button btnReport;
        private UILabel uiLabel7;
        private UILabel uiLabel8;
        private UILabel uiLabel5;
        private UITextBox txtSprayKpa;
        private UILabel uiLabel6;
        private UILabel uiLabel4;
        private UITextBox txtSprayTime;
        private UILabel uiLabel1;
        private UILabel uiLabel9;
        private UITextBox txtType;
        private UILabel uiLabel10;
        private UILabel uiLabel11;
        private UITextBox txtPressureRelief;
        private UITextBox txtApplyPressure;
    }
}
