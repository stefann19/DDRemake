using System;

namespace DDRemakeProject.GamePlay.New
{

    public class Resistance
    {
        public double Value { get; set; }
        public Func<double,double> Calculate { get; set; }

        public Resistance(double value,Func<double,double> calculate)
        {
            Value = value;
            Calculate = calculate;
            
        }
    }
}