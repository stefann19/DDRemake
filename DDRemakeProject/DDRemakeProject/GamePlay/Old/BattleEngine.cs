using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DDRemakeProject.GamePlay.New;

namespace DDRemakeProject.GamePlay.Old
{
    public class BattleEngine
    {
        public List<Character> Characters { get; }

        public static BattleWindow BattleWindowUi { get; set; }


        public CharacterMiniWindow CurrentCharacterMiniWindow { get; set; }
        public CharacterMiniWindow SelectedCharacterMiniWindow { get; set; }

        public HashSet<AnimatedButtonPopUp> AnimatedButtonPopUps { get; set; }

        public Character SelectedCharacter
        {
            get => SelectedCharacter1;
            set
            {
                SelectedCharacter1 = value;
                SelectChar(SelectedCharacter1);
            }
        }

        public Character SelectedCharacter1 { get; set; }

        public ActionGridController ActionGrid { get; set; }

        public static bool WaitForAnimation { get; set; }

        public static Character WeakestCharacter { get; set; }

        public BattleEngine(IEnumerable<CharacterLogic> allyCharacters, IEnumerable<CharacterLogic> enemyCharacters)
        {
            WaitForAnimation = false;
            BattleWindowUi = new BattleWindow(this);
            ActionGrid = new ActionGridController(BattleWindowUi.ActionGrid);
            //GenerateMap();

            Characters = new List<Character>();
            foreach (CharacterLogic characterStats in allyCharacters)
                Characters.Add(new Character(characterStats, CharacterTypes.Type.Ally, this));
            foreach (CharacterLogic characterStats in enemyCharacters)
                Characters.Add(new Character(characterStats, CharacterTypes.Type.Enemy, this));
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
                new AnimatedButtonPopUp(new AnimatedButtonController(ButtonStatesList.Attack, BattleWindowUi.Attack,
                    ActionTypes.ActionType.Attack, this)),
                new AnimatedButtonPopUp(new AnimatedButtonController(ButtonStatesList.Spell, BattleWindowUi.Spell,
                    ActionTypes.ActionType.Spell, this)),
                new AnimatedButtonPopUp(new AnimatedButtonController(ButtonStatesList.Defend, BattleWindowUi.Defend,
                    ActionTypes.ActionType.Defence, this)),
                new AnimatedButtonPopUp(new AnimatedButtonController(ButtonStatesList.Inventory, BattleWindowUi.Item,
                    ActionTypes.ActionType.Item, this))
            };
            //AnimatedButtonControllers.Add(new AnimatedButtonController("", "", "", BattleWindowUi.Defend));
            //AnimatedButtonControllers.Add(new AnimatedButtonController("", "", "", BattleWindowUi.Spell));
            //AnimatedButtonControllers.Add(new AnimatedButtonController("", "", "", BattleWindowUi.Item));
        }

        public void SetNextTurn()
        {
            Characters[TurnSystem.CurrentCharIndex].CharacterUiControl.ScalingType = CharacterUi.ScaleType.NoScaling;

            TurnSystem.GetNextTurn(Characters);

            CurrentCharacterMiniWindow.SetStats(Characters[TurnSystem.CurrentCharIndex].CharacterLogic);
            CurrentCharacterMiniWindow.SelectType = "Current Turn";


            Characters[TurnSystem.CurrentCharIndex].CharacterUiControl.ScalingType = CharacterUi.ScaleType.Selected;

           /* if (Characters[TurnSystem.CurrentCharIndex].Type == CharacterTypes.Type.Enemy)
            {
                WeakestCharacter = ChooseWeakestAllyTarget();
                Fight(ChooseWeakestAllyTarget(),
                    Characters[TurnSystem.CurrentCharIndex].CharacterLogic.Actions.First());
            }*/
        }


        public void ReloadChar()
        {
            CharacterLogic ch = Characters[TurnSystem.CurrentCharIndex].CharacterLogic;
            CurrentCharacterMiniWindow.SetStats(ch);
        }


        public void SelectChar(Character character)

        {
            if (character == null) return;
            SelectedCharacterMiniWindow.SetVisibility(Visibility.Visible);
            SelectedCharacterMiniWindow.SetStats(character.CharacterLogic);
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
                       ch.Type == CharacterTypes.Type.Ally && ch.Status == CharacterTypes.Status.Alive) == 0
                ? null
                : Characters
                    .Where(character =>
                        character.Type == CharacterTypes.Type.Ally && character.Status == CharacterTypes.Status.Alive)
                    .Aggregate((a, b) => a.CharacterLogic.Stats.Health.CurrentValue < b.CharacterLogic.Stats.Health.CurrentValue ? a : b);
        }

        public void AttackFromTrigger()
        {
            Fight(WeakestCharacter, Characters[TurnSystem.CurrentCharIndex].CharacterLogic.Actions.First());
        }


        public void Fight(Character defenderCharacter, Action skill)
        {
            if (defenderCharacter == null) defenderCharacter = SelectedCharacter;

            if (defenderCharacter == null) return;

            if (skill.ApCost <= Characters[TurnSystem.CurrentCharIndex].CharacterLogic.Stats.Stamina.CurrentValue)
            {
                // execute the attack animation in the UI
                //skill.SkillEffect;

                SkillEffects skillEffect = new SkillEffects(skill.Effect, this)
                {
                    GifImage = {Margin = defenderCharacter.CharacterUiControl.CharAvatarControl.Margin}
                };


                //execute the hp/ap drain animation in the UI


                //Execute the stats changes of the fight\
                DoFightStatChanges(Characters[TurnSystem.CurrentCharIndex].CharacterLogic, defenderCharacter, skill);
            }
            WeakestCharacter = null;
            //SetNextTurn();
        }

        private void DoFightStatChanges(CharacterLogic attackerCharacter, Character defenderCharacter, Action skill)
        {
            int damageAfterReductions = (int)defenderCharacter.CharacterLogic.Resistances.Armour.Calculate(skill.Damage);
/*
                (int) StatsLogic.CalculateDamageAfterAr(defenderCharacter.CharacterLogic.Resistances.Armour.Calculate, skill.Damage);
*/
            defenderCharacter.CharacterLogic.Stats.Health.CurrentValue -= damageAfterReductions;
            attackerCharacter.Stats.Stamina.CurrentValue -= skill.ApCost;
            attackerCharacter.Stats.Mana.CurrentValue -= skill.MpCost;
            CheckIfDead(defenderCharacter);
        }

        private bool CheckIfDead(Character ch)
        {
            if (ch.CharacterLogic.Stats.Health.CurrentValue <= 0) //Declare dead
            {
                //Execute Cleanup function
                DeadCleanup(ch);

                return true;
            }
            return false;
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
            return Characters[TurnSystem.CurrentCharIndex].CharacterLogic.Actions
                .Where(action => action.ActionType == actionType).ToList();
        }
    }
}