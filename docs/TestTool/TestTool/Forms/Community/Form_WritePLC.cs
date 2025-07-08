using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Sunny.UI;
using TestTool.Config;
using TestTool.Messages.Kepserverv6;
using TestTool.Parameters.ListPar;
using TestTool.Parameters.ParCommunity;
using TestTool.Test;
namespace TestTool.Forms.Community
{
    public partial class Form_WritePLC : UIForm
    {
        public Form_WritePLC()
        {
            InitializeComponent();
            InitForm();

        }
        private void InitForm()
        {
            ShowTreeview();
            ShowDatagridviewCellCombox();
            ShowDatagridview();

        }
        private void ShowTreeview()
        {
            List<KepServerItem> li = new List<KepServerItem>();
            string plc_name;
            TreeNode rootNode;
            li = JsonDog.ReadKepServerAddress();
            foreach (KepServerItem item in li)
            {
                plc_name = item.kepName;
                rootNode = new TreeNode(plc_name);
                uiTreeView1.Nodes.Add(rootNode);
            }

        }
        private List<string> ReadVar()
        {
            List<string> li = new List<string>();
            List<VarItem> items = new List<VarItem>();
            items = JsonDog.ReadVarItems();
            foreach (VarItem item in items)
            {
                li.Add(item.varName);
            }
            return li;
        }
        private void ShowItem()
        {
            ListParameter_WritePLC li = new ListParameter_WritePLC();
            SingletonStatus singletonStatus = SingletonStatus.Instance;
            //dynamic li = ((dynamic)obj).LictPLC;
            object par = JsonDog.ReadChild(singletonStatus)[singletonStatus.stepNum].stepParameter;
            if (par == null)
            {
                return;
            }
            li = JsonConvert.DeserializeObject<ListParameter_WritePLC>(par.ToString());
            foreach (var item in li.LictPLC)
            {
                uiDataGridView2.AddRow(item.plcName, item.plcValue, item.varName);
            }


        }
        private void ShowDatagridview()
        {
            ShowItem();

        }
        private void ShowDatagridviewCellCombox()
        {
            List<string> li = ReadVar();
            //dataGridViewTextBoxColumn2.Items.AddRange("0");
            //dataGridViewTextBoxColumn2.Items.AddRange("1");
            foreach (string item in li)
            {
                dataGridViewTextBoxColumn2.Items.AddRange(item);
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
            ListParameter_WritePLC li = new ListParameter_WritePLC();
            Parameter_WritePLC parameter_WritePLC;
            for (int i = 0; i < uiDataGridView2.Rows.Count; i++)
            {
                if (!uiDataGridView2.Rows[i].IsNewRow)
                {
                    parameter_WritePLC = new Parameter_WritePLC();
                    parameter_WritePLC.plcName = uiDataGridView2.Rows[i].Cells[0].Value.ToString();
                    parameter_WritePLC.plcValue = uiDataGridView2.Rows[i].Cells[1].Value.ToString();
                    parameter_WritePLC.varName = uiDataGridView2.Rows[i].Cells[2].Value.ToString();
                    li.LictPLC.Add(parameter_WritePLC);
                }
            }
            SingletonStatus singletonStatus = SingletonStatus.Instance;
            JsonDog.AddParameter(singletonStatus,li);
            //JsonDog.AddParameter(singletonStatus,); 
            //OpcDog.OpcConnect();
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
                uiDataGridView2.AddRow(draggedNode.Text, "", "");


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

        private void Test_Click(object sender, EventArgs e)
        {
            ListParameter_WritePLC li = new ListParameter_WritePLC();
            Parameter_WritePLC parameter_WritePLC;
            for (int i = 0; i < uiDataGridView2.Rows.Count; i++)
            {
                if (!uiDataGridView2.Rows[i].IsNewRow)
                {
                    parameter_WritePLC = new Parameter_WritePLC();
                    parameter_WritePLC.plcName = uiDataGridView2.Rows[i].Cells[0].Value.ToString();
                    parameter_WritePLC.plcValue = uiDataGridView2.Rows[i].Cells[1].Value.ToString();
                    parameter_WritePLC.varName = uiDataGridView2.Rows[i].Cells[2].Value.ToString();
                    li.LictPLC.Add(parameter_WritePLC);
                }
            }
            MethodCollection.Method_WritePLC(li);
        }

    }
}
