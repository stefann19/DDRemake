namespace DDRemakeProject.GamePlay.New.Character.Logic
{
    public class Trait
    {
        public Trait(double value,double growthPerLevel)
        {
            @base = value;
            _growthPerLevel = growthPerLevel;
        }

        private double @base;
        private double _growthPerLevel;
        public double Value(int level) => @base + _growthPerLevel * level;
    }
}
