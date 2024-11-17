using System;
using System.Numerics;

namespace ThreePhaseLibrary
{
    public struct Vector
    {
        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static double Module(Vector a)
        {
            return Math.Sqrt(a.X * a.X + a.Y * a.Y);
        }
        public double X { get; set; }
        public double Y { get; set; }
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }
        public static Vector operator *(double a, Vector b)
        {
            return (b * a);
        }
        public static Vector operator *(Vector a, double b)
        {
            return new Vector(a.X * b, a.Y * b);
        }
        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.X * b.X, a.Y * b.Y);
        }
        public static Vector operator /(Vector a, double b)
        {
            if (b < 1)
            {
                b = 1;
            }

            return new Vector(a.X / b, a.Y / b);
        }
        public static Vector operator /(double a, Vector b)
        {
            if (b.X < 1 || b.Y < 1)
            {
                if (b.X < 1) b.X = 1;
                if (b.Y < 1) b.Y = 1;
            }


            return new Vector(a / b.X, a / b.Y);
        }
    }
}
