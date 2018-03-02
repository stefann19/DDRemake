using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay.New
{
    public class Stats
    {
        public Stat Strength { get; set; }
        public Stat Agility { get; set; }
        public Stat Intelligence { get; set; }
        public Stat Endurance { get; set; }

        Stats()
        {
            Strength = new Stat();
        }
    }
}
