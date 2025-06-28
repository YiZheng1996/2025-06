using MainUI.Procedure.DSL.LogicalConfiguration.Parameter;
using System.Text.Json;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    public partial class Form_DelayTime : UIForm
    {
        public Form_DelayTime()
        {
            InitializeComponent();
            InitForm();
        }

        // 带参数构造函数
        public Form_DelayTime(SingletonStatus Status)
        {
            InitializeComponent();
            InitForm();
        }

        // 加载参数（只读内存临时步骤）
        private void InitForm()
        {
            var steps = SingletonStatus.Instance.IempSteps;
            int idx = SingletonStatus.Instance.StepNum;
            if (steps != null && idx >= 0 && idx < steps.Count)
            {
                var paramObj = steps[idx].StepParameter;
                if (paramObj is Parameter_DelayTime param)
                {
                    txtTime.Text = param.T.ToString();
                }
                else if (paramObj is not null)
                {
                    try
                    {
                        var p = JsonSerializer.Deserialize<Parameter_DelayTime>(paramObj.ToString());
                        txtTime.Text = p?.T.ToString() ?? "200";
                    }
                    catch
                    {
                        txtTime.Text = "200";
                    }
                }
                else
                {
                    txtTime.Text = "200";
                }
            }
            else
            {
                txtTime.Text = "200";
            }
        }

        // 保存参数（只改内存临时步骤）
        private void BtnSave_Click(object sender, EventArgs e)
        {
            var steps = SingletonStatus.Instance.IempSteps;
            int idx = SingletonStatus.Instance.StepNum;
            if (steps != null && idx >= 0 && idx < steps.Count)
            {
                if (double.TryParse(txtTime.Text, out double t))
                {
                    steps[idx].StepParameter = new Parameter_DelayTime { T = t };
                    MessageHelper.MessageOK("参数已暂存，主界面点击保存后才会写入文件。", AntdUI.TType.Info);
                    Close();
                }
                else
                {
                    MessageHelper.MessageOK("请输入有效的延时时间。", AntdUI.TType.Warn);
                }
            }
            else
            {
                MessageHelper.MessageOK("步骤索引无效，无法保存参数。", AntdUI.TType.Error);
            }
        }
    }
}
