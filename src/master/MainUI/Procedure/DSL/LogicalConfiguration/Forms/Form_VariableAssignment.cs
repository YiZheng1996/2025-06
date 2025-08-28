using MainUI.Procedure.DSL.LogicalConfiguration.Engine;
using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using MainUI.Procedure.DSL.LogicalConfiguration.Services;
using MainUI.Procedure.DSL.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// 变量赋值工具窗体
    /// </summary>
    public partial class Form_VariableAssignment : BaseParameterForm, IParameterForm<Form_VariableAssignment>
    {
        #region 私有字段

        private readonly IWorkflowStateService _workflowStateService;
        private readonly GlobalVariableManager _globalVariableManager;
        private readonly ILogger<Form_VariableAssignment> _logger;

        // 核心引擎
        private readonly ExpressionValidator _expressionValidator;
        private readonly VariableAssignmentEngine _assignmentEngine;


        // 实时验证定时器
        private System.Windows.Forms.Timer _validationTimer;
        private System.Windows.Forms.Timer _previewTimer;

        // 界面状态
        private bool _isInitializing = true;
        private bool _hasUnsavedChanges = false;

        private Parameter_VariableAssignment _parameter;
        public Parameter_VariableAssignment Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_VariableAssignment();
                // 只有在窗体完全加载且不处于基类的加载状态时才更新界面
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }

        Form_VariableAssignment IParameterForm<Form_VariableAssignment>.Parameter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion

        #region 构造函数

        public Form_VariableAssignment()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                InitializeForm();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Form_VariableAssignment(
            IWorkflowStateService workflowStateService,
            GlobalVariableManager globalVariableManager,
            ILogger<Form_VariableAssignment> logger = null)
        {
            InitializeComponent();

            _workflowStateService = workflowStateService ?? throw new ArgumentNullException(nameof(workflowStateService));
            _globalVariableManager = globalVariableManager ?? throw new ArgumentNullException(nameof(globalVariableManager));
            _logger = logger;

            // 初始化核心引擎
            _expressionValidator = new ExpressionValidator(_globalVariableManager);
            _assignmentEngine = new VariableAssignmentEngine(_globalVariableManager, _plcManager, _expressionValidator);

            // 设置窗体样式
            InitializeFormStyle();

            // 初始化定时器
            InitializeTimers();
        }
        #endregion

        #region 初始化方法
        private void InitializeForm()
        {
            try
            {
                _isInitializing = true;

                LoadParameter(new Parameter_VariableAssignment());

                // 初始化界面数据
                InitializeVariableComboBox();

                // 设置默认值
                if (_parameter == null)
                {
                    cmbAssignmentType.SelectedIndex = 0;
                    chkEnabled.Checked = true;
                }

                // 绑定事件
                BindEvents();

                // 初始验证
                ValidateConfigurationAsync();

                _isInitializing = false;
                _logger?.LogInformation("变量赋值工具窗体加载完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "窗体加载时发生错误");
                UIMessageBox.ShowError($"窗体加载失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 加载参数到界面
        /// </summary>
        /// <param name="parameter">变量赋值参数</param>
        public void LoadParameter(Parameter_VariableAssignment parameter)
        {
            _parameter = parameter ?? new Parameter_VariableAssignment();

            try
            {
                // 加载目标变量
                if (!string.IsNullOrEmpty(_parameter.TargetVarName))
                {
                    cmbTargetVariable.Text = _parameter.TargetVarName;
                }

                // 加载赋值内容
                txtAssignmentContent.Text = _parameter.Expression ?? string.Empty;

                // 加载赋值方式 (根据现有参数推断)
                if (!string.IsNullOrEmpty(_parameter.AssignmentForm))
                {
                    cmbAssignmentType.Text = _parameter.AssignmentForm;
                }
                else
                {
                    cmbAssignmentType.SelectedIndex = 0; // 默认直接赋值
                }

                // 加载启用状态
                chkEnabled.Checked = _parameter.IsAssignment;

                // 刷新预览
                RefreshPreviewAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载参数时发生错误");
                UIMessageBox.ShowError($"加载参数失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 初始化变量下拉框
        /// </summary>
        private void InitializeVariableComboBox()
        {
            try
            {
                var variables = _globalVariableManager.GetAllVariables();
                var variableItems = variables.Select(v => new
                {
                    //Text = $"{v.VarName} ({v.VarType})",
                    Text = $"{v.VarName}",
                    Value = v.VarName
                }).ToList();

                cmbTargetVariable.DisplayMember = "Text";
                cmbTargetVariable.ValueMember = "Value";
                cmbTargetVariable.DataSource = variableItems;

                if (variableItems.Count > 0 && cmbTargetVariable.SelectedIndex == -1)
                {
                    cmbTargetVariable.SelectedIndex = 0;
                }

                pnlVoluationSource.Visible = true;
                pnlPlcSource.Visible = false;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "初始化变量下拉框时发生错误");
            }
        }

        #endregion

        #region 窗体事件


        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void Form_VariableAssignment_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // 检查是否有未保存的更改
                if (_hasUnsavedChanges && this.DialogResult != DialogResult.OK)
                {
                    var result = MessageHelper.MessageYes(
                        "您有未保存的更改，是否要保存？");

                    switch (result)
                    {
                        case DialogResult.OK:
                            // 尝试保存
                            var validation = ValidateConfigurationSync();
                            if (!validation.IsValid)
                            {
                                UIMessageBox.ShowWarning($"配置验证失败，无法保存：{validation.Message}");
                                e.Cancel = true;
                                return;
                            }
                            DialogResult = DialogResult.OK;
                            break;

                        case DialogResult.Cancel:
                            e.Cancel = true;
                            return;

                        case DialogResult.No:
                            // 不保存，直接退出
                            break;
                    }
                }

                // 释放定时器资源
                _validationTimer?.Stop();
                _validationTimer?.Dispose();
                _previewTimer?.Stop();
                _previewTimer?.Dispose();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "窗体关闭时发生错误");
            }
        }

        #endregion

        #region 按钮事件

        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                btnOK.Enabled = false;
                btnOK.Text = "验证中...";

                // 验证配置
                var validationResult = ValidateConfigurationAsync();
                if (!validationResult.IsValid)
                {
                    UIMessageBox.ShowWarning($"配置验证失败：\n{string.Join("\n", validationResult.Errors)}");
                    return;
                }

                // 如果有警告，询问用户是否继续
                if (validationResult.Warnings?.Any() == true)
                {
                    var warningMessage = $"发现以下警告：\n{string.Join("\n", validationResult.Warnings)}\n\n是否继续保存？";
                    var result = MessageHelper.MessageYes(warningMessage);
                    if (result != DialogResult.OK)
                        return;
                }

                _hasUnsavedChanges = false;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "确定按钮处理时发生错误");
                UIMessageBox.ShowError($"操作失败：{ex.Message}");
            }
            finally
            {
                btnOK.Enabled = true;
                btnOK.Text = "确定";
            }
        }

        /// <summary>
        /// 测试按钮点击事件
        /// </summary>
        private async void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                btnTest.Enabled = false;
                btnTest.Text = "测试中...";

                // 先验证配置
                var validationResult = ValidateConfigurationAsync();
                if (!validationResult.IsValid)
                {
                    UIMessageBox.ShowWarning($"配置验证失败，无法测试：\n{string.Join("\n", validationResult.Errors)}");
                    return;
                }

                // 获取当前参数
                var parameter = GetParameter();
                parameter.Condition = txtCondition.Text?.Trim(); // 确保包含条件

                // 执行测试赋值
                var testResult = await _assignmentEngine.ExecuteAssignmentAsync(parameter);

                if (testResult.Success)
                {
                    if (testResult.Skipped)
                    {
                        UIMessageBox.ShowInfo($"测试完成（已跳过）：\n{testResult.SkipReason}");
                    }
                    else
                    {
                        var message = $"测试成功！\n" +
                                    $"变量：{testResult.TargetVariableName}\n" +
                                    $"原值：{testResult.OldValue ?? "null"}\n" +
                                    $"新值：{testResult.NewValue ?? "null"}\n" +
                                    $"执行时间：{testResult.ExecutionTime.TotalMilliseconds:F2}ms";

                        UIMessageBox.ShowSuccess(message);
                    }
                }
                else
                {
                    var errorMessage = $"测试失败：{testResult.ErrorMessage}";
                    if (testResult.ValidationErrors?.Any() == true)
                    {
                        errorMessage += $"\n验证错误：\n{string.Join("\n", testResult.ValidationErrors)}";
                    }

                    UIMessageBox.ShowWarning(errorMessage);
                }

                // 刷新预览
                RefreshPreviewAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "测试赋值时发生错误");
                UIMessageBox.ShowError($"测试失败：{ex.Message}");
            }
            finally
            {
                btnTest.Enabled = true;
                btnTest.Text = "测试";
            }
        }

        /// <summary>
        /// 表达式构建器按钮点击事件
        /// </summary>
        private void btnExpressionBuilder_Click(object sender, EventArgs e)
        {
            try
            {
                // 创建表达式构建器对话框
                using var builder = new ExpressionBuilderDialog(_globalVariableManager, _expressionValidator);
                builder.InitialExpression = txtAssignmentContent.Text;
                builder.TargetVariableType = GetTargetVariableType();

                if (builder.ShowDialog(this) == DialogResult.OK)
                {
                    txtAssignmentContent.Text = builder.GeneratedExpression;
                    _hasUnsavedChanges = true;

                    // 触发验证
                    RestartValidationTimer();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "打开表达式构建器时发生错误");
                UIMessageBox.ShowError($"无法打开表达式构建器：{ex.Message}");
            }
        }

        /// <summary>
        /// 帮助按钮点击事件
        /// </summary>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHelpDialog();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "显示帮助时发生错误");
                UIMessageBox.ShowError($"显示帮助失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 显示帮助对话框
        /// </summary>
        private void ShowHelpDialog()
        {
            var helpText = @"变量赋值工具使用说明

=== 基础配置 ===
• 目标变量：选择要赋值的变量
• 赋值方式：选择赋值的方式
• 赋值内容：输入要赋的值或表达式

=== 赋值方式说明 ===
• 直接赋值：直接设置固定值
• 表达式计算：使用表达式计算结果
• 从其他变量复制：复制其他变量的值
• 从PLC读取：从PLC模块读取值

=== 表达式语法 ===
• 变量引用：{变量名}
• 字符串连接：{Var1} + ""_"" + {Var2}
• 数值计算：{Var1} + {Var2}
• 直接值：""Hello World""

=== 高级配置 ===
• 执行条件：设置赋值的执行条件
• 描述说明：对赋值操作的详细说明
• 启用状态：控制是否执行此赋值

=== 注意事项 ===
• 必须先定义目标变量
• 表达式中引用的变量必须存在
• 建议先使用测试功能验证配置";

            MessageHelper.MessageOK(helpText);
        }


        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region 核心功能方法

        /// <summary>
        /// 异步验证配置
        /// </summary>
        private ValidationResult ValidateConfigurationAsync()
        {
            try
            {
                var parameter = GetParameter();
                var context = new ValidationContext
                {
                    TargetVariableName = parameter.TargetVarName,
                    TargetVariableType = GetTargetVariableType()
                };

                // 验证表达式
                var expressionResult = _expressionValidator.ValidateExpression(parameter.Expression, context);

                // 验证条件（如果有）
                ValidationResult conditionResult = null;
                if (!string.IsNullOrWhiteSpace(parameter.Condition))
                {
                    conditionResult = _expressionValidator.ValidateExpression(parameter.Condition);
                }

                // 合并验证结果
                var combinedResult = new ValidationResult
                {
                    IsValid = expressionResult.IsValid && (conditionResult?.IsValid != false),
                    Errors = new List<string>(),
                    Warnings = new List<string>()
                };

                combinedResult.Errors.AddRange(expressionResult.Errors);
                combinedResult.Warnings.AddRange(expressionResult.Warnings);

                if (conditionResult != null)
                {
                    combinedResult.Errors.AddRange(conditionResult.Errors.Select(e => $"条件验证: {e}"));
                    combinedResult.Warnings.AddRange(conditionResult.Warnings.Select(w => $"条件警告: {w}"));
                }

                combinedResult.Message = GenerateValidationMessage(combinedResult);

                // 在UI线程更新界面
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => UpdateValidationUI(combinedResult)));
                }
                else
                {
                    UpdateValidationUI(combinedResult);
                }

                return combinedResult;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "异步验证配置时发生错误");
                var errorResult = ValidationResult.Error($"验证过程发生错误: {ex.Message}");
                UpdateValidationUI(errorResult);
                return errorResult;
            }
        }

        /// <summary>
        /// 同步验证配置（用于窗体关闭时）
        /// </summary>
        private ValidationResult ValidateConfigurationSync()
        {
            try
            {
                var parameter = GetParameter();
                var context = new ValidationContext
                {
                    TargetVariableName = parameter.TargetVarName,
                    TargetVariableType = GetTargetVariableType()
                };

                return _expressionValidator.ValidateExpression(parameter.Expression, context);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "同步验证配置时发生错误");
                return ValidationResult.Error($"验证过程发生错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 刷新预览
        /// </summary>
        private void RefreshPreviewAsync()
        {
            try
            {
                var previewLines = new List<string>();
                var parameter = GetParameter();

                // 基本信息
                previewLines.Add($"目标变量：{parameter.TargetVarName}");
                previewLines.Add($"赋值方式：{parameter.AssignmentForm}");
                previewLines.Add($"赋值内容：{parameter.Expression}");

                if (!string.IsNullOrWhiteSpace(parameter.Condition))
                {
                    previewLines.Add($"执行条件：{parameter.Condition}");
                }

                previewLines.Add($"状态：{(parameter.IsAssignment ? "启用" : "禁用")}");
                previewLines.Add("");

                // 计算预期结果
                try
                {
                    var targetVar = _globalVariableManager.FindVariableByName(parameter.TargetVarName);
                    if (targetVar != null)
                    {
                        previewLines.Add($"当前值：{targetVar.VarValue ?? "null"}");

                        // 使用表达式验证器计算预期值
                        var calculationResult = _expressionValidator.CalculateExpectedValue(parameter.Expression);
                        if (calculationResult.Success)
                        {
                            previewLines.Add($"预期值：{calculationResult.Value ?? "null"}");

                            if (!Equals(targetVar.VarValue?.ToString(), calculationResult.Value?.ToString()))
                            {
                                previewLines.Add("✅ 值将发生变化");
                            }
                            else
                            {
                                previewLines.Add("ℹ️ 值不会发生变化");
                            }
                        }
                        else
                        {
                            previewLines.Add($"⚠️ 预期值计算失败：{calculationResult.ErrorMessage}");
                        }

                        // 条件评估
                        if (!string.IsNullOrWhiteSpace(parameter.Condition))
                        {
                            var conditionResult = _expressionValidator.CalculateExpectedValue(parameter.Condition);
                            if (conditionResult.Success)
                            {
                                var conditionMet = ToBool(conditionResult.Value);
                                previewLines.Add($"条件评估：{conditionResult.Value} ({(conditionMet ? "满足" : "不满足")})");
                            }
                            else
                            {
                                previewLines.Add($"⚠️ 条件评估失败：{conditionResult.ErrorMessage}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    previewLines.Add($"⚠️ 预览计算失败：{ex.Message}");
                }

                // 在UI线程更新界面
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        rtbPreviewResult.Clear();
                        rtbPreviewResult.Text = string.Join(Environment.NewLine, previewLines);
                        rtbPreviewResult.ForeColor = Color.FromArgb(73, 80, 87);
                    }));
                }
                else
                {
                    rtbPreviewResult.Clear();
                    rtbPreviewResult.Text = string.Join(Environment.NewLine, previewLines);
                    rtbPreviewResult.ForeColor = Color.FromArgb(73, 80, 87);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "刷新预览时发生错误");
            }
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        public Parameter_VariableAssignment GetParameter()
        {
            try
            {
                var parameter = new Parameter_VariableAssignment
                {
                    TargetVarName = cmbTargetVariable.Text?.Trim(),
                    Expression = txtAssignmentContent.Text?.Trim(),
                    IsAssignment = chkEnabled.Checked,
                    AssignmentForm = cmbAssignmentType.Text?.Trim(),
                    Condition = txtCondition.Text?.Trim(),
                    Description = txtDescription.Text?.Trim()
                };
                return parameter;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "获取参数时发生错误");
                throw new InvalidOperationException($"获取参数失败：{ex.Message}", ex);
            }
        }

        #endregion


        #region 私有辅助方法

        /// <summary>
        /// 初始化定时器
        /// </summary>
        private void InitializeTimers()
        {
            // 验证定时器 - 延迟验证以避免频繁触发
            _validationTimer = new System.Windows.Forms.Timer
            {
                Interval = 500 // 500ms延迟
            };
            _validationTimer.Tick += (s, e) =>
            {
                _validationTimer.Stop();
                ValidateConfigurationAsync();
            };

            // 预览定时器 - 延迟预览更新
            _previewTimer = new System.Windows.Forms.Timer
            {
                Interval = 800 // 800ms延迟
            };
            _previewTimer.Tick += (s, e) =>
            {
                _previewTimer.Stop();
                RefreshPreviewAsync();
            };
        }

        /// <summary>
        /// 重启验证定时器
        /// </summary>
        private void RestartValidationTimer()
        {
            _validationTimer.Stop();
            _validationTimer.Start();
        }

        /// <summary>
        /// 重启预览定时器
        /// </summary>
        private void RestartPreviewTimer()
        {
            _previewTimer.Stop();
            _previewTimer.Start();
        }

        /// <summary>
        /// 初始化窗体样式
        /// </summary>
        private void InitializeFormStyle()
        {
            // 设置窗体属性
            this.Text = "变量赋值工具";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;

            // 设置Sunny.UI主题
            this.Style = UIStyle.Custom;
            this.StyleCustomMode = true;
            this.TitleColor = Color.FromArgb(65, 100, 204);
            this.TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            this.RectColor = Color.FromArgb(65, 100, 204);
        }

        /// <summary>
        /// 根据赋值类型更新UI
        /// </summary>
        private void UpdateUIBasedOnAssignmentType()
        {
            var selectedType = cmbAssignmentType.Text;

            pnlVoluationSource.Visible = true;
            pnlPlcSource.Visible = false;
            switch (selectedType)
            {
                case "表达式计算":
                    txtAssignmentContent.Watermark = "输入计算表达式，如：{Var1} + {Var2} * 2";
                    btnExpressionBuilder.Visible = true;
                    break;

                case "从其他变量复制":
                    txtAssignmentContent.Watermark = "输入源变量名或使用 {变量名} 格式";
                    btnExpressionBuilder.Visible = false;
                    break;

                case "从PLC读取":
                    txtAssignmentContent.Watermark = "输入PLC地址，如：Module1:DB1.DBD0";
                    btnExpressionBuilder.Visible = false;
                    pnlVoluationSource.Visible = false;
                    pnlPlcSource.Visible = true;
                    break;

                default: // 直接赋值
                    txtAssignmentContent.Watermark = "输入要赋的值，字符串请用引号包围";
                    btnExpressionBuilder.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// 获取目标变量类型
        /// </summary>
        private string GetTargetVariableType()
        {
            try
            {
                var targetVar = _globalVariableManager.FindVariableByName(cmbTargetVariable.Text);
                return targetVar?.VarType;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 更新验证UI
        /// </summary>
        private void UpdateValidationUI(ValidationResult result)
        {
            try
            {
                rtbValidationResult.Clear();
                rtbValidationResult.Text = result.Message;

                // 设置颜色
                if (result.IsValid)
                {
                    rtbValidationResult.ForeColor = Color.FromArgb(40, 167, 69);
                    btnOK.Enabled = true;
                    btnTest.Enabled = true;
                }
                else
                {
                    rtbValidationResult.ForeColor = Color.FromArgb(220, 53, 69);
                    btnOK.Enabled = false;
                    btnTest.Enabled = false;
                }

                // 如果有警告但验证通过，使用橙色
                if (result.IsValid && result.Warnings?.Any() == true)
                {
                    rtbValidationResult.ForeColor = Color.FromArgb(255, 193, 7);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更新验证UI时发生错误");
            }
        }

        /// <summary>
        /// 生成验证消息
        /// </summary>
        private string GenerateValidationMessage(ValidationResult result)
        {
            var messages = new List<string>();

            if (result.IsValid)
            {
                messages.Add("✅ 配置验证通过");
            }
            else
            {
                messages.Add("❌ 配置验证失败");
            }

            if (result.Errors?.Any() == true)
            {
                messages.AddRange(result.Errors.Select(e => $"❌ {e}"));
            }

            if (result.Warnings?.Any() == true)
            {
                messages.AddRange(result.Warnings.Select(w => $"⚠️ {w}"));
            }

            return string.Join(Environment.NewLine, messages);
        }

        /// <summary>
        /// 转换为布尔值
        /// </summary>
        private bool ToBool(object value)
        {
            if (value is bool boolValue)
                return boolValue;

            if (value == null)
                return false;

            var stringValue = value.ToString();
            if (bool.TryParse(stringValue, out var parsedBool))
                return parsedBool;

            if (double.TryParse(stringValue, out var numericValue))
                return Math.Abs(numericValue) > double.Epsilon;

            return !string.IsNullOrEmpty(stringValue);
        }

        /// <summary>
        /// 绑定事件（重写基类方法）
        /// </summary>
        private void BindEvents()
        {
            try
            {
                // 变量选择改变事件
                cmbTargetVariable.SelectedIndexChanged += CmbTargetVariable_SelectedIndexChanged;

                // 赋值方式改变事件
                cmbAssignmentType.SelectedIndexChanged += CmbAssignmentType_SelectedIndexChanged;

                // 内容改变事件
                txtAssignmentContent.TextChanged += TxtAssignmentContent_TextChanged;
                txtCondition.TextChanged += TxtCondition_TextChanged;
                txtDescription.TextChanged += TxtDescription_TextChanged;
                chkEnabled.CheckedChanged += ChkEnabled_CheckedChanged;

                // 按钮事件
                btnOK.Click += btnOK_Click;
                btnCancel.Click += btnCancel_Click;
                btnTest.Click += btnTest_Click;
                btnExpressionBuilder.Click += btnExpressionBuilder_Click;
                btnHelp.Click += btnHelp_Click;

                // 窗体事件
                this.FormClosing += Form_VariableAssignment_FormClosing;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "绑定事件时发生错误");
            }
        }

        #endregion


        #region 事件处理

        /// <summary>
        /// 目标变量选择改变事件
        /// </summary>
        private void CmbTargetVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            _hasUnsavedChanges = true;
            RestartValidationTimer();
            RestartPreviewTimer();
        }

        /// <summary>
        /// 赋值方式选择改变事件
        /// </summary>
        private void CmbAssignmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            UpdateUIBasedOnAssignmentType();
            _hasUnsavedChanges = true;
            RestartValidationTimer();
            RestartPreviewTimer();
        }

        /// <summary>
        /// 赋值内容改变事件
        /// </summary>
        private void TxtAssignmentContent_TextChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            _hasUnsavedChanges = true;
            RestartValidationTimer();
            RestartPreviewTimer();
        }

        /// <summary>
        /// 执行条件改变事件
        /// </summary>
        private void TxtCondition_TextChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            _hasUnsavedChanges = true;
            RestartValidationTimer();
        }

        /// <summary>
        /// 描述改变事件
        /// </summary>
        private void TxtDescription_TextChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;
            _hasUnsavedChanges = true;
        }

        /// <summary>
        /// 启用状态改变事件
        /// </summary>
        private void ChkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;
            _hasUnsavedChanges = true;
        }
        #endregion

        #region IParameterForm<Parameter_Detection> 接口实现
        public void PopulateControls(Form_VariableAssignment parameter)
        {
            throw new NotImplementedException();
        }

        protected override void SetDefaultValues()
        {
            SetDefaultValues();
        }

        public bool ValidateTypedParameters()
        {
            throw new NotImplementedException();
        }

        public Form_VariableAssignment CollectTypedParameters()
        {
            throw new NotImplementedException();
        }

        public Form_VariableAssignment ConvertParameter(object stepParameter)
        {
            throw new NotImplementedException();
        }

        void IParameterForm<Form_VariableAssignment>.SetDefaultValues()
        {
            SetDefaultValues();
        }



        #endregion

    }

    #region 辅助类


    /// <summary>
    /// 测试结果
    /// </summary>
    public class TestResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// 赋值方式枚举
    /// </summary>
    public enum AssignmentTypeEnum
    {
        DirectAssignment,       // 直接赋值
        ExpressionCalculation,  // 表达式计算
        VariableCopy,          // 变量复制
        PLCRead                // PLC读取
    }

    #endregion
}