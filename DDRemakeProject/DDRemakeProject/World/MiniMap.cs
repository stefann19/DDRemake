using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DDRemakeProject.GamePlay.Old;
using Newtonsoft.Json;
using Point = System.Windows.Point;
using Size = System.Windows.Size;


namespace DDRemakeProject.World
{
    public class MiniMap
    {
        public MiniMap(Vector scale,Engine engine)
        {
            MiniMapCanvas = engine.MapWindow.Canvas_MiniMap;

            VisitedShapes = new HashSet<IMultiTileShape>();
            scale = new Vector(scale.X * engine.Generator.Size.X / 100, scale.Y * engine.Generator.Size.Y / 100).Normalized()* 285;
            //engine.Generator.Size.X / engine.MapWindow.Canvas.Height;
            double scaleX = engine.Generator.Size.X * Constants.TilePx / (scale.X );
            double scaleY = engine.Generator.Size.Y * Constants.TilePx / (scale.Y );
            Scale =  new Vector(scaleX,scaleY);

            //Scale = scale;


            Border = engine.MapWindow.Border_MiniMap;
            Tiles = new HashSet<MapShape>();
            Engine = engine;
            ResizeMiniMap(scale);

            PlayerMapShape = GeneratePlayerShape();

        }

        public MiniMap(HashSet<IMultiTileShape> visitedShape)
        {
            VisitedShapes = visitedShape;
        }

        public MiniMap()
        {
        }

        [JsonIgnore]
        public HashSet<IMultiTileShape> VisitedShapes { get; set; }
        public Vector Scale { get; set; }
        public HashSet<MapShape> Tiles { get; set; }
        [JsonIgnore]
        public Engine Engine { get; set; }
        [JsonIgnore]
        public Canvas MiniMapCanvas { get; set; }
        [JsonIgnore]
        public Border Border { get; set; }

        public MapShape PlayerMapShape { get; set; }

        private void ResizeMiniMap(Vector size)
        {
            MiniMapCanvas.Width = size.X;
            MiniMapCanvas.Height = size.Y;
            MiniMapCanvas.Margin = new Thickness(MiniMapCanvas.Margin.Left +(200-size.X), MiniMapCanvas.Margin.Top,0,0);
            Border.Width = size.X+20;
            Border.Height = size.Y+20;
        }

        private MapShape GeneratePlayerShape()
        {
            Point position = new Point(Engine.Player.Tile.Position.X * Constants.TilePx / Scale.X, Engine.Player.Tile.Position.Y * Constants.TilePx / Scale.Y);
            Size size = new Size(3 * Constants.TilePx / Scale.X,3 * Constants.TilePx / Scale.Y);
            //position = new Point(position.X + Engine.MapWindow.Canvas_MiniMap.Margin.Left,position.Y +Engine.MapWindow.Canvas_MiniMap.Margin.Top);

            Rect rectangle = new Rect(position, size);
            MapShape t = new MapShape((Vector)position, size,MapShape.TypeEnum.Player);

            return t;
        }

        public void MovePlayer()
        {
            Point minimMapPosition = new Point((Engine.Player.Tile.Position.X-1) * Constants.TilePx / Scale.X, (Engine.Player.Tile.Position.Y-1) * Constants.TilePx / Scale.Y);
            PlayerMapShape.Rectangle.SetMinimapPosition((Vector)minimMapPosition);
        }

        public MapShape AddVisitedItem(IMultiTileShape shape)
        {
            if (VisitedShapes.Contains(shape)) return null;
            else
            {
                Point position =new Point(shape.Rect.Location.X * Constants.TilePx / Scale.X, shape.Rect.Location.Y * Constants.TilePx / Scale.Y);
                Size size = new Size(shape.Rect.Size.Width *Constants.TilePx  /Scale.X , shape.Rect.Size.Height * Constants.TilePx / Scale.Y );
                //position = new Point(position.X + Engine.MapWindow.Canvas_MiniMap.Margin.Left,position.Y +Engine.MapWindow.Canvas_MiniMap.Margin.Top);

                Rect rectangle = new Rect(position,size);
                MapShape t = new MapShape((Vector)position,size,shape is RoomModule ? MapShape.TypeEnum.Room : MapShape.TypeEnum.Road);
                Tiles.Add(t);
                VisitedShapes.Add(shape);

                if (shape is RoomModule)
                {
                    RoomModule s = (RoomModule)shape;
                    s.Roads.ForEach(road=> AddVisitedItem(road));
                }

                return t;
            }
            
        }


    }
}
