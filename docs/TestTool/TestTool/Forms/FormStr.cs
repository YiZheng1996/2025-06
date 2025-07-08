namespace TestTool.Forms
{
    internal class FormStr
    {
        public FormStr(string text, string name, string method, string parameter)
        {
            formText = text;
            formName = name;
            formMethod = method;
            formParameter = parameter;
        }
        public string formText { get; set; }
        public string formName { get; set; }
        public string formMethod { get; set; }
        public string formParameter { get; set; }

    }
}
