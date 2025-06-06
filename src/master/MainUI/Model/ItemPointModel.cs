using System.ComponentModel;

namespace MainUI.Model
{
    public class ItemPointModel : NotifyProperty
    {
        int _colorState;
        [DefaultValue(0)]
        public int ColorState
        {
            get => _colorState;
            set => _colorState = value;
        }

        string _itemname;
        public string ItemName
        {
            get => _itemname;
            set => _itemname = value;
        }

        bool _check;
        public bool Check
        {
            get => _check;
            set
            {
                if (_check == value) return;
                _check = value;
                OnPropertyChanged(nameof(Check));
            }
        }
    }

   
}
