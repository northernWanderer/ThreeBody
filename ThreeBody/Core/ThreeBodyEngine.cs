using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreePhaseLibrary
{
    public class ThreeBodyEngine : BindableBase
    {
        private ObservableCollection<MassiveBody> bodys;
        private double dt;
        MassiveBody _maxMass;
        private BodyVector _centerMass;
        private MassiveBody _minSpeed;
        private int _wight;
        private int _height;

        public ObservableCollection<MassiveBody> Bodys
        {
            get => bodys;
            set => SetProperty(ref bodys, value);
        }
        public double Dt { get => dt; set => SetProperty(ref dt, value); }
        public int Wight { get => _wight; set => SetProperty(ref _wight, value); }
        public int Height { get => _height; set => SetProperty(ref _height, value); }
        public void Run()
        {
            BodyVector vindowHalfSize = new BodyVector(Wight / 2, Height / 2);
            //_maxMass = Bodys.OrderByDescending(m => m.Mass).FirstOrDefault();
            _centerMass = CalculateCenterOfMass(Bodys);
            _minSpeed = Bodys.OrderBy(m => BodyVector.Module(m.Speed)).FirstOrDefault();
            foreach (var body in Bodys)
            {
                body.Coordinate = body.Coordinate - _centerMass + vindowHalfSize;
                //body.Speed = body.Speed - _minSpeed.Speed;
            }
            foreach (var body in Bodys)
            {
                body.GetCoordinate(Bodys, Dt, _wight, _height);
            }
        }

        private BodyVector CalculateCenterOfMass(IEnumerable<MassiveBody> massiveBodies)
        {
            double xCenter = massiveBodies.Select(b => b.Mass*b.Coordinate).Sum(d => d.X) / massiveBodies.Sum(b => b.Mass);
            double yCenter = massiveBodies.Select(b => b.Mass*b.Coordinate).Sum(d => d.Y) / massiveBodies.Sum(b => b.Mass);
           return new BodyVector(xCenter, yCenter);
        }
    }
}
