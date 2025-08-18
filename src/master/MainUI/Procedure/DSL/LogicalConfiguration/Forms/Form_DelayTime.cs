using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// 延时参数配置表单 - 解决方法冲突问题
    /// </summary>
    public partial class Form_DelayTime : BaseParameterForm, IParameterForm<Parameter_DelayTime>
    {
        private const double DEFAULT_DELAY_TIME = 1000.0;

        /// <summary>
        /// 参数对象
        /// </summary>
        public Parameter_DelayTime Parameter { get; set; }

        #region 构造函数

        public Form_DelayTime()
        {
            InitializeComponent();
            InitializeForm();
        }

        public Form_DelayTime(Parameter_DelayTime parameter) : this()
        {
            Parameter = parameter ?? new Parameter_DelayTime();
        }

        private void InitializeForm()
        {
            Parameter = new Parameter_DelayTime();
            BindEvents();
        }

        private void BindEvents()
        {
            if (BtnSave != null) BtnSave.Click += OnSaveClick;
            //if (BtnCancel != null) BtnCancel.Click += OnCancelClick;
            //if (BtnReset != null) BtnReset.Click += OnResetClick;

            // 绑定输入验证事件
            if (txtTime != null)
            {
                txtTime.KeyPress += txtTime_KeyPress;
                txtTime.Leave += txtTime_Leave;
            }
        }

        #endregion

        #region 重写基类方法

        protected override void LoadParameterFromStep(object stepParameter)
        {
            Parameter = ConvertParameter(stepParameter);
            PopulateControls(Parameter);
        }

        protected override void SetDefaultValues()
        {
            Parameter = new Parameter_DelayTime { T = DEFAULT_DELAY_TIME };
            PopulateControls(Parameter);
        }

        protected override bool ValidateParameters()
        {
            // 调用接口的类型安全验证方法
            return ValidateTypedParameters();
        }

        protected override object CollectParameters()
        {
            // 调用接口的类型安全收集方法
            return CollectTypedParameters();
        }

        #endregion

        #region 实现泛型接口 - 使用新的方法名

        /// <summary>
        /// 填充控件
        /// </summary>
        public void PopulateControls(Parameter_DelayTime parameter)
        {
            if (parameter == null) return;

            SetControlText(txtTime, parameter.T.ToString());
        }

        /// <summary>
        /// 验证参数 - 接口方法，新命名
        /// </summary>
        public bool ValidateTypedParameters()
        {
            string timeText = GetControlText(txtTime);

            if (string.IsNullOrEmpty(timeText))
            {
                ShowValidationError("请输入延时时间。", txtTime);
                return false;
            }

            if (!double.TryParse(timeText, out double delayTime))
            {
                ShowValidationError("请输入有效的数字。", txtTime);
                return false;
            }

            if (delayTime < 0)
            {
                ShowValidationError("延时时间不能为负数。", txtTime);
                return false;
            }

            if (delayTime > 3600000) // 最大1小时
            {
                ShowValidationError("延时时间不能超过1小时(3600000毫秒)。", txtTime);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 收集参数 - 接口方法
        /// </summary>
        public Parameter_DelayTime CollectTypedParameters()
        {
            string timeText = GetControlText(txtTime);

            if (double.TryParse(timeText, out double delayTime))
            {
                return new Parameter_DelayTime { T = delayTime };
            }

            return new Parameter_DelayTime { T = DEFAULT_DELAY_TIME };
        }

        /// <summary>
        /// 转换参数
        /// </summary>
        public Parameter_DelayTime ConvertParameter(object stepParameter)
        {
            return ConvertJsonParameter<Parameter_DelayTime>(stepParameter);
        }

        #endregion

        #region 输入验证事件

        private void txtTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许数字、小数点、退格键
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            // 只允许一个小数点
            if (e.KeyChar == '.' && txtTime.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void txtTime_Leave(object sender, EventArgs e)
        {
            if (IsLoading || DesignMode) return;

            string timeText = GetControlText(txtTime);

            if (!string.IsNullOrEmpty(timeText) &&
                double.TryParse(timeText, out double value))
            {
                SetControlText(txtTime, value.ToString("F1"));
            }
        }

        void IParameterForm<Parameter_DelayTime>.SetDefaultValues()
        {
            SetDefaultValues();
        }

        #endregion
    }
}
