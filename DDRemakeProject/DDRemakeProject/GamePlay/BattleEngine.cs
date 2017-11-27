using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DDRemakeProject.GamePlay
{
    public class BattleEngine
    {
        //public List<Character> AllyCharacters { get; private set; }
        //public List<Character> EnemyCharacters { get; private set; }
        public List<Character> Characters { get; private set; }

        public BattleWindow BWindow { get; set; }


        public List<int> AttackOrder;
        public int currentCharacterIndex;

        public CharacterMiniWindow CurrentCharacterMiniWindow { get; set; }
        public CharacterMiniWindow SelectedCharacterMiniWindow { get; set; }


        public BattleEngine(List<CharacterStats> allyCharacters, List<CharacterStats> enemyCharacters)
        {
            currentCharacterIndex = -1;
            Characters = new List<Character>();
            foreach (CharacterStats characterStats in allyCharacters)
            {
                Characters.Add(new Character(characterStats,CharacterTypes.Type.Ally));
            }
            foreach (CharacterStats characterStats in enemyCharacters)
            {
                Characters.Add(new Character(characterStats, CharacterTypes.Type.Enemy));
            }




            BWindow = new BattleWindow(this);
            GenerateMap();
            InitialiseBottomRightMiniWindows();
            //UiBackEndInstance.SetPopOutWindow(BWindow.PopOutWindow);
        }

        private void InitialiseBottomRightMiniWindows()
        {
            CurrentCharacterMiniWindow = new CharacterMiniWindow(BWindow.CurrentChar);
            SelectedCharacterMiniWindow = new CharacterMiniWindow(BWindow.TargetChar);
            
           // GenerateAttackOrder();
            currentCharacterIndex = -1;
            SetNextTurn();
        }

        private void SetNextTurn()
        {

            currentCharacterIndex++;
            //if (currentCharacterIndex >= AttackOrder.Count) currentCharacterIndex -= AttackOrder.Count;
            //Characters[AttackOrder[currentCharacterIndex]].CurrentAp =
            //    Characters[AttackOrder[currentCharacterIndex]].Ap;


            CurrentCharacterMiniWindow.SetStats(Characters[currentCharacterIndex].CharacterStats);
            CurrentCharacterMiniWindow.SelectType = "Current Turn";

            
            
            BWindow.SelectTurnChar(Characters[currentCharacterIndex].ImageGif);
        }

        public void ReloadChar()
        {
            CharacterStats ch = Characters[currentCharacterIndex].CharacterStats;
            CurrentCharacterMiniWindow.SetStats(ch);
        }


        public void SelectChar(int index)
        {
            SelectedCharacterMiniWindow.SetVisibility(Visibility.Visible);
            SelectedCharacterMiniWindow.SetStats(Characters[index].CharacterStats);
            SelectedCharacterMiniWindow.SelectType = "Selected";
        }

        public void DeselectCHar()
        {
            SelectedCharacterMiniWindow.SetVisibility(Visibility.Hidden);
        }

        //private void GenerateAttackOrder()
        //{
        //    AttackOrder =new List<int>();
        //    currentCharacterIndex = 0;
        //    List<Character> chararacters = new List<Character>(Characters);
        //    while (chararacters.Count > 0)
        //    {
        //        int indexOfMax = 0;
        //        for (int i = 0; i < chararacters.Count; i++)
        //        {
        //            if (chararacters[indexOfMax].Inteligence < chararacters[i].Inteligence)
        //            {
        //                indexOfMax = i;
        //            }
        //        }
        //        int globalIndex = Characters.IndexOf(chararacters[indexOfMax]);
        //        AttackOrder.Add(globalIndex);
        //        chararacters.RemoveAt(indexOfMax);
        //    }
        //}

        /// <summary>
        /// Set every character ui and stats for current game
        /// </summary>
        private void GenerateMap()
        {
            
            BWindow.InitializeBattleUi(Characters);
        }

        //private CharacterStats SelectedCharacter(bool enemy)
        //{
        //    if (enemy) return EnemyCharacters.ElementAt(BWindow.SelectedCharacterIndex).;
        //    return AllyCharacters.ElementAt(BWindow.SelectedCharacterIndex);
        //}



        public void SelectNewCharacter()
        {
            currentCharacterIndex++;
            if (currentCharacterIndex > Characters.Count)
            {
                currentCharacterIndex -= Characters.Count;
            }
        }

        public void Fight(Character defenderCharacter,Action skill)
        {

            // execute the attack animation in the UI
            //skill.SkillEffect;


            //execute the hp/ap drain animation in the UI


            //Execute the stats changes of the fight\
            if (skill.ApCost <= Characters[currentCharacterIndex].CharacterStats.CurrentAp)
                DoFightStatChanges(Characters[currentCharacterIndex].CharacterStats, defenderCharacter, skill);
            SetNextTurn();
        }

        private void DoFightStatChanges(CharacterStats attackerCharacter, Character defenderCharacter, Action skill)
        {
            int damageAfterReductions = (int)(StatsLogic.CalculateDamageAfterAr(defenderCharacter.CharacterStats.Armour, skill.Damage));
            defenderCharacter.CharacterStats.CurrentHp -= damageAfterReductions;
            attackerCharacter.CurrentAp -= skill.ApCost;
            attackerCharacter.CurrentMp -= skill.MpCost;
            CheckIfDead(defenderCharacter);
        }

        private bool CheckIfDead(Character ch)
        {
            if (ch.CharacterStats.CurrentHp <= 0) //Declare dead
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
            ch.ImageGif.Visibility = Visibility.Hidden;
            
            Characters.Remove(ch);
            Characters.Sort();
            AttackOrder.RemoveAt(currentCharacterIndex);

        }

        public List<Action> GetAvailableActions(ActionTypes.ActionType actionType)
        {
            List<Action> availableActions = new List<Action>();

            foreach (Action action in Characters[currentCharacterIndex].CharacterStats.Actions)
            {
                if (action.ActionType == actionType)
                {
                    availableActions.Add(action);
                }
            }

            return availableActions;
        }

    }
}
