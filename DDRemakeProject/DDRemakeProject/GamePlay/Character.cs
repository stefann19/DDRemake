using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDRemakeProject.GamePlay;

namespace DDRemakeProject
{
    class Character
    {
        #region Properties
        public int Hp { get; set; }
        public int Mp { get; set; }

        public int Ap { get; set; }

        public int Strength { get; }
        public int Inteligence { get; }
        public int Constitution { get;  }
        public int Speed { get; }


        public int Level { get; }
        public int CurrentXp { get; }

        public string CharacterIcon { get;}
        #endregion

        #region Constructors
        public Character(string characterIcon, int level, int strength, int inteligence, int constitution, int speed)
        {
            CharacterIcon = characterIcon;
            CurrentXp = 0;
            Level = level;
            Strength = strength;
            Inteligence = inteligence;
            Constitution = constitution;
            Speed = speed;

            Hp = StatsLogic.GetHp(this);
            Ap = StatsLogic.GetAp(this);
            Mp = StatsLogic.GetMp(this);

        }
        #endregion

        #region private Methods



        #endregion

    }
}
