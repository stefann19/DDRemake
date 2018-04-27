using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DDRemakeProject.GamePlay.New.Character;
using DDRemakeProject.GamePlay.New.Character.Logic;
using MahApps.Metro.Controls;

namespace DDRemakeProject.UserControls
{
    /// <summary>
    /// Interaction logic for CharacterCreationSubMenu.xaml
    /// </summary>
    public partial class CharacterCreationSubMenu : UserControl
    {
        public static readonly DependencyProperty RaceProperty = DependencyProperty.Register("Race", typeof(object), typeof(CharacterCreationSubMenu), new PropertyMetadata(Races.Dino));
        public static readonly DependencyProperty CharacterProperty = DependencyProperty.Register("Character", typeof(Character), typeof(CharacterCreationSubMenu), new PropertyMetadata(default(Character)));

        public CharacterCreationSubMenu()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public object Race {
            get {
                TextWithArrows textWithArrows = this.GetChildObjects().Where(child => child is TextWithArrows)
                    .FirstOrDefault(child => (child as TextWithArrows).Typee == BasicPropType.Race) as TextWithArrows; 
                return DDRemakeProject.GamePlay.New.Character.Logic.Race.GetRaceFromEnum((Races) Enum.Parse(typeof(Races), textWithArrows.Value as string)).AvatarPath ;
            }
        }

        public Character Character {
            get { return (Character) GetValue(CharacterProperty); }
            set { SetValue(CharacterProperty, value); }
        }
    }
}
