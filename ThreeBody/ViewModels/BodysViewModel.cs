﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
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
        SolidColorBrush _color1;
        SolidColorBrush _color2;
        SolidColorBrush _color3;
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
        public SolidColorBrush Color1 { get => _color1; set => SetProperty(ref _color1 , value); }
        public SolidColorBrush Color2 { get => _color2; set => SetProperty(ref _color2, value); }
        public SolidColorBrush Color3 { get => _color3; set => SetProperty(ref _color3, value); }

        public ICommand Command { get; set; }
        public ICommand ChangeColorCommand { get; set; }

        public BodysViewModel()
        {
            Wigth = 800;
            Height = 450;
            StratButtonHeight = 50;
            Engine = new ThreeBodyEngine();
            Engine.Dt = 10;
            Command = new DelegateCommand(RunObjectsInThread);
            ChangeColorCommand = new DelegateCommand(ChangeColor);
            timeSpan = new TimeSpan(0, 0, 0, 0, 1);
            Engine.Wight = Wigth;
            Engine.Height = Height - StratButtonHeight;

            Color1 = new SolidColorBrush(Color.FromRgb(255, 255, 0));
            Color2 = new SolidColorBrush(Color.FromRgb(255, 255, 0));
            Color3 = new SolidColorBrush(Color.FromRgb(255, 255, 0));
        }

        private void ChangeColor()
        {
            Random random = new Random();
            Color1 = new SolidColorBrush(Color.FromRgb((byte)random.Next(0,256), (byte)random.Next(0, 256), (byte)random.Next(0, 256)));
            Color2 = new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256)));
            Color3 = new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256)));
        }

        private async void RunObjectsInThread()
        {
            Engine.Bodys = new System.Collections.ObjectModel.ObservableCollection<MassiveBody>
            {
                new MassiveBody(1000000, new Vector(0.0008, 0), new Vector(0, 0), new Vector(Wigth/2 - 30, Height/2 - 30)),
                new MassiveBody(5000000, new Vector(0, 0), new Vector(0, 0), new Vector(Wigth/2, Height/2)),
                new MassiveBody(2000000, new Vector(-0.001, 0), new Vector(0, 0), new Vector(Wigth/2 + 20, Height/2 + 20))
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
                        X1 = Engine.Bodys[0].Coordinate.X;
                        X2 = Engine.Bodys[1].Coordinate.X;
                        X3 = Engine.Bodys[2].Coordinate.X;
                        Y1 = Engine.Bodys[0].Coordinate.Y;
                        Y2 = Engine.Bodys[1].Coordinate.Y;
                        Y3 = Engine.Bodys[2].Coordinate.Y;
                    }
                }
            }).Start();
        }
    }
}
