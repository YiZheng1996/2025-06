namespace MainUI.Service
{
    /// <summary>
    /// 报表服务类，提供管理报告文件的功能，包括初始化、文件路径生成和保存测试记录
    /// </summary>
    /// <param name="reportPath">保存路径</param>
    public class ReportService(string reportPath)
    {
        private string _reportFilename;
        private string _reportFilePath;

        /// <summary>
        /// 初始化报告文件
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public string InitializeReportFile(ParaConfig config)
        {
            _reportFilename = config.RptFile;
            _reportFilePath = Path.GetFileNameWithoutExtension(_reportFilename);

            string rptPath = Path.Combine(Application.StartupPath, "reports", _reportFilePath);

            Directory.CreateDirectory(Path.GetDirectoryName(reportPath));
            File.Copy($"{rptPath}.xlsx", reportPath, true);

            return rptPath;
        }

        /// <summary>
        /// 构建保存文件路径
        /// </summary>
        /// <param name="modelName">型号名称</param>
        /// <returns></returns>
        public string BuildSaveFilePath(string modelName)
        {
            string rootPath = Path.Combine(Application.StartupPath, "Save");
            Directory.CreateDirectory(rootPath);

            string fileName = $"{modelName}_{DateTime.Now:yyyyMMddHHmmss}";
            return Path.Combine(rootPath, fileName);
        }

        /// <summary>
        /// 保存测试记录
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="record"></param>
        public void SaveTestRecord(string filePath, TestRecordModel record)
        {
            var recordBll = new TestRecordNewBLL();
            recordBll.SaveTestRecord(record);
        }
    }
}