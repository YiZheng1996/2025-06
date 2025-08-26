using Sunny.UI;
namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    partial class Form_Detection
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
            lblDetectionName = new Label();
            txtDetectionName = new UITextBox();
            lblDetectionType = new Label();
            lblTimeout = new Label();
            lblRetryCount = new Label();
            lblRetryInterval = new Label();
            pnlPlcSource = new Panel();
            pnlExpressionSource = new Panel();
            txtExpression = new UITextBox();
            lblExpression = new Label();
            txtPlcAddress = new UITextBox();
            txtPlcModule = new UITextBox();
            lblPlcModule = new Label();
            lblPlcAddress = new Label();
            lblDataSourceType = new Label();
            pnlVariableSource = new Panel();
            txtVariableName = new UITextBox();
            lblVariableName = new Label();
            pnlRangeCondition = new Panel();
            numMaxValue = new AntdUI.InputNumber();
            numMinValue = new AntdUI.InputNumber();
            lblMinValue = new Label();
            lblMaxValue = new Label();
            pnlEqualityCondition = new Panel();
            numTolerance = new AntdUI.InputNumber();
            txtTargetValue = new UITextBox();
            lblTargetValue = new Label();
            lblTolerance = new Label();
            pnlThresholdCondition = new Panel();
            numThreshold = new AntdUI.InputNumber();
            lblOperator = new Label();
            cmbOperator = new UIComboBox();
            lblThreshold = new Label();
            pnlCustomCondition = new Panel();
            txtCustomExpression = new UITextBox();
            lblCustomExpression = new Label();
            grpResultHandling = new GroupBox();
            chkSaveResult = new CheckBox();
            lblResultVariable = new Label();
            txtResultVariable = new TextBox();
            chkSaveValue = new CheckBox();
            lblValueVariable = new Label();
            txtValueVariable = new TextBox();
            lblFailureAction = new Label();
            cmbFailureAction = new ComboBox();
            pnlJumpStep = new Panel();
            lblFailureStep = new Label();
            numFailureStep = new NumericUpDown();
            lblSuccessStep = new Label();
            numSuccessStep = new NumericUpDown();
            chkShowResult = new CheckBox();
            lblMessageTemplate = new Label();
            txtMessageTemplate = new TextBox();
            btnOK = new Button();
            btnCancel = new Button();
            btnTestDetection = new Button();
            grpBasicInfo = new UIPanel();
            numRetryInterval = new AntdUI.InputNumber();
            numRetryCount = new AntdUI.InputNumber();
            numTimeout = new AntdUI.InputNumber();
            cmbDetectionType = new UIComboBox();
            uiLine2 = new UILine();
            uiLine1 = new UILine();
            grpDataSource = new UIPanel();
            cmbDataSourceType = new UIComboBox();
            uiLine3 = new UILine();
            grpCondition = new UIPanel();
            pnlPlcSource.SuspendLayout();
            pnlExpressionSource.SuspendLayout();
            pnlVariableSource.SuspendLayout();
            pnlRangeCondition.SuspendLayout();
            pnlEqualityCondition.SuspendLayout();
            pnlThresholdCondition.SuspendLayout();
            pnlCustomCondition.SuspendLayout();
            grpResultHandling.SuspendLayout();
            pnlJumpStep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numFailureStep).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSuccessStep).BeginInit();
            grpBasicInfo.SuspendLayout();
            grpDataSource.SuspendLayout();
            grpCondition.SuspendLayout();
            SuspendLayout();
            // 
            // lblDetectionName
            // 
            lblDetectionName.AutoSize = true;
            lblDetectionName.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblDetectionName.Location = new Point(45, 19);
            lblDetectionName.Name = "lblDetectionName";
            lblDetectionName.Size = new Size(82, 23);
            lblDetectionName.TabIndex = 0;
            lblDetectionName.Text = "检测名称:";
            // 
            // txtDetectionName
            // 
            txtDetectionName.FillColor = Color.FromArgb(218, 220, 230);
            txtDetectionName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtDetectionName.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtDetectionName.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtDetectionName.Font = new Font("微软雅黑", 12.75F);
            txtDetectionName.Location = new Point(135, 17);
            txtDetectionName.Margin = new Padding(4, 5, 4, 5);
            txtDetectionName.MinimumSize = new Size(1, 16);
            txtDetectionName.Name = "txtDetectionName";
            txtDetectionName.Padding = new Padding(5);
            txtDetectionName.RectColor = Color.FromArgb(218, 220, 230);
            txtDetectionName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtDetectionName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtDetectionName.ShowText = false;
            txtDetectionName.Size = new Size(184, 30);
            txtDetectionName.TabIndex = 1;
            txtDetectionName.TextAlignment = ContentAlignment.MiddleLeft;
            txtDetectionName.Watermark = "请输入";
            txtDetectionName.WatermarkActiveColor = Color.FromArgb(48, 48, 48);
            txtDetectionName.WatermarkColor = Color.FromArgb(48, 48, 48);
            // 
            // lblDetectionType
            // 
            lblDetectionType.AutoSize = true;
            lblDetectionType.Font = new Font("微软雅黑", 12.75F);
            lblDetectionType.Location = new Point(402, 21);
            lblDetectionType.Name = "lblDetectionType";
            lblDetectionType.Size = new Size(82, 23);
            lblDetectionType.TabIndex = 2;
            lblDetectionType.Text = "检测类型:";
            // 
            // lblTimeout
            // 
            lblTimeout.AutoSize = true;
            lblTimeout.Font = new Font("微软雅黑", 12.75F);
            lblTimeout.Location = new Point(21, 73);
            lblTimeout.Name = "lblTimeout";
            lblTimeout.Size = new Size(118, 23);
            lblTimeout.TabIndex = 4;
            lblTimeout.Text = "超时时间(ms):";
            // 
            // lblRetryCount
            // 
            lblRetryCount.AutoSize = true;
            lblRetryCount.Font = new Font("微软雅黑", 12.75F);
            lblRetryCount.Location = new Point(270, 73);
            lblRetryCount.Name = "lblRetryCount";
            lblRetryCount.Size = new Size(82, 23);
            lblRetryCount.TabIndex = 6;
            lblRetryCount.Text = "重试次数:";
            // 
            // lblRetryInterval
            // 
            lblRetryInterval.AutoSize = true;
            lblRetryInterval.Font = new Font("微软雅黑", 12.75F);
            lblRetryInterval.Location = new Point(464, 73);
            lblRetryInterval.Name = "lblRetryInterval";
            lblRetryInterval.Size = new Size(118, 23);
            lblRetryInterval.TabIndex = 8;
            lblRetryInterval.Text = "重试间隔(ms):";
            // 
            // pnlPlcSource
            // 
            pnlPlcSource.Controls.Add(pnlExpressionSource);
            pnlPlcSource.Controls.Add(txtPlcAddress);
            pnlPlcSource.Controls.Add(txtPlcModule);
            pnlPlcSource.Controls.Add(lblPlcModule);
            pnlPlcSource.Controls.Add(lblPlcAddress);
            pnlPlcSource.Location = new Point(19, 58);
            pnlPlcSource.Name = "pnlPlcSource";
            pnlPlcSource.Size = new Size(679, 80);
            pnlPlcSource.TabIndex = 3;
            pnlPlcSource.Visible = false;
            // 
            // pnlExpressionSource
            // 
            pnlExpressionSource.Controls.Add(txtExpression);
            pnlExpressionSource.Controls.Add(lblExpression);
            pnlExpressionSource.Location = new Point(0, 0);
            pnlExpressionSource.Name = "pnlExpressionSource";
            pnlExpressionSource.Size = new Size(679, 80);
            pnlExpressionSource.TabIndex = 4;
            pnlExpressionSource.Visible = false;
            // 
            // txtExpression
            // 
            txtExpression.FillColor = Color.FromArgb(218, 220, 230);
            txtExpression.FillColor2 = Color.FromArgb(218, 220, 230);
            txtExpression.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtExpression.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtExpression.Font = new Font("微软雅黑", 12.75F);
            txtExpression.Location = new Point(88, 10);
            txtExpression.Margin = new Padding(4, 5, 4, 5);
            txtExpression.MinimumSize = new Size(1, 16);
            txtExpression.Name = "txtExpression";
            txtExpression.Padding = new Padding(5);
            txtExpression.RectColor = Color.FromArgb(218, 220, 230);
            txtExpression.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtExpression.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtExpression.ShowText = false;
            txtExpression.Size = new Size(400, 30);
            txtExpression.TabIndex = 6;
            txtExpression.TextAlignment = ContentAlignment.MiddleLeft;
            txtExpression.Watermark = "请输入";
            txtExpression.WatermarkActiveColor = Color.FromArgb(48, 48, 48);
            txtExpression.WatermarkColor = Color.FromArgb(48, 48, 48);
            // 
            // lblExpression
            // 
            lblExpression.AutoSize = true;
            lblExpression.Font = new Font("微软雅黑", 12.75F);
            lblExpression.Location = new Point(16, 13);
            lblExpression.Name = "lblExpression";
            lblExpression.Size = new Size(65, 23);
            lblExpression.TabIndex = 0;
            lblExpression.Text = "表达式:";
            // 
            // txtPlcAddress
            // 
            txtPlcAddress.FillColor = Color.FromArgb(218, 220, 230);
            txtPlcAddress.FillColor2 = Color.FromArgb(218, 220, 230);
            txtPlcAddress.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtPlcAddress.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPlcAddress.Font = new Font("微软雅黑", 12.75F);
            txtPlcAddress.Location = new Point(412, 10);
            txtPlcAddress.Margin = new Padding(4, 5, 4, 5);
            txtPlcAddress.MinimumSize = new Size(1, 16);
            txtPlcAddress.Name = "txtPlcAddress";
            txtPlcAddress.Padding = new Padding(5);
            txtPlcAddress.RectColor = Color.FromArgb(218, 220, 230);
            txtPlcAddress.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtPlcAddress.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPlcAddress.ShowText = false;
            txtPlcAddress.Size = new Size(184, 30);
            txtPlcAddress.TabIndex = 6;
            txtPlcAddress.TextAlignment = ContentAlignment.MiddleLeft;
            txtPlcAddress.Watermark = "请输入";
            txtPlcAddress.WatermarkActiveColor = Color.FromArgb(48, 48, 48);
            txtPlcAddress.WatermarkColor = Color.FromArgb(48, 48, 48);
            // 
            // txtPlcModule
            // 
            txtPlcModule.FillColor = Color.FromArgb(218, 220, 230);
            txtPlcModule.FillColor2 = Color.FromArgb(218, 220, 230);
            txtPlcModule.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtPlcModule.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPlcModule.Font = new Font("微软雅黑", 12.75F);
            txtPlcModule.Location = new Point(91, 10);
            txtPlcModule.Margin = new Padding(4, 5, 4, 5);
            txtPlcModule.MinimumSize = new Size(1, 16);
            txtPlcModule.Name = "txtPlcModule";
            txtPlcModule.Padding = new Padding(5);
            txtPlcModule.RectColor = Color.FromArgb(218, 220, 230);
            txtPlcModule.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtPlcModule.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtPlcModule.ShowText = false;
            txtPlcModule.Size = new Size(184, 30);
            txtPlcModule.TabIndex = 5;
            txtPlcModule.TextAlignment = ContentAlignment.MiddleLeft;
            txtPlcModule.Watermark = "请输入";
            txtPlcModule.WatermarkActiveColor = Color.FromArgb(48, 48, 48);
            txtPlcModule.WatermarkColor = Color.FromArgb(48, 48, 48);
            // 
            // lblPlcModule
            // 
            lblPlcModule.AutoSize = true;
            lblPlcModule.Font = new Font("微软雅黑", 12.75F);
            lblPlcModule.Location = new Point(6, 13);
            lblPlcModule.Name = "lblPlcModule";
            lblPlcModule.Size = new Size(78, 23);
            lblPlcModule.TabIndex = 0;
            lblPlcModule.Text = "PLC模块:";
            // 
            // lblPlcAddress
            // 
            lblPlcAddress.AutoSize = true;
            lblPlcAddress.Font = new Font("微软雅黑", 12.75F);
            lblPlcAddress.Location = new Point(331, 13);
            lblPlcAddress.Name = "lblPlcAddress";
            lblPlcAddress.Size = new Size(78, 23);
            lblPlcAddress.TabIndex = 2;
            lblPlcAddress.Text = "PLC地址:";
            // 
            // lblDataSourceType
            // 
            lblDataSourceType.AutoSize = true;
            lblDataSourceType.Font = new Font("微软雅黑", 12.75F);
            lblDataSourceType.Location = new Point(28, 16);
            lblDataSourceType.Name = "lblDataSourceType";
            lblDataSourceType.Size = new Size(99, 23);
            lblDataSourceType.TabIndex = 0;
            lblDataSourceType.Text = "数据源类型:";
            // 
            // pnlVariableSource
            // 
            pnlVariableSource.Controls.Add(txtVariableName);
            pnlVariableSource.Controls.Add(lblVariableName);
            pnlVariableSource.Location = new Point(19, 58);
            pnlVariableSource.Name = "pnlVariableSource";
            pnlVariableSource.Size = new Size(679, 80);
            pnlVariableSource.TabIndex = 2;
            // 
            // txtVariableName
            // 
            txtVariableName.FillColor = Color.FromArgb(218, 220, 230);
            txtVariableName.FillColor2 = Color.FromArgb(218, 220, 230);
            txtVariableName.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtVariableName.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtVariableName.Font = new Font("微软雅黑", 12.75F);
            txtVariableName.Location = new Point(78, 10);
            txtVariableName.Margin = new Padding(4, 5, 4, 5);
            txtVariableName.MinimumSize = new Size(1, 16);
            txtVariableName.Name = "txtVariableName";
            txtVariableName.Padding = new Padding(5);
            txtVariableName.RectColor = Color.FromArgb(218, 220, 230);
            txtVariableName.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtVariableName.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtVariableName.ShowText = false;
            txtVariableName.Size = new Size(184, 30);
            txtVariableName.TabIndex = 3;
            txtVariableName.TextAlignment = ContentAlignment.MiddleLeft;
            txtVariableName.Watermark = "请输入";
            txtVariableName.WatermarkActiveColor = Color.FromArgb(48, 48, 48);
            txtVariableName.WatermarkColor = Color.FromArgb(48, 48, 48);
            // 
            // lblVariableName
            // 
            lblVariableName.AutoSize = true;
            lblVariableName.Font = new Font("微软雅黑", 12.75F);
            lblVariableName.Location = new Point(6, 13);
            lblVariableName.Name = "lblVariableName";
            lblVariableName.Size = new Size(65, 23);
            lblVariableName.TabIndex = 0;
            lblVariableName.Text = "变量名:";
            // 
            // pnlRangeCondition
            // 
            pnlRangeCondition.Controls.Add(numMaxValue);
            pnlRangeCondition.Controls.Add(numMinValue);
            pnlRangeCondition.Controls.Add(lblMinValue);
            pnlRangeCondition.Controls.Add(lblMaxValue);
            pnlRangeCondition.Location = new Point(19, 11);
            pnlRangeCondition.Name = "pnlRangeCondition";
            pnlRangeCondition.Size = new Size(679, 110);
            pnlRangeCondition.TabIndex = 0;
            // 
            // numMaxValue
            // 
            numMaxValue.BackColor = Color.FromArgb(218, 220, 230);
            numMaxValue.Font = new Font("微软雅黑", 12.75F);
            numMaxValue.Location = new Point(288, 5);
            numMaxValue.Name = "numMaxValue";
            numMaxValue.SelectionStart = 1;
            numMaxValue.Size = new Size(109, 42);
            numMaxValue.TabIndex = 446;
            numMaxValue.Text = "0";
            // 
            // numMinValue
            // 
            numMinValue.BackColor = Color.FromArgb(218, 220, 230);
            numMinValue.Font = new Font("微软雅黑", 12.75F);
            numMinValue.Location = new Point(88, 5);
            numMinValue.Name = "numMinValue";
            numMinValue.SelectionStart = 1;
            numMinValue.Size = new Size(109, 42);
            numMinValue.TabIndex = 445;
            numMinValue.Text = "0";
            // 
            // lblMinValue
            // 
            lblMinValue.AutoSize = true;
            lblMinValue.Font = new Font("微软雅黑", 12.75F);
            lblMinValue.Location = new Point(15, 14);
            lblMinValue.Name = "lblMinValue";
            lblMinValue.Size = new Size(65, 23);
            lblMinValue.TabIndex = 0;
            lblMinValue.Text = "最小值:";
            // 
            // lblMaxValue
            // 
            lblMaxValue.AutoSize = true;
            lblMaxValue.Font = new Font("微软雅黑", 12.75F);
            lblMaxValue.Location = new Point(215, 14);
            lblMaxValue.Name = "lblMaxValue";
            lblMaxValue.Size = new Size(65, 23);
            lblMaxValue.TabIndex = 2;
            lblMaxValue.Text = "最大值:";
            // 
            // pnlEqualityCondition
            // 
            pnlEqualityCondition.Controls.Add(numTolerance);
            pnlEqualityCondition.Controls.Add(txtTargetValue);
            pnlEqualityCondition.Controls.Add(lblTargetValue);
            pnlEqualityCondition.Controls.Add(lblTolerance);
            pnlEqualityCondition.Location = new Point(19, 11);
            pnlEqualityCondition.Name = "pnlEqualityCondition";
            pnlEqualityCondition.Size = new Size(679, 110);
            pnlEqualityCondition.TabIndex = 1;
            pnlEqualityCondition.Visible = false;
            // 
            // numTolerance
            // 
            numTolerance.BackColor = Color.FromArgb(218, 220, 230);
            numTolerance.Font = new Font("微软雅黑", 12.75F);
            numTolerance.Location = new Point(331, 5);
            numTolerance.Name = "numTolerance";
            numTolerance.SelectionStart = 1;
            numTolerance.Size = new Size(91, 42);
            numTolerance.TabIndex = 446;
            numTolerance.Text = "0";
            numTolerance.Value = new decimal(new int[] { 100, 0, 0, 196608 });
            // 
            // txtTargetValue
            // 
            txtTargetValue.FillColor = Color.FromArgb(218, 220, 230);
            txtTargetValue.FillColor2 = Color.FromArgb(218, 220, 230);
            txtTargetValue.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtTargetValue.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtTargetValue.Font = new Font("微软雅黑", 12.75F);
            txtTargetValue.Location = new Point(87, 10);
            txtTargetValue.Margin = new Padding(4, 5, 4, 5);
            txtTargetValue.MinimumSize = new Size(1, 16);
            txtTargetValue.Name = "txtTargetValue";
            txtTargetValue.Padding = new Padding(5);
            txtTargetValue.RectColor = Color.FromArgb(218, 220, 230);
            txtTargetValue.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtTargetValue.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtTargetValue.ShowText = false;
            txtTargetValue.Size = new Size(163, 30);
            txtTargetValue.TabIndex = 4;
            txtTargetValue.TextAlignment = ContentAlignment.MiddleLeft;
            txtTargetValue.Watermark = "请输入";
            txtTargetValue.WatermarkActiveColor = Color.FromArgb(48, 48, 48);
            txtTargetValue.WatermarkColor = Color.FromArgb(48, 48, 48);
            // 
            // lblTargetValue
            // 
            lblTargetValue.AutoSize = true;
            lblTargetValue.Font = new Font("微软雅黑", 12.75F);
            lblTargetValue.Location = new Point(16, 13);
            lblTargetValue.Name = "lblTargetValue";
            lblTargetValue.Size = new Size(65, 23);
            lblTargetValue.TabIndex = 0;
            lblTargetValue.Text = "目标值:";
            // 
            // lblTolerance
            // 
            lblTolerance.AutoSize = true;
            lblTolerance.Font = new Font("微软雅黑", 12.75F);
            lblTolerance.Location = new Point(273, 13);
            lblTolerance.Name = "lblTolerance";
            lblTolerance.Size = new Size(48, 23);
            lblTolerance.TabIndex = 2;
            lblTolerance.Text = "容差:";
            // 
            // pnlThresholdCondition
            // 
            pnlThresholdCondition.Controls.Add(numThreshold);
            pnlThresholdCondition.Controls.Add(lblOperator);
            pnlThresholdCondition.Controls.Add(cmbOperator);
            pnlThresholdCondition.Controls.Add(lblThreshold);
            pnlThresholdCondition.Location = new Point(19, 11);
            pnlThresholdCondition.Name = "pnlThresholdCondition";
            pnlThresholdCondition.Size = new Size(679, 110);
            pnlThresholdCondition.TabIndex = 2;
            pnlThresholdCondition.Visible = false;
            // 
            // numThreshold
            // 
            numThreshold.BackColor = Color.FromArgb(218, 220, 230);
            numThreshold.Font = new Font("微软雅黑", 12.75F);
            numThreshold.Location = new Point(327, 5);
            numThreshold.Name = "numThreshold";
            numThreshold.SelectionStart = 1;
            numThreshold.Size = new Size(83, 42);
            numThreshold.TabIndex = 446;
            numThreshold.Text = "0";
            // 
            // lblOperator
            // 
            lblOperator.AutoSize = true;
            lblOperator.Font = new Font("微软雅黑", 12.75F);
            lblOperator.Location = new Point(12, 13);
            lblOperator.Name = "lblOperator";
            lblOperator.Size = new Size(82, 23);
            lblOperator.TabIndex = 0;
            lblOperator.Text = "比较操作:";
            // 
            // cmbOperator
            // 
            cmbOperator.BackColor = Color.Transparent;
            cmbOperator.DataSource = null;
            cmbOperator.FillColor = Color.FromArgb(218, 220, 230);
            cmbOperator.FillColor2 = Color.FromArgb(218, 220, 230);
            cmbOperator.FillDisableColor = Color.FromArgb(218, 220, 230);
            cmbOperator.FilterMaxCount = 50;
            cmbOperator.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cmbOperator.ForeDisableColor = Color.FromArgb(48, 48, 48);
            cmbOperator.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbOperator.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbOperator.Location = new Point(101, 10);
            cmbOperator.Margin = new Padding(4, 5, 4, 5);
            cmbOperator.MinimumSize = new Size(63, 0);
            cmbOperator.Name = "cmbOperator";
            cmbOperator.Padding = new Padding(0, 0, 30, 2);
            cmbOperator.Radius = 10;
            cmbOperator.RectColor = Color.Gray;
            cmbOperator.RectDisableColor = Color.Gray;
            cmbOperator.RectSides = ToolStripStatusLabelBorderSides.None;
            cmbOperator.Size = new Size(140, 30);
            cmbOperator.SymbolSize = 24;
            cmbOperator.TabIndex = 125;
            cmbOperator.TextAlignment = ContentAlignment.MiddleLeft;
            cmbOperator.Watermark = "请选择";
            cmbOperator.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            cmbOperator.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // lblThreshold
            // 
            lblThreshold.AutoSize = true;
            lblThreshold.Font = new Font("微软雅黑", 12.75F);
            lblThreshold.Location = new Point(271, 14);
            lblThreshold.Name = "lblThreshold";
            lblThreshold.Size = new Size(48, 23);
            lblThreshold.TabIndex = 2;
            lblThreshold.Text = "阈值:";
            // 
            // pnlCustomCondition
            // 
            pnlCustomCondition.Controls.Add(txtCustomExpression);
            pnlCustomCondition.Controls.Add(lblCustomExpression);
            pnlCustomCondition.Location = new Point(19, 11);
            pnlCustomCondition.Name = "pnlCustomCondition";
            pnlCustomCondition.Size = new Size(679, 110);
            pnlCustomCondition.TabIndex = 3;
            pnlCustomCondition.Visible = false;
            // 
            // txtCustomExpression
            // 
            txtCustomExpression.FillColor = Color.FromArgb(218, 220, 230);
            txtCustomExpression.FillColor2 = Color.FromArgb(218, 220, 230);
            txtCustomExpression.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtCustomExpression.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtCustomExpression.Font = new Font("微软雅黑", 12.75F);
            txtCustomExpression.Location = new Point(148, 10);
            txtCustomExpression.Margin = new Padding(4, 5, 4, 5);
            txtCustomExpression.MinimumSize = new Size(1, 16);
            txtCustomExpression.Name = "txtCustomExpression";
            txtCustomExpression.Padding = new Padding(5);
            txtCustomExpression.RectColor = Color.FromArgb(218, 220, 230);
            txtCustomExpression.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtCustomExpression.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtCustomExpression.ShowText = false;
            txtCustomExpression.Size = new Size(439, 30);
            txtCustomExpression.TabIndex = 7;
            txtCustomExpression.TextAlignment = ContentAlignment.MiddleLeft;
            txtCustomExpression.Watermark = "请输入";
            txtCustomExpression.WatermarkActiveColor = Color.FromArgb(48, 48, 48);
            txtCustomExpression.WatermarkColor = Color.FromArgb(48, 48, 48);
            // 
            // lblCustomExpression
            // 
            lblCustomExpression.AutoSize = true;
            lblCustomExpression.Font = new Font("微软雅黑", 12.75F);
            lblCustomExpression.Location = new Point(25, 13);
            lblCustomExpression.Name = "lblCustomExpression";
            lblCustomExpression.Size = new Size(116, 23);
            lblCustomExpression.TabIndex = 0;
            lblCustomExpression.Text = "自定义表达式:";
            // 
            // grpResultHandling
            // 
            grpResultHandling.Controls.Add(chkSaveResult);
            grpResultHandling.Controls.Add(lblResultVariable);
            grpResultHandling.Controls.Add(txtResultVariable);
            grpResultHandling.Controls.Add(chkSaveValue);
            grpResultHandling.Controls.Add(lblValueVariable);
            grpResultHandling.Controls.Add(txtValueVariable);
            grpResultHandling.Controls.Add(lblFailureAction);
            grpResultHandling.Controls.Add(cmbFailureAction);
            grpResultHandling.Controls.Add(pnlJumpStep);
            grpResultHandling.Controls.Add(chkShowResult);
            grpResultHandling.Controls.Add(lblMessageTemplate);
            grpResultHandling.Controls.Add(txtMessageTemplate);
            grpResultHandling.Location = new Point(13, 616);
            grpResultHandling.Name = "grpResultHandling";
            grpResultHandling.Size = new Size(707, 180);
            grpResultHandling.TabIndex = 30;
            grpResultHandling.TabStop = false;
            grpResultHandling.Text = "结果处理配置";
            // 
            // chkSaveResult
            // 
            chkSaveResult.AutoSize = true;
            chkSaveResult.Checked = true;
            chkSaveResult.CheckState = CheckState.Checked;
            chkSaveResult.Location = new Point(20, 28);
            chkSaveResult.Name = "chkSaveResult";
            chkSaveResult.Size = new Size(122, 20);
            chkSaveResult.TabIndex = 0;
            chkSaveResult.Text = "保存检测结果";
            chkSaveResult.UseVisualStyleBackColor = true;
            // 
            // lblResultVariable
            // 
            lblResultVariable.AutoSize = true;
            lblResultVariable.Location = new Point(150, 29);
            lblResultVariable.Name = "lblResultVariable";
            lblResultVariable.Size = new Size(79, 16);
            lblResultVariable.TabIndex = 1;
            lblResultVariable.Text = "结果变量:";
            // 
            // txtResultVariable
            // 
            txtResultVariable.Location = new Point(230, 26);
            txtResultVariable.Name = "txtResultVariable";
            txtResultVariable.Size = new Size(150, 26);
            txtResultVariable.TabIndex = 2;
            // 
            // chkSaveValue
            // 
            chkSaveValue.AutoSize = true;
            chkSaveValue.Location = new Point(20, 58);
            chkSaveValue.Name = "chkSaveValue";
            chkSaveValue.Size = new Size(106, 20);
            chkSaveValue.TabIndex = 3;
            chkSaveValue.Text = "保存检测值";
            chkSaveValue.UseVisualStyleBackColor = true;
            // 
            // lblValueVariable
            // 
            lblValueVariable.AutoSize = true;
            lblValueVariable.Location = new Point(150, 59);
            lblValueVariable.Name = "lblValueVariable";
            lblValueVariable.Size = new Size(63, 16);
            lblValueVariable.TabIndex = 4;
            lblValueVariable.Text = "值变量:";
            // 
            // txtValueVariable
            // 
            txtValueVariable.Location = new Point(230, 56);
            txtValueVariable.Name = "txtValueVariable";
            txtValueVariable.Size = new Size(150, 26);
            txtValueVariable.TabIndex = 5;
            // 
            // lblFailureAction
            // 
            lblFailureAction.AutoSize = true;
            lblFailureAction.Location = new Point(20, 89);
            lblFailureAction.Name = "lblFailureAction";
            lblFailureAction.Size = new Size(79, 16);
            lblFailureAction.TabIndex = 6;
            lblFailureAction.Text = "失败处理:";
            // 
            // cmbFailureAction
            // 
            cmbFailureAction.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFailureAction.FormattingEnabled = true;
            cmbFailureAction.Location = new Point(100, 86);
            cmbFailureAction.Name = "cmbFailureAction";
            cmbFailureAction.Size = new Size(120, 24);
            cmbFailureAction.TabIndex = 7;
            // 
            // pnlJumpStep
            // 
            pnlJumpStep.Controls.Add(lblFailureStep);
            pnlJumpStep.Controls.Add(numFailureStep);
            pnlJumpStep.Controls.Add(lblSuccessStep);
            pnlJumpStep.Controls.Add(numSuccessStep);
            pnlJumpStep.Location = new Point(240, 86);
            pnlJumpStep.Name = "pnlJumpStep";
            pnlJumpStep.Size = new Size(448, 42);
            pnlJumpStep.TabIndex = 8;
            pnlJumpStep.Visible = false;
            // 
            // lblFailureStep
            // 
            lblFailureStep.AutoSize = true;
            lblFailureStep.Location = new Point(17, 11);
            lblFailureStep.Name = "lblFailureStep";
            lblFailureStep.Size = new Size(79, 16);
            lblFailureStep.TabIndex = 0;
            lblFailureStep.Text = "失败跳转:";
            // 
            // numFailureStep
            // 
            numFailureStep.Location = new Point(97, 8);
            numFailureStep.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            numFailureStep.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numFailureStep.Name = "numFailureStep";
            numFailureStep.Size = new Size(80, 26);
            numFailureStep.TabIndex = 1;
            numFailureStep.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // lblSuccessStep
            // 
            lblSuccessStep.AutoSize = true;
            lblSuccessStep.Location = new Point(197, 11);
            lblSuccessStep.Name = "lblSuccessStep";
            lblSuccessStep.Size = new Size(79, 16);
            lblSuccessStep.TabIndex = 2;
            lblSuccessStep.Text = "成功跳转:";
            // 
            // numSuccessStep
            // 
            numSuccessStep.Location = new Point(277, 8);
            numSuccessStep.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            numSuccessStep.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numSuccessStep.Name = "numSuccessStep";
            numSuccessStep.Size = new Size(80, 26);
            numSuccessStep.TabIndex = 3;
            numSuccessStep.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // chkShowResult
            // 
            chkShowResult.AutoSize = true;
            chkShowResult.Checked = true;
            chkShowResult.CheckState = CheckState.Checked;
            chkShowResult.Location = new Point(20, 119);
            chkShowResult.Name = "chkShowResult";
            chkShowResult.Size = new Size(122, 20);
            chkShowResult.TabIndex = 9;
            chkShowResult.Text = "显示检测结果";
            chkShowResult.UseVisualStyleBackColor = true;
            // 
            // lblMessageTemplate
            // 
            lblMessageTemplate.AutoSize = true;
            lblMessageTemplate.Location = new Point(20, 149);
            lblMessageTemplate.Name = "lblMessageTemplate";
            lblMessageTemplate.Size = new Size(79, 16);
            lblMessageTemplate.TabIndex = 10;
            lblMessageTemplate.Text = "消息模板:";
            // 
            // txtMessageTemplate
            // 
            txtMessageTemplate.Location = new Point(110, 146);
            txtMessageTemplate.Name = "txtMessageTemplate";
            txtMessageTemplate.Size = new Size(400, 26);
            txtMessageTemplate.TabIndex = 11;
            txtMessageTemplate.Text = "检测项 {DetectionName}: {Result}";
            // 
            // btnOK
            // 
            btnOK.Location = new Point(550, 806);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(80, 30);
            btnOK.TabIndex = 40;
            btnOK.Text = "确定";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(640, 806);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 30);
            btnCancel.TabIndex = 41;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnTestDetection
            // 
            btnTestDetection.Location = new Point(450, 806);
            btnTestDetection.Name = "btnTestDetection";
            btnTestDetection.Size = new Size(90, 30);
            btnTestDetection.TabIndex = 42;
            btnTestDetection.Text = "测试检测";
            btnTestDetection.UseVisualStyleBackColor = true;
            // 
            // grpBasicInfo
            // 
            grpBasicInfo.BackColor = Color.Transparent;
            grpBasicInfo.Controls.Add(numRetryInterval);
            grpBasicInfo.Controls.Add(numRetryCount);
            grpBasicInfo.Controls.Add(numTimeout);
            grpBasicInfo.Controls.Add(cmbDetectionType);
            grpBasicInfo.Controls.Add(lblDetectionName);
            grpBasicInfo.Controls.Add(txtDetectionName);
            grpBasicInfo.Controls.Add(lblRetryInterval);
            grpBasicInfo.Controls.Add(lblDetectionType);
            grpBasicInfo.Controls.Add(lblRetryCount);
            grpBasicInfo.Controls.Add(lblTimeout);
            grpBasicInfo.FillColor = Color.White;
            grpBasicInfo.FillColor2 = Color.White;
            grpBasicInfo.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpBasicInfo.Location = new Point(13, 74);
            grpBasicInfo.Margin = new Padding(4, 5, 4, 5);
            grpBasicInfo.MinimumSize = new Size(1, 1);
            grpBasicInfo.Name = "grpBasicInfo";
            grpBasicInfo.Radius = 10;
            grpBasicInfo.RectColor = Color.White;
            grpBasicInfo.RectDisableColor = Color.White;
            grpBasicInfo.Size = new Size(707, 119);
            grpBasicInfo.TabIndex = 43;
            grpBasicInfo.Text = null;
            grpBasicInfo.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // numRetryInterval
            // 
            numRetryInterval.BackColor = Color.FromArgb(218, 220, 230);
            numRetryInterval.Font = new Font("微软雅黑", 12.75F);
            numRetryInterval.Location = new Point(588, 66);
            numRetryInterval.Name = "numRetryInterval";
            numRetryInterval.SelectionStart = 4;
            numRetryInterval.Size = new Size(85, 42);
            numRetryInterval.TabIndex = 446;
            numRetryInterval.Text = "5000";
            numRetryInterval.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            // 
            // numRetryCount
            // 
            numRetryCount.BackColor = Color.FromArgb(218, 220, 230);
            numRetryCount.Font = new Font("微软雅黑", 12.75F);
            numRetryCount.Location = new Point(358, 66);
            numRetryCount.Name = "numRetryCount";
            numRetryCount.SelectionStart = 1;
            numRetryCount.Size = new Size(83, 42);
            numRetryCount.TabIndex = 445;
            numRetryCount.Text = "0";
            // 
            // numTimeout
            // 
            numTimeout.BackColor = Color.FromArgb(218, 220, 230);
            numTimeout.Font = new Font("微软雅黑", 12.75F);
            numTimeout.Location = new Point(146, 66);
            numTimeout.Name = "numTimeout";
            numTimeout.SelectionStart = 5;
            numTimeout.Size = new Size(109, 42);
            numTimeout.TabIndex = 444;
            numTimeout.Text = "30000";
            numTimeout.Value = new decimal(new int[] { 30000, 0, 0, 0 });
            // 
            // cmbDetectionType
            // 
            cmbDetectionType.BackColor = Color.Transparent;
            cmbDetectionType.DataSource = null;
            cmbDetectionType.FillColor = Color.FromArgb(218, 220, 230);
            cmbDetectionType.FillColor2 = Color.FromArgb(218, 220, 230);
            cmbDetectionType.FillDisableColor = Color.FromArgb(218, 220, 230);
            cmbDetectionType.FilterMaxCount = 50;
            cmbDetectionType.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cmbDetectionType.ForeDisableColor = Color.FromArgb(48, 48, 48);
            cmbDetectionType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbDetectionType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbDetectionType.Location = new Point(489, 17);
            cmbDetectionType.Margin = new Padding(4, 5, 4, 5);
            cmbDetectionType.MinimumSize = new Size(63, 0);
            cmbDetectionType.Name = "cmbDetectionType";
            cmbDetectionType.Padding = new Padding(0, 0, 30, 2);
            cmbDetectionType.Radius = 10;
            cmbDetectionType.RectColor = Color.Gray;
            cmbDetectionType.RectDisableColor = Color.Gray;
            cmbDetectionType.RectSides = ToolStripStatusLabelBorderSides.None;
            cmbDetectionType.Size = new Size(184, 30);
            cmbDetectionType.SymbolSize = 24;
            cmbDetectionType.TabIndex = 123;
            cmbDetectionType.TextAlignment = ContentAlignment.MiddleLeft;
            cmbDetectionType.Watermark = "请选择";
            cmbDetectionType.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            cmbDetectionType.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // uiLine2
            // 
            uiLine2.BackColor = Color.Transparent;
            uiLine2.EndCap = UILineCap.Circle;
            uiLine2.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine2.LineColor = Color.White;
            uiLine2.Location = new Point(3, 38);
            uiLine2.MinimumSize = new Size(1, 1);
            uiLine2.Name = "uiLine2";
            uiLine2.Size = new Size(717, 29);
            uiLine2.TabIndex = 443;
            uiLine2.Text = "检测选择";
            uiLine2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.EndCap = UILineCap.Circle;
            uiLine1.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.White;
            uiLine1.Location = new Point(3, 217);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(717, 29);
            uiLine1.TabIndex = 445;
            uiLine1.Text = "数据源配置";
            uiLine1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpDataSource
            // 
            grpDataSource.BackColor = Color.Transparent;
            grpDataSource.Controls.Add(pnlPlcSource);
            grpDataSource.Controls.Add(pnlVariableSource);
            grpDataSource.Controls.Add(cmbDataSourceType);
            grpDataSource.Controls.Add(lblDataSourceType);
            grpDataSource.FillColor = Color.White;
            grpDataSource.FillColor2 = Color.White;
            grpDataSource.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpDataSource.Location = new Point(13, 253);
            grpDataSource.Margin = new Padding(4, 5, 4, 5);
            grpDataSource.MinimumSize = new Size(1, 1);
            grpDataSource.Name = "grpDataSource";
            grpDataSource.Radius = 10;
            grpDataSource.RectColor = Color.White;
            grpDataSource.RectDisableColor = Color.White;
            grpDataSource.Size = new Size(707, 151);
            grpDataSource.TabIndex = 444;
            grpDataSource.Text = null;
            grpDataSource.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // cmbDataSourceType
            // 
            cmbDataSourceType.BackColor = Color.Transparent;
            cmbDataSourceType.DataSource = null;
            cmbDataSourceType.FillColor = Color.FromArgb(218, 220, 230);
            cmbDataSourceType.FillColor2 = Color.FromArgb(218, 220, 230);
            cmbDataSourceType.FillDisableColor = Color.FromArgb(218, 220, 230);
            cmbDataSourceType.FilterMaxCount = 50;
            cmbDataSourceType.Font = new Font("微软雅黑", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cmbDataSourceType.ForeDisableColor = Color.FromArgb(48, 48, 48);
            cmbDataSourceType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmbDataSourceType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cmbDataSourceType.Location = new Point(135, 11);
            cmbDataSourceType.Margin = new Padding(4, 5, 4, 5);
            cmbDataSourceType.MinimumSize = new Size(63, 0);
            cmbDataSourceType.Name = "cmbDataSourceType";
            cmbDataSourceType.Padding = new Padding(0, 0, 30, 2);
            cmbDataSourceType.Radius = 10;
            cmbDataSourceType.RectColor = Color.Gray;
            cmbDataSourceType.RectDisableColor = Color.Gray;
            cmbDataSourceType.RectSides = ToolStripStatusLabelBorderSides.None;
            cmbDataSourceType.Size = new Size(184, 30);
            cmbDataSourceType.SymbolSize = 24;
            cmbDataSourceType.TabIndex = 124;
            cmbDataSourceType.TextAlignment = ContentAlignment.MiddleLeft;
            cmbDataSourceType.Watermark = "请选择";
            cmbDataSourceType.WatermarkActiveColor = Color.FromArgb(64, 64, 64);
            cmbDataSourceType.WatermarkColor = Color.FromArgb(64, 64, 64);
            // 
            // uiLine3
            // 
            uiLine3.BackColor = Color.Transparent;
            uiLine3.EndCap = UILineCap.Circle;
            uiLine3.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiLine3.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine3.LineColor = Color.White;
            uiLine3.Location = new Point(3, 425);
            uiLine3.MinimumSize = new Size(1, 1);
            uiLine3.Name = "uiLine3";
            uiLine3.Size = new Size(717, 29);
            uiLine3.TabIndex = 447;
            uiLine3.Text = "检测条件配置";
            uiLine3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpCondition
            // 
            grpCondition.BackColor = Color.Transparent;
            grpCondition.Controls.Add(pnlEqualityCondition);
            grpCondition.Controls.Add(pnlRangeCondition);
            grpCondition.Controls.Add(pnlCustomCondition);
            grpCondition.Controls.Add(pnlThresholdCondition);
            grpCondition.FillColor = Color.White;
            grpCondition.FillColor2 = Color.White;
            grpCondition.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpCondition.Location = new Point(13, 461);
            grpCondition.Margin = new Padding(4, 5, 4, 5);
            grpCondition.MinimumSize = new Size(1, 1);
            grpCondition.Name = "grpCondition";
            grpCondition.Radius = 10;
            grpCondition.RectColor = Color.White;
            grpCondition.RectDisableColor = Color.White;
            grpCondition.Size = new Size(707, 127);
            grpCondition.TabIndex = 446;
            grpCondition.Text = null;
            grpCondition.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // Form_Detection
            // 
            AcceptButton = btnOK;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            CancelButton = btnCancel;
            ClientSize = new Size(734, 845);
            Controls.Add(uiLine3);
            Controls.Add(grpCondition);
            Controls.Add(uiLine1);
            Controls.Add(grpDataSource);
            Controls.Add(uiLine2);
            Controls.Add(grpBasicInfo);
            Controls.Add(btnTestDetection);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(grpResultHandling);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_Detection";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "检测工具配置";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14.25F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 784, 691);
            pnlPlcSource.ResumeLayout(false);
            pnlPlcSource.PerformLayout();
            pnlExpressionSource.ResumeLayout(false);
            pnlExpressionSource.PerformLayout();
            pnlVariableSource.ResumeLayout(false);
            pnlVariableSource.PerformLayout();
            pnlRangeCondition.ResumeLayout(false);
            pnlRangeCondition.PerformLayout();
            pnlEqualityCondition.ResumeLayout(false);
            pnlEqualityCondition.PerformLayout();
            pnlThresholdCondition.ResumeLayout(false);
            pnlThresholdCondition.PerformLayout();
            pnlCustomCondition.ResumeLayout(false);
            pnlCustomCondition.PerformLayout();
            grpResultHandling.ResumeLayout(false);
            grpResultHandling.PerformLayout();
            pnlJumpStep.ResumeLayout(false);
            pnlJumpStep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numFailureStep).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSuccessStep).EndInit();
            grpBasicInfo.ResumeLayout(false);
            grpBasicInfo.PerformLayout();
            grpDataSource.ResumeLayout(false);
            grpDataSource.PerformLayout();
            grpCondition.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblDetectionName;
        private UITextBox txtDetectionName;
        private System.Windows.Forms.Label lblDetectionType;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.Label lblRetryCount;
        private System.Windows.Forms.Label lblRetryInterval;
        private System.Windows.Forms.Label lblDataSourceType;
        private System.Windows.Forms.Panel pnlVariableSource;
        private System.Windows.Forms.Label lblVariableName;
        private System.Windows.Forms.Panel pnlPlcSource;
        private System.Windows.Forms.Label lblPlcModule;
        private System.Windows.Forms.Label lblPlcAddress;
        private System.Windows.Forms.Panel pnlExpressionSource;
        private System.Windows.Forms.Label lblExpression;
        private System.Windows.Forms.Panel pnlRangeCondition;
        private System.Windows.Forms.Label lblMinValue;
        private System.Windows.Forms.Label lblMaxValue;
        private System.Windows.Forms.Panel pnlEqualityCondition;
        private System.Windows.Forms.Label lblTargetValue;
        private System.Windows.Forms.Label lblTolerance;
        private System.Windows.Forms.Panel pnlThresholdCondition;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.Label lblThreshold;
        private System.Windows.Forms.Panel pnlCustomCondition;
        private System.Windows.Forms.Label lblCustomExpression;

        private System.Windows.Forms.GroupBox grpResultHandling;
        private System.Windows.Forms.CheckBox chkSaveResult;
        private System.Windows.Forms.Label lblResultVariable;
        private System.Windows.Forms.TextBox txtResultVariable;
        private System.Windows.Forms.CheckBox chkSaveValue;
        private System.Windows.Forms.Label lblValueVariable;
        private System.Windows.Forms.TextBox txtValueVariable;
        private System.Windows.Forms.Label lblFailureAction;
        private System.Windows.Forms.ComboBox cmbFailureAction;
        private System.Windows.Forms.Panel pnlJumpStep;
        private System.Windows.Forms.Label lblFailureStep;
        private System.Windows.Forms.NumericUpDown numFailureStep;
        private System.Windows.Forms.Label lblSuccessStep;
        private System.Windows.Forms.NumericUpDown numSuccessStep;
        private System.Windows.Forms.CheckBox chkShowResult;
        private System.Windows.Forms.Label lblMessageTemplate;
        private System.Windows.Forms.TextBox txtMessageTemplate;

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnTestDetection;
        private UIPanel grpBasicInfo;
        private UIComboBox cmbDetectionType;
        private UILine uiLine2;
        private AntdUI.InputNumber numTimeout;
        private AntdUI.InputNumber numRetryCount;
        private AntdUI.InputNumber numRetryInterval;
        private UILine uiLine1;
        private UIPanel grpDataSource;
        private UITextBox txtVariableName;
        private UIComboBox cmbDataSourceType;
        private UITextBox txtPlcModule;
        private UITextBox txtPlcAddress;
        private UITextBox txtExpression;
        private AntdUI.InputNumber numMinValue;
        private UILine uiLine3;
        private UIPanel grpCondition;
        private AntdUI.InputNumber numMaxValue;
        private UITextBox txtTargetValue;
        private AntdUI.InputNumber numTolerance;
        private UIComboBox cmbOperator;
        private AntdUI.InputNumber numThreshold;
        private UITextBox txtCustomExpression;
    }
}