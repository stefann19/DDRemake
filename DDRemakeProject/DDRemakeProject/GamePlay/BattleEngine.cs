using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay
{
    class BattleEngine
    {
        public List<Character> AllyCharacters { get; private set; }
        public List<Character> EnemyCharacters { get; private set; }

        public BattleWindow BWindow { get; set; }

        public BattleEngine(List<Character> allyCharacters, List<Character> enemyCharacters)
        {
            AllyCharacters = allyCharacters;
            EnemyCharacters = enemyCharacters;
            BWindow = new BattleWindow();
            GenerateMap();
        }

        /// <summary>
        /// Set every character ui and stats for current game
        /// </summary>
        private void GenerateMap()
        {
            
            BWindow.InitializeBattleUI(AllyCharacters,EnemyCharacters);
        }

        private Character SelectCharacter()
        {
            return null;
        }

        private void Fight(Character attackerCharacter,Character defenderCharacter,Skill skill)
        {

            // execute the attack animation in the UI
            //skill.SkillEffect;



            //execute the hp/ap drain animation in the UI
            int damageAfterReductions =(int) (StatsLogic.CalculateDamageAfterAr(defenderCharacter.Armour, skill.Damage));
            defenderCharacter.CurrentHp -= damageAfterReductions;
            attackerCharacter.CurrentAp -= skill.ApCost;
            attackerCharacter.CurrentMp -= skill.MpCost;
        }

        private bool CheckIfDead(Character ch)
        {
            if (ch.CurrentHp <= 0) //Declare dead
            {
                //Execute Cleanup function
                DeadCleanup(ch);

                return true;
            }
            else return false;
        }

        private void DeadCleanup(Character ch)
        {
            // execute UI cleanup

            //backend
            if (AllyCharacters.Contains(ch))
            {
                AllyCharacters.Remove(ch);
            }else if (EnemyCharacters.Contains(ch))
            {
                EnemyCharacters.Remove(ch);
            }

        }

    }
}
