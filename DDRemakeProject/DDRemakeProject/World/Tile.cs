using System;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace DDRemakeProject.World
{
    public class Tile : IComparable<Tile>
    {
        #region Properties
        [XmlIgnore]
        public System.Windows.Shapes.Rectangle Rect { get; set; }

        public bool IsWall { get; set; }
        /// <summary>
        /// //TODO
        /// </summary>
        public bool IsDoor { get; set; }
        /// <summary>
        /// The coordonate of the Tile in tPixels
        /// </summary>
        public Point Coord { get; set; }
        /// <summary>
        /// The direction of the wall.
        /// (0,0) if it's not a wall
        /// </summary>
        public Point OuterDirection { get; set; }
        public System.Windows.Media.Color Color { get; set; }
        public RoomSpace RoomDim { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Basic Constructor.
        /// Probably not used
        /// </summary>
        public Tile() { }
        /// <summary>
        /// Instantiate a tile
        /// </summary>
        /// <param name="x"> x coordinate of the Tile in tPixels</param>
        /// <param name="y"> y coordinate of the Tile in tPixels</param>
        public Tile(int x, int y, RoomSpace r)
        {

            this.IsWall = false;
            this.IsDoor = false;
            this.Coord = new Point(x, y);
            this.RoomDim = r;
            this.Color = Colors.Plum;
            InitialiseRect();
        }

        #endregion



        #region Operators
        /// <summary>
        /// Compare function to sort the tiles in the sorted set
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Compare(Tile a, Tile b)
        {
            if (a.Coord.Y > b.Coord.Y) return 1;
            else if (a.Coord.Y == b.Coord.Y)
            {
                if (a.Coord.X > b.Coord.X) return 1;
                else return 0;
            }
            else return 0;
        }
        /// <summary>
        /// Compare function to sort the tiles in the sorted set
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Tile other)
        {
            if (this.Coord.Y > other.Coord.Y) return 1;
            else if (this.Coord.Y == other.Coord.Y)
            {
                if (this.Coord.X > other.Coord.X) return 1;
                else return 0;
            }
            else return 0;
        }



        #endregion

        #region Methods

        public void InitialiseRect()
        {
            Application.Current.Dispatcher.Invoke((System.Action)delegate
            {
                Rect = new System.Windows.Shapes.Rectangle();

                this.Rect.Margin = new Thickness(Coord.X * Constants.TilePx, Coord.Y * Constants.TilePx, 0, 0);
                this.Rect.Width = Constants.TilePx;
                this.Rect.Height = Constants.TilePx;
                this.Rect.Fill = new System.Windows.Media.SolidColorBrush(Color);
                //this.Rect.Fill = System.Windows.Media.Color.FromRgb();
                //this.Rect.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220));
                this.Rect.StrokeThickness = 0;
                // MainWindow.CanvasS1.Children.Add()
                //MainWindow.CanvasS1.Children.Add(this.Rect);
                //MainWindow.CanvasS1.
                MainWindow.CanvasS1.Children.Add(this.Rect);

            });
        }


        public static void ShowTile(Point p, System.Windows.Media.Color c)
        {
            Tile t;
            int pos = -1;
            Application.Current.Dispatcher.Invoke((System.Action)delegate
            {
                t = new Tile(p.X, p.Y, new RoomSpace(0, 0, 0, 0));

                SolidColorBrush s = new SolidColorBrush(c);
                t.Rect.Fill = s;
                pos = MainWindow.CanvasS1.Children.IndexOf(t.Rect);
            });
            //System.Threading.Thread.Sleep(5);


            Application.Current.Dispatcher.Invoke((System.Action)delegate
            {
                if (pos > 0)
                    MainWindow.CanvasS1.Children.RemoveAt(pos);
            });


        }


        #endregion

    }
}
