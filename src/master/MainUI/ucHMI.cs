using AntdUI;
using RW.DSL;
using RW.DSL.Procedures;
using Color = System.Drawing.Color;
using Label = System.Windows.Forms.Label;

namespace MainUI
{
    public partial class UcHMI : UserControl
    {
        #region 全局变量
        private readonly frmMainMenu frm = new();
        public delegate void RunStatusHandler(bool obj);
        public event RunStatusHandler EmergencyStatusChanged;
        private static ParaConfig paraconfig;
        List<ItemPointModel> _itemPoints = [];
        private readonly ControlMappings controls = new();
        public delegate void TestStateHandler(bool isTesting);
        public event TestStateHandler TestStateChanged;
        private readonly string reportPath;
        private readonly string dslPath;
        private readonly OPCEventRegistration _opcEventRegistration;
        #endregion

        public UcHMI()
        {
            InitializeComponent();
            _opcEventRegistration = new OPCEventRegistration(this);
            reportPath = Path.Combine(Application.StartupPath, Constants.ReportsPath);
            dslPath = Path.Combine(Application.StartupPath, Constants.ProcedurePath);
        }

        #region DSL初始化
        public Dictionary<string, DSLProcedure> DicProcedureModules = [];

        // 根据型号加载DSL
        public void ModelLoadDSL(int modelId)
        {
            if (modelId <= 0)
            {
                throw new ArgumentException("模型ID必须大于0", nameof(modelId));
            }

            try
            {
                DicProcedureModules.Clear();
                LoadAndInitializeProcedures(modelId);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载DSL文件错误", ex);
                MessageHelper.MessageOK($"加载DSL文件错误：{ex.Message}");
                throw;
            }
        }

        // 加载并初始化试验步骤
        private void LoadAndInitializeProcedures(int modelId)
        {
            var testStep = new TestStepBLL();
            var testItems = testStep.GetTestItems(modelId);
            
            if (testItems.Count == 0)
            {
                MessageHelper.MessageOK("未找到测试项目");
                return;
            }

            var visitor = DSLFactory.CreateVisitor();
            var moduleParameters = new Dictionary<string, object>
            {
                ["参数"] = paraconfig
            };

            string modelName = VarHelper.TestViewModel.ModelName;
            foreach (var testItem in testItems)
            {
                LoadSingleProcedure(testItem, modelName, moduleParameters, visitor);
            }
        }

        // 加载单个试验步骤
        private void LoadSingleProcedure(TestStepNewModel testItem, string modelName, 
            Dictionary<string, object> moduleParameters, object visitor)
        {
            string processName = testItem.ProcessName;
            string dslFilePath = Path.Combine(dslPath, modelName, $"{processName}.rw1");

            if (!File.Exists(dslFilePath))
            {
                MessageHelper.MessageOK($"试验项点：{processName},找不到自动试验文件！");
                return;
            }

            var procedure = DSLFactory.CreateProcedure(dslFilePath);
            procedure.AddModules(moduleParameters);
            procedure.AddModules(ModuleComponent.Instance.GetList());
            procedure.DomainEventInvoked += new DomainHandler<object>(visitor_DomainEventInvoked);

            DicProcedureModules.Add(processName, procedure);
        }

        void visitor_DomainEventInvoked(object sender, string name, List<object> output)
        {
            try
            {
                if (name == "试验提示")
                {
                    Invoke(() => { });
                }
                Debug.WriteLine($"监听事件：{name}  值数量：{output.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "事件触发错误：" + ex.Message);
            }
        }
        #endregion

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            AddBtn();
            LoaddicDI();
            LoaddicDO();
            LoaddicAI();
            InitColumn();
            LoaddicDOBtn();
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
            OPCHelper.TestCongrp[39] = true;
            btnStopTest.Enabled = false;
        }

        public void TestCongrp_TestConGroupChanged(object sender, int index, object value)
        {
            if (controls.DigitalOutputs.TryGetValue(index, out UISwitch iSwitch))
            {
                iSwitch.Active = value.ToBool();
            }
            if (controls.DigitalOutputButtons.TryGetValue(index, out UIButton btn))
            {
                BtnColor(btn, value.ToBool());
            }
        }

        public void Currentgrp_CurrentvalueGrpChaned(object sender, int index, double value)
        {
            if (controls.TemperaturePoints.TryGetValue(index, out Label label))
            {
                label.Text = value.ToString("f1");
            }
        }

        private void BaseTest_TipsChanged(object sender, object info)
        {
            AppendText(info.ToString());
        }

        private void BaseTest_TestStateChanged(bool isTesting)
        {
            Disable(isTesting);
        }

        private void Disable(bool isTesting)
        {
            grpDO.Enabled = !isTesting;
            grpServoGrp.Enabled = !isTesting;
            btnStopTest.Enabled = isTesting;
            btnStartTest.Enabled = !isTesting;
            btnProductSelection.Enabled = !isTesting;
            TableItemPoint.Enabled = !isTesting;
            panelHand.Enabled = !isTesting;
        }

        private static class ButtonColors
        {
            public static readonly Color ActiveColor = Color.LimeGreen;
            public static readonly Color InactiveColor = Color.FromArgb(80, 160, 255);
        }

        private void BtnColor(UIButton btn, bool isActive)
        {
            var color = isActive ? ButtonColors.ActiveColor : ButtonColors.InactiveColor;

            // 使用对象初始化器设置所有颜色属性
            var properties = new[]
            {
                nameof(btn.FillColor),
                nameof(btn.RectColor),
                nameof(btn.FillDisableColor),
                nameof(btn.RectDisableColor),
                nameof(btn.FillHoverColor),
                nameof(btn.FillPressColor),
                nameof(btn.FillSelectedColor)
            };

            foreach (var property in properties)
            {
                btn.GetType().GetProperty(property)?.SetValue(btn, color);
            }
        }

        /// <summary>
        /// 加载DI模块
        /// </summary>
        private void LoaddicDI()
        {
            controls.DigitalInputs.Clear();
        }

        private void LoaddicDOBtn()
        {
            controls.DigitalOutputButtons.Clear();
            UIButton[] labBtns =
            [
               btnWaterPumpStart, btnFaultRemoval, btnElectricalInit, btnSynchronous12, btnSynchronous34
            ];
            foreach (var btn in labBtns)
            {
                controls.DigitalOutputButtons.TryAdd(btn.Tag.ToInt32(), btn);
            }
        }

        //加载AI模块
        private void LoaddicAI()
        {
            controls.AnalogInputs.Clear();
            controls.TemperaturePoints.Clear();
            Label[] labAIs =
            [
                LabAI01, LabAI02, LabAI03, LabAI04, LabAI05, LabAI06, LabAI07
            ];
            foreach (var lab in labAIs)
            {
                controls.AnalogInputs.TryAdd(lab.Tag.ToInt32(), lab);
            }

            Label[] labTPs =
            [
                LabTP01, LabTP02, LabTP03, LabTP04, LabTP05,
                LabTP06, LabTP07, LabTP08, LabTP09
            ];
            foreach (var lab in labTPs)
            {
                controls.TemperaturePoints.TryAdd(lab.Tag.ToInt32(), lab);
            }
        }

        //加载AI模块
        private void LoaddicDO()
        {
            controls.DigitalOutputs.Clear();
            AddContrls(grpDO);
        }

        private void AddBtn()
        {
            controls.NavigationButtons.Clear();
            controls.NavigationButtons.Add(0, btnDetection);
            controls.NavigationButtons.Add(1, btnCurve);
        }

        /// <summary>
        /// 递归查找
        /// </summary>
        /// <param name="con"></param>
        private void AddContrls(Control con)
        {
            foreach (Control item in con.Controls)
            {
                if (item is UISwitch)
                {
                    int key = item.Tag.ToInt32();
                    if (!controls.DigitalOutputs.ContainsKey(key))
                        controls.DigitalOutputs.Add(key, item as UISwitch);
                }
                AddContrls(item);
            }
        }
        #endregion

        #region 值改变事件
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
                case 0:
                    break;
                case 1:
                    break;
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

        public void DOgrp_DOgrpChanged(object sender, int index, bool value)
        {

        }
        #endregion

        #region 参数
        private string RptFilename;
        private string RptFilePath; 

        private void InitParaConfig()
        {
            try
            {
                // 验证必要条件
                if (VarHelper.TestViewModel == null) return;

                // 初始化和加载参数配置
                paraconfig = new ParaConfig();
                paraconfig.SetSectionName(VarHelper.ModelTypeName);
                paraconfig.Load();
                BaseTest.para = paraconfig;

                // 初始化测试项和DSL
                InitItem();
                ModelLoadDSL(VarHelper.TestViewModel.ID);

                // 处理报表文件
                if (!string.IsNullOrEmpty(paraconfig.RptFile))
                {
                    InitializeReportFile();
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"加载参数错误：{ex.Message}");
                NlogHelper.Default.Error($"加载参数错误", ex);
            }
        }

        private void InitializeReportFile()
        {
            rowIndex = 29;
            RptFilename = paraconfig.RptFile;
            RptFilePath = Path.GetFileNameWithoutExtension(RptFilename);
            
            string rptPath = Path.Combine(Application.StartupPath, "reports", RptFilePath);
            
            // 确保目标目录存在
            Directory.CreateDirectory(Path.GetDirectoryName(reportPath));
            
            // 复制报表文件
            File.Copy($"{rptPath}.xlsx", reportPath, true);
            
            // 加载报表文件
            ucGrid1.LoadFile(rptPath);
        }

        //刷新型号
        public void sRefresh()
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

        // 初始化表格列
        private void InitColumn()
        {
            TableItemPoint.Columns =
            [
               new ColumnCheck("Check") { Checked = true },
                new Column("ItemName", "项点名称") { Align = ColumnAlign.Left, Width = "285" },
            ];
        }

        // 加载试验步骤
        private void InitItem()
        {
            _itemPoints.Clear();
            TestStepBLL stepBLL = new();
            var testSteps = stepBLL.GetTestItems(VarHelper.TestViewModel.ID);
            _itemPoints.AddRange(testSteps.Select(ts => new ItemPointModel
            {
                Check = true,
                ItemName = ts.TestProcessName
            }));
            TableItemPoint.DataSource = _itemPoints;
            TableItemPoint.SetRowStyle += TableItemPoint_SetRowStyle;
        }

        private Table.CellStyleInfo TableItemPoint_SetRowStyle(object sender, TableSetRowStyleEventArgs e)
        {
            try
            {
                if (e.Record is ItemPointModel data)
                {
                    return data.ColorState switch
                    {
                        0 => new Table.CellStyleInfo { BackColor = Color.Transparent },
                        1 => new Table.CellStyleInfo { BackColor = Color.FromArgb(255, 255, 128) },
                        2 => new Table.CellStyleInfo { BackColor = Color.FromArgb(50, 205, 50) },
                        _ => null
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("任务查看界面，颜色改变错误：", ex);
                return null;
            }
        }

        private void TableItemPoint_CheckedChanged(object sender, TableCheckEventArgs e)
        {
            if (e.Record is ItemPointModel item)
            {
                int index = _itemPoints.FindIndex(p => p.ItemName == item.ItemName);
                if (index != -1)
                {
                    _itemPoints[index].Check = item.Check;
                }
            }
        }
        #endregion

        #region 自动试验
        private CancellationTokenSource _cancellationTokenSource = new();
        private async void btnStartTest_Click(object sender, EventArgs e)
        {
            try
            {
                (bool Result, string txt) = FrmText();
                if (!Result)
                {
                    MessageHelper.MessageOK(txt, TType.Error);
                    return;
                }
                TestInit();
                Disable(true);
                TestStateChanged?.Invoke(true);
                _cancellationTokenSource = new CancellationTokenSource();
                StartCountdown(paraconfig.SprayTime.ToInt(), _cancellationTokenSource.Token);
                await Task.Factory.StartNew(() => BackgroundWorker(), _cancellationTokenSource.Token,
                   TaskCreationOptions.LongRunning, TaskScheduler.Default);
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
        // 试验前初始化
        private void TestInit()
        {

        }
        // 设置行颜色
        private void TableColor(ItemPointModel itemPoint, int state)
        {
            itemPoint.ColorState = state;
            TableItemPoint.Invalidate();
        }

        // 试验项点启动
        private void BackgroundWorker()
        {
            // 初始化颜色状态
            _itemPoints.ForEach(i => i.ColorState = 0);
            foreach (var itemPoint in _itemPoints)
            {
                if (!itemPoint.Check) continue;
                if (DicProcedureModules.TryGetValue(itemPoint.ItemName, out DSLProcedure procedure))
                {
                    Invoke(() => TableColor(itemPoint, 1));
                    try
                    {
                        procedure.Execute();
                    }
                    catch (RWDSLException dslex)
                    {
                        NlogHelper.Default.Debug($"执行DSL错误：RW.DSL.RWDSLException{dslex.Message}");
                    }
                    catch (NullReferenceException nullex)
                    {
                        NlogHelper.Default.Debug($"执行DSL错误：NullReferenceException{nullex.Message}");
                    }
                    catch (Exception ex)
                    {
                        NlogHelper.Default.Error($"执行DSL错误：Exception{ex.Message}");
                        MessageHelper.MessageOK(ex.Message);
                    }
                    finally
                    {
                        Invoke(() => TableColor(itemPoint, 2));
                    }
                }
                else
                {
                    IsTestEnd();
                    MessageHelper.MessageOK("未找到自动试验文件，试验开始失败！");
                    break;
                }
            }
            IsTestEnd();
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

        private void StartCountdown(int totalMinutes, CancellationToken token)
        {
            TimeSpan remainingTime = TimeSpan.FromMinutes(totalMinutes);
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    LabTestTime.Text = $"{remainingTime.Hours:D2}:{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";
                    await Task.Delay(1000, token);
                    remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
                }
            }, _cancellationTokenSource.Token);
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

        #region 报表控件
        int AlturaCount = 29;
        int rowIndex = 0;
        private bool isDow = false;
        private void btnPageUp_Click(object sender, EventArgs e)
        {
            if (isDow) { rowIndex -= AlturaCount; isDow = false; }
            rowIndex -= LabelNumber.Value.ToInt32();
            ucGrid1.PageTurning(rowIndex);
        }

        private void btnPageDown_Click(object sender, EventArgs e)
        {
            if (!isDow) { rowIndex = AlturaCount; isDow = true; }
            rowIndex += LabelNumber.Value.ToInt32();
            ucGrid1.PageTurning(rowIndex);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateSaveParameters()) return;// 参数校验

                if (!ConfirmSaveReport()) return;  // 提示确认

                string saveFilePath = BuildSaveFilePath(); // 保存路径

                SaveTestRecord(saveFilePath); // 保存记录

                ucGrid1.SaveAsPdf(saveFilePath, saveFilePath);  // 导出PDF

                MessageHelper.MessageOK("保存成功", TType.Success);
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"保存失败: {ex.Message}", TType.Error);
            }
        }

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

        private bool ConfirmSaveReport()
        {
            return MessageHelper.MessageYes("是否保存试验结果？") == DialogResult.Yes;
        }

        private string BuildSaveFilePath()
        {
            string rootPath = Path.Combine(Application.StartupPath, "Save");
            Directory.CreateDirectory(rootPath);

            string fileName = $"{VarHelper.TestViewModel.ModelName}_{DateTime.Now:yyyyMMddHHmmss}";
            return Path.Combine(rootPath, fileName);
        }

        private void SaveTestRecord(string filePath)
        {
            var record = new TestRecordModel
            {
                KindID = VarHelper.TestViewModel.TypeID,
                ModelID = VarHelper.TestViewModel.ID,
                TestID = txtNumber.Text.Trim(),
                Tester = NewUsers.NewUserInfo.Username,
                TestTime = DateTime.Now,
                ReportPath = filePath
            };

            var recordBll = new TestRecordNewBLL();
            recordBll.SaveTestRecord(record);
        }
        #endregion

        #region 其他
        private void UcHMI_Load(object sender, EventArgs e)
        {

        }

        private void AppendText(string text)
        {
            //txtTestRecord.AppendText($"{DateTime.Now:HH:mm:ss}：{text}\n");
            //txtTestRecord.ScrollToCaret();
        }

        private void BtnColor(int index)
        {
            foreach (var item in controls.NavigationButtons)
            {
                if (item.Key == index)
                {
                    item.Value.BackColor = Color.White;
                    item.Value.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    item.Value.BackColor = Color.FromArgb(196, 199, 204);
                    item.Value.ForeColor = Color.White;
                }
            }
        }

        private void btnTechnology_Click(object sender, EventArgs e)
        {
            tabs1.SelectedIndex = 0;
            BtnColor(tabs1.SelectedIndex);
        }

        private void btnCurve_Click(object sender, EventArgs e)
        {
            tabs1.SelectedIndex = 1;
            BtnColor(tabs1.SelectedIndex);
        }


        private void btnProductSelection_Click(object sender, EventArgs e)
        {
            using frmSpec frmSpec = new();
            VarHelper.ShowDialogWithOverlay(frm, frmSpec);
            if (frmSpec.DialogResult == DialogResult.OK)
            {
                txtModel.Text = VarHelper.TestViewModel.ModelName;
                sRefresh();
            }
        }

        private void RadioAuto_Click(object sender, EventArgs e)
        {
            OPCHelper.TestCongrp[39] = RadioAuto.Checked;
        }

        private void btnWaterPump_Click(object sender, EventArgs e)
        {

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


    sealed class OPCEventRegistration
    {
        private readonly UcHMI _form;

        public OPCEventRegistration(UcHMI form)
        {
            _form = form;
        }

        public void RegisterAll()
        {
            RegisterAIGroup();
            RegisterDIGroup();
            RegisterAOGroup();
            RegisterDOGroup();
            RegisterCurrentGroup();
            RegisterTestConGroup();
        }

        private void RegisterAIGroup()
        {
            SafeRegister(() =>
            {
                OPCHelper.AIgrp.AIvalueGrpChanged += _form.AIgrp_AIvalueGrpChanged;
                OPCHelper.AIgrp.Fresh();
            }, "AI组");
        }

        private void RegisterDIGroup()
        {
            SafeRegister(() =>
            {
                OPCHelper.DIgrp.DIGroupChanged += _form.DIgrp_DIGroupChanged;
                OPCHelper.DIgrp.Fresh();
            }, "DI组");
        }

        private void RegisterAOGroup()
        {
            SafeRegister(() =>
            {
                OPCHelper.AOgrp.AOvalueGrpChaned += _form.AOgrp_AOvalueGrpChanged;
                OPCHelper.AOgrp.Fresh();
            }, "AO组");
        }

        private void RegisterDOGroup()
        {
            SafeRegister(() =>
            {
                OPCHelper.DOgrp.DOgrpChanged += _form.DOgrp_DOgrpChanged;
                OPCHelper.DOgrp.Fresh();
            }, "DO组");
        }

        private void RegisterCurrentGroup()
        {
            SafeRegister(() =>
            {
                OPCHelper.Currentgrp.CurrentvalueGrpChaned += _form.Currentgrp_CurrentvalueGrpChaned;
                OPCHelper.Currentgrp.Fresh();
            }, "Current组");
        }

        private void RegisterTestConGroup()
        {
            SafeRegister(() =>
            {
                OPCHelper.TestCongrp.TestConGroupChanged += _form.TestCongrp_TestConGroupChanged;
                OPCHelper.TestCongrp.Fresh();
            }, "TestCon组");
        }

        private static void SafeRegister(Action registration, string groupName)
        {
            try
            {
                registration();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"注册{groupName}事件失败:", ex);
            }
        }
    }

    static class Constants
    {
        public const string ReportsPath = @"reports\report.xlsx";
        public const string ProcedurePath = @"\Procedure\";
    }

    sealed class ControlMappings
    {
        public Dictionary<int, Label> AnalogInputs { get; } = [];
        public Dictionary<int, Label> TemperaturePoints { get; } = [];
        public Dictionary<int, UISwitch> DigitalOutputs { get; } = [];
        public Dictionary<int, UIButton> DigitalOutputButtons { get; } = [];
        public Dictionary<int, Procedure.Controls.SwitchPictureBox> DigitalInputs { get; } = [];
        public Dictionary<int, AntdUI.Button> NavigationButtons { get; } = [];

        public void Clear()
        {
            AnalogInputs.Clear();
            TemperaturePoints.Clear();
            DigitalOutputs.Clear();
            DigitalOutputButtons.Clear();
            DigitalInputs.Clear();
            NavigationButtons.Clear();
        }
    }
}