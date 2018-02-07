using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using DDRemakeProject.World;
using static System.Linq.Enumerable;
using Point = DDRemakeProject.Deprecated.Point;

namespace DDRemakeProject.WorldGeneration
{
    public class GeneratorV1
    {
        private int _nrOfUsedtiles;
       

        #region Constructors

        /// <summary>
        ///     Generate a map of tiles using own algorithm
        /// </summary>
        /// <param name="sizeX">Worlds width in number of tiles</param>
        /// <param name="sizeY">Worlds height in number of tiles</param>
        public GeneratorV1(Vector size)
        {
            Size = size;
            Generate(size);
        }

        /// <summary>
        ///     Needed for serialization
        /// </summary>
        public GeneratorV1()
        {
        }

        #endregion

        #region Properties

        public Vector Size { get; set; }

        //[XmlIgnore]
        //public List<Tile> GenerationTiles { get; set; }

        public List<RoomModule> Rooms { get; set; }
        public List<RoomModule> GenerationRooms { get; set; }

        public Dictionary<Vector, Tile> Tiles { get; set; }

        #endregion

        #region Methods

    
        public void Generate(Vector Size)
        {
            //GenerationTiles = new List<Tile>();
            Rooms = new List<RoomModule>();
            GenerationRooms = new List<RoomModule>();
            Tiles = new Dictionary<Vector, Tile>();
            //for (int i = 0; i < sizeX; i++) TilesMatrix[i] = new Tile[sizeY];

            _nrOfUsedtiles = 0;

            Random rnd = new Random();
            //int startingPosX = (int) (Size.X / 2);
            //int startingPosy = (int) (Size.Y / 2);
            System.Windows.Point startingPos = new System.Windows.Point((int) (MainWindow.CanvasS1.Width / (2 *Constants.TilePx)), (int) (MainWindow.CanvasS1.Height / (2 * Constants.TilePx)));

            //GenerationTiles.Add(new Tile((Vector)startingPos));
            bool firstRoom = true;


            while ( (GenerationRooms.Count>0 || firstRoom ==true) && _nrOfUsedtiles < Size.X * Size.Y )
            {
                //Tile current = GenerationTiles.First();
                //Tile.ShowTile(current.Position, Color.FromRgb(255, 0, 0));
                //MessageBox.Show(generationTiles.First().outerDirection.X.ToString());
                if (firstRoom)
                {
                    // STARTING TILE
                    int size = (GeneratorExtensions.MaxRoomSize + GeneratorExtensions.MinRoomSize) / 2;
                    RoomModule r = new RoomModule(new Rect(startingPos,new Size(size, size)));
                    AddRoom(r);
                    firstRoom = false;


                    //RoomSpace r = RoomModule.CheckForSpace(current.Position, TilesMatrix,
                    //    new System.Windows.Point(SizeX, SizeY), 5);
                    //MakeRoom(r.GetTopLeft(), r.GetSize());
                }
                else
                {

                    Rect r= GenerationRooms[rnd.Next(0,GenerationRooms.Count)].CheckForSpace();

                    if (!r.IsEmpty)
                    {
                        AddRoom(new RoomModule(r));
                    }
                    else
                    {
                        GenerationRooms.Remove(GenerationRooms.First());
                    }

                    //Point derivedP;
                    //int aux = rnd.Next(4, 6);
                    //derivedP = current.Position + current.OuterDirection * aux;


                    //Tile.ShowTile(derivedP, Color.FromRgb(0, 255, 0));

                    //RoomSpace r = RoomModule.CheckForSpace(derivedP, TilesMatrix,
                    //    new System.Windows.Point(SizeX, SizeY), aux);
                    //if (r.GetSize().X > aux + 1 && r.GetSize().Y > aux + 1)
                    //{
                    //    RoomModule rm = MakeRoom(r.GetTopLeft(), r.GetSize());
                    //    //MakeDoorBetter(rm, current);
                    //}
                    //else
                    //{
                    //    GenerationTiles.Remove(GenerationTiles.First());
                    //}
                }
                //MakeDoor(current);
                Thread.Sleep(10);
            }
        }

        public void Generate()
        {
            foreach (RoomModule rm in Rooms)
            {
                rm.Generate(true);
                Thread.Sleep(10);
            }
        }


        private void AddRoom(RoomModule r)
        {
            Rooms.Add(r);
            GenerationRooms.Add(r);
            Rooms.Where(room=> !r.Neighbors.ContainsKey((Vector)room.RoomRect.Location)).ToList().ForEach(room =>
            {
                double distance = Math.Abs(((Vector) (room.RoomRect.Location) - (Vector) (r.RoomRect.Location)).Length);
                if (distance < GeneratorExtensions.MaxRoomSize*2f + GeneratorExtensions.SpaceBetweenRooms)
                {
                    if (!r.Neighbors.ContainsKey((Vector)room.RoomRect.Location) )
                    {
                        r.Neighbors.Add((Vector) room.RoomRect.Location, room);


                        r.CalculateRemainingAngles(r.CalculateAngle(room));
                    }
                    if (!room.Neighbors.ContainsKey((Vector) r.RoomRect.Location))
                    {
                        room.Neighbors.Add((Vector)r.RoomRect.Location, r);
                        room.CalculateRemainingAngles(room.CalculateAngle(r));

                    }

                }
            });

            //r.Tiles.ToList().ForEach(tile=> Tiles.Add(tile.Key,tile.Value));
            _nrOfUsedtiles += r.Tiles.Count;
            //Tiles.Add();
        }



    }

    #endregion


    public static class GeneratorExtensions
    {
        public const int SpaceBetweenRooms =6;
        public const int MinRoomSize =4;
        public const int MaxRoomSize = 10;

        public static Rect CheckForSpace(this RoomModule roomModule)
        {
            Vector pointer = new Vector(0, 1);
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            List<Rect> allRects = new List<Rect>();
            
            roomModule.AvailableAngles.ForEach(angle =>
            {
                if (angle.X > angle.Y)
                {
                    double angleY = angle.X;
                    angle.X = angleY;
                    angle.Y = angleY;
                }
                float rndAngle = rnd.Next((int)angle.X,(int) angle.Y);
                List<Rect> rects = new List<Rect>();

                Range((int)angle.X,(int)angle.Y - (int)angle.X).GroupBy(v=> v%20).SelectMany(v=>v).Where(v=> v> angle.X && v < angle.Y).ToList().ForEach(eachAngle =>
                {
                    
                    Vector point = pointer.Rotate(eachAngle) * (MaxRoomSize / 2f + roomModule.RoomRect.Width / 2f + rnd.Next(3, SpaceBetweenRooms));
                    point = new Vector((int)point.X, (int)point.Y) + (Vector)roomModule.RoomRect.Location;

                    if (!MainWindow.Size.Contains((System.Windows.Point)point)) return;


                    for (int i = MaxRoomSize; i >= MinRoomSize; i--)
                    {
                        Rect r = new Rect((System.Windows.Point)point, new Size(MaxRoomSize, MaxRoomSize));

                        if (!MainWindow.Size.Contains(r)) continue;

                        if (roomModule.Neighbors.Values.All(room => !room.RoomRect.IntersectsWith(r)))
                        {
                            rects.Add(r);
                        }
                        else
                        {
                            break;
                        }
                    }


                });
             
                
                if (rects.Count > 0)
                {
                    allRects.AddRange(rects);
                }

            });
            if (allRects.Count > 0)
            {
                return allRects[rnd.Next(0, allRects.Count)];
            }
            else
            {
                return Rect.Empty;
            }
        }


        //room -> (room,connected rooms) Dictionary
        // MakeDoorBetween(room1,room2);
        //Dictionary.randomRoom , Dictionary.randomRoom2!= connected rooms -> randomroom.connectedROoms += randoomRoom2; randomRoom2 remove 
    }
}

