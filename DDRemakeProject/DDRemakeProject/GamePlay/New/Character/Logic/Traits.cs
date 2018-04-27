using System.ComponentModel;
using System.Runtime.CompilerServices;
using DDRemakeProject.Annotations;

namespace DDRemakeProject.GamePlay.New.Character.Logic
{
    public class Traits :INotifyPropertyChanged
    {
        public Traits(CharacterLogic characterLogic,int strength, int agility, int intelligence, int endurance)
        {
            CharacterLogic = characterLogic;
            StrengthTrait = new Trait(strength,characterLogic.Race.StrengthGrowth);
            AgilityTrait = new Trait(agility, characterLogic.Race.AgilityGrowth); 
            IntelligenceTrait = new Trait(intelligence, characterLogic.Race.IntelligenceGrowth); 
            EnduranceTrait = new Trait(endurance, characterLogic.Race.EnduranceGrowth); 
        }
        public Traits(Trait strength, Trait agility, Trait intelligence, Trait endurance)
        {
            StrengthTrait = strength;
            AgilityTrait = agility;
            IntelligenceTrait = intelligence;
            EnduranceTrait = endurance;
        }
        public Traits(int strength, int agility, int intelligence, int endurance)
        {
            //StrengthTrait = new Trait(strength,0);
            //AgilityTrait = new Trait(agility,0);
            //IntelligenceTrait = new Trait(intelligence,0);
            //EnduranceTrait = new Trait(endurance,0);
        }

        public Traits(CharacterLogic characterLogic)
        {
            CharacterLogic = characterLogic;
            StrengthTrait = new Trait(characterLogic.Traits.Strength, characterLogic.Race.StrengthGrowth);
            AgilityTrait = new Trait(characterLogic.Traits.Agility, characterLogic.Race.AgilityGrowth);
            IntelligenceTrait = new Trait(characterLogic.Traits.Intelligence, characterLogic.Race.IntelligenceGrowth);
            EnduranceTrait = new Trait(characterLogic.Traits.Endurance, characterLogic.Race.EnduranceGrowth);

            OnPropertyChanged(nameof(Strength));
        }


        public double Strength {
            get=> StrengthTrait?.Value(_level) ?? 0;
            set {
                StrengthTrait.Modifiers++;
                OnPropertyChanged();
            }
        }
        public double Agility
        {
            get => AgilityTrait?.Value(_level) ?? 0;
            set {
                AgilityTrait.Modifiers++;
                OnPropertyChanged();
            }
        }
        public double Intelligence
        {
            get => IntelligenceTrait?.Value(_level) ?? 0;
            set {
                IntelligenceTrait.Modifiers++;
                OnPropertyChanged();
            }
        }
        public double Endurance
        {
            get => EnduranceTrait?.Value(_level) ?? 0;
            set {
                EnduranceTrait.Modifiers++;
                OnPropertyChanged();
            }
        }



        private int _level => CharacterLogic?.Level.CurrentLevel ?? 0;

        public CharacterLogic CharacterLogic { get; set; }
        private Trait StrengthTrait { get; set; }
        private Trait AgilityTrait { get; set; }
        private Trait IntelligenceTrait { get; set; }
        private Trait EnduranceTrait { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
