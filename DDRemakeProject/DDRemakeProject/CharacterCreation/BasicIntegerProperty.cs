using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using DDRemakeProject.Annotations;
using DDRemakeProject.UserControls;

namespace DDRemakeProject.CharacterCreation
{
    public class BasicIntegerProperty :BasicProperty
    {
        public TextWithArrows TextWithArrows { get; set; }

        public BasicIntegerProperty(TextWithArrows textWithArrows)
        {
            TextWithArrows = textWithArrows;
        }

        public string Title => TextWithArrows.Name;
        public int Value {
            get=>int.Parse(TextWithArrows.Value as string);
            set => TextWithArrows.Value = value.ToString();
        }

        public void RaiseValue()
        {
            ++Value;
        }
        public void LowerValue()
        {
            --Value;
        }
    }
}