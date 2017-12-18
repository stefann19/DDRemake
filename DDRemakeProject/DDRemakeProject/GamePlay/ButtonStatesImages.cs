namespace DDRemakeProject.GamePlay
{
    public class ButtonStatesImages
    {
        public string NormalStateImage { get; set; }
        public string HoverStateImage { get; set; }
        public string PressedStateImage { get; set; }

        public ButtonStatesImages(string normalStateImage, string hoverStateImage, string pressedStateImage)
        {
            NormalStateImage = normalStateImage;
            HoverStateImage = hoverStateImage;
            PressedStateImage = pressedStateImage;
        }



        //public static List<ButtonStatesImages> DefaultButtonStateImages = new List<ButtonStatesImages>
        //{
        //    new ButtonStatesImages("../../Assets/HUD/Attack.png", "../../Assets/HUD/Attack2.png", "../../Assets/HUD/Attack3.png")
        //};
        //public enum DefaultButtonStatesBinding :ButtonStatesImages
        //{
        //    AttackPopUp =0,
        //    LowAttack =0,
        //    MediumAttack =0
        //}
    }
}
