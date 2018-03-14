using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using DDRemakeProject.WorldGeneration;
using Newtonsoft.Json;
using Point = System.Windows.Point;

namespace DDRemakeProject.World
{
    public class RoomModule : IMultiTileShape
    {
        [JsonIgnore]
        public Generator Generator { get; set; }
        public Rect Rect { get; set; }

        /// <summary>
        /// Matrix holding the Image(tiles) objects of the room
        /// </summary>
        public Dictionary<Vector, Tile> Tiles { get; set; }

        /// <summary>
        /// List of Tiles containing all the wall tiles in the Room
        /// </summary>
        /// 
        [JsonIgnore]
        public HashSet<Tile> WallTiles { get; set; }

        [JsonIgnore]
        public Dictionary<Vector,RoomModule> Neighbors { get; set; }
        [JsonIgnore]
        public List<Vector> AvailableAngles { get; set; }
        [JsonIgnore]
        public List<RoomModule> LinkedRoomsList { get; set; }
        [JsonIgnore]
        public List<RoomModule> ConnectedRoomModules { get; set; }
        [JsonIgnore]
        public List<Road> Roads { get; set; }

        public RoomModule()
        {

        }

        private bool isRoad;
        public RoomModule(Rect size,Generator generator)
        {
            this.Rect = size;
            if (Rect.Width == 3 || Rect.Height == 3) isRoad = true;
            Generate(false,generator);
            AvailableAngles = new List<Vector>{new Vector(-180,180)};
            Neighbors = new Dictionary<Vector, RoomModule>();
            ConnectedRoomModules = new List<RoomModule>();
            Roads = new List<Road>();

        }

        public void Generate(bool loadingFromFile,Generator generator)
        {
            Generator = generator;
            //if (!loadingFromFile)
            //{
            //    Tiles = new Dictionary<Vector, Tile>();
            //    WallTiles = new HashSet<Tile>();
            //}
            Tiles = new Dictionary<Vector, Tile>();
            WallTiles = new HashSet<Tile>();
            if (loadingFromFile)
            {
                Roads = new List<Road>();
            }
            
            int area = (int) (Rect.Width * Rect.Height);
            for (int i = area  - 1; i >= 0; i--)
            {
                Vector tilePosition = new Vector(Math.Floor(i % Rect.Size.Width),Math.Floor((i / Rect.Size.Width))) +(Vector) Rect.Location;
                Tile.TypeEnum tileType;

                if (i < Rect.Size.Width || i > area - Rect.Size.Width || Math.Abs(i % Rect.Size.Width) < 0.1f ||
                    Math.Abs(i % Rect.Size.Width - (Rect.Size.Width- 1)) < 0.1f)
                {
                    tileType = Tile.TypeEnum.Wall;
                }
                else
                {
                    
                    tileType = isRoad ? Tile.TypeEnum.Road : Tile.TypeEnum.Floor;
                }

                this.AddTile(tilePosition, tileType);   
            }
            

        }

        /*public void AddTile( Vector Position, Tile.TypeEnum type,MultiTileShape multiTileShape)
        {
            Tile t = new Tile(Position,multiTileShape, type);
            //t.InitialiseRect();
            if (this.Tiles.ContainsKey(t.Position)) return;

            this.Tiles.Add(t.Position, t);

            if (t.Type == Tile.TypeEnum.Wall)
            {
                this.WallTiles.Add(t);
            }
        }*/

        public Vector CalculateAngle(RoomModule roomModule)
        {

            List<Vector> Positions = new List<Vector>
            {
                new Vector(roomModule.Rect.X,roomModule.Rect.Y),
                new Vector(roomModule.Rect.X+roomModule.Rect.Width,roomModule.Rect.Y),
                new Vector(roomModule.Rect.X,roomModule.Rect.Y+roomModule.Rect.Height),
                new Vector(roomModule.Rect.X+roomModule.Rect.Width,roomModule.Rect.Y+roomModule.Rect.Height)
            };

            List<double> Angles = new List<double>();
            Positions.ForEach(pos =>
            {
                double angle = Vector.AngleBetween((Vector) this.Rect.Location, pos);
                Angles.Add(angle);
            });


            return new Vector(Angles.Min(), Angles.Max());
        }

        public void CalculateRemainingAngles(Vector addedAngle)
        {
            List<Vector> AddAngles =new List<Vector>();


            AvailableAngles.ForEach(angle =>
            {
                void AddAngle(Vector v)
                {
                    if(Math.Abs(v.X-v.Y)>4)
                        AddAngles.Add(v);
                }

                if (addedAngle.X > angle.X && addedAngle.Y < angle.Y)
                {
                    AddAngle(new Vector(addedAngle.Y, angle.Y));
                    AddAngle(new Vector(angle.X,addedAngle.X));
                    //angle.Y = addedAngle.X;
                }
                else
                {
                    if (addedAngle.X > angle.X)
                    {
                        AddAngle(new Vector(angle.X, addedAngle.X));
                        //angle.Y = addedAngle.X;
                    }
                    else
                    {
                        //angle.X = addedAngle.Y;
                        AddAngle(new Vector(addedAngle.Y, angle.Y));
                    }
                }
            });
            
            AvailableAngles = AddAngles.Distinct().ToList();
        }

    }

  
}
 