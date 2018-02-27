using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

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

        [XmlIgnore]
        public Rectangle Rect { get; set; }

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
            get => new Vector((int) Rect.Margin.Left, (int) Rect.Margin.Top) / Constants.TilePx;
            set => Rect?.SetPosition(value);
        }


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

        #endregion


        #region Methods

        public void InitialiseRect(Vector position, Size size)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                Rect = new Rectangle();
                Rect.SetMinimapPosition(position);
                Rect.Width = size.Width;
                Rect.Height = size.Height;
                Rect.Fill = TypeBrushes[_type];
                Rect.StrokeThickness = 0;
                
                
                Panel.SetZIndex(Rect,TypeZIndexDictionary[_type]);
                MapWindow.MapCanvas.Children.Add(Rect);
            });
        }

        #endregion
    }
}