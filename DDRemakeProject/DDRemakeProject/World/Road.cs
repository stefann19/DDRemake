using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DDRemakeProject.World
{
    public class Road : IMultiTileShape
    {

        public Rect Rect { get; set; }

        /// <summary>
        /// Matrix holding the Image(tiles) objects of the room
        /// </summary>
        public Dictionary<Vector, Tile> Tiles { get; set; }

        /// <summary>
        /// List of Tiles containing all the wall tiles in the Room
        /// </summary>
        public HashSet<Tile> WallTiles { get; set; }

        public List<RoomModule> ConnectedRoomsList { get; set; }

        public Road()
        {

        }

        public Road(Rect size)
        {
            this.Rect = size;
            Generate(false);



        }

        public void Generate(bool loadingFromFile)
        {

            //if (!loadingFromFile)
            //{
            //    Tiles = new Dictionary<Vector, Tile>();
            //    WallTiles = new HashSet<Tile>();
            //}
            Tiles = new Dictionary<Vector, Tile>();
            WallTiles = new HashSet<Tile>();


            int area = (int)(Rect.Width * Rect.Height);
            for (int i = area - 1; i >= 0; i--)
            {
                Vector tilePosition = new Vector(Math.Floor(i % Rect.Size.Width), Math.Floor((i / Rect.Size.Width))) + (Vector)Rect.Location;
                Tile.TypeEnum tileType;

                if (i < Rect.Size.Width || i > area - Rect.Size.Width || Math.Abs(i % Rect.Size.Width) < 0.1f ||
                    Math.Abs(i % Rect.Size.Width - (Rect.Size.Width - 1)) < 0.1f)
                {
                    tileType = Tile.TypeEnum.Wall;
                }
                else
                {

                    tileType = Tile.TypeEnum.Road;
                }

                this.AddTile(tilePosition, tileType);
            }


        }

        /*public static void AddTile(this MultiTileShape multiTileShape,Vector Position, Tile.TypeEnum type)
        {
            Tile t = new Tile(Position,multiTileShape ,type,false);
            //t.InitialiseRect();
            if (multiTileShape.Tiles.ContainsKey(t.Position)) return;
            multiTileShape.Tiles.Add(t.Position, t);

            if (t.Type == Tile.TypeEnum.Wall)
            {
                multiTileShape.WallTiles.Add(t);
            }
        }*/



    }
}
