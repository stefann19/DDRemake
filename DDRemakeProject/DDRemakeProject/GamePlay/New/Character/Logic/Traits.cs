namespace DDRemakeProject.GamePlay.New.Character.Logic
{
    public class Traits
    {
        public Traits(Trait strength, Trait agility, Trait intelligence, Trait endurance)
        {
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Endurance = endurance;
        }

        public Traits(int strength, int agility, int intelligence, int endurance)
        {
            Strength = new Trait(strength);
            Agility = new Trait(agility);
            Intelligence = new Trait(intelligence);
            Endurance = new Trait(endurance);
        }

        public Trait Strength { get; set; }
        public Trait Agility { get; set; }
        public Trait Intelligence { get; set; }
        public Trait Endurance { get; set; }
    }
}
