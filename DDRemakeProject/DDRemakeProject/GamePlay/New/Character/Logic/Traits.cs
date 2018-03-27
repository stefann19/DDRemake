namespace DDRemakeProject.GamePlay.New.Character.Logic
{
    public class Traits
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


        public double Strength => StrengthTrait?.Value(_level) ?? 0;
        public double Agility => AgilityTrait?.Value(_level) ?? 0;
        public double Intelligence => IntelligenceTrait?.Value(_level) ?? 0;
        public double Endurance => EnduranceTrait?.Value(_level) ?? 0;



        private int _level => CharacterLogic?.Level.CurrentLevel ?? 0;

        public CharacterLogic CharacterLogic { get; set; }
        private Trait StrengthTrait { get; set; }
        private Trait AgilityTrait { get; set; }
        private Trait IntelligenceTrait { get; set; }
        private Trait EnduranceTrait { get; set; }
    }
}
