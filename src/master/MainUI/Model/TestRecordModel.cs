using FreeSql.DataAnnotations;

namespace MainUI.Model
{
    [Table(Name = "Record")]
    public class TestRecordModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 类型名称ID
        /// </summary>
        public int KindID { get; set; }

        /// <summary>
        /// 型号名称ID
        /// </summary>
        public int ModelID { get; set; }

        /// <summary>
        /// ？？？
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// 车型车号编号
        /// </summary>
        public string TestID { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Tester { get; set; }

        /// <summary>
        /// 保存时间
        /// </summary>
        public DateTime TestTime { get; set; }

        /// <summary>
        /// 保存报告路径
        /// </summary>
        public string ReportPath { get; set; }
    }

    public class TestRecordModelDto : TestRecordModel
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string ModelTypeName { get; set; }

        /// <summary>
        /// 型号名称
        /// </summary>
        public string ModelName { get; set; }
    }
}
