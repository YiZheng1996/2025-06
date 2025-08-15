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
            grpBasicInfo = new GroupBox();
            lblDetectionName = new Label();
            txtDetectionName = new TextBox();
            lblDetectionType = new Label();
            cmbDetectionType = new ComboBox();
            lblTimeout = new Label();
            numTimeout = new NumericUpDown();
            lblRetryCount = new Label();
            numRetryCount = new NumericUpDown();
            lblRetryInterval = new Label();
            numRetryInterval = new NumericUpDown();
            grpDataSource = new GroupBox();
            lblDataSourceType = new Label();
            cmbDataSourceType = new ComboBox();
            pnlVariableSource = new Panel();
            lblVariableName = new Label();
            txtVariableName = new TextBox();
            pnlPlcSource = new Panel();
            lblPlcModule = new Label();
            txtPlcModule = new TextBox();
            lblPlcAddress = new Label();
            txtPlcAddress = new TextBox();
            lblDataType = new Label();
            txtDataType = new TextBox();
            pnlExpressionSource = new Panel();
            lblExpression = new Label();
            txtExpression = new TextBox();
            grpCondition = new GroupBox();
            pnlRangeCondition = new Panel();
            lblMinValue = new Label();
            numMinValue = new NumericUpDown();
            lblMaxValue = new Label();
            numMaxValue = new NumericUpDown();
            pnlEqualityCondition = new Panel();
            lblTargetValue = new Label();
            txtTargetValue = new TextBox();
            lblTolerance = new Label();
            numTolerance = new NumericUpDown();
            pnlThresholdCondition = new Panel();
            lblOperator = new Label();
            cmbOperator = new ComboBox();
            lblThreshold = new Label();
            numThreshold = new NumericUpDown();
            pnlCustomCondition = new Panel();
            lblCustomExpression = new Label();
            txtCustomExpression = new TextBox();
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
            grpBasicInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTimeout).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRetryCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRetryInterval).BeginInit();
            grpDataSource.SuspendLayout();
            pnlVariableSource.SuspendLayout();
            pnlPlcSource.SuspendLayout();
            pnlExpressionSource.SuspendLayout();
            grpCondition.SuspendLayout();
            pnlRangeCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMinValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxValue).BeginInit();
            pnlEqualityCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTolerance).BeginInit();
            pnlThresholdCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numThreshold).BeginInit();
            pnlCustomCondition.SuspendLayout();
            grpResultHandling.SuspendLayout();
            pnlJumpStep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numFailureStep).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSuccessStep).BeginInit();
            SuspendLayout();
            // 
            // grpBasicInfo
            // 
            grpBasicInfo.Controls.Add(lblDetectionName);
            grpBasicInfo.Controls.Add(txtDetectionName);
            grpBasicInfo.Controls.Add(lblDetectionType);
            grpBasicInfo.Controls.Add(cmbDetectionType);
            grpBasicInfo.Controls.Add(lblTimeout);
            grpBasicInfo.Controls.Add(numTimeout);
            grpBasicInfo.Controls.Add(lblRetryCount);
            grpBasicInfo.Controls.Add(numRetryCount);
            grpBasicInfo.Controls.Add(lblRetryInterval);
            grpBasicInfo.Controls.Add(numRetryInterval);
            grpBasicInfo.Location = new Point(12, 40);
            grpBasicInfo.Name = "grpBasicInfo";
            grpBasicInfo.Size = new Size(760, 120);
            grpBasicInfo.TabIndex = 0;
            grpBasicInfo.TabStop = false;
            grpBasicInfo.Text = "基本信息";
            // 
            // lblDetectionName
            // 
            lblDetectionName.AutoSize = true;
            lblDetectionName.Location = new Point(20, 28);
            lblDetectionName.Name = "lblDetectionName";
            lblDetectionName.Size = new Size(79, 16);
            lblDetectionName.TabIndex = 0;
            lblDetectionName.Text = "检测名称:";
            // 
            // txtDetectionName
            // 
            txtDetectionName.Location = new Point(110, 25);
            txtDetectionName.Name = "txtDetectionName";
            txtDetectionName.Size = new Size(200, 26);
            txtDetectionName.TabIndex = 1;
            // 
            // lblDetectionType
            // 
            lblDetectionType.AutoSize = true;
            lblDetectionType.Location = new Point(330, 28);
            lblDetectionType.Name = "lblDetectionType";
            lblDetectionType.Size = new Size(79, 16);
            lblDetectionType.TabIndex = 2;
            lblDetectionType.Text = "检测类型:";
            // 
            // cmbDetectionType
            // 
            cmbDetectionType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDetectionType.FormattingEnabled = true;
            cmbDetectionType.Location = new Point(420, 25);
            cmbDetectionType.Name = "cmbDetectionType";
            cmbDetectionType.Size = new Size(150, 24);
            cmbDetectionType.TabIndex = 3;
            // 
            // lblTimeout
            // 
            lblTimeout.AutoSize = true;
            lblTimeout.Location = new Point(20, 76);
            lblTimeout.Name = "lblTimeout";
            lblTimeout.Size = new Size(111, 16);
            lblTimeout.TabIndex = 4;
            lblTimeout.Text = "超时时间(ms):";
            // 
            // numTimeout
            // 
            numTimeout.Location = new Point(133, 73);
            numTimeout.Maximum = new decimal(new int[] { 60000, 0, 0, 0 });
            numTimeout.Name = "numTimeout";
            numTimeout.Size = new Size(100, 26);
            numTimeout.TabIndex = 5;
            numTimeout.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            // 
            // lblRetryCount
            // 
            lblRetryCount.AutoSize = true;
            lblRetryCount.Location = new Point(253, 76);
            lblRetryCount.Name = "lblRetryCount";
            lblRetryCount.Size = new Size(79, 16);
            lblRetryCount.TabIndex = 6;
            lblRetryCount.Text = "重试次数:";
            // 
            // numRetryCount
            // 
            numRetryCount.Location = new Point(343, 73);
            numRetryCount.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numRetryCount.Name = "numRetryCount";
            numRetryCount.Size = new Size(80, 26);
            numRetryCount.TabIndex = 7;
            // 
            // lblRetryInterval
            // 
            lblRetryInterval.AutoSize = true;
            lblRetryInterval.Location = new Point(443, 76);
            lblRetryInterval.Name = "lblRetryInterval";
            lblRetryInterval.Size = new Size(111, 16);
            lblRetryInterval.TabIndex = 8;
            lblRetryInterval.Text = "重试间隔(ms):";
            // 
            // numRetryInterval
            // 
            numRetryInterval.Location = new Point(557, 73);
            numRetryInterval.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numRetryInterval.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            numRetryInterval.Name = "numRetryInterval";
            numRetryInterval.Size = new Size(100, 26);
            numRetryInterval.TabIndex = 9;
            numRetryInterval.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // grpDataSource
            // 
            grpDataSource.Controls.Add(lblDataSourceType);
            grpDataSource.Controls.Add(cmbDataSourceType);
            grpDataSource.Controls.Add(pnlVariableSource);
            grpDataSource.Controls.Add(pnlPlcSource);
            grpDataSource.Controls.Add(pnlExpressionSource);
            grpDataSource.Location = new Point(12, 171);
            grpDataSource.Name = "grpDataSource";
            grpDataSource.Size = new Size(760, 150);
            grpDataSource.TabIndex = 10;
            grpDataSource.TabStop = false;
            grpDataSource.Text = "数据源配置";
            // 
            // lblDataSourceType
            // 
            lblDataSourceType.AutoSize = true;
            lblDataSourceType.Location = new Point(20, 28);
            lblDataSourceType.Name = "lblDataSourceType";
            lblDataSourceType.Size = new Size(95, 16);
            lblDataSourceType.TabIndex = 0;
            lblDataSourceType.Text = "数据源类型:";
            // 
            // cmbDataSourceType
            // 
            cmbDataSourceType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDataSourceType.FormattingEnabled = true;
            cmbDataSourceType.Location = new Point(110, 25);
            cmbDataSourceType.Name = "cmbDataSourceType";
            cmbDataSourceType.Size = new Size(150, 24);
            cmbDataSourceType.TabIndex = 1;
            // 
            // pnlVariableSource
            // 
            pnlVariableSource.Controls.Add(lblVariableName);
            pnlVariableSource.Controls.Add(txtVariableName);
            pnlVariableSource.Location = new Point(20, 55);
            pnlVariableSource.Name = "pnlVariableSource";
            pnlVariableSource.Size = new Size(720, 80);
            pnlVariableSource.TabIndex = 2;
            // 
            // lblVariableName
            // 
            lblVariableName.AutoSize = true;
            lblVariableName.Location = new Point(6, 13);
            lblVariableName.Name = "lblVariableName";
            lblVariableName.Size = new Size(63, 16);
            lblVariableName.TabIndex = 0;
            lblVariableName.Text = "变量名:";
            // 
            // txtVariableName
            // 
            txtVariableName.Location = new Point(76, 10);
            txtVariableName.Name = "txtVariableName";
            txtVariableName.Size = new Size(200, 26);
            txtVariableName.TabIndex = 1;
            // 
            // pnlPlcSource
            // 
            pnlPlcSource.Controls.Add(lblPlcModule);
            pnlPlcSource.Controls.Add(txtPlcModule);
            pnlPlcSource.Controls.Add(lblPlcAddress);
            pnlPlcSource.Controls.Add(txtPlcAddress);
            pnlPlcSource.Controls.Add(lblDataType);
            pnlPlcSource.Controls.Add(txtDataType);
            pnlPlcSource.Location = new Point(20, 55);
            pnlPlcSource.Name = "pnlPlcSource";
            pnlPlcSource.Size = new Size(720, 80);
            pnlPlcSource.TabIndex = 3;
            pnlPlcSource.Visible = false;
            // 
            // lblPlcModule
            // 
            lblPlcModule.AutoSize = true;
            lblPlcModule.Location = new Point(0, 13);
            lblPlcModule.Name = "lblPlcModule";
            lblPlcModule.Size = new Size(71, 16);
            lblPlcModule.TabIndex = 0;
            lblPlcModule.Text = "PLC模块:";
            // 
            // txtPlcModule
            // 
            txtPlcModule.Location = new Point(70, 10);
            txtPlcModule.Name = "txtPlcModule";
            txtPlcModule.Size = new Size(150, 26);
            txtPlcModule.TabIndex = 1;
            // 
            // lblPlcAddress
            // 
            lblPlcAddress.AutoSize = true;
            lblPlcAddress.Location = new Point(240, 13);
            lblPlcAddress.Name = "lblPlcAddress";
            lblPlcAddress.Size = new Size(71, 16);
            lblPlcAddress.TabIndex = 2;
            lblPlcAddress.Text = "PLC地址:";
            // 
            // txtPlcAddress
            // 
            txtPlcAddress.Location = new Point(310, 10);
            txtPlcAddress.Name = "txtPlcAddress";
            txtPlcAddress.Size = new Size(150, 26);
            txtPlcAddress.TabIndex = 3;
            // 
            // lblDataType
            // 
            lblDataType.AutoSize = true;
            lblDataType.Location = new Point(480, 13);
            lblDataType.Name = "lblDataType";
            lblDataType.Size = new Size(79, 16);
            lblDataType.TabIndex = 4;
            lblDataType.Text = "数据类型:";
            // 
            // txtDataType
            // 
            txtDataType.Location = new Point(550, 10);
            txtDataType.Name = "txtDataType";
            txtDataType.Size = new Size(100, 26);
            txtDataType.TabIndex = 5;
            txtDataType.Text = "Float";
            // 
            // pnlExpressionSource
            // 
            pnlExpressionSource.Controls.Add(lblExpression);
            pnlExpressionSource.Controls.Add(txtExpression);
            pnlExpressionSource.Location = new Point(20, 55);
            pnlExpressionSource.Name = "pnlExpressionSource";
            pnlExpressionSource.Size = new Size(720, 80);
            pnlExpressionSource.TabIndex = 4;
            pnlExpressionSource.Visible = false;
            // 
            // lblExpression
            // 
            lblExpression.AutoSize = true;
            lblExpression.Location = new Point(0, 13);
            lblExpression.Name = "lblExpression";
            lblExpression.Size = new Size(63, 16);
            lblExpression.TabIndex = 0;
            lblExpression.Text = "表达式:";
            // 
            // txtExpression
            // 
            txtExpression.Location = new Point(70, 10);
            txtExpression.Name = "txtExpression";
            txtExpression.PlaceholderText = "例: {变量1} + {变量2} * 2";
            txtExpression.Size = new Size(400, 26);
            txtExpression.TabIndex = 1;
            // 
            // grpCondition
            // 
            grpCondition.Controls.Add(pnlRangeCondition);
            grpCondition.Controls.Add(pnlEqualityCondition);
            grpCondition.Controls.Add(pnlThresholdCondition);
            grpCondition.Controls.Add(pnlCustomCondition);
            grpCondition.Location = new Point(12, 325);
            grpCondition.Name = "grpCondition";
            grpCondition.Size = new Size(760, 150);
            grpCondition.TabIndex = 20;
            grpCondition.TabStop = false;
            grpCondition.Text = "检测条件配置";
            // 
            // pnlRangeCondition
            // 
            pnlRangeCondition.Controls.Add(lblMinValue);
            pnlRangeCondition.Controls.Add(numMinValue);
            pnlRangeCondition.Controls.Add(lblMaxValue);
            pnlRangeCondition.Controls.Add(numMaxValue);
            pnlRangeCondition.Location = new Point(20, 25);
            pnlRangeCondition.Name = "pnlRangeCondition";
            pnlRangeCondition.Size = new Size(720, 110);
            pnlRangeCondition.TabIndex = 0;
            // 
            // lblMinValue
            // 
            lblMinValue.AutoSize = true;
            lblMinValue.Location = new Point(15, 14);
            lblMinValue.Name = "lblMinValue";
            lblMinValue.Size = new Size(63, 16);
            lblMinValue.TabIndex = 0;
            lblMinValue.Text = "最小值:";
            // 
            // numMinValue
            // 
            numMinValue.DecimalPlaces = 2;
            numMinValue.Location = new Point(85, 11);
            numMinValue.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numMinValue.Minimum = new decimal(new int[] { 999999, 0, 0, int.MinValue });
            numMinValue.Name = "numMinValue";
            numMinValue.Size = new Size(100, 26);
            numMinValue.TabIndex = 1;
            // 
            // lblMaxValue
            // 
            lblMaxValue.AutoSize = true;
            lblMaxValue.Location = new Point(215, 14);
            lblMaxValue.Name = "lblMaxValue";
            lblMaxValue.Size = new Size(63, 16);
            lblMaxValue.TabIndex = 2;
            lblMaxValue.Text = "最大值:";
            // 
            // numMaxValue
            // 
            numMaxValue.DecimalPlaces = 2;
            numMaxValue.Location = new Point(285, 11);
            numMaxValue.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numMaxValue.Minimum = new decimal(new int[] { 999999, 0, 0, int.MinValue });
            numMaxValue.Name = "numMaxValue";
            numMaxValue.Size = new Size(100, 26);
            numMaxValue.TabIndex = 3;
            numMaxValue.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // pnlEqualityCondition
            // 
            pnlEqualityCondition.Controls.Add(lblTargetValue);
            pnlEqualityCondition.Controls.Add(txtTargetValue);
            pnlEqualityCondition.Controls.Add(lblTolerance);
            pnlEqualityCondition.Controls.Add(numTolerance);
            pnlEqualityCondition.Location = new Point(20, 25);
            pnlEqualityCondition.Name = "pnlEqualityCondition";
            pnlEqualityCondition.Size = new Size(720, 110);
            pnlEqualityCondition.TabIndex = 1;
            pnlEqualityCondition.Visible = false;
            // 
            // lblTargetValue
            // 
            lblTargetValue.AutoSize = true;
            lblTargetValue.Location = new Point(0, 13);
            lblTargetValue.Name = "lblTargetValue";
            lblTargetValue.Size = new Size(63, 16);
            lblTargetValue.TabIndex = 0;
            lblTargetValue.Text = "目标值:";
            // 
            // txtTargetValue
            // 
            txtTargetValue.Location = new Point(70, 10);
            txtTargetValue.Name = "txtTargetValue";
            txtTargetValue.Size = new Size(150, 26);
            txtTargetValue.TabIndex = 1;
            // 
            // lblTolerance
            // 
            lblTolerance.AutoSize = true;
            lblTolerance.Location = new Point(240, 13);
            lblTolerance.Name = "lblTolerance";
            lblTolerance.Size = new Size(47, 16);
            lblTolerance.TabIndex = 2;
            lblTolerance.Text = "容差:";
            // 
            // numTolerance
            // 
            numTolerance.DecimalPlaces = 3;
            numTolerance.Location = new Point(310, 10);
            numTolerance.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numTolerance.Name = "numTolerance";
            numTolerance.Size = new Size(100, 26);
            numTolerance.TabIndex = 3;
            numTolerance.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // pnlThresholdCondition
            // 
            pnlThresholdCondition.Controls.Add(lblOperator);
            pnlThresholdCondition.Controls.Add(cmbOperator);
            pnlThresholdCondition.Controls.Add(lblThreshold);
            pnlThresholdCondition.Controls.Add(numThreshold);
            pnlThresholdCondition.Location = new Point(20, 25);
            pnlThresholdCondition.Name = "pnlThresholdCondition";
            pnlThresholdCondition.Size = new Size(720, 110);
            pnlThresholdCondition.TabIndex = 2;
            pnlThresholdCondition.Visible = false;
            // 
            // lblOperator
            // 
            lblOperator.AutoSize = true;
            lblOperator.Location = new Point(0, 13);
            lblOperator.Name = "lblOperator";
            lblOperator.Size = new Size(79, 16);
            lblOperator.TabIndex = 0;
            lblOperator.Text = "比较操作:";
            // 
            // cmbOperator
            // 
            cmbOperator.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOperator.FormattingEnabled = true;
            cmbOperator.Location = new Point(80, 10);
            cmbOperator.Name = "cmbOperator";
            cmbOperator.Size = new Size(120, 24);
            cmbOperator.TabIndex = 1;
            // 
            // lblThreshold
            // 
            lblThreshold.AutoSize = true;
            lblThreshold.Location = new Point(220, 13);
            lblThreshold.Name = "lblThreshold";
            lblThreshold.Size = new Size(47, 16);
            lblThreshold.TabIndex = 2;
            lblThreshold.Text = "阈值:";
            // 
            // numThreshold
            // 
            numThreshold.DecimalPlaces = 2;
            numThreshold.Location = new Point(280, 10);
            numThreshold.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numThreshold.Minimum = new decimal(new int[] { 999999, 0, 0, int.MinValue });
            numThreshold.Name = "numThreshold";
            numThreshold.Size = new Size(100, 26);
            numThreshold.TabIndex = 3;
            // 
            // pnlCustomCondition
            // 
            pnlCustomCondition.Controls.Add(lblCustomExpression);
            pnlCustomCondition.Controls.Add(txtCustomExpression);
            pnlCustomCondition.Location = new Point(20, 25);
            pnlCustomCondition.Name = "pnlCustomCondition";
            pnlCustomCondition.Size = new Size(720, 110);
            pnlCustomCondition.TabIndex = 3;
            pnlCustomCondition.Visible = false;
            // 
            // lblCustomExpression
            // 
            lblCustomExpression.AutoSize = true;
            lblCustomExpression.Location = new Point(0, 13);
            lblCustomExpression.Name = "lblCustomExpression";
            lblCustomExpression.Size = new Size(111, 16);
            lblCustomExpression.TabIndex = 0;
            lblCustomExpression.Text = "自定义表达式:";
            // 
            // txtCustomExpression
            // 
            txtCustomExpression.Location = new Point(110, 10);
            txtCustomExpression.Name = "txtCustomExpression";
            txtCustomExpression.PlaceholderText = "例: {value} > 10 && {value} < 50";
            txtCustomExpression.Size = new Size(400, 26);
            txtCustomExpression.TabIndex = 1;
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
            grpResultHandling.Location = new Point(12, 485);
            grpResultHandling.Name = "grpResultHandling";
            grpResultHandling.Size = new Size(760, 180);
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
            btnOK.Location = new Point(601, 676);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(80, 30);
            btnOK.TabIndex = 40;
            btnOK.Text = "确定";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(691, 676);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 30);
            btnCancel.TabIndex = 41;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnTestDetection
            // 
            btnTestDetection.Location = new Point(501, 676);
            btnTestDetection.Name = "btnTestDetection";
            btnTestDetection.Size = new Size(90, 30);
            btnTestDetection.TabIndex = 42;
            btnTestDetection.Text = "测试检测";
            btnTestDetection.UseVisualStyleBackColor = true;
            // 
            // Form_Detection
            // 
            AcceptButton = btnOK;
            AutoScaleMode = AutoScaleMode.None;
            CancelButton = btnCancel;
            ClientSize = new Size(784, 718);
            Controls.Add(btnTestDetection);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(grpResultHandling);
            Controls.Add(grpCondition);
            Controls.Add(grpDataSource);
            Controls.Add(grpBasicInfo);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_Detection";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "检测工具配置";
            ZoomScaleRect = new Rectangle(15, 15, 784, 691);
            grpBasicInfo.ResumeLayout(false);
            grpBasicInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numTimeout).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRetryCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRetryInterval).EndInit();
            grpDataSource.ResumeLayout(false);
            grpDataSource.PerformLayout();
            pnlVariableSource.ResumeLayout(false);
            pnlVariableSource.PerformLayout();
            pnlPlcSource.ResumeLayout(false);
            pnlPlcSource.PerformLayout();
            pnlExpressionSource.ResumeLayout(false);
            pnlExpressionSource.PerformLayout();
            grpCondition.ResumeLayout(false);
            pnlRangeCondition.ResumeLayout(false);
            pnlRangeCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMinValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxValue).EndInit();
            pnlEqualityCondition.ResumeLayout(false);
            pnlEqualityCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numTolerance).EndInit();
            pnlThresholdCondition.ResumeLayout(false);
            pnlThresholdCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numThreshold).EndInit();
            pnlCustomCondition.ResumeLayout(false);
            pnlCustomCondition.PerformLayout();
            grpResultHandling.ResumeLayout(false);
            grpResultHandling.PerformLayout();
            pnlJumpStep.ResumeLayout(false);
            pnlJumpStep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numFailureStep).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSuccessStep).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpBasicInfo;
        private System.Windows.Forms.Label lblDetectionName;
        private System.Windows.Forms.TextBox txtDetectionName;
        private System.Windows.Forms.Label lblDetectionType;
        private System.Windows.Forms.ComboBox cmbDetectionType;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.Label lblRetryCount;
        private System.Windows.Forms.NumericUpDown numRetryCount;
        private System.Windows.Forms.Label lblRetryInterval;
        private System.Windows.Forms.NumericUpDown numRetryInterval;

        private System.Windows.Forms.GroupBox grpDataSource;
        private System.Windows.Forms.Label lblDataSourceType;
        private System.Windows.Forms.ComboBox cmbDataSourceType;
        private System.Windows.Forms.Panel pnlVariableSource;
        private System.Windows.Forms.Label lblVariableName;
        private System.Windows.Forms.TextBox txtVariableName;
        private System.Windows.Forms.Panel pnlPlcSource;
        private System.Windows.Forms.Label lblPlcModule;
        private System.Windows.Forms.TextBox txtPlcModule;
        private System.Windows.Forms.Label lblPlcAddress;
        private System.Windows.Forms.TextBox txtPlcAddress;
        private System.Windows.Forms.Label lblDataType;
        private System.Windows.Forms.TextBox txtDataType;
        private System.Windows.Forms.Panel pnlExpressionSource;
        private System.Windows.Forms.Label lblExpression;
        private System.Windows.Forms.TextBox txtExpression;

        private System.Windows.Forms.GroupBox grpCondition;
        private System.Windows.Forms.Panel pnlRangeCondition;
        private System.Windows.Forms.Label lblMinValue;
        private System.Windows.Forms.NumericUpDown numMinValue;
        private System.Windows.Forms.Label lblMaxValue;
        private System.Windows.Forms.NumericUpDown numMaxValue;
        private System.Windows.Forms.Panel pnlEqualityCondition;
        private System.Windows.Forms.Label lblTargetValue;
        private System.Windows.Forms.TextBox txtTargetValue;
        private System.Windows.Forms.Label lblTolerance;
        private System.Windows.Forms.NumericUpDown numTolerance;
        private System.Windows.Forms.Panel pnlThresholdCondition;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.ComboBox cmbOperator;
        private System.Windows.Forms.Label lblThreshold;
        private System.Windows.Forms.NumericUpDown numThreshold;
        private System.Windows.Forms.Panel pnlCustomCondition;
        private System.Windows.Forms.Label lblCustomExpression;
        private System.Windows.Forms.TextBox txtCustomExpression;

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
    }
}