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

namespace DDRemakeProject
{
    /// <summary>
    /// Interaction logic for NewMapMenu.xaml
    /// </summary>
    public partial class NewMapMenu : Window
    {
        public NewMapMenu()
        {
            InitializeComponent();
            textBoxWidth.Text = "50";
            textBoxHeight.Text = "50";
            textBoxName.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text.Length > 0)
            {
                MapBasicInfo map = new MapBasicInfo(int.Parse(textBoxWidth.Text), int.Parse(textBoxHeight.Text), textBoxName.Text);
                MainWindow window = new MainWindow(false, map);
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Map name is mandatory ...");
            }
        }
    }
}
