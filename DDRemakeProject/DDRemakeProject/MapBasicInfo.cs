using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject
{
    public class MapBasicInfo
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }

        public MapBasicInfo(int width,int height,string name)
        {
            this.Width = width;
            this.Height = height;
            this.Name = name;
        }
        //public string FilePath { get; set; }
    }
}
