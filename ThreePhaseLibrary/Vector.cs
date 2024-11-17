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
            if (b == 0)
            {
                b = 0.001;
            }

            return new Vector(a.X / b, a.Y / b);
        }
        public static Vector operator /(double a, Vector b)
        {
            if (b.X == 0 || b.Y == 0)
            {
                if (b.X == 0) b.X = 0.001;
                if (b.Y == 0) b.Y = 0.001;
            }


            return new Vector(a / b.X, a / b.Y);
        }
    }
}
