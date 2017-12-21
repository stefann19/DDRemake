using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DDRemakeProject.GamePlay
{
    public class BattleEngine
    {
        private Character _selectedCharacter;

        public List<Character> Characters { get; private set; }

        public static BattleWindow BattleWindowUi { get; set; }


        public CharacterMiniWindow CurrentCharacterMiniWindow { get; set; }
        public CharacterMiniWindow SelectedCharacterMiniWindow { get; set; }

        public HashSet<AnimatedButtonPopUp> AnimatedButtonPopUps { get; set; }

        private ActionGridController _actionGrid;

        public static bool WaitForAnimation;
        public static Character WeakestCharacter;

        public Character SelectedCharacter
        {
            get => _selectedCharacter;
            set
            {
                _selectedCharacter = value;
                SelectChar(_selectedCharacter);
            }
        }
        



        public BattleEngine(IEnumerable<CharacterStats> allyCharacters, IEnumerable<CharacterStats> enemyCharacters)
        {
            WaitForAnimation = false;
            BattleWindowUi = new BattleWindow(this);
            _actionGrid = new ActionGridController(BattleWindowUi.ActionGrid);
            GenerateMap();

            Characters = new List<Character>();
            foreach (CharacterStats characterStats in allyCharacters)
            {
                Characters.Add(new Character(characterStats, CharacterTypes.Type.Ally,this));
            }
            foreach (CharacterStats characterStats in enemyCharacters)
            {
                Characters.Add(new Character(characterStats, CharacterTypes.Type.Enemy,this));
            }
            InitialiseBottomRightMiniWindows();
            InitialiseButtons();
            //UiBackEndInstance.SetPopOutWindow(BWindow.PopOutWindow);
        }

        private void InitialiseBottomRightMiniWindows()
        {

            CurrentCharacterMiniWindow = new CharacterMiniWindow(BattleWindowUi.CurrentChar);
            SelectedCharacterMiniWindow = new CharacterMiniWindow(BattleWindowUi.TargetChar);
            
            SetNextTurn();

            // GenerateAttackOrder();
        }

        private void InitialiseButtons()
        {
            AnimatedButtonPopUps = new HashSet<AnimatedButtonPopUp>
            {
                new AnimatedButtonPopUp(new AnimatedButtonController(ButtonStatesList.Attack, BattleWindowUi.Attack,ActionTypes.ActionType.Attack,this))
            };
            //AnimatedButtonControllers.Add(new AnimatedButtonController("", "", "", BattleWindowUi.Defend));
            //AnimatedButtonControllers.Add(new AnimatedButtonController("", "", "", BattleWindowUi.Spell));
            //AnimatedButtonControllers.Add(new AnimatedButtonController("", "", "", BattleWindowUi.Item));


        }

        public void SetNextTurn()
        {

            Characters[TurnSystem.CurrentCharIndex].CharacterUiControl.ScalingType = CharacterUi.ScaleType.NoScaling;

            TurnSystem.GetNextTurn(Characters);

            CurrentCharacterMiniWindow.SetStats(Characters[TurnSystem.CurrentCharIndex].CharacterStats);
            CurrentCharacterMiniWindow.SelectType = "Current Turn";


            Characters[TurnSystem.CurrentCharIndex].CharacterUiControl.ScalingType = CharacterUi.ScaleType.Selected;

            if (Characters[TurnSystem.CurrentCharIndex].Type == CharacterTypes.Type.Enemy)
            {
                WeakestCharacter = ChooseWeakestAllyTarget();
                Fight(ChooseWeakestAllyTarget(),Characters[TurnSystem.CurrentCharIndex].CharacterStats.Actions.First());
            }

        }



        public void ReloadChar()
        {
            CharacterStats ch = Characters[TurnSystem.CurrentCharIndex].CharacterStats;
            CurrentCharacterMiniWindow.SetStats(ch);
        }


        public void SelectChar(Character character)

        {
            if (character == null) return;
            SelectedCharacterMiniWindow.SetVisibility(Visibility.Visible);
            SelectedCharacterMiniWindow.SetStats(character.CharacterStats);
            SelectedCharacterMiniWindow.SelectType = "Selected";
        }

        public void DeselectCHar()
        {
            SelectedCharacterMiniWindow.SetVisibility(Visibility.Hidden);
            SelectedCharacter = null;
        }




        private Character ChooseWeakestAllyTarget()
        {
            return Characters.Count(ch =>
                       ch.Type == CharacterTypes.Type.Ally && ch.Status == CharacterTypes.Status.Alive) == 0 ? null : Characters.Where(character => character.Type == CharacterTypes.Type.Ally && character.Status == CharacterTypes.Status.Alive).Aggregate((a,b)=> a.CharacterStats.CurrentHp< b.CharacterStats.CurrentHp ? a :b);
        }

        public void AttackFromTrigger()
        {
            
            Fight(WeakestCharacter, Characters[TurnSystem.CurrentCharIndex].CharacterStats.Actions.First());
        }
        

        public void Fight(Character defenderCharacter,Action skill)
        {
            if (defenderCharacter == null) defenderCharacter = SelectedCharacter;

            if(defenderCharacter == null)return;

            if (skill.ApCost <= Characters[TurnSystem.CurrentCharIndex].CharacterStats.CurrentAp)
            {

                // execute the attack animation in the UI
                //skill.SkillEffect;
                
                    SkillEffects skillEffect = new SkillEffects(skill.Effect, this)
                    {
                        GifImage = {Margin = defenderCharacter.CharacterUiControl.CharAvatarControl.Margin}
                    };
                


                //execute the hp/ap drain animation in the UI


                //Execute the stats changes of the fight\
                DoFightStatChanges(Characters[TurnSystem.CurrentCharIndex].CharacterStats, defenderCharacter, skill);
            }
            WeakestCharacter = null;
            //SetNextTurn();
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
            ch.CharacterDied();
            DeselectCHar();
            //ch.Status = CharacterTypes.Status.Dead;


        }

        public List<Action> GetAvailableActions(ActionTypes.ActionType actionType)
        {
            return Characters[TurnSystem.CurrentCharIndex].CharacterStats.Actions.Where(action => action.ActionType == actionType).ToList();
        }

    }
}
