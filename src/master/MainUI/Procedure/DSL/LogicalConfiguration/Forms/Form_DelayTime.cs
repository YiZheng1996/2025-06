using AntdUI;
using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using Newtonsoft.Json;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// 延时配置表单
    /// </summary>
    public partial class Form_DelayTime : UIForm
    {
        private const double DEFAULT_DELAY_TIME = 1000.0;
        private readonly SingletonStatus _status;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Form_DelayTime()
        {
            InitializeComponent();
            _status = SingletonStatus.Instance;
            LoadDelayParameters();
        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="status">单例状态实例</param>
        public Form_DelayTime(SingletonStatus status)
        {
            InitializeComponent();
            _status = status;
            LoadDelayParameters();
        }

        /// <summary>
        /// 加载延时参数
        /// </summary>
        private void LoadDelayParameters()
        {
            try
            {
                var currentStep = GetCurrentStep();
                if (currentStep?.StepParameter is null)
                {
                    SetDefaultDelayTime();
                    return;
                }

                var delayTime = ExtractDelayTime(currentStep.StepParameter);
                txtTime.Text = delayTime.ToString();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载延时参数失败", ex);
                SetDefaultDelayTime();
            }
        }

        /// <summary>
        /// 获取当前步骤
        /// </summary>
        private ChildModel GetCurrentStep()
        {
            var steps = _status.IempSteps;
            var idx = _status.StepNum;

            return (steps != null && idx >= 0 && idx < steps.Count) ? steps[idx] : null;
        }

        /// <summary>
        /// 提取延时时间
        /// </summary>
        private static double ExtractDelayTime(object paramObj)
        {
            if (paramObj is Parameter_DelayTime param)
            {
                return param.T;
            }

            try
            {
                var deserializedParam = JsonConvert.DeserializeObject<Parameter_DelayTime>(paramObj.ToString() ?? "");
                return deserializedParam?.T ?? DEFAULT_DELAY_TIME;
            }
            catch
            {
                return DEFAULT_DELAY_TIME;
            }
        }

        /// <summary>
        /// 设置默认延时时间
        /// </summary>
        private void SetDefaultDelayTime()
        {
            txtTime.Text = DEFAULT_DELAY_TIME.ToString();
        }

        /// <summary>
        /// 保存参数按钮点击事件
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var currentStep = GetCurrentStep();
                if (currentStep is null)
                {
                    MessageHelper.MessageOK("步骤索引无效，无法保存参数。", TType.Error);
                    return;
                }

                if (!ValidateAndSaveDelayTime(currentStep))
                {
                    return;
                }

                MessageHelper.MessageOK("参数已暂存，主界面点击保存后才会写入文件。", TType.Info);
                Close();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存延时参数失败", ex);
                MessageHelper.MessageOK($"保存参数失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 验证并保存延时时间
        /// </summary>
        private bool ValidateAndSaveDelayTime(ChildModel step)
        {
            if (!double.TryParse(txtTime.Text, out double delayTime))
            {
                MessageHelper.MessageOK("请输入有效的延时时间。", TType.Warn);
                return false;
            }

            step.StepParameter = new Parameter_DelayTime { T = delayTime };
            return true;
        }
    }
}
