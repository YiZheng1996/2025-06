namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    partial class Form_WriteCells
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
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            BtnDelete = new UISymbolButton();
            uiSymbolButton1 = new UISymbolButton();
            DataGridViewDefineVar = new UIDataGridView();
            ColVarName = new DataGridViewTextBoxColumn();
            ColVarType = new DataGridViewComboBoxColumn();
            ColVarText = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).BeginInit();
            SuspendLayout();
            // 
            // BtnDelete
            // 
            BtnDelete.Cursor = Cursors.Hand;
            BtnDelete.FillColor = Color.DodgerBlue;
            BtnDelete.FillColor2 = Color.DodgerBlue;
            BtnDelete.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            BtnDelete.LightColor = Color.FromArgb(248, 248, 248);
            BtnDelete.Location = new Point(136, 479);
            BtnDelete.MinimumSize = new Size(1, 1);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.RectColor = Color.DodgerBlue;
            BtnDelete.RectDisableColor = Color.DodgerBlue;
            BtnDelete.RectHoverColor = Color.FromArgb(64, 128, 204);
            BtnDelete.Size = new Size(132, 39);
            BtnDelete.Style = UIStyle.Custom;
            BtnDelete.Symbol = 561695;
            BtnDelete.SymbolSize = 32;
            BtnDelete.TabIndex = 13;
            BtnDelete.Text = "删除";
            BtnDelete.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // uiSymbolButton1
            // 
            uiSymbolButton1.Cursor = Cursors.Hand;
            uiSymbolButton1.FillColor = Color.DodgerBlue;
            uiSymbolButton1.FillColor2 = Color.DodgerBlue;
            uiSymbolButton1.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiSymbolButton1.LightColor = Color.FromArgb(248, 248, 248);
            uiSymbolButton1.Location = new Point(370, 479);
            uiSymbolButton1.MinimumSize = new Size(1, 1);
            uiSymbolButton1.Name = "uiSymbolButton1";
            uiSymbolButton1.RectColor = Color.DodgerBlue;
            uiSymbolButton1.RectDisableColor = Color.DodgerBlue;
            uiSymbolButton1.Size = new Size(132, 39);
            uiSymbolButton1.Style = UIStyle.Custom;
            uiSymbolButton1.Symbol = 61639;
            uiSymbolButton1.SymbolSize = 32;
            uiSymbolButton1.TabIndex = 12;
            uiSymbolButton1.Text = "保存";
            uiSymbolButton1.TipsFont = new Font("宋体", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // DataGridViewDefineVar
            // 
            dataGridViewCellStyle6.BackColor = Color.FromArgb(235, 243, 255);
            DataGridViewDefineVar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            DataGridViewDefineVar.BackgroundColor = Color.White;
            DataGridViewDefineVar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle7.Font = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            dataGridViewCellStyle7.ForeColor = Color.White;
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            DataGridViewDefineVar.ColumnHeadersHeight = 35;
            DataGridViewDefineVar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewDefineVar.Columns.AddRange(new DataGridViewColumn[] { ColVarName, ColVarType, ColVarText });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.White;
            dataGridViewCellStyle8.Font = new Font("宋体", 13F);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = Color.White;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            DataGridViewDefineVar.DefaultCellStyle = dataGridViewCellStyle8;
            DataGridViewDefineVar.EnableHeadersVisualStyles = false;
            DataGridViewDefineVar.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            DataGridViewDefineVar.GridColor = Color.FromArgb(64, 64, 64);
            DataGridViewDefineVar.Location = new Point(24, 56);
            DataGridViewDefineVar.MultiSelect = false;
            DataGridViewDefineVar.Name = "DataGridViewDefineVar";
            DataGridViewDefineVar.RectColor = Color.White;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(248, 248, 248);
            dataGridViewCellStyle9.Font = new Font("微软雅黑", 13F);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle9.SelectionForeColor = Color.White;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            DataGridViewDefineVar.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            DataGridViewDefineVar.RowHeadersWidth = 28;
            dataGridViewCellStyle10.BackColor = Color.White;
            dataGridViewCellStyle10.Font = new Font("微软雅黑", 13F);
            DataGridViewDefineVar.RowsDefaultCellStyle = dataGridViewCellStyle10;
            DataGridViewDefineVar.RowTemplate.Height = 30;
            DataGridViewDefineVar.SelectedIndex = -1;
            DataGridViewDefineVar.SelectionMode = DataGridViewSelectionMode.CellSelect;
            DataGridViewDefineVar.Size = new Size(591, 409);
            DataGridViewDefineVar.StripeOddColor = Color.FromArgb(235, 243, 255);
            DataGridViewDefineVar.Style = UIStyle.Custom;
            DataGridViewDefineVar.TabIndex = 11;
            // 
            // ColVarName
            // 
            ColVarName.HeaderText = "单元格名称";
            ColVarName.Name = "ColVarName";
            ColVarName.Width = 150;
            // 
            // ColVarType
            // 
            ColVarType.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            ColVarType.HeaderText = "填写类型";
            ColVarType.Items.AddRange(new object[] { "自定义", "操作员", "产品类型", "产品型号", "产品图号", "当前时间", "自定义变量" });
            ColVarType.Name = "ColVarType";
            ColVarType.Resizable = DataGridViewTriState.True;
            ColVarType.SortMode = DataGridViewColumnSortMode.Automatic;
            ColVarType.Width = 150;
            // 
            // ColVarText
            // 
            ColVarText.HeaderText = "自定义填写内容";
            ColVarText.Name = "ColVarText";
            ColVarText.Width = 260;
            // 
            // Form_ReadCells
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            ClientSize = new Size(639, 533);
            Controls.Add(BtnDelete);
            Controls.Add(uiSymbolButton1);
            Controls.Add(DataGridViewDefineVar);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_ReadCells";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            Text = "写入单元格";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 14.25F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            ((System.ComponentModel.ISupportInitialize)DataGridViewDefineVar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private UISymbolButton BtnDelete;
        private UISymbolButton uiSymbolButton1;
        private UIDataGridView DataGridViewDefineVar;
        private DataGridViewTextBoxColumn ColVarName;
        private DataGridViewComboBoxColumn ColVarType;
        private DataGridViewTextBoxColumn ColVarText;
    }
}