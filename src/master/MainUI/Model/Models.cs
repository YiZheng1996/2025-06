using FreeSql.DataAnnotations;

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

        public string ModelType { get; set; }

        public string Mark { get; set; }
    }
    /// <summary>
    /// 产品型号表
    /// </summary>
    [Table(Name = "ModelsTable")]
    public class Models
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Mark { get; set; }
        public int TypeID { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class NewModels : Models
    {
        public int ModelTypeID { get; set; }
        public string ModelType { get; set; }
    }
}
