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

namespace TestTool.Forms
{
    public partial class SetPLC : UIForm
    {
        public SetPLC()
        {
            InitializeComponent();
            InitShow();
        }
        private void InitShow()
        {
            List<KepServerItem> li = new List<KepServerItem>();
            string channelName = JsonDog.ReadKepServerChannelName();
            string deviceName = JsonDog.ReadKepServerDeviceName();
            string groupName = JsonDog.ReadKepServerGroupName();
            li = JsonDog.ReadKepServerAddress();
            uiTextBox1.Text = channelName;
            uiTextBox2.Text = deviceName;
            uiTextBox3.Text = groupName;
            uiDataGridView1.Rows.Clear();
            if (li.Count == 0)
            {
                return;
            }
            foreach (KepServerItem item in li)
            {
                uiDataGridView1.AddRow(item.kepName, item.kepType, item.kepAddr);
            }
        }
        private void Run_Click(object sender, EventArgs e)
        {
            WriteOpc2Json();
        }
        private void WriteOpc2Json()
        {
            KepServerItem item;
            JsonDog.WriteKepServerChannelName(uiTextBox1.Text);
            JsonDog.WriteKepServerDeviceName(uiTextBox2.Text);
            JsonDog.WriteKepServerGroupName(uiTextBox3.Text);
            JsonDog.ReferishKepServerAddress();
            for (int i = 0; i < uiDataGridView1.Rows.Count; i++)
            {
                if (!uiDataGridView1.Rows[i].IsNewRow) // Skip the new row template if it exists
                {
                    item = new KepServerItem();
                    item.kepName = uiDataGridView1.Rows[i].Cells[0].Value.ToString();
                    item.kepType = uiDataGridView1.Rows[i].Cells[1].Value.ToString();
                    item.kepAddr = uiDataGridView1.Rows[i].Cells[2].Value.ToString();
                    JsonDog.WriteKepServerAddress(item);
                }

            }

        }
        private void SetOpcConnect()
        {
            List<string> li = new List<string>();
            string str0 = uiTextBox1.Text + "." + uiTextBox2.Text + "." + uiTextBox3.Text + ".";
            string str = "";
            for (int i = 0; i < uiDataGridView1.Rows.Count; i++)
            {
                if (!uiDataGridView1.Rows[i].IsNewRow) // Skip the new row template if it exists
                {
                    str = str0 + uiDataGridView1.Rows[i].Cells[2].Value;
                    li.Add(str);
                }

            }
            if (li.Count > 0)
            {
                //OpcDog.OpcConnect(li);

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
    }
}
