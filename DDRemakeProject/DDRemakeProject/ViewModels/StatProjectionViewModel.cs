using System.ComponentModel;
using System.Runtime.CompilerServices;
using DDRemakeProject.Annotations;

namespace DDRemakeProject.ViewModels
{
    public class StatProjectionViewModel :INotifyPropertyChanged 
    {
        private string _value;
        private string _name;

        public StatProjectionViewModel()
        {
        }

        public StatProjectionViewModel(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name {
            get => _name;
            set {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Value {
            get => _value;
            set {
                if (value == _value) return;
                _value = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}