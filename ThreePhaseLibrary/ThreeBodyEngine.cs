using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreePhaseLibrary
{
    public class ThreeBodyEngine : BindableBase
    {
        private ObservableCollection<MassiveBody> bodys;
        private double dt;

        public ObservableCollection<MassiveBody> Bodys
        {
            get => bodys;
            set => SetProperty(ref bodys, value);
        }
        public double Dt { get => dt; set => SetProperty(ref dt , value); }
        public void Run()
        {
            foreach (var body in Bodys)
            {
                body.GetCoordinate(Bodys, Dt);
                Thread.Sleep(10);
            }
        }

    }
}
