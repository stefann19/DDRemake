using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay.New
{
    public class Stat : IAttribute
    {
        public Stat(string name)
        {
            Name = name;
            AttributeModifiers = new Dictionary<string, double>();
        }
        public Stat(string name, Func<Character,double> value):this(name)
        {
            FuncValue = value;
        }
        public Stat(string name, Func<Character, double> value,Character character):this(name,value)
        {
            Character = character;
        }
        public Stat(string name, Func<Character, double> value, Character character, Dictionary<string, double> attributeModifiers) : this(name, value,character)
        {
            AttributeModifiers = attributeModifiers;
        }

        public string Name { get; set; }

        public double Value => FuncValue(Character);

        public Func<Character,double> FuncValue { get; set; }
        public Character Character { get; set; }

        public Dictionary<string,double> AttributeModifiers { get; set; }
    }
}
