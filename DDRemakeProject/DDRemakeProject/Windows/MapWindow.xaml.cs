using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DDRemakeProject.GamePlay.Old;
using DDRemakeProject.SerializingHelpers;
using DDRemakeProject.World;
using DDRemakeProject.WorldGeneration;
using Newtonsoft.Json;
using JsonSerializer = DDRemakeProject.SerializingHelpers.JsonSerializer;

namespace DDRemakeProject
{
    /// <summary>
    /// Interaction logic for MapWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        public static Canvas BackgroundCanvas;
        public static Canvas DynamicCanvas;
        public static Canvas MapCanvas;

        /*
                private readonly BackgroundWorker _worker = new BackgroundWorker();
        */
        public string Titlee;
        private readonly bool _loadFromFile;
        public static MapBasicInfo MapBasicInfo;
        public WorldGeneration.Generator Generator { get; set; }
        public MapWindow(bool loadFromFile, MapBasicInfo map)
        {
            InitializeComponent();
            this._loadFromFile = loadFromFile;
            BackgroundCanvas = Canvas_Background;
            DynamicCanvas = Canvas_Dynamic;
            MapCanvas = Canvas_MiniMap;
            //canvas.Background = Brushes.Black;
            Titlee = this.Title;
            MapBasicInfo = map;
/*
            _worker.DoWork += worker_DoWork;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            _worker.RunWorkerAsync();*/

            //Canvas.MouseWheel += new MouseWheelEventHandler(mainWindow_MouseWheel);

            Closing += OnWindowClosing;
            DoShit();

            Canvas.Width = map.Size.Width * Constants.TilePx;
            Canvas.Height = map.Size.Height * Constants.TilePx;

            DynamicCanvas.Width = Canvas.Width;
            DynamicCanvas.Height = Canvas.Height;

            BackgroundCanvas.Width = Canvas.Width;
            BackgroundCanvas.Height = Canvas.Height;
        }
        double _scale;

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            /* XmlToFile.WriteToXmlFile(@"Maps\" + MapBasicInfo.Name + ".xml", Generator);*/
            /*
                        MapBasicInfo.Name = MapBasicInfo.Name.Replace($"({Generator.Size.X},{Generator.Size.Y})", "");
            */
            System.IO.Directory.CreateDirectory(@"Maps\" + MapBasicInfo.Name);

            JsonSerializer.WriteToFile(JsonSerializer.GetGeneratorPath(MapBasicInfo.Name), Generator);
            JsonSerializer.WriteToFile(JsonSerializer.GetMapInfoPath(MapBasicInfo.Name), MapBasicInfo);
            JsonSerializer.WriteToFile(JsonSerializer.GetMinimapPath(MapBasicInfo.Name), new MinimapSerializer(_engine.MiniMap.Tiles));
            JsonSerializer.WriteToFile(JsonSerializer.GetPlayerPath(MapBasicInfo.Name), new PlayerSerializer(_engine.Player.Tile.Position));

            /*
                        JsonSerializer.WriteToFile(@"Maps\"+ MapBasicInfo.Name + @"\" + MapBasicInfo.Name+$"({Generator.Size.X},{Generator.Size.Y})" + ".json", Generator);
            */
            /*
                        JsonSerializer.WriteToFile(@"Maps\" + MapBasicInfo.Name + @"\" + MapBasicInfo.Name +$"({Generator.Size.X},{Generator.Size.Y})" + ".json", _engine);
            */

            // Handle closing logic, set e.Cancel as needed
        }

        private Engine _engine;
        private void DoShit()
        {
            if (!_loadFromFile)
            {

                GenerateMap(MapBasicInfo);

            }
            else
            {
                LoadMap(MapBasicInfo.Name);
                //DeleteCanvas();
            }

            //_v1.MoveMethod();
        }
        private void GenerateMap(MapBasicInfo map)
        {
            Generator = new WorldGeneration.Generator((Vector)map.Size);
            _engine = new Engine(this);
        }
        private void LoadMap(string mapName)
        {
            /*Generator = XmlToFile.ReadFromXmlFile<WorldGeneration.Generator>(@"Maps\" + mapName + ".xml");*/
/*
            Generator = JsonConvert.DeserializeObject<Generator>(File.ReadAllText(@"Maps\"+mapName.Split('(').First()+@"\" + mapName + ".json"));
*/
            Generator = JsonConvert.DeserializeObject<Generator>(File.ReadAllText(JsonSerializer.GetGeneratorPath(mapName)));
            Generator.Generate();

            _engine = new Engine(this);
            PlayerSerializer player = JsonConvert.DeserializeObject<PlayerSerializer>(File.ReadAllText(JsonSerializer.GetPlayerPath(mapName)));
            _engine.Player.Tile.Position = player.Position;
            MinimapSerializer minimap =  JsonConvert.DeserializeObject<MinimapSerializer>(
                File.ReadAllText(JsonSerializer.GetMinimapPath(mapName)));
            _engine.MiniMap.Tiles.UnionWith(minimap.MapShapes);
            _engine.MiniMap.Tiles.ToList().ForEach(tile=> tile.InitialiseRect((Vector) tile.Rect.Location, tile.Rect.Size));
            _engine.MiniMap.MovePlayer();
            _engine.Camera.CheatsActivated = true;
            _engine.Camera.CheatsActivated = false;

            /*_engine = new Engine(this);
            Engine old = JsonConvert.DeserializeObject<Engine>(File.ReadAllText(JsonSerializer.GetEnginePath(mapName)));
            _engine.Player = old.Player;*/
            /*_engine =
            _engine.Generator = Generator;*/
            /*
                        _engine = new Engine( this);
            */
        }

        private void DeleteCanvas()
        {
            Application.Current.Dispatcher.Invoke((System.Action)delegate
            {

                Canvas.Children.Clear();


            });
        }


        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //update ui once worker complete his work
        }
/*
        private void mainWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ZoomOnCanvas(Canvas, e);
            //ZoomOnCanvas(canvas2, e);

        }

        private void ZoomOnCanvas(Canvas c, MouseWheelEventArgs e)
        {
            var element = c as UIElement;

            var position = e.GetPosition(element);
            var transform = element.RenderTransform as MatrixTransform;
            var matrix = transform.Matrix;
            _scale = e.Delta >= 0 ? 1.1 : (1.0 / 1.1); // choose appropriate scaling factor

            matrix.ScaleAtPrepend(_scale, _scale, position.X, position.Y);

            element.RenderTransform = new MatrixTransform(matrix);
        }

        bool _c1;
        Point _lastMousePos;
        private void mainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            DragCanvas(Canvas, e);
            //DragCanvas(canvas2, e);

        }

        private void DragCanvas(Canvas c, MouseEventArgs e)
        {
            var element = c as UIElement;

            Point position = e.GetPosition(element);
            var transform = element.RenderTransform as MatrixTransform;
            var matrix = transform.Matrix;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_c1)
                {
                    if (position != _lastMousePos)
                    {
                        matrix.TranslatePrepend(+position.X - _lastMousePos.X, position.Y - _lastMousePos.Y);
                        element.RenderTransform = new MatrixTransform(matrix);
                    }
                }
                else
                {
                    _c1 = false;
                }
            }
            else if (e.LeftButton == MouseButtonState.Released)
            {
                _lastMousePos = position;
                _c1 = true;
            }
        }
        */
    }
}