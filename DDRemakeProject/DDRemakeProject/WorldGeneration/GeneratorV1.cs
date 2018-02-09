using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using DDRemakeProject.Deprecated;
using DDRemakeProject.World;
using static System.Linq.Enumerable;

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
        public List<RoomModule> Roads { get; set; }
        public Dictionary<Vector, Tile> Tiles { get; set; }
        #endregion

        #region Methods

    
        public void Generate(Vector Size)
        {
            Rooms = new List<RoomModule>();
            GenerationRooms = new List<RoomModule>();
            Tiles = new Dictionary<Vector, Tile>();

            _nrOfUsedtiles = 0;

            Random rnd = new Random();

            System.Windows.Point startingPos = new System.Windows.Point((int) (MainWindow.CanvasS1.Width / (2 *Constants.TilePx)), (int) (MainWindow.CanvasS1.Height / (2 * Constants.TilePx)));

            bool firstRoom = true;


            while ( (GenerationRooms.Count>0 || firstRoom ==true) && _nrOfUsedtiles < Size.X * Size.Y )
            {

                if (firstRoom)
                {
                    int size = (GeneratorExtensions.MaxRoomSize + GeneratorExtensions.MinRoomSize) / 2;
                    RoomModule r = new RoomModule(new Rect(startingPos,new Size(size, size)));
                    AddRoom(r);
                    firstRoom = false;


                }
                else
                {
                    int index = rnd.Next(0, GenerationRooms.Count);
                    Rect r= GenerationRooms[index].CheckForSpace();

                    if (!r.IsEmpty)
                    {
                        AddRoom(new RoomModule(r));
                    }
                    else
                    {
                        GenerationRooms.RemoveAt(index);
                    }

                }
            }
            GenerateRoads();
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

            r.Tiles.ToList().ForEach(tile=> Tiles.Add(tile.Key,tile.Value));
            _nrOfUsedtiles += r.Tiles.Count;
            //Tiles.Add();
        }

        private void GenerateRoads()
        {
            Roads = new List<RoomModule>();
            HashSet<HashSet<RoomModule>> LinkedRoomsListOfLists = new HashSet<HashSet<RoomModule>>(Rooms.Select(r =>
            {
                r.LinkedRoomsList = new HashSet<RoomModule> {r};
                return r.LinkedRoomsList;
            }));

            foreach (HashSet<RoomModule> linkedRoomsList in LinkedRoomsListOfLists)
            {
                
                foreach (RoomModule roomModule in linkedRoomsList)
                {
                    foreach (KeyValuePair<Vector, RoomModule> keyValuePair in roomModule.Neighbors.AsEnumerable().Where(neighbor => neighbor.Value.LinkedRoomsList != roomModule.LinkedRoomsList))
                    {
                        GenerateRoad(roomModule, keyValuePair.Value);

                        //Rect r = roomModule.GenerateRoad(keyValuePair.Value);
                        //Roads.Add(new RoomModule(r));
                        //roomModule.LinkedRoomsList.UnionWith(keyValuePair.Value.LinkedRoomsList);
                        //keyValuePair.Value.LinkedRoomsList = roomModule.LinkedRoomsList;
                    }
                    //break;
                    
                }
                //break;

            }
        }

        private bool GenerateRoad(RoomModule startingRoomModule,RoomModule endingRoomModule)
        {
            //make vertical road
            //if(startingRoomModule.RoomRect.Y < endingRoomModule.RoomRect.)
            List<RoomModule> rooms = new List<RoomModule>{startingRoomModule,endingRoomModule};
            Rect s = new Rect();
            Rect e = new Rect();
            //s.Y = startingRoomModule.RoomRect.Y;
            //s.Size = startingRoomModule.RoomRect.Size;

            //e.Y = endingRoomModule.RoomRect.Y;
            //e.Size = endingRoomModule.RoomRect.Size;

            //if (!s.IntersectsWith(e))
            //{
            //    //create vertical road
            //    System.Windows.Point roadPosition = (System.Windows.Point)((startingRoomModule.RoomRect.LocationCenter() + endingRoomModule.RoomRect.LocationCenter())/2);
            //    roadPosition = new System.Windows.Point((int) roadPosition.X, (int) roadPosition.Y);
            //    Size roadSize = new Size(3, Math.Abs(startingRoomModule.RoomRect.Y - endingRoomModule.RoomRect.Y));
            //    Rect roadRect = new Rect(roadPosition, roadSize);
            //    RoomModule road = new RoomModule(roadRect);
            //    AddRoad(road);
            //}


            s = new Rect();
            e = new Rect();
            s.X = startingRoomModule.RoomRect.X;
            s.Size = startingRoomModule.RoomRect.Size;

            e.X = endingRoomModule.RoomRect.X;
            e.Size = endingRoomModule.RoomRect.Size;

            if (!s.IntersectsWith(e))
            {
                //create vertical road
                System.Windows.Point roadPosition = (System.Windows.Point)((startingRoomModule.RoomRect.LocationCenter() + endingRoomModule.RoomRect.LocationCenter()) / 2);
                Size roadSize = new Size(Math.Abs(startingRoomModule.RoomRect.X - endingRoomModule.RoomRect.X),3);
                roadPosition = (Point)((Vector)roadPosition - (Vector)roadSize / 2);
                roadPosition = new System.Windows.Point((int)roadPosition.X, (int)roadPosition.Y);

                Rect roadRect = new Rect(roadPosition, roadSize);
                RoomModule road = new RoomModule(roadRect);
                AddRoad(road);
                //check edges for edginess

                List<Vector> roadPoints = new List<Vector>{new Vector(roadRect.Location.X,roadRect.LocationCenter().Y), new Vector(roadRect.Location.X+roadRect.Width, roadRect.LocationCenter().Y) };

                roadPoints.ForEach(point =>
                {
                    RoomModule minRoomModule = rooms.First(room => Math.Abs((room.RoomRect.LocationCenter() - point).Length) == rooms.Min(v => Math.Abs((v.RoomRect.LocationCenter()-point).Length)));
                    if (minRoomModule.Tiles.ContainsKey(point))
                    {
                        
                       return;
                        
                    }
                    Size roadSecondarySize = new Size(3,(int) Math.Abs(point.Y - minRoomModule.RoomRect.Y));
                    Vector roadSecondaryPosition = point - (Vector)roadSecondarySize / 2f;
                    roadSecondaryPosition = new Vector((int)roadSecondaryPosition.X,(int)roadSecondaryPosition.Y);
                    Rect roadSecondaryRect = new Rect((Point)roadSecondaryPosition, roadSecondarySize);
                    RoomModule roadSecondary = new RoomModule(roadSecondaryRect);
                    AddRoad(roadSecondary);

                });
                return true;
            }

            return false;

        }

        private void AddRoad(RoomModule road)
        {
            Roads.Add(road);
            road.Tiles.Where(v=> Tiles.ContainsKey(v.Key)).ToList().ForEach(tile =>
            {
                MainWindow.CanvasS1.Children.Remove(tile.Value.Rect);
                if (Tiles[tile.Key].Type == Tile.TypeEnum.Floor)
                {
                    //tile. = Tiles[tile.Key
                    road.Tiles[tile.Key] = Tiles[tile.Key];
                }
                else
                {
                    if (Tiles[tile.Key].Type == Tile.TypeEnum.Wall && tile.Value.Type == Tile.TypeEnum.Floor)
                    {
                        Tiles[tile.Key].Type = Tile.TypeEnum.Floor;
                        road.Tiles[tile.Key] = Tiles[tile.Key];
                    }
                }
                
            });
            
            road.Tiles.Where(v=>!Tiles.ContainsKey(v.Key)).ToList().ForEach(tile=>Tiles.Add(tile.Key,tile.Value));
        }

    }

    #endregion


    public static class GeneratorExtensions
    {
        public const int SpaceBetweenRooms =6;
        public const int MinRoomSize =7;
        public const int MaxRoomSize = 11;
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());

        public static Rect CheckForSpace(this RoomModule roomModule)
        {
            Vector pointer = new Vector(0, 1);
            //Random rnd = new Random(Guid.NewGuid().GetHashCode());
            List<Rect> allRects = new List<Rect>();
            
            roomModule.AvailableAngles.ForEach(angle =>
            {
                if (angle.X > angle.Y)
                {
                    double angleY = angle.X;
                    angle.X = angle.Y;
                    angle.Y = angleY;
                }
                List<Rect> rects = new List<Rect>();

                List<int> angles = Range((int) angle.X, (int) angle.Y - (int) angle.X).GroupBy( v=>v%20 ).Take(1).SelectMany(v=>v).Where(v => v > angle.X && v < angle.Y).ToList();

                //.SelectMany(v => v).Where(v => v > angle.X && v < angle.Y).ToList();

                angles.ForEach(eachAngle =>
                {
                    
                    Vector point = pointer.Rotate(eachAngle) * (MaxRoomSize / 2f + roomModule.RoomRect.Width / 2f + Random.Next(3, SpaceBetweenRooms));
                    point = new Vector((int)point.X, (int)point.Y) + (Vector)roomModule.RoomRect.Location;

                    if (!MainWindow.Size.Contains((System.Windows.Point)point)) return;


                    for (int i = MaxRoomSize; i >= MinRoomSize; i-=2)
                    {
                        Rect r = new Rect((System.Windows.Point)point, new Size(i, i));

                        if (!MainWindow.Size.Contains(r)) continue;

                        if (roomModule.Neighbors.Values.All(room => !room.RoomRect.IntersectsWith(r)))
                        {
                            rects.Add(r);
                            if(i>MinRoomSize)
                            //Range(MinRoomSize,(i)-MinRoomSize).ToList().ForEach(size =>
                            for(int j = i;j>=MinRoomSize;j-=2)
                            {
                                rects.Add(new Rect((System.Windows.Point)point, new Size(j, j)));
                            }
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
                return allRects[Random.Next(0, allRects.Count)];
            }
            else
            {
                return Rect.Empty;
            }
        }

        //public static Rect GenerateRoad(this RoomModule startingRoomModule, RoomModule endingRoomModule)
        //{
        //    double angle = Vector.AngleBetween((Vector)startingRoomModule.RoomRect.Location, (Vector)endingRoomModule.RoomRect.Location);
        //    System.Windows.Point middlePoint = (System.Windows.Point) (((Vector) startingRoomModule.RoomRect.Location +
        //                                  (Vector) endingRoomModule.RoomRect.Location) / 2f);
        //    double distance = Math.Abs(((Vector) startingRoomModule.RoomRect.Location - (Vector) endingRoomModule.RoomRect.Location)
        //        .Length);
        //    Rect road = new Rect(middlePoint,new Size(3, distance) );
           
        //    Matrix m =new Matrix();
        //    m.Rotate(angle);
        //    road.Transform(m);
        //    return road;   
        //}


        //room -> (room,connected rooms) Dictionary
        // MakeDoorBetween(room1,room2);
        //Dictionary.randomRoom , Dictionary.randomRoom2!= connected rooms -> randomroom.connectedROoms += randoomRoom2; randomRoom2 remove 
    }
}

