using System.ComponentModel;
using System.Runtime.CompilerServices;
using DDRemakeProject.Annotations;

namespace DDRemakeProject.GamePlay.New.Character.Logic
{
    public class Trait :INotifyPropertyChanged
    {
        public Trait(double value,double growthPerLevel)
        {
            @base = value;
            _growthPerLevel = growthPerLevel;
            Modifiers = 0;
        }

        
        private double @base;
        private double _growthPerLevel;
        private double _modifiers;
        private int _level;

        public int Level
        {
            get { return _level; }
            set
            {
                if (value == _level) return;
                _level = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Value));
            }
        }

        public double Modifiers {
            get => _modifiers;
            set {
                if (value.Equals(_modifiers)) return;
                _modifiers = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Value));
            }
        }

        public double Value => @base + (_growthPerLevel * Level) + _modifiers;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
