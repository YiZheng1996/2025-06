using AntdUI;
using Sunny.UI;
using System.Drawing;
using System.Windows.Forms;
using Panel = AntdUI.Panel;
using Splitter = System.Windows.Forms.Splitter;

namespace MainUI
{
    partial class Form1
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
            TreeNode treeNode1 = new TreeNode("变量定义");
            TreeNode treeNode2 = new TreeNode("变量赋值");
            TreeNode treeNode3 = new TreeNode("变量监控");
            TreeNode treeNode4 = new TreeNode("变量管理", new TreeNode[] { treeNode1, treeNode2, treeNode3 });
            TreeNode treeNode5 = new TreeNode("等待");
            TreeNode treeNode6 = new TreeNode("系统提示");
            TreeNode treeNode7 = new TreeNode("检测工具");
            TreeNode treeNode8 = new TreeNode("系统工具", new TreeNode[] { treeNode5, treeNode6, treeNode7 });
            TreeNode treeNode9 = new TreeNode("保存报表");
            TreeNode treeNode10 = new TreeNode("数据分析");
            TreeNode treeNode11 = new TreeNode("报表工具", new TreeNode[] { treeNode9, treeNode10 });
            TreeNode treeNode12 = new TreeNode("数学运算");
            TreeNode treeNode13 = new TreeNode("统计分析");
            TreeNode treeNode14 = new TreeNode("计算工具", new TreeNode[] { treeNode12, treeNode13 });
            panelMain = new Panel();
            panelContent = new Panel();
            panelCenter = new Panel();
            dgvProcess = new UIDataGridView();
            colStepNumber = new DataGridViewTextBoxColumn();
            colStepName = new DataGridViewTextBoxColumn();
            colStepStatus = new DataGridViewTextBoxColumn();
            colStepAction = new DataGridViewButtonColumn();
            panelCenterTop = new UITitlePanel();
            lblProcessStats = new AntdUI.Label();
            lblProcessTitle = new UILabel();
            splitterRight = new Splitter();
            panelRight = new Panel();
            groupStepLog = new UITitlePanel();
            txtExecutionLog = new RichTextBox();
            groupStepInfo = new UITitlePanel();
            tableStepInfo = new TableLayoutPanel();
            lblStepNumberTitle = new AntdUI.Label();
            lblStepNameTitle = new AntdUI.Label();
            lblStatusTitle = new AntdUI.Label();
            lblExecTimeTitle = new AntdUI.Label();
            lblCreateTimeTitle = new AntdUI.Label();
            lblStepNumberValue = new AntdUI.Label();
            lblStepNameValue = new AntdUI.Label();
            lblStatusValue = new AntdUI.Label();
            lblExecTimeValue = new AntdUI.Label();
            lblCreateTimeValue = new AntdUI.Label();
            splitterLeft = new Splitter();
            panelLeft = new Panel();
            tvToolbox = new UITreeView();
            panelLeftTop = new UITitlePanel();
            lblToolboxTitle = new UILabel();
            panelTop = new UITitlePanel();
            panelButtons = new Panel();
            btnClose = new AntdUI.Button();
            btnExecute = new AntdUI.Button();
            btnSave = new AntdUI.Button();
            lblTitle = new UILabel();
            contextMenuStep = new UIContextMenuStrip();
            menuItemEdit = new ToolStripMenuItem();
            menuItemDelete = new ToolStripMenuItem();
            panelMain.SuspendLayout();
            panelContent.SuspendLayout();
            panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProcess).BeginInit();
            panelCenterTop.SuspendLayout();
            panelRight.SuspendLayout();
            groupStepLog.SuspendLayout();
            groupStepInfo.SuspendLayout();
            tableStepInfo.SuspendLayout();
            panelLeft.SuspendLayout();
            panelLeftTop.SuspendLayout();
            panelTop.SuspendLayout();
            panelButtons.SuspendLayout();
            contextMenuStep.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(245, 245, 245);
            panelMain.Controls.Add(panelContent);
            panelMain.Controls.Add(panelTop);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1200, 800);
            panelMain.TabIndex = 0;
            // 
            // panelContent
            // 
            panelContent.Controls.Add(panelCenter);
            panelContent.Controls.Add(splitterRight);
            panelContent.Controls.Add(panelRight);
            panelContent.Controls.Add(splitterLeft);
            panelContent.Controls.Add(panelLeft);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 57);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(1200, 743);
            panelContent.TabIndex = 1;
            // 
            // panelCenter
            // 
            panelCenter.BackColor = Color.White;
            panelCenter.Controls.Add(dgvProcess);
            panelCenter.Controls.Add(panelCenterTop);
            panelCenter.Dock = DockStyle.Fill;
            panelCenter.Location = new Point(283, 0);
            panelCenter.Name = "panelCenter";
            panelCenter.Size = new Size(564, 743);
            panelCenter.TabIndex = 2;
            // 
            // dgvProcess
            // 
            dgvProcess.AllowUserToAddRows = false;
            dgvProcess.AllowUserToDeleteRows = false;
            dgvProcess.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            dgvProcess.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvProcess.BackgroundColor = Color.White;
            dgvProcess.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvProcess.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvProcess.ColumnHeadersHeight = 40;
            dgvProcess.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProcess.Columns.AddRange(new DataGridViewColumn[] { colStepNumber, colStepName, colStepStatus, colStepAction });
            dgvProcess.Dock = DockStyle.Fill;
            dgvProcess.EnableHeadersVisualStyles = false;
            dgvProcess.Font = new Font("Microsoft YaHei UI", 9F);
            dgvProcess.GridColor = Color.FromArgb(230, 230, 230);
            dgvProcess.Location = new Point(0, 45);
            dgvProcess.MultiSelect = false;
            dgvProcess.Name = "dgvProcess";
            dgvProcess.RectColor = Color.FromArgb(230, 230, 230);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle3.Font = new Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvProcess.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvProcess.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(248, 248, 248);
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvProcess.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvProcess.RowTemplate.Height = 35;
            dgvProcess.SelectedIndex = -1;
            dgvProcess.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProcess.Size = new Size(564, 698);
            dgvProcess.StripeEvenColor = Color.FromArgb(248, 248, 248);
            dgvProcess.StripeOddColor = Color.White;
            dgvProcess.Style = UIStyle.Custom;
            dgvProcess.TabIndex = 1;
            // 
            // colStepNumber
            // 
            colStepNumber.HeaderText = "步骤";
            colStepNumber.Name = "colStepNumber";
            colStepNumber.ReadOnly = true;
            colStepNumber.Width = 60;
            // 
            // colStepName
            // 
            colStepName.HeaderText = "操作名称";
            colStepName.Name = "colStepName";
            colStepName.ReadOnly = true;
            colStepName.Width = 250;
            // 
            // colStepStatus
            // 
            colStepStatus.HeaderText = "状态";
            colStepStatus.Name = "colStepStatus";
            colStepStatus.ReadOnly = true;
            // 
            // colStepAction
            // 
            colStepAction.HeaderText = "操作";
            colStepAction.Name = "colStepAction";
            colStepAction.ReadOnly = true;
            colStepAction.Text = "编辑";
            colStepAction.UseColumnTextForButtonValue = true;
            colStepAction.Width = 80;
            // 
            // panelCenterTop
            // 
            panelCenterTop.Controls.Add(lblProcessStats);
            panelCenterTop.Controls.Add(lblProcessTitle);
            panelCenterTop.Dock = DockStyle.Top;
            panelCenterTop.FillColor = Color.FromArgb(250, 250, 250);
            panelCenterTop.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            panelCenterTop.ForeColor = Color.FromArgb(64, 64, 64);
            panelCenterTop.Location = new Point(0, 0);
            panelCenterTop.Margin = new Padding(4, 5, 4, 5);
            panelCenterTop.MinimumSize = new Size(1, 1);
            panelCenterTop.Name = "panelCenterTop";
            panelCenterTop.Padding = new Padding(1, 19, 1, 1);
            panelCenterTop.RectColor = Color.FromArgb(230, 230, 230);
            panelCenterTop.ShowText = false;
            panelCenterTop.Size = new Size(564, 45);
            panelCenterTop.Style = UIStyle.Custom;
            panelCenterTop.TabIndex = 0;
            panelCenterTop.Text = null;
            panelCenterTop.TextAlignment = ContentAlignment.MiddleCenter;
            panelCenterTop.TitleColor = Color.FromArgb(64, 64, 64);
            panelCenterTop.TitleHeight = 19;
            // 
            // lblProcessStats
            // 
            lblProcessStats.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblProcessStats.Font = new Font("Microsoft YaHei UI", 9F);
            lblProcessStats.ForeColor = Color.FromArgb(128, 128, 128);
            lblProcessStats.Location = new Point(350, 20);
            lblProcessStats.Name = "lblProcessStats";
            lblProcessStats.Size = new Size(200, 17);
            lblProcessStats.TabIndex = 1;
            lblProcessStats.Text = "总步骤:0 | 已配置:0 | 待配置:0";
            // 
            // lblProcessTitle
            // 
            lblProcessTitle.AutoSize = true;
            lblProcessTitle.BackColor = Color.Transparent;
            lblProcessTitle.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            lblProcessTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblProcessTitle.Location = new Point(15, 20);
            lblProcessTitle.Name = "lblProcessTitle";
            lblProcessTitle.Size = new Size(134, 22);
            lblProcessTitle.Style = UIStyle.Custom;
            lblProcessTitle.TabIndex = 0;
            lblProcessTitle.Text = "📋 当前试验流程";
            lblProcessTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // splitterRight
            // 
            splitterRight.BackColor = Color.FromArgb(230, 230, 230);
            splitterRight.Dock = DockStyle.Right;
            splitterRight.Location = new Point(847, 0);
            splitterRight.Name = "splitterRight";
            splitterRight.Size = new Size(3, 743);
            splitterRight.TabIndex = 3;
            splitterRight.TabStop = false;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.White;
            panelRight.Controls.Add(groupStepLog);
            panelRight.Controls.Add(groupStepInfo);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(850, 0);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(350, 743);
            panelRight.TabIndex = 4;
            // 
            // groupStepLog
            // 
            groupStepLog.Controls.Add(txtExecutionLog);
            groupStepLog.Dock = DockStyle.Fill;
            groupStepLog.FillColor = Color.White;
            groupStepLog.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            groupStepLog.ForeColor = Color.FromArgb(64, 64, 64);
            groupStepLog.Location = new Point(0, 215);
            groupStepLog.Margin = new Padding(4, 5, 4, 5);
            groupStepLog.MinimumSize = new Size(1, 1);
            groupStepLog.Name = "groupStepLog";
            groupStepLog.Padding = new Padding(10);
            groupStepLog.RectColor = Color.FromArgb(230, 230, 230);
            groupStepLog.ShowText = false;
            groupStepLog.Size = new Size(350, 528);
            groupStepLog.Style = UIStyle.Custom;
            groupStepLog.TabIndex = 1;
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
            txtExecutionLog.Size = new Size(330, 509);
            txtExecutionLog.TabIndex = 0;
            txtExecutionLog.Text = "[系统] 等待选择步骤...";
            txtExecutionLog.WordWrap = false;
            // 
            // groupStepInfo
            // 
            groupStepInfo.Controls.Add(tableStepInfo);
            groupStepInfo.Dock = DockStyle.Top;
            groupStepInfo.FillColor = Color.White;
            groupStepInfo.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            groupStepInfo.ForeColor = Color.FromArgb(64, 64, 64);
            groupStepInfo.Location = new Point(0, 0);
            groupStepInfo.Margin = new Padding(4, 5, 4, 5);
            groupStepInfo.MinimumSize = new Size(1, 1);
            groupStepInfo.Name = "groupStepInfo";
            groupStepInfo.Padding = new Padding(10);
            groupStepInfo.RectColor = Color.FromArgb(230, 230, 230);
            groupStepInfo.ShowText = false;
            groupStepInfo.Size = new Size(350, 215);
            groupStepInfo.Style = UIStyle.Custom;
            groupStepInfo.TabIndex = 0;
            groupStepInfo.Text = "📊 步骤详情";
            groupStepInfo.TextAlignment = ContentAlignment.MiddleCenter;
            groupStepInfo.TitleColor = Color.FromArgb(64, 64, 64);
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
            tableStepInfo.Controls.Add(lblCreateTimeTitle, 0, 4);
            tableStepInfo.Controls.Add(lblStepNumberValue, 1, 0);
            tableStepInfo.Controls.Add(lblStepNameValue, 1, 1);
            tableStepInfo.Controls.Add(lblStatusValue, 1, 2);
            tableStepInfo.Controls.Add(lblExecTimeValue, 1, 3);
            tableStepInfo.Controls.Add(lblCreateTimeValue, 1, 4);
            tableStepInfo.Dock = DockStyle.Bottom;
            tableStepInfo.Location = new Point(10, 45);
            tableStepInfo.Name = "tableStepInfo";
            tableStepInfo.RowCount = 5;
            tableStepInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableStepInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableStepInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableStepInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableStepInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableStepInfo.Size = new Size(330, 160);
            tableStepInfo.TabIndex = 0;
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
            // lblExecTimeTitle
            // 
            lblExecTimeTitle.Dock = DockStyle.Fill;
            lblExecTimeTitle.Font = new Font("Microsoft YaHei UI", 9F);
            lblExecTimeTitle.ForeColor = Color.FromArgb(128, 128, 128);
            lblExecTimeTitle.Location = new Point(3, 78);
            lblExecTimeTitle.Name = "lblExecTimeTitle";
            lblExecTimeTitle.Size = new Size(84, 19);
            lblExecTimeTitle.TabIndex = 3;
            lblExecTimeTitle.Text = "执行时间:";
            // 
            // lblCreateTimeTitle
            // 
            lblCreateTimeTitle.Dock = DockStyle.Fill;
            lblCreateTimeTitle.Font = new Font("Microsoft YaHei UI", 9F);
            lblCreateTimeTitle.ForeColor = Color.FromArgb(128, 128, 128);
            lblCreateTimeTitle.Location = new Point(3, 103);
            lblCreateTimeTitle.Name = "lblCreateTimeTitle";
            lblCreateTimeTitle.Size = new Size(84, 54);
            lblCreateTimeTitle.TabIndex = 4;
            lblCreateTimeTitle.Text = "创建时间:";
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
            // lblExecTimeValue
            // 
            lblExecTimeValue.Dock = DockStyle.Fill;
            lblExecTimeValue.Font = new Font("Microsoft YaHei UI", 9F);
            lblExecTimeValue.ForeColor = Color.FromArgb(64, 64, 64);
            lblExecTimeValue.Location = new Point(93, 78);
            lblExecTimeValue.Name = "lblExecTimeValue";
            lblExecTimeValue.Size = new Size(234, 19);
            lblExecTimeValue.TabIndex = 8;
            lblExecTimeValue.Text = "--";
            // 
            // lblCreateTimeValue
            // 
            lblCreateTimeValue.Dock = DockStyle.Fill;
            lblCreateTimeValue.Font = new Font("Microsoft YaHei UI", 9F);
            lblCreateTimeValue.ForeColor = Color.FromArgb(64, 64, 64);
            lblCreateTimeValue.Location = new Point(93, 103);
            lblCreateTimeValue.Name = "lblCreateTimeValue";
            lblCreateTimeValue.Size = new Size(234, 54);
            lblCreateTimeValue.TabIndex = 9;
            lblCreateTimeValue.Text = "--";
            // 
            // splitterLeft
            // 
            splitterLeft.BackColor = Color.FromArgb(230, 230, 230);
            splitterLeft.Location = new Point(280, 0);
            splitterLeft.Name = "splitterLeft";
            splitterLeft.Size = new Size(3, 743);
            splitterLeft.TabIndex = 1;
            splitterLeft.TabStop = false;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.White;
            panelLeft.Controls.Add(tvToolbox);
            panelLeft.Controls.Add(panelLeftTop);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(280, 743);
            panelLeft.TabIndex = 0;
            // 
            // tvToolbox
            // 
            tvToolbox.BackColor = Color.White;
            tvToolbox.Dock = DockStyle.Fill;
            tvToolbox.FillColor = Color.White;
            tvToolbox.Font = new Font("Microsoft YaHei UI", 10F);
            tvToolbox.ForeColor = Color.FromArgb(64, 64, 64);
            tvToolbox.LineColor = Color.FromArgb(230, 230, 230);
            tvToolbox.Location = new Point(0, 45);
            tvToolbox.Margin = new Padding(4, 5, 4, 5);
            tvToolbox.MinimumSize = new Size(1, 1);
            tvToolbox.Name = "tvToolbox";
            treeNode1.Name = "";
            treeNode1.Text = "变量定义";
            treeNode2.Name = "";
            treeNode2.Text = "变量赋值";
            treeNode3.Name = "";
            treeNode3.Text = "变量监控";
            treeNode4.Name = "";
            treeNode4.Text = "变量管理";
            treeNode5.Name = "";
            treeNode5.Text = "等待";
            treeNode6.Name = "";
            treeNode6.Text = "系统提示";
            treeNode7.Name = "";
            treeNode7.Text = "检测工具";
            treeNode8.Name = "";
            treeNode8.Text = "系统工具";
            treeNode9.Name = "";
            treeNode9.Text = "保存报表";
            treeNode10.Name = "";
            treeNode10.Text = "数据分析";
            treeNode11.Name = "";
            treeNode11.Text = "报表工具";
            treeNode12.Name = "";
            treeNode12.Text = "数学运算";
            treeNode13.Name = "";
            treeNode13.Text = "统计分析";
            treeNode14.Name = "";
            treeNode14.Text = "计算工具";
            tvToolbox.Nodes.AddRange(new TreeNode[] { treeNode4, treeNode8, treeNode11, treeNode14 });
            tvToolbox.RectColor = Color.White;
            tvToolbox.ScrollBarStyleInherited = false;
            tvToolbox.SelectedColor = Color.FromArgb(230, 247, 255);
            tvToolbox.ShowText = false;
            tvToolbox.Size = new Size(280, 698);
            tvToolbox.Style = UIStyle.Custom;
            tvToolbox.TabIndex = 1;
            tvToolbox.Text = null;
            tvToolbox.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // panelLeftTop
            // 
            panelLeftTop.Controls.Add(lblToolboxTitle);
            panelLeftTop.Dock = DockStyle.Top;
            panelLeftTop.FillColor = Color.FromArgb(250, 250, 250);
            panelLeftTop.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            panelLeftTop.ForeColor = Color.FromArgb(64, 64, 64);
            panelLeftTop.Location = new Point(0, 0);
            panelLeftTop.Margin = new Padding(4, 5, 4, 5);
            panelLeftTop.MinimumSize = new Size(1, 1);
            panelLeftTop.Name = "panelLeftTop";
            panelLeftTop.Padding = new Padding(1, 19, 1, 1);
            panelLeftTop.RectColor = Color.FromArgb(230, 230, 230);
            panelLeftTop.ShowText = false;
            panelLeftTop.Size = new Size(280, 45);
            panelLeftTop.Style = UIStyle.Custom;
            panelLeftTop.TabIndex = 0;
            panelLeftTop.Text = null;
            panelLeftTop.TextAlignment = ContentAlignment.MiddleCenter;
            panelLeftTop.TitleColor = Color.FromArgb(64, 64, 64);
            panelLeftTop.TitleHeight = 19;
            // 
            // lblToolboxTitle
            // 
            lblToolboxTitle.AutoSize = true;
            lblToolboxTitle.BackColor = Color.Transparent;
            lblToolboxTitle.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            lblToolboxTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblToolboxTitle.Location = new Point(15, 20);
            lblToolboxTitle.Name = "lblToolboxTitle";
            lblToolboxTitle.Size = new Size(86, 22);
            lblToolboxTitle.Style = UIStyle.Custom;
            lblToolboxTitle.TabIndex = 0;
            lblToolboxTitle.Text = "\U0001f9f0 工具箱";
            lblToolboxTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(24, 144, 255);
            panelTop.Controls.Add(panelButtons);
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.FillColor = Color.FromArgb(24, 144, 255);
            panelTop.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            panelTop.ForeColor = Color.White;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(4, 5, 4, 5);
            panelTop.MinimumSize = new Size(1, 1);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(1, 19, 1, 1);
            panelTop.RectColor = Color.FromArgb(24, 144, 255);
            panelTop.ShowText = false;
            panelTop.Size = new Size(1200, 57);
            panelTop.Style = UIStyle.Custom;
            panelTop.TabIndex = 0;
            panelTop.Text = null;
            panelTop.TextAlignment = ContentAlignment.MiddleCenter;
            panelTop.TitleColor = Color.White;
            panelTop.TitleHeight = 19;
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.Transparent;
            panelButtons.Controls.Add(btnClose);
            panelButtons.Controls.Add(btnExecute);
            panelButtons.Controls.Add(btnSave);
            panelButtons.Dock = DockStyle.Right;
            panelButtons.Location = new Point(915, 19);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(284, 37);
            panelButtons.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(255, 77, 79);
            btnClose.Cursor = Cursors.Hand;
            btnClose.Font = new Font("Microsoft YaHei UI", 10F);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(195, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 36);
            btnClose.TabIndex = 2;
            btnClose.Text = "❌ 关闭";
            btnClose.Type = TTypeMini.Error;
            // 
            // btnExecute
            // 
            btnExecute.BackColor = Color.FromArgb(82, 196, 26);
            btnExecute.Cursor = Cursors.Hand;
            btnExecute.Font = new Font("Microsoft YaHei UI", 10F);
            btnExecute.ForeColor = Color.White;
            btnExecute.Location = new Point(105, 4);
            btnExecute.Name = "btnExecute";
            btnExecute.Size = new Size(80, 36);
            btnExecute.TabIndex = 1;
            btnExecute.Text = "▶️ 执行";
            btnExecute.Type = TTypeMini.Success;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(22, 119, 255);
            btnSave.Cursor = Cursors.Hand;
            btnSave.Font = new Font("Microsoft YaHei UI", 10F);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(19, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(80, 36);
            btnSave.TabIndex = 0;
            btnSave.Text = "💾 保存";
            btnSave.Type = TTypeMini.Primary;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(183, 30);
            lblTitle.Style = UIStyle.Custom;
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🔬 试验逻辑配置";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // contextMenuStep
            // 
            contextMenuStep.BackColor = Color.FromArgb(243, 249, 255);
            contextMenuStep.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            contextMenuStep.Items.AddRange(new ToolStripItem[] { menuItemEdit, menuItemDelete });
            contextMenuStep.Name = "contextMenuStep";
            contextMenuStep.Size = new Size(139, 48);
            // 
            // menuItemEdit
            // 
            menuItemEdit.Name = "menuItemEdit";
            menuItemEdit.Size = new Size(138, 22);
            menuItemEdit.Text = "编辑步骤";
            // 
            // menuItemDelete
            // 
            menuItemDelete.Name = "menuItemDelete";
            menuItemDelete.Size = new Size(138, 22);
            menuItemDelete.Text = "删除步骤";
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1200, 800);
            ControlBox = false;
            Controls.Add(panelMain);
            Font = new Font("Microsoft YaHei UI", 9F);
            MaximizeBox = false;
            Name = "Form1";
            WindowState = FormWindowState.Maximized;
            panelMain.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            panelCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProcess).EndInit();
            panelCenterTop.ResumeLayout(false);
            panelCenterTop.PerformLayout();
            panelRight.ResumeLayout(false);
            groupStepLog.ResumeLayout(false);
            groupStepInfo.ResumeLayout(false);
            tableStepInfo.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            panelLeftTop.ResumeLayout(false);
            panelLeftTop.PerformLayout();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelButtons.ResumeLayout(false);
            contextMenuStep.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        #region 控件声明

        private Panel panelMain;
        private UITitlePanel panelTop;
        private Panel panelButtons;
        private AntdUI.Button btnClose;
        private AntdUI.Button btnExecute;
        private AntdUI.Button btnSave;
        private UILabel lblTitle;
        private Panel panelContent;
        private Panel panelRight;
        private UITitlePanel groupStepLog;
        private RichTextBox txtExecutionLog;
        private UITitlePanel groupStepInfo;
        private TableLayoutPanel tableStepInfo;
        private AntdUI.Label lblStepNumberTitle;
        private AntdUI.Label lblStepNameTitle;
        private AntdUI.Label lblStatusTitle;
        private AntdUI.Label lblExecTimeTitle;
        private AntdUI.Label lblCreateTimeTitle;
        private AntdUI.Label lblStepNumberValue;
        private AntdUI.Label lblStepNameValue;
        private AntdUI.Label lblStatusValue;
        private AntdUI.Label lblExecTimeValue;
        private AntdUI.Label lblCreateTimeValue;
        private Splitter splitterRight;
        private Panel panelCenter;
        private UIDataGridView dgvProcess;
        private DataGridViewTextBoxColumn colStepNumber;
        private DataGridViewTextBoxColumn colStepName;
        private DataGridViewTextBoxColumn colStepStatus;
        private DataGridViewButtonColumn colStepAction;
        private UITitlePanel panelCenterTop;
        private UILabel lblProcessTitle;
        private AntdUI.Label lblProcessStats;
        private Splitter splitterLeft;
        private Panel panelLeft;
        private UITreeView tvToolbox;
        private UITitlePanel panelLeftTop;
        private UILabel lblToolboxTitle;
        private UIContextMenuStrip contextMenuStep;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;

        #endregion
    }
}