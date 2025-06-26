using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using System.Text.Json;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    public partial class Form_DelayTime : UIForm
    {
        private readonly JsonManager _jsonManager;

        private static readonly JsonSerializerOptions CachedJsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public Form_DelayTime()
        {
            InitializeComponent();
            _jsonManager = new JsonManager();
            InitForm();
        }

        // 带参数构造函数
        public Form_DelayTime(SingletonStatus Status)
        {
            InitializeComponent();
            _jsonManager = new JsonManager();
            InitForm();
        }

        // 加载参数
        private async Task ShowItemAsync()
        {
            try
            {
                SingletonStatus singleton = SingletonStatus.Instance;

                // 读取配置
                var config = await JsonManager.GetOrCreateConfigAsync();
                var form = config.Form.FirstOrDefault(p =>
                    p.ModelTypeName == singleton.ModelTypeName &&
                    p.ModelName == singleton.ModelName &&
                    p.ItemName == singleton.ItemName);

                if (form?.ChildSteps == null || form.ChildSteps.Count <= singleton.StepNum)
                {
                    await SetDefaultValuesAsync();
                    return;
                }

                var stepParameter = form.ChildSteps[singleton.StepNum].StepParameter;
                if (stepParameter == null)
                {
                    await SetDefaultValuesAsync();
                    return;
                }

                try
                {
                    var parameters = JsonSerializer.Deserialize<Parameter_DelayTime>(
                        stepParameter.ToString(),
                        CachedJsonOptions);

                    if (parameters == null)
                    {
                        await SetDefaultValuesAsync();
                        return;
                    }

                    txtTime.Text = parameters.T.ToString();
                }
                catch (JsonException)
                {
                    await SetDefaultValuesAsync();
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载延时参数失败", ex);
                await SetDefaultValuesAsync();
            }
        }

        /// <summary>
        /// 异步设置默认值
        /// </summary>
        /// <returns></returns>
        private async Task SetDefaultValuesAsync()
        {
            try
            {
                const double defaultDelay = 200.0;
                txtTime.Text = defaultDelay.ToString();

                var parameters = new Parameter_DelayTime { T = defaultDelay };
                await JsonManager.UpdateConfigAsync(async config =>
                {
                    var form = config.Form.FirstOrDefault(p =>
                        p.ModelTypeName == SingletonStatus.Instance.ModelTypeName &&
                        p.ModelName == SingletonStatus.Instance.ModelName &&
                        p.ItemName == SingletonStatus.Instance.ItemName);

                    if (form != null && form.ChildSteps != null &&
                        form.ChildSteps.Count > SingletonStatus.Instance.StepNum)
                    {
                        form.ChildSteps[SingletonStatus.Instance.StepNum].StepParameter = parameters;
                    }
                    await Task.CompletedTask;
                });

                MessageHelper.MessageOK("已加载默认参数", AntdUI.TType.Info);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("设置默认参数失败", ex);
                MessageHelper.MessageOK($"设置默认参数失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        private async Task WriteItemAsync()
        {
            try
            {
                var parameters = new Parameter_DelayTime
                {
                    T = double.Parse(txtTime.Text)
                };

                // 修改当前步骤参数值
                await JsonManager.UpdateConfigAsync(async config =>
                {
                    // 找到所有步骤集合
                    var form = config.Form.FirstOrDefault(p =>
                        p.ModelTypeName == SingletonStatus.Instance.ModelTypeName &&
                        p.ModelName == SingletonStatus.Instance.ModelName &&
                        p.ItemName == SingletonStatus.Instance.ItemName);

                    // 找到对应步骤需要修改的参数
                    if (form != null && form.ChildSteps != null &&
                        form.ChildSteps.Count > SingletonStatus.Instance.StepNum)
                    {
                        form.ChildSteps[SingletonStatus.Instance.StepNum].StepParameter = parameters;
                    }
                    await Task.CompletedTask;
                });

                MessageHelper.MessageOK("保存成功", AntdUI.TType.Success);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存参数失败", ex);
                MessageHelper.MessageOK($"保存失败：{ex.Message}");
            }
        }

        private void InitForm()
        {
            _ = ShowItemAsync();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            _ = WriteItemAsync();
        }
    }
}
