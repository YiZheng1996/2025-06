using System.Runtime.InteropServices;

namespace MainUI
{
    public partial class frmLogin : Form
    {
        private readonly OperateUserBLL _userBLL;

        public frmLogin()
        {
            InitializeComponent();
            _userBLL = new OperateUserBLL();
        }

        #region 窗体拖动相关
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
        }
        #endregion

        private void frmLogin_Load(object sender, EventArgs e)
        {
            InitializeUserComboBox();
            txtPassword.Focus();
        }

        /// <summary>
        /// 初始化用户下拉列表
        /// </summary>
        private void InitializeUserComboBox()
        {
            try
            {
                var users = _userBLL.GetUsers()
                    .Where(u => u.Username != "admin")
                    .ToList();

                cboUserName.DataSource = users;
                cboUserName.DisplayMember = nameof(OperateUserNewModel.Username);
                cboUserName.ValueMember = nameof(OperateUserNewModel.ID);
            }
            catch (Exception ex)
            {
                ShowError("初始化用户列表失败", ex);
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e) => AttemptLogin();

        private void btnExit_Click(object sender, EventArgs e) => Application.Exit();

        /// <summary>
        /// 执行登录验证
        /// </summary>
        private void AttemptLogin()
        {
            try
            {
                string username = cboUserName.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (!ValidateLoginInput(password))
                    return;

                var user = GetUserForAuthentication(username);
                if (user == null)
                {
                    ShowLoginError("未找到该用户!");
                    return;
                }

                if (AuthenticateUser(user, password))
                {
                    CompleteSuccessfulLogin(user);
                }
            }
            catch (Exception ex)
            {
                ShowError("登录过程发生错误", ex);
            }
        }

        private bool ValidateLoginInput(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                ShowLoginError("密码不能为空，请重新输入!");
                txtPassword.Focus();
                return false;
            }
            return true;
        }

        private OperateUserNewModel GetUserForAuthentication(string username)
        {
            if (username == "admin")
            {
                return _userBLL.SelectUser(username);
            }

            return _userBLL.SelectUser(new OperateUserNewModel
            {
                ID = cboUserName.SelectedValue.ToInt32()
            });
        }

        private bool AuthenticateUser(OperateUserNewModel user, string password)
        {
            if (user.Password != password)
            {
                ShowLoginError("密码错误，请重新输入!");
                txtPassword.Focus();
                return false;
            }
            return true;
        }

        private void CompleteSuccessfulLogin(OperateUserNewModel user)
        {
            NewUsers.NewUserInfo.InitUser(user);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ShowLoginError(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "错误", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            NlogHelper.Default.Error(message, ex);
        }
    }
}