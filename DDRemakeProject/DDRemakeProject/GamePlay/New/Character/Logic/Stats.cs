namespace DDRemakeProject.GamePlay.New.Character.Logic
{
    public class Stats
    {
        public Stats(CharacterLogic characterLogic)
        {
            CharacterLogic = characterLogic;
            double enduranceValue = characterLogic.Traits.EnduranceTrait.Value;
            double strengthValue = characterLogic.Traits.StrengthTrait.Value;
            double intelligenceValue = characterLogic.Traits.IntelligenceTrait.Value;
            double agilityValue = characterLogic.Traits.AgilityTrait.Value;
            int level = characterLogic.Level.CurrentLevel;

            double health = enduranceValue * 7 + strengthValue * 3;
            Health = NewStat(health,5);
            double mana = intelligenceValue * 5;
            Mana = NewStat(mana, 1);
            double stamina = agilityValue * 4;
            Stamina = NewStat(stamina, 3);
            double rage = agilityValue * 3 + strengthValue * 3;
            Rage = NewStat(rage, 2);
        }

        private Stat NewStat(double @base,double growthPerLevel)
        {
            return new Stat(CharacterLogic,@base,growthPerLevel);
        }

        public CharacterLogic CharacterLogic { get; set; }

        public Stat Health { get; set; }
        public Stat Mana { get; set; }
        public Stat Stamina { get; set; }
        public Stat Rage { get; set; }

        /*Stats()
        {
            Health = new Stat();
        }*/
    }
}
