using System.IO;
using System.Windows;
using DDRemakeProject.GamePlay.New;
using DDRemakeProject.GamePlay.New.Character.Logic;
using DDRemakeProject.GamePlay.Old;
using Newtonsoft.Json;

namespace DDRemakeProject
{
    /// <summary>
    ///     Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*List<CharacterStats> allies = new List<CharacterStats>
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

           BattleEngine be = new BattleEngine(allies, enemies);
           BattleWindow window = BattleEngine.BattleWindowUi;*/

            Race dino = new Race(name: Races.Dino, iconPath: "../../Assets/monster/dino/idle2.gif",
                avatarPath: "../../Assets/monster/dino/idle.gif", strengthGrowth: 3, agilityGrowth: 1, intelligenceGrowth: 2, enduranceGrowth: 4,
                armourResistanceFactors: new Race.Resist(flat: 0.2, percentage: 0.3), airResistanceFactors: new Race.Resist(flat: 0.2, percentage: 0.3), fireArmourResistanceFactors: new Race.Resist(flat: 0.4, percentage: 0.4),
                waterResistanceFactors: new Race.Resist(flat: 0.4, percentage: 0.3),
                earthResistanceFactors: new Race.Resist(flat: 0.2, percentage: 0.3),
                armourAttackFormulaString: "(traits.Strength * 5+ traits.Agility * 6 )", fireAttackFormulaString: "(traits.Strength * 5 + traits.Agility * 6)",
                waterAttackFormulaString: "(traits.Strength * 5 + traits.Agility * 6)", airAttackFormulaString: "( traits.Strength * 5 + traits.Agility * 6)",
                earthAttackFormulaString: "( traits.Strength * 5 + traits.Agility * 6)");
            string te = JsonConvert.SerializeObject(value: dino, formatting: Formatting.Indented);

            File.WriteAllText(path: Race.GetRaceLocation(race: dino.Name), contents: te);

            dino = JsonConvert.DeserializeObject<Race>(value: File.ReadAllText(path: Race.GetRaceLocation(race: dino.Name)));

            Character ch =
                new Character(
                    characterLogic: new CharacterLogic(race: Races.Dino,level: 10, agility: 10, endurance: 10, strength: 10, intelligence: 10),
                    type: CharacterTypes.Type.Ally, battleEngine: null);


            Race dinoCon = JsonConvert.DeserializeObject<Race>(value: te);


            NewMapMenu window = new NewMapMenu();
            window.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadMapMenu window = new LoadMapMenu();
            window.Show();
            Close();
        }

        /*List<CharacterStats> allies = new List<CharacterStats>
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

           BattleEngine be = new BattleEngine(allies, enemies);
           BattleWindow window = BattleEngine.BattleWindowUi;*/
    }
}