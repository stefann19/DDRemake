using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using DDRemakeProject.Annotations;
using DDRemakeProject.Commands;
using DDRemakeProject.GamePlay.New.Character;

namespace DDRemakeProject.ViewModels
{
    public class LevelSelectionViewModel : DependencyObject,INotifyPropertyChanged
    {
        private string _value;

        public static readonly DependencyProperty LevelValueProperty = DependencyProperty.Register("LevelValue", typeof(string), typeof(LevelSelectionViewModel), new PropertyMetadata(default(string)));
/*
        public static readonly DependencyProperty ValProperty = DependencyProperty.Register("Val", typeof(object), typeof(LevelSelectionViewModel), new PropertyMetadata(default(object)));
*/

        /*public string Value {
            get => _value;
            set {
                if (value == _value) return;
                _value = value;
                OnPropertyChanged();
            }
        }*/
        public ICommand LowerValueCommand { get; set; }

        public LevelSelectionViewModel()
        {
/*
            LevelValue = 1.ToString();
*/
            /*LowerValueCommand = new RelayCommand(param =>
            {
                int val = int.Parse((string)((object[])param)[0] );
                LevelValue = (val - 1).ToString();
            }, param =>
            {
                if (param == null) return false;

                string s = (string)((object[])param)[0];
                if (string.IsNullOrEmpty(s)) return true;
                int val = int.Parse(s);
                return val > 1;
            });
            RaiseValueCommand = new RelayCommand(param =>
            {
                int val = int.Parse((string)((object[])param)[0]);
                LevelValue = (val + 1).ToString();
            }, param =>
            {
                if (param == null) return false;
                string s = (string)((object[])param)[0];
                if (string.IsNullOrEmpty(s)) return true;
                int val = int.Parse(s);
                return val < 20;
            });*/

            LowerValueCommand = new RelayCommand(param =>
            {
/*
                Character ch = param as Character;
*/
                Character.CharacterLogic.Level.CurrentLevel--;
                OnPropertyChanged(nameof(Character));
            }, param =>
            {
/*
                Character ch = param as Character;
*/
                return Character?.CharacterLogic.Level.CurrentLevel > 1;
            });

            RaiseValueCommand = new RelayCommand(param =>
            {
/*
                Character ch = param as Character;
*/
                Character.CharacterLogic.Level.CurrentLevel++;
                OnPropertyChanged(nameof(Character));
            }, param =>
            {
/*
                Character ch = param as Character;
*/
                return Character?.CharacterLogic.Level.CurrentLevel < 20;
            });
        }

        public LevelSelectionViewModel(Character character):this()
        {
            /*
                        LevelValue = Value;
            */
            Character = character;
        }

        public ICommand RaiseValueCommand { get; set; }

        public Character Character { get; set; }

        public string LevelValue {
            get { return (string) GetValue(LevelValueProperty); }
            set { SetValue(LevelValueProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}