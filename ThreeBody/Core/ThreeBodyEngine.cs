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
            MassiveBody lessSpeed = Bodys.OrderBy(m => m.Mass).FirstOrDefault();
            foreach (var body in Bodys)
            {
                body.GetCoordinate(Bodys, Dt, _wight, _height);
                //body.Coordinate = body.Coordinate - lessSpeed.Coordinate;
            }
        }

    }
}
