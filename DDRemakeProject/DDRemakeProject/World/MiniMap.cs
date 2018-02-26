using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Point = System.Windows.Point;
using Size = System.Windows.Size;


namespace DDRemakeProject.World
{
    public class MiniMap
    {
        public MiniMap(Vector scale)
        {
            VisitedShapes = new HashSet<IMultiTileShape>();
            Scale = scale;
            Tiles = new HashSet<Tile>();
        }

        public MiniMap(HashSet<IMultiTileShape> visitedShape, Canvas miniMapCanvas)
        {
            VisitedShapes = visitedShape;
            MiniMapCanvas = miniMapCanvas;
        }

        public HashSet<IMultiTileShape> VisitedShapes { get; set; }
        public Vector Scale { get; set; }
        public Canvas MiniMapCanvas { get; set; }
        public HashSet<Tile> Tiles { get; set; }

        public void AddVisitedItem(IMultiTileShape shape)
        {
            if (VisitedShapes.Contains(shape)) return;
            else
            {
                Point position =new Point(shape.Rect.Location.X/Scale.X,shape.Rect.Location.Y/Scale.Y);
                Size size = new Size(shape.Rect.Size.Width/Scale.X,shape.Rect.Size.Height/Scale.Y);

                Rect rectangle = new Rect(position,size);
                Tile t = new Tile((Vector)position,size);
                Tiles.Add(t);
                VisitedShapes.Add(shape);
            }
        }
    }
}
