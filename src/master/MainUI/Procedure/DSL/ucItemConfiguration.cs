using MainUI.Procedure.DSL.LogicalConfiguration.Forms;
using MainUI.Procedure.DSL.LogicalConfiguration.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MainUI.Procedure.DSL
{
    public partial class ucItemConfiguration : ucBaseManagerUI
    {
        ModelTypeBLL ModelBLL = new();
        TestStepBLL StepBLL = new();
        TestProcessBLL TestProcessBLL = new();
        public ucItemConfiguration()
        {
            InitializeComponent();
            LoadCboModels();
            cboModel.SelectedIndexChanged += CboModel_SelectedIndexChanged;
        }

        private void CboModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProcess();
            LoadConfiguaredProcess();
        }

        void LoadCboModels()
        {
            cboType.DisplayMember = "ModelTypeName";
            cboType.ValueMember = "ID";
            cboType.DataSource = ModelBLL.GetModels();
            LoadProcess();
            LoadConfiguaredProcess();
        }

        List<TestProcessModel> lstTestProcess = [];
        void LoadProcess()
        {
            lstAllPoint.Items.Clear();
            lstTestProcess = TestProcessBLL.GetTestProcess(true);
            foreach (var item in lstTestProcess)
            {
                lstAllPoint.Items.Add(item.ProcessName);
            }
        }
        private void LoadConfiguaredProcess()
        {
            try
            {
                lstTestPoint.Items.Clear();
                List<TestStepModel> lstTestStep = StepBLL.GetTestSteps(new TestStep { ModelID = (int)cboModel?.SelectedValue });
                foreach (TestStepModel step in lstTestStep)
                {
                    foreach (var p in lstTestProcess)
                    {
                        if (step.ProcessName == p.ProcessName)
                        {
                            lstTestPoint.Items.Add(p.ProcessName);
                            lstAllPoint.Items.Remove(p.ProcessName);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"加载项点名称错误：{ex.Message}");
            }
        }

        private void MoveTo(UIListBox from, UIListBox to)
        {
            for (int i = 0; i < from.SelectedItems.Count; i++)
            {
                to.Items.Add(from.SelectedItems[i]);
            }
            to.ClearSelected();
            to.SelectedIndex = to.Items.Count - 1;
            int beforeIndex = -1;
            while (from.SelectedItems.Count > 0)
            {
                beforeIndex = from.SelectedIndex;
                from.Items.Remove(from.SelectedItems[0]);
            }

            if (from.Items.Count == beforeIndex)
                from.SelectedIndex = beforeIndex - 1;
            else
                from.SelectedIndex = beforeIndex;
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            MoveTo(lstAllPoint, lstTestPoint);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            MoveTo(lstTestPoint, lstAllPoint);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<TestStepModel> lstTestStep = [];
                for (int i = 0; i < lstTestPoint.Count; i++)
                {
                    TestStepModel testStep = new();
                    {
                        testStep.Step = i;
                        testStep.ModelID = (int)cboModel.SelectedValue;
                        testStep.ProcessName = $"{lstTestPoint.Items[i]}";
                        testStep.TestProcessID = lstTestProcess.Find(x => x.ProcessName == testStep.ProcessName).ID;
                    }
                    lstTestStep.Add(testStep);
                }
                StepBLL.InsertTestStep(lstTestStep, (int)cboModel.SelectedValue);
                LoadConfiguaredProcess();
                MessageHelper.MessageOK("保存成功！");
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"保存错误：{ex.Message}");
            }
        }

        private void lstTestPoint_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EnditTest(sender as UIListBox);
        }

        /// <summary>
        /// 修改对应项点自动试验逻辑
        /// </summary>
        /// <param name="lstbox"></param>
        private void EnditTest(UIListBox lstbox)
        {
            try
            {
                if (cboModel.Items.Count > 0 & lstbox.Items.Count > 0)
                {
                    TestProcessBLL bll = new();
                    string ModelType = cboType.SelectedText;
                    string ModelName = cboModel.SelectedText;
                    string LstName = lstbox.SelectedItem.ToString();
                    string LstIndex = lstbox.SelectedIndex.ToString();
                    string TestPath = $"{Application.StartupPath}Procedure\\{ModelType}\\{ModelName}\\{LstName}.json";
                    Debug.WriteLine($"选择型号：{ModelName},选择下标：{LstIndex},选择项点：{LstName}，路径：{TestPath}");

                    var formFactory = Program.ServiceProvider.GetRequiredService<FormFactory>();
                    var form = formFactory.CreateLogicalConfigurationForm(TestPath, ModelType, ModelName, LstName);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageHelper.MessageOK($"获取自动试验逻辑失败：{err}");
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModelTypeBLL bModelType = new();
            cboModel.ValueMember = "ID";
            cboModel.DisplayMember = "ModelName";
            cboModel.DataSource = bModelType.GetNewModels(cboType.SelectedValue.ToInt32());
        }

    }
}
