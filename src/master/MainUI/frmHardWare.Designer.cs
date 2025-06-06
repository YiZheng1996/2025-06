namespace MainUI
{
    partial class frmHardWare
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
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            label12 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            grpAI = new UIGroupBox();
            panel3 = new Panel();
            ucCalibration6 = new MainUI.Procedure.UCCalibration();
            ucCalibration7 = new MainUI.Procedure.UCCalibration();
            ucCalibration5 = new MainUI.Procedure.UCCalibration();
            ucCalibration4 = new MainUI.Procedure.UCCalibration();
            ucCalibration3 = new MainUI.Procedure.UCCalibration();
            ucCalibration2 = new MainUI.Procedure.UCCalibration();
            ucCalibration1 = new MainUI.Procedure.UCCalibration();
            panel2 = new Panel();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            grpAO = new UIGroupBox();
            panel1.SuspendLayout();
            grpAI.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            grpAO.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(44, 62, 80);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Font = new Font("微软雅黑", 15F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panel1.ForeColor = Color.White;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(650, 38);
            panel1.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            label2.ImeMode = ImeMode.NoControl;
            label2.Location = new Point(340, 7);
            label2.Name = "label2";
            label2.Size = new Size(66, 26);
            label2.TabIndex = 7;
            label2.Text = "增益值";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            label3.ImeMode = ImeMode.NoControl;
            label3.Location = new Point(154, 7);
            label3.Name = "label3";
            label3.Size = new Size(66, 26);
            label3.TabIndex = 7;
            label3.Text = "测定值";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(250, 7);
            label1.Name = "label1";
            label1.Size = new Size(66, 26);
            label1.TabIndex = 7;
            label1.Text = "零点值";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("思源黑体 CN Bold", 17F, FontStyle.Bold);
            label12.ForeColor = Color.FromArgb(46, 46, 46);
            label12.ImeMode = ImeMode.NoControl;
            label12.Location = new Point(19, 615);
            label12.Name = "label12";
            label12.Size = new Size(481, 33);
            label12.TabIndex = 54;
            label12.Text = "计算公式：测定值 = 工程值 * 增益值 - 零点值  ";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // grpAI
            // 
            grpAI.BackColor = Color.White;
            grpAI.Controls.Add(panel3);
            grpAI.FillColor = Color.White;
            grpAI.FillColor2 = Color.White;
            grpAI.FillDisableColor = Color.FromArgb(49, 54, 64);
            grpAI.Font = new Font("思源黑体 CN Bold", 17F, FontStyle.Bold);
            grpAI.ForeColor = Color.FromArgb(46, 46, 46);
            grpAI.ForeDisableColor = Color.FromArgb(235, 227, 221);
            grpAI.Location = new Point(19, 65);
            grpAI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            grpAI.MinimumSize = new Size(1, 1);
            grpAI.Name = "grpAI";
            grpAI.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            grpAI.Radius = 15;
            grpAI.RectColor = Color.White;
            grpAI.RectDisableColor = Color.White;
            grpAI.Size = new Size(650, 541);
            grpAI.TabIndex = 382;
            grpAI.Text = "输入检测";
            grpAI.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.AutoScroll = true;
            panel3.BackColor = Color.White;
            panel3.Controls.Add(ucCalibration6);
            panel3.Controls.Add(ucCalibration7);
            panel3.Controls.Add(ucCalibration5);
            panel3.Controls.Add(ucCalibration4);
            panel3.Controls.Add(ucCalibration3);
            panel3.Controls.Add(ucCalibration2);
            panel3.Controls.Add(ucCalibration1);
            panel3.Controls.Add(panel1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 32);
            panel3.Name = "panel3";
            panel3.Size = new Size(650, 509);
            panel3.TabIndex = 17;
            // 
            // ucCalibration6
            // 
            ucCalibration6.Font = new Font("微软雅黑", 12F);
            ucCalibration6.GainValue = 0D;
            ucCalibration6.Index = 6;
            ucCalibration6.Location = new Point(23, 427);
            ucCalibration6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ucCalibration6.Name = "ucCalibration6";
            ucCalibration6.Size = new Size(623, 38);
            ucCalibration6.TabIndex = 17;
            ucCalibration6.Text = "流量传感器";
            ucCalibration6.Submited += ucCalibration_AI_Submited;
            // 
            // ucCalibration7
            // 
            ucCalibration7.Font = new Font("微软雅黑", 12F);
            ucCalibration7.GainValue = 0D;
            ucCalibration7.Index = 5;
            ucCalibration7.Location = new Point(23, 367);
            ucCalibration7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ucCalibration7.Name = "ucCalibration7";
            ucCalibration7.Size = new Size(623, 38);
            ucCalibration7.TabIndex = 16;
            ucCalibration7.Text = "腔室压力传感器";
            ucCalibration7.Submited += ucCalibration_AI_Submited;
            // 
            // ucCalibration5
            // 
            ucCalibration5.Font = new Font("微软雅黑", 12F);
            ucCalibration5.GainValue = 0D;
            ucCalibration5.Index = 4;
            ucCalibration5.Location = new Point(23, 307);
            ucCalibration5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ucCalibration5.Name = "ucCalibration5";
            ucCalibration5.Size = new Size(623, 38);
            ucCalibration5.TabIndex = 15;
            ucCalibration5.Text = "拉绳传感器05";
            ucCalibration5.Submited += ucCalibration_AI_Submited;
            // 
            // ucCalibration4
            // 
            ucCalibration4.Font = new Font("微软雅黑", 12F);
            ucCalibration4.GainValue = 0D;
            ucCalibration4.Index = 3;
            ucCalibration4.Location = new Point(23, 247);
            ucCalibration4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ucCalibration4.Name = "ucCalibration4";
            ucCalibration4.Size = new Size(623, 38);
            ucCalibration4.TabIndex = 14;
            ucCalibration4.Text = "拉绳传感器04";
            ucCalibration4.Submited += ucCalibration_AI_Submited;
            // 
            // ucCalibration3
            // 
            ucCalibration3.Font = new Font("微软雅黑", 12F);
            ucCalibration3.GainValue = 0D;
            ucCalibration3.Index = 2;
            ucCalibration3.Location = new Point(23, 187);
            ucCalibration3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ucCalibration3.Name = "ucCalibration3";
            ucCalibration3.Size = new Size(623, 38);
            ucCalibration3.TabIndex = 13;
            ucCalibration3.Text = "拉绳传感器03";
            ucCalibration3.Submited += ucCalibration_AI_Submited;
            // 
            // ucCalibration2
            // 
            ucCalibration2.Font = new Font("微软雅黑", 12F);
            ucCalibration2.GainValue = 0D;
            ucCalibration2.Index = 1;
            ucCalibration2.Location = new Point(23, 127);
            ucCalibration2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ucCalibration2.Name = "ucCalibration2";
            ucCalibration2.Size = new Size(623, 38);
            ucCalibration2.TabIndex = 12;
            ucCalibration2.Text = "拉绳传感器02";
            ucCalibration2.Submited += ucCalibration_AI_Submited;
            // 
            // ucCalibration1
            // 
            ucCalibration1.Font = new Font("微软雅黑", 12F);
            ucCalibration1.GainValue = 0D;
            ucCalibration1.Location = new Point(23, 67);
            ucCalibration1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ucCalibration1.Name = "ucCalibration1";
            ucCalibration1.Size = new Size(623, 38);
            ucCalibration1.TabIndex = 11;
            ucCalibration1.Text = "拉绳传感器01";
            ucCalibration1.Submited += ucCalibration_AI_Submited;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(239, 154, 78);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label6);
            panel2.Dock = DockStyle.Top;
            panel2.Font = new Font("微软雅黑", 15F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panel2.Location = new Point(0, 32);
            panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(42, 38);
            panel2.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(235, 227, 221);
            label4.ImeMode = ImeMode.NoControl;
            label4.Location = new Point(340, 7);
            label4.Name = "label4";
            label4.Size = new Size(66, 26);
            label4.TabIndex = 7;
            label4.Text = "增益值";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(235, 227, 221);
            label5.ImeMode = ImeMode.NoControl;
            label5.Location = new Point(154, 7);
            label5.Name = "label5";
            label5.Size = new Size(66, 26);
            label5.TabIndex = 7;
            label5.Text = "测定值";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(235, 227, 221);
            label6.ImeMode = ImeMode.NoControl;
            label6.Location = new Point(250, 7);
            label6.Name = "label6";
            label6.Size = new Size(66, 26);
            label6.TabIndex = 7;
            label6.Text = "零点值";
            // 
            // grpAO
            // 
            grpAO.BackColor = Color.FromArgb(49, 54, 64);
            grpAO.Controls.Add(panel2);
            grpAO.FillColor = Color.FromArgb(49, 54, 64);
            grpAO.FillColor2 = Color.FromArgb(49, 54, 64);
            grpAO.FillDisableColor = Color.FromArgb(49, 54, 64);
            grpAO.Font = new Font("思源黑体 CN Bold", 17F, FontStyle.Bold);
            grpAO.ForeColor = Color.FromArgb(235, 227, 221);
            grpAO.Location = new Point(544, 619);
            grpAO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            grpAO.MinimumSize = new Size(1, 1);
            grpAO.Name = "grpAO";
            grpAO.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            grpAO.RectColor = Color.FromArgb(49, 54, 64);
            grpAO.RectDisableColor = Color.FromArgb(49, 54, 64);
            grpAO.Size = new Size(42, 29);
            grpAO.TabIndex = 383;
            grpAO.Text = "输出检测";
            grpAO.TextAlignment = ContentAlignment.MiddleCenter;
            grpAO.Visible = false;
            // 
            // frmHardWare
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(696, 664);
            Controls.Add(grpAO);
            Controls.Add(grpAI);
            Controls.Add(label12);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmHardWare";
            RectColor = Color.FromArgb(49, 54, 64);
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "硬件校准";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("思源黑体 CN Heavy", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 1270, 771);
            Load += frmHardWare_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            grpAI.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            grpAO.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.Timer timer1;
        private Sunny.UI.UIGroupBox grpAI;
        private System.Windows.Forms.Panel panel3;
        private Panel panel2;
        private Label label4;
        private Label label5;
        private Label label6;
        private UIGroupBox grpAO;
        private Procedure.UCCalibration ucCalibration1;
        private Procedure.UCCalibration ucCalibration5;
        private Procedure.UCCalibration ucCalibration4;
        private Procedure.UCCalibration ucCalibration3;
        private Procedure.UCCalibration ucCalibration2;
        private Procedure.UCCalibration ucCalibration6;
        private Procedure.UCCalibration ucCalibration7;
    }
}