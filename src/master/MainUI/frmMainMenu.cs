using MainUI.Procedure.DSL;
using MainUI.Service;
using Timer = System.Windows.Forms.Timer;

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
    private readonly TimeTrackingService _timeTrackingService;

    public frmMainMenu()
    {
        InitializeComponent();
        InitializeBasicSettings();
        _timeTrackingService = new TimeTrackingService();
    }

    #region 初始化
    /// <summary>
    /// 初始化基本设置
    /// </summary>
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

    /// <summary>
    /// 初始化组件
    /// </summary>
    private void InitializeComponents()
    {
        _hmi = new UcHMI { Dock = DockStyle.Fill };
        _hmi.Init();
        _hardWare = new frmHardWare();
        _opcStatus = OPCHelper.opcStatus;
    }

    /// <summary>
    /// 初始化事件
    /// </summary>
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

    /// <summary>
    /// 初始化HMI
    /// </summary>
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
    #endregion

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
        _hmi.ParaRefresh();
    }

    /// <summary>
    /// 配置主数据节点设置
    /// </summary>
    /// <param name="main">主设置窗体实例</param>
    private void ConfigureMainDataNodes(frmSettingMain main)
    {
        ArgumentNullException.ThrowIfNull(main);
        try
        {
            var userId = NewUsers.NewUserInfo.ID;
            if (IsAdminUser(userId))
            {
                ConfigureAdminNodes(main);
                return;
            }
            ConfigureRegularUserNodes(main, userId);
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"配置主数据节点失败: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 节点配置列表
    /// </summary>
    /// <returns></returns>
    private static (string Name, UserControl Control, int Index)[]
      GetMainDataNodes() =>
      [
         ("用户管理", new ucUserManager(), 0),
         ("角色管理", new ucRole(), 6),
         ("权限管理", new ucPermission(), 7),
         ("权限分配", new ucPermissionAllocation(), 8),
         ("类型管理", new ucKindManage(), 1),
         ("型号管理", new ucModelManage(), 9),
         ("模块注入", new ucModules(), 3),
         ("项点管理", new ucItemManagerial(), 4),
         ("项点配置", new ucItemConfiguration(), 5),
         ("试验参数", new ucTestParams(), 2)
      ];

    /// <summary>
    /// 检查是否为管理员用户
    /// </summary>
    private static bool IsAdminUser(int userId) => userId == 1;

    /// <summary>
    /// 配置管理员节点
    /// </summary>
    private static void ConfigureAdminNodes(frmSettingMain main)
    {
        foreach (var node in GetMainDataNodes())
        {
            main.AddNodes(node.Name, node.Control, node.Index);
        }
    }

    /// <summary>
    /// 配置普通用户节点
    /// </summary>
    private void ConfigureRegularUserNodes(frmSettingMain main, int userId)
    {
        var permissionConfigs = GetPermissionConfigurations();
        var nodes = GetMainDataNodes()
            .Where(node => !IsRestrictedNode(node.Name));

        foreach (var node in nodes)
        {
            AddNodeWithPermissionCheck(main, node, permissionConfigs);
        }
    }

    /// <summary>
    /// 检查是否为受限节点
    /// </summary>
    private static bool IsRestrictedNode(string nodeName) => 
        nodeName is "权限管理" or "权限分配";

    /// <summary>
    /// 获取权限配置信息
    /// </summary>
    private Dictionary<string, string> GetPermissionConfigurations()
    {
        try
        {
            var permissionConfigBLL = new PermissionBLL();
            return permissionConfigBLL.GetPermissions()
                .Where(p => !string.IsNullOrEmpty(p.ControlName))
                .ToDictionary(
                    p => p.ControlName,
                    p => p.PermissionCode,
                    StringComparer.OrdinalIgnoreCase); //大小写
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"获取权限配置失败: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 添加带权限检查的节点
    /// </summary>
    private void AddNodeWithPermissionCheck(
        frmSettingMain main,
        (string Name, UserControl Control, int Index) node,
        Dictionary<string, string> permissionConfigs)
    {
        try
        {
            if (!TryGetPermissionCode(node.Control.Name, permissionConfigs, out var code))
            {
                return;
            }

            if (PermissionHelper.HasPermission(NewUsers.NewUserInfo.ID, code))
            {
                main.AddNodes(node.Name, node.Control, node.Index);
            }
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"添加节点[{node.Name}]失败：{ex.Message}");
        }
    }

    /// <summary>
    /// 尝试获取权限代码
    /// </summary>
    private bool TryGetPermissionCode(
        string controlName,
        Dictionary<string, string> permissionConfigs,
        out string code)
    {
        if (!permissionConfigs.TryGetValue(controlName, out code))
        {
            NlogHelper.Default.Warn($"控件[{controlName}]未找到对应的权限配置");
            return false;
        }
        return true;
    }

    private void ShowDialog<T>(object sender = null) where T : Form, new()
    {
        var btn = sender as UIButton;
        if (btn.Tag != null && !CheckPermission(btn.Tag.ToString()))
            return;

        using var form = new T();
        VarHelper.ShowDialogWithOverlay(this, form);
    }

    private void btnHardwareTest_Click(object sender, EventArgs e) =>
        ShowDialog<frmHardWare>(sender);

    private void btnReports_Click(object sender, EventArgs e) =>
        ShowDialog<frmDataManager>(sender);

    private void btnChangePwd_Click(object sender, EventArgs e) =>
        ShowDialog<frmRemindEdit>(sender);

    private void btnNLog_Click(object sender, EventArgs e) =>
        ShowDialog<frmNLogs>(sender);

    private void btnMeteringRemind_Click(object sender, EventArgs e) =>
        ShowDialog<frmMeteringRemind>(sender);

    private void btnDeviceDetection_Click(object sender, EventArgs e) =>
        ShowDialog<frmDeviceInspect>(sender);

    private void btnErrStatistics_Click(object sender, EventArgs e) =>
        ShowDialog<frmErrStatistics>(sender);

    private void btnAboutDevice_Click(object sender, EventArgs e) =>
        ShowDialog<frmAboutDevice>(sender);
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

        //var SysTime = $"系统运行时间：{TimeTrackingService.FormatTimeSpan(_timeTrackingService.GetSystemUptime())}";
        //var RunTime = $"软件运行时间：{TimeTrackingService.FormatTimeSpan(_timeTrackingService.GetApplicationUptime())}";
    }

    private void frmMainMenu_FormClosing(object sender, FormClosingEventArgs e)
    {
   
    }
    #endregion
}
