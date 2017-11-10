using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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
using WpfAnimatedGif;
using Image = System.Drawing.Image;

namespace DDRemakeProject
{
    /// <summary>
    /// Interaction logic for BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window
    {
        public BattleWindow()
        {
            InitializeComponent();
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        /// <summary>
        /// is on worker thread :)
        /// </summary>
        public void InitializeBattleUI(List<Character> allyCharacters, List<Character> enemyCharacters)
        {
            #region foreach character
            for (int i = 0; i < allyCharacters.Count; i++)
            {
                //set battle image
                string file = System.IO.Path.Combine(Environment.CurrentDirectory, allyCharacters[i].CharacterPng);
                ImageBehavior.SetAnimatedSource((this.FindName("BChar" + (i + 1)) as System.Windows.Controls.Image), new BitmapImage(new Uri(file)));
                
                //set icon image
                file = System.IO.Path.Combine(Environment.CurrentDirectory, allyCharacters[i].CharacterIconPng);
                (this.FindName("Icon" + i) as System.Windows.Controls.ContentControl).Resources["Img"] =
                   new BitmapImage(new Uri(file));

                //set ico
            }

            for (int i = 0; i < enemyCharacters.Count; i++)
            {
                string file = System.IO.Path.Combine(Environment.CurrentDirectory, enemyCharacters[i].CharacterPng);

                ImageBehavior.SetAnimatedSource((this.FindName("BeChar" + (i + 1)) as System.Windows.Controls.Image), new BitmapImage(new Uri(file)));


                file = System.IO.Path.Combine(Environment.CurrentDirectory, enemyCharacters[i].CharacterIconPng);
                (this.FindName("Icon" +(allyCharacters.Count+i)) as System.Windows.Controls.ContentControl).Resources["Img"] =
                    new BitmapImage(new Uri(file));
            }
            #endregion


        }
    }
}
