using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDRemakeProject.World;

namespace DDRemakeProject.WorldGeneration
{
    class GeneratorFromFile
    {

        /// <summary>
        /// Width of the map in tPixels
        /// </summary>
        public int SizeX { get; set; }
        /// <summary>
        /// Height of the map in tPixels
        /// </summary>
        public int SizeY { get; set; }

        int _nrOfUsedtiles = 0;

        List<RoomModule> _rooms;
        Tile[][] _tilesMatrix;

        public GeneratorFromFile()
        {
            //load



        }
    }
}
