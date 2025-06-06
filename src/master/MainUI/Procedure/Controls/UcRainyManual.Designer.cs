namespace MainUI.Procedure.Controls
{
    partial class UcRainyManual
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
            switchPictureBox = new SwitchPictureBox();
            uiPanel1 = new UIPanel();
            LabPressure = new UIDigitalLabel();
            LabPressureName = new UIPanel();
            uiPanel2 = new UIPanel();
            LabInternetTraffic = new UIDigitalLabel();
            LabInternetTrafficName = new UIPanel();
            TitlePanel = new UITitlePanel();
            ((System.ComponentModel.ISupportInitialize)switchPictureBox).BeginInit();
            uiPanel1.SuspendLayout();
            uiPanel2.SuspendLayout();
            TitlePanel.SuspendLayout();
            SuspendLayout();
            // 
            // switchPictureBox
            // 
            switchPictureBox.BackColor = Color.Transparent;
            switchPictureBox.CanClick = true;
            switchPictureBox.FalseImage = Resources.红灯;
            switchPictureBox.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            switchPictureBox.Image = Resources.绿灯;
            switchPictureBox.Location = new Point(73, 50);
            switchPictureBox.Name = "switchPictureBox";
            switchPictureBox.Size = new Size(50, 50);
            switchPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            switchPictureBox.Switch = true;
            switchPictureBox.TabIndex = 0;
            switchPictureBox.TabStop = false;
            switchPictureBox.Text = "电磁阀控制1";
            switchPictureBox.TextBackColor = Color.Transparent;
            switchPictureBox.TextLayout = TextLayout.Bottom;
            switchPictureBox.TrueImage = Resources.绿灯;
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(LabPressure);
            uiPanel1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiPanel1.Location = new Point(12, 154);
            uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Padding = new System.Windows.Forms.Padding(2);
            uiPanel1.Radius = 0;
            uiPanel1.RectColor = Color.FromArgb(239, 154, 78);
            uiPanel1.RectDisableColor = Color.FromArgb(239, 154, 78);
            uiPanel1.RectSize = 2;
            uiPanel1.Size = new Size(174, 63);
            uiPanel1.TabIndex = 493;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // LabPressure
            // 
            LabPressure.BackColor = Color.FromArgb(49, 54, 64);
            LabPressure.DecimalPlaces = 1;
            LabPressure.DigitalSize = 23;
            LabPressure.Dock = DockStyle.Fill;
            LabPressure.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            LabPressure.ForeColor = Color.FromArgb(255, 224, 192);
            LabPressure.Location = new Point(2, 2);
            LabPressure.MinimumSize = new Size(1, 1);
            LabPressure.Name = "LabPressure";
            LabPressure.RectSize = 2;
            LabPressure.Size = new Size(170, 59);
            LabPressure.TabIndex = 467;
            LabPressure.Tag = "0";
            LabPressure.TextAlign = HorizontalAlignment.Center;
            // 
            // LabPressureName
            // 
            LabPressureName.BackColor = Color.Transparent;
            LabPressureName.FillColor = Color.Transparent;
            LabPressureName.FillColor2 = Color.Transparent;
            LabPressureName.FillDisableColor = Color.FromArgb(42, 47, 55);
            LabPressureName.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            LabPressureName.ForeColor = Color.FromArgb(235, 227, 221);
            LabPressureName.ForeDisableColor = Color.FromArgb(235, 227, 221);
            LabPressureName.Location = new Point(19, 220);
            LabPressureName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            LabPressureName.MinimumSize = new Size(1, 1);
            LabPressureName.Name = "LabPressureName";
            LabPressureName.Radius = 0;
            LabPressureName.RectColor = Color.Transparent;
            LabPressureName.RectDisableColor = Color.Transparent;
            LabPressureName.Size = new Size(162, 35);
            LabPressureName.Style = UIStyle.Custom;
            LabPressureName.TabIndex = 492;
            LabPressureName.Tag = "3";
            LabPressureName.Text = "压力传感器1(kPa)";
            LabPressureName.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiPanel2
            // 
            uiPanel2.Controls.Add(LabInternetTraffic);
            uiPanel2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiPanel2.Location = new Point(10, 286);
            uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiPanel2.MinimumSize = new Size(1, 1);
            uiPanel2.Name = "uiPanel2";
            uiPanel2.Padding = new System.Windows.Forms.Padding(2);
            uiPanel2.Radius = 0;
            uiPanel2.RectColor = Color.FromArgb(239, 154, 78);
            uiPanel2.RectSize = 2;
            uiPanel2.Size = new Size(174, 63);
            uiPanel2.TabIndex = 496;
            uiPanel2.Text = null;
            uiPanel2.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // LabInternetTraffic
            // 
            LabInternetTraffic.BackColor = Color.FromArgb(49, 54, 64);
            LabInternetTraffic.DecimalPlaces = 1;
            LabInternetTraffic.DigitalSize = 23;
            LabInternetTraffic.Dock = DockStyle.Fill;
            LabInternetTraffic.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            LabInternetTraffic.ForeColor = Color.FromArgb(255, 224, 192);
            LabInternetTraffic.Location = new Point(2, 2);
            LabInternetTraffic.MinimumSize = new Size(1, 1);
            LabInternetTraffic.Name = "LabInternetTraffic";
            LabInternetTraffic.RectSize = 2;
            LabInternetTraffic.Size = new Size(170, 59);
            LabInternetTraffic.TabIndex = 467;
            LabInternetTraffic.Tag = "0";
            LabInternetTraffic.TextAlign = HorizontalAlignment.Center;
            // 
            // LabInternetTrafficName
            // 
            LabInternetTrafficName.BackColor = Color.Transparent;
            LabInternetTrafficName.FillColor = Color.Transparent;
            LabInternetTrafficName.FillColor2 = Color.Transparent;
            LabInternetTrafficName.FillDisableColor = Color.FromArgb(42, 47, 55);
            LabInternetTrafficName.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            LabInternetTrafficName.ForeColor = Color.FromArgb(235, 227, 221);
            LabInternetTrafficName.ForeDisableColor = Color.FromArgb(235, 227, 221);
            LabInternetTrafficName.Location = new Point(17, 352);
            LabInternetTrafficName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            LabInternetTrafficName.MinimumSize = new Size(1, 1);
            LabInternetTrafficName.Name = "LabInternetTrafficName";
            LabInternetTrafficName.Radius = 0;
            LabInternetTrafficName.RectColor = Color.Transparent;
            LabInternetTrafficName.RectDisableColor = Color.Transparent;
            LabInternetTrafficName.Size = new Size(162, 35);
            LabInternetTrafficName.Style = UIStyle.Custom;
            LabInternetTrafficName.TabIndex = 495;
            LabInternetTrafficName.Tag = "3";
            LabInternetTrafficName.Text = "流量传感器1(m³/h)";
            LabInternetTrafficName.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // TitlePanel
            // 
            TitlePanel.Controls.Add(switchPictureBox);
            TitlePanel.Controls.Add(uiPanel2);
            TitlePanel.Controls.Add(LabInternetTrafficName);
            TitlePanel.Controls.Add(LabPressureName);
            TitlePanel.Controls.Add(uiPanel1);
            TitlePanel.Dock = DockStyle.Fill;
            TitlePanel.FillColor = Color.FromArgb(49, 54, 64);
            TitlePanel.FillColor2 = Color.FromArgb(49, 54, 64);
            TitlePanel.Font = new Font("思源黑体 CN Bold", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
            TitlePanel.Location = new Point(0, 0);
            TitlePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            TitlePanel.MinimumSize = new Size(1, 1);
            TitlePanel.Name = "TitlePanel";
            TitlePanel.Padding = new System.Windows.Forms.Padding(1, 30, 1, 1);
            TitlePanel.RectColor = Color.FromArgb(49, 54, 64);
            TitlePanel.RectDisableColor = Color.FromArgb(49, 54, 64);
            TitlePanel.ShowText = false;
            TitlePanel.Size = new Size(197, 409);
            TitlePanel.TabIndex = 497;
            TitlePanel.Text = "标题栏";
            TitlePanel.TextAlignment = ContentAlignment.MiddleCenter;
            TitlePanel.TitleColor = Color.FromArgb(239, 154, 78);
            TitlePanel.TitleForeColor = Color.FromArgb(235, 227, 221);
            TitlePanel.TitleHeight = 30;
            // 
            // UcRainyManual
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 46, 57);
            Controls.Add(TitlePanel);
            DoubleBuffered = true;
            Name = "UcRainyManual";
            Size = new Size(197, 409);
            ((System.ComponentModel.ISupportInitialize)switchPictureBox).EndInit();
            uiPanel1.ResumeLayout(false);
            uiPanel2.ResumeLayout(false);
            TitlePanel.ResumeLayout(false);
            TitlePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SwitchPictureBox switchPictureBox;
        private UIPanel uiPanel1;
        private UIDigitalLabel LabPressure;
        private UIPanel LabPressureName;
        private UIPanel uiPanel2;
        private UIDigitalLabel LabInternetTraffic;
        private UIPanel LabInternetTrafficName;
        private UITitlePanel TitlePanel;
    }
}
