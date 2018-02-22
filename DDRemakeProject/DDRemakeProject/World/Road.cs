using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DDRemakeProject.World
{
    public class Road
    {

        public Rect RoadRect { get; set; }

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
            this.RoadRect = size;
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


            int area = (int)(RoadRect.Width * RoadRect.Height);
            for (int i = area - 1; i >= 0; i--)
            {
                Vector tilePosition = new Vector(Math.Floor(i % RoadRect.Size.Width), Math.Floor((i / RoadRect.Size.Width))) + (Vector)RoadRect.Location;
                Tile.TypeEnum tileType;

                if (i < RoadRect.Size.Width || i > area - RoadRect.Size.Width || Math.Abs(i % RoadRect.Size.Width) < 0.1f ||
                    Math.Abs(i % RoadRect.Size.Width - (RoadRect.Size.Width - 1)) < 0.1f)
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

        public void AddTile(Vector Position, Tile.TypeEnum type)
        {
            Tile t = new Tile(Position, type,false);
            //t.InitialiseRect();
            if (this.Tiles.ContainsKey(t.Position)) return;
            this.Tiles.Add(t.Position, t);

            if (t.Type == Tile.TypeEnum.Wall)
            {
                this.WallTiles.Add(t);
            }
        }



    }
}
