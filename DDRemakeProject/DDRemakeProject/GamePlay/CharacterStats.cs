using System.Collections.Generic;

namespace DDRemakeProject.GamePlay
{
    public class CharacterStats
    {
        public Character CharacterParent;
        #region Properties

        /// <summary>
        /// Max HP
        /// </summary>
        public int Hp { get; set; }

        /// <summary>
        /// Current Hp
        /// </summary>
        public int CurrentHp
        {
            get => _currentHp;
            set
            {
                _currentHp = value;
                CharacterParent?.UpdateHpUi();
            }
        }

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

        public List<Action> Actions;
        private int _currentHp;

        #endregion

        #region Constructors
        public CharacterStats(string name,string characterPng,string characterIconPng, int level, int strength, int inteligence, int constitution, int speed)
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
            IsEnemy = characterPng.Contains("monster");
        }
        
        public bool IsEnemy { get;private set; }
        public string UiImageName { get; set; }

        private void Intialize()
        {
            Hp = StatsLogic.GetHp(this);
            Ap = StatsLogic.GetAp(this);
            Mp = StatsLogic.GetMp(this);

            Armour = StatsLogic.GetAr(this);
            GetDefaultActions();
        }

        private void GetDefaultActions()
        {
            Actions = new List<Action> {StatsLogic.LowAttack, StatsLogic.MediumAttack, StatsLogic.FireSpell};

        }
        #endregion

        #region private Methods



        #endregion

        
    }
}
