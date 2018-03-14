using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace DDRemakeProject.World
{
    public class MapShape
    {
        #region Properties

        public static Dictionary<TypeEnum, Brush> TypeBrushes = new Dictionary<TypeEnum, Brush>
        {
            {TypeEnum.Road, Brushes.Crimson},
            {TypeEnum.Room, Brushes.CornflowerBlue},
            {TypeEnum.Player, Brushes.Black},
        };
        public static Dictionary<TypeEnum, int> TypeZIndexDictionary = new Dictionary<TypeEnum, int>
        {
            {TypeEnum.Road,0},
            {TypeEnum.Room, 1},
            {TypeEnum.Player, 2},
        };
        public enum TypeEnum
        {
            Room,
            Road,
            Player,
        }


        private TypeEnum _type;

        [JsonIgnore]
        public Rectangle Rectangle { get; set; }

        public TypeEnum Type
        {
            get => _type;
            set
            {
                _type = value;
                if (Rectangle == null) return;
                Rectangle.Fill = TypeBrushes[value];
            }
        }
        [JsonIgnore]
        public Vector Position
        {
            get => new Vector((int) Rectangle.Margin.Left, (int) Rectangle.Margin.Top) / Constants.TilePx;
            set
            {
                if (MultiTileShape == null)
                {
                    Rect = new Rect((Point)value,new Size(Constants.TilePx,Constants.TilePx));
                }
                else
                {
                    Rect = new Rect((Point)value, MultiTileShape.Rect.Size);

                }

                Rectangle?.SetPosition(value);
            }
        }

        public Rect Rect { get; set; }  

        [JsonIgnore]
        public IMultiTileShape MultiTileShape { get; set; }

        #endregion

        #region Constructors

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
        public MapShape(Vector position, Size size)
        {
            Type = TypeEnum.Room;

            InitialiseRect(position, size);
        }

        public MapShape(Vector position, Size size, TypeEnum type)
        {
            Type = type;

            InitialiseRect(position, size);
        }

        public MapShape()
        {
        }

        #endregion


        #region Methods

        public void InitialiseRect(Vector position, Size size)
        {
            Rect = new Rect((Point) position,size);
            Application.Current.Dispatcher.Invoke(delegate
            {
                Rectangle = new Rectangle();
                Rectangle.SetMinimapPosition(position);
                Rectangle.Width = size.Width;
                Rectangle.Height = size.Height;
                Rectangle.Fill = TypeBrushes[_type];
                Rectangle.StrokeThickness = 0;
                
                
                Panel.SetZIndex(Rectangle,TypeZIndexDictionary[_type]);
                MapWindow.MapCanvas.Children.Add(Rectangle);
            });
        }

        #endregion
    }
}