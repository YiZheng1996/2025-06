using Padding = System.Windows.Forms.Padding;

namespace MainUI.Procedure.DSL
{
    partial class ucModules
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
            uiPanel1 = new UIPanel();
            uiLabel3 = new UILabel();
            uiLabel2 = new UILabel();
            cboCOMType = new UIComboBox();
            btnRight = new UIButton();
            btnLeft = new UIButton();
            btnSave = new UIButton();
            uiLabel1 = new UILabel();
            uiLabel20 = new UILabel();
            lstAllModule = new UIListBox();
            lstInjectModule = new UIListBox();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiPanel1
            // 
            uiPanel1.BackColor = Color.FromArgb(236, 236, 236);
            uiPanel1.Controls.Add(uiLabel3);
            uiPanel1.Controls.Add(uiLabel2);
            uiPanel1.Controls.Add(cboCOMType);
            uiPanel1.Controls.Add(btnRight);
            uiPanel1.Controls.Add(btnLeft);
            uiPanel1.Controls.Add(btnSave);
            uiPanel1.Controls.Add(uiLabel1);
            uiPanel1.Controls.Add(uiLabel20);
            uiPanel1.Controls.Add(lstAllModule);
            uiPanel1.Controls.Add(lstInjectModule);
            uiPanel1.Dock = DockStyle.Fill;
            uiPanel1.FillColor = Color.FromArgb(236, 236, 236);
            uiPanel1.FillColor2 = Color.FromArgb(236, 236, 236);
            uiPanel1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiPanel1.Location = new Point(0, 0);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.RectColor = Color.FromArgb(236, 236, 236);
            uiPanel1.RectDisableColor = Color.FromArgb(236, 236, 236);
            uiPanel1.Size = new Size(665, 660);
            uiPanel1.TabIndex = 0;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("思源黑体 CN Bold", 14F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(192, 0, 0);
            uiLabel3.Location = new Point(38, 612);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(368, 28);
            uiLabel3.TabIndex = 439;
            uiLabel3.Text = "注：双击[模块注入]名称，可修改模块参数";
            uiLabel3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.Black;
            uiLabel2.Location = new Point(157, 18);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(90, 26);
            uiLabel2.TabIndex = 438;
            uiLabel2.Text = "通讯类型:";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboCOMType
            // 
            cboCOMType.BackColor = Color.Transparent;
            cboCOMType.DataSource = null;
            cboCOMType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboCOMType.FillColor = Color.FromArgb(218, 220, 230);
            cboCOMType.FillColor2 = Color.FromArgb(218, 220, 230);
            cboCOMType.FillDisableColor = Color.FromArgb(49, 54, 64);
            cboCOMType.FilterMaxCount = 50;
            cboCOMType.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            cboCOMType.ForeColor = Color.Black;
            cboCOMType.ForeDisableColor = Color.Black;
            cboCOMType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboCOMType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboCOMType.Location = new Point(249, 16);
            cboCOMType.Margin = new Padding(4, 5, 4, 5);
            cboCOMType.MinimumSize = new Size(63, 0);
            cboCOMType.Name = "cboCOMType";
            cboCOMType.Padding = new Padding(0, 0, 30, 2);
            cboCOMType.Radius = 10;
            cboCOMType.RectColor = Color.Gray;
            cboCOMType.RectDisableColor = Color.Gray;
            cboCOMType.Size = new Size(200, 29);
            cboCOMType.SymbolSize = 24;
            cboCOMType.TabIndex = 437;
            cboCOMType.TextAlignment = ContentAlignment.MiddleLeft;
            cboCOMType.Watermark = "请选择";
            cboCOMType.SelectedIndexChanged += cboCOMType_SelectedIndexChanged;
            // 
            // btnRight
            // 
            btnRight.Cursor = Cursors.Hand;
            btnRight.Font = new Font("微软雅黑", 15F, FontStyle.Bold);
            btnRight.ForeDisableColor = Color.White;
            btnRight.Location = new Point(313, 368);
            btnRight.MinimumSize = new Size(1, 1);
            btnRight.Name = "btnRight";
            btnRight.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnRight.Size = new Size(35, 35);
            btnRight.TabIndex = 436;
            btnRight.Text = "→";
            btnRight.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnRight.TipsText = "1";
            btnRight.Click += btnRight_Click;
            // 
            // btnLeft
            // 
            btnLeft.Cursor = Cursors.Hand;
            btnLeft.Font = new Font("微软雅黑", 15F, FontStyle.Bold);
            btnLeft.ForeDisableColor = Color.White;
            btnLeft.Location = new Point(313, 292);
            btnLeft.MinimumSize = new Size(1, 1);
            btnLeft.Name = "btnLeft";
            btnLeft.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnLeft.Size = new Size(35, 35);
            btnLeft.TabIndex = 435;
            btnLeft.Text = "←";
            btnLeft.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnLeft.TipsText = "1";
            btnLeft.Click += btnLeft_Click;
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            btnSave.ForeDisableColor = Color.White;
            btnSave.Location = new Point(475, 611);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSave.Size = new Size(147, 37);
            btnSave.TabIndex = 434;
            btnSave.Text = "保 存";
            btnSave.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSave.TipsText = "1";
            btnSave.Click += btnSave_Click;
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("思源黑体 CN Bold", 15F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.Black;
            uiLabel1.Location = new Point(441, 67);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(93, 29);
            uiLabel1.TabIndex = 433;
            uiLabel1.Text = "模块列表";
            uiLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // uiLabel20
            // 
            uiLabel20.AutoSize = true;
            uiLabel20.BackColor = Color.Transparent;
            uiLabel20.Font = new Font("思源黑体 CN Bold", 15F, FontStyle.Bold);
            uiLabel20.ForeColor = Color.Black;
            uiLabel20.Location = new Point(104, 67);
            uiLabel20.Name = "uiLabel20";
            uiLabel20.Size = new Size(93, 29);
            uiLabel20.TabIndex = 432;
            uiLabel20.Text = "模块注入";
            uiLabel20.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lstAllModule
            // 
            lstAllModule.FillColor = Color.White;
            lstAllModule.FillColor2 = Color.White;
            lstAllModule.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lstAllModule.ForeColor = Color.Black;
            lstAllModule.ForeDisableColor = Color.Black;
            lstAllModule.HoverColor = Color.FromArgb(155, 200, 255);
            lstAllModule.ItemSelectBackColor = Color.FromArgb(189, 179, 172);
            lstAllModule.ItemSelectForeColor = Color.White;
            lstAllModule.Location = new Point(368, 100);
            lstAllModule.Margin = new Padding(4, 5, 4, 5);
            lstAllModule.MinimumSize = new Size(1, 1);
            lstAllModule.Name = "lstAllModule";
            lstAllModule.Padding = new Padding(2);
            lstAllModule.RectColor = Color.White;
            lstAllModule.RectDisableColor = Color.White;
            lstAllModule.ShowText = false;
            lstAllModule.Size = new Size(254, 496);
            lstAllModule.TabIndex = 1;
            lstAllModule.Text = null;
            // 
            // lstInjectModule
            // 
            lstInjectModule.FillColor = Color.White;
            lstInjectModule.FillColor2 = Color.White;
            lstInjectModule.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lstInjectModule.ForeColor = Color.Black;
            lstInjectModule.ForeDisableColor = Color.Black;
            lstInjectModule.HoverColor = Color.FromArgb(155, 200, 255);
            lstInjectModule.ItemSelectBackColor = Color.FromArgb(189, 179, 172);
            lstInjectModule.ItemSelectForeColor = Color.White;
            lstInjectModule.Location = new Point(41, 100);
            lstInjectModule.Margin = new Padding(4, 5, 4, 5);
            lstInjectModule.MinimumSize = new Size(1, 1);
            lstInjectModule.Name = "lstInjectModule";
            lstInjectModule.Padding = new Padding(2);
            lstInjectModule.RectColor = Color.White;
            lstInjectModule.RectDisableColor = Color.White;
            lstInjectModule.ShowText = false;
            lstInjectModule.Size = new Size(254, 496);
            lstInjectModule.TabIndex = 0;
            lstInjectModule.Text = null;
            lstInjectModule.MouseDoubleClick += lstInjectModule_MouseDoubleClick;
            // 
            // ucModules
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(uiPanel1);
            ForeColor = SystemColors.ControlLightLight;
            Name = "ucModules";
            Size = new Size(665, 660);
            uiPanel1.ResumeLayout(false);
            uiPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private UIPanel uiPanel1;
        private UIListBox lstInjectModule;
        private UIListBox lstAllModule;
        private UILabel uiLabel20;
        private UILabel uiLabel1;
        private UIButton btnSave;
        private UIButton btnLeft;
        private UIButton btnRight;
        private UILabel uiLabel2;
        private UIComboBox cboCOMType;
        private UILabel uiLabel3;
    }
}
