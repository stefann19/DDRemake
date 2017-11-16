using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DDRemakeProject.GamePlay;
using WpfAnimatedGif;
using Image = System.Drawing.Image;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Mvvm;


namespace DDRemakeProject
{

    //public class Questionnaire : BindableBase
    //{
    //    private string favoriteColor;
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    public string FavoriteColor
    //    {
    //        get

    //        {

    //            return favoriteColor;

    //        }

    //        set

    //        {

    //            favoriteColor = value;

    //            OnPropertyChanged("FavoriteColor");

    //        }
    //    }
    //}

    public class PercentageConverter : MarkupExtension, IValueConverter
    {
        private static PercentageConverter _instance;

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value) * System.Convert.ToDouble(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new PercentageConverter());
        }
    }

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
        private string _message;

        public void InitializeBattleUI(List<Character> allyCharacters, List<Character> enemyCharacters)
        {
            Grid re = Icon0.FindName("gri") as Grid;

            re.Width = 10;
            #region foreach character
            for (int i = 0; i < allyCharacters.Count; i++)
            {
                //set battle image
                string file = System.IO.Path.Combine(Environment.CurrentDirectory, allyCharacters[i].CharacterPng);
                ImageBehavior.SetAnimatedSource((this.FindName("BChar" + (i + 1)) as System.Windows.Controls.Image), new BitmapImage(new Uri(file)));
                
                //set icon image
                file = System.IO.Path.Combine(Environment.CurrentDirectory, allyCharacters[i].CharacterIconPng);
                (this.FindName("Icon" + i) as System.Windows.Controls.ContentControl).Resources["Img"] =new BitmapImage(new Uri(file));

                //redBarSize

                 

                //IEnumerable<ProgressBar> collection = Icon0.FindName("progressBar");
                //foreach (var progressBar in collection)
                //{
                //    progressBar.Value = 50;
                //}
                //(Icon0.FindName("rectangle") as Rectangle).Width = 20;
                //((allyCharacters[i].CurrentHp*100 / allyCharacters[i].Hp)).ToString()

                //Questionnaire q = new Questionnaire();
                //q.FavoriteColor = "20";
                //favoriteColor = 20;
                //(this.FindName("Icon" + i) as System.Windows.Controls.ContentControl).Resources["redBarSize"] = 20;//77 is the hp bar length in pixels;
                //set ico
            }

            for (int i = 0; i < enemyCharacters.Count; i++)
            {
                string file = System.IO.Path.Combine(Environment.CurrentDirectory, enemyCharacters[i].CharacterPng);

                ImageBehavior.SetAnimatedSource((this.FindName("BeChar" + (i + 1)) as System.Windows.Controls.Image), new BitmapImage(new Uri(file)));


                file = System.IO.Path.Combine(Environment.CurrentDirectory, enemyCharacters[i].CharacterIconPng);
                (this.FindName("Icon" +(allyCharacters.Count+i)) as System.Windows.Controls.ContentControl).Resources["Img"] = new BitmapImage(new Uri(file));
               // (this.FindName("Icon" + (allyCharacters.Count + i)) as System.Windows.Controls.ContentControl).Resources["redBarSize"] = ((allyCharacters[i].CurrentHp / allyCharacters[i].Hp) * 77).ToString();//77 is the hp bar length in pixels;

            }
            #endregion


        }
    }
}
