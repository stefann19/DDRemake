namespace DDRemakeProject.GamePlay.New.Character.Logic
{

    public class Stat
    {
       

        public Stat(CharacterLogic characterLogic, double @base, double growthPerLevel):this(characterLogic)
        {
            Base = @base;
            GrowthPerLevel = growthPerLevel;
            CurrentValue = MaxValue;
        }

        public Stat(CharacterLogic characterLogic)
        {
            CharacterLogic = characterLogic;
        }

        public CharacterLogic CharacterLogic { get; set; }
      /*  public Stat(double maxValue,double currentValue):this(maxValue)
        {
            CurrentValue = currentValue;
        }*/

        public double MaxValue => Base + GrowthPerLevel * CharacterLogic.Level.CurrentLevel;
        public double CurrentValue { get; set; }

        public double Base { get; set; }
        public double GrowthPerLevel { get; set; }  
    }
}
