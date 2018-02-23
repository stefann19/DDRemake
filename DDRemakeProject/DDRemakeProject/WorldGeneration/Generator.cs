using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using DDRemakeProject.World;
using static System.Linq.Enumerable;

namespace DDRemakeProject.WorldGeneration
{
    public class Generator
    {
        private int _nrOfUsedtiles;
       

        #region Constructors

        /// <summary>
        ///     Generate a map of tiles using own algorithm
        /// </summary>
        /// <param name="sizeX">Worlds width in number of tiles</param>
        /// <param name="sizeY">Worlds height in number of tiles</param>
        public Generator(Vector size)
        {
            Size = size;
            Generate(size);
        }

        /// <summary>
        ///     Needed for serialization
        /// </summary>
        public Generator()
        {
        }

        #endregion

        #region Properties

        public Vector Size { get; set; }

        //[XmlIgnore]
        //public List<Tile> GenerationTiles { get; set; }

        public List<RoomModule> Rooms { get; set; }
        public List<RoomModule> GenerationRooms { get; set; }
        public List<Road> Roads { get; set; }
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

            System.Windows.Point startingPos = new System.Windows.Point((int) (MapWindow.BackgroundCanvas.Width / (2 *Constants.TilePx)), (int) (MapWindow.BackgroundCanvas.Height / (2 * Constants.TilePx)));

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
            GenerateRoads(45);

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
            Rooms.Where(room => !r.Neighbors.ContainsKey((Vector) room.Rect.Location)).ToList()
                .ForEach(room =>
                {
                    double distance = Math.Abs(Vector
                        .Subtract((Vector) room.Rect.Location, (Vector) r.Rect.Location).Length);

                    if (distance <= GeneratorExtensions.MaxRoomSize*2f + GeneratorExtensions.SpaceBetweenRooms)
                    {
                       
                            r.Neighbors.Add((Vector) room.Rect.Location, room);


                            r.CalculateRemainingAngles(r.CalculateAngle(room));
                        
                        if (!room.Neighbors.ContainsKey((Vector) r.Rect.Location))
                        {
                            room.Neighbors.Add((Vector) r.Rect.Location, r);
                            room.CalculateRemainingAngles(room.CalculateAngle(r));
                        }
                    }
                });
            r.Tiles.Where(tile=> !Tiles.ContainsKey(tile.Key)).ToList().ForEach(tile => Tiles.Add(tile.Key, tile.Value));
            _nrOfUsedtiles += r.Tiles.Count;
        }

        private void GenerateRoads(int additionalRoadChance)
        {
            Roads = new List<Road>();
            List<List<RoomModule>> LinkedRoomsListOfLists = new List<List<RoomModule>>();
            Rooms.ForEach(room =>
            {
                room.LinkedRoomsList = new List<RoomModule>{room};
                LinkedRoomsListOfLists.Add(room.LinkedRoomsList);
               
            });


            SortedDictionary < double, Tuple<RoomModule, RoomModule>> lowestRoad =new SortedDictionary<double, Tuple<RoomModule, RoomModule>>();
            Rooms.ForEach(room =>
                room.Neighbors.Values.Where(r=> !r.Equals(room)).ToList().ForEach(neighbor =>
                {
                    double distance =
                        Math.Abs((room.Rect.LocationCenter() - neighbor.Rect.LocationCenter()).Length);
                    bool add = false;
                    while (lowestRoad.ContainsKey(distance)) distance += 0.0001f;
                    lowestRoad.Add(distance, new Tuple<RoomModule, RoomModule>(neighbor, room));

                    //if (lowestRoad.Count <= 0)
                    //{
                    //    lowestRoad.Add(distance, new Tuple<RoomModule, RoomModule>(neighbor, room));
                    //    return;

                    //}

                    //KeyValuePair<double, Tuple<RoomModule, RoomModule>> a = lowestRoad.FirstOrDefault(value =>
                    //    value.Value.Equals(new Tuple<RoomModule, RoomModule>(room, neighbor)));
                    //KeyValuePair<double, Tuple<RoomModule, RoomModule>> b = lowestRoad.FirstOrDefault(value =>
                    //    value.Value.Equals(new Tuple<RoomModule, RoomModule>(neighbor, room)));
                    //if (a.Value != null)
                    //{
                    //    if (a.Key > distance)
                    //        add = true;
                    //}
                    //else if (b.Value != null)
                    //{
                    //    if (b.Key > distance)
                    //        add = true;
                    //}

                    //else
                    //{
                    //    lowestRoad[distance] = new Tuple<RoomModule, RoomModule>(neighbor, room);
                    //}

                    //if (add)
                    //    lowestRoad.Add(distance, new Tuple<RoomModule, RoomModule>(neighbor, room));
                }));

            int it = 0;
            Random rnd = new Random();
            lowestRoad.Values.TakeWhile(predicate=>LinkedRoomsListOfLists.Count!=1).ToList().ForEach(rooms =>
            {
                if(rooms.Item1.Roads.Count > 5 || rooms.Item2.Roads.Count > 5) return;

                if (rooms.Item1.LinkedRoomsList != rooms.Item2.LinkedRoomsList)
                {
                    it++;
                    if(!GenerateRoad(rooms.Item1, rooms.Item2))return;


                    //Rect r = roomModule.(neighbor.Value);
                    //Roads.Add(new RoomModule(r));
                    rooms.Item1.LinkedRoomsList.AddRange(rooms.Item2.LinkedRoomsList);
                    LinkedRoomsListOfLists.Remove(rooms.Item2.LinkedRoomsList);
                    //rooms.Item2.LinkedRoomsList = rooms.Item1.LinkedRoomsList;

                    rooms.Item2.LinkedRoomsList.ForEach(r => r.LinkedRoomsList = rooms.Item1.LinkedRoomsList);
                }
                else
                {
                    if (rnd.Next(0, 100) < additionalRoadChance && !rooms.Item1.Roads.Contains(rooms.Item2))
                    {
                        it++;
                        GenerateRoad(rooms.Item1, rooms.Item2);
                    }
                }
            });


          
         
        }


        private bool GenerateRoad(RoomModule startingRoomModule, RoomModule endingRoomModule)
        {

            if (!startingRoomModule.Roads.Contains(endingRoomModule))
            {
                startingRoomModule.Roads.Add(endingRoomModule);
            }
            if (!endingRoomModule.Roads.Contains(startingRoomModule))
            {
                endingRoomModule.Roads.Add(startingRoomModule);
            }


            List<RoomModule> rooms = new List<RoomModule> {startingRoomModule, endingRoomModule};
            List<Rect> roadRects = new List<Rect>();
        
            if(Math.Abs(startingRoomModule.Rect.LocationCenter().X -
                        endingRoomModule.Rect.LocationCenter().X) <= 
                        Math.Abs(startingRoomModule.Rect.LocationCenter().Y -
                        endingRoomModule.Rect.LocationCenter().Y) )
            { 
                

                Point roadPosition =
                        (Point)((startingRoomModule.Rect.LocationCenter() +
                                  endingRoomModule.Rect.LocationCenter()) /
                                 2f);
                Size roadSize = new Size(3,2 +Math.Abs(startingRoomModule.Rect.LocationCenter().Y -endingRoomModule.Rect.LocationCenter().Y));
                roadSize = new Size((int)roadSize.Width, (int)roadSize.Height);
                roadPosition = (Point)((Vector)roadPosition - (Vector)roadSize / 2f);
                roadPosition = new Point((int)roadPosition.X, (int)roadPosition.Y);

                Rect roadRect = new Rect(roadPosition, roadSize);
                roadRects.Add(roadRect);
                //RoomModule road = new RoomModule(roadRect);
                //AddRoad(road);
                //check edges for edginess

                List<Vector> roadPoints = new List<Vector>
                {
                    new Vector( (int) roadRect.LocationCenter().X,(int) roadRect.Top+2),
                    new Vector( (int) roadRect.LocationCenter().X,(int) roadRect.Bottom-1 )
                };

                roadPoints.ForEach(point =>
                {
                    RoomModule minRoomModule = rooms.Count==2 ? rooms.First(room =>
                        Math.Abs(Math.Abs((room.Rect.LocationCenter() - point).Length) -
                                 rooms.Min(v => Math.Abs((v.Rect.LocationCenter() - point).Length))) < 0.1f) : Rooms.First();
                    //rooms.Remove(minRoomModule);
                    Vector roadSecondaryPosition = point;
                    double xPos = (point.X + minRoomModule.Rect.LocationCenter().X) / 2f;
                    Size roadSecondarySize =
                        new Size( Math.Abs(point.X - minRoomModule.Rect.LocationCenter().X) +2,3);
                    if (point.X >= minRoomModule.Rect.Right-1)
                        roadSecondarySize.Width++;

                    roadSecondarySize = new Size((int)roadSecondarySize.Width, (int)roadSecondarySize.Height);
                    roadSecondaryPosition = new Vector(Math.Round(xPos), (int)roadSecondaryPosition.Y);
                    roadSecondaryPosition = roadSecondaryPosition - (Vector)roadSecondarySize / 2f;
                    roadSecondaryPosition = new Vector((int)roadSecondaryPosition.X, (int)roadSecondaryPosition.Y);
                    roadSecondarySize = new Size((int)roadSecondarySize.Width, (int)roadSecondarySize.Height);
                    Rect roadSecondaryRect = new Rect((Point)roadSecondaryPosition, roadSecondarySize);
                    roadRects.Add(roadSecondaryRect);
                    //RoomModule roadSecondary = new RoomModule(roadSecondaryRect);
                    //AddRoad(roadSecondary);
                });
            }
            else
            {
                //create vertical road
                Point roadPosition =
                    (Point) ((startingRoomModule.Rect.LocationCenter() +
                              endingRoomModule.Rect.LocationCenter()) /
                             2f);
                Size roadSize =
                    new Size(2+
                        Math.Abs(startingRoomModule.Rect.LocationCenter().X -
                                 endingRoomModule.Rect.LocationCenter().X), 3);
                roadSize = new Size((int)roadSize.Width,(int)roadSize.Height);
                roadPosition = (Point) ((Vector) roadPosition - (Vector) roadSize / 2f);
                roadPosition = new Point((int) roadPosition.X, (int) roadPosition.Y);

                Rect roadRect = new Rect(roadPosition, roadSize);

                roadRects.Add(roadRect);

                //RoomModule road = new RoomModule(roadRect);
                //AddRoad(road);
                //check edges for edginess

                List<Vector> roadPoints = new List<Vector>
                {
                    new Vector((int) roadRect.Left+2, (int) roadRect.LocationCenter().Y),
                    new Vector((int) (roadRect.Right - 1), (int) roadRect.LocationCenter().Y)
                };

                roadPoints.ForEach(point =>
                {
                    RoomModule minRoomModule = rooms.First(room =>
                        Math.Abs(Math.Abs((room.Rect.LocationCenter() - point).Length) -
                                 rooms.Min(v => Math.Abs((v.Rect.LocationCenter() - point).Length))) < 0.1f);
                   
                    Vector roadSecondaryPosition = point;
                    double yPos = (point.Y + minRoomModule.Rect.LocationCenter().Y) / 2f;
                    Size roadSecondarySize =
                        new Size(3, Math.Abs(point.Y - minRoomModule.Rect.LocationCenter().Y) + 2);
                    if (point.Y >= minRoomModule.Rect.Bottom - 1)
                        roadSecondarySize.Height++;

                    roadSecondarySize = new Size((int)roadSecondarySize.Width,(int)roadSecondarySize.Height);
                    roadSecondaryPosition = new Vector((int)roadSecondaryPosition.X, Math.Round(yPos));
                    roadSecondaryPosition = roadSecondaryPosition - (Vector) roadSecondarySize / 2f;
                    roadSecondaryPosition = new Vector((int) roadSecondaryPosition.X, (int)roadSecondaryPosition.Y);
                    
                    Rect roadSecondaryRect = new Rect((Point) roadSecondaryPosition, roadSecondarySize);

                    roadRects.Add(roadSecondaryRect);
                    //RoomModule roadSecondary = new RoomModule(roadSecondaryRect);
                    //AddRoad(roadSecondary);
                });
            }
            
            
            return AddRoads(roadRects, rooms) !=null;
        }

        private List<Road> AddRoads(List<Rect> roadRects, List<RoomModule> rooms)
        {
            List<Road> newRoads = new List<Road>();
            roadRects.ForEach(roadRect => newRoads.Add(new Road(roadRect)));

            int roadIntersects = 0;
            int wallIntersects = 0;
            int floorIntersects = 0;
            newRoads.ForEach(road =>
            {
                road.Tiles.Where(v => Tiles.ContainsKey(v.Key)).ToList().ForEach(tile =>
                {
                    if (Tiles[tile.Key].Type == Tile.TypeEnum.Road && tile.Value.Type == Tile.TypeEnum.Road)
                        roadIntersects++;
                    if (Tiles[tile.Key].Type == Tile.TypeEnum.Wall && tile.Value.Type == Tile.TypeEnum.Road)
                        wallIntersects++;
                    if (Tiles[tile.Key].Type == Tile.TypeEnum.Floor && tile.Value.Type == Tile.TypeEnum.Road)
                        if (rooms.All(room => !room.Tiles.ContainsKey(tile.Key))) floorIntersects++;
                });
            });
            if (roadIntersects > 0 || wallIntersects > 2 || floorIntersects >= GeneratorExtensions.MinRoomSize)
                return null;

            newRoads.ForEach(road =>
            {
                road.Tiles.Where(v => Tiles.ContainsKey(v.Key)).ToList().ForEach(tile =>
                {
                    MapWindow.BackgroundCanvas.Children.Remove(tile.Value.Rect);

                    if (Tiles[tile.Key].Type != Tile.TypeEnum.Wall)
                    {
                        road.Tiles[tile.Key] = Tiles[tile.Key];
                    }
                    else if (tile.Value.Type == Tile.TypeEnum.Road )
                    {
                        Tiles[tile.Key].Type = Tiles[tile.Key].MultiTileShape is RoomModule ? Tile.TypeEnum.Door : Tile.TypeEnum.Road;
                        road.Tiles[tile.Key] = Tiles[tile.Key];
                       /* road.Tiles[tile.Key].Type = Tiles[tile.Key].MultiTileShape is RoomModule ? Tile.TypeEnum.Door : Tile.TypeEnum.Road;
                        Tiles[tile.Key] = road.Tiles[tile.Key];*/
                    }
                });

                road.Tiles.Where(v => !Tiles.ContainsKey(v.Key)).ToList()
                    .ForEach(tile => Tiles.Add(tile.Key, tile.Value));
            });
            rooms.First().Roads.Add(rooms.Last());
            rooms.Last().Roads.Add(rooms.First());

            newRoads.ForEach(road => road.Tiles.Values.ToList().ForEach(tile => tile.InitialiseRect()));
            return newRoads;
        }

/*

        private void AddRoad(RoomModule road)
        {
            Roads.Add(road);

            road.Tiles.Where(v=> Tiles.ContainsKey(v.Key)).ToList().ForEach(tile =>
            {
                MapWindow.BackgroundCanvas.Children.Remove(tile.Value.Rect);
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
*/

        public static int Clamp(double value, double min, double max)
        {
            return (int) (value < min ? min : value > max ? max : value);
        }
        #endregion

    }



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
                    
                    Vector point = pointer.Rotate(eachAngle) * (MaxRoomSize / 2f + roomModule.Rect.Width / 2f + Random.Next(3, SpaceBetweenRooms));
                    point = new Vector((int)point.X, (int)point.Y) + (Vector)roomModule.Rect.Location;

                    if (!MapWindow.MapBasicInfo.Rect.Contains((System.Windows.Point)point)) return;


                    for (int i = MaxRoomSize; i >= MinRoomSize; i-=2)
                    {
                        Rect r = new Rect((System.Windows.Point)point, new Size(i, i));

                        if (!MapWindow.MapBasicInfo.Rect.Contains(r)) continue;

                        if (roomModule.Neighbors.Values.All(room => !room.Rect.IntersectsWith(r)))
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

