using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DDRemakeProject.Annotations;
using DDRemakeProject.Commands;
using DDRemakeProject.GamePlay.New.Character;
using DDRemakeProject.GamePlay.New.Character.Logic;

namespace DDRemakeProject.ViewModels
{
    public class RacePanelViewModel : INotifyPropertyChanged
    {
        private Races _value;
        private ImageSource _currentRaceAvatar;
        private string _currentRace;
        private ObservableCollection<string> _statsGrowth;

        public Race Race { get; set; }

        public RacePanelViewModel()
        {
            CurrentRaceAvatar = Race.GetRaceFromEnum(RaceType).Avatar;
            NextRaceCommand = new RelayCommand((param)=>{ModifyValue(1,param);});
            PreviousRaceCommand = new RelayCommand((param) => { ModifyValue(-1,param); });

           /* ModifyValue(1);
            ModifyValue(-1);*/
        }

        public ICommand NextRaceCommand { get; set; }
        public ICommand PreviousRaceCommand { get; set; }

        public string CurrentRace => RaceType.ToString();

        public ImageSource CurrentRaceAvatar {
            get => _currentRaceAvatar;
            set {
                if (Equals(value, _currentRaceAvatar)) return;
                _currentRaceAvatar = value;
                OnPropertyChanged();
            }
        }


        private int RacesLength => Enum.GetNames(typeof(Races)).Length;
        private int Mod(int x) => (x % RacesLength + RacesLength) % RacesLength;
        public void ModifyValue(int change,object param)
        {
            Character ch = ((object[])param)[0] as Character;
            
            ch.CharacterLogic.Race = Race.GetRaceFromEnum((Races)Mod((int)ch.CharacterLogic.Race.Name + change));
        }
        public Races RaceType {
            get => _value;
            set
            {
                _value = value;
                Race = Race.GetRaceFromEnum(RaceType);
                
                OnPropertyChanged(nameof(CurrentRace));
                OnPropertyChanged(nameof(StatsGrowth));
                OnPropertyChanged(nameof(Offence));
                OnPropertyChanged(nameof(Defence));
                CurrentRaceAvatar = Race.Avatar;
            }

        }


        public ObservableCollection<StatProjectionViewModel> StatsGrowth => Race==null?null: new ObservableCollection<StatProjectionViewModel>
        {
            new StatProjectionViewModel("Strength:",Race.StrengthGrowth.ToString()),new StatProjectionViewModel("Agility:",Race.AgilityGrowth.ToString()),
            new StatProjectionViewModel("Intelligence:",Race.IntelligenceGrowth.ToString()),new StatProjectionViewModel("Endurance:",Race.EnduranceGrowth.ToString())
        };
        public ObservableCollection<StatProjectionViewModel> Offence => Race == null ? null : new ObservableCollection<StatProjectionViewModel>
        {
            new StatProjectionViewModel("Armour:",Race.ArmourAttackFormulaString.Replace("traits.","").Replace(" ","")),new StatProjectionViewModel("Air:",Race.AirAttackFormulaString.Replace("traits.","").Replace(" ","")),
            new StatProjectionViewModel("Earth:",Race.EarthAttackFormulaString.Replace("traits.","").Replace(" ","")),new StatProjectionViewModel("Fire:",Race.FireAttackFormulaString.Replace("traits.","").Replace(" ","")),
            new StatProjectionViewModel("Water:",Race.WaterAttackFormulaString.Replace("traits.","").Replace(" ",""))
        };
        public ObservableCollection<StatProjectionViewModel> Defence => Race == null ? null : new ObservableCollection<StatProjectionViewModel>
        {
            new StatProjectionViewModel("Armour:",Race.ArmourResistanceFactors.ToString()),new StatProjectionViewModel("Air:",Race.AirResistanceFactors.ToString()),
            new StatProjectionViewModel("Earth:",Race.EarthResistanceFactors.ToString()),new StatProjectionViewModel("Fire:",Race.FireResistanceFactors.ToString()),
            new StatProjectionViewModel("Water:",Race.WaterResistanceFactors.ToString())
        };
        /**/

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}