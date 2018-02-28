using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay.New
{
    public class IAttribute
    {
        string Name { get; set; }
        Func<Character, double> FuncValue { get; set; }
        Character Character { get; set; }
        double Value { get; }
        Dictionary<string, double> AttributeModifiers { get; set; }
    }
}
