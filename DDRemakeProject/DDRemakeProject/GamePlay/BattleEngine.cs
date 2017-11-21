using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay
{
    public class BattleEngine
    {
        public List<Character> AllyCharacters { get; private set; }
        public List<Character> EnemyCharacters { get; private set; }
        public List<Character> Characters { get; private set; }

        public BattleWindow BWindow { get; set; }
        public UIBackEnd UiBackEndInstance;


        public List<int> AttackOrder;
        public int currentCharacterIndex;

        public BattleEngine(List<Character> allyCharacters, List<Character> enemyCharacters)
        {
            AllyCharacters = allyCharacters;
            EnemyCharacters = enemyCharacters;

            Characters = new List<Character>();
            Characters.AddRange(AllyCharacters);
            Characters.AddRange(EnemyCharacters);


            UiBackEndInstance = new UIBackEnd(this);
            BWindow = new BattleWindow(UiBackEndInstance);
            UiBackEndInstance.SetPopOutWindow(BWindow.PopOutWindow);
            GenerateMap();
        }


        private void GenerateAttackOrder()
        {
            currentCharacterIndex = 0;
            List<Character> chararacters = new List<Character>(Characters);
            while (chararacters.Count > 0)
            {
                int indexOfMax = 0;
                for (int i = 0; i < chararacters.Count; i++)
                {
                    if (chararacters[indexOfMax].Inteligence < chararacters[i].Inteligence)
                    {
                        indexOfMax = i;
                    }
                }
                AttackOrder.Add(indexOfMax);
                chararacters.RemoveAt(indexOfMax);
            }
        }

        /// <summary>
        /// Set every character ui and stats for current game
        /// </summary>
        private void GenerateMap()
        {
            
            BWindow.InitializeBattleUI(AllyCharacters,EnemyCharacters);
        }

        private Character SelectedCharacter(bool enemy)
        {
            if (enemy) return EnemyCharacters.ElementAt(BWindow.SelectedCharacterIndex);
            return AllyCharacters.ElementAt(BWindow.SelectedCharacterIndex);
        }



        public void SelectNewCharacter()
        {
            currentCharacterIndex++;
            if (currentCharacterIndex > Characters.Count)
            {
                currentCharacterIndex -= Characters.Count;
            }
        }

        private void Fight(Character defenderCharacter,Skill skill)
        {

            // execute the attack animation in the UI
            //skill.SkillEffect;


            //execute the hp/ap drain animation in the UI
            
            
            //Execute the stats changes of the fight
            DoFightStatChanges(Characters[currentCharacterIndex], defenderCharacter, skill);
        }

        private void DoFightStatChanges(Character attackerCharacter, Character defenderCharacter, Skill skill)
        {
            int damageAfterReductions = (int)(StatsLogic.CalculateDamageAfterAr(defenderCharacter.Armour, skill.Damage));
            defenderCharacter.CurrentHp -= damageAfterReductions;
            attackerCharacter.CurrentAp -= skill.ApCost;
            attackerCharacter.CurrentMp -= skill.MpCost;
            CheckIfDead(defenderCharacter);
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
