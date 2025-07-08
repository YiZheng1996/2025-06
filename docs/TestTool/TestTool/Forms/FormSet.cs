using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestTool.Config;
using TestTool.Test;

namespace TestTool.Forms
{
    internal class FormSet
    {
        public static void OpenFormByName(string formName)
        {
            // 获取当前程序集
            Assembly assembly = Assembly.GetExecutingAssembly();
            string frm = GetFormName(formName);
            if(frm == null)
            {
                return;
            }
            // 查找指定名称的窗体类型
            Type formType = assembly.GetTypes()
                .FirstOrDefault(t => t.IsSubclassOf(typeof(Form)) && t.Name == frm);

            if (formType != null)
            {
                // 创建窗体的实例
                Form newForm = (Form)Activator.CreateInstance(formType);

                // 显示窗体
                newForm.ShowDialog();
            }
            else
            {
                MessageBox.Show($"未找到名称为 {formName} 的窗体。");
            }
        }
        private static string GetFormName(string formName)
        {

            List<FormStr> li = new List<FormStr>();
            li = FormInfo.readOnlyList.ToList();
            foreach (FormStr formStr in li)
            {
                if(formStr.formText == formName)
                {
                    return formStr.formName;
                }
            }
            return null;
        }
    }
}
