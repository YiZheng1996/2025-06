using FreeSql.DataAnnotations;

namespace MainUI.Model
{
    [Table(Name = "LoggerInfo")]
    public class NlogsModel
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; } 

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime MessTime { get; set; }

        /// <summary>
        /// 错误等级
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 标题信息
        /// </summary>
        public string MessageName { get; set; }

        /// <summary>
        /// 错误来源
        /// </summary>
        public string Source { get; set; }

    }
}
