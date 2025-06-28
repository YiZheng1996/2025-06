using AntdUI;
namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    public partial class Form_WritePLC : UIForm
    {
        public Form_WritePLC()
        {
            InitializeComponent();
            InitForm();
            InitializePointLocationPLC();
        }

        private void InitForm()
        {
           
        }

        /// <summary>
        /// 加载全部PLC点位
        /// </summary>
        private void InitializePointLocationPLC()
        {
            try
            {
                TreeViewPLC.Nodes.Clear();
                foreach (var kvp in PointLocationPLC.Instance.DicModelsContent)
                {
                    // 创建主节点(Key)
                    TreeNode parentNode = new(kvp.Key);

                    // 添加子节点(Value)
                    foreach (var value in kvp.Value)
                    {
                        // 如果Key是"ServerName"，则不添加到TreeView中
                        if (value.Key != "ServerName")
                            parentNode.Nodes.Add(value.Key);
                    }
                    TreeViewPLC.Nodes.Add(parentNode);
                }
                // 默认全部展开
                TreeViewPLC.ExpandAll();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载全部PLC点位错误", ex);
                MessageHelper.MessageOK($"加载全部PLC点位错误：{ex.Message}", TType.Error);
            }
        }

        private void TreeViewPLC_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeViewPLC.DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        private void DataGridViewPLCList_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    var node = (TreeNode)e.Data.GetData(typeof(TreeNode));
                    if (node?.Parent != null)
                    {
                        DataGridViewPLCList.Rows.Add($"{node?.Parent.Text}.{node.Text}");
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("拖拽步骤错误", ex);
                MessageHelper.MessageOK($"拖拽步骤错误：{ex.Message}", TType.Error);
            }
        }

        private void DataGridViewPLCList_DragEnter_1(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(TreeNode)) ?
               DragDropEffects.Copy : DragDropEffects.None;
        }
    }
}
