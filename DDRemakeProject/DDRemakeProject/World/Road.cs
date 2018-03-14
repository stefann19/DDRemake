using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using DDRemakeProject.WorldGeneration;
using Newtonsoft.Json;

namespace DDRemakeProject.World
{
    public class Road : IMultiTileShape
    {
        [JsonIgnore]
        public Generator Generator { get; set; }
        public Rect Rect { get; set; }

        /// <summary>
        /// Matrix holding the Image(tiles) objects of the room
        /// </summary>
        public Dictionary<Vector, Tile> Tiles { get; set; }

        /// <summary>
        /// List of Tiles containing all the wall tiles in the Room
        /// </summary>
        [JsonIgnore]
        public HashSet<Tile> WallTiles { get; set; }
        [JsonIgnore]
        public List<RoomModule> ConnectedRoomsList { get; set; }

        public Road()
        {

        }

        public Road(Rect size,Generator generator)
        {
            Generator = generator;
            this.Rect = size;
            Generate(false,Generator);



        }

        public void Generate(bool loadingFromFile,Generator generator)
        {
            Generator = generator;
            //if (!loadingFromFile)
            //{
            //    Tiles = new Dictionary<Vector, Tile>();
            //    WallTiles = new HashSet<Tile>();
            //}
            if (loadingFromFile)
            {
                RebuildTiles();
                return;
            }

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

        private void RebuildTiles()
        {
            Dictionary<Vector,Tile> tilesCopy = new Dictionary<Vector, Tile>(Tiles);
            Tiles = new Dictionary<Vector, Tile>();
            WallTiles  = new HashSet<Tile>();
            tilesCopy.Values.ToList().ForEach(tile =>
            {
                Tile t =this.AddTile(tile.Position,tile.Type);
                t.InitialiseRect(new Size(Constants.TilePx,Constants.TilePx));
                if (Generator.Tiles.ContainsKey(t.Position))
                {
                    if(t.Type == Tile.TypeEnum.Door)(Generator.Tiles[t.Position].MultiTileShape as RoomModule)?.Roads.Add(this);

                    if (t.Type==Tile.TypeEnum.Road || t.Type == Tile.TypeEnum.Door)
                    Generator.Tiles[t.Position] = t;

                   
                }else
                Generator.Tiles.Add(t.Position,t);
            });
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
