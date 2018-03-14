﻿using System.Windows;
using Newtonsoft.Json;

namespace DDRemakeProject.World
{
    public class MapBasicInfo
    {

        public Size Size { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public Rect Rect { get; set; }

        public MapBasicInfo(string name,Size size)
        {
            this.Size = size;
            this.Name = name;
            this.Rect = new Rect(new Point(0,0),size);
        }
        //public string FilePath { get; set; }
    }
}
