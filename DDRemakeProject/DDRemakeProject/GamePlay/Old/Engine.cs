using System.Windows;
using DDRemakeProject.World;
using DDRemakeProject.WorldGeneration;
using Newtonsoft.Json;

namespace DDRemakeProject.GamePlay.Old
{
    public class Engine
    {
        //private readonly Generator generator;

        [JsonIgnore]
        public MapWindow MapWindow { get; set; }
        [JsonIgnore]
        public Generator Generator { get; set; }
        public World.Player Player { get; set; }
        [JsonIgnore]
        public KeyboardInput KeyboardInput { get; set; }
        [JsonIgnore]
        public Camera Camera { get; set; }
        public MiniMap MiniMap { get; set; }

        public Engine(MapWindow mapWindow)
        {
            if(mapWindow==null)return;
            
            MapWindow = mapWindow;

            Generator = mapWindow.Generator;
            Player = new World.Player(this);
            KeyboardInput = new KeyboardInput(this);
            Camera = new Camera(this);
            MiniMap = new MiniMap(new Vector(200,200),this);
        }

      

    }
}