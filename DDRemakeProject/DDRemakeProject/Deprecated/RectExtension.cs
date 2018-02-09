using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DDRemakeProject.Deprecated
{
    public static class RectExtension
    {
        public static Vector LocationCenter(this Rect rect)
        {
            return new Vector(rect.Left + rect.Width / 2,
                rect.Top + rect.Height / 2);
        }
    }

    
}
