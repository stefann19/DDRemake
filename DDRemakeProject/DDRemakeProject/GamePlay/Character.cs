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
        private int _hp;

        /// <summary>
        /// Max MP
        /// </summary>
        public int Mp { get; private set; }
        /// <summary>
        /// Current MP
        /// </summary>
        private int _mp;


        /// <summary>
        /// Max AP
        /// </summary>
        public int Ap { get; private set; }
        /// <summary>
        /// Current AP
        /// </summary>
        private int _ap;


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

            _hp = 0;
            _mp = 0;
            _ap = 0;
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
