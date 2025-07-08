using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using TestTool.Config;
using TestTool.Test;

namespace TestTool.Forms
{
    public partial class User : UIForm
    {
        //private static string filePath;
        private SingletonStatus singletonStatus;
        public User()
        {
            InitializeComponent();
            singletonStatus = SingletonStatus.Instance;
            singletonStatus.cycle = false;
            singletonStatus.MyStaticPropertyChanged += MyStaticClass_MyStaticPropertyChanged;
            
        }
        private void InitMem()
        {
            //初始化变量
            List<VarItem> var_items = new List<VarItem>();
            var_items = JsonDog.ReadVarItems();
            singletonStatus.obj = new List<object>();
            if(var_items.Count > 0)
            {
                foreach(var item in var_items)
                {
                    singletonStatus.obj.Add(0);
                }
            }
        }

        /// <summary>
        /// 根据节点查找步骤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (uiTreeView1.SelectedNode.Parent != null)
            {
                singletonStatus.childName = uiTreeView1.SelectedNode.Text;
                singletonStatus.parentName = uiTreeView1.SelectedNode.Parent.Text;
                //string nodeName = uiTreeView1.SelectedNode.Text;
                List<Child> children = new List<Child>();
                children = JsonDog.ReadChild(singletonStatus);
                uiDataGridView1.Rows.Clear();
                if (children != null)
                {
                    // 假设你有一个名为dataGridView1的DataGridView控件  
                    foreach (Child child in children)
                    {
                        uiDataGridView1.AddRow(child.stepName, "", "");
                    }
                }
            }
        }

        private void uiTreeView2_ItemDrag(object sender, ItemDragEventArgs e)
        {
            uiTreeView2.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void AddChild2Json(string childName,int step)
        {
            Child child = new Child();
            child.stepName = childName;
            //child.stepNum = GetStepNum(childName);
            child.status = 1;
            child.stepNum = step;
            JsonDog.AddChild(singletonStatus, child);
            //JsonWrite.WriteChild(uiTreeView1.SelectedNode.Parent.Text, uiTreeView1.SelectedNode.Text, child);
        }

        private void Group_DragDrop(object sender, DragEventArgs e)
        {
            if (uiTreeView1.SelectedNode == null)
            {
                return;
            }
            if (uiTreeView1.SelectedNode.Parent != null)
            {
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

                    // 在Panel中添加Label控件来显示节点文本  
                    if (draggedNode.Parent != null)
                    {
                        uiDataGridView1.AddRow(draggedNode.Text, "", "");
                        AddChild2Json(draggedNode.Text,uiDataGridView1.Rows.Count);
                        //AddWorks2Json(2, draggedNode.Text);
                        //RefreshWorks2Json();
                    }


                }
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

        private void Group_DragOver(object sender, DragEventArgs e)
        {

        }

        private void GetParameter_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = uiDataGridView1.SelectedRows[0];
            string frmName = selectedRow.Cells[0].Value.ToString();
            singletonStatus.stepNum = selectedRow.Index;
            singletonStatus.stepName = frmName;
            FormSet.OpenFormByName(frmName);
        }

        private void DeleteStep_Click(object sender, EventArgs e)
        {
            if (uiDataGridView1.SelectedRows.Count > 0)
            {
                // 获取当前选中的第一行（通常用户一次只会选中一行进行删除操作）  
                JsonDog.DeleteChild(singletonStatus, uiDataGridView1.CurrentRow.Index);
                DataGridViewRow selectedRow = uiDataGridView1.SelectedRows[0];
                uiDataGridView1.Rows.Remove(selectedRow);
                //RefreshWorks2Json();

            }
        }

        private void CreateNode_Click(object sender, EventArgs e)
        {
            if (uiTreeView1.SelectedNode == null)
            {
                return;
            }
            if (uiTreeView1.SelectedNode.Parent == null)
            {
                TestProcess tp = new TestProcess();
                tp.ShowDialog();
                string nodeName = tp.ProcessName;
                if (!nodeName.IsNullOrEmpty())
                {
                    singletonStatus.childName = nodeName;
                    singletonStatus.parentName = uiTreeView1.SelectedNode.Text;
                    uiTreeView1.SelectedNode.Nodes.Add(nodeName);
                    //JsonWrite.WriteBasic(uiTreeView1.SelectedNode.Text, nodeName);
                    JsonDog.AddParent(singletonStatus);
                }
            }
        }

        private void DeleteNode_Click(object sender, EventArgs e)
        {
            if (uiTreeView1.SelectedNode.Parent != null)
            {
                JsonDog.DeleteParent(singletonStatus);
                uiTreeView1.SelectedNode.Parent.Nodes.Remove(uiTreeView1.SelectedNode);

            }
        }

        private void OpenConfigFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择一个 .json 文件",
                Filter = "二进制文件 (*.json)|*.json|所有文件 (*.*)|*.*",
                DefaultExt = "json",
                CheckFileExists = true,
                CheckPathExists = true
            };

            // 显示对话框并检查用户是否选择了一个文件
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                JsonDog.filePath = openFileDialog.FileName;
                ShowParent();
            }
        }
        private void User_Load(object sender, EventArgs e)
        {
            
        }

        private void SelectConfig_Click(object sender, EventArgs e)
        {
            // 创建 OpenFileDialog 实例
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择一个 .json 文件",
                Filter = "二进制文件 (*.json)|*.json|所有文件 (*.*)|*.*",
                DefaultExt = "json",
                CheckFileExists = true,
                CheckPathExists = true
            };

            // 显示对话框并检查用户是否选择了一个文件
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                JsonDog.filePath = openFileDialog.FileName;
                ShowParent();
                InitMem();
            }
        }
        private void ShowParent()
        {
            //显示主流程
            List<Parent> li0 = new List<Parent>();
            li0 = JsonDog.ReadParent();
            if (li0.Count > 0)
            {
                foreach (Parent c in li0)
                {
                    foreach (TreeNode tn in uiTreeView1.Nodes)
                    {
                        if (tn.Text == c.parentName)
                        {
                            tn.Nodes.Add(c.childName);
                        }
                    }
                }
            }
            uiTreeView1.ExpandAll();
        }

        private void uiSymbolButton6_Click(object sender, EventArgs e)
        {

        }

        private void uiSymbolButton7_Click(object sender, EventArgs e)
        {
            SetPLC f = new SetPLC();
            f.ShowDialog();
        }

        private void MyStaticClass_MyStaticPropertyChanged(int newValue)
        {
            if (!singletonStatus.cycle)
            {
                return;
            }
            // 更新界面，例如设置TextBox的Text属性
            this.Invoke(new Action(() =>
            {
                //myTextBox.Text = newValue;
                //uiRichTextBox1.Text = uiRichTextBox1.Text + Local.stepNum + "\n";
                if (singletonStatus.stepNum > 0)
                {
                    if (singletonStatus.status)
                    {
                        uiDataGridView1.Rows[singletonStatus.stepNum-1].Cells[1].Value = "✔";
                    }
                    else
                    {
                        uiDataGridView1.Rows[singletonStatus.stepNum-1].Cells[1].Value = "×";
                    }
                    //uiDataGridView1.Rows[Local.stepNum - 1].Cells[1].Value = Local.status;
                    uiDataGridView1.Rows[singletonStatus.stepNum-1].Cells[2].Value = singletonStatus.t;
                    

                }
            }));
        }

        private void Run_Click(object sender, EventArgs e)
        {
            Works.WorksEnQueue(singletonStatus.childName);
            Thread workerThread = new Thread(Works.WorkerMethod);
            workerThread.Start();
            // 等待线程完成
            //workerThread.Join();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
