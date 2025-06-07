using AntdUI;
using RW.DSL;
using RW.DSL.Debugger;
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
        private static ParaConfig paraconfig = null;
        List<ItemPointModel> _itemPoints = [];
        Dictionary<int, AntdUI.Button> pairs = [];
        Dictionary<int, Label> DicAI = [];
        Dictionary<int, Label> DicTP = [];
        Dictionary<int, UISwitch> DicDO = [];
        Dictionary<int, UIButton> DicDOBtn = [];
        Dictionary<int, Procedure.Controls.SwitchPictureBox> DicDI = [];
        public delegate void TestStateHandler(bool isTesting);
        public event TestStateHandler TestStateChanged;
        string path2 = Application.StartupPath + @"reports\report.xlsx";
        string RptFilename = "";  //报表名称
        string RptFilePath = "";  //报表地址
        #endregion

        public UcHMI() => InitializeComponent();

        #region DSL初始化
        public Dictionary<string, DSLProcedure> DicProcedureModules = [];
        private string DSLPath = Application.StartupPath + @"\Procedure\";

        public void ModelLoadDSL(int ModelID)
        {
            try
            {
                DicProcedureModules.Clear();
                var testStep = new TestStepBLL();
                var lstStep = testStep.GetTestItems(ModelID);
                var visitor = DSLFactory.CreateVisitor();
                var modules2 = new Dictionary<string, object>
                {
                    ["参数"] = paraconfig,
                };
                string modelName = VarHelper.TestViewModel.ModelName;
                foreach (var step in lstStep)
                {
                    string processName = step.ProcessName;
                    string dslNamePath = Path.Combine(DSLPath, modelName, $"{processName}.rw1");
                    if (File.Exists(dslNamePath))
                    {
                        var proc = DSLFactory.CreateProcedure(dslNamePath);
                        proc.AddModules(modules2);
                        proc.AddModules(ModuleComponent.Instance.GetList());
                        DicProcedureModules.Add(processName, proc);
                        proc.DomainEventInvoked += new DomainHandler<object>(visitor_DomainEventInvoked);
                    }
                    else
                    {
                        MessageBox.Show($"试验项点：{processName},找不到自动试验文件！");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"加载DSL文件错误：{ex.Message}");
            }
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
                OPCHelper.Init();
                AddBtn();
                LoaddicDI();
                LoaddicDO();
                LoaddicAI();
                InitColumn();
                LoaddicDOBtn();
                OPCHelper.AIgrp.AIvalueGrpChanged += AIgrp_AIvalueGrpChanged;
                OPCHelper.AIgrp.Fresh();
                OPCHelper.DIgrp.DIGroupChanged += DIgrp_DIGroupChanged;
                OPCHelper.DIgrp.Fresh();
                OPCHelper.AOgrp.AOvalueGrpChaned += AOgrp_AOvalueGrpChanged;
                OPCHelper.AOgrp.Fresh();
                OPCHelper.DOgrp.DOgrpChanged += DOgrp_DOgrpChanged;
                OPCHelper.DOgrp.Fresh();
                OPCHelper.Currentgrp.CurrentvalueGrpChaned += Currentgrp_CurrentvalueGrpChaned;
                OPCHelper.Currentgrp.Fresh();
                OPCHelper.TestCongrp.TestConGroupChanged += TestCongrp_TestConGroupChanged;
                OPCHelper.TestCongrp.Fresh();
                BaseTest.TestStateChanged += BaseTest_TestStateChanged;
                BaseTest.TipsChanged += BaseTest_TipsChanged;
                OPCHelper.TestCongrp[39] = true;
                btnStopTest.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TestCongrp_TestConGroupChanged(object sender, int index, object value)
        {
            if (DicDO.TryGetValue(index, out UISwitch iSwitch))
            {
                iSwitch.Active = value.ToBool();
            }
            if (DicDOBtn.TryGetValue(index, out UIButton btn))
            {
                BtnColor(btn, value.ToBool());
            }
        }

        private void Currentgrp_CurrentvalueGrpChaned(object sender, int index, double value)
        {
            if (DicTP.TryGetValue(index, out Label label))
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

        private void BtnColor(UIButton btn, bool value)
        {
            Color trueCOlor = Color.LimeGreen;
            Color FalseColor = Color.FromArgb(80, 160, 255);
            if (value)
            {
                btn.FillColor = trueCOlor;
                btn.RectColor = trueCOlor;
                btn.FillDisableColor = trueCOlor;
                btn.RectDisableColor = trueCOlor;
                btn.FillHoverColor = trueCOlor;
                btn.FillPressColor = trueCOlor;
                btn.FillSelectedColor = trueCOlor;
            }
            else
            {
                btn.FillColor = FalseColor;
                btn.RectColor = FalseColor;
                btn.FillDisableColor = FalseColor;
                btn.RectDisableColor = FalseColor;
                btn.FillHoverColor = FalseColor;
                btn.FillPressColor = FalseColor;
                btn.FillSelectedColor = FalseColor;
            }
        }

        /// <summary>
        /// 加载DI模块
        /// </summary>
        private void LoaddicDI()
        {
            DicDI.Clear();
        }

        private void LoaddicDOBtn()
        {
            DicDOBtn.Clear();
            UIButton[] labBtns =
            [
               btnWaterPumpStart, btnFaultRemoval, btnElectricalInit, btnSynchronous12, btnSynchronous34
            ];
            foreach (var btn in labBtns)
            {
                DicDOBtn.TryAdd(btn.Tag.ToInt32(), btn);
            }
        }

        //加载AI模块
        private void LoaddicAI()
        {
            DicAI.Clear();
            DicTP.Clear();
            Label[] labAIs =
            [
                LabAI01, LabAI02, LabAI03, LabAI04, LabAI05, LabAI06, LabAI07
            ];
            foreach (var lab in labAIs)
            {
                DicAI.TryAdd(lab.Tag.ToInt32(), lab);
            }

            Label[] labTPs =
            [
                LabTP01, LabTP02, LabTP03, LabTP04, LabTP05,
                LabTP06, LabTP07, LabTP08, LabTP09
            ];
            foreach (var lab in labTPs)
            {
                DicTP.TryAdd(lab.Tag.ToInt32(), lab);
            }
        }

        //加载AI模块
        private void LoaddicDO()
        {
            DicDO.Clear();
            AddContrls(grpDO);
        }

        private void AddBtn()
        {
            pairs.Clear();
            pairs.Add(0, btnDetection);
            pairs.Add(1, btnCurve);
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
                    if (!DicDO.ContainsKey(key))
                        DicDO.Add(key, item as UISwitch);
                }
                AddContrls(item);
            }
        }
        #endregion

        #region 值改变事件
        private void AIgrp_AIvalueGrpChanged(object sender, int index, double value)
        {
            if (DicAI.TryGetValue(index, out Label label))
            {
                label.Text = value.ToString("f1");
            }
        }

        private void AOgrp_AOvalueGrpChanged(object sender, int index, double value)
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
        private void DIgrp_DIGroupChanged(object sender, int index, bool value)
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

        private void DOgrp_DOgrpChanged(object sender, int index, bool value)
        {

        }
        #endregion

        #region 参数
        /// <summary>
        /// 读取配置文件，配置参数
        /// </summary>
        private void InitParaConfig()
        {
            try
            {
                if (VarHelper.TestViewModel == null) return;
                paraconfig = new();
                paraconfig.SetSectionName(VarHelper.ModelTypeName);
                paraconfig.Load();
                BaseTest.para = paraconfig;
                InitItem();
                ModelLoadDSL(VarHelper.TestViewModel.ID);
                if (!string.IsNullOrEmpty(paraconfig.RptFile))
                {
                    rowIndex = 0;
                    rowIndex = 29;
                    RptFilename = paraconfig.RptFile;
                    RptFilePath = Path.GetFileNameWithoutExtension(RptFilename);
                    string RptPath = Application.StartupPath + "reports\\" + RptFilePath;
                    File.Copy(RptPath + ".xlsx", path2, true);
                    ucGrid1.LoadFile(RptPath);
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"加载参数错误：{ex.Message}");
            }
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
                if (string.IsNullOrEmpty(paraconfig?.RptFile))
                {
                    MessageHelper.MessageOK("报表模板未设置，无法保存！");
                    return;
                }
                if (string.IsNullOrEmpty(VarHelper.TestViewModel.ModelName))
                {
                    MessageHelper.MessageOK("型号未选择！");
                    return;
                }

                DialogResult result = MessageBox.Show("是否保存试验结果？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;
                string rootPath = Application.StartupPath + @"Save\";
                if (!Directory.Exists(rootPath)) Directory.CreateDirectory(rootPath);
                string filname = rootPath + VarHelper.TestViewModel.ModelName + "_" + "" + "_" + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
                TestRecordNewBLL recordbll = new();
                recordbll.SaveTestRecord(new TestRecordModel
                {
                    KindID = VarHelper.TestViewModel.TypeID,
                    ModelID = VarHelper.TestViewModel.ID,
                    TestID = txtNumber.Text.Trim(),
                    Tester = NewUsers.NewUserInfo.Username,
                    TestTime = DateTime.Now,
                    ReportPath = filname
                });
                ucGrid1.SaveAsPdf(filname, filname);
                MessageBox.Show("保存成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败:" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 其他
        private void UcHMI_Load(object sender, EventArgs e)
        {
            //InitializeTimer();
            //InitViewRecovery();
            //InitializeParameterNames();
            //InitializeChart();
        }

        private void AppendText(string text)
        {
            //txtTestRecord.AppendText($"{DateTime.Now:HH:mm:ss}：{text}\n");
            //txtTestRecord.ScrollToCaret();
        }

        private void BtnColor(int index)
        {
            foreach (var item in pairs)
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
}