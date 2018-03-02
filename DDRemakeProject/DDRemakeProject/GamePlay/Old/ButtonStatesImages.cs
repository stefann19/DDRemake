namespace DDRemakeProject.GamePlay.Old
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
    }
}