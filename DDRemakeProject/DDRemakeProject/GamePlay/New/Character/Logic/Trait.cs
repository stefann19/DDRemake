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

        public double Modifiers {
            get => _modifiers;
            set {
                if (value.Equals(_modifiers)) return;
                _modifiers = value;
                OnPropertyChanged();
            }
        }

        public double Value(int level) => @base + _growthPerLevel * level+Modifiers;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
