using AntdUI;
using MainUI.Service;
using Label = System.Windows.Forms.Label;
using Timer = System.Windows.Forms.Timer;

namespace MainUI
{
    public partial class UcHMI : UserControl
    {
        #region 全局变量
        private readonly frmMainMenu frm = new();
        public delegate void RunStatusHandler(bool obj);
        public event RunStatusHandler EmergencyStatusChanged;
        private static ParaConfig paraconfig;
        private readonly List<ItemPointModel> _itemPoints = [];
        private readonly ControlMappings controls = new();
        private readonly ControlInitializationService _controlInitService;
        public delegate void TestStateHandler(bool isTesting);
        public event TestStateHandler TestStateChanged;
        private readonly string reportPath;
        private readonly string dslPath;
        private readonly OPCEventRegistration _opcEventRegistration;
        private readonly DSLService _dslService;
        private readonly TestExecutionService _testService;
        private readonly ReportService _reportService;
        private readonly TableService _tableService;
        private readonly CountdownService _countdownService;

        #endregion

        public UcHMI()
        {
            InitializeComponent();
            _opcEventRegistration = new OPCEventRegistration(this);
            reportPath = Path.Combine(Application.StartupPath, Constants.ReportsPath);
            dslPath = Path.Combine(Application.StartupPath, Constants.ProcedurePath);
            _dslService = new DSLService(dslPath);
            _reportService = new ReportService(reportPath);
            _testService = new TestExecutionService(_dslService, TableColor);
            _tableService = new TableService(TableItemPoint, _itemPoints);
            _countdownService = new CountdownService(LabTestTime);
            _controlInitService = new ControlInitializationService(controls);
        }

        #region 初始化
        public void Init()
        {
            try
            {
                InitializeOPC();  //初始化OPC连接和组
                InitializeControls();  //初始化控件和数据
                RegisterOPCHandlers();  //注册OPC组事件处理程序
                RegisterTestEventHandlers();  //注册测试状态和提示事件处理程序
                SetInitialState();  //设置初始状态
                InitializePermissions(); //初始化权限
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 初始化权限
        /// </summary>
        private void InitializePermissions()
        {
            try
            {
                var currentUser = NewUsers.NewUserInfo;
                PermissionHelper.Initialize(currentUser.ID, currentUser.Role_ID);
                PermissionHelper.ApplyPermissionToControl(this, currentUser.ID);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"初始化权限失败：{ex.Message}");
                throw;
            }
        }

        // 初始化OPC连接和组
        private void InitializeOPC()
        {
            OPCHelper.Init();
        }

        // 初始化控件和数据
        private void InitializeControls()
        {
            _controlInitService.InitializeAllControls(this);
            _tableService.InitColumns();
        }

        private Table.CellStyleInfo TableItemPoint_SetRowStyle(object sender, TableSetRowStyleEventArgs e)
        {
            return _tableService.SetRowStyle(e.Record);
        }

        private void TableItemPoint_CheckedChanged(object sender, TableCheckEventArgs e)
        {
            _tableService.HandleCheckedChanged(e);
        }

        // 注册OPC组事件处理程序
        private void RegisterOPCHandlers()
        {
            _opcEventRegistration.RegisterAll();
        }

        // 注册测试状态和提示事件处理程序
        private void RegisterTestEventHandlers()
        {
            BaseTest.TestStateChanged += BaseTest_TestStateChanged;
            BaseTest.TipsChanged += BaseTest_TipsChanged;
        }

        // 设置初始状态
        private void SetInitialState()
        {
            btnStopTest.Enabled = false;
        }

        private void BaseTest_TipsChanged(object sender, object info)
        {
            AppendText(info.ToString());
        }

        private void BaseTest_TestStateChanged(bool isTesting)
        {
            Disable(isTesting);
        }

        /// <summary>
        /// 根据测试状态启用或禁用控件
        /// </summary>
        /// <param name="isTesting">是否正在测试中</param>
        private void Disable(bool isTesting)
        {
            btnStopTest.Enabled = isTesting;  // 停止按钮在测试时启用
            btnStartTest.Enabled = !isTesting; // 开始按钮在测试时禁用

            // 在测试进行时禁用的控件组
            var controlsToDisable = new Control[]
            {
                grpDO,           // 数字输出控制组
                grpServoGrp,     // 伺服控制组
                btnProductSelection, // 产品选择按钮
                TableItemPoint,    // 测试项表格
                panelHand         // 手动控制面板
            };

            // 批量设置控件状态
            foreach (var control in controlsToDisable)
            {
                control.Enabled = !isTesting;
            }

            Refresh(); // 触发UI更新
        }
        #endregion

        #region 值改变事件
        public void TestCongrp_TestConGroupChanged(object sender, int index, object value)
        {
            if (controls.DigitalOutputs.TryGetValue(index, out UISwitch iSwitch))
            {
                iSwitch.Active = value.ToBool();
            }
            if (controls.DigitalOutputButtons.TryGetValue(index, out UIButton btn))
            {
                NavigationButtonStyles.BtnColor(btn, value.ToBool());
            }
        }

        public void Currentgrp_CurrentvalueGrpChaned(object sender, int index, double value)
        {
            if (controls.TemperaturePoints.TryGetValue(index, out Label label))
            {
                label.Text = value.ToString("f1");
            }
        }
        public void AIgrp_AIvalueGrpChanged(object sender, int index, double value)
        {
            if (controls.AnalogInputs.TryGetValue(index, out Label label))
            {
                label.Text = value.ToString("f1");
            }
        }

        public void AOgrp_AOvalueGrpChanged(object sender, int index, double value)
        {
            switch (index)
            {
                default:
                    break;
            }
        }
        public void DIgrp_DIGroupChanged(object sender, int index, bool value)
        {
            try
            {
                if (index == 0)
                {
                    if (!value) IsTestEnd();
                    EmergencyStatusChanged?.Invoke(value);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("DI模块事件报错：" + ex.Message);
            }
        }

        public void DOgrp_DOgrpChanged(object sender, int index, bool value) { }
        #endregion

        #region 参数
        private void InitParaConfig()
        {
            try
            {
                if (VarHelper.TestViewModel == null) return;

                // 初始化和加载参数配置
                paraconfig = new ParaConfig();
                paraconfig.SetSectionName(VarHelper.ModelTypeName);
                paraconfig.Load();
                BaseTest.para = paraconfig;

                // 初始化测试项
                _tableService.LoadTestItems();

                // 加载DSL
                _dslService.LoadDSL(VarHelper.TestViewModel.ID, paraconfig);

                // 处理报表文件
                if (!string.IsNullOrEmpty(paraconfig.RptFile))
                {
                    ucGrid1.LoadFile(_reportService
                        .InitializeReportFile(paraconfig));
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"加载参数错误：{ex.Message}");
                NlogHelper.Default.Error($"加载参数错误", ex);
            }
        }

        //刷新型号
        public void ParaRefresh()
        {
            try
            {
                if (string.IsNullOrEmpty(VarHelper.TestViewModel.ModelTypeName)) return;
                InitParaConfig();
            }
            catch (Exception ex)
            {
                MessageHelper.MessageYes("刷新型号错误：" + ex.Message);
            }
        }
        #endregion

        #region 自动试验
        private CancellationTokenSource _cancellationTokenSource = new();
        private async void btnStartTest_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. 检查前置条件
                (bool Result, string txt) = FrmText();
                if (!Result)
                {
                    MessageHelper.MessageOK(txt, TType.Error);
                    return;
                }

                // 2. 设置UI状态
                Disable(true);
                TestStateChanged?.Invoke(true);

                // 3. 初始化取消计时器令牌
                _cancellationTokenSource = new CancellationTokenSource();

                // 4. 并行执行倒计时和测试
                var countdownTask = _countdownService.StartCountdown(
                    paraconfig.SprayTime.ToInt(), 
                    _cancellationTokenSource.Token
                );

                var testTask = _testService.StartTest(_itemPoints);

                // 5. 等待所有任务完成
                await Task.WhenAll(countdownTask, testTask);
            }
            catch (TaskCanceledException ex)
            {
                Debug.WriteLine($"试验已取消：{ex.Message}");
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine($"试验已取消: {ex}");
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"试验开始错误：{ex.Message}");
            }
        }

        // 设置行颜色
        private void TableColor(ItemPointModel itemPoint, int state)
        {
            itemPoint.ColorState = state;
            TableItemPoint.Invalidate();
        }

        private void btnStopTest_Click(object sender, EventArgs e) => IsTestEnd();

        private static (bool Result, string txt) FrmText()
        {
            if (!OPCHelper.DIgrp[0])
            {
                return (false, "请注意，急停情况下无法启动自动试验!");
            }
            if (string.IsNullOrEmpty(VarHelper.TestViewModel.ModelName))
            {
                return (false, "未选择型号，无法启动自动试验!");
            }
            if (!OPCHelper.TestCongrp[39].ToBool())
            {
                return (false, "请注意，手动情况下无法启动自动试验!");
            }
            if (string.IsNullOrEmpty(paraconfig.SprayTime) || string.IsNullOrEmpty(paraconfig.ApplyPressure))
            {
                return (false, "请注意，该型号试验参数未设置，无法启动自动试验!");
            }
            return (true, "");
        }

        // 结束试验操作
        private void IsTestEnd()
        {
            try
            {
                Disable(false);
                AppendText("试验结束");
                OPCHelper.TestCongrp[41] = true;
                TestStateChanged?.Invoke(false);

                // 停止测试服务
                _testService.StopTest();
                _countdownService.StopCountdown();
                _cancellationTokenSource.Cancel();
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine($"Task被取消: {ex}");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"结束试验错误：{ex.Message}", ex);
                MessageHelper.MessageOK(frm, $"结束试验错误：{ex.Message}");
            }
        }
        #endregion

        #region 模拟量设置
        private void btnNozzleMotor_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as UIButton;
                using frmSetOutValue fs = new(OPCHelper.TestCongrp[btn.Tag.ToInt32()].ToDouble(), btn.Text, 10000);
                VarHelper.ShowDialogWithOverlay(frm, fs);
                if (fs.DialogResult == DialogResult.OK)
                {
                    ControlHelper.ButtonClickAsync(sender, () =>
                    {
                        //LabAO01.Text = fs.OutValue.ToString();
                        OPCHelper.TestCongrp[btn.Tag.ToInt32()] = fs.OutValue;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK(frm, $"模拟量设定错误：{ex.Message}");
            }
        }
        #endregion

        #region 报表翻页控制
        private const int DefaultPageSize = 29; // 默认每页显示行数
        private int currentRowIndex = 0; // 当前行索引
        private bool isScrollingDown = false; // 是否向下滚动标记

        /// <summary>
        /// 向上翻页
        /// </summary>
        private void btnPageUp_Click(object sender, EventArgs e)
        {
            int pageSize = LabelNumber.Value.ToInt32();

            // 如果之前是向下滚动,需要先回到起始位置
            if (isScrollingDown)
            {
                currentRowIndex -= DefaultPageSize;
                isScrollingDown = false;
            }

            // 向上翻页,减少行索引
            currentRowIndex = Math.Max(0, currentRowIndex - pageSize);

            // 执行翻页
            ucGrid1.PageTurning(currentRowIndex);
        }

        /// <summary>
        /// 向下翻页
        /// </summary>
        private void btnPageDown_Click(object sender, EventArgs e)
        {
            int pageSize = LabelNumber.Value.ToInt32();

            // 如果是首次向下滚动,需要先移动到默认页大小位置
            if (!isScrollingDown)
            {
                currentRowIndex = DefaultPageSize;
                isScrollingDown = true;
            }
            else
            {
                // 继续向下翻页,增加行索引
                currentRowIndex += pageSize;
            }

            // 执行翻页
            ucGrid1.PageTurning(currentRowIndex);
        }
        #endregion

        #region 报表控件
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateSaveParameters()) return;// 参数校验

                if (!ConfirmSaveReport()) return;  // 提示确认

                string saveFilePath = _reportService.BuildSaveFilePath(VarHelper.TestViewModel.ModelName); // 保存路径

                _reportService.SaveTestRecord(saveFilePath, new TestRecordModel
                {
                    //TODO: 填充试验记录数据
                }); // 保存记录

                ucGrid1.SaveAsPdf(saveFilePath, saveFilePath);  // 导出PDF

                MessageHelper.MessageOK("保存成功", TType.Success);
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"保存失败: {ex.Message}", TType.Error);
            }
        }

        // 保存试验报告前参数校验
        private bool ValidateSaveParameters()
        {
            if (string.IsNullOrEmpty(paraconfig?.RptFile))
            {
                MessageHelper.MessageOK("报表模板未设置，无法保存！", TType.Warn);
                return false;
            }

            if (string.IsNullOrEmpty(VarHelper.TestViewModel.ModelName))
            {
                MessageHelper.MessageOK("型号未选择！", TType.Warn);
                return false;
            }
            return true;
        }

        // 确认保存报表
        private static bool ConfirmSaveReport() =>
            MessageHelper.MessageYes("是否保存试验结果？") == DialogResult.Yes;
        #endregion

        #region 其他
        private void AppendText(string text)
        {
            //txtTestRecord.AppendText($"{DateTime.Now:HH:mm:ss}：{text}\n");
            //txtTestRecord.ScrollToCaret();
        }

        private void btnTechnology_Click(object sender, EventArgs e)
        {
            tabs1.SelectedIndex = 0;
            NavigationButtonStyles.UpdateNavigationButtons
                (tabs1.SelectedIndex, controls);
        }

        private void btnCurve_Click(object sender, EventArgs e)
        {
            tabs1.SelectedIndex = 1;
            NavigationButtonStyles.UpdateNavigationButtons
                (tabs1.SelectedIndex, controls);
        }

        private void btnProductSelection_Click(object sender, EventArgs e)
        {
            using frmSpec frmSpec = new();
            VarHelper.ShowDialogWithOverlay(frm, frmSpec);
            if (frmSpec.DialogResult == DialogResult.OK)
            {
                txtModel.Text = VarHelper.TestViewModel.ModelName;
                ParaRefresh();
            }
        }

        private void RadioAuto_Click(object sender, EventArgs e)
        {
            OPCHelper.TestCongrp[39] = RadioAuto.Checked;
        }

        private void btnWaterPumpStart_Click(object sender, EventArgs e)
        {
            var btn = sender as UIButton;
            OPCHelper.TestCongrp[btn.Tag.ToInt32()] =
                !OPCHelper.TestCongrp[btn.Tag.ToInt32()].ToBool();
        }

        private async void btnFaultRemoval_Click(object sender, EventArgs e)
        {
            var btn = sender as UIButton;
            await FaultClearingAsync(btn);
        }
        private async Task FaultClearingAsync(UIButton btn)
        {
            OPCHelper.TestCongrp[btn.Tag.ToInt32()] = true;
            await Task.Delay(1000);
            OPCHelper.TestCongrp[btn.Tag.ToInt32()] = false;
        }

        private void uiSwitch_MouseDown(object sender, MouseEventArgs e)
        {
            var sder = sender as UISwitch;
            OPCHelper.TestCongrp[sder.Tag.ToInt32()] = true;
        }

        private void uiSwitch_MouseUp(object sender, MouseEventArgs e)
        {
            var sder = sender as UISwitch;
            OPCHelper.TestCongrp[sder.Tag.ToInt32()] = false;
        }

      
        #endregion
    }
}