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
    }

    
}
