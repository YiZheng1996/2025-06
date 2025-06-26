namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    partial class frmLogicalConfiguration
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
            ToolTreeView = new UITreeView();
            uiLine1 = new UILine();
            ProcessDataGridView = new UIDataGridView();
            ColProcessName = new DataGridViewTextBoxColumn();
            ColStepNum = new DataGridViewTextBoxColumn();
            uiContextMenuStrip1 = new UIContextMenuStrip();
            toolDeleteStep = new ToolStripMenuItem();
            uiLine2 = new UILine();
            btnSave = new UIButton();
            BtnClose = new UIButton();
            uiPanel1 = new UIPanel();
            ((System.ComponentModel.ISupportInitialize)ProcessDataGridView).BeginInit();
            uiContextMenuStrip1.SuspendLayout();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // ToolTreeView
            // 
            ToolTreeView.BackColor = Color.Transparent;
            ToolTreeView.FillColor = Color.White;
            ToolTreeView.FillColor2 = Color.White;
            ToolTreeView.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ToolTreeView.Location = new Point(26, 87);
            ToolTreeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ToolTreeView.MinimumSize = new Size(1, 1);
            ToolTreeView.Name = "ToolTreeView";
            ToolTreeView.Radius = 10;
            ToolTreeView.RectColor = Color.White;
            ToolTreeView.RectDisableColor = Color.White;
            ToolTreeView.ScrollBarStyleInherited = false;
            ToolTreeView.ShowLines = true;
            ToolTreeView.ShowNodeToolTips = true;
            ToolTreeView.ShowText = false;
            ToolTreeView.Size = new Size(321, 714);
            ToolTreeView.TabIndex = 0;
            ToolTreeView.Text = null;
            ToolTreeView.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.EndCap = UILineCap.Circle;
            uiLine1.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.White;
            uiLine1.Location = new Point(26, 50);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(321, 29);
            uiLine1.TabIndex = 1;
            uiLine1.Text = "工具箱";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ProcessDataGridView
            // 
            ProcessDataGridView.AllowDrop = true;
            ProcessDataGridView.AllowUserToAddRows = false;
            ProcessDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            ProcessDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            ProcessDataGridView.BackgroundColor = Color.White;
            ProcessDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.Font = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            ProcessDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            ProcessDataGridView.ColumnHeadersHeight = 40;
            ProcessDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ProcessDataGridView.Columns.AddRange(new DataGridViewColumn[] { ColProcessName, ColStepNum });
            ProcessDataGridView.ContextMenuStrip = uiContextMenuStrip1;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            ProcessDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            ProcessDataGridView.EnableHeadersVisualStyles = false;
            ProcessDataGridView.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            ProcessDataGridView.GridColor = Color.Black;
            ProcessDataGridView.Location = new Point(376, 87);
            ProcessDataGridView.Name = "ProcessDataGridView";
            ProcessDataGridView.ReadOnly = true;
            ProcessDataGridView.RectColor = Color.White;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            ProcessDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ProcessDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            ProcessDataGridView.RowTemplate.Height = 35;
            ProcessDataGridView.SelectedIndex = -1;
            ProcessDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ProcessDataGridView.Size = new Size(585, 714);
            ProcessDataGridView.StripeOddColor = Color.FromArgb(235, 243, 255);
            ProcessDataGridView.TabIndex = 2;
            // 
            // ColProcessName
            // 
            ColProcessName.HeaderText = "步骤名称";
            ColProcessName.Name = "ColProcessName";
            ColProcessName.ReadOnly = true;
            ColProcessName.Width = 200;
            // 
            // ColStepNum
            // 
            ColStepNum.HeaderText = "步骤号";
            ColStepNum.Name = "ColStepNum";
            ColStepNum.ReadOnly = true;
            // 
            // uiContextMenuStrip1
            // 
            uiContextMenuStrip1.BackColor = Color.FromArgb(243, 249, 255);
            uiContextMenuStrip1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiContextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolDeleteStep });
            uiContextMenuStrip1.Name = "uiContextMenuStrip1";
            uiContextMenuStrip1.Size = new Size(142, 30);
            // 
            // toolDeleteStep
            // 
            toolDeleteStep.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            toolDeleteStep.Name = "toolDeleteStep";
            toolDeleteStep.Size = new Size(141, 26);
            toolDeleteStep.Text = "删除步骤";
            toolDeleteStep.Click += toolDeleteStep_Click;
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.EndCap = UILineCap.Circle;
            uiLine2.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.White;
            uiLine2.Location = new Point(376, 50);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(585, 29);
            uiLine2.TabIndex = 3;
            uiLine2.Text = "当前流程";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            btnSave.Location = new Point(222, 9);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSave.Size = new Size(147, 37);
            btnSave.TabIndex = 435;
            btnSave.Text = "保 存";
            btnSave.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSave.TipsText = "1";
            btnSave.Click += btnSave_Click;
            // 
            // BtnClose
            // 
            BtnClose.Cursor = Cursors.Hand;
            BtnClose.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            BtnClose.Location = new Point(565, 9);
            BtnClose.MinimumSize = new Size(1, 1);
            BtnClose.Name = "BtnClose";
            BtnClose.RectDisableColor = Color.FromArgb(80, 160, 255);
            BtnClose.Size = new Size(147, 37);
            BtnClose.TabIndex = 436;
            BtnClose.Text = "取 消";
            BtnClose.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnClose.TipsText = "1";
            BtnClose.Click += BtnClose_Click;
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(btnSave);
            uiPanel1.Controls.Add(BtnClose);
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiPanel1.Location = new Point(26, 809);
            uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 10;
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.Size = new Size(935, 57);
            uiPanel1.TabIndex = 437;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // frmLogicalConfiguration
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(986, 878);
            ControlBox = false;
            Controls.Add(uiPanel1);
            Controls.Add(uiLine2);
            Controls.Add(ProcessDataGridView);
            Controls.Add(uiLine1);
            Controls.Add(ToolTreeView);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmLogicalConfiguration";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "试验逻辑配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            ((System.ComponentModel.ISupportInitialize)ProcessDataGridView).EndInit();
            uiContextMenuStrip1.ResumeLayout(false);
            uiPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private UITreeView ToolTreeView;
        private UILine uiLine1;
        private UILine uiLine2;
        private UIDataGridView ProcessDataGridView;
        private DataGridViewTextBoxColumn ColProcessName;
        private UIButton btnSave;
        private UIButton BtnClose;
        private UIPanel uiPanel1;
        private DataGridViewTextBoxColumn ColStepNum;
        private UIContextMenuStrip uiContextMenuStrip1;
        private ToolStripMenuItem toolDeleteStep;
    }
}