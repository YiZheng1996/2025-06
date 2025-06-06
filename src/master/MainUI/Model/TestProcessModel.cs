using AntdUI;
using FreeSql.DataAnnotations;

namespace MainUI.Model
{
    [Table(Name = "TestProcess")]
    internal class TestProcessModel : NotifyProperty
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        public string ProcessName { get; set; }

        public bool IsVisible { get; set; }

        CellLink[] _Buttns =
          [new CellButton("Save", "保存", TTypeMini.Success)
            {
                BorderWidth = 1,
                //Fore = Color.FromArgb(236, 236, 236),
                //Back = Color.FromArgb(90, 124, 236),
                //BackHover = Color.FromArgb(90, 124, 236),
            },
           new CellButton("Delete", "删除", TTypeMini.Error)
            {
                BorderWidth = 1,
                //Fore = Color.FromArgb(235, 227, 221),
                //Back = Color.FromArgb(70, 75, 85),
                //BackHover = Color.FromArgb(70, 75, 85)
             }
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

        private bool _enable;
        [Column(IsIgnore = true)]
        public bool Enable
        {
            get => _enable;
            set
            {
                if (_enable == value) return;
                _enable = value;
                OnPropertyChanged(nameof(Enable));
            }
        }

        bool _Check = false;
        [Column(IsIgnore = true)]
        public bool Check
        {
            get => _Check;
            set
            {
                if (_Check == value) return;
                _Check = value;
                OnPropertyChanged(nameof(Check));
            }
        }
    }
}
