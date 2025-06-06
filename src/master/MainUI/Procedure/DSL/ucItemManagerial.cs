using AntdUI;
using MainUI.CurrencyHelper;

namespace MainUI.Procedure.DSL
{
    public partial class ucItemManagerial : ucBaseManagerUI
    {
        int RowCount = 0;
        TestProcessBLL ProcessBll = new();
        public ucItemManagerial()
        {
            InitializeComponent();
            InitTableTitle();
        }

        private void InitTableTitle()
        {
            TableTestProcess.Columns = [
                new ColumnCheck("Check"),
                new Column("ID","ID"){ Align = ColumnAlign.Center,Visible=false},
                new Column("ProcessName","项点名称"){ Align = ColumnAlign.Center,Width="390"},
                new ColumnSwitch("Enable","启用",ColumnAlign.Center){ Call = (value , record , i_row , i_col)=>
                {
                    Thread.Sleep(200);
                    return value;
                 }},
                new Column("Buttns","操作",ColumnAlign.Center){ Width = "150"}
            ];
            var data = ProcessBll.GetTestProcess();
            RowCount = data.Count;
            data.ForEach(x => x.Enable = x.IsVisible);
            TableTestProcess.DataSource = data;
            TableTestProcessIndex(TableTestProcess.SelectedIndex);
        }

        private void TableTestProcess_CellButtonClick(object sender, TableButtonEventArgs e)
        {
            try
            {
                if (e.Record is TestProcessModel data)
                {
                    switch (e.Btn.Id)
                    {
                        case "Save":
                            if (ProcessBll.SaveTestProcess(data))
                            {
                                InitTableTitle();
                                MessageHelper.MessageOK($"保存成功！");
                            }
                            break;
                        case "Delete":
                            if (MessageHelper.MessageYes($"删除后将无法恢复！确定要永久删除「{data.ProcessName}」吗？") == DialogResult.OK)
                            {
                                if (ProcessBll.DelTestProcess(data.ID))
                                {
                                    InitTableTitle();
                                    MessageHelper.MessageOK($"删除成功！");
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK("保存删除错误：" + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProcessBll.AddTestProcess())
                {
                    InitTableTitle();
                    TableTestProcessIndex(RowCount);
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK("添加错误：" + ex.Message);
            }
        }

        void TableTestProcessIndex(int index)
        {
            if (index > RowCount) index -= 1;
            if (index < 0) index = RowCount;
            TableTestProcess.ScrollLine(index);
            TableTestProcess.SelectedIndex = index;
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                var checkedItems = ((IEnumerable<TestProcessModel>)TableTestProcess.DataSource)
                    .Where(item => item.Check).ToList();
                if (checkedItems.Count == 0)
                {
                    MessageHelper.MessageOK("未选择任何数据！");
                    return;
                }

                foreach (var item in checkedItems)
                {
                    ProcessBll.DelTestProcess(item.ID);
                }
                InitTableTitle();
                MessageHelper.MessageOK("批量删除成功！");
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK("批量删除错误：" + ex.Message);
            }
        }

        private void TableTestProcess_CellClick(object sender, TableClickEventArgs e)
        {
            TableTestProcess.EditModeClose();
        }
    }
}
