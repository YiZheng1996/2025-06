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
using TestTool.Messages.Kepserverv6;
using TestTool.Test;

namespace TestTool.Forms.Calculator
{
    public partial class Form_VariableAssignment : UIForm
    {
        public Form_VariableAssignment()
        {
            InitializeComponent();
            InitForm();
        }
        private void ShowItem()
        {

        }
        private void uiTreeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            uiTreeView1.DoDragDrop(e.Item, DragDropEffects.Move);
        }
        private void Group_DragDrop(object sender, DragEventArgs e)
        {
            if (uiTreeView1.SelectedNode == null)
            {
                return;
            }
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
                uiDataGridView2.AddRow(draggedNode.Text, "", "","");


            }
        }

        private void Group_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void Clean_Click(object sender, EventArgs e)
        {
            if (uiDataGridView2.SelectedRows.Count > 0)
            {
                // 获取当前选中的第一行（通常用户一次只会选中一行进行删除操作）  
                //JsonDog.DeleteChild(uiTreeView1.SelectedNode.Parent.Text, uiTreeView1.SelectedNode.Text, uiDataGridView1.CurrentRow.Index);

                uiDataGridView2.Rows.Remove(uiDataGridView2.SelectedRows[0]);
                //RefreshWorks2Json();

            }
        }
        private void Save_Click(object sender, EventArgs e)
        {
            WriteItem();
        }

        private void WriteItem()
        {

        }
        private void InitForm()
        {
            ShowTreeview();
            ShowDatagridviewCellCombox();
        }
        private void Test_Click(object sender, EventArgs e)
        {

        }
        private void ShowTreeview()
        {
            string var_name;
            TreeNode rootNode;
            List<VarItem> items = new List<VarItem>();
            items = JsonDog.ReadVarItems();
            foreach (VarItem item in items)
            {
                var_name = item.varName;
                rootNode = new TreeNode(var_name);
                uiTreeView1.Nodes.Add(rootNode);
            }

        }
        private void ShowDatagridviewCellCombox()
        {
            List<KepServerItem> li = new List<KepServerItem>();
            li = JsonDog.ReadKepServerAddress();
            foreach (KepServerItem item in li)
            {
                dataGridViewTextBoxColumn2.Items.AddRange(item.kepName);
            }

        }
    }
}
