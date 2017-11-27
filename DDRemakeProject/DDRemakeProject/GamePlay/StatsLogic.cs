using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay
{
    public class StatsLogic
    {
        #region HP
        public const int LevelToHp = 3;
        public const int ConstitutionToHp = 6;
        public const int StrengthToHp = 4;
        public const int SpeedToHp = 3;
        public const int InteligenceToHp = 2;

        
        public static int GetHp(CharacterStats ch)
        {
            int hp = 0;
            hp = ch.Level * LevelToHp + ch.Constitution * ConstitutionToHp + ch.Strength * StrengthToHp +
                 ch.Inteligence * InteligenceToHp + ch.Speed * SpeedToHp;
            return hp;
        }
        #endregion

        #region MP
        public const int LevelToMp = 2;
        public const int ConstitutionToMp = 1;
        public const int StrengthToMp = 1;
        public const int SpeedToMp = 2;
        public const int InteligenceToMp = 5;


        public static int GetMp(CharacterStats ch)
        {
            int mp = 0;
            mp = ch.Level * LevelToMp + ch.Constitution * ConstitutionToMp + ch.Strength * StrengthToMp +
                 ch.Inteligence * InteligenceToMp + ch.Speed * SpeedToMp;
            return mp;
        }
        #endregion

        #region AP
        public const float LevelToAp = 0.33f;
        public const float ConstitutionToAp = 0.1f;
        public const float StrengthToAp = 0.1f;
        public const float SpeedToAp = 0.5f;
        public const float InteligenceToAp = 0.2f;


        public static int GetAp(CharacterStats ch)
        {
            int ap = 0;
            ap = (int)( ch.Level * LevelToAp + ch.Constitution * ConstitutionToAp + ch.Strength * StrengthToAp +
                 ch.Inteligence * SpeedToAp + ch.Speed * InteligenceToAp);
            return ap;
        }
        #endregion

        #region Armour
        public const float LevelToAr = 1f;
        public const float ConstitutionToAr = 2f;
        public const float StrengthToAr = 1f;
        public const float SpeedToAr = 0.5f;
        public const float InteligenceToAr = 0.3f;

        public static float CalculateDamageAfterAr(float armour,float damage)
        {
            return damage * (1-(damage /( damage + armour)));
        }


        public static int GetAr(CharacterStats ch)
        {
            int ar = 0;
            ar = (int)(ch.Level * LevelToAr + ch.Constitution * ConstitutionToAr + ch.Strength * StrengthToAr +
                       ch.Inteligence * SpeedToAr + ch.Speed * InteligenceToAr);
            return ar;
        }


        #endregion

        #region Actions

        //default Medium Attack
        public static Action MediumAttack = new Action("../../Assets/HUD/MediumAttack.png","../../Assets/fx/5.gif",10,2,0,ActionTypes.ActionType.Attack);
        //default Low Attack
        public static Action LowAttack = new Action("../../Assets/HUD/LowAttack.png", "../../Assets/fx/2.gif", 6, 1, 0, ActionTypes.ActionType.Attack);
        public static Action FireSpell = new Action("../../Assets/HUD/Fire.png", "../../Assets/fx/9.gif", 15, 1, 5, ActionTypes.ActionType.Spell);


        #endregion
    }
}
