using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DDRemakeProject.UserControls;
using DDRemakeProject.World;
using Newtonsoft.Json;
using JsonSerializer = DDRemakeProject.SerializingHelpers.JsonSerializer;

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
            /*string[] files = Directory.GetFiles(@"Maps\")
                              .Where(p => p.EndsWith(".json"))
                              .ToArray();
            List<MapBasicInfo> list = new List<MapBasicInfo>();
            foreach (string s in files)
            {
                string namePlusDotJson = s.Split('\\').Last();
                string name = namePlusDotJson.Split('(').First();
                string sizeString = namePlusDotJson.Split('(')[1].Replace(").json","");
                Size size = new Size(int.Parse(sizeString.Split(',').First()), int.Parse(sizeString.Split(',').Last()));
                list.Add(new MapBasicInfo(name, size));
            }*/
            List<MapBasicInfo> list =Directory.GetFiles(@"Maps\","*",SearchOption.AllDirectories).Where(f=> f.EndsWith("Info.json")).Select(path=>JsonConvert.DeserializeObject<MapBasicInfo>(File.ReadAllText(path))).ToList();
/*
            list.Add(JsonConvert.DeserializeObject<MapBasicInfo>(File.ReadAllText()));
*/
            return list;
        }
        private void MakeButtonsForMaps(List<MapBasicInfo> list)
        {
            int y = 0;
            foreach (MapBasicInfo map in list)
            {
/*
                DateTime lastModified = System.IO.File.GetLastWriteTime(@"Maps\"+map.Name+".json");
*/


                /*Button b = new Button();
                Label l = new Label();
                b.Content = map.Name +$"({map.Size.Width},{map.Size.Height})";
                l.Content = "Last modified date: "+lastModified.ToString("dd/MM/yy HH:mm:ss");
                b.Margin = new Thickness(50, 20 + y, 0, 0);
                b.Width = 20+ b.Content.ToString().Length * 5.5f;
                b.Height = 30;
                l.Margin = new Thickness(60+b.Width, 20 + y, 0, 0);
                y += 40;
                b.Click += LoadMap;
                canvas.Children.Add(b);
                canvas.Children.Add(l);
                */

                MapButton button = new MapButton(map,y+=40,this);
                y += 40;
                canvas.Children.Add(button);
            }
        }
        private void LoadMap(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string sizeString = b.Content.ToString().Split('(')[1].Replace(")", "");
            Size size = new Size(int.Parse(sizeString.Split(',').First()), int.Parse(sizeString.Split(',').Last()));
            MapWindow window = new MapWindow(true, new MapBasicInfo(b.Content.ToString(), size));
            window.Show();
            this.Close();
        }
    }
}
