using System;
using System.Numerics;

namespace ThreePhaseLibrary
{
    public struct BodyVector
    {
        public BodyVector(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static double Module(BodyVector a)
        {
            return Math.Sqrt(a.X * a.X + a.Y * a.Y);
        }
        public double X { get; set; }
        public double Y { get; set; }
        public static BodyVector operator +(BodyVector a, BodyVector b)
        {
            return new BodyVector(a.X + b.X, a.Y + b.Y);
        }

        public static BodyVector operator -(BodyVector a, BodyVector b)
        {
            return new BodyVector(a.X - b.X, a.Y - b.Y);
        }
        public static BodyVector operator *(double a, BodyVector b)
        {
            return (b * a);
        }
        public static BodyVector operator *(BodyVector a, double b)
        {
            return new BodyVector(a.X * b, a.Y * b);
        }
        public static BodyVector operator *(BodyVector a, BodyVector b)
        {
            return new BodyVector(a.X * b.X, a.Y * b.Y);
        }
        public static BodyVector operator /(BodyVector a, double b)
        {
            if (b < 1)
            {
                b = 1;
            }

            return new BodyVector(a.X / b, a.Y / b);
        }
        public static BodyVector operator /(double a, BodyVector b)
        {
            if (b.X < 1 || b.Y < 1)
            {
                if (b.X < 1) b.X = 1;
                if (b.Y < 1) b.Y = 1;
            }


            return new BodyVector(a / b.X, a / b.Y);
        }
    }
}
