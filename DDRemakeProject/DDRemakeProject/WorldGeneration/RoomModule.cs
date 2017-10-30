using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Threading;
using System.Windows.Media;

namespace DDRemakeProject.WorldGeneration
{
    public class RoomModule
    {
        /// <summary>
        ///  Room's size starting from the top right corner to the right
        /// </summary>
        public int SizeX { get; set; }
        /// <summary>
        ///  Room's size starting from the top right corner to the bottom
        /// </summary>
        public int SizeY { get; set; }

        /// <summary>
        ///  The x coordinate of the top right corner of the room
        /// </summary>
        public int X { get; set; }
        /// <summary>
        ///  The y coordinate of the top right corner of the room
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Matrix holding the Image(tiles) objects of the room
        /// </summary>
        public Tile[][] Tiles { get; set; }

        /// <summary>
        /// List of Tiles containing all the wall tiles in the Room
        /// </summary>
        public List<Tile> WallTiles { get; set; }
        /// <summary>
        ///  Contruct room by giving the top right corner coordinates and room size in tPixels
        /// </summary>
        /// 
        public static RoomSpace CheckForSpace(Point coord,Tile[][] dungeon,Point size,int scanSize)
        {
            int lX=0, rX=0, tY=0, bY=0;
            if(!(coord.X>=0 && coord.X< size.X && coord.Y>=0 && coord.Y < size.Y)) return new RoomSpace(lX, rX, tY, bY);
            if (dungeon[coord.X][coord.Y] != null) return new RoomSpace(lX, rX, tY, bY);
            bool breaked=false;
            int k;

            //Top
            k = 1;
            //int scanSize=6;
            for (int j = coord.Y - 1; j >= coord.Y - scanSize; j--)
            {
                for(int i = coord.X -k; i <= coord.X +k; i++)
                {
                    if (j >= size.Y || j<0 || i >= size.X || i < 0 ) tY = j + 1;
                    else if(dungeon[i][j] != null)tY = j+ 1;
                    if (tY != 0)
                    {
                        breaked = true;
                        break;
                    }
                    Tile.ShowTile(new Point(i,j), System.Windows.Media.Color.FromRgb(0, 0, 255));
                }
                if (breaked)
                {
                    break;
                }
                k++;

            }
            if (!breaked)
            {
                tY = coord.Y -scanSize;
            }
            breaked = false;

            //bottom
            k = 1;
            for (int j = coord.Y +1 ; j <= coord.Y + scanSize; j++)
            {
                for (int i = coord.X - k; i <= coord.X + k; i++)
                {
                    if (j >= size.Y || j < 0 || i >= size.X || i < 0) bY = j - 1;
                    else if (dungeon[i][j] != null) bY = j - 1;
                    if (bY != 0)
                    {
                        breaked = true;
                        break;
                    }
                    Tile.ShowTile(new Point(i,j), System.Windows.Media.Color.FromRgb(0, 255, 255));

                }
                k++;
                if (breaked)
                {
                    break;
                }
            }
            if (!breaked)
            {
                bY = coord.Y + scanSize;
            }
            breaked = false;

            //right
            k = 1;
            for (int i = coord.X + 1; i <= coord.X + scanSize; i++)
            {
                for (int j = coord.Y - k; j <= coord.Y + k; j++)
                {
                    if (j >= size.Y || j < 0 || i >= size.X || i < 0) rX = i - 1;
                    else if ( dungeon[i][j] != null) rX = i - 1;
                    if (rX != 0)
                    {
                        breaked = true;
                        break;
                    }
                    Tile.ShowTile(new Point(i, j), System.Windows.Media.Color.FromRgb(255, 0, 255));

                }
                k++;
                if (breaked)
                {
                    break;
                }
            }
            if (!breaked)
            {
                rX = coord.X + scanSize;
            }
            breaked = false;

            // left
            k = 1;
            for (int i = coord.X -1; i >= coord.X - scanSize; i--)
            {
                for (int j = coord.Y - k; j <= coord.Y + k; j++)
                {
                    if (j >= size.Y || j < 0 || i >= size.X || i < 0) lX = i + 1;
                    else if (dungeon[i][j] != null) lX = i + 1;
                    if (lX != 0)
                    {
                        breaked = true;
                        break;
                    }
                    Tile.ShowTile(new Point(i, j), System.Windows.Media.Color.FromRgb(120, 255, 255));

                }
                k++;
                if (breaked)
                {
                    break;
                }
            }
            if (!breaked)
            {
                lX = coord.X - scanSize;
            }
            breaked = false;
            RoomSpace rm = new RoomSpace(lX, rX, tY, bY);
            Tile.ShowTile(rm.GetTopLeft()               , System.Windows.Media.Color.FromRgb(0, 100, 0));
            Tile.ShowTile(rm.GetSize()+rm.GetTopLeft()  , System.Windows.Media.Color.FromRgb(0, 100, 0));
            Tile.ShowTile(rm.GetTopLeft(), System.Windows.Media.Color.FromRgb(0, 100, 0));
            Tile.ShowTile(rm.GetSize() + rm.GetTopLeft(), System.Windows.Media.Color.FromRgb(0, 100, 0));
            Tile.ShowTile(rm.GetTopLeft(), System.Windows.Media.Color.FromRgb(0, 100, 0));
            Tile.ShowTile(rm.GetSize() + rm.GetTopLeft(), System.Windows.Media.Color.FromRgb(0, 100, 0));
            Tile.ShowTile(rm.GetTopLeft(), System.Windows.Media.Color.FromRgb(0, 100, 0));
            Tile.ShowTile(rm.GetSize() + rm.GetTopLeft(), System.Windows.Media.Color.FromRgb(0, 100, 0));
            return rm;
        }



        public RoomModule()
        {

        }
        public RoomModule(int x, int y, int sizeX, int sizeY)
        {
            this.X = x;
            this.Y = y;
            this.SizeX = sizeX;
            this.SizeY = sizeY;

            //string floorPath = "pack://application:,,,/Textures/sand_";
            //string wallImg = "pack://application:,,,/Textures/rect_gray_";

            //tiles = new Image[((int)this.SizeX / 32)][((int)this.SizeY / 32)];
            Generate(false);

        }

        public void Generate(bool load)
        {
            if (!load)
            {
                Tiles = new Tile[SizeY + 1][];
                WallTiles = new List<Tile>();
            }
            //Random rnd = new Random();
            RoomSpace rs = new RoomSpace(X, SizeX + X, Y, Y + SizeY);
            for (int j = 0; j <= this.SizeY; j++)
            {
                if (!load)
                {
                    Tiles[j] = new Tile[SizeX + 1];
                }
                for (int i = 0; i <= this.SizeX; i++)
                {
                    //t;
                    Tile t;
                    if (load)
                    {
                        t = Tiles[j][i];
                        t.InitialiseRect();


                    }
                    else
                    {
                        t = new Tile(this.X + i, this.Y + j, rs);
                        Tiles[j][i] = (t);

                        System.Windows.Media.Color c = Colors.Plum;
                        if (t.Color != null)
                        {
                            c = t.Color;
                        }
                        bool top, bottom, left, right, corner;

                        top = j == 0;
                        bottom = j == this.SizeY;
                        left = i == 0;
                        right = i == this.SizeX;
                        corner = ((top & (left || right)) || (bottom & (left || right)));
                        if (!top && !bottom && !right && !left)
                        {
                            //it's a floor
                            //t.Source = new BitmapImage(new Uri(floorPath + rnd.Next(1, 9) + ".png"));
                            Application.Current.Dispatcher.Invoke((System.Action) delegate
                            {
                                t.Color = Colors.LightCyan;
                                t.Rect.Fill = System.Windows.Media.Brushes.LightCyan;
                            });
                        }
                        else
                        {
                            // it's a wall
                            //t.Source = new BitmapImage(new Uri(wallImg + rnd.Next(0, 4) + "_new.png"));
                            if (corner)
                            {
//it's a corner
                                Application.Current.Dispatcher.Invoke((System.Action) delegate
                                {
                                    t.Color = Colors.Black;
                                    t.Rect.Fill = System.Windows.Media.Brushes.Black;
                                });
                            }
                            else
                            {
//just a wall
                                Application.Current.Dispatcher.Invoke((System.Action) delegate
                                {
                                    t.Color = Colors.DarkCyan;

                                    t.Rect.Fill = System.Windows.Media.Brushes.DarkCyan;
                                });
                                t.IsWall = true;
                                if (top)
                                {
                                    t.OuterDirection = new Point(0, -1);
                                }
                                else if (bottom)
                                {
                                    t.OuterDirection = new Point(0, 1);
                                }
                                if (left)
                                {
                                    t.OuterDirection = new Point(-1, 0);
                                }
                                else if (right)
                                {
                                    t.OuterDirection = new Point(1, 0);
                                }

                                WallTiles.Add(t); //if not corner


                            }
                        }

                        if (c != Colors.Plum)
                        {
                            Application.Current.Dispatcher.Invoke((System.Action) delegate
                            {
                                t.Rect.Fill = new System.Windows.Media.SolidColorBrush(c);
                            });
                        }
                    }

                }
            }
        }


    }
}
