using System.Collections.Generic;
using System.Windows;
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
<<<<<<< HEAD
            //List<CharacterStats> allies = new List<CharacterStats>
            //{
            //    new CharacterStats("Wolf", "../../Assets/char/paladin/idle.gif","../../Assets/char/paladin/faceset.png", 1, 5, 2, 4, 3),
            //    new CharacterStats("Alpha", "../../Assets/char/mage/idle.gif","../../Assets/char/mage/faceset.png", 1, 3, 5, 3, 2),
            //    new CharacterStats("Malachi", "../../Assets/char/paladin/idle.gif","../../Assets/char/paladin/faceset.png", 2, 2, 5, 3, 4)
            //};
            //List<CharacterStats> enemies = new List<CharacterStats>
            //{
            //    new CharacterStats("Lizzy", "../../Assets/monster/dino/idle.gif","../../Assets/monster/dino/idle.gif", 2, 8, 4, 4, 3),
            //    new CharacterStats("Bats", "../../Assets/monster/bat/idle.gif","../../Assets/monster/bat/idle.gif", 6, 10, 3,4, 2),
            //    new CharacterStats("Bob", "../../Assets/monster/boar/idle.gif","../../Assets/monster/boar/idle.gif", 3, 2, 5,5, 4)
            //};
            //for (int i = 0; i < allies.Count; i++)
            //{
            //    allies[i].UiImageName = "BChar" + (i+1);
            //    enemies[i].UiImageName = "BeChar" + (i + 1);
            //}
=======
            List<CharacterStats> allies = new List<CharacterStats>
            {
                new CharacterStats("Wolf", "../../Assets/char/paladin/idle2.gif","../../Assets/char/paladin/faceset.png", 1, 5, 2, 4, 3),
                new CharacterStats("Alpha", "../../Assets/char/mage/idle2.gif","../../Assets/char/mage/faceset.png", 1, 3, 5, 3, 2),
                new CharacterStats("Malachi", "../../Assets/char/paladin/idle2.gif","../../Assets/char/paladin/faceset.png", 2, 2, 5, 3, 4)
            };
            List<CharacterStats> enemies = new List<CharacterStats>
            {
                new CharacterStats("Lizzy", "../../Assets/monster/dino/idle2.gif","../../Assets/monster/dino/idle.gif", 2, 8, 4, 4, 3),
                new CharacterStats("Bats", "../../Assets/monster/bat/idle2.gif","../../Assets/monster/bat/idle.gif", 6, 10, 3,4, 2),
                new CharacterStats("Bob", "../../Assets/monster/boar/idle2.gif","../../Assets/monster/boar/idle.gif", 3, 2, 5,5, 4)
            };
            for (int i = 0; i < allies.Count; i++)
            {
                allies[i].UiImageName = "BChar" + (i + 1);
                enemies[i].UiImageName = "BeChar" + (i + 1);
            }
>>>>>>> master

            //BattleEngine be = new BattleEngine(allies, enemies);
            //BattleWindow window = BattleEngine.BattleWindowUi;
            NewMapMenu window = new NewMapMenu();
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
