using Padding = System.Windows.Forms.Padding;

namespace MainUI.Procedure.DSL
{
    partial class FrmSetParameter
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            uiPanel1 = new UIPanel();
            StopBits2 = new UIRadioButton();
            StopBits1 = new UIRadioButton();
            uiLabel5 = new UILabel();
            uiLabel4 = new UILabel();
            cboParity = new UIComboBox();
            uiLabel3 = new UILabel();
            cboDataBits = new UIComboBox();
            uiLabel1 = new UILabel();
            cboBaudRate = new UIComboBox();
            uiLabel2 = new UILabel();
            btnCancel = new UIButton();
            cboSerialPort = new UIComboBox();
            btnSave = new UIButton();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(StopBits2);
            uiPanel1.Controls.Add(StopBits1);
            uiPanel1.Controls.Add(uiLabel5);
            uiPanel1.Controls.Add(uiLabel4);
            uiPanel1.Controls.Add(cboParity);
            uiPanel1.Controls.Add(uiLabel3);
            uiPanel1.Controls.Add(cboDataBits);
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Controls.Add(cboBaudRate);
            uiPanel1.Controls.Add(uiLabel2);
            uiPanel1.Controls.Add(btnCancel);
            uiPanel1.Controls.Add(cboSerialPort);
            uiPanel1.Controls.Add(btnSave);
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.FillDisableColor = Color.White;
            uiPanel1.Font = new Font("宋体", 12F);
            uiPanel1.ForeColor = Color.FromArgb(49, 54, 64);
            uiPanel1.ForeDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel1.Location = new Point(18, 51);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 15;
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.Size = new Size(522, 383);
            uiPanel1.TabIndex = 410;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // StopBits2
            // 
            StopBits2.BackColor = Color.Transparent;
            StopBits2.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            StopBits2.ForeColor = Color.Black;
            StopBits2.Location = new Point(327, 260);
            StopBits2.MinimumSize = new Size(1, 1);
            StopBits2.Name = "StopBits2";
            StopBits2.Size = new Size(61, 29);
            StopBits2.TabIndex = 401;
            StopBits2.Text = "2";
            // 
            // StopBits1
            // 
            StopBits1.BackColor = Color.Transparent;
            StopBits1.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            StopBits1.ForeColor = Color.Black;
            StopBits1.Location = new Point(223, 260);
            StopBits1.MinimumSize = new Size(1, 1);
            StopBits1.Name = "StopBits1";
            StopBits1.Size = new Size(68, 29);
            StopBits1.TabIndex = 400;
            StopBits1.Text = "1";
            // 
            // uiLabel5
            // 
            uiLabel5.BackColor = Color.Transparent;
            uiLabel5.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel5.ForeColor = Color.Black;
            uiLabel5.ImeMode = ImeMode.NoControl;
            uiLabel5.Location = new Point(113, 260);
            uiLabel5.Name = "uiLabel5";
            uiLabel5.Size = new Size(76, 23);
            uiLabel5.TabIndex = 399;
            uiLabel5.Text = "停止位:";
            uiLabel5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // uiLabel4
            // 
            uiLabel4.BackColor = Color.Transparent;
            uiLabel4.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel4.ForeColor = Color.Black;
            uiLabel4.ImeMode = ImeMode.NoControl;
            uiLabel4.Location = new Point(113, 201);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(76, 23);
            uiLabel4.TabIndex = 398;
            uiLabel4.Text = "校验位:";
            uiLabel4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboParity
            // 
            cboParity.BackColor = Color.Transparent;
            cboParity.DataSource = null;
            cboParity.DropDownStyle = UIDropDownStyle.DropDownList;
            cboParity.FillColor = Color.FromArgb(218, 220, 230);
            cboParity.FillColor2 = Color.FromArgb(218, 220, 230);
            cboParity.FilterMaxCount = 50;
            cboParity.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            cboParity.ForeColor = Color.Black;
            cboParity.ForeDisableColor = Color.Black;
            cboParity.ItemFillColor = Color.FromArgb(42, 47, 55);
            cboParity.ItemForeColor = Color.White;
            cboParity.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboParity.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboParity.Location = new Point(199, 200);
            cboParity.Margin = new Padding(4, 5, 4, 5);
            cboParity.MinimumSize = new Size(63, 0);
            cboParity.Name = "cboParity";
            cboParity.Padding = new Padding(0, 0, 30, 2);
            cboParity.Radius = 10;
            cboParity.RectColor = Color.White;
            cboParity.RectDisableColor = Color.White;
            cboParity.RectSides = ToolStripStatusLabelBorderSides.None;
            cboParity.Size = new Size(210, 29);
            cboParity.SymbolSize = 24;
            cboParity.TabIndex = 397;
            cboParity.TextAlignment = ContentAlignment.MiddleLeft;
            cboParity.Watermark = "请选择";
            // 
            // uiLabel3
            // 
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.Black;
            uiLabel3.ImeMode = ImeMode.NoControl;
            uiLabel3.Location = new Point(113, 141);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(76, 23);
            uiLabel3.TabIndex = 396;
            uiLabel3.Text = "数据位:";
            uiLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboDataBits
            // 
            cboDataBits.BackColor = Color.Transparent;
            cboDataBits.DataSource = null;
            cboDataBits.DropDownStyle = UIDropDownStyle.DropDownList;
            cboDataBits.FillColor = Color.FromArgb(218, 220, 230);
            cboDataBits.FillColor2 = Color.FromArgb(218, 220, 230);
            cboDataBits.FilterMaxCount = 50;
            cboDataBits.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            cboDataBits.ForeColor = Color.Black;
            cboDataBits.ForeDisableColor = Color.Black;
            cboDataBits.ItemFillColor = Color.FromArgb(42, 47, 55);
            cboDataBits.ItemForeColor = Color.White;
            cboDataBits.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboDataBits.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboDataBits.Location = new Point(199, 143);
            cboDataBits.Margin = new Padding(4, 5, 4, 5);
            cboDataBits.MinimumSize = new Size(63, 0);
            cboDataBits.Name = "cboDataBits";
            cboDataBits.Padding = new Padding(0, 0, 30, 2);
            cboDataBits.Radius = 10;
            cboDataBits.RectColor = Color.White;
            cboDataBits.RectDisableColor = Color.White;
            cboDataBits.RectSides = ToolStripStatusLabelBorderSides.None;
            cboDataBits.Size = new Size(210, 29);
            cboDataBits.SymbolSize = 24;
            cboDataBits.TabIndex = 395;
            cboDataBits.TextAlignment = ContentAlignment.MiddleLeft;
            cboDataBits.Watermark = "请选择";
            // 
            // uiLabel1
            // 
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.Black;
            uiLabel1.ImeMode = ImeMode.NoControl;
            uiLabel1.Location = new Point(113, 86);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(76, 23);
            uiLabel1.TabIndex = 394;
            uiLabel1.Text = "波特率:";
            uiLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboBaudRate
            // 
            cboBaudRate.BackColor = Color.Transparent;
            cboBaudRate.DataSource = null;
            cboBaudRate.DropDownStyle = UIDropDownStyle.DropDownList;
            cboBaudRate.FillColor = Color.FromArgb(218, 220, 230);
            cboBaudRate.FillColor2 = Color.FromArgb(218, 220, 230);
            cboBaudRate.FilterMaxCount = 50;
            cboBaudRate.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold);
            cboBaudRate.ForeColor = Color.Black;
            cboBaudRate.ForeDisableColor = Color.Black;
            cboBaudRate.ItemFillColor = Color.FromArgb(42, 47, 55);
            cboBaudRate.ItemForeColor = Color.White;
            cboBaudRate.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboBaudRate.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboBaudRate.Location = new Point(199, 86);
            cboBaudRate.Margin = new Padding(4, 5, 4, 5);
            cboBaudRate.MinimumSize = new Size(63, 0);
            cboBaudRate.Name = "cboBaudRate";
            cboBaudRate.Padding = new Padding(0, 0, 30, 2);
            cboBaudRate.Radius = 10;
            cboBaudRate.RectColor = Color.White;
            cboBaudRate.RectDisableColor = Color.White;
            cboBaudRate.RectSides = ToolStripStatusLabelBorderSides.None;
            cboBaudRate.Size = new Size(210, 29);
            cboBaudRate.SymbolSize = 24;
            cboBaudRate.TabIndex = 393;
            cboBaudRate.TextAlignment = ContentAlignment.MiddleLeft;
            cboBaudRate.Watermark = "请选择";
            // 
            // uiLabel2
            // 
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.Black;
            uiLabel2.ImeMode = ImeMode.NoControl;
            uiLabel2.Location = new Point(113, 29);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(76, 23);
            uiLabel2.TabIndex = 80;
            uiLabel2.Text = "串口号:";
            uiLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(235, 227, 221);
            btnCancel.Location = new Point(272, 328);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnCancel.Size = new Size(126, 37);
            btnCancel.TabIndex = 392;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.TipsText = "1";
            btnCancel.Click += btnCance_Click;
            // 
            // cboSerialPort
            // 
            cboSerialPort.BackColor = Color.Transparent;
            cboSerialPort.DataSource = null;
            cboSerialPort.DropDownStyle = UIDropDownStyle.DropDownList;
            cboSerialPort.FillColor = Color.FromArgb(218, 220, 230);
            cboSerialPort.FillColor2 = Color.FromArgb(218, 220, 230);
            cboSerialPort.FilterMaxCount = 50;
            cboSerialPort.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            cboSerialPort.ForeColor = Color.Black;
            cboSerialPort.ForeDisableColor = Color.Black;
            cboSerialPort.ItemFillColor = Color.FromArgb(42, 47, 55);
            cboSerialPort.ItemForeColor = Color.White;
            cboSerialPort.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboSerialPort.Items.AddRange(new object[] { "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM10", "COM11", "COM12", "COM13", "COM14", "COM15", "COM16", "COM17", "COM18", "COM19", "COM20" });
            cboSerialPort.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboSerialPort.Location = new Point(199, 29);
            cboSerialPort.Margin = new Padding(4, 5, 4, 5);
            cboSerialPort.MinimumSize = new Size(63, 0);
            cboSerialPort.Name = "cboSerialPort";
            cboSerialPort.Padding = new Padding(0, 0, 30, 2);
            cboSerialPort.Radius = 10;
            cboSerialPort.RectColor = Color.White;
            cboSerialPort.RectDisableColor = Color.White;
            cboSerialPort.RectSides = ToolStripStatusLabelBorderSides.None;
            cboSerialPort.Size = new Size(210, 29);
            cboSerialPort.SymbolSize = 24;
            cboSerialPort.TabIndex = 79;
            cboSerialPort.TextAlignment = ContentAlignment.MiddleLeft;
            cboSerialPort.Watermark = "请选择";
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnSave.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(235, 227, 221);
            btnSave.Location = new Point(124, 328);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSave.Size = new Size(126, 37);
            btnSave.TabIndex = 391;
            btnSave.Text = "保存";
            btnSave.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSave.TipsText = "1";
            btnSave.Click += btnSave_Click;
            // 
            // FrmSetParameter
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(558, 450);
            ControlBox = false;
            Controls.Add(uiPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSetParameter";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "参数更改";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 15F, FontStyle.Bold);
            TitleForeColor = Color.FromArgb(236, 236, 236);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            uiPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private UIPanel uiPanel1;
        private UILabel uiLabel2;
        private UIButton btnCancel;
        private UIComboBox cboSerialPort;
        private UIButton btnSave;
        private UILabel uiLabel5;
        private UILabel uiLabel4;
        private UIComboBox cboParity;
        private UILabel uiLabel3;
        private UIComboBox cboDataBits;
        private UILabel uiLabel1;
        private UIComboBox cboBaudRate;
        private UIRadioButton StopBits1;
        private UIRadioButton StopBits2;
    }
}