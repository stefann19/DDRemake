using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using DDRemakeProject.GamePlay;
using DDRemakeProject.WorldGeneration;

namespace DDRemakeProject.World
{
    public class Tile : IComparable<Tile>
    {

        #region Properties
        public static Dictionary<TypeEnum, Brush> TypeBrushes = new Dictionary<TypeEnum, Brush>
        {
            {TypeEnum.Door,Brushes.Crimson },
            {TypeEnum.Floor,Brushes.Cyan },
            {TypeEnum.Wall,Brushes.Black },
            {TypeEnum.Road,Brushes.Chocolate }
        };

        public enum TypeEnum
        {
            Wall,
            Door,
            Floor,
            Road
        }


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
                if (Rect == null) return PositionWhileNotIntialised;
                return new Vector((int)Rect.Margin.Left,(int) Rect.Margin.Top)/Constants.TilePx;
            }
            set => Rect?.SetPosition(value);
        }
/*
        public Vector LocalPosition 
        {
            get
            { 
            if (Rect == null) return VectorExtensions.Empty;
                return new Vector((int)Rect.Margin.Left, (int)Rect.Margin.Top) -(Vector)RoomModule.RoomRect.Location;
            }    
        }
*/
        private Vector PositionWhileNotIntialised;


        public IMultiTileShape MultiTileShape { get; set; }
        
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
        //public Tile(Vector position,TypeEnum type)
        //{

        //    this.Type = type;
        //    //this.RoomModule =roomModule;
        //    InitialiseRect(position);
        //}

        public Tile(Vector position)
        {

            this.Type = TypeEnum.Floor;
            PositionWhileNotIntialised = position;

            InitialiseRect();
        }

        public Tile(Vector position,IMultiTileShape multiTileShape ,TypeEnum type,bool initialise = true)
        {

            this.Type = type;
            this.MultiTileShape = multiTileShape;
            PositionWhileNotIntialised = position;

            if (initialise) InitialiseRect();
            //InitialiseRect(position);
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

        public void InitialiseRect()
        {
            Application.Current.Dispatcher.Invoke((System.Action)delegate
            {
                Rect = new System.Windows.Shapes.Rectangle();
                this.Rect.SetPosition(PositionWhileNotIntialised);
                this.Rect.Width = Constants.TilePx;
                this.Rect.Height = Constants.TilePx;
                this.Rect.Fill = TypeBrushes[_type];
                this.Rect.StrokeThickness = 0;
                MapWindow.BackgroundCanvas.Children.Add(this.Rect);

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
