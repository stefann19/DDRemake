using DDRemakeProject.UserControls;

namespace DDRemakeProject.CharacterCreation
{
    public interface BasicProperty
    {
        TextWithArrows TextWithArrows { get; set; }

/*
        BasicProperty(TextWithArrows textWithArrows)
        {
            TextWithArrows = textWithArrows;
        }
*/



        void RaiseValue();

        void LowerValue();
    }
}