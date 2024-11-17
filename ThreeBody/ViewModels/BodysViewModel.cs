using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ThreePhaseLibrary;

namespace ThreeBody.DynamicViewModel
{
    internal class BodysViewModel : BindableBase
    {
        private double _x1;
        private double _y1;
        private double _y3;
        private double _x3;
        private double _y2;
        private double _x2;
        private DateTime _dateTime;
        private TimeSpan timeSpan;
        private int _wigh;
        private int _height;
        private int _stratButtonHeight;

        public ThreeBodyEngine Engine { get; }

        public int Wigth { get => _wigh; set => SetProperty(ref _wigh, value); }
        public int Height { get => _height; set => SetProperty(ref _height, value); }
        public int StratButtonHeight { get => _stratButtonHeight; set => SetProperty(ref _stratButtonHeight, value); }
        public double X1
        {
            get => _x1;
            set => SetProperty(ref _x1, value);
        }
        public double Y1
        {
            get => _y1;
            set => SetProperty(ref _y1, value);
        }
        public double X2
        {
            get => _x2;
            set => SetProperty(ref _x2, value);
        }
        public double Y2
        {
            get => _y2;
            set => SetProperty(ref _y2, value);
        }
        public double X3
        {
            get => _x3;
            set => SetProperty(ref _x3, value);
        }
        public double Y3
        {
            get => _y3;
            set => SetProperty(ref _y3, value);
        }
        public ICommand Command { get; set; }

        public BodysViewModel()
        {
            Wigth = 800;
            Height = 450;
            StratButtonHeight = 50;
            Engine = new ThreeBodyEngine();
            Engine.Dt = 10;
            Command = new DelegateCommand(RunObjectsInThread);
            timeSpan = new TimeSpan(0, 0, 0, 0, 1);
            Engine.Wight = Wigth;
            Engine.Height = Height - StratButtonHeight;
        }

        private async void RunObjectsInThread()
        {
            Engine.Bodys = new System.Collections.ObjectModel.ObservableCollection<MassiveBody>
            {
                new MassiveBody(100000, new Vector(0.001, 0), new Vector(0, 0), new Vector(Wigth/2 - 30, Height/2 - 30)),
                new MassiveBody(500000, new Vector(0, 0), new Vector(0, 0), new Vector(Wigth/2, Height/2)),
                new MassiveBody(200000, new Vector(-0.002, 0), new Vector(0, 0), new Vector(Wigth/2 + 20, Height/2 + 20))
            };
            await RunObjects();
        }

        private async Task RunObjects()
        {
            new Task(() =>
            {

                while (true)
                {

                    if (DateTime.Now - _dateTime > timeSpan)
                    {
                        _dateTime = DateTime.Now;
                        Engine.Run();
                        X1 = Engine.Bodys[0].Coordinate.X + Wigth / 2;
                        X2 = Engine.Bodys[1].Coordinate.X + Wigth / 2;
                        X3 = Engine.Bodys[2].Coordinate.X + Wigth / 2;
                        Y1 = Engine.Bodys[0].Coordinate.Y + Height / 2;
                        Y2 = Engine.Bodys[1].Coordinate.Y + Height / 2;
                        Y3 = Engine.Bodys[2].Coordinate.Y + Height / 2;
                    }
                }
            }).Start();
        }
    }
}
