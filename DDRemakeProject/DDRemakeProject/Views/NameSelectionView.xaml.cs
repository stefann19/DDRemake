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

namespace DDRemakeProject.Views
{
    /// <summary>
    /// Interaction logic for NameSelectionView.xaml
    /// </summary>
    public partial class NameSelectionView : UserControl
    {
        public static readonly DependencyProperty CharacterNameProperty = DependencyProperty.Register("CharacterName", typeof(string), typeof(NameSelectionView), new PropertyMetadata(default(string)));

        public NameSelectionView()
        {
            InitializeComponent();
        }


        public string CharacterName {
            get { return (string) GetValue(CharacterNameProperty); }
            set { SetValue(CharacterNameProperty, value); }
        }
    }
}
