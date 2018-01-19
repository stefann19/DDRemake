using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using DDRemakeProject.GamePlay;
using DDRemakeProject.WorldGeneration;
using Point = DDRemakeProject.Deprecated.Point;

namespace DDRemakeProject.World
{
    public class Tile : IComparable<Tile>
    {   
        public static Dictionary<TypeEnum, Brush> TypeBrushes = new Dictionary<TypeEnum, Brush>
        {
            {TypeEnum.Door,Brushes.ForestGreen },
            {TypeEnum.Floor,Brushes.Cyan },
            {TypeEnum.Wall,Brushes.Black },

        };
        
        public enum TypeEnum
        {
            Wall,
            Door,
            Floor
        }

        #region Properties

        private TypeEnum _type;
        [XmlIgnore]
        public System.Windows.Shapes.Rectangle Rect { get; set; }

        public TypeEnum Type
        {
            get => _type;
            set
            {
                _type = value;
                if (Rect == null) return;
                Rect.Fill = TypeBrushes[value];
            }
        }

        public Vector Position
        {
            get
            {
                if (Rect == null) return VectorExtensions.Empty;
                return new Vector((int)Rect.Margin.Left,(int) Rect.Margin.Top)/Constants.TilePx;
            }
        }
        public Vector LocalPosition 
        {
            get
            { 
            if (Rect == null) return VectorExtensions.Empty;
                return new Vector((int)Rect.Margin.Left, (int)Rect.Margin.Top) -(Vector)RoomModule.RoomRect.Location;
            }    
        }


    /// <summary>
    /// The direction of the wall.
    /// (0,0) if it's not a wall
    /// </summary>
    public Vector OuterDirection => ((Vector)RoomModule.RoomRect.Location - Position).NormalizeRet();

        public RoomModule RoomModule { get; set; }


        //public System.Windows.Media.Color Color { get; set; }
        
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
        public Tile(Vector position,RoomModule roomModule,TypeEnum type)
        {

            this.Type = type;
            this.RoomModule =roomModule;
            InitialiseRect(position);
        }

        public Tile(Vector position)
        {

            this.Type = TypeEnum.Floor;
           
            InitialiseRect(position);
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
            if (a.Position.Y > b.Position.Y) return 1;
            else if (a.Position.Y == b.Position.Y)
            {
                if (a.Position.X > b.Position.X) return 1;
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
            if (this.Position.Y > other.Position.Y) return 1;
            else if (this.Position.Y == other.Position.Y)
            {
                if (this.Position.X > other.Position.X) return 1;
                else return 0;
            }
            else return 0;
        }



        #endregion

        #region Methods

        public void InitialiseRect(Vector position)
        {
            Application.Current.Dispatcher.Invoke((System.Action)delegate
            {
                Rect = new System.Windows.Shapes.Rectangle();

                this.Rect.Margin = new Thickness(position.X * Constants.TilePx, position.Y * Constants.TilePx, 0, 0);
                this.Rect.Width = Constants.TilePx;
                this.Rect.Height = Constants.TilePx;
                this.Rect.Fill = TypeBrushes[_type];
                //this.Rect.Fill = new System.Windows.Media.SolidColorBrush(Color);
                //this.Rect.Fill = System.Windows.Media.Color.FromRgb();
                //this.Rect.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220));
                this.Rect.StrokeThickness = 0;
                // MainWindow.CanvasS1.Children.Add()
                //MainWindow.CanvasS1.Children.Add(this.Rect);
                //MainWindow.CanvasS1.
                MainWindow.CanvasS1.Children.Add(this.Rect);

            });
        }



        //public static void ShowTile(Vector p, System.Windows.Media.Color c)
        //{
        //    Tile t;
        //    int pos = -1;
        //    Application.Current.Dispatcher.Invoke((System.Action)delegate
        //    {
        //        t = new Tile(p,null);

        //        SolidColorBrush s = new SolidColorBrush(c);
        //        t.Rect.Fill = s;
        //        pos = MainWindow.CanvasS1.Children.IndexOf(t.Rect);
        //    });
        //    //System.Threading.Thread.Sleep(5);


        //    Application.Current.Dispatcher.Invoke((System.Action)delegate
        //    {
        //        if (pos > 0)
        //            MainWindow.CanvasS1.Children.RemoveAt(pos);
        //    });


        //}


        #endregion

    }
}
