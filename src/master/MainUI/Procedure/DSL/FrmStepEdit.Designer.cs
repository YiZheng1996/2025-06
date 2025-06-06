using Padding = System.Windows.Forms.Padding;

namespace MainUI.Procedure.DSL
{
    partial class FrmStepEdit
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
            txtDSL = new UIRichTextBox();
            btnSave = new UISymbolButton();
            btnCanl = new UISymbolButton();
            SuspendLayout();
            // 
            // txtDSL
            // 
            txtDSL.BackColor = Color.Transparent;
            txtDSL.FillColor = Color.White;
            txtDSL.FillColor2 = Color.White;
            txtDSL.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            txtDSL.ForeColor = Color.FromArgb(235, 227, 221);
            txtDSL.ForeDisableColor = Color.FromArgb(235, 227, 221);
            txtDSL.Location = new Point(33, 56);
            txtDSL.Margin = new Padding(4, 5, 4, 5);
            txtDSL.MinimumSize = new Size(1, 1);
            txtDSL.Name = "txtDSL";
            txtDSL.Padding = new Padding(2);
            txtDSL.Radius = 10;
            txtDSL.RectColor = Color.White;
            txtDSL.RectDisableColor = Color.White;
            txtDSL.ShowText = false;
            txtDSL.Size = new Size(837, 728);
            txtDSL.TabIndex = 0;
            txtDSL.TextAlignment = ContentAlignment.MiddleCenter;
            txtDSL.Load += FrmStepEdit_Load;
            // 
            // btnSave
            // 
            btnSave.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnSave.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnSave.ForeDisableColor = Color.White;
            btnSave.Location = new Point(887, 320);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSave.Size = new Size(136, 37);
            btnSave.Symbol = 0;
            btnSave.TabIndex = 120;
            btnSave.Text = "保 存";
            btnSave.TipsFont = new Font("宋体", 11F);
            btnSave.Click += btnSave_Click;
            // 
            // btnCanl
            // 
            btnCanl.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnCanl.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnCanl.ForeDisableColor = Color.White;
            btnCanl.Location = new Point(887, 453);
            btnCanl.MinimumSize = new Size(1, 1);
            btnCanl.Name = "btnCanl";
            btnCanl.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnCanl.Size = new Size(136, 37);
            btnCanl.Symbol = 0;
            btnCanl.TabIndex = 121;
            btnCanl.Text = "取 消";
            btnCanl.TipsFont = new Font("宋体", 11F);
            btnCanl.Click += btnCanl_Click;
            // 
            // FrmStepEdit
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(1036, 810);
            ControlBox = false;
            Controls.Add(btnCanl);
            Controls.Add(btnSave);
            Controls.Add(txtDSL);
            Font = new Font("思源黑体 CN Bold", 11F, FontStyle.Bold);
            ForeColor = Color.FromArgb(235, 227, 221);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmStepEdit";
            RectColor = Color.FromArgb(236, 236, 236);
            ShowIcon = false;
            Text = "试验逻辑编写";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 15F, FontStyle.Bold);
            TitleForeColor = Color.FromArgb(236, 236, 236);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            ResumeLayout(false);
        }

        #endregion

        private UIRichTextBox txtDSL;
        private UISymbolButton btnSave;
        private UISymbolButton btnCanl;
    }
}