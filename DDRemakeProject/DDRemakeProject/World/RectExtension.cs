using System;
using System.Windows;

namespace DDRemakeProject.World
{
    public static class RectExtension
    {
        public static Vector LocationCenter(this Rect rect)
        {
            return new Vector(rect.Left + rect.Width / 2,
                rect.Top + rect.Height / 2);
        }

        public static double Area(this Rect rect)
        {
            return rect.Width * rect.Height;
        }

        public static double MinWH(this Rect rect)
        {
            return Math.Min(rect.Width, rect.Height);
        }
    }

    
}
