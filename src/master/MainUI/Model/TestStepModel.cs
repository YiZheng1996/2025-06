using FreeSql.DataAnnotations;

namespace MainUI.Model
{
    [Table(Name = "TestStep")]
    public class TestStepModel
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        public int Step { get; set; }

        public string ProcessName { get; set; }

        public int ModelID { get; set; }

        public int TestProcessID { get; set; }
    }

    public class TestStepNewModel : TestStepModel
    {
        public string TestProcessName { get; set; }
        public bool IsVisible { get; set; }
    }
}
