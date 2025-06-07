using FreeSql.DataAnnotations;

namespace MainUI.Model
{
    [Table(Name = "AboutDevice")]
    public class AboutDeviceModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 设备开机时间
        /// </summary>
        public DateTime OnTime { get; set; }

        /// <summary>
        /// 设备每天使用时长（小时）
        /// </summary>
        public int EveryDay { get; set; }

        /// <summary>
        /// 设备每月使用时长（小时）
        /// </summary>
        public int Monthly { get; set; }

        /// <summary>
        /// 设备使用总时间
        /// </summary>
        public DateTime UsageTime { get; set; }
    }
}
