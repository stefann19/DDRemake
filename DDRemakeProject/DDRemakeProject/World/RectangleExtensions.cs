using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace DDRemakeProject.World
{
    public static class RectangleExtensions
    {
        public static void SetPosition(this Rectangle rect, Vector positionVector)
        {
            int x = (int)(Math.Round(positionVector.X) * Constants.TilePx);
            int y = (int)(Math.Round(positionVector.Y) * Constants.TilePx);
            rect.Margin = new Thickness(x,y,0,0);
        }
    }
}
