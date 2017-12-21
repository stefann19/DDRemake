namespace DDRemakeProject.GamePlay
{
    public class AnimatedButtonPopUp
    {
        public AnimatedButtonController AnimatedButtonController { get; set; }


        public AnimatedButtonPopUp(AnimatedButtonController animatedButtonController)
        {
            AnimatedButtonController = animatedButtonController;

            animatedButtonController.AnimatedButton.Button.MouseEnter += Button_MouseEnter; ;
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ActionTypes.ActionType actionType = ActionTypes.ActionType.Attack;
            ActionGridController.Activate(actionType, AnimatedButtonController.BattleEngine);
        }


    }
}
