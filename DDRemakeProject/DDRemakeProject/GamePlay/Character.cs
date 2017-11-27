using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay
{
    public class Character : IComparable <Character>
    {
        public CharacterStats CharacterStats { get; set; }
        public CharacterTypes.Status Status { get; set; }
        public CharacterTypes.Type Type { get; set; }



        public System.Windows.Controls.Image ImageGif { get; set; }

        public Character(CharacterStats characterStats,CharacterTypes.Type type)
        {
            CharacterStats = characterStats;
            Status = CharacterTypes.Status.Alive;
            Type = type;
        }



        public int CompareTo(Character ch)
        {
            return this.CharacterStats.Inteligence - ch.CharacterStats.Inteligence;
        }
    }
}
