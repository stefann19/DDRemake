using System.Windows;
using DDRemakeProject.World;

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

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text.Length > 0)
            {
                Size mapSize = new Size(int.Parse(textBoxWidth.Text), int.Parse(textBoxHeight.Text));
                MapBasicInfo map = new MapBasicInfo(textBoxName.Text,mapSize);
                MapWindow window = new MapWindow(false, map);
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
