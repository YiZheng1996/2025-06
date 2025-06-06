
namespace MainUI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            InitUser();
            txtPassword.Focus();
        }

        private void InitUser()
        {
            OperateUserBLL bLL = new();
            var users = bLL.GetUsers();
            cboUserName.DataSource = users;
            cboUserName.DisplayMember = "Username";
            cboUserName.ValueMember = "ID";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void btnSignIn_Click(object sender, EventArgs e)
        {
            LogOn();
        }

        private void LogOn()
        {
            OperateUserBLL bLL = new();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "密码不能为空，请重新输入!";
                lblMessage.Visible = true;
                txtPassword.Focus();
                return;
            }

            var user = bLL.SelectUser(new OperateUserNewModel
            {
                ID = cboUserName.SelectedValue.ToInt32()
            });
            if (user != null)
            {
                if (user.Password != password)
                {
                    lblMessage.Text = "密码错误，请重新输入!";
                    lblMessage.Visible = true;
                    txtPassword.Focus();
                    return;
                }
                else
                {
                    NewUsers.NewUserInfo.InitUser(user);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                lblMessage.Text = "未找到该用户!";
                lblMessage.Visible = true;
                return;
            }
        }

    }
}