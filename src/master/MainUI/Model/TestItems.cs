namespace MainUI.Model
{
    internal class TestItems
    {
        public string Model { get; set; }

        public string ProcessKey { get; set; }

        public string ProcessName { get; set; }

        public bool IsSelected { get; set; }

        public string DSLName { get; set; }

        public string ReportRow { get; set; }

        public override string ToString()
        {
            string str = "";
            str += "Model：" + Model + "；";
            str += "ProcessKey：" + ProcessKey + "；";
            str += "ProcessName：" + ProcessName + "；";
            str += "IsSelected：" + IsSelected.ToString() + "；";
            str += "DSLName：" + DSLName.ToString() + "；";
            str += "ReportRow：" + ReportRow.ToString() + "；";
            return str;
        }
    }
}