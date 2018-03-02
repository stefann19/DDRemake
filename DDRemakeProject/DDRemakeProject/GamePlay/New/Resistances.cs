using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay.New
{
    public class Resistances
    {
        public Resistance Armour { get; set; }
        public Resistance Fire { get; set; }
        public Resistance Water { get; set; }
        public Resistance Air { get; set; }
        public Resistance Earth { get; set; }

        Resistances(double armour,double fire,double water,double air,double earth)
        {
            Armour = new Resistance(armour, ( damage) => damage - armour * 0.2f - (0.1f * armour * damage /100f)  );

            Fire = new Resistance(fire, (damage) => damage - fire * 0.10f - (0.4f * fire * damage / 100f));
            Water = new Resistance(water, (damage) => damage - water * 0.2f - (0.3f * water * damage / 100f));
            Air = new Resistance(air, (damage) => damage - air * 0.3f - (0.2f * air * damage / 100f));
            Earth = new Resistance(earth, (damage) => damage - earth * 0.4f - (0.1f * earth * damage / 100f));

        }
    }
}
