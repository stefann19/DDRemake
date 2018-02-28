using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay.New
{
    public enum Stats
    {
       Strength,
       Intelligence,
       Agility,
       Endurance
    }

    public enum Attributes
    {
        Armour,
        FireResist,
        WaterResist,
        EarthResist,
        AirResist,
    }

    public static class StatsLogic
    {
        public static Stat Strength = new Stat("Strength");
        public static Stat Intelligence = new Stat("Intelligence");
        public static Stat Agility = new Stat("Agility");
        public static Stat Endurance = new Stat("Endurance");


        public static Stat Armour = new Stat("Armour", (ch) => ch.Stats["Strength"].Value * ch.Stats["Strength"].AttributeModifiers["Armour"] + ch.Stats["Endurance"].Value * 0.5f);

      
    }
}
