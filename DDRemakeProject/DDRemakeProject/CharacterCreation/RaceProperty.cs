using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using DDRemakeProject.GamePlay.New.Character.Logic;
using DDRemakeProject.UserControls;
using MahApps.Metro.Controls;

namespace DDRemakeProject.CharacterCreation
{
    public class RaceProperty : BasicProperty
    {
        public TextWithArrows TextWithArrows { get; set; }

        public RaceProperty(TextWithArrows textWithArrows)
        {
            TextWithArrows = textWithArrows;
        }
        private int RacesLength => Enum.GetNames(typeof(Races)).Length;
        public string Title => TextWithArrows.Name;
        public Races Value
        {
            get => (Races) Enum.Parse(typeof(Races), TextWithArrows.Value as string) ;
            set => TextWithArrows.Value = value;
        }

        public void RaiseValue()
        {
            Value = (Races)Mod((int)Value+1);
            UpdateRaceUI();
        }
        public void LowerValue()
        {
            Value = (Races)Mod((int)Value -1);
            UpdateRaceUI();
        }

        
        private void UpdateRaceUI()
        {
            ImageSource raceAvatar = Race.GetRaceFromEnum(Value).Avatar;

            Canvas parentCanvas = null;
            FrameworkElement element = TextWithArrows;
            while (parentCanvas==null)
            {
                if(element==null)return;
                if (element.Parent as Canvas != null)
                {
                    parentCanvas = (Canvas)element.Parent;
/*                    if (parentCanvas.Resources["Race"]==null)
                        parentCanvas = null;*/
                }

                else element = element.Parent as FrameworkElement;
            }

            parentCanvas.Resources["Race"] = raceAvatar;
        }

        private int Mod(int x)=>(x % RacesLength + RacesLength) % RacesLength;
    }
}