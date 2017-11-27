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
using System.Windows.Shapes;
using DDRemakeProject.GamePlay;

namespace DDRemakeProject
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<CharacterStats> allies = new List<CharacterStats>
            {
                new CharacterStats("Wolf", "../../Assets/char/paladin/idle.gif","../../Assets/char/paladin/faceset.png", 1, 5, 2, 4, 3),
                new CharacterStats("Alpha", "../../Assets/char/mage/idle.gif","../../Assets/char/mage/faceset.png", 1, 3, 5, 3, 2),
                new CharacterStats("Malachi", "../../Assets/char/paladin/idle.gif","../../Assets/char/paladin/faceset.png", 2, 2, 5, 3, 4)
            };
            List<CharacterStats> enemies = new List<CharacterStats>
            {
                new CharacterStats("Lizzy", "../../Assets/monster/dino/idle.gif","../../Assets/monster/dino/idle.gif", 1, 5, 2, 4, 3),
                new CharacterStats("Bats", "../../Assets/monster/bat/idle.gif","../../Assets/monster/bat/idle.gif", 1, 5, 3,4, 2),
                new CharacterStats("Bob", "../../Assets/monster/boar/idle.gif","../../Assets/monster/boar/idle.gif", 3, 2, 5,5, 4)
            };
            for (int i = 0; i < allies.Count; i++)
            {
                allies[i].UiImageName = "BChar" + (i+1);
                enemies[i].UiImageName = "BeChar" + (i + 1);
            }

            BattleEngine be = new BattleEngine(allies, enemies);
            BattleWindow window = be.BWindow;
            //NewMapMenu window = new NewMapMenu();
            window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadMapMenu window = new LoadMapMenu();
            window.Show();
            this.Close();
        }
    }
}
