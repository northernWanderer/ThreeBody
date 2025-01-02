using System;
using System.Collections.Generic;

namespace ThreePhaseLibrary
{
    public class MassiveBody
    {
        private static double G = (6.674301515 * Math.Pow(10, -11));
        public MassiveBody(double mass, BodyVector speed, BodyVector acceleration, BodyVector coordinate)
        {
            Mass = mass;
            Speed = speed;
            Acceleration = acceleration;
            Coordinate = coordinate;
        }

        public double Mass { get; }
        public BodyVector Speed { get; set; }
        public BodyVector Acceleration { get; private set; }
        public BodyVector Coordinate { get; set; }

        public BodyVector GetCoordinate(IEnumerable<MassiveBody> massiveBodies, double dt, int wight, int hight)
        {
            BodyVector coordinate = Coordinate + GetSpeed(massiveBodies, dt) * dt;
            //coordinate.Y = coordinate.Y > 0 ? coordinate.Y % hight : (hight - coordinate.Y);
            //coordinate.X = coordinate.X > 0 ? coordinate.X % wight : (wight - coordinate.X);
            Coordinate = coordinate;
            return coordinate;
        }
        private BodyVector GetSpeed(IEnumerable<MassiveBody> massiveBodies, double dt)
        {
            BodyVector speed = Speed + GetAcceleration(massiveBodies) * dt;
            Speed = speed;
            return speed;
        }
        private BodyVector GetAcceleration(IEnumerable<MassiveBody> massiveBodies)
        {

            BodyVector force = GetFource(massiveBodies);
            BodyVector acceleration = force / Mass;
            Acceleration = acceleration;
            return acceleration;
        }
        private BodyVector GetFource(IEnumerable<MassiveBody> massiveBodies)
        {
            if (massiveBodies == null)
                throw new ArgumentNullException("Список тел не может быть пустым!");
            BodyVector forceVector = new BodyVector(0, 0);
            foreach (MassiveBody body in massiveBodies)
            {
                if (body == this) continue;

                forceVector += G * (Mass * body.Mass) * (body.Coordinate - Coordinate) / BodyVector.Module((Coordinate - body.Coordinate) * (Coordinate - body.Coordinate) * (Coordinate - body.Coordinate));
            }
            return forceVector;
        }
    }
}
