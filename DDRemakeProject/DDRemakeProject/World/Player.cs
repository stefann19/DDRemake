using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DDRemakeProject.GamePlay;
using DDRemakeProject.WorldGeneration;

namespace DDRemakeProject.World
{
    public class Player
    {
        
        public Player(Engine engine)
        {
            Tile tile;
            tile = new Tile(engine.Generator.Size, new RoomModule(), Tile.TypeEnum.Wall);
            tile.Rect.Fill = Brushes.DarkBlue;
            tile.Position = engine.Generator.Rooms.First().Rect.LocationCenter();


            Tile = tile;
            MapWindow.BackgroundCanvas.Children.Remove(Tile.Rect);
            MapWindow.DynamicCanvas.Children.Add(Tile.Rect);
            Engine = engine;
        }

        public Engine Engine { get; set; }
        public Tile Tile { get; set; }

        public void MoveTile(KeyEventArgs e)
        {
            if (e.IsRepeat) return;
            Vector movingVector = e.Key == Key.S
                ? new Vector(0, 1)
                : new Vector(0, 0)
                  + (e.Key == Key.W ? new Vector(0, -1) : new Vector(0, 0))
                  + (e.Key == Key.A ? new Vector(-1, 0) : new Vector(0, 0))
                  + (e.Key == Key.D ? new Vector(1, 0) : new Vector(0, 0));

            //_r.Rect.Margin = new Thickness();
            Tile.TypeEnum lastType = CurrentTile.Type;
            if (Engine.Generator.Tiles[Tile.Position + movingVector]?.Type != Tile.TypeEnum.Wall)
            {
                Tile.Position += movingVector;
                //e.Handled = true;
                if (!Engine.Camera.CheatsActivated)
                {
                    Engine.Camera.Follow(movingVector);
                    if (lastType == Tile.TypeEnum.Door)
                    {
                        Engine.Camera.Zoom();
                    }
                }
            }
           

        }

        public Tile CurrentTile
        {
            get
            {
                if (!Engine.Generator.Tiles.ContainsKey(Tile.Position)) return null;
                else return Engine.Generator.Tiles[Tile.Position];
            }
        }

        public IMultiTileShape CurrentMultiTileShape => CurrentTile.MultiTileShape;
    }
}
