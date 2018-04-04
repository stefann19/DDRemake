using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDRemakeProject.GamePlay.Old
{
    public class ActionGridController
    {
        public ActionGridController(Grid actionGrid)
        {
            ActionGrid = actionGrid;
            ActionGrid.Children.Clear();
            
        }

        public static Grid ActionGrid { get; set; }

        public static void Activate(ActionTypes.ActionType actionType, BattleEngine battleEngine)
        {
           /* Character currentCharacter = battleEngine.Characters[TurnSystem.CurrentCharIndex];
            ActionGrid.Children.Clear();

            List<Action> actions = currentCharacter.CharacterLogic.Actions
                .Where(action => action.ActionType == actionType).ToList();
            List<AnimatedButtonController> animatedButtonControllers = new List<AnimatedButtonController>();

            ActionGrid.Width = actions.Count * 40;
            int left = 0;
            actions.ForEach(action =>
            {
                AnimatedButton animatedButton = new AnimatedButton
                {
                    Width = 40,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(left, 0, 0, 0),
                    Button = {Width = 40},
                    Action = action,
                    BattleEngine = battleEngine
                };
                animatedButton.Button.PreviewMouseDown += InitiateFight;
                animatedButtonControllers.Add(new AnimatedButtonController(action.Icon, animatedButton,
                    action.ActionType, battleEngine));
                ActionGrid.Children.Add(animatedButton);
                left += 40;
            });*/
        }

        private static void InitiateFight(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            Grid grid = button?.Parent as Grid;
            UserControls.AnimatedButton animatedButton = grid?.Parent as UserControls.AnimatedButton;

            animatedButton?.BattleEngine.Fight(null, animatedButton.Action);
            animatedButton?.BattleEngine.SelectChar(animatedButton.BattleEngine.SelectedCharacter);
        }
    }
}