using Padding = System.Windows.Forms.Padding;

namespace MainUI
{
    partial class frmAboutDevice
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
            uiPanel2 = new UIPanel();
            uiLabel1 = new UILabel();
            uiLabel4 = new UILabel();
            uiLabel5 = new UILabel();
            dtpStart = new UIDatePicker();
            dtpEnd = new UIDatePicker();
            Tables = new AntdUI.Table();
            uiPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // uiPanel2
            // 
            uiPanel2.Controls.Add(uiLabel1);
            uiPanel2.Controls.Add(uiLabel4);
            uiPanel2.Controls.Add(uiLabel5);
            uiPanel2.Controls.Add(dtpStart);
            uiPanel2.Controls.Add(dtpEnd);
            uiPanel2.FillColor = Color.White;
            uiPanel2.FillColor2 = Color.White;
            uiPanel2.FillDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel2.Font = new Font("宋体", 12F);
            uiPanel2.ForeColor = Color.FromArgb(49, 54, 64);
            uiPanel2.ForeDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel2.Location = new Point(20, 48);
            uiPanel2.Margin = new Padding(4, 5, 4, 5);
            uiPanel2.MinimumSize = new Size(1, 1);
            uiPanel2.Name = "uiPanel2";
            uiPanel2.Radius = 15;
            uiPanel2.RectColor = Color.White;
            uiPanel2.RectDisableColor = Color.White;
            uiPanel2.Size = new Size(995, 62);
            uiPanel2.TabIndex = 409;
            uiPanel2.Text = null;
            uiPanel2.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLabel1
            // 
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.Black;
            uiLabel1.Location = new Point(35, 20);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(322, 23);
            uiLabel1.TabIndex = 396;
            uiLabel1.Text = "软件使用时长：36天06小时37分";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel4
            // 
            uiLabel4.BackColor = Color.Transparent;
            uiLabel4.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel4.ForeColor = Color.Black;
            uiLabel4.Location = new Point(456, 18);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(91, 23);
            uiLabel4.TabIndex = 395;
            uiLabel4.Text = "记录时间:";
            uiLabel4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel5
            // 
            uiLabel5.BackColor = Color.Transparent;
            uiLabel5.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel5.ForeColor = Color.Black;
            uiLabel5.Location = new Point(739, 18);
            uiLabel5.Name = "uiLabel5";
            uiLabel5.Size = new Size(25, 23);
            uiLabel5.TabIndex = 394;
            uiLabel5.Text = "至";
            uiLabel5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpStart
            // 
            dtpStart.BackColor = Color.Transparent;
            dtpStart.FillColor = Color.FromArgb(218, 220, 230);
            dtpStart.FillColor2 = Color.FromArgb(218, 220, 230);
            dtpStart.FillDisableColor = Color.FromArgb(218, 220, 230);
            dtpStart.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            dtpStart.ForeColor = Color.Black;
            dtpStart.ForeDisableColor = Color.Black;
            dtpStart.Location = new Point(551, 17);
            dtpStart.Margin = new Padding(4, 5, 4, 5);
            dtpStart.MaxLength = 10;
            dtpStart.MinimumSize = new Size(63, 0);
            dtpStart.Name = "dtpStart";
            dtpStart.Padding = new Padding(0, 0, 30, 2);
            dtpStart.Radius = 10;
            dtpStart.RectColor = Color.Silver;
            dtpStart.RectDisableColor = Color.Silver;
            dtpStart.RectSides = ToolStripStatusLabelBorderSides.None;
            dtpStart.Size = new Size(170, 29);
            dtpStart.SymbolDropDown = 61555;
            dtpStart.SymbolNormal = 61555;
            dtpStart.SymbolSize = 24;
            dtpStart.TabIndex = 393;
            dtpStart.Text = "2025-02-01";
            dtpStart.TextAlignment = ContentAlignment.MiddleCenter;
            dtpStart.Value = new DateTime(2025, 2, 1, 0, 0, 0, 0);
            dtpStart.Watermark = "";
            dtpStart.ValueChanged += DtpStart_ValueChanged;
            // 
            // dtpEnd
            // 
            dtpEnd.BackColor = Color.Transparent;
            dtpEnd.FillColor = Color.FromArgb(218, 220, 230);
            dtpEnd.FillColor2 = Color.FromArgb(218, 220, 230);
            dtpEnd.FillDisableColor = Color.FromArgb(43, 46, 57);
            dtpEnd.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            dtpEnd.ForeColor = Color.Black;
            dtpEnd.ForeDisableColor = Color.Black;
            dtpEnd.Location = new Point(781, 17);
            dtpEnd.Margin = new Padding(4, 5, 4, 5);
            dtpEnd.MaxLength = 10;
            dtpEnd.MinimumSize = new Size(63, 0);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Padding = new Padding(0, 0, 30, 2);
            dtpEnd.Radius = 10;
            dtpEnd.RectColor = Color.Silver;
            dtpEnd.RectDisableColor = Color.Silver;
            dtpEnd.RectSides = ToolStripStatusLabelBorderSides.None;
            dtpEnd.Size = new Size(170, 29);
            dtpEnd.SymbolDropDown = 61555;
            dtpEnd.SymbolNormal = 61555;
            dtpEnd.SymbolSize = 24;
            dtpEnd.TabIndex = 392;
            dtpEnd.Text = "2025-05-01";
            dtpEnd.TextAlignment = ContentAlignment.MiddleCenter;
            dtpEnd.Value = new DateTime(2025, 5, 1, 0, 0, 0, 0);
            dtpEnd.Watermark = "";
            dtpEnd.ValueChanged += DtpStart_ValueChanged;
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
            Tables.Font = new Font("微软雅黑", 14.25F);
            Tables.ImeMode = ImeMode.NoControl;
            Tables.Location = new Point(20, 118);
            Tables.Name = "Tables";
            Tables.RightToLeft = RightToLeft.No;
            Tables.RowHeight = 50;
            Tables.RowHeightHeader = 40;
            Tables.Size = new Size(995, 556);
            Tables.SwitchSize = 25;
            Tables.TabIndex = 410;
            // 
            // frmAboutDevice
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(1035, 691);
            Controls.Add(Tables);
            Controls.Add(uiPanel2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAboutDevice";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "关于设备";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
            TitleForeColor = Color.FromArgb(236, 236, 236);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            Load += frmAboutDevice_Load;
            uiPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private UIPanel uiPanel2;
        private UIDatePicker dtpEnd;
        private UIDatePicker dtpStart;
        private UILabel uiLabel5;
        private UILabel uiLabel4;
        private UILabel uiLabel1;
        private AntdUI.Table Tables;
    }
}