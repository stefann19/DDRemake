using System;

namespace DDRemakeProject.GamePlay.New.Character.Logic
{

    public class Resistance
    {

        public CharacterLogic CharacterLogic { get; set; }
        public double Value  => Base + GrowthPerLevel * CharacterLogic.Level.CurrentLevel;
        public Func<double,double> Calculate { get; set; }
        public Resistance(Func<double,double> calculate,CharacterLogic characterLogic)
        {
            Calculate = calculate;
            CharacterLogic = characterLogic;
        }

        public Resistance(Func<double, double> calculate, double @base, double growthPerLevel,CharacterLogic characterLogic) : this( @base,growthPerLevel)
        {
            CharacterLogic = characterLogic;
            Calculate = calculate;

        }

        public Resistance(double @base, double growthPerLevel)
        {
            Base = @base;
            GrowthPerLevel = growthPerLevel;
        }

        public double Base { get; set; }
        public double GrowthPerLevel { get; set; }

        
    }
}