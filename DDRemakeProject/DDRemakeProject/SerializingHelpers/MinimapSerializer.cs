using System.Collections.Generic;
using DDRemakeProject.World;

namespace DDRemakeProject.SerializingHelpers
{
    public class MinimapSerializer
    {
        public HashSet<MapShape> MapShapes { get; set; }

        public MinimapSerializer()
        {
            if (true)
            {

            }
/*
            MapShapes.ForEach(shape => shape.InitialiseRect(shape.Position));
*/
        }

        public MinimapSerializer(HashSet<MapShape> mapShapes)
        {
            MapShapes = mapShapes;
        }
    }
}