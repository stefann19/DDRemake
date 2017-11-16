using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDRemakeProject.GamePlay;

namespace DDRemakeProject.GamePlay
{
    public class Character
    {
        #region Properties

        /// <summary>
        /// Max HP
        /// </summary>
        public int Hp { get; private set; }
        /// <summary>
        /// Current Hp
        /// </summary>
        public int CurrentHp { get; set; }

        /// <summary>
        /// Max MP
        /// </summary>
        public int Mp { get; private set; }
        /// <summary>
        /// Current MP
        /// </summary>
        public int CurrentMp { get; set; }


        /// <summary>
        /// Max AP
        /// </summary>
        public int Ap { get; private set; }
        /// <summary>
        /// Current AP
        /// </summary>
        public int CurrentAp { get; set; }

        public float Armour { get;private set; }

        public int Strength { get; private set; }
        public int Inteligence { get; private set; }
        public int Constitution { get; private set; }
        public int Speed { get; private set; }

        public int Level { get; set; }
        public int CurrentXp { get; set; }

        public string CharacterPng { get; set; }
        public string CharacterIconPng { get; set; }
        public string Name { get; private set; }
        #endregion

        #region Constructors
        public Character(string name,string characterPng,string characterIconPng, int level, int strength, int inteligence, int constitution, int speed)
        {
            #region SetStats

            
            CurrentXp = 0;
            CharacterIconPng = characterIconPng;
            CharacterPng = characterPng;
            Name = name;
            Level = level;
            Strength = strength;
            Inteligence = inteligence;
            Constitution = constitution;
            Speed = speed;
            #endregion
            //get HP/MP/AP Values from the above stats
            Intialize();

            CurrentHp = Hp;
            CurrentMp = Mp;
            CurrentAp = Ap;
        }

        private void Intialize()
        {
            Hp = StatsLogic.GetHp(this);
            Ap = StatsLogic.GetAp(this);
            Mp = StatsLogic.GetMp(this);
        }
        #endregion

        #region private Methods



        #endregion

    }
}
