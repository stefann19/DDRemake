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
using DDRemakeProject.SerializingHelpers;
using DDRemakeProject.World;
using DDRemakeProject.WorldGeneration;

namespace DDRemakeProject.UserControls
{
    /// <summary>
    /// Interaction logic for MapButton.xaml
    /// </summary>
    public partial class MapButton : UserControl
    {
        public MapBasicInfo Map { get; set; }
        public int Order { get; set; }
        public LoadMapMenu LoadMapMenu { get; set; }
        public MapButton(MapBasicInfo mapBasicInfo,int order,LoadMapMenu mapMenu)
        {
            InitializeComponent();
            Map = mapBasicInfo;
            Order = order;
            LoadMapMenu = mapMenu;
            IntialiseButton();



        }

        private void IntialiseButton()
        {
            Button.Content = Map.Name + $"({Map.Size.Width},{Map.Size.Height})";

            DateTime lastModified = System.IO.File.GetLastWriteTime(JsonSerializer.GetMapInfoPath(Map.Name));


            Label.Content = "Last modified date: " + lastModified.ToString("dd/MM/yy HH:mm:ss");
            this.Margin = new Thickness(50, 20 + Order, 0, 0);
            this.Width = 20 + Button.Content.ToString().Length * 5.5f;
            this.Height = 30;
            Label.Margin = new Thickness(60 + this.Width, 20 + Order, 0, 0);
            Order += 40;
            Button.Click += LoadMap;

        }

        private void LoadMap(object sender, RoutedEventArgs e)
        {  
            MapWindow window = new MapWindow(true, Map);

            window.Show();
            LoadMapMenu.Close();
        }
    }
}
