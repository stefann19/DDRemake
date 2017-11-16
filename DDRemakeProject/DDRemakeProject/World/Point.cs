using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DDRemakeProject.World
{
    public class Point
    {
        #region Properties
        /// <summary>
        /// X coordinate of the tile in tPixels 
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y coordinate of the tile in tPixels 
        /// </summary>
        public int Y { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Make a point with the X and Y coordinate of the tile in tPixels 
        /// </summary>
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        /// <summary>
        /// Needed for serialization
        /// </summary>
        public Point()
        {

        }

        #endregion


        #region Directions
        /// <summary>
        /// true if x==1 and y==0
        /// </summary>
        public bool IsRight()
        {
            if (X == 1 && Y == 0) return true;
            else return false;
        }
        /// <summary>
        /// true if x==0 and y==1
        /// </summary>
        public bool IsTop()
        {
            if (X == 0 && Y == -1) return true;
            else return false;
        }
        /// <summary>
        /// true if x==-1 and y==0
        /// </summary>
        public bool IsLeft()
        {
            if (X == -1 && Y == 0) return true;
            else return false;
        }
        /// <summary>
        /// true if x==0 and y==-1;
        /// </summary>
        public bool IsBottom()
        {
            if (X == 0 && Y == 1) return true;
            else return false;
        }
        /// <summary>
        /// true if x==0 and y==0;
        /// </summary>
        public bool IsEmpty()
        {
            if (X == 0 && Y == 0) return true;
            else return false;
        }


        #endregion

        #region Operators

        public static Point operator +(Point b, Point c)
        {
            return new Point(b.X + c.X, b.Y + c.Y);
        }
        public static Point operator *(Point b, int c)
        {
            return new Point(b.X * c, b.Y * c);
        }

        public static implicit operator Point(System.Windows.Point v)
        {
            return new Point((int)v.X, (int)v.Y);
        }


        #endregion



    }
}
