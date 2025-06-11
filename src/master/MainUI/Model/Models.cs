using AntdUI;
using FreeSql.DataAnnotations;
using System.ComponentModel;

namespace MainUI.Model
{
    /// <summary>
    /// 模型类型表
    /// </summary>
    [Table(Name = "ModelTypeTable")]
    public class ModelsType
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string ModelTypeName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Mark { get; set; }
    }
    /// <summary>
    /// 产品型号表
    /// </summary>
    [Table(Name = "ModelsTable")]
    public class Models : NotifyProperty
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 型号名称
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 图号
        /// </summary>
        public string DrawingNo { get; set; }

        /// <summary>
        /// 是否是发布
        /// </summary>
        [DefaultValue(0)]
        public bool IsRelease { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [DefaultValue("未发布")]
        public string ReleaseTime { get; set; }

        CellLink[] _Buttns =
         [new CellButton("Release", "发布", TTypeMini.Primary)
            {
                BorderWidth = 1,
                Fore = Color.FromArgb(236, 236, 236),
                Back = Color.FromArgb(90, 124, 236),
                BackHover = Color.FromArgb(90, 124, 236),
            },
          ];

        [Column(IsIgnore = true)]
        public CellLink[] Buttns
        {
            get => _Buttns;
            set
            {
                _Buttns = value;
                OnPropertyChanged(nameof(Buttns));
            }
        }
    }

    public class NewModels : Models
    {
        public int ModelTypeID { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string ModelTypeName { get; set; }
    }
}
