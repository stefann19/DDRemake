namespace DDRemakeProject.GamePlay
{
    public class AnimatedButtonPopUp
    {
        public AnimatedButtonController AnimatedButtonController { get; set; }


        public AnimatedButtonPopUp(AnimatedButtonController animatedButtonController)
        {
            AnimatedButtonController = animatedButtonController;

            //animatedButtonController.AnimatedButton.Button.
            animatedButtonController.AnimatedButton.Button.MouseEnter += Button_MouseEnter; ;
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ActionTypes.ActionType actionType = ActionTypes.ActionType.Attack;
            ActionGridController.Activate(actionType, AnimatedButtonController._battleEngine);
        }

        //private void Button_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    ActionTypes.ActionType actionType = ActionTypes.ActionType.Attack ;
        //    ActionGridController.Activate(actionType, AnimatedButtonController._battleEngine);
        //}

    }
}
