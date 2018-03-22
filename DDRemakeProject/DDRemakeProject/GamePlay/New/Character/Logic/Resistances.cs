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

            double enduranceValue = characterLogic.Traits.Endurance;
            double strengthValue = characterLogic.Traits.Strength;
            double intelligenceValue = characterLogic.Traits.Intelligence;
            double agilityValue = characterLogic.Traits.Agility;

            Armour = new Resistance(enduranceValue * 2 + strengthValue * 2, 1);
            Fire = new Resistance(enduranceValue + intelligenceValue * 2 + agilityValue, 1);
            Water = new Resistance(intelligenceValue * 2 + agilityValue * 2, 1);
            Air = new Resistance(agilityValue * 3 + intelligenceValue, 1);
            Earth = new Resistance(enduranceValue * 3 + strengthValue, 1);

            void AssignValues(Resistance resist,Race.Resist resistFactors)
            {
                Resistance resist1 = resist;
                resist = NewResistance((damage) => damage - resist1.Value * resistFactors.Flat - (resistFactors.Percentage * resist1.Value * damage / 100f));
            }
            AssignValues(Armour,characterLogic.Race.ArmourResistanceFactors);
            AssignValues(Fire, characterLogic.Race.FireArmourResistanceFactors);
            AssignValues(Water, characterLogic.Race.WaterResistanceFactors);
            AssignValues(Air, characterLogic.Race.AirResistanceFactors);
            AssignValues(Earth, characterLogic.Race.EarthResistanceFactors);
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
