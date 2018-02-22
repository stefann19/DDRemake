using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DDRemakeProject.World;
using DDRemakeProject.WorldGeneration;

namespace DDRemakeProject.GamePlay
{
    internal class Engine
    {
        private readonly Tile _r;
        private readonly Generator generator;

        public Engine(Generator generatorV1)
        {
            generator = generatorV1;
            

            _r = new Tile(generatorV1.Size, Tile.TypeEnum.Wall);
            _r.Rect.Fill = Brushes.DarkBlue;
            _r.Position = generatorV1.Rooms.First().RoomRect.LocationCenter();

            MapWindow.BackgroundCanvas.Children.Remove(_r.Rect);
            MapWindow.DynamicCanvas.Children.Add(_r.Rect);

            EventManager.RegisterClassHandler(typeof(MapWindow), UIElement.KeyDownEvent, new KeyEventHandler(MoveMethod));

        }

        private void MoveMethod(object sender, KeyEventArgs e)
        {
            if(e.IsRepeat) return;
            Vector movingVector;
            movingVector = e.Key == Key.S
                ? new Vector(0, 1)
                : new Vector(0, 0)
                  + (e.Key == Key.W ? new Vector(0, -1) : new Vector(0, 0))
                  + (e.Key == Key.A ? new Vector(-1, 0) : new Vector(0, 0))
                  + (e.Key == Key.D ? new Vector(1, 0) : new Vector(0, 0));

            //_r.Rect.Margin = new Thickness();
            if (generator.Tiles[_r.Position+movingVector]?.Type == Tile.TypeEnum.Floor || generator.Tiles[_r.Position + movingVector]?.Type == Tile.TypeEnum.Road )
            {
                _r.Position += movingVector;
                //e.Handled = true;
            }
        }
    }
}