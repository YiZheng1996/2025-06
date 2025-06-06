using MainUI.Procedure.DSL;

namespace MainUI;
public partial class frmMainMenu : Form
{
    private UcHMI _hmi;
    private OpcStatusGrp _opcStatus;
    private frmHardWare _hardWare;

    [System.Runtime.InteropServices.LibraryImport("user32.dll")]
    [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
    public static partial bool ReleaseCapture();
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
    public const int WM_SYSCOMMAND = 0x0112;
    public const int SC_MOVE = 0xF010;
    public const int HTCAPTION = 0x0002;

    public frmMainMenu()
    {
        InitializeComponent();
        InitializeBasicSettings();
    }

    private void InitializeBasicSettings()
    {
        try
        {
            timerHeartbeat.Start();
            Text = VarHelper.SoftName;
            lblTitle.Text = VarHelper.SoftName;
            CheckForIllegalCrossThreadCalls = false;
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"初始化基本设置失败：{ex.Message}");
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        try
        {
            // 1. 初始化组件
            InitializeComponents();

            // 2. 初始化权限
            InitializePermissions();

            // 3. 初始化事件
            InitializeEvents();

            // 4. 初始化HMI
            InitializeHMI();
        }
        catch (Exception ex)
        {
            MessageHelper.MessageOK($"窗体初始化失败：{ex.Message}");
        }
    }

    private void InitializeComponents()
    {
        _hmi = new UcHMI { Dock = DockStyle.Fill };
        _hmi.Init();
        _hardWare = new frmHardWare();
        _opcStatus = OPCHelper.opcStatus;
    }

    private void InitializeEvents()
    {
        try
        {
            // HMI事件
            _hmi.EmergencyStatusChanged += OnEmergencyStatusChanged;
            _hmi.TestStateChanged += OnTestStateChanged;

            // 测试状态事件
            BaseTest.TestStateChanged += OnBaseTestStateChanged;
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"初始化事件失败：{ex.Message}");
            throw;
        }
    }

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

    private void InitializeHMI()
    {
        try
        {
            _hardWare.InitZeroGain();
            PanelHmi.Controls.Add(_hmi);
            timerPLC.Enabled = true;
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"初始化HMI失败：{ex.Message}");
            throw;
        }
    }

    #region 权限验证
    private bool CheckPermission(string permissionCode)
    {
        if (!PermissionHelper.HasPermission(NewUsers.NewUserInfo.ID, permissionCode))
        {
            MessageHelper.MessageOK("您没有执行此操作的权限！");
            return false;
        }
        return true;
    }
    #endregion

    #region 事件处理
    private void OnTestStateChanged(bool isTesting) => UpdateControlsState(!isTesting);

    private void OnBaseTestStateChanged(bool isTesting) => UpdateControlsState(!isTesting);

    private void OnEmergencyStatusChanged(bool enabled)
    {
        // 更新控件状态
        UpdateControlsState(enabled);

        // 更新急停状态图标
        picRunStatus.Image = enabled ? Resources.noemergent : Resources.emergent;
        PanelHmi.Enabled = enabled;
    }

    private void UpdateControlsState(bool enabled)
    {
        // 批量更新按钮状态
        var buttons = new[]
        {
            btnNLog, btnReports, btnHardwareTest, btnMainData,
            btnChangePwd, btnExit, btnMeteringRemind, btnErrStatistics,
            btnDeviceDetection, btnAboutDevice
        };

        foreach (var button in buttons)
        {
            button.Enabled = enabled;
        }
    }
    #endregion

    #region 窗体拖动
    private void lblTitle_MouseDown(object sender, MouseEventArgs e)
    {
        ReleaseCapture();
        SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
    }
    #endregion

    #region 功能按钮事件处理
    private void btnMainData_Click(object sender, EventArgs e)
    {
        using var main = new frmSettingMain();
        ConfigureMainDataNodes(main);
        VarHelper.ShowDialogWithOverlay(this, main);
        _hmi.sRefresh();
    }

    private void ConfigureMainDataNodes(frmSettingMain main)
    {
        var nodes = new (string Name, UserControl Control, int Index)[]
        {
            ("用户管理", new ucUserManager(), 0),
            ("角色管理", new ucRole(), 6),
            ("权限管理", new ucPermission(), 7),
            ("权限分配", new ucPermissionAllocation(), 8),
            ("类型管理", new ucKindManage(), 1),
            ("型号管理", new ucTypeManage(), 2),
            ("模块注入", new ucModules(), 3),
            ("项点管理", new ucItemManagerial(), 4),
            ("项点配置", new ucItemConfiguration(), 5)
        };

        foreach (var node in nodes)
        {
            main.AddNodes(node.Name, node.Control, node.Index);
        }
    }

    private void ShowDialog<T>(string permissionCode = null) where T : Form, new()
    {
        if (permissionCode != null && !CheckPermission(permissionCode))
            return;

        using var form = new T();
        VarHelper.ShowDialogWithOverlay(this, form);
    }

    private void btnHardwareTest_Click(object sender, EventArgs e) =>
        ShowDialog<frmHardWare>();

    private void btnReports_Click(object sender, EventArgs e) =>
        ShowDialog<frmDataManager>();

    private void btnChangePwd_Click(object sender, EventArgs e) =>
        ShowDialog<frmRemindEdit>();

    private void btnNLog_Click(object sender, EventArgs e) =>
        ShowDialog<frmNLogs>();

    private void btnMeteringRemind_Click(object sender, EventArgs e) =>
        ShowDialog<frmMeteringRemind>();

    private void btnDeviceDetection_Click(object sender, EventArgs e) =>
        ShowDialog<frmDeviceInspect>();

    private void btnErrStatistics_Click(object sender, EventArgs e) =>
        ShowDialog<frmErrStatistics>();

    private void btnAboutDevice_Click(object sender, EventArgs e) =>
        ShowDialog<frmAboutDevice>();
    #endregion

    #region 系统操作
    private void btnLogout_Click(object sender, EventArgs e) =>
        Application.Restart();

    private void btnExit_Click(object sender, EventArgs e)
    {
        if (MessageHelper.MessageYes(this, "确定要退出试验吗？") != DialogResult.OK)
            return;

        try
        {
            OPCHelper.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        finally
        {
            Application.Exit();
        }
    }

    #endregion

    #region 定时器事件
    private void timerPLC_Tick(object sender, EventArgs e)
    {
        try
        {
            UpdateUI();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void UpdateUI()
    {
        // 更新时间显示
        lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // 更新用户信息
        var userInfo = NewUsers.NewUserInfo;
        tslblUser.Text = $"当前登录用户： {userInfo.Username}  当前权限：{userInfo.Describe} ";

        // 更新PLC状态
        tslblPLC.Text = _opcStatus.NoError ? " PLC连接正常 " : " PLC连接失败 ";
        tslblPLC.BackColor = _opcStatus.NoError ? Color.FromArgb(110, 190, 40) : Color.Salmon;

        if (_opcStatus.Simulated)
        {
            tslblPLC.Text += " 仿真模式 ";
        }
    }
    #endregion
}
