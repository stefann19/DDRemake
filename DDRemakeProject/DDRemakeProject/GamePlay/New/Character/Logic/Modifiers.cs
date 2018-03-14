namespace DDRemakeProject.GamePlay.New.Character.Logic
{
    public class Modifiers
    {
        public Modifiers(CharacterLogic characterLogic)
        {
            double enduranceValue = characterLogic.Traits.Endurance.Value;
            double strengthValue = characterLogic.Traits.Strength.Value;
            double intelligenceValue = characterLogic.Traits.Intelligence.Value;
            double agilityValue = characterLogic.Traits.Agility.Value;
            int level = characterLogic.Level.CurrentLevel;

            PhysicalAttack = strengthValue * 5 + level * 2 + agilityValue * 3;
            FireAttack = strengthValue * 3 + intelligenceValue * 7;
            WaterAttack = intelligenceValue * 3 + agilityValue * 4 + strengthValue *3;
            AirAttack = intelligenceValue * 8 + agilityValue * 2;
            EarthAttack = enduranceValue * 3 + intelligenceValue * 4 + strengthValue *3;
        }

        public double PhysicalAttack { get; set; }
        public double FireAttack { get; set; }
        public double WaterAttack { get; set; }
        public double AirAttack { get; set; }
        public double EarthAttack { get; set; } 
    }
}
