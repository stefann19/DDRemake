using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DDRemakeProject.World;
using DDRemakeProject.WorldGeneration;

namespace DDRemakeProject.GamePlay
{
    internal class EngineV1
    {
        private readonly Tile _r;
        private readonly GeneratorV1 generatorV1;

        public EngineV1(ref GeneratorV1 generatorV1)
        {
            this.generatorV1 = generatorV1;


            _r = new Tile(generatorV1.Size, new RoomModule(),Tile.TypeEnum.Wall );
            Application.Current.Dispatcher.Invoke(delegate
            {
                _r.Rect.Fill = Brushes.DarkRed;
                MainWindow.CanvasS1.Children.Remove(_r.Rect);
                MainWindow.CanvasS2.Children.Add(_r.Rect);
            });
            //MoveMethod1();
        }

        public void MoveMethod2(object sender, KeyEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                Vector movingVector;
                movingVector = Keyboard.IsKeyDown(Key.S) ? new Vector(0,-1) : new Vector(0,0) 
                             + (Keyboard.IsKeyDown(Key.W) ? new Vector(0, 1) : new Vector(0, 0) )
                             + (Keyboard.IsKeyDown(Key.A) ? new Vector(-1, 0) : new Vector(0, 0))
                             + (Keyboard.IsKeyDown(Key.S) ? new Vector(1, 0) : new Vector(0, 0));

                _r.Rect.Margin = new Thickness();

                /*if (Keyboard.IsKeyDown(Key.S))
                    try
                    {
                        if (generatorV1.Tiles[(int) (_r.Rect.Margin.Left / 16)][
                                (int) ((_r.Rect.Margin.Top + 16f) / 16)].Color == Colors.LightCyan)
                            _r.Rect.Margin = new Thickness(_r.Rect.Margin.Left, _r.Rect.Margin.Top + 16f, 0, 0);
                    }
                    catch
                    {
                    }
                if (Keyboard.IsKeyDown(Key.W))
                    try
                    {
                        if (generatorV1.TilesMatrix[(int) (_r.Rect.Margin.Left / 16)][
                                (int) ((_r.Rect.Margin.Top - 16f) / 16)].Color == Colors.LightCyan)
                            _r.Rect.Margin = new Thickness(_r.Rect.Margin.Left, _r.Rect.Margin.Top - 16f, 0, 0);
                    }
                    catch
                    {
                    }
                if (Keyboard.IsKeyDown(Key.A))
                    try
                    {
                        if (generatorV1.TilesMatrix[(int) ((_r.Rect.Margin.Left - 16) / 16)]
                                [(int) (_r.Rect.Margin.Top / 16)].Color == Colors.LightCyan)
                            _r.Rect.Margin = new Thickness(_r.Rect.Margin.Left - 16f, _r.Rect.Margin.Top, 0, 0);
                    }
                    catch
                    {
                    }
                if (Keyboard.IsKeyDown(Key.D))
                    try
                    {
                        if (generatorV1.TilesMatrix[(int) ((_r.Rect.Margin.Left + 16) / 16)]
                                [(int) (_r.Rect.Margin.Top / 16)].Color == Colors.LightCyan)
                            _r.Rect.Margin = new Thickness(_r.Rect.Margin.Left + 16f, _r.Rect.Margin.Top, 0, 0);
                    }
                    catch
                    {
                    }*/
            });
        }
    }
}