namespace DDRemakeProject.GamePlay.Old
{
    public class AnimatedButtonPopUp
    {
        public AnimatedButtonController AnimatedButtonController { get; set; }


        public AnimatedButtonPopUp(AnimatedButtonController animatedButtonController)
        {
            AnimatedButtonController = animatedButtonController;

            animatedButtonController.AnimatedButton.Button.PreviewMouseDown += Button_MouseDown; 
        }

        private void Button_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ActionTypes.ActionType actionType = AnimatedButtonController.ActionType;
            ActionGridController.Activate(actionType, AnimatedButtonController.BattleEngine);
        }


    }
}
