using System;
using System.Collections.Generic;

namespace ThreePhaseLibrary
{
    public class MassiveBody
    {
        private static double G = (6.674301515 * Math.Pow(10, -11));
        public MassiveBody(double mass, Vector speed, Vector acceleration, Vector coordinate)
        {
            Mass = mass;
            Speed = speed;
            Acceleration = acceleration;
            Coordinate = coordinate;
        }

        public double Mass { get; }
        public Vector Speed { get; private set; }
        public Vector Acceleration { get; private set; }
        public Vector Coordinate { get; set; }

        public Vector GetCoordinate(IEnumerable<MassiveBody> massiveBodies, double dt, int wight, int hight)
        {
            Vector coordinate = Coordinate + GetSpeed(massiveBodies, dt) * dt;
            //coordinate.Y = coordinate.Y > 0 ? coordinate.Y % hight : (hight - coordinate.Y);
            //coordinate.X = coordinate.X > 0 ? coordinate.X % wight : (wight - coordinate.X);
            Coordinate = coordinate;
            return coordinate;
        }
        private Vector GetSpeed(IEnumerable<MassiveBody> massiveBodies, double dt)
        {
            Vector speed = Speed + GetAcceleration(massiveBodies) * dt;
            Speed = speed;
            return speed;
        }
        private Vector GetAcceleration(IEnumerable<MassiveBody> massiveBodies)
        {

            Vector force = GetFource(massiveBodies);
            Vector acceleration = force / Mass;
            Acceleration = acceleration;
            return acceleration;
        }
        private Vector GetFource(IEnumerable<MassiveBody> massiveBodies)
        {
            if (massiveBodies == null)
                throw new ArgumentNullException("Список тел не может быть пустым!");
            Vector forceVector = new Vector(0, 0);
            foreach (MassiveBody body in massiveBodies)
            {
                if (body == this) continue;

                forceVector += G * (Mass * body.Mass) * (body.Coordinate - Coordinate) / Vector.Module((Coordinate - body.Coordinate) * (Coordinate - body.Coordinate) * (Coordinate - body.Coordinate));
            }
            return forceVector;
        }
    }
}
