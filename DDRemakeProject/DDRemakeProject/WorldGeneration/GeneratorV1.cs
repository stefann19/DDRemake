using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace DDRemakeProject.WorldGeneration
{
    public class GeneratorV1
    {
        private int _nrOfUsedtiles;

        /// <summary>
        ///     Generate a map of tiles using own algorithm
        /// </summary>
        /// <param name="sizeX">Worlds width in number of tiles</param>
        /// <param name="sizeY">Worlds height in number of tiles</param>
        public GeneratorV1(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Generate(sizeX, sizeY);
        }

        public GeneratorV1()
        {
        }

        /// <summary>
        ///     Width of the map in tPixels
        /// </summary>
        public int SizeX { get; set; }

        /// <summary>
        ///     Height of the map in tPixels
        /// </summary>
        public int SizeY { get; set; }
        [XmlIgnore]
        public List<Tile> GenerationTiles { get; set; }
        public List<RoomModule> Rooms { get; set; }
        public Tile[][] TilesMatrix { get; set; }


        private RoomModule MakeRoom(Point coord, Point size)
        {
            if (coord.X > SizeX) return null;
            if (coord.Y > SizeY) return null;
            if (coord.X + size.X > SizeX) return null;
            if (coord.Y + size.Y > SizeY) return null;

            var rm = new RoomModule(coord.X, coord.Y, size.X, size.Y);
            Rooms.Add(rm);
            GenerationTiles.Remove(GenerationTiles.First());
            foreach (var t in rm.WallTiles)
                GenerationTiles.Add(t);

            foreach (var columns in rm.Tiles)
            foreach (var t in columns)
            {
                _nrOfUsedtiles++;
                if (t.Coord.X < SizeX && t.Coord.Y < SizeY && t.Coord.X > 0 && t.Coord.Y > 0)
                    TilesMatrix[t.Coord.X][t.Coord.Y] = t;
            }
            return rm;
        }


        public void MakeDoor(Tile t)
        {
            if (t.IsWall)
            {
                var p = t.Coord + t.OuterDirection;
                if (p.X > 0 && p.X < SizeX && p.Y > 0 && p.Y < SizeY)
                    if (TilesMatrix[p.X][p.Y] != null)
                    {
                        if (TilesMatrix[p.X][p.Y].IsWall)
                        {
                            t.Color = Colors.LightCyan;
                            TilesMatrix[p.X][p.Y].Color = Colors.LightCyan;
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                t.Rect.Fill = Brushes.LightCyan;
                                TilesMatrix[p.X][p.Y].Rect.Fill = Brushes.LightCyan;
                            });
                        }

                        if (t.OuterDirection.IsTop() || t.OuterDirection.IsBottom())
                        {
                            var k = 1;
                            while (true)
                                if (p.X + k < SizeX)
                                {
                                    if (TilesMatrix[p.X + k][p.Y] != null &&
                                        TilesMatrix[t.Coord.X + k][t.Coord.Y] != null)
                                        if (TilesMatrix[p.X + k][p.Y].IsWall)
                                        {
                                            TilesMatrix[p.X + k][p.Y].IsWall = false;
                                            TilesMatrix[t.Coord.X + k][t.Coord.Y].IsWall = false;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    k++;
                                }
                                else
                                {
                                    break;
                                }
                            k = 1;
                            while (true)
                                if (p.X - k > 0)
                                {
                                    if (TilesMatrix[p.X - k][p.Y] != null &&
                                        TilesMatrix[t.Coord.X - k][t.Coord.Y] != null)
                                        if (TilesMatrix[p.X - k][p.Y].IsWall)
                                        {
                                            TilesMatrix[p.X - k][p.Y].IsWall = false;
                                            TilesMatrix[t.Coord.X - k][t.Coord.Y].IsWall = false;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    k++;
                                }
                                else
                                {
                                    break;
                                }
                        }
                        if (t.OuterDirection.IsLeft() || t.OuterDirection.IsRight())
                        {
                            var k = 1;
                            while (true)
                                if (p.Y + k < SizeY)
                                {
                                    if (TilesMatrix[p.X][p.Y + k] != null &&
                                        TilesMatrix[t.Coord.X][t.Coord.Y + k] != null)
                                        if (TilesMatrix[p.X][p.Y + k].IsWall)
                                        {
                                            TilesMatrix[p.X][p.Y + k].IsWall = false;
                                            TilesMatrix[t.Coord.X][t.Coord.Y + k].IsWall = false;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    k++;
                                }
                                else
                                {
                                    break;
                                }
                            k = 1;
                            while (true)
                                if (p.Y - k > 0)
                                {
                                    if (TilesMatrix[p.X][p.Y - k] != null &&
                                        TilesMatrix[t.Coord.X][t.Coord.Y - k] != null)
                                        if (TilesMatrix[p.X][p.Y - k].IsWall)
                                        {
                                            TilesMatrix[p.X][p.Y - k].IsWall = false;
                                            TilesMatrix[t.Coord.X][t.Coord.Y - k].IsWall = false;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    k++;
                                }
                                else
                                {
                                    break;
                                }
                        }
                    }
            }
        }


        public void MakeDoorBetter(RoomModule otherRoom, Tile callingRoomTile)
        {
            double x2, x1, x21;
            x1 = (callingRoomTile.RoomDim.LeftX + callingRoomTile.RoomDim.RightX) / 2;
            x2 = otherRoom.X + otherRoom.SizeX / 2;
            x21 = (x2 + x1) / 2f;

            double y2, y1, y21;
            y1 = (callingRoomTile.RoomDim.TopY + callingRoomTile.RoomDim.BotY) / 2;
            y2 = otherRoom.Y + otherRoom.SizeY / 2;
            y21 = (y2 + y1) / 2f;
            if ((callingRoomTile.OuterDirection.IsTop() || callingRoomTile.OuterDirection.IsBottom()) &&
                (callingRoomTile.Coord.Y - (otherRoom.Y + otherRoom.SizeY) == 1 ||
                 callingRoomTile.RoomDim.BotY - otherRoom.Y == -1))
            {
                if (x21 > otherRoom.X && x21 < callingRoomTile.RoomDim.RightX ||
                    x21 < otherRoom.X + otherRoom.SizeX && x21 > callingRoomTile.RoomDim.LeftX)
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        if (callingRoomTile.OuterDirection.IsTop())
                        {
                            TilesMatrix[(int) x21][callingRoomTile.Coord.Y].Rect.Fill = Brushes.ForestGreen;
                            TilesMatrix[(int) x21][callingRoomTile.Coord.Y - 1].Rect.Fill = Brushes.ForestGreen;
                        }
                        else
                        {
                            TilesMatrix[(int) x21][callingRoomTile.Coord.Y].Rect.Fill = Brushes.ForestGreen;
                            TilesMatrix[(int) x21][callingRoomTile.Coord.Y + 1].Rect.Fill = Brushes.ForestGreen;
                        }
                    });
            }
            else if ((callingRoomTile.OuterDirection.IsLeft() || callingRoomTile.OuterDirection.IsRight()) &&
                     (callingRoomTile.Coord.X - (otherRoom.X + otherRoom.SizeX) == 1 ||
                      callingRoomTile.RoomDim.RightX - otherRoom.X == -1))
            {
                if (y21 < otherRoom.Y + otherRoom.SizeY && y21 > callingRoomTile.RoomDim.TopY ||
                    y21 > otherRoom.Y + otherRoom.SizeX && y21 < callingRoomTile.RoomDim.BotY)
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        if (callingRoomTile.OuterDirection.IsLeft())
                        {
                            TilesMatrix[callingRoomTile.Coord.X][(int) y21].Rect.Fill = Brushes.ForestGreen;
                            TilesMatrix[callingRoomTile.Coord.X - 1][(int) y21].Rect.Fill = Brushes.ForestGreen;
                        }
                        else
                        {
                            TilesMatrix[callingRoomTile.Coord.X][(int) y21].Rect.Fill = Brushes.ForestGreen;
                            TilesMatrix[callingRoomTile.Coord.X + 1][(int) y21].Rect.Fill = Brushes.ForestGreen;
                        }
                    });
            }
        }

        public void Generate(int sizeX, int sizeY)
        {
            GenerationTiles = new List<Tile>();
            Rooms = new List<RoomModule>();
            TilesMatrix = new Tile[sizeX][];
            for (var i = 0; i < sizeX; i++) TilesMatrix[i] = new Tile[sizeY];


            _nrOfUsedtiles = 0;

            var rnd = new Random();
            var startingPosX = sizeX / 2;
            var startingPosy = sizeY / 2;

            GenerationTiles.Add(new Tile(startingPosX, startingPosy,
                new RoomSpace(startingPosX, startingPosX + sizeX, startingPosy, startingPosy + sizeY))
            {
                OuterDirection = new Point(0, 0)
            });

            while (_nrOfUsedtiles <= sizeX * SizeY * 0.4f && GenerationTiles.Count > 0)
            {
                var current = GenerationTiles.First();
                Tile.ShowTile(current.Coord, Color.FromRgb(255, 0, 0));
                //MessageBox.Show(generationTiles.First().outerDirection.X.ToString());
                if (GenerationTiles.First().OuterDirection.IsEmpty())
                {
                    // STARTING TILE
                    var r = RoomModule.CheckForSpace(current.Coord, TilesMatrix, new Point(SizeX, SizeY), 5);
                    MakeRoom(r.GetTopLeft(), r.GetSize());
                }
                else
                {
                    Point derivedP;
                    var aux = rnd.Next(4, 6);
                    derivedP = current.Coord + current.OuterDirection * aux;


                    Tile.ShowTile(derivedP, Color.FromRgb(0, 255, 0));

                    var r = RoomModule.CheckForSpace(derivedP, TilesMatrix, new Point(SizeX, SizeY), aux);
                    if (r.GetSize().X > aux + 1 && r.GetSize().Y > aux + 1)
                    {
                        var rm = MakeRoom(r.GetTopLeft(), r.GetSize());
                        //MakeDoorBetter(rm, current);
                    }
                    else
                    {
                        GenerationTiles.Remove(GenerationTiles.First());
                    }
                }
                MakeDoor(current);
                Thread.Sleep(10);
            }
        }

        public void Generate()
        {
            foreach (var rm in Rooms)
            {
                rm.Generate(true);
                Thread.Sleep(10);
            }
        }
    }
}