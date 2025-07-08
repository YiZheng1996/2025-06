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
using TestTool.Parameters.ParSystemSetup;
using TestTool.Test;

namespace TestTool.Forms.SystemSetup
{
    public partial class Form_DelayTime : UIForm
    {
        public Form_DelayTime()
        {
            InitializeComponent();
            InitForm();
        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            WriteItem();
        }
        private void WriteItem()
        {
            Parameter_DelayTime par = new Parameter_DelayTime();
            par.t = double.Parse(uiTextBox1.Text);
            SingletonStatus singletonStatus = SingletonStatus.Instance;
            JsonDog.AddParameter(singletonStatus, par);
            //JsonDog.AddParameter(singletonStatus,); 
            //OpcDog.OpcConnect();
        }
        private void ShowItem()
        {
            Parameter_DelayTime par = new Parameter_DelayTime();
            SingletonStatus singletonStatus = SingletonStatus.Instance;
            //dynamic li = ((dynamic)obj).LictPLC;
            object obj = JsonDog.ReadChild(singletonStatus)[singletonStatus.stepNum].stepParameter;
            if(obj == null)
            {
                return;
            }
            par = JsonConvert.DeserializeObject<Parameter_DelayTime>(obj.ToString());
            uiTextBox1.Text = par.t.ToString();
        }
        private void InitForm()
        {
            ShowItem();
        }
    }
}
