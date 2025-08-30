using AntdUI;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    partial class ExpressionBuilderDialog
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
            this.components = new System.ComponentModel.Container();
            this.grpExpression = new UIGroupBox();
            this.grpButtons = new UIGroupBox();
            this.lblExpression = new UILabel();
            this.txtExpression = new UITextBox();
            this.lblVariables = new UILabel();
            this.lstVariables = new UIListBox();
            this.lblFunctions = new UILabel();
            this.lstFunctions = new UIListBox();
            this.lblOperators = new UILabel();
            this.lstOperators = new UIListBox();
            this.lblPreview = new UILabel();
            this.rtbPreview = new UIRichTextBox();
            this.lblValidationResult = new UILabel();
            this.btnValidate = new UIButton();
            this.btnOK = new UISymbolButton();
            this.btnCancel = new UISymbolButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpExpression.SuspendLayout();
            this.grpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpExpression
            // 
            this.grpExpression.BackColor = System.Drawing.Color.Transparent;
            this.grpExpression.Controls.Add(this.btnValidate);
            this.grpExpression.Controls.Add(this.lblValidationResult);
            this.grpExpression.Controls.Add(this.rtbPreview);
            this.grpExpression.Controls.Add(this.lblPreview);
            this.grpExpression.Controls.Add(this.lstOperators);
            this.grpExpression.Controls.Add(this.lblOperators);
            this.grpExpression.Controls.Add(this.lstFunctions);
            this.grpExpression.Controls.Add(this.lblFunctions);
            this.grpExpression.Controls.Add(this.lstVariables);
            this.grpExpression.Controls.Add(this.lblVariables);
            this.grpExpression.Controls.Add(this.txtExpression);
            this.grpExpression.Controls.Add(this.lblExpression);
            this.grpExpression.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.grpExpression.Location = new System.Drawing.Point(10, 10);
            this.grpExpression.Name = "grpExpression";
            this.grpExpression.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.grpExpression.Size = new System.Drawing.Size(870, 420);
            this.grpExpression.TabIndex = 0;
            this.grpExpression.Text = "表达式编辑";
            this.grpExpression.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpButtons
            // 
            this.grpButtons.BackColor = System.Drawing.Color.Transparent;
            this.grpButtons.Controls.Add(this.btnCancel);
            this.grpButtons.Controls.Add(this.btnOK);
            this.grpButtons.Location = new System.Drawing.Point(10, 440);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.RectColor = System.Drawing.Color.Transparent;
            this.grpButtons.Size = new System.Drawing.Size(870, 80);
            this.grpButtons.TabIndex = 1;
            this.grpButtons.Text = "";
            // 
            // lblExpression
            // 
            this.lblExpression.BackColor = System.Drawing.Color.Transparent;
            this.lblExpression.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            // lblExpression
            // 
            this.lblExpression.BackColor = System.Drawing.Color.Transparent;
            this.lblExpression.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblExpression.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblExpression.Location = new System.Drawing.Point(10, 30);
            this.lblExpression.Name = "lblExpression";
            this.lblExpression.Size = new System.Drawing.Size(100, 25);
            this.lblExpression.TabIndex = 0;
            this.lblExpression.Text = "表达式:";
            this.lblExpression.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtExpression
            // 
            this.txtExpression.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtExpression.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtExpression.Location = new System.Drawing.Point(10, 60);
            this.txtExpression.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtExpression.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtExpression.Multiline = true;
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Padding = new System.Windows.Forms.Padding(5);
            this.txtExpression.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.txtExpression.ShowScrollBar = true;
            this.txtExpression.ShowText = false;
            this.txtExpression.Size = new System.Drawing.Size(620, 80);
            this.txtExpression.TabIndex = 1;
            this.txtExpression.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.toolTip.SetToolTip(this.txtExpression, "在此输入表达式，例如: {Variable1} + {Variable2} * 10");
            this.txtExpression.Watermark = "在此输入表达式，例如: {Variable1} + {Variable2} * 10";
            // 
            // lblVariables
            // 
            this.lblVariables.BackColor = System.Drawing.Color.Transparent;
            this.lblVariables.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblVariables.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblVariables.Location = new System.Drawing.Point(10, 160);
            this.lblVariables.Name = "lblVariables";
            this.lblVariables.Size = new System.Drawing.Size(100, 25);
            this.lblVariables.TabIndex = 2;
            this.lblVariables.Text = "可用变量:";
            this.lblVariables.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstVariables
            // 
            this.lstVariables.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lstVariables.ItemSelectForeColor = System.Drawing.Color.White;
            this.lstVariables.Location = new System.Drawing.Point(10, 185);
            this.lstVariables.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstVariables.MinimumSize = new System.Drawing.Size(1, 1);
            this.lstVariables.Name = "lstVariables";
            this.lstVariables.Padding = new System.Windows.Forms.Padding(2);
            this.lstVariables.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.lstVariables.ShowText = false;
            this.lstVariables.Size = new System.Drawing.Size(200, 200);
            this.lstVariables.TabIndex = 3;
            this.toolTip.SetToolTip(this.lstVariables, "双击变量名插入到表达式中");
            // 
            // lblFunctions
            // 
            this.lblFunctions.BackColor = System.Drawing.Color.Transparent;
            this.lblFunctions.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblFunctions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblFunctions.Location = new System.Drawing.Point(230, 160);
            this.lblFunctions.Name = "lblFunctions";
            this.lblFunctions.Size = new System.Drawing.Size(100, 25);
            this.lblFunctions.TabIndex = 4;
            this.lblFunctions.Text = "内置函数:";
            this.lblFunctions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstFunctions
            // 
            this.lstFunctions.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lstFunctions.ItemSelectForeColor = System.Drawing.Color.White;
            this.lstFunctions.Location = new System.Drawing.Point(230, 185);
            this.lstFunctions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstFunctions.MinimumSize = new System.Drawing.Size(1, 1);
            this.lstFunctions.Name = "lstFunctions";
            this.lstFunctions.Padding = new System.Windows.Forms.Padding(2);
            this.lstFunctions.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.lstFunctions.ShowText = false;
            this.lstFunctions.Size = new System.Drawing.Size(200, 200);
            this.lstFunctions.TabIndex = 5;
            this.toolTip.SetToolTip(this.lstFunctions, "双击函数名插入到表达式中");
            // 
            // lblOperators
            // 
            this.lblOperators.BackColor = System.Drawing.Color.Transparent;
            this.lblOperators.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblOperators.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblOperators.Location = new System.Drawing.Point(450, 160);
            this.lblOperators.Name = "lblOperators";
            this.lblOperators.Size = new System.Drawing.Size(100, 25);
            this.lblOperators.TabIndex = 6;
            this.lblOperators.Text = "运算符:";
            this.lblOperators.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstOperators
            // 
            this.lstOperators.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lstOperators.ItemSelectForeColor = System.Drawing.Color.White;
            this.lstOperators.Location = new System.Drawing.Point(450, 185);
            this.lstOperators.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstOperators.MinimumSize = new System.Drawing.Size(1, 1);
            this.lstOperators.Name = "lstOperators";
            this.lstOperators.Padding = new System.Windows.Forms.Padding(2);
            this.lstOperators.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.lstOperators.ShowText = false;
            this.lstOperators.Size = new System.Drawing.Size(180, 200);
            this.lstOperators.TabIndex = 7;
            this.toolTip.SetToolTip(this.lstOperators, "双击运算符插入到表达式中");
            // 
            // lblPreview
            // 
            this.lblPreview.BackColor = System.Drawing.Color.Transparent;
            this.lblPreview.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblPreview.Location = new System.Drawing.Point(650, 30);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(100, 25);
            this.lblPreview.TabIndex = 8;
            this.lblPreview.Text = "实时预览:";
            this.lblPreview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rtbPreview
            // 
            this.rtbPreview.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.rtbPreview.Location = new System.Drawing.Point(650, 60);
            this.rtbPreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rtbPreview.MinimumSize = new System.Drawing.Size(1, 1);
            this.rtbPreview.Name = "rtbPreview";
            this.rtbPreview.Padding = new System.Windows.Forms.Padding(2);
            this.rtbPreview.ReadOnly = true;
            this.rtbPreview.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.rtbPreview.ShowText = false;
            this.rtbPreview.Size = new System.Drawing.Size(200, 130);
            this.rtbPreview.TabIndex = 9;
            this.rtbPreview.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.toolTip.SetToolTip(this.rtbPreview, "显示表达式的预期计算结果");
            // 
            // lblValidationResult
            // 
            this.lblValidationResult.BackColor = System.Drawing.Color.Transparent;
            this.lblValidationResult.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblValidationResult.ForeColor = System.Drawing.Color.Gray;
            this.lblValidationResult.Location = new System.Drawing.Point(10, 400);
            this.lblValidationResult.Name = "lblValidationResult";
            this.lblValidationResult.Size = new System.Drawing.Size(620, 25);
            this.lblValidationResult.TabIndex = 10;
            this.lblValidationResult.Text = "验证结果: 等待输入";
            this.lblValidationResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnValidate
            // 
            this.btnValidate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnValidate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnValidate.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnValidate.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnValidate.Location = new System.Drawing.Point(650, 210);
            this.btnValidate.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(100, 35);
            this.btnValidate.TabIndex = 11;
            this.btnValidate.Text = "验证表达式";
            this.btnValidate.TipsFont = new System.Drawing.Font("微软雅黑", 9F);
            this.toolTip.SetToolTip(this.btnValidate, "手动验证当前表达式的语法和逻辑");
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnOK.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(100)))), ((int)(((byte)(204)))));
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnOK.Location = new System.Drawing.Point(750, 20);
            this.btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 40);
            this.btnOK.Symbol = 61528;
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "确定";
            this.btnOK.TipsFont = new System.Drawing.Font("微软雅黑", 9F);
            this.toolTip.SetToolTip(this.btnOK, "应用当前表达式并关闭对话框");
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnCancel.Location = new System.Drawing.Point(640, 20);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.TipsFont = new System.Drawing.Font("微软雅黑", 9F);
            this.toolTip.SetToolTip(this.btnCancel, "取消编辑并关闭对话框");
            // 
            // ExpressionBuilderDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(890, 530);
            this.Controls.Add(this.grpButtons);
            this.Controls.Add(this.grpExpression);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExpressionBuilderDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "表达式构建器";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 890, 530);
            this.grpExpression.ResumeLayout(false);
            this.grpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UIGroupBox grpExpression;
        private UIGroupBox grpButtons;
        private UILabel lblExpression;
        private UITextBox txtExpression;
        private UILabel lblVariables;
        private UIListBox lstVariables;
        private UILabel lblFunctions;
        private UIListBox lstFunctions;
        private UILabel lblOperators;
        private UIListBox lstOperators;
        private UILabel lblPreview;
        private UIRichTextBox rtbPreview;
        private UILabel lblValidationResult;
        private UIButton btnValidate;
        private UISymbolButton btnOK;
        private UISymbolButton btnCancel;
        private System.Windows.Forms.ToolTip toolTip;
    }
}