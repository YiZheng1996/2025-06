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
namespace TestTool.Forms
{
    public partial class TestProcess : UIForm
    {
        public TestProcess()
        {
            InitializeComponent();
        }
        public string ProcessName { get; set; }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            ProcessName = uiTextBox1.Text;
            //this.Close();
        }
    }
}
