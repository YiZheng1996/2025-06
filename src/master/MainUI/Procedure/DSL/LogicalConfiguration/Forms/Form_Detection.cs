using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    public partial class Form_Detection : UIForm
    {
        private Parameter_Detection _parameter;

        public Form_Detection()
        {
            InitializeComponent();
            InitializeForm();
        }

        public Form_Detection(Parameter_Detection parameter) : this()
        {
            _parameter = parameter ?? new Parameter_Detection();
            LoadParameterToForm();
        }

        private void InitializeForm()
        {
            _parameter = new Parameter_Detection();
            InitializeComboBoxes();
            SetupEventHandlers();
        }

        private void InitializeComboBoxes()
        {
            // 初始化检测类型下拉框
            cmbDetectionType.DataSource = Enum.GetValues(typeof(DetectionType));

            // 初始化数据源类型下拉框
            cmbDataSourceType.DataSource = Enum.GetValues(typeof(DataSourceType));

            // 初始化比较操作符下拉框
            cmbOperator.DataSource = Enum.GetValues(typeof(ComparisonOperator));

            // 初始化失败处理行为下拉框
            cmbFailureAction.DataSource = Enum.GetValues(typeof(FailureAction));

            // 设置默认值
            cmbDetectionType.SelectedItem = DetectionType.ValueRange;
            cmbDataSourceType.SelectedItem = DataSourceType.Variable;
            cmbOperator.SelectedItem = ComparisonOperator.GreaterThan;
            cmbFailureAction.SelectedItem = FailureAction.Continue;
        }

        private void SetupEventHandlers()
        {
            cmbDetectionType.SelectedIndexChanged += CmbDetectionType_SelectedIndexChanged;
            cmbDataSourceType.SelectedIndexChanged += CmbDataSourceType_SelectedIndexChanged;
            cmbFailureAction.SelectedIndexChanged += CmbFailureAction_SelectedIndexChanged;

            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
            btnTestDetection.Click += BtnTestDetection_Click;
        }

        private void LoadParameterToForm()
        {
            if (_parameter == null) return;

            // 基本信息
            txtDetectionName.Text = _parameter.DetectionName;
            cmbDetectionType.SelectedItem = _parameter.Type;
            numTimeout.Value = _parameter.TimeoutMs;
            numRetryCount.Value = _parameter.RetryCount;
            numRetryInterval.Value = _parameter.RetryIntervalMs;

            // 数据源配置
            cmbDataSourceType.SelectedItem = _parameter.DataSource.SourceType;
            txtVariableName.Text = _parameter.DataSource.VariableName;
            txtPlcModule.Text = _parameter.DataSource.PlcConfig.ModuleName;
            txtPlcAddress.Text = _parameter.DataSource.PlcConfig.Address;
            txtDataType.Text = _parameter.DataSource.PlcConfig.DataType;
            txtExpression.Text = _parameter.DataSource.Expression;

            // 检测条件
            numMinValue.Value = (decimal)_parameter.Condition.MinValue;
            numMaxValue.Value = (decimal)_parameter.Condition.MaxValue;
            txtTargetValue.Text = _parameter.Condition.TargetValue;
            numThreshold.Value = (decimal)_parameter.Condition.ThresholdValue;
            cmbOperator.SelectedItem = _parameter.Condition.Operator;
            numTolerance.Value = (decimal)_parameter.Condition.Tolerance;
            txtCustomExpression.Text = _parameter.Condition.CustomExpression;

            // 结果处理
            chkSaveResult.Checked = _parameter.ResultHandling.SaveToVariable;
            txtResultVariable.Text = _parameter.ResultHandling.ResultVariableName;
            chkSaveValue.Checked = _parameter.ResultHandling.SaveValueToVariable;
            txtValueVariable.Text = _parameter.ResultHandling.ValueVariableName;
            cmbFailureAction.SelectedItem = _parameter.ResultHandling.OnFailure;
            numFailureStep.Value = _parameter.ResultHandling.FailureStepIndex;
            numSuccessStep.Value = _parameter.ResultHandling.SuccessStepIndex;
            chkShowResult.Checked = _parameter.ResultHandling.ShowResult;
            txtMessageTemplate.Text = _parameter.ResultHandling.MessageTemplate;

            // 更新界面显示
            UpdateFormLayout();
        }

        private void SaveFormToParameter()
        {
            if (_parameter == null) return;

            // 基本信息
            _parameter.DetectionName = txtDetectionName.Text;
            _parameter.Type = (DetectionType)cmbDetectionType.SelectedItem;
            _parameter.TimeoutMs = (int)numTimeout.Value;
            _parameter.RetryCount = (int)numRetryCount.Value;
            _parameter.RetryIntervalMs = (int)numRetryInterval.Value;

            // 数据源配置
            _parameter.DataSource.SourceType = (DataSourceType)cmbDataSourceType.SelectedItem;
            _parameter.DataSource.VariableName = txtVariableName.Text;
            _parameter.DataSource.PlcConfig.ModuleName = txtPlcModule.Text;
            _parameter.DataSource.PlcConfig.Address = txtPlcAddress.Text;
            _parameter.DataSource.PlcConfig.DataType = txtDataType.Text;
            _parameter.DataSource.Expression = txtExpression.Text;

            // 检测条件
            _parameter.Condition.MinValue = (double)numMinValue.Value;
            _parameter.Condition.MaxValue = (double)numMaxValue.Value;
            _parameter.Condition.TargetValue = txtTargetValue.Text;
            _parameter.Condition.ThresholdValue = (double)numThreshold.Value;
            _parameter.Condition.Operator = (ComparisonOperator)cmbOperator.SelectedItem;
            _parameter.Condition.Tolerance = (double)numTolerance.Value;
            _parameter.Condition.CustomExpression = txtCustomExpression.Text;

            // 结果处理
            _parameter.ResultHandling.SaveToVariable = chkSaveResult.Checked;
            _parameter.ResultHandling.ResultVariableName = txtResultVariable.Text;
            _parameter.ResultHandling.SaveValueToVariable = chkSaveValue.Checked;
            _parameter.ResultHandling.ValueVariableName = txtValueVariable.Text;
            _parameter.ResultHandling.OnFailure = (FailureAction)cmbFailureAction.SelectedItem;
            _parameter.ResultHandling.FailureStepIndex = (int)numFailureStep.Value;
            _parameter.ResultHandling.SuccessStepIndex = (int)numSuccessStep.Value;
            _parameter.ResultHandling.ShowResult = chkShowResult.Checked;
            _parameter.ResultHandling.MessageTemplate = txtMessageTemplate.Text;
        }

        private void CmbDetectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFormLayout();
        }

        private void CmbDataSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataSourceLayout();
        }

        private void CmbFailureAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFailureActionLayout();
        }

        private void UpdateFormLayout()
        {
            var detectionType = (DetectionType)cmbDetectionType.SelectedItem;

            // 隐藏所有条件面板
            pnlRangeCondition.Visible = false;
            pnlEqualityCondition.Visible = false;
            pnlThresholdCondition.Visible = false;
            pnlCustomCondition.Visible = false;

            // 根据检测类型显示相应的条件配置面板
            switch (detectionType)
            {
                case DetectionType.ValueRange:
                    pnlRangeCondition.Visible = true;
                    break;
                case DetectionType.Equality:
                    pnlEqualityCondition.Visible = true;
                    break;
                case DetectionType.Threshold:
                case DetectionType.Status:
                    pnlThresholdCondition.Visible = true;
                    break;
                case DetectionType.CustomExpression:
                    pnlCustomCondition.Visible = true;
                    break;
            }
        }

        private void UpdateDataSourceLayout()
        {
            var sourceType = (DataSourceType)cmbDataSourceType.SelectedItem;

            // 隐藏所有数据源配置面板
            pnlVariableSource.Visible = false;
            pnlPlcSource.Visible = false;
            pnlExpressionSource.Visible = false;

            // 根据数据源类型显示相应的配置面板
            switch (sourceType)
            {
                case DataSourceType.Variable:
                    pnlVariableSource.Visible = true;
                    break;
                case DataSourceType.PLC:
                    pnlPlcSource.Visible = true;
                    break;
                case DataSourceType.Expression:
                    pnlExpressionSource.Visible = true;
                    break;
            }
        }

        private void UpdateFailureActionLayout()
        {
            var failureAction = (FailureAction)cmbFailureAction.SelectedItem;

            // 根据失败处理方式显示相应的配置
            pnlJumpStep.Visible = failureAction == FailureAction.Jump;
        }

        private async void BtnTestDetection_Click(object sender, EventArgs e)
        {
            try
            {
                btnTestDetection.Enabled = false;
                btnTestDetection.Text = "测试中...";

                // 保存当前配置到参数对象
                SaveFormToParameter();

                // 执行测试检测
                bool result = await TestDetection();

                string message = result ? "检测测试成功！" : "检测测试失败！";
                MessageHelper.MessageOK(message);
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"测试失败: {ex.Message}");
            }
            finally
            {
                btnTestDetection.Enabled = true;
                btnTestDetection.Text = "测试检测";
            }
        }

        private async Task<bool> TestDetection()
        {
            try
            {
                // 这里调用实际的检测方法进行测试
                return await MethodCollection.Method_Detection(_parameter);
            }
            catch (Exception ex)
            {
                throw new Exception($"检测测试失败: {ex.Message}", ex);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证输入
                if (!ValidateInput())
                {
                    return;
                }

                // 保存配置
                SaveFormToParameter();

                // 将参数保存到流程中
                SaveParameterToProcess();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"保存失败: {ex.Message}");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidateInput()
        {
            // 检测名称不能为空
            if (string.IsNullOrWhiteSpace(txtDetectionName.Text))
            {
                MessageHelper.MessageOK("请输入检测项名称");
                txtDetectionName.Focus();
                return false;
            }

            // 根据数据源类型验证相应的输入
            var sourceType = (DataSourceType)cmbDataSourceType.SelectedItem;
            switch (sourceType)
            {
                case DataSourceType.Variable:
                    if (string.IsNullOrWhiteSpace(txtVariableName.Text))
                    {
                        MessageHelper.MessageOK("请输入变量名");
                        txtVariableName.Focus();
                        return false;
                    }
                    break;
                case DataSourceType.PLC:
                    if (string.IsNullOrWhiteSpace(txtPlcModule.Text) ||
                        string.IsNullOrWhiteSpace(txtPlcAddress.Text))
                    {
                        MessageHelper.MessageOK("请输入完整的PLC地址信息");
                        return false;
                    }
                    break;
                case DataSourceType.Expression:
                    if (string.IsNullOrWhiteSpace(txtExpression.Text))
                    {
                        MessageHelper.MessageOK("请输入计算表达式");
                        txtExpression.Focus();
                        return false;
                    }
                    break;
            }

            // 验证检测条件
            var detectionType = (DetectionType)cmbDetectionType.SelectedItem;
            if (detectionType == DetectionType.ValueRange)
            {
                if (numMinValue.Value >= numMaxValue.Value)
                {
                    MessageHelper.MessageOK("最小值必须小于最大值");
                    return false;
                }
            }

            return true;
        }

        private void SaveParameterToProcess()
        {
            // 这里需要将参数保存到当前流程步骤中
            // 具体实现需要根据流程管理器的接口来调整
            var singleton = SingletonStatus.Instance;
            // 保存逻辑...
        }

        public Parameter_Detection GetParameter()
        {
            SaveFormToParameter();
            return _parameter;
        }
    }
}
