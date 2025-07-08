namespace MainUI.Procedure.DSL.LogicalConfiguration.Parameter
{
    public class Parameter_SystemPrompt
    {
        public string Title { get; set; }           // 提示标题
        public string Message { get; set; }         // 提示内容
        //public MessageType MessageType { get; set; } // 提示类型
        public bool WaitForResponse { get; set; }   // 是否等待用户响应
    }

    public enum MessageType
    {
        Info,
        Warning,
        Error,
        Question
    }
}
