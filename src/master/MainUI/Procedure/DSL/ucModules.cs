using MainUI.Devices;

namespace MainUI.Procedure.DSL
{
    public partial class ucModules : ucBaseManagerUI
    {
        private readonly ModuleConfig ModuleConfig = new() { ListJson = [] };
        public ucModules()
        {
            InitializeComponent();
            InitCboData();
        }

        private void InitModule(string TypeName)
        {
            try
            {
                ModuleConfig.Load();
                var value = ModuleConfig.ListJson;
                lstAllModule.Items.Clear();
                lstInjectModule.Items.Clear();
                foreach (var module in RWDSLHelper.ModuleDic)
                {
                    if (module.Value.TypeName.ToString() == TypeName)
                    {
                        var parameter = module.Value as BaseModuleNew;
                        if (!IsNameExist(value, parameter.ModuleName))
                        {
                            lstAllModule.Items.Add(parameter.ModuleName);
                        }
                        else
                        {
                            lstInjectModule.Items.Add(parameter.ModuleName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK("模块注入数据加载失败:" + ex.Message);
            }
        }

        private void InitCboData()
        {
            Array enumValues = Enum.GetValues(typeof(DeviceType));
            foreach (var item in enumValues)
            {
                cboCOMType.Items.Add(item);
            }
            cboCOMType.SelectedIndex = 0;
        }

        private bool IsNameExist(List<string> lstName, string Name)
        {
            foreach (var item in lstName)
            {
                if (item == Name)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            MoveTo(lstAllModule, lstInjectModule);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            MoveTo(lstInjectModule, lstAllModule);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (var item in lstInjectModule.Items)
            {
                ModuleConfig.ListJson.Add(item.ToString());
            }
            if (lstInjectModule.Items.Count <= 0) ModuleConfig.ListJson = [];
            ModuleConfig.Save();
            MessageHelper.MessageOK("保存成功！");
        }

        //private void btnClear_Click(object sender, EventArgs e)
        //{
        //    DialogResult dr = MessageHelper.MessageYes(Form, "确定要清空所有已配置的模块吗？");
        //    if (dr == DialogResult.OK)
        //    {
        //        lstInjectModule.Items.Clear();
        //        InitModule(cboCOMType.SelectedText);
        //        MessageBox.Show(this, "清空成功！", "系统提示");
        //    }
        //}

        private void cboCOMType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitModule(cboCOMType.SelectedText);
        }

        private void lstInjectModule_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                IsCreateFile();
                string LstName = lstInjectModule.SelectedItem.ToString();
                switch (RWDSLHelper.ModuleDic[LstName].TypeName)
                {
                    case DeviceType.Modbus:
                        FrmSetParameter frmSetParameter = new(LstName);
                        frmSetParameter.ShowDialog();
                        break;
                    case DeviceType.TCPIP:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK("创建模块参数错误：" + ex.Message);
            }
        }

        private static bool IsCreateFile()
        {
            string path = Application.StartupPath + "MyModules";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return true;
        }

    }
}
