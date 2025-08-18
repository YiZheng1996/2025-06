using AntdUI;
using Newtonsoft.Json;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// 参数表单基类
    /// </summary>
    public class BaseParameterForm : UIForm
    {
        private bool _isLoading = true;

        #region 构造函数和生命周期

        // 无参构造函数
        public BaseParameterForm() { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode) return;

            try
            {
                _isLoading = true;
                LoadParameters();
                _isLoading = false;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载参数失败", ex);
                MessageHelper.MessageOK($"加载参数失败：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 虚方法 - 子类重写实现

        /// <summary>
        /// 加载参数 - 统一的加载逻辑
        /// </summary>
        protected virtual void LoadParameters()
        {
            if (DesignMode) return;

            var currentStep = GetCurrentStepSafely();
            if (currentStep?.StepParameter != null)
            {
                try
                {
                    LoadParameterFromStep(currentStep.StepParameter);
                }
                catch (Exception ex)
                {
                    NlogHelper.Default.Error("参数转换失败", ex);
                    SetDefaultValues();
                }
            }
            else
            {
                SetDefaultValues();
            }
        }

        /// <summary>
        /// 从步骤参数加载 - 子类重写
        /// </summary>
        protected virtual void LoadParameterFromStep(object stepParameter) { }

        /// <summary>
        /// 设置默认值 - 子类重写
        /// </summary>
        protected virtual void SetDefaultValues() { }

        /// <summary>
        /// 验证参数 - 基类方法
        /// </summary>
        protected virtual bool ValidateParameters()
        {
            return true;
        }

        /// <summary>
        /// 收集参数 - 子类重写
        /// </summary>
        protected virtual object CollectParameters()
        {
            return null;
        }

        #endregion

        #region 统一的通用逻辑

        /// <summary>
        /// 保存参数 - 统一的保存逻辑
        /// </summary>
        protected virtual void SaveParameters()
        {
            if (DesignMode) return;

            try
            {
                var currentStep = GetCurrentStepSafely();
                if (currentStep == null)
                {
                    MessageHelper.MessageOK("步骤索引无效，无法保存参数。", TType.Error);
                    return;
                }

                if (!ValidateParameters())
                {
                    return;
                }

                var parameter = CollectParameters();
                if (parameter != null)
                {
                    currentStep.StepParameter = parameter;
                    MessageHelper.MessageOK("参数已暂存，主界面点击保存后才会写入文件。", TType.Info);
                    Close();
                }
                else
                {
                    MessageHelper.MessageOK("收集参数失败。", TType.Error);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存参数失败", ex);
                MessageHelper.MessageOK($"保存参数失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 安全获取当前步骤
        /// </summary>
        protected ChildModel GetCurrentStepSafely()
        {
            if (DesignMode) return null;

            try
            {
                var steps = SingletonStatus.Instance.IempSteps;
                int idx = SingletonStatus.Instance.StepNum;

                if (steps == null || idx < 0 || idx >= steps.Count)
                {
                    return null;
                }

                return steps[idx];
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("获取当前步骤失败", ex);
                return null;
            }
        }

        /// <summary>
        /// 检查是否正在加载中
        /// </summary>
        protected bool IsLoading => _isLoading;

        #endregion

        #region 通用事件处理

        protected virtual void OnSaveClick(object sender, EventArgs e)
        {
            SaveParameters();
        }

        protected virtual void OnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        protected virtual void OnResetClick(object sender, EventArgs e)
        {
            if (DesignMode) return;

            if (MessageHelper.MessageYes("确定要重置所有参数吗？") == DialogResult.Yes)
            {
                SetDefaultValues();
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 通用JSON参数转换方法
        /// </summary>
        protected T ConvertJsonParameter<T>(object stepParameter) where T : class, new()
        {
            if (DesignMode) return new T();
            if (stepParameter == null) return new T();

            if (stepParameter is T directParam)
                return directParam;

            try
            {
                var json = stepParameter.ToString();
                return JsonConvert.DeserializeObject<T>(json) ?? new T();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"JSON参数转换失败: {typeof(T).Name}", ex);
                return new T();
            }
        }

        /// <summary>
        /// 显示验证错误信息
        /// </summary>
        protected void ShowValidationError(string message, Control focusControl = null)
        {
            MessageHelper.MessageOK(message, TType.Warn);
            focusControl?.Focus();
        }

        /// <summary>
        /// 安全获取控件文本
        /// </summary>
        protected string GetControlText(Control control)
        {
            return control?.Text?.Trim() ?? "";
        }

        /// <summary>
        /// 安全设置控件文本
        /// </summary>
        protected void SetControlText(Control control, string text)
        {
            if (control != null)
            {
                control.Text = text ?? "";
            }
        }

        #endregion
    }
}