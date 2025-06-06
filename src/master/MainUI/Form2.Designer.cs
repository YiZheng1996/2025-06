namespace MainUI
{
    partial class Form2
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
            cartesianChart1 = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            btnStart = new UIButton();
            btnStop = new UIButton();
            btnSavePicture = new UIButton();
            btnResetView = new UIButton();
            label1 = new AntdUI.Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // cartesianChart1
            // 
            cartesianChart1.BorderStyle = BorderStyle.FixedSingle;
            cartesianChart1.Location = new Point(12, 29);
            cartesianChart1.Name = "cartesianChart1";
            cartesianChart1.Size = new Size(1270, 657);
            cartesianChart1.TabIndex = 0;
            // 
            // btnStart
            // 
            btnStart.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnStart.Location = new Point(339, 714);
            btnStart.MinimumSize = new Size(1, 1);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(127, 44);
            btnStart.TabIndex = 1;
            btnStart.Text = "开始绘画";
            btnStart.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnStop.Location = new Point(502, 714);
            btnStop.MinimumSize = new Size(1, 1);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(127, 44);
            btnStop.TabIndex = 2;
            btnStop.Text = "结束绘画";
            btnStop.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnStop.Click += btnStop_Click;
            // 
            // btnSavePicture
            // 
            btnSavePicture.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSavePicture.Location = new Point(665, 714);
            btnSavePicture.MinimumSize = new Size(1, 1);
            btnSavePicture.Name = "btnSavePicture";
            btnSavePicture.Size = new Size(127, 44);
            btnSavePicture.TabIndex = 3;
            btnSavePicture.Text = "保存图片";
            btnSavePicture.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSavePicture.Click += btnSavePicture_Click;
            // 
            // btnResetView
            // 
            btnResetView.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnResetView.Location = new Point(828, 714);
            btnResetView.MinimumSize = new Size(1, 1);
            btnResetView.Name = "btnResetView";
            btnResetView.Size = new Size(127, 44);
            btnResetView.TabIndex = 4;
            btnResetView.Text = "重置视图";
            btnResetView.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnResetView.Click += btnResetView_Click;
            // 
            // label1
            // 
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.Size = new Size(75, 23);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // button1
            // 
            button1.Location = new Point(84, 3);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1294, 770);
            Controls.Add(btnResetView);
            Controls.Add(btnSavePicture);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(cartesianChart1);
            Name = "Form2";
            ShowIcon = false;
            Text = " ";
            ResumeLayout(false);
        }

        #endregion

        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart cartesianChart1;
        private UIButton btnStart;
        private UIButton btnStop;
        private UIButton btnSavePicture;
        private UIButton btnResetView;
        private Button button1;
        private AntdUI.Label label1;
    }
}