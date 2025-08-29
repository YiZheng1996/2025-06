using AntdUI;
using MainUI.Procedure.DSL.LogicalConfiguration.Engine;
using MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure;
using MainUI.Procedure.DSL.LogicalConfiguration.LogicalManager;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using MainUI.Procedure.DSL.LogicalConfiguration.Services;
using MainUI.Procedure.DSL.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// 变量赋值工具窗体
    /// </summary>
    public partial class Form_VariableAssignment : BaseParameterForm, IParameterForm<Parameter_VariableAssignment>
    {
        #region 私有字段

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

        #endregion

        #region 属性

        /// <summary>
        /// 参数对象
        /// </summary>
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

        #endregion

        #region 构造函数

        /// <summary>
        /// 设计器构造函数
        /// </summary>
        public Form_VariableAssignment()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                InitializeForm();
            }
        }
        /// <summary>
        /// 带参数的依赖注入构造函数
        /// </summary>
        public Form_VariableAssignment(
            IWorkflowStateService workflowState,
            ILogger<Form_VariableAssignment> logger,
            IPLCManager pLcManager)
            : base(workflowState, logger, pLcManager)
        {
            InitializeComponent();
            InitializeForm();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化表单
        /// </summary>
        private void InitializeForm()
        {
            if (DesignMode) return;

            try
            {
                _isInitializing = true;

                // 获取服务（优先使用基类提供的服务，再尝试从ServiceProvider获取）
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                var plcManager = _plcManager ?? Program.ServiceProvider?.GetService<IPLCManager>();

                if (globalVariableManager == null)
                {
                    Logger?.LogWarning("无法获取GlobalVariableManager服务");
                }

                // 初始化核心引擎
                InitializeEngines(globalVariableManager, plcManager);

                // 设置窗体样式
                InitializeFormStyle();

                // 初始化定时器
                InitializeTimers();

                // 初始化界面数据
                InitializeVariableComboBox();

                // 初始化赋值类型下拉框
                InitializeAssignmentType();

                // 绑定事件
                BindEvents();

                // 初始验证
                ValidateConfigurationAsync();

                _isInitializing = false;
                Logger?.LogInformation("变量赋值工具窗体加载完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "变量赋值表单初始化失败");
                MessageHelper.MessageOK($"初始化失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 初始化引擎
        /// </summary>
        private void InitializeEngines(GlobalVariableManager globalVariableManager, IPLCManager plcManager)
        {
            try
            {
                if (globalVariableManager != null)
                {
                    // 初始化核心引擎 
                    var expressionValidator = new ExpressionValidator(globalVariableManager);
                    var assignmentEngine = new VariableAssignmentEngine(globalVariableManager, plcManager, expressionValidator);

                    // 使用反射设置私有字段（保持兼容性）
                    var expressionField = typeof(Form_VariableAssignment).GetField("_expressionValidator",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    expressionField?.SetValue(this, expressionValidator);

                    var assignmentField = typeof(Form_VariableAssignment).GetField("_assignmentEngine",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    assignmentField?.SetValue(this, assignmentEngine);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化引擎失败");
            }
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

            // 预览定时器，默认刷新速率500ms - 延迟预览更新
            _previewTimer = new System.Windows.Forms.Timer
            {
                Interval = 500
            };
            _previewTimer.Tick += (s, e) =>
            {
                _previewTimer.Stop();
                RefreshPreviewAsync();
            };
        }

        /// <summary>
        /// 初始化赋值类型下拉框 - 使用枚举绑定
        /// </summary>
        private void InitializeAssignmentType()
        {
            if (cmbAssignmentType != null)
            {
                cmbAssignmentType.DataSource = EnumHelper.GetDisplayItems<AssignmentTypeEnum>();
                cmbAssignmentType.DisplayMember = "DisplayName";
                cmbAssignmentType.ValueMember = "Value";
                cmbAssignmentType.SelectedValue = AssignmentTypeEnum.DirectAssignment;
            }

        }

        /// <summary>
        /// 初始化变量下拉框 
        /// </summary>
        private void InitializeVariableComboBox()
        {
            try
            {
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                if (globalVariableManager != null && cmbTargetVariable != null)
                {
                    var variables = globalVariableManager.GetAllVariables();
                    var variableItems = variables.Select(v => new
                    {
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
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化变量下拉框失败");
            }
        }

        /// <summary>
        /// 绑定事件 
        /// </summary>
        private void BindEvents()
        {
            try
            {
                // 目标变量改变事件
                cmbTargetVariable.SelectedIndexChanged += CmbTargetVariable_SelectedIndexChanged;

                // 赋值方式改变事件
                cmbAssignmentType.SelectedIndexChanged += CmbAssignmentType_SelectedIndexChanged;

                // 内容改变事件
                txtAssignmentContent.TextChanged += TxtAssignmentContent_TextChanged;
                txtCondition.TextChanged += TxtCondition_TextChanged;
                txtDescription.TextChanged += TxtDescription_TextChanged;
                chkEnabled.CheckedChanged += ChkEnabled_CheckedChanged;

                // PLC相关事件
                CboPlcModule.SelectedIndexChanged += CboPlcModule_SelectedIndexChanged;
                CboPlcAddress.SelectedIndexChanged += CboPlcAddress_SelectedIndexChanged;

                // 按钮事件
                btnOK.Click += BtnOK_Click;
                btnCancel.Click += BtnCancel_Click;
                btnTest.Click += BtnTest_Click;
                btnExpressionBuilder.Click += BtnExpressionBuilder_Click;
                btnHelp.Click += BtnHelp_Click;

                // 窗体事件
                FormClosing += Form_VariableAssignment_FormClosing;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "绑定事件时发生错误");
            }
        }

        #endregion

        #region 参数加载与保存 

        /// <summary>
        /// 加载参数到界面 
        /// </summary>
        /// <param name="parameter">变量赋值参数</param>
        public async void LoadParameter(Parameter_VariableAssignment parameter)
        {
            _parameter = parameter ?? new Parameter_VariableAssignment();

            try
            {
                // 基本信息加载
                cmbTargetVariable.Text = _parameter.TargetVarName ?? "";
                cmbAssignmentType.SelectedValue = _parameter.AssignmentType;
                txtCondition.Text = _parameter.Condition ?? "";
                txtDescription.Text = _parameter.Description ?? "";
                chkEnabled.Checked = _parameter.IsAssignment;

                // 根据赋值类型加载相应数据
                switch (_parameter.AssignmentType)
                {
                    case AssignmentTypeEnum.PLCRead:
                        await Parameterplcs();// 先加载PLC模块列表
                        SetComboBoxValue(CboPlcModule, _parameter.DataSource.PlcConfig.ModuleName);// 设置PLC配置

                        // 如果有模块名，加载对应的地址列表
                        if (!string.IsNullOrEmpty(_parameter.DataSource.PlcConfig.ModuleName))
                        {
                            await LoadPlcAddresses(_parameter.DataSource.PlcConfig.ModuleName);
                            SetComboBoxValue(CboPlcAddress, _parameter.DataSource.PlcConfig.Address);
                        }
                        break;

                    default:
                        // 其他类型使用Expression字段
                        if (txtAssignmentContent != null)
                            txtAssignmentContent.Text = _parameter.Expression ?? "";
                        break;
                }

                // 更新UI布局
                UpdateUIBasedOnAssignmentType();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数时发生错误");
                MessageHelper.MessageOK($"加载参数失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 从表单加载到Parameter属性（基类兼容）
        /// </summary>
        private void LoadParameterToForm()
        {
            LoadParameter(_parameter);
        }

        /// <summary>
        /// 获取参数 
        /// </summary>
        private Parameter_VariableAssignment GetParameter()
        {
            var parameter = new Parameter_VariableAssignment
            {
                TargetVarName = cmbTargetVariable?.Text ?? "",
                AssignmentType = (AssignmentTypeEnum)cmbAssignmentType.SelectedValue,
                Condition = txtCondition?.Text ?? "",
                Description = txtDescription?.Text ?? "",
                IsAssignment = chkEnabled?.Checked ?? false,
                DataSource = new DataSourceConfig()
            };

            // 根据赋值类型设置数据
            switch (parameter.AssignmentType)
            {
                case AssignmentTypeEnum.PLCRead:
                    parameter.DataSource.SourceType = DataSourceType.PLC;
                    parameter.DataSource.PlcConfig.ModuleName = CboPlcModule?.Text ?? "";
                    parameter.DataSource.PlcConfig.Address = CboPlcAddress?.Text ?? "";
                    parameter.Expression = ""; // PLC读取时Expression为空
                    break;

                default:
                    parameter.Expression = txtAssignmentContent?.Text ?? "";
                    parameter.DataSource.SourceType = DataSourceType.Variable;
                    break;
            }

            return parameter;
        }

        /// <summary>
        /// 从表单保存到参数对象
        /// </summary>
        private void SaveFormToParameter()
        {
            if (_parameter == null) return;

            try
            {
                _parameter.TargetVarName = cmbTargetVariable?.Text ?? "";
                _parameter.AssignmentType = (AssignmentTypeEnum)cmbAssignmentType.SelectedValue;
                _parameter.Condition = txtCondition?.Text ?? "";
                _parameter.Description = txtDescription?.Text ?? "";
                _parameter.IsAssignment = chkEnabled?.Checked ?? false;

                // 根据赋值类型保存数据
                switch (_parameter.AssignmentType)
                {
                    case AssignmentTypeEnum.PLCRead:
                        _parameter.DataSource.SourceType = DataSourceType.PLC;
                        _parameter.DataSource.PlcConfig.ModuleName = CboPlcModule?.Text ?? "";
                        _parameter.DataSource.PlcConfig.Address = CboPlcAddress?.Text ?? "";
                        _parameter.Expression = ""; // PLC读取时Expression为空
                        break;

                    default:
                        _parameter.Expression = txtAssignmentContent?.Text ?? "";
                        _parameter.DataSource.SourceType = DataSourceType.Variable; // 默认
                        break;
                }

                _hasUnsavedChanges = false;
                Logger?.LogDebug("表单数据已保存到参数对象");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存表单数据失败");
                throw;
            }
        }

        /// <summary>
        /// 加载所有PLC参数
        /// </summary>
        private async Task Parameterplcs()
        {
            if (CboPlcModule.Items.Count == 0)
            {

            }
            CboPlcModule.Clear();
            CboPlcAddress.Clear();
            try
            {
                var modules = await PLCManager.GetModuleTagsAsync();
                foreach (var module in modules)
                {
                    CboPlcModule.Items.Add(module.Key);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载PLC模块失败");
            }
        }

        /// <summary>
        /// 加载指定PLC模块的地址列表
        /// </summary>
        private async Task LoadPlcAddresses(string moduleName)
        {
            if (string.IsNullOrEmpty(moduleName) || CboPlcAddress == null) return;

            try
            {
                CboPlcAddress.Clear();
                var modules = await PLCManager.GetModuleTagsAsync();
                if (modules.TryGetValue(moduleName, out List<string> addresses))
                {
                    foreach (var address in addresses)
                    {
                        CboPlcAddress.Items.Add(address);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载PLC模块[{moduleName}]地址失败", moduleName);
            }
        }

        /// <summary>
        /// 安全设置ComboBox的值
        /// </summary>
        private void SetComboBoxValue(UIComboBox comboBox, string value)
        {
            if (comboBox == null || string.IsNullOrEmpty(value)) return;

            try
            {
                // 方法1：先尝试在Items中查找匹配项
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    if (comboBox.Items[i].ToString().Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        comboBox.SelectedIndex = i;
                        return;
                    }
                }

                // 方法2：如果没有找到匹配项，且ComboBox允许编辑，则设置Text属性
                if (comboBox.DropDownStyle == UIDropDownStyle.DropDown)
                {
                    comboBox.Text = value;
                }
                else
                {
                    // 方法3：对于DropDownList类型，如果找不到匹配项，添加该项
                    comboBox.Items.Add(value);
                    comboBox.SelectedItem = value;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogWarning("设置ComboBox值失败: {value}", ex);
                // 最后的备用方案
                try
                {
                    comboBox.Text = value;
                }
                catch
                {
                    // 忽略最终的设置失败
                }
            }
        }

        #endregion

        #region 基类重写方法

        /// <summary>
        /// 从步骤参数加载
        /// </summary>
        protected override void LoadParameterFromStep(object stepParameter)
        {
            try
            {
                Parameter_VariableAssignment loadedParameter = null;

                // 尝试直接类型转换
                if (stepParameter is Parameter_VariableAssignment directParam)
                {
                    loadedParameter = directParam;
                    Logger?.LogDebug("直接获取Parameter_VariableAssignment参数");
                }
                // 尝试JSON反序列化
                else if (stepParameter != null)
                {
                    try
                    {
                        string jsonString = stepParameter is string s ? s : JsonConvert.SerializeObject(stepParameter);
                        loadedParameter = JsonConvert.DeserializeObject<Parameter_VariableAssignment>(jsonString);
                        Logger?.LogDebug("JSON反序列化获取Parameter_VariableAssignment参数");
                    }
                    catch (JsonException jsonEx)
                    {
                        Logger?.LogWarning(jsonEx, "JSON反序列化失败，使用默认参数");
                        loadedParameter = null;
                    }
                }

                // 如果加载成功，设置参数并加载到界面
                if (loadedParameter != null)
                {
                    _parameter = loadedParameter;
                    Logger?.LogInformation("成功加载检测参数: {Description}", _parameter.Description);
                }
                else
                {
                    // 加载失败，使用默认值
                    Logger?.LogWarning("加载参数失败，使用默认参数");
                    SetDefaultValues();
                    return;
                }

                // 加载参数到表单控件
                LoadParameterToForm();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数时发生错误");
                MessageHelper.MessageOK($"加载参数失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        protected override void SetDefaultValues()
        {
            _parameter = new Parameter_VariableAssignment
            {
                TargetVarName = "",
                Expression = "",
                Condition = "",
                Description = $"变量赋值步骤 {_workflowState?.StepNum + 1}",
                IsAssignment = true,
                AssignmentType = AssignmentTypeEnum.DirectAssignment,
                DataSource = new DataSourceConfig()
            };

            Logger?.LogDebug("设置变量赋值参数默认值");
            LoadParameterToForm();
        }

        /// <summary>
        /// 验证参数
        /// </summary>
        protected override bool ValidateParameters()
        {
            return ValidateInput();
        }

        /// <summary>
        /// 收集参数
        /// </summary>
        protected override object CollectParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        #endregion

        #region 验证逻辑 

        /// <summary>
        /// 验证输入
        /// </summary>
        private bool ValidateInput()
        {
            try
            {
                // 验证目标变量名
                if (string.IsNullOrWhiteSpace(cmbTargetVariable?.Text))
                {
                    MessageHelper.MessageOK("请选择或输入目标变量名", TType.Warn);
                    cmbTargetVariable?.Focus();
                    return false;
                }

                // 验证目标变量是否存在
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                var targetVar = globalVariableManager?.FindVariableByName(cmbTargetVariable.Text);
                if (targetVar == null)
                {
                    MessageHelper.MessageOK($"目标变量 '{cmbTargetVariable.Text}' 不存在", TType.Warn);
                    cmbTargetVariable?.Focus();
                    return false;
                }

                // 获取当前选择的赋值类型
                if (cmbAssignmentType?.SelectedValue == null)
                {
                    MessageHelper.MessageOK("请选择赋值方式", TType.Warn);
                    cmbAssignmentType?.Focus();
                    return false;
                }

                var assignmentType = (AssignmentTypeEnum)cmbAssignmentType.SelectedValue;

                // 根据赋值类型进行不同的验证
                switch (assignmentType)
                {
                    case AssignmentTypeEnum.DirectAssignment:
                        if (string.IsNullOrWhiteSpace(txtAssignmentContent?.Text))
                        {
                            MessageHelper.MessageOK("请输入要赋的值", TType.Warn);
                            txtAssignmentContent?.Focus();
                            return false;
                        }
                        break;

                    case AssignmentTypeEnum.ExpressionCalculation:
                        if (string.IsNullOrWhiteSpace(txtAssignmentContent?.Text))
                        {
                            MessageHelper.MessageOK("请输入计算表达式", TType.Warn);
                            txtAssignmentContent?.Focus();
                            return false;
                        }

                        // 验证表达式语法
                        if (_expressionValidator != null)
                        {
                            var context = new ValidationContext
                            {
                                TargetVariableName = cmbTargetVariable.Text,
                                TargetVariableType = targetVar.VarType
                            };

                            var expressionResult = _expressionValidator.ValidateExpression(txtAssignmentContent.Text, context);
                            if (!expressionResult.IsValid)
                            {
                                var errorMsg = string.Join("\n", expressionResult.Errors);
                                MessageHelper.MessageOK($"表达式语法错误：\n{errorMsg}", TType.Error);
                                txtAssignmentContent?.Focus();
                                return false;
                            }
                        }
                        break;

                    case AssignmentTypeEnum.VariableCopy:
                        if (string.IsNullOrWhiteSpace(txtAssignmentContent?.Text))
                        {
                            MessageHelper.MessageOK("请输入源变量名", TType.Warn);
                            txtAssignmentContent?.Focus();
                            return false;
                        }

                        // 验证源变量是否存在
                        var sourceVarName = txtAssignmentContent.Text.Trim();
                        if (sourceVarName.StartsWith("{") && sourceVarName.EndsWith("}"))
                        {
                            sourceVarName = sourceVarName.Substring(1, sourceVarName.Length - 2);
                        }

                        var sourceVar = globalVariableManager?.FindVariableByName(sourceVarName);
                        if (sourceVar == null)
                        {
                            MessageHelper.MessageOK($"源变量 '{sourceVarName}' 不存在", TType.Warn);
                            txtAssignmentContent?.Focus();
                            return false;
                        }
                        break;

                    case AssignmentTypeEnum.PLCRead:
                        // 验证PLC模块
                        if (string.IsNullOrWhiteSpace(CboPlcModule?.Text))
                        {
                            MessageHelper.MessageOK("请选择PLC模块", TType.Warn);
                            CboPlcModule?.Focus();
                            return false;
                        }

                        // 验证PLC地址
                        if (string.IsNullOrWhiteSpace(CboPlcAddress?.Text))
                        {
                            MessageHelper.MessageOK("请选择PLC地址", TType.Warn);
                            CboPlcAddress?.Focus();
                            return false;
                        }

                        // 可选：验证PLC配置有效性
                        var plcManager = _plcManager ?? Program.ServiceProvider?.GetService<IPLCManager>();
                        if (plcManager != null)
                        {
                            var plcConfig = new PlcAddressConfig
                            {
                                ModuleName = CboPlcModule.Text,
                                Address = CboPlcAddress.Text
                            };

                            if (!plcManager.ValidatePlcConfig(plcConfig))
                            {
                                MessageHelper.MessageOK("PLC配置无效，请检查模块名和地址", TType.Warn);
                                return false;
                            }
                        }
                        break;

                    default:
                        MessageHelper.MessageOK("不支持的赋值方式", TType.Error);
                        return false;
                }

                // 验证执行条件（如果有）
                if (!string.IsNullOrWhiteSpace(txtCondition?.Text))
                {
                    if (_expressionValidator != null)
                    {
                        var conditionResult = _expressionValidator.ValidateExpression(txtCondition.Text);
                        if (!conditionResult.IsValid)
                        {
                            var errorMsg = string.Join("\n", conditionResult.Errors);
                            MessageHelper.MessageOK($"执行条件语法错误：\n{errorMsg}", TType.Error);
                            txtCondition?.Focus();
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证输入时发生异常");
                MessageHelper.MessageOK($"验证失败：{ex.Message}", TType.Error);
                return false;
            }
        }

        /// <summary>
        /// 异步验证配置
        /// </summary>
        private ValidationResult ValidateConfigurationAsync()
        {
            try
            {
                var parameter = GetParameter();
                var errors = new List<string>();
                var warnings = new List<string>();

                // 基本验证
                if (string.IsNullOrWhiteSpace(parameter.TargetVarName))
                {
                    errors.Add("目标变量名不能为空");
                }
                else
                {
                    // 验证目标变量是否存在
                    var targetVar = GetTargetVariableType();
                    if (targetVar == null)
                    {
                        errors.Add($"目标变量 '{parameter.TargetVarName}' 不存在");
                    }
                }

                // 根据赋值类型进行不同的验证
                switch (parameter.AssignmentType)
                {
                    case AssignmentTypeEnum.DirectAssignment:
                        if (string.IsNullOrWhiteSpace(parameter.Expression))
                        {
                            errors.Add("直接赋值的值不能为空");
                        }
                        break;

                    case AssignmentTypeEnum.ExpressionCalculation:
                        if (string.IsNullOrWhiteSpace(parameter.Expression))
                        {
                            errors.Add("表达式不能为空");
                        }
                        else
                        {
                            // 验证表达式语法
                            var context = new ValidationContext
                            {
                                TargetVariableName = parameter.TargetVarName,
                                TargetVariableType = GetTargetVariableType()
                            };

                            var expressionResult = _expressionValidator?.ValidateExpression(parameter.Expression, context);
                            if (expressionResult != null)
                            {
                                if (!expressionResult.IsValid)
                                    errors.AddRange(expressionResult.Errors);
                                warnings.AddRange(expressionResult.Warnings);
                            }
                        }
                        break;

                    case AssignmentTypeEnum.VariableCopy:
                        if (string.IsNullOrWhiteSpace(parameter.Expression))
                        {
                            errors.Add("源变量名不能为空");
                        }
                        else
                        {
                            // 验证源变量是否存在
                            var sourceVarName = parameter.Expression.Trim();
                            if (sourceVarName.StartsWith("{") && sourceVarName.EndsWith("}"))
                            {
                                sourceVarName = sourceVarName.Substring(1, sourceVarName.Length - 2);
                            }

                            var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                            var sourceVar = globalVariableManager?.FindVariableByName(sourceVarName);
                            if (sourceVar == null)
                            {
                                errors.Add($"源变量 '{sourceVarName}' 不存在");
                            }
                        }
                        break;

                    case AssignmentTypeEnum.PLCRead:
                        // PLC读取验证
                        if (string.IsNullOrWhiteSpace(parameter.DataSource.PlcConfig.ModuleName))
                        {
                            errors.Add("请选择PLC模块");
                        }
                        if (string.IsNullOrWhiteSpace(parameter.DataSource.PlcConfig.Address))
                        {
                            errors.Add("请选择PLC地址");
                        }

                        // 验证PLC连接状态（可选）
                        if (!string.IsNullOrEmpty(parameter.DataSource.PlcConfig.ModuleName))
                        {
                            var plcManager = _plcManager ?? Program.ServiceProvider?.GetService<IPLCManager>();
                            if (plcManager != null)
                            {
                                // 异步检查PLC模块连接状态
                                Task.Run(async () =>
                                {
                                    try
                                    {
                                        var isOnline = await plcManager.IsModuleOnlineAsync(parameter.DataSource.PlcConfig.ModuleName);
                                        if (!isOnline)
                                        {
                                            warnings.Add($"PLC模块 '{parameter.DataSource.PlcConfig.ModuleName}' 可能未连接");
                                        }
                                    }
                                    catch
                                    {
                                        warnings.Add("无法检查PLC模块连接状态");
                                    }
                                });
                            }
                        }
                        break;
                }

                // 验证条件表达式（如果有）
                if (!string.IsNullOrWhiteSpace(parameter.Condition))
                {
                    var conditionResult = _expressionValidator?.ValidateExpression(parameter.Condition);
                    if (conditionResult != null)
                    {
                        if (!conditionResult.IsValid)
                            errors.AddRange(conditionResult.Errors.Select(e => $"条件验证: {e}"));
                        warnings.AddRange(conditionResult.Warnings.Select(w => $"条件警告: {w}"));
                    }
                }

                // 合并验证结果
                var combinedResult = new ValidationResult
                {
                    IsValid = errors.Count == 0,
                    Errors = errors,
                    Warnings = warnings,
                    Message = GenerateValidationMessage(new ValidationResult { IsValid = !errors.Any(), Errors = errors, Warnings = warnings })
                };

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
                Logger?.LogError(ex, "异步验证配置时发生错误");
                var errorResult = ValidationResult.Error($"验证过程发生错误: {ex.Message}");
                UpdateValidationUI(errorResult);
                return errorResult;
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
                messages.Add("✓ 配置验证通过");

                // 根据赋值类型添加特定提示
                var parameter = GetParameter();
                switch (parameter.AssignmentType)
                {
                    case AssignmentTypeEnum.PLCRead:
                        messages.Add($"  PLC模块: {parameter.DataSource.PlcConfig.ModuleName}");
                        messages.Add($"  PLC地址: {parameter.DataSource.PlcConfig.Address}");
                        break;
                    case AssignmentTypeEnum.ExpressionCalculation:
                        messages.Add("  表达式语法正确");
                        break;
                    case AssignmentTypeEnum.VariableCopy:
                        messages.Add("  源变量存在且可访问");
                        break;
                }
            }

            if (result.Errors?.Any() == true)
            {
                messages.Add("❌ 发现错误：");
                messages.AddRange(result.Errors.Select(e => $"  • {e}"));
            }

            if (result.Warnings?.Any() == true)
            {
                messages.Add("⚠️ 警告信息：");
                messages.AddRange(result.Warnings.Select(w => $"  • {w}"));
            }

            return string.Join("\n", messages);
        }

        #endregion

        #region 事件处理 

        /// <summary>
        /// PLC模块选择改变事件
        /// </summary>
        private async void CboPlcModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            var selectedModule = CboPlcModule?.Text;
            if (!string.IsNullOrEmpty(selectedModule))
            {
                await LoadPlcAddresses(selectedModule);
            }

            _hasUnsavedChanges = true;

            // 触发验证更新
            RestartValidationTimer();
        }

        /// <summary>
        /// PLC地址选择改变事件
        /// </summary>
        private void CboPlcAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;
            _hasUnsavedChanges = true;

            // 触发验证更新
            RestartValidationTimer();
        }

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

            txtAssignmentContent.Text = string.Empty;
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

        /// <summary>
        /// 窗体关闭事件 
        /// </summary>
        private void Form_VariableAssignment_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_hasUnsavedChanges && DialogResult != DialogResult.OK)
                {
                    var result = MessageHelper.MessageYes("有未保存的更改，是否放弃修改？");
                    if (result != DialogResult.OK)
                    {
                        e.Cancel = true;
                        return;
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
                Logger?.LogError(ex, "窗体关闭时发生错误");
            }
        }

        #endregion

        #region 按钮事件 

        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                btnOK.Enabled = false;
                btnOK.Text = "验证中...";

                // 验证配置
                var validationResult = ValidateConfigurationAsync();
                if (!validationResult.IsValid)
                {
                    MessageHelper.MessageOK($"配置验证失败：\n{string.Join("\n", validationResult.Errors)}", TType.Warn);
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

                // 使用基类方法，保存参数到流程中
                SaveParameters();

                _hasUnsavedChanges = false;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "确定按钮处理时发生错误");
                MessageHelper.MessageOK($"操作失败：{ex.Message}", TType.Error);
            }
            finally
            {
                btnOK.Enabled = true;
                btnOK.Text = "确定";
            }
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 测试按钮点击事件 
        /// </summary>
        private async void BtnTest_Click(object sender, EventArgs e)
        {
            try
            {
                btnTest.Enabled = false;
                btnTest.Text = "测试中...";

                // 先验证配置
                var validationResult = ValidateConfigurationAsync();
                if (!validationResult.IsValid)
                {
                    MessageHelper.MessageOK($"配置验证失败，无法测试：\n{string.Join("\n", validationResult.Errors)}", TType.Warn);
                    return;
                }

                // 获取当前参数
                var parameter = GetParameter();
                parameter.Condition = txtCondition?.Text?.Trim(); // 确保包含条件

                // 执行测试赋值
                if (_assignmentEngine != null)
                {
                    var testResult = await _assignmentEngine.ExecuteAssignmentAsync(parameter);

                    if (testResult.Success)
                    {
                        if (testResult.Skipped)
                        {
                            MessageHelper.MessageOK($"测试完成（已跳过）：\n{testResult.SkipReason}", TType.Info);
                        }
                        else
                        {
                            var message = $"测试成功！\n" +
                                        $"变量：{testResult.TargetVariableName}\n" +
                                        $"原值：{testResult.OldValue ?? "null"}\n" +
                                        $"新值：{testResult.NewValue ?? "null"}\n" +
                                        $"执行时间：{testResult.ExecutionTime.TotalMilliseconds:F2}ms";

                            MessageHelper.MessageOK(message, TType.Success);
                        }
                    }
                    else
                    {
                        var errorMessage = $"测试失败：{testResult.ErrorMessage}";
                        if (testResult.ValidationErrors?.Any() == true)
                        {
                            errorMessage += $"\n验证错误：\n{string.Join("\n", testResult.ValidationErrors)}";
                        }

                        MessageHelper.MessageOK(errorMessage, TType.Warn);
                    }

                    // 刷新预览
                    RefreshPreviewAsync();
                }
                else
                {
                    MessageHelper.MessageOK("赋值引擎未初始化，无法执行测试", TType.Warn);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "测试赋值时发生错误");
                MessageHelper.MessageOK($"测试失败：{ex.Message}", TType.Error);
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
        private void BtnExpressionBuilder_Click(object sender, EventArgs e)
        {
            try
            {
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                if (globalVariableManager == null || _expressionValidator == null)
                {
                    MessageHelper.MessageOK("表达式构建器服务未初始化", TType.Warn);
                    return;
                }

                // 创建表达式构建器对话框
                using var builder = new ExpressionBuilderDialog(globalVariableManager, _expressionValidator);
                builder.InitialExpression = txtAssignmentContent?.Text ?? "";
                builder.TargetVariableType = GetTargetVariableType();

                if (builder.ShowDialog(this) == DialogResult.OK)
                {
                    if (txtAssignmentContent != null)
                        txtAssignmentContent.Text = builder.GeneratedExpression;
                    _hasUnsavedChanges = true;

                    // 触发验证
                    RestartValidationTimer();
                    RestartPreviewTimer();
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "打开表达式构建器时发生错误");
                MessageHelper.MessageOK($"打开表达式构建器失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 帮助按钮点击事件
        /// </summary>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                var helpText = @"变量赋值工具使用说明：

1. 赋值方式：
   - 直接赋值：直接设置变量的值
   - 表达式计算：使用数学表达式计算结果
   - 从其他变量复制：复制其他变量的值
   - 从PLC读取：从PLC地址读取值

2. 表达式语法：
   - 使用 {变量名} 引用变量
   - 支持四则运算：+、-、*、/
   - 支持函数：Math.Sin、Math.Cos等

3. 执行条件：
   - 可选设置，为空时总是执行
   - 使用布尔表达式，如：{Var1} > 10

4. 测试功能：
   - 点击测试按钮可以验证配置
   - 不会影响实际流程执行";

                MessageHelper.MessageOK(helpText, TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "显示帮助时发生错误");
            }
        }

        #endregion

        #region 定时器方法 

        /// <summary>
        /// 重启验证定时器
        /// </summary>
        private void RestartValidationTimer()
        {
            _validationTimer?.Stop();
            _validationTimer?.Start();
        }

        /// <summary>
        /// 重启预览定时器
        /// </summary>
        private void RestartPreviewTimer()
        {
            _previewTimer?.Stop();
            _previewTimer?.Start();
        }

        #endregion

        #region UI更新方法 

        /// <summary>
        /// 根据赋值类型更新UI 
        /// </summary>
        private async void UpdateUIBasedOnAssignmentType()
        {
            if (cmbAssignmentType?.SelectedValue == null) return;

            var selectedType = (AssignmentTypeEnum)cmbAssignmentType.SelectedValue;

            // 显示/隐藏相应的面板
            pnlVoluationSource.Visible = true;
            pnlPlcSource.Visible = false;

            switch (selectedType)
            {
                case AssignmentTypeEnum.ExpressionCalculation:
                    txtAssignmentContent.Watermark = "输入计算表达式，如：{Var1} + {Var2} * 2";
                    btnExpressionBuilder.Visible = true;
                    break;
                case AssignmentTypeEnum.VariableCopy:
                    txtAssignmentContent.Watermark = "输入源变量名或使用 {变量名} 格式";
                    btnExpressionBuilder.Visible = false;
                    break;
                case AssignmentTypeEnum.PLCRead:
                    txtAssignmentContent.Watermark = "输入PLC地址";
                    btnExpressionBuilder.Visible = false;
                    pnlVoluationSource.Visible = false;
                    pnlPlcSource.Visible = true;
                    if (CboPlcModule.Items.Count > 0) break;
                    await Parameterplcs();
                    break;
                case AssignmentTypeEnum.DirectAssignment:
                    txtAssignmentContent.Watermark = "输入要赋的值，字符串请用引号包围";
                    btnExpressionBuilder.Visible = false;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 更新验证UI
        /// </summary>
        private void UpdateValidationUI(ValidationResult result)
        {
            try
            {
                if (rtbValidationResult != null)
                {
                    rtbValidationResult.Clear();
                    rtbValidationResult.Text = result.Message;

                    // 根据验证结果设置颜色和按钮状态
                    if (result.IsValid)
                    {
                        rtbValidationResult.ForeColor = Color.FromArgb(40, 167, 69); // 绿色

                        // 有警告时使用橙色
                        if (result.Warnings?.Any() == true)
                        {
                            rtbValidationResult.ForeColor = Color.FromArgb(255, 193, 7); // 橙色
                        }

                        if (btnOK != null) btnOK.Enabled = true;
                        if (btnTest != null) btnTest.Enabled = true;
                    }
                    else
                    {
                        rtbValidationResult.ForeColor = Color.FromArgb(220, 53, 69); // 红色
                        if (btnOK != null) btnOK.Enabled = false;
                        if (btnTest != null) btnTest.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "更新验证UI失败");
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取目标变量类型
        /// </summary>
        private string GetTargetVariableType()
        {
            try
            {
                var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                var targetVar = globalVariableManager?.FindVariableByName(cmbTargetVariable?.Text);
                return targetVar?.VarType;
            }
            catch
            {
                return null;
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
                previewLines.Add($"赋值方式：{parameter.AssignmentType.GetDescription()}");

                // 根据赋值类型显示内容
                switch (parameter.AssignmentType)
                {
                    case AssignmentTypeEnum.PLCRead:
                        previewLines.Add($"PLC模块：{parameter.DataSource.PlcConfig.ModuleName}");
                        previewLines.Add($"PLC地址：{parameter.DataSource.PlcConfig.Address}");
                        break;
                    default:
                        previewLines.Add($"赋值内容：{parameter.Expression}");
                        break;
                }

                if (!string.IsNullOrWhiteSpace(parameter.Condition))
                {
                    previewLines.Add($"执行条件：{parameter.Condition}");
                }

                previewLines.Add($"状态：{(parameter.IsAssignment ? "启用" : "禁用")}");
                previewLines.Add("");

                // 计算预期结果
                try
                {
                    var globalVariableManager = _globalVariable ?? Program.ServiceProvider?.GetService<GlobalVariableManager>();
                    var targetVar = globalVariableManager?.FindVariableByName(parameter.TargetVarName);
                    if (targetVar != null)
                    {
                        previewLines.Add($"当前值：{targetVar.VarValue ?? "null"}");

                        // 使用表达式验证器计算预期值
                        var calculationResult = _expressionValidator?.CalculateExpectedValue(parameter.Expression);
                        if (calculationResult?.Success == true)
                        {
                            previewLines.Add($"预期值：{calculationResult.Value ?? "null"}");
                        }
                        else
                        {
                            previewLines.Add($"预期值：计算失败 - {calculationResult?.ErrorMessage}");
                        }
                    }
                    else
                    {
                        previewLines.Add("变量不存在或未找到");
                    }
                }
                catch (Exception ex)
                {
                    previewLines.Add($"预览计算异常：{ex.Message}");
                }

                // 更新预览界面
                var previewText = string.Join("\n", previewLines);
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        if (rtbPreviewResult != null)
                            rtbPreviewResult.Text = previewText;
                    }));
                }
                else
                {
                    if (rtbPreviewResult != null)
                        rtbPreviewResult.Text = previewText;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "刷新预览时发生错误");
            }
        }

        #endregion

        #region IParameterForm<Parameter_VariableAssignment> 接口实现

        public void PopulateControls(Parameter_VariableAssignment parameter)
        {
            Parameter = parameter;
        }

        void IParameterForm<Parameter_VariableAssignment>.SetDefaultValues()
        {
            SetDefaultValues();
        }

        public bool ValidateTypedParameters()
        {
            return ValidateInput();
        }

        public Parameter_VariableAssignment CollectTypedParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        public Parameter_VariableAssignment ConvertParameter(object stepParameter)
        {
            if (stepParameter is Parameter_VariableAssignment paramObj)
                return paramObj;

            if (stepParameter is string jsonStr && !string.IsNullOrEmpty(jsonStr))
            {
                try
                {
                    return JsonConvert.DeserializeObject<Parameter_VariableAssignment>(jsonStr)
                        ?? new Parameter_VariableAssignment();
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "反序列化参数失败");
                    return new Parameter_VariableAssignment();
                }
            }

            return new Parameter_VariableAssignment();
        }

        #endregion
    }

    #region 辅助类

    /// <summary>
    /// 赋值方式枚举
    /// </summary>
    public enum AssignmentTypeEnum
    {
        /// <summary>
        /// 直接赋值
        /// </summary>
        [Description("直接赋值")]
        DirectAssignment,

        /// <summary>
        /// 表达式计算
        /// </summary>
        [Description("表达式计算")]
        ExpressionCalculation,

        /// <summary>
        /// 从其他变量复制
        /// </summary>
        [Description("从其他变量复制")]
        VariableCopy,

        /// <summary>
        /// 从PLC读取
        /// </summary>
        [Description("从PLC读取")]
        PLCRead
    }

    #endregion
}