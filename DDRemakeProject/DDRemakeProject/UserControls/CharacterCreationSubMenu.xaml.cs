using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DDRemakeProject.GamePlay.New.Character;
using DDRemakeProject.GamePlay.New.Character.Logic;
using DDRemakeProject.ViewModels;
using MahApps.Metro.Controls;

namespace DDRemakeProject.UserControls
{
    /// <summary>
    /// Interaction logic for CharacterCreationSubMenu.xaml
    /// </summary>
    public partial class CharacterCreationSubMenu : UserControl
    {
        public static readonly DependencyProperty RaceProperty = DependencyProperty.Register("Race", typeof(Race), typeof(CharacterCreationSubMenu), new PropertyMetadata(default(Race)));
        public static readonly DependencyProperty CharacterProperty = DependencyProperty.Register("Character", typeof(Character), typeof(CharacterCreationSubMenu), new PropertyMetadata(default(Character)));

        public CharacterCreationSubMenu()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public Race Race
        {
            get { return (Race)GetValue(RaceProperty); }
            set { SetValue(RaceProperty, value); }
        }

        public Character Character {
            get { return (Character) GetValue(CharacterProperty); }
            set { SetValue(CharacterProperty, value); }
        }

        //private void CharacterCreationSubMenu_OnLoaded(object sender, RoutedEventArgs e)
        //{
        //    LayoutRoot.DataContext = new CharacterCreationSubViewModel(Character);
        //}
    }
}
