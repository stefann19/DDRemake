using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DDRemakeProject.GamePlay.New;
using DDRemakeProject.GamePlay.New.Character.Logic;
using DDRemakeProject.GamePlay.Old;
using DDRemakeProject.WorldGeneration;
using Newtonsoft.Json;
using Character = DDRemakeProject.GamePlay.New.Character.Character;

namespace DDRemakeProject.World
{
    public class Player
    {
        
        public Player(Engine engine)
        {
            if(engine==null)return;
            
            Tile = new Tile(engine.Generator.Size, new RoomModule(), Tile.TypeEnum.Wall)
            {
                Rect = {Fill = Brushes.DarkBlue},
                Position = engine.Generator.Rooms.First().Rect.LocationCenter()
            };


            MapWindow.BackgroundCanvas.Children.Remove(Tile.Rect);
            MapWindow.DynamicCanvas.Children.Add(Tile.Rect);
            Engine = engine;

            List<Character> characters = new List<Character>{new Character(5,5,5,5),new Character(10,5,2,3),new Character(2,2,10,6)};
            Party = new Party(characters);

        }

        [JsonIgnore]
        public Engine Engine { get; set; }
        public Tile Tile { get; set; }
        [JsonIgnore]
        public Party Party { get; set; }

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
            Engine.MiniMap.MovePlayer();
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
                        Engine.MiniMap.AddVisitedItem(CurrentMultiTileShape);
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
        [JsonIgnore]
        public IMultiTileShape CurrentMultiTileShape => CurrentTile.MultiTileShape;
    }
}
