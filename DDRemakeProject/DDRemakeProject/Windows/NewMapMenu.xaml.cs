using System.Windows;

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
            textBoxWidth.Text = "100";
            textBoxHeight.Text = "100";
            textBoxName.Text = "A";
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
