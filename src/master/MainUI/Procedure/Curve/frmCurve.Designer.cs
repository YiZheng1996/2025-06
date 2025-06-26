namespace MainUI.Procedure.Curve
{
    partial class frmCurve
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
            LineCurve = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            uiLabel4 = new UILabel();
            uiComBoxModel = new UIComboBox();
            uiLabel3 = new UILabel();
            uiLabel2 = new UILabel();
            uiLabel1 = new UILabel();
            uiUpDownRate = new UIIntegerUpDown();
            uiTimeStart = new UIDatetimePicker();
            btnStop = new UIButton();
            btnStart = new UIButton();
            uiPanel1 = new UIPanel();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // LineCurve
            // 
            LineCurve.BackColor = Color.White;
            LineCurve.Location = new Point(15, 48);
            LineCurve.Name = "LineCurve";
            LineCurve.Size = new Size(1390, 674);
            LineCurve.TabIndex = 0;
            // 
            // uiLabel4
            // 
            uiLabel4.AutoSize = true;
            uiLabel4.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            uiLabel4.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel4.Location = new Point(389, 27);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(94, 28);
            uiLabel4.TabIndex = 1185;
            uiLabel4.Text = "时间选择:";
            uiLabel4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // uiComBoxModel
            // 
            uiComBoxModel.DataSource = null;
            uiComBoxModel.DropDownStyle = UIDropDownStyle.DropDownList;
            uiComBoxModel.FillColor = Color.FromArgb(218, 220, 230);
            uiComBoxModel.FillColor2 = Color.FromArgb(218, 220, 230);
            uiComBoxModel.FillDisableColor = Color.FromArgb(218, 220, 230);
            uiComBoxModel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiComBoxModel.ForeColor = Color.FromArgb(46, 46, 46);
            uiComBoxModel.ItemHoverColor = Color.FromArgb(155, 200, 255);
            uiComBoxModel.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            uiComBoxModel.Location = new Point(154, 24);
            uiComBoxModel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiComBoxModel.MinimumSize = new Size(63, 0);
            uiComBoxModel.Name = "uiComBoxModel";
            uiComBoxModel.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            uiComBoxModel.RectColor = Color.Gray;
            uiComBoxModel.RectDisableColor = Color.Gray;
            uiComBoxModel.Size = new Size(159, 33);
            uiComBoxModel.Style = UIStyle.Custom;
            uiComBoxModel.SymbolSize = 24;
            uiComBoxModel.TabIndex = 1184;
            uiComBoxModel.TextAlignment = ContentAlignment.MiddleLeft;
            uiComBoxModel.Watermark = "";
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel3.Location = new Point(36, 27);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(113, 28);
            uiLabel3.TabIndex = 1183;
            uiLabel3.Text = "被试品型号:";
            uiLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel2.Location = new Point(1004, 27);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(39, 28);
            uiLabel2.TabIndex = 1182;
            uiLabel2.Text = "ms";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(46, 46, 46);
            uiLabel1.Location = new Point(761, 27);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(94, 28);
            uiLabel1.TabIndex = 1181;
            uiLabel1.Text = "刷新速率:";
            uiLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // uiUpDownRate
            // 
            //uiUpDownRate.ButtonFillColor = Color.FromArgb(65, 100, 204);
            //uiUpDownRate.ButtonRectColor = Color.FromArgb(65, 100, 204);
            uiUpDownRate.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiUpDownRate.ForeColor = Color.FromArgb(46, 46, 46);
            uiUpDownRate.Location = new Point(865, 24);
            uiUpDownRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiUpDownRate.MinimumSize = new Size(100, 0);
            uiUpDownRate.Name = "uiUpDownRate";
            uiUpDownRate.RectColor = Color.FromArgb(65, 100, 204);
            uiUpDownRate.ShowText = false;
            uiUpDownRate.Size = new Size(133, 33);
            uiUpDownRate.Style = UIStyle.Custom;
            uiUpDownRate.TabIndex = 1180;
            uiUpDownRate.Text = null;
            uiUpDownRate.TextAlignment = ContentAlignment.MiddleCenter;
            uiUpDownRate.Value = 200;
            // 
            // uiTimeStart
            // 
            uiTimeStart.DateFormat = "yyyy-MM-dd";
            uiTimeStart.FillColor = Color.FromArgb(218, 220, 230);
            uiTimeStart.FillColor2 = Color.FromArgb(218, 220, 230);
            uiTimeStart.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiTimeStart.ForeColor = Color.FromArgb(46, 46, 46);
            uiTimeStart.Location = new Point(490, 24);
            uiTimeStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiTimeStart.MaxLength = 10;
            uiTimeStart.MinimumSize = new Size(63, 0);
            uiTimeStart.Name = "uiTimeStart";
            uiTimeStart.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            uiTimeStart.RectColor = Color.Gray;
            uiTimeStart.RectDisableColor = Color.Gray;
            uiTimeStart.ShowToday = true;
            uiTimeStart.Size = new Size(159, 33);
            uiTimeStart.Style = UIStyle.Custom;
            uiTimeStart.SymbolDropDown = 61555;
            uiTimeStart.SymbolNormal = 61555;
            uiTimeStart.SymbolSize = 25;
            uiTimeStart.TabIndex = 1179;
            uiTimeStart.Text = "2024-06-05";
            uiTimeStart.TextAlignment = ContentAlignment.MiddleCenter;
            uiTimeStart.Value = new DateTime(2024, 6, 5, 0, 0, 0, 0);
            uiTimeStart.Watermark = "";
            // 
            // btnStop
            // 
            btnStop.Cursor = Cursors.Hand;
            btnStop.FillColor = Color.FromArgb(230, 83, 100);
            btnStop.FillColor2 = Color.FromArgb(230, 83, 100);
            btnStop.FillDisableColor = Color.FromArgb(153, 153, 161);
            btnStop.FillHoverColor = Color.FromArgb(235, 115, 115);
            btnStop.FillPressColor = Color.FromArgb(184, 64, 64);
            btnStop.FillSelectedColor = Color.FromArgb(184, 64, 64);
            btnStop.Font = new Font("思源黑体 CN Bold", 16F, FontStyle.Bold);
            btnStop.ForeDisableColor = Color.White;
            btnStop.LightColor = Color.FromArgb(253, 243, 243);
            btnStop.Location = new Point(1242, 18);
            btnStop.MinimumSize = new Size(1, 1);
            btnStop.Name = "btnStop";
            btnStop.Radius = 7;
            btnStop.RectColor = Color.FromArgb(230, 83, 100);
            btnStop.RectDisableColor = Color.FromArgb(153, 153, 161);
            btnStop.RectHoverColor = Color.FromArgb(235, 115, 115);
            btnStop.RectPressColor = Color.FromArgb(184, 64, 64);
            btnStop.RectSelectedColor = Color.FromArgb(184, 64, 64);
            btnStop.Size = new Size(136, 48);
            btnStop.Style = UIStyle.Custom;
            btnStop.StyleCustomMode = true;
            btnStop.TabIndex = 1187;
            btnStop.Text = "结 束";
            btnStop.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnStop.Click += btnStop_Click;
            // 
            // btnStart
            // 
            btnStart.Cursor = Cursors.Hand;
            btnStart.FillColor = Color.FromArgb(90, 124, 236);
            btnStart.FillColor2 = Color.FromArgb(90, 124, 236);
            btnStart.FillDisableColor = Color.FromArgb(153, 153, 161);
            btnStart.FillHoverColor = Color.FromArgb(90, 124, 236);
            btnStart.FillPressColor = Color.FromArgb(90, 124, 236);
            btnStart.FillSelectedColor = Color.FromArgb(90, 124, 236);
            btnStart.Font = new Font("思源黑体 CN Bold", 16F, FontStyle.Bold);
            btnStart.ForeDisableColor = Color.White;
            btnStart.LightColor = Color.FromArgb(245, 251, 241);
            btnStart.Location = new Point(1077, 17);
            btnStart.MinimumSize = new Size(1, 1);
            btnStart.Name = "btnStart";
            btnStart.Radius = 7;
            btnStart.RectColor = Color.FromArgb(90, 124, 236);
            btnStart.RectDisableColor = Color.FromArgb(153, 153, 161);
            btnStart.RectHoverColor = Color.FromArgb(90, 124, 236);
            btnStart.RectPressColor = Color.FromArgb(90, 124, 236);
            btnStart.RectSelectedColor = Color.FromArgb(90, 124, 236);
            btnStart.Size = new Size(136, 48);
            btnStart.Style = UIStyle.Custom;
            btnStart.StyleCustomMode = true;
            btnStart.TabIndex = 1186;
            btnStart.Text = "开 始";
            btnStart.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnStart.Click += btnStart_Click;
            // 
            // uiPanel1
            // 
            uiPanel1.BackColor = Color.Transparent;
            uiPanel1.Controls.Add(uiLabel3);
            uiPanel1.Controls.Add(btnStop);
            uiPanel1.Controls.Add(uiTimeStart);
            uiPanel1.Controls.Add(btnStart);
            uiPanel1.Controls.Add(uiUpDownRate);
            uiPanel1.Controls.Add(uiLabel4);
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Controls.Add(uiComBoxModel);
            uiPanel1.Controls.Add(uiLabel2);
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.FillDisableColor = Color.White;
            uiPanel1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiPanel1.Location = new Point(15, 732);
            uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 10;
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.Size = new Size(1390, 82);
            uiPanel1.TabIndex = 1188;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // frmCurve
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(1421, 824);
            Controls.Add(uiPanel1);
            Controls.Add(LineCurve);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmCurve";
            RectColor = Color.FromArgb(46, 46, 46);
            Text = "历史曲线";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("思源黑体 CN Bold", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            uiPanel1.ResumeLayout(false);
            uiPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart LineCurve;
        private UILabel uiLabel4;
        private UIComboBox uiComBoxModel;
        private UILabel uiLabel3;
        private UILabel uiLabel2;
        private UILabel uiLabel1;
        private UIIntegerUpDown uiUpDownRate;
        private UIDatetimePicker uiTimeStart;
        private UIButton btnStop;
        private UIButton btnStart;
        private UIPanel uiPanel1;
    }
}