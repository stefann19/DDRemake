using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DDRemakeProject.World
{
    public static class VectorExtensions
    {
        public static Vector Empty = new Vector(double.MaxValue, double.MaxValue);
        public static Vector Null(this Vector v)
        {
            return new Vector(double.MaxValue, double.MaxValue);
        }

        public static Vector NormalizeRet(this Vector v)
        {
            v.Normalize();
            v= new Vector((int)v.X,(int)v.Y);
            return v;
        }

        private const double DegToRad = Math.PI / 180f;

        public static Vector Rotate(this Vector v, double degrees)
        {
            return v.RotateRadians(degrees * DegToRad);
        }

        public static Vector RotateRadians(this Vector v, double radians)
        {
            double ca = Math.Cos(radians);
            double sa = Math.Sin(radians);
            return new Vector(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y);
        }
    }
}

