using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DDRemakeProject.GamePlay;

namespace DDRemakeProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Canvas CanvasS1;
        public static Canvas CanvasS2;
        public static Rect Size;
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        public string Titlee;
        private readonly bool _loadFromFile;
        private readonly MapBasicInfo _map;
        private WorldGeneration.GeneratorV1 _generator;
        public MainWindow(bool loadFromFile,MapBasicInfo map)
        {
            InitializeComponent();
            this._loadFromFile = loadFromFile;            
            CanvasS1 = canvas3;
            CanvasS2 = canvas2;
            Size = new Rect(new Point(),new Size(canvas3.Width/Constants.TilePx,canvas3.Height / Constants.TilePx));
            //canvas.Background = Brushes.Black;
            Titlee = this.Title;
            this._map = map;


            if (!_loadFromFile)
            {

                GenerateMap(_map);

            }
            else
            {
                LoadMap(_map.Name);
                //DeleteCanvas();
            }

            //_worker.DoWork += worker_DoWork;
            //_worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            //_worker.RunWorkerAsync();

            mainWindow.MouseWheel += new MouseWheelEventHandler(mainWindow_MouseWheel);

            Closing += OnWindowClosing;
            

        }
        double _scale;

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            XmlToFile.WriteToXmlFile(@"C:\VisualStudioProjects\DDRemake\DDRemakeProject\DDRemakeProject\" + _map.Name + ".xml", _generator);
            // Handle closing logic, set e.Cancel as needed
        }

        private EngineV1 _v1;
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!_loadFromFile)
            {
                
                GenerateMap(_map);
               
            }
            else
            {
                LoadMap(_map.Name);
                //DeleteCanvas();
            }

            //v1.MoveMethod1();
        }
        private void GenerateMap(MapBasicInfo map)
        {
            _generator = new WorldGeneration.GeneratorV1(new Vector(map.Width,map.Height));
            _v1 = new EngineV1(ref _generator);
        }
        private void LoadMap(string mapName)
        {
            _generator= XmlToFile.ReadFromXmlFile<WorldGeneration.GeneratorV1>(@"C:\VisualStudioProjects\DDRemake\DDRemakeProject\DDRemakeProject\"+mapName+".xml");
            _generator.Generate();
            _v1 = new EngineV1(ref _generator);
        }

         private void DeleteCanvas()
        {
            Application.Current.Dispatcher.Invoke((System.Action)delegate
            {

                canvas.Children.Clear();

                
            });
        }


        private void worker_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e)
        {
            //update ui once worker complete his work
        }

        private void mainWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ZoomOnCanvas(canvas,e);
            //ZoomOnCanvas(canvas2, e);

        }

        private void ZoomOnCanvas(Canvas c,MouseWheelEventArgs e)
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
            DragCanvas(canvas,e);
            //DragCanvas(canvas2, e);

        }

        private void DragCanvas(Canvas c,MouseEventArgs e)
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

        private void mainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            //_v1.MoveMethod2(sender,e);
        }
    }
}
