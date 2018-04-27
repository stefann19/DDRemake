using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DDRemakeProject.Annotations;
using DDRemakeProject.Commands;

namespace DDRemakeProject.ViewModels
{
    public class StatsSelectionViewModel :INotifyPropertyChanged
    {
        private string _strength;
        private string _agility;
        private string _endurance;
        private string _intelligence;

        public StatsSelectionViewModel()
        {

            Strength = 10.ToString();
            Agility = 10.ToString();
            Intelligence = 10.ToString();
            Endurance = 10.ToString();

            RaiseValueCommand = new RelayCommand(o =>ModifyValue(o,1) ,o=> CanModifyValue(o,1));
            LowerValueCommand = new RelayCommand(o => ModifyValue(o, -1), o => CanModifyValue(o, -1));

        }

        private bool CanModifyValue(object o,int changeValue)
        {
            if (o == null) return false;
            object[] obj = (object[]) o;
            int value = int.Parse(obj[0] as string ?? "0");
            string target = obj[1] as string;
            // wip
            bool valueBounds = value+ changeValue >= 1 && value+ changeValue<=20;
            return valueBounds;
        }
        private void ModifyValue(object o, int changeValue)
        {
            object[] obj = (object[])o;
            int value = int.Parse(obj[0] as string ?? "0");
            string target = obj[1] as string;

            switch (target)
            {
                case "Strength": Strength = (value + changeValue).ToString();
                    break;
                case "Agility":
                    Agility = (value + changeValue).ToString();
                    break;
                case "Intelligence":
                    Intelligence = (value + changeValue).ToString();
                    break;
                case "Endurance":
                    Endurance = (value + changeValue).ToString();
                    break;
                default:
                    break;
            }
        }


        public string Strength {
            get => _strength;
            set {
                if (value == _strength) return;
                _strength = value;
                OnPropertyChanged();
            }
        }

        public string Agility {
            get => _agility;
            set {
                if (value == _agility) return;
                _agility = value;
                OnPropertyChanged();
            }
        }

        public string Endurance {
            get => _endurance;
            set {
                if (value == _endurance) return;
                _endurance = value;
                OnPropertyChanged();
            }
        }

        public string Intelligence {
            get => _intelligence;
            set {
                if (value == _intelligence) return;
                _intelligence = value;
                OnPropertyChanged();
            }
        }

        public ICommand RaiseValueCommand { get; set; }
        public ICommand LowerValueCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}