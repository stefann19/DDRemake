using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DDRemakeProject.GamePlay.New.Character;
using DDRemakeProject.ViewModels;

namespace DDRemakeProject.Views
{
    /// <summary>
    /// Interaction logic for LevelSelectionView.xaml
    /// </summary>
    public partial class LevelSelectionView : UserControl
    {
        public static readonly DependencyProperty CharacterProperty = DependencyProperty.Register("Character", typeof(Character), typeof(LevelSelectionView), new PropertyMetadata(default(Character)));

        public LevelSelectionView()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
/*
            ((LevelSelectionViewModel)DataContext).LevelValue = CharacterLevel;
*/
        }



        public Character Character {
            get { return (Character) GetValue(CharacterProperty); }
            set { SetValue(CharacterProperty, value); }
        }

        private void LevelSelectionView_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextValueArrowsSelect.DataContext = new LevelSelectionViewModel();
        }
    }
}
