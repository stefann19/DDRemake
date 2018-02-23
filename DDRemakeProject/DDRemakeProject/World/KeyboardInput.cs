using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DDRemakeProject.GamePlay;

namespace DDRemakeProject.World
{
    public class KeyboardInput
    {
        public KeyboardInput(Engine engine)
        {
            EventManager.RegisterClassHandler(typeof(MapWindow), UIElement.KeyDownEvent, new KeyEventHandler(InputReceiver));
            Engine = engine;
        }


        private void InputReceiver(object sender, KeyEventArgs e)
        {
            if (e.IsRepeat) return;

            switch (e.Key)
            {
                case Key.W:
                case Key.A:
                case Key.S:
                case Key.D:
                    Engine.Player.MoveTile(e);
                    break;
                    
                case Key.P:
                    
                    Engine.Camera.CheatsActivated = !Engine.Camera.CheatsActivated;
                    break;

                default: break;
            }

           
            
        }

        public Engine Engine { get; set; }
    }
}
