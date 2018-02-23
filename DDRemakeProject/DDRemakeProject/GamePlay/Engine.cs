using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DDRemakeProject.World;
using DDRemakeProject.WorldGeneration;

namespace DDRemakeProject.GamePlay
{
    public class Engine
    {
        //private readonly Generator generator;

        public MapWindow MapWindow { get; set; }
        public Generator Generator { get; set; }
        public World.Player Player { get; set; }
        public KeyboardInput KeyboardInput { get; set; }
        public Camera Camera { get; set; }
        public Engine(MapWindow mapWindow)
        {
            MapWindow = mapWindow;

            Generator = mapWindow.Generator;
            Player = new World.Player(this);
            KeyboardInput = new KeyboardInput(this);
            Camera = new Camera(this);

        }

      

    }
}