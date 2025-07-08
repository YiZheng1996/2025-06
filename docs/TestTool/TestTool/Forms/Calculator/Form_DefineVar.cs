using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using TestTool.Config;
using TestTool.Test;

namespace TestTool.Forms.Calculator
{
    public partial class Form_DefineVar : UIForm
    {
        public Form_DefineVar()
        {
            InitializeComponent();
            InitForm();
        }
        private void InitForm()
        {
            ShowItem();
        }
        private void ShowItem()
        {
            List<VarItem> li = new List<VarItem>();

            li = JsonDog.ReadVarItems();
            uiDataGridView1.Rows.Clear();
            if (li.Count == 0)
            {
                return;
            }
            foreach (VarItem item in li)
            {
                uiDataGridView1.AddRow(item.varName, item.varType, item.varText);
            }
        }
        private void Clean_Click(object sender, EventArgs e)
        {
            if (uiDataGridView1.SelectedRows.Count > 0)
            {
                // 获取当前选中的第一行（通常用户一次只会选中一行进行删除操作）  
                //JsonDog.DeleteChild(uiTreeView1.SelectedNode.Parent.Text, uiTreeView1.SelectedNode.Text, uiDataGridView1.CurrentRow.Index);

                uiDataGridView1.Rows.Remove(uiDataGridView1.SelectedRows[0]);
                //RefreshWorks2Json();

            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            WriteItem();
        }
        private void WriteItem()
        {
            VarItem item;
            JsonDog.ReferishVarItems();
            for (int i = 0; i < uiDataGridView1.Rows.Count; i++)
            {
                if (!uiDataGridView1.Rows[i].IsNewRow) // Skip the new row template if it exists
                {
                    item = new VarItem();
                    item.varName = uiDataGridView1.Rows[i].Cells[0].Value.ToString();
                    item.varType = uiDataGridView1.Rows[i].Cells[1].Value.ToString();
                    item.varText = uiDataGridView1.Rows[i].Cells[2].Value.ToString();
                    JsonDog.WriteVarItems(item);
                }

            }

        }
    }
}
