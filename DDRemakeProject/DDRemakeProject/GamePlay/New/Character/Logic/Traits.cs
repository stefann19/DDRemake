using System.ComponentModel;
using System.Runtime.CompilerServices;
using DDRemakeProject.Annotations;

namespace DDRemakeProject.GamePlay.New.Character.Logic
{
    public class Traits :INotifyPropertyChanged
    {
        private int _level;

        public Traits(CharacterLogic characterLogic,int strength, int agility, int intelligence, int endurance)
        {
            CharacterLogic = characterLogic;
            StrengthTrait = new Trait(strength,characterLogic.Race.StrengthGrowth);
            AgilityTrait = new Trait(agility, characterLogic.Race.AgilityGrowth); 
            IntelligenceTrait = new Trait(intelligence, characterLogic.Race.IntelligenceGrowth); 
            EnduranceTrait = new Trait(endurance, characterLogic.Race.EnduranceGrowth);

            Level = characterLogic.Level.CurrentLevel;
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

        //public Traits(CharacterLogic characterLogic)
        //{
        //    CharacterLogic = characterLogic;
        //    StrengthTrait = new Trait(characterLogic.Traits.Strength, characterLogic.Race.StrengthGrowth);
        //    AgilityTrait = new Trait(characterLogic.Traits.Agility, characterLogic.Race.AgilityGrowth);
        //    IntelligenceTrait = new Trait(characterLogic.Traits.Intelligence, characterLogic.Race.IntelligenceGrowth);
        //    EnduranceTrait = new Trait(characterLogic.Traits.Endurance, characterLogic.Race.EnduranceGrowth);


        //}


        public CharacterLogic CharacterLogic { get; set; }
        public Trait StrengthTrait { get; set; }
        public Trait AgilityTrait { get; set; }
        public Trait IntelligenceTrait { get; set; }
        public Trait EnduranceTrait { get; set; }

        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                StrengthTrait.Level = _level;
                AgilityTrait.Level = _level;
                IntelligenceTrait.Level = _level;
                EnduranceTrait.Level = _level;
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
