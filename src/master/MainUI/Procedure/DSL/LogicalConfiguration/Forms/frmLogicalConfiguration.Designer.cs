namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    partial class FrmLogicalConfiguration
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
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            ToolTreeView = new UITreeView();
            uiLine1 = new UILine();
            ProcessDataGridView = new UIDataGridView();
            ColProcessName = new DataGridViewTextBoxColumn();
            ColStepNum = new DataGridViewTextBoxColumn();
            uiContextMenuStrip1 = new UIContextMenuStrip();
            toolDeleteStep = new ToolStripMenuItem();
            uiLine2 = new UILine();
            uiPanel1 = new UIPanel();
            btnExecute = new UISymbolButton();
            BtnClose = new UISymbolButton();
            btnSave = new UISymbolButton();
            uiLine3 = new UILine();
            groupStepLog = new UITitlePanel();
            txtExecutionLog = new RichTextBox();
            groupStepInfo = new UITitlePanel();
            lblExecTimeValue = new AntdUI.Label();
            lblStatusValue = new AntdUI.Label();
            lblStepNameValue = new AntdUI.Label();
            lblStepNumberValue = new AntdUI.Label();
            lblExecTimeTitle = new AntdUI.Label();
            lblStatusTitle = new AntdUI.Label();
            lblStepNameTitle = new AntdUI.Label();
            lblStepNumberTitle = new AntdUI.Label();
            tableStepInfo = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)ProcessDataGridView).BeginInit();
            uiContextMenuStrip1.SuspendLayout();
            uiPanel1.SuspendLayout();
            groupStepLog.SuspendLayout();
            groupStepInfo.SuspendLayout();
            tableStepInfo.SuspendLayout();
            SuspendLayout();
            // 
            // ToolTreeView
            // 
            ToolTreeView.BackColor = Color.Transparent;
            ToolTreeView.FillColor = Color.White;
            ToolTreeView.FillColor2 = Color.White;
            ToolTreeView.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ToolTreeView.Location = new Point(29, 87);
            ToolTreeView.Margin = new Padding(4, 5, 4, 5);
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
            uiLine1.Location = new Point(29, 50);
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
            dataGridViewCellStyle6.BackColor = Color.FromArgb(235, 243, 255);
            ProcessDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            ProcessDataGridView.BackgroundColor = Color.White;
            ProcessDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle7.Font = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            dataGridViewCellStyle7.ForeColor = Color.White;
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            ProcessDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            ProcessDataGridView.ColumnHeadersHeight = 40;
            ProcessDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ProcessDataGridView.Columns.AddRange(new DataGridViewColumn[] { ColProcessName, ColStepNum });
            ProcessDataGridView.ContextMenuStrip = uiContextMenuStrip1;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            ProcessDataGridView.DefaultCellStyle = dataGridViewCellStyle8;
            ProcessDataGridView.EnableHeadersVisualStyles = false;
            ProcessDataGridView.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            ProcessDataGridView.GridColor = Color.Black;
            ProcessDataGridView.Location = new Point(379, 87);
            ProcessDataGridView.Name = "ProcessDataGridView";
            ProcessDataGridView.ReadOnly = true;
            ProcessDataGridView.RectColor = Color.White;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle9.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle9.SelectionForeColor = Color.White;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            ProcessDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = Color.White;
            dataGridViewCellStyle10.Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ProcessDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle10;
            ProcessDataGridView.RowTemplate.Height = 35;
            ProcessDataGridView.SelectedIndex = -1;
            ProcessDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ProcessDataGridView.Size = new Size(345, 714);
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
            uiLine2.Location = new Point(379, 50);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(345, 29);
            uiLine2.TabIndex = 3;
            uiLine2.Text = "当前流程";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(btnExecute);
            uiPanel1.Controls.Add(BtnClose);
            uiPanel1.Controls.Add(btnSave);
            uiPanel1.FillColor = Color.White;
            uiPanel1.FillColor2 = Color.White;
            uiPanel1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiPanel1.Location = new Point(29, 809);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 10;
            uiPanel1.RectColor = Color.White;
            uiPanel1.RectDisableColor = Color.White;
            uiPanel1.Size = new Size(1045, 57);
            uiPanel1.TabIndex = 437;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnExecute
            // 
            btnExecute.Cursor = Cursors.Hand;
            btnExecute.FillColor = Color.DodgerBlue;
            btnExecute.FillColor2 = Color.DodgerBlue;
            btnExecute.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnExecute.LightColor = Color.FromArgb(248, 248, 248);
            btnExecute.Location = new Point(687, 12);
            btnExecute.MinimumSize = new Size(1, 1);
            btnExecute.Name = "btnExecute";
            btnExecute.RectColor = Color.DodgerBlue;
            btnExecute.RectDisableColor = Color.DodgerBlue;
            btnExecute.Size = new Size(147, 37);
            btnExecute.Style = UIStyle.Custom;
            btnExecute.Symbol = 61515;
            btnExecute.SymbolSize = 32;
            btnExecute.TabIndex = 442;
            btnExecute.Text = "执行";
            btnExecute.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnExecute.Click += btnExecute_Click;
            // 
            // BtnClose
            // 
            BtnClose.Cursor = Cursors.Hand;
            BtnClose.FillColor = Color.DodgerBlue;
            BtnClose.FillColor2 = Color.DodgerBlue;
            BtnClose.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            BtnClose.LightColor = Color.FromArgb(248, 248, 248);
            BtnClose.Location = new Point(414, 12);
            BtnClose.MinimumSize = new Size(1, 1);
            BtnClose.Name = "BtnClose";
            BtnClose.RectColor = Color.DodgerBlue;
            BtnClose.RectDisableColor = Color.DodgerBlue;
            BtnClose.Size = new Size(147, 37);
            BtnClose.Style = UIStyle.Custom;
            BtnClose.Symbol = 61457;
            BtnClose.SymbolSize = 32;
            BtnClose.TabIndex = 441;
            BtnClose.Text = "退 出";
            BtnClose.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            BtnClose.Click += BtnClose_Click;
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.FillColor = Color.DodgerBlue;
            btnSave.FillColor2 = Color.DodgerBlue;
            btnSave.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            btnSave.LightColor = Color.FromArgb(248, 248, 248);
            btnSave.Location = new Point(210, 12);
            btnSave.MinimumSize = new Size(1, 1);
            btnSave.Name = "btnSave";
            btnSave.RectColor = Color.DodgerBlue;
            btnSave.RectDisableColor = Color.DodgerBlue;
            btnSave.Size = new Size(147, 37);
            btnSave.Style = UIStyle.Custom;
            btnSave.Symbol = 61639;
            btnSave.SymbolSize = 32;
            btnSave.TabIndex = 440;
            btnSave.Text = "保 存";
            btnSave.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSave.Click += BtnSave_Click;
            // 
            // uiLine3
            // 
            uiLine3.BackColor = Color.Transparent;
            uiLine3.EndCap = UILineCap.Circle;
            uiLine3.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine3.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine3.LineColor = Color.White;
            uiLine3.Location = new Point(753, 50);
            uiLine3.MinimumSize = new Size(1, 1);
            uiLine3.Name = "uiLine3";
            uiLine3.Size = new Size(321, 29);
            uiLine3.TabIndex = 439;
            uiLine3.Text = "步骤执行详情";
            uiLine3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // groupStepLog
            // 
            groupStepLog.Controls.Add(txtExecutionLog);
            groupStepLog.FillColor = Color.White;
            groupStepLog.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            groupStepLog.ForeColor = Color.FromArgb(64, 64, 64);
            groupStepLog.Location = new Point(741, 309);
            groupStepLog.Margin = new Padding(4, 5, 4, 5);
            groupStepLog.MinimumSize = new Size(1, 1);
            groupStepLog.Name = "groupStepLog";
            groupStepLog.Padding = new Padding(10);
            groupStepLog.RectColor = Color.FromArgb(230, 230, 230);
            groupStepLog.ShowText = false;
            groupStepLog.Size = new Size(350, 492);
            groupStepLog.Style = UIStyle.Custom;
            groupStepLog.TabIndex = 441;
            groupStepLog.Text = "📝 执行日志";
            groupStepLog.TextAlignment = ContentAlignment.MiddleCenter;
            groupStepLog.TitleColor = Color.FromArgb(64, 64, 64);
            // 
            // txtExecutionLog
            // 
            txtExecutionLog.BackColor = Color.FromArgb(40, 40, 40);
            txtExecutionLog.BorderStyle = BorderStyle.None;
            txtExecutionLog.Font = new Font("Consolas", 9F);
            txtExecutionLog.ForeColor = Color.FromArgb(0, 255, 127);
            txtExecutionLog.Location = new Point(10, 41);
            txtExecutionLog.Name = "txtExecutionLog";
            txtExecutionLog.ReadOnly = true;
            txtExecutionLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtExecutionLog.Size = new Size(330, 438);
            txtExecutionLog.TabIndex = 0;
            txtExecutionLog.Text = "[系统] 等待选择步骤...";
            txtExecutionLog.WordWrap = false;
            // 
            // groupStepInfo
            // 
            groupStepInfo.Controls.Add(tableStepInfo);
            groupStepInfo.FillColor = Color.White;
            groupStepInfo.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            groupStepInfo.ForeColor = Color.FromArgb(64, 64, 64);
            groupStepInfo.Location = new Point(741, 87);
            groupStepInfo.Margin = new Padding(4, 5, 4, 5);
            groupStepInfo.MinimumSize = new Size(1, 1);
            groupStepInfo.Name = "groupStepInfo";
            groupStepInfo.Padding = new Padding(10);
            groupStepInfo.RectColor = Color.FromArgb(230, 230, 230);
            groupStepInfo.ShowText = false;
            groupStepInfo.Size = new Size(350, 215);
            groupStepInfo.Style = UIStyle.Custom;
            groupStepInfo.TabIndex = 440;
            groupStepInfo.Text = "📊 步骤详情";
            groupStepInfo.TextAlignment = ContentAlignment.MiddleCenter;
            groupStepInfo.TitleColor = Color.FromArgb(64, 64, 64);
            // 
            // lblExecTimeValue
            // 
            lblExecTimeValue.Dock = DockStyle.Fill;
            lblExecTimeValue.Font = new Font("Microsoft YaHei UI", 9F);
            lblExecTimeValue.ForeColor = Color.FromArgb(64, 64, 64);
            lblExecTimeValue.Location = new Point(93, 78);
            lblExecTimeValue.Name = "lblExecTimeValue";
            lblExecTimeValue.Size = new Size(234, 77);
            lblExecTimeValue.TabIndex = 8;
            lblExecTimeValue.Text = "--";
            // 
            // lblStatusValue
            // 
            lblStatusValue.Dock = DockStyle.Fill;
            lblStatusValue.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            lblStatusValue.ForeColor = Color.FromArgb(108, 117, 125);
            lblStatusValue.Location = new Point(93, 53);
            lblStatusValue.Name = "lblStatusValue";
            lblStatusValue.Size = new Size(234, 19);
            lblStatusValue.TabIndex = 7;
            lblStatusValue.Text = "未选择";
            // 
            // lblStepNameValue
            // 
            lblStepNameValue.Dock = DockStyle.Fill;
            lblStepNameValue.Font = new Font("Microsoft YaHei UI", 9F);
            lblStepNameValue.ForeColor = Color.FromArgb(64, 64, 64);
            lblStepNameValue.Location = new Point(93, 28);
            lblStepNameValue.Name = "lblStepNameValue";
            lblStepNameValue.Size = new Size(234, 19);
            lblStepNameValue.TabIndex = 6;
            lblStepNameValue.Text = "--";
            // 
            // lblStepNumberValue
            // 
            lblStepNumberValue.Dock = DockStyle.Fill;
            lblStepNumberValue.Font = new Font("Microsoft YaHei UI", 9F);
            lblStepNumberValue.ForeColor = Color.FromArgb(64, 64, 64);
            lblStepNumberValue.Location = new Point(93, 3);
            lblStepNumberValue.Name = "lblStepNumberValue";
            lblStepNumberValue.Size = new Size(234, 19);
            lblStepNumberValue.TabIndex = 5;
            lblStepNumberValue.Text = "--";
            // 
            // lblExecTimeTitle
            // 
            lblExecTimeTitle.Dock = DockStyle.Fill;
            lblExecTimeTitle.Font = new Font("Microsoft YaHei UI", 9F);
            lblExecTimeTitle.ForeColor = Color.FromArgb(128, 128, 128);
            lblExecTimeTitle.Location = new Point(3, 78);
            lblExecTimeTitle.Name = "lblExecTimeTitle";
            lblExecTimeTitle.Size = new Size(84, 77);
            lblExecTimeTitle.TabIndex = 3;
            lblExecTimeTitle.Text = "执行时间:";
            // 
            // lblStatusTitle
            // 
            lblStatusTitle.Dock = DockStyle.Fill;
            lblStatusTitle.Font = new Font("Microsoft YaHei UI", 9F);
            lblStatusTitle.ForeColor = Color.FromArgb(128, 128, 128);
            lblStatusTitle.Location = new Point(3, 53);
            lblStatusTitle.Name = "lblStatusTitle";
            lblStatusTitle.Size = new Size(84, 19);
            lblStatusTitle.TabIndex = 2;
            lblStatusTitle.Text = "当前状态:";
            // 
            // lblStepNameTitle
            // 
            lblStepNameTitle.Dock = DockStyle.Fill;
            lblStepNameTitle.Font = new Font("Microsoft YaHei UI", 9F);
            lblStepNameTitle.ForeColor = Color.FromArgb(128, 128, 128);
            lblStepNameTitle.Location = new Point(3, 28);
            lblStepNameTitle.Name = "lblStepNameTitle";
            lblStepNameTitle.Size = new Size(84, 19);
            lblStepNameTitle.TabIndex = 1;
            lblStepNameTitle.Text = "步骤名称:";
            // 
            // lblStepNumberTitle
            // 
            lblStepNumberTitle.Dock = DockStyle.Fill;
            lblStepNumberTitle.Font = new Font("Microsoft YaHei UI", 9F);
            lblStepNumberTitle.ForeColor = Color.FromArgb(128, 128, 128);
            lblStepNumberTitle.Location = new Point(3, 3);
            lblStepNumberTitle.Name = "lblStepNumberTitle";
            lblStepNumberTitle.Size = new Size(84, 19);
            lblStepNumberTitle.TabIndex = 0;
            lblStepNumberTitle.Text = "步骤编号:";
            // 
            // tableStepInfo
            // 
            tableStepInfo.ColumnCount = 2;
            tableStepInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tableStepInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableStepInfo.Controls.Add(lblStepNumberTitle, 0, 0);
            tableStepInfo.Controls.Add(lblStepNameTitle, 0, 1);
            tableStepInfo.Controls.Add(lblStatusTitle, 0, 2);
            tableStepInfo.Controls.Add(lblExecTimeTitle, 0, 3);
            tableStepInfo.Controls.Add(lblStepNumberValue, 1, 0);
            tableStepInfo.Controls.Add(lblStepNameValue, 1, 1);
            tableStepInfo.Controls.Add(lblStatusValue, 1, 2);
            tableStepInfo.Controls.Add(lblExecTimeValue, 1, 3);
            tableStepInfo.Dock = DockStyle.Bottom;
            tableStepInfo.Location = new Point(10, 47);
            tableStepInfo.Name = "tableStepInfo";
            tableStepInfo.RowCount = 4;
            tableStepInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableStepInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableStepInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableStepInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableStepInfo.Size = new Size(330, 158);
            tableStepInfo.TabIndex = 0;
            // 
            // FrmLogicalConfiguration
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(1102, 878);
            ControlBox = false;
            Controls.Add(groupStepLog);
            Controls.Add(groupStepInfo);
            Controls.Add(uiLine3);
            Controls.Add(uiPanel1);
            Controls.Add(uiLine2);
            Controls.Add(ProcessDataGridView);
            Controls.Add(uiLine1);
            Controls.Add(ToolTreeView);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLogicalConfiguration";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "试验逻辑配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 15F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            ((System.ComponentModel.ISupportInitialize)ProcessDataGridView).EndInit();
            uiContextMenuStrip1.ResumeLayout(false);
            uiPanel1.ResumeLayout(false);
            groupStepLog.ResumeLayout(false);
            groupStepInfo.ResumeLayout(false);
            tableStepInfo.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private UITreeView ToolTreeView;
        private UILine uiLine1;
        private UILine uiLine2;
        private UIDataGridView ProcessDataGridView;
        private DataGridViewTextBoxColumn ColProcessName;
        private UIPanel uiPanel1;
        private DataGridViewTextBoxColumn ColStepNum;
        private UIContextMenuStrip uiContextMenuStrip1;
        private ToolStripMenuItem toolDeleteStep;
        private UILine uiLine3;
        private UISymbolButton btnSave;
        private UISymbolButton BtnClose;
        private UISymbolButton btnExecute;
        private UITitlePanel groupStepLog;
        private RichTextBox txtExecutionLog;
        private UITitlePanel groupStepInfo;
        private TableLayoutPanel tableStepInfo;
        private AntdUI.Label lblStepNumberTitle;
        private AntdUI.Label lblStepNameTitle;
        private AntdUI.Label lblStatusTitle;
        private AntdUI.Label lblExecTimeTitle;
        private AntdUI.Label lblStepNumberValue;
        private AntdUI.Label lblStepNameValue;
        private AntdUI.Label lblStatusValue;
        private AntdUI.Label lblExecTimeValue;
    }
}