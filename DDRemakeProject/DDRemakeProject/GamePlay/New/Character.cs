using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay.New
{
    public class Character
    {
        public Character(Dictionary<string, Attribute> stats, Dictionary<string, Attribute> attributes)
        {
            Stats = stats;
            Attributes = attributes;
        }

        public Dictionary<string, Attribute> Stats { get; set; }
        public Dictionary<string, Attribute> Attributes { get; set; }


        public double this[Stat s]
        {
            get
            {
                double val = 0;
                this.Stats.Values.ToList().ForEach(stat=>
                {
                    if(stat.AttributeModifiers.ContainsKey(s.Name))
                        val += stat.Value * stat.AttributeModifiers[s.Name];
                    
                });
                return val;
            }
        }


        public void AddAttribute(Attribute attribute)
        {
            Attributes.Add(attribute.Name,attribute);
        }
    }
}
