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
            
            Tile = new Tile(position: engine.Generator.Size, multiTileShape: new RoomModule(), type: Tile.TypeEnum.Wall)
            {
                Rect = {Fill = Brushes.DarkBlue},
                Position = engine.Generator.Rooms.First().Rect.LocationCenter()
            };


            MapWindow.BackgroundCanvas.Children.Remove(element: Tile.Rect);
            MapWindow.DynamicCanvas.Children.Add(element: Tile.Rect);
            Engine = engine;

            List<Character> characters = new List<Character>
            {
                new Character(characterLogic: new CharacterLogic(race: Races.Warrior,level: 1,strength: 2,agility: 1,endurance: 2,intelligence: 3))
                ,new Character(characterLogic: new CharacterLogic(race: Races.Mage,level: 2,strength: 1,agility: 2,endurance: 3,intelligence: 2)),
                new Character(characterLogic: new CharacterLogic(race: Races.Paladin,level: 1,strength: 4,agility: 2,endurance: 1,intelligence: 2))
            };
            Party = new Party(characters: characters);

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
                ? new Vector(x: 0, y: 1)
                : new Vector(x: 0, y: 0)
                  + (e.Key == Key.W ? new Vector(x: 0, y: -1) : new Vector(x: 0, y: 0))
                  + (e.Key == Key.A ? new Vector(x: -1, y: 0) : new Vector(x: 0, y: 0))
                  + (e.Key == Key.D ? new Vector(x: 1, y: 0) : new Vector(x: 0, y: 0));

            //_r.Rect.Margin = new Thickness();
            Tile.TypeEnum lastType = CurrentTile.Type;
            Engine.MiniMap.MovePlayer();
            if (Engine.Generator.Tiles[key: Tile.Position + movingVector]?.Type != Tile.TypeEnum.Wall)
            {
                Tile.Position += movingVector;
                //e.Handled = true;
                if (!Engine.Camera.CheatsActivated)
                {
                    Engine.Camera.Follow(movementVector: movingVector);
                    if (lastType == Tile.TypeEnum.Door)
                    {
                        Engine.Camera.Zoom();
                        Engine.MiniMap.AddVisitedItem(shape: CurrentMultiTileShape);
                    }
                }
            }
           

        }

        public Tile CurrentTile
        {
            get
            {
                if (!Engine.Generator.Tiles.ContainsKey(key: Tile.Position)) return null;
                else return Engine.Generator.Tiles[key: Tile.Position];
            }
        }
        [JsonIgnore]
        public IMultiTileShape CurrentMultiTileShape => CurrentTile.MultiTileShape;
    }
}
