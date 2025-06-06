using Padding = System.Windows.Forms.Padding;

namespace MainUI
{
    partial class frmAboutDevice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            dataGridView = new UIDataGridView();
            colID = new DataGridViewTextBoxColumn();
            colSort = new DataGridViewTextBoxColumn();
            colPermission = new DataGridViewTextBoxColumn();
            colJobNumber = new DataGridViewTextBoxColumn();
            uiPanel2 = new UIPanel();
            dtpStartEnd = new UIDatePicker();
            uiDatePicker1 = new UIDatePicker();
            uiLabel5 = new UILabel();
            uiLabel4 = new UILabel();
            uiLabel1 = new UILabel();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            uiPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(49, 54, 64);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(235, 227, 221);
            dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.BackgroundColor = Color.FromArgb(49, 54, 64);
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(239, 154, 78);
            dataGridViewCellStyle2.Font = new Font("微软雅黑", 12F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(235, 227, 221);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(239, 154, 78);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(235, 227, 221);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView.ColumnHeadersHeight = 32;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { colID, colSort, colPermission, colJobNumber });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(220, 236, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.Font = new Font("微软雅黑", 12F);
            dataGridView.GridColor = Color.FromArgb(42, 47, 55);
            dataGridView.Location = new Point(24, 118);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RectColor = Color.FromArgb(49, 54, 64);
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(243, 249, 255);
            dataGridViewCellStyle4.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(49, 54, 64);
            dataGridViewCellStyle5.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.FromArgb(235, 227, 221);
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(235, 227, 221);
            dataGridViewCellStyle5.SelectionForeColor = Color.FromArgb(49, 54, 64);
            dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridView.RowTemplate.Height = 35;
            dataGridView.ScrollBarBackColor = Color.FromArgb(49, 54, 64);
            dataGridView.ScrollBarColor = Color.FromArgb(239, 154, 78);
            dataGridView.ScrollBarRectColor = Color.FromArgb(239, 154, 78);
            dataGridView.ScrollBarStyleInherited = false;
            dataGridView.SelectedIndex = -1;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(703, 423);
            dataGridView.StripeEvenColor = Color.FromArgb(49, 54, 64);
            dataGridView.StripeOddColor = Color.FromArgb(49, 54, 64);
            dataGridView.TabIndex = 400;
            // 
            // colID
            // 
            colID.DataPropertyName = "ID";
            colID.HeaderText = "ID";
            colID.Name = "colID";
            colID.ReadOnly = true;
            colID.Visible = false;
            colID.Width = 250;
            // 
            // colSort
            // 
            colSort.DataPropertyName = "Sort";
            colSort.HeaderText = "设备开关机时间";
            colSort.Name = "colSort";
            colSort.ReadOnly = true;
            colSort.Width = 300;
            // 
            // colPermission
            // 
            colPermission.DataPropertyName = "Permission";
            colPermission.HeaderText = "每天使用时长(H)";
            colPermission.Name = "colPermission";
            colPermission.ReadOnly = true;
            colPermission.Width = 200;
            // 
            // colJobNumber
            // 
            colJobNumber.DataPropertyName = "JobNumber";
            colJobNumber.HeaderText = "月使用时长(H)";
            colJobNumber.Name = "colJobNumber";
            colJobNumber.ReadOnly = true;
            colJobNumber.Width = 200;
            // 
            // uiPanel2
            // 
            uiPanel2.Controls.Add(uiLabel1);
            uiPanel2.Controls.Add(uiLabel4);
            uiPanel2.Controls.Add(uiLabel5);
            uiPanel2.Controls.Add(uiDatePicker1);
            uiPanel2.Controls.Add(dtpStartEnd);
            uiPanel2.FillColor = Color.FromArgb(49, 54, 64);
            uiPanel2.FillColor2 = Color.FromArgb(49, 54, 64);
            uiPanel2.FillDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel2.Font = new Font("宋体", 12F);
            uiPanel2.ForeColor = Color.FromArgb(49, 54, 64);
            uiPanel2.ForeDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel2.Location = new Point(24, 48);
            uiPanel2.Margin = new Padding(4, 5, 4, 5);
            uiPanel2.MinimumSize = new Size(1, 1);
            uiPanel2.Name = "uiPanel2";
            uiPanel2.Radius = 15;
            uiPanel2.RectColor = Color.FromArgb(49, 54, 64);
            uiPanel2.RectDisableColor = Color.FromArgb(49, 54, 64);
            uiPanel2.Size = new Size(703, 62);
            uiPanel2.TabIndex = 409;
            uiPanel2.Text = null;
            uiPanel2.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // dtpStartEnd
            // 
            dtpStartEnd.FillColor = Color.FromArgb(43, 46, 57);
            dtpStartEnd.FillColor2 = Color.FromArgb(43, 46, 57);
            dtpStartEnd.FillDisableColor = Color.FromArgb(43, 46, 57);
            dtpStartEnd.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            dtpStartEnd.ForeColor = Color.FromArgb(235, 227, 221);
            dtpStartEnd.ForeDisableColor = Color.FromArgb(235, 227, 221);
            dtpStartEnd.Location = new Point(554, 17);
            dtpStartEnd.Margin = new Padding(4, 5, 4, 5);
            dtpStartEnd.MaxLength = 10;
            dtpStartEnd.MinimumSize = new Size(63, 0);
            dtpStartEnd.Name = "dtpStartEnd";
            dtpStartEnd.Padding = new Padding(0, 0, 30, 2);
            dtpStartEnd.RadiusSides = UICornerRadiusSides.None;
            dtpStartEnd.RectColor = Color.Silver;
            dtpStartEnd.RectDisableColor = Color.Silver;
            dtpStartEnd.RectSides = ToolStripStatusLabelBorderSides.None;
            dtpStartEnd.Size = new Size(131, 29);
            dtpStartEnd.SymbolDropDown = 61555;
            dtpStartEnd.SymbolNormal = 61555;
            dtpStartEnd.SymbolSize = 24;
            dtpStartEnd.TabIndex = 392;
            dtpStartEnd.Text = "2025-05-01";
            dtpStartEnd.TextAlignment = ContentAlignment.MiddleCenter;
            dtpStartEnd.Value = new DateTime(2025, 5, 1, 0, 0, 0, 0);
            dtpStartEnd.Watermark = "";
            // 
            // uiDatePicker1
            // 
            uiDatePicker1.FillColor = Color.FromArgb(43, 46, 57);
            uiDatePicker1.FillColor2 = Color.FromArgb(43, 46, 57);
            uiDatePicker1.FillDisableColor = Color.FromArgb(43, 46, 57);
            uiDatePicker1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiDatePicker1.ForeColor = Color.FromArgb(235, 227, 221);
            uiDatePicker1.ForeDisableColor = Color.FromArgb(235, 227, 221);
            uiDatePicker1.Location = new Point(381, 17);
            uiDatePicker1.Margin = new Padding(4, 5, 4, 5);
            uiDatePicker1.MaxLength = 10;
            uiDatePicker1.MinimumSize = new Size(63, 0);
            uiDatePicker1.Name = "uiDatePicker1";
            uiDatePicker1.Padding = new Padding(0, 0, 30, 2);
            uiDatePicker1.RadiusSides = UICornerRadiusSides.None;
            uiDatePicker1.RectColor = Color.Silver;
            uiDatePicker1.RectDisableColor = Color.Silver;
            uiDatePicker1.RectSides = ToolStripStatusLabelBorderSides.None;
            uiDatePicker1.Size = new Size(131, 29);
            uiDatePicker1.SymbolDropDown = 61555;
            uiDatePicker1.SymbolNormal = 61555;
            uiDatePicker1.SymbolSize = 24;
            uiDatePicker1.TabIndex = 393;
            uiDatePicker1.Text = "2025-02-01";
            uiDatePicker1.TextAlignment = ContentAlignment.MiddleCenter;
            uiDatePicker1.Value = new DateTime(2025, 2, 1, 0, 0, 0, 0);
            uiDatePicker1.Watermark = "";
            // 
            // uiLabel5
            // 
            uiLabel5.BackColor = Color.FromArgb(49, 54, 64);
            uiLabel5.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel5.ForeColor = Color.FromArgb(235, 227, 221);
            uiLabel5.Location = new Point(519, 17);
            uiLabel5.Name = "uiLabel5";
            uiLabel5.Size = new Size(25, 23);
            uiLabel5.TabIndex = 394;
            uiLabel5.Text = "至";
            uiLabel5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel4
            // 
            uiLabel4.BackColor = Color.FromArgb(49, 54, 64);
            uiLabel4.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel4.ForeColor = Color.FromArgb(235, 227, 221);
            uiLabel4.Location = new Point(286, 18);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(91, 23);
            uiLabel4.TabIndex = 395;
            uiLabel4.Text = "记录时间:";
            uiLabel4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel1
            // 
            uiLabel1.BackColor = Color.FromArgb(49, 54, 64);
            uiLabel1.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(235, 227, 221);
            uiLabel1.Location = new Point(16, 18);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(243, 23);
            uiLabel1.TabIndex = 396;
            uiLabel1.Text = "软件使用时长：36天06小时37分";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmAboutDevice
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(42, 47, 55);
            ClientSize = new Size(751, 558);
            ControlBox = false;
            Controls.Add(uiPanel2);
            Controls.Add(dataGridView);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAboutDevice";
            RectColor = Color.FromArgb(42, 47, 55);
            ShowIcon = false;
            Text = "关于设备";
            TitleColor = Color.FromArgb(47, 55, 64);
            TitleFont = new Font("思源黑体 CN Heavy", 15F, FontStyle.Bold);
            TitleForeColor = Color.FromArgb(239, 154, 78);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            uiPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private UIDataGridView dataGridView;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colSort;
        private DataGridViewTextBoxColumn colPermission;
        private DataGridViewTextBoxColumn colJobNumber;
        private UIPanel uiPanel2;
        private UIDatePicker dtpStartEnd;
        private UIDatePicker uiDatePicker1;
        private UILabel uiLabel5;
        private UILabel uiLabel4;
        private UILabel uiLabel1;
    }
}