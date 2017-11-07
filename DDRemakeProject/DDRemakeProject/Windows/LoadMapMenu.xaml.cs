using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for LoadMapMenu.xaml
    /// </summary>
    public partial class LoadMapMenu : Window
    {
        public LoadMapMenu()
        {
            InitializeComponent();
            MakeButtonsForMaps(GetMapsFromFile());
            
        }
        private MapBasicInfo _selectedMap;
        private List<MapBasicInfo> GetMapsFromFile()
        {
            string[] files = Directory.GetFiles(@"C:\VisualStudioProjects\DDRemake\DDRemakeProject\DDRemakeProject\")
                              .Where(p => p.EndsWith(".xml"))
                              .ToArray();
            List<MapBasicInfo> list = new List<MapBasicInfo>();
            foreach (string s in files)
            {
                string namePlusDotXml = s.Split('\\').Last();
                list.Add(new MapBasicInfo(0, 0, namePlusDotXml.Substring(0, namePlusDotXml.Length - 4)));
            }
            return list;
        }
        private void MakeButtonsForMaps(List<MapBasicInfo> list)
        {
            int y = 0;
            foreach (MapBasicInfo map in list)
            {
                DateTime lastModified = System.IO.File.GetLastWriteTime(@"C:\VisualStudioProjects\DDRemake\DDRemakeProject\DDRemakeProject\"+map.Name+".xml");

                Button b = new Button();
                Label l = new Label();
                b.Content = map.Name;
                l.Content = "Last modified date: "+lastModified.ToString("dd/MM/yy HH:mm:ss");
                b.Margin = new Thickness(50, 20 + y, 0, 0);
                b.Width = 20+ b.Content.ToString().Length * 5.5f;
                b.Height = 30;
                l.Margin = new Thickness(60+b.Width, 20 + y, 0, 0);
                y += 40;
                b.Click += LoadMap;
                canvas.Children.Add(b);
                canvas.Children.Add(l);
                
            }
        }
        private void LoadMap(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            MainWindow window = new MainWindow(true, new MapBasicInfo(0, 0, b.Content.ToString()));
            window.Show();
            this.Close();
        }
    }
}
