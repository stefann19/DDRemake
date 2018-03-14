using System;

namespace DDRemakeProject.GamePlay.New.Character.Logic
{
    public class Resistances
    {
        public CharacterLogic CharacterLogic { get; set; }
        public Resistance Armour { get; set; }
        public Resistance Fire { get; set; }
        public Resistance Water { get; set; }
        public Resistance Air { get; set; }
        public Resistance Earth { get; set; }

        public Resistances(CharacterLogic characterLogic)
        {
            CharacterLogic = characterLogic;

            double enduranceValue = characterLogic.Traits.Endurance.Value;
            double strengthValue = characterLogic.Traits.Strength.Value;
            double intelligenceValue = characterLogic.Traits.Intelligence.Value;
            double agilityValue = characterLogic.Traits.Agility.Value;

            Armour = new Resistance(enduranceValue * 2 + strengthValue * 2,1);
            Fire = new Resistance(enduranceValue + intelligenceValue * 2 + agilityValue,1);
            Water = new Resistance(intelligenceValue * 2 + agilityValue * 2,1);
            Air = new Resistance(agilityValue * 3 + intelligenceValue,1);
            Earth = new Resistance(enduranceValue*3 + strengthValue,1);

            void AssignValues(Resistance resist,double flat,double percentage)
            {
                Resistance resist1 = resist;
                resist = NewResistance((damage) => damage - resist1.Value * flat - (percentage * resist1.Value * damage / 100f));
            }
            AssignValues(Armour,0.2,0.1);
            AssignValues(Fire,0.1,0.4);
            AssignValues(Water,0.2,0.3);
            AssignValues(Air,0.3,0.2);
            AssignValues(Earth,0.4,0.1);
        }

      /*  private double Reduction(double damage,double flat,double percentage,double a)
        {

        }*/

        private Resistance NewResistance(Func<double, double> calculate)
        {
            return new Resistance(calculate,CharacterLogic);
        }
    }
}
