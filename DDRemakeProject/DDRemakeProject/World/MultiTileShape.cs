using System.Collections.Generic;
using System.Windows;

namespace DDRemakeProject.World
{
    public interface IMultiTileShape
    {
        Rect Rect { get; set; }

        /// <summary>
        ///     Matrix holding the Image(tiles) objects of the room
        /// </summary>
        Dictionary<Vector, Tile> Tiles { get; set; }

        /// <summary>
        ///     List of Tiles containing all the wall tiles in the Room
        /// </summary>
        HashSet<Tile> WallTiles { get; set; }

        void Generate(bool loadingFromFile);
    }

    public static class MultiTileShapeExtensions
    {
        public static void AddTile(this IMultiTileShape multiTileShape, Vector position, Tile.TypeEnum type)
        {
            Tile t = new Tile(position, multiTileShape, type, multiTileShape is RoomModule);
            if (multiTileShape.Tiles.ContainsKey(t.Position)) return;
            multiTileShape.Tiles.Add(t.Position, t);

            if (t.Type == Tile.TypeEnum.Wall)
                multiTileShape.WallTiles.Add(t);
        }
    }
}