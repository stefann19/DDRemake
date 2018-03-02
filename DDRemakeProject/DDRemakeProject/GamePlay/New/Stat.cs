using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay.New
{

    public class Stat
    {
        public Stat(double maxValue)
        {
            MaxValue = maxValue;
            CurrentValue = maxValue;
        }
        public Stat(double maxValue,double currentValue):this(maxValue)
        {
            CurrentValue = currentValue;
        }
        public double MaxValue { get; set; }
        public double CurrentValue { get; set; }
    }
}
