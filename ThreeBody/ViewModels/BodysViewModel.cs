using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows;
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
        private Visibility _startPanelVisibility;
        private bool isStarted;
        private bool _firstBodyChecked;
        private bool _secondBodyChecked;
        private bool _thirdBodyChecked;
        private double _ySlider;
        private double _xSlider;
        private Visibility _startPanelVisibilityInverse;
        private double _firstBodySpeedX;
        private double _firstBodySpeedY;
        private double _secondBodySpeedY;
        private double _secondBodySpeedX;
        private double _thirdBodySpeedX;
        private double _thirdBodySpeedY;
        private double _firstBodyMass;
        private double _secondBodyMass;
        private double _thirdBodyMass;

        public ThreeBodyEngine Engine { get; }
        public Visibility StartPanelVisibility { get => _startPanelVisibility; set => SetProperty(ref _startPanelVisibility, value); }
        public Visibility StartPanelVisibilityInverse { get => _startPanelVisibilityInverse; set => SetProperty(ref _startPanelVisibilityInverse, value); }
        public int Wigth { get => _wigh; set => SetProperty(ref _wigh, value); }
        public int Height { get => _height; set => SetProperty(ref _height, value); }
        public int StratButtonHeight { get => _stratButtonHeight; set => SetProperty(ref _stratButtonHeight, value); }
        #region координаты
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
        #endregion
        #region векторы скорости
        public double FirstBodySpeedX
        {
            get => _firstBodySpeedX;
            set => SetProperty(ref _firstBodySpeedX, value);
        }
        public double FirstBodySpeedY
        {
            get => _firstBodySpeedY;
            set => SetProperty(ref _firstBodySpeedY, value);
        }
        public double SecondBodySpeedX
        {
            get => _secondBodySpeedX;
            set => SetProperty(ref _secondBodySpeedX, value);
        }
        public double SecondBodySpeedY
        {
            get => _secondBodySpeedY;
            set => SetProperty(ref _secondBodySpeedY, value);
        }
        public double ThirdBodySpeedX
        {
            get => _thirdBodySpeedX;
            set => SetProperty(ref _thirdBodySpeedX, value);
        }
        public double ThirdBodySpeedY
        {
            get => _thirdBodySpeedY;
            set => SetProperty(ref _thirdBodySpeedY, value);
        }
        #endregion

        #region Массы тел
        public double FirstBodyMass
        {
            get => _firstBodyMass;
            set => SetProperty(ref _firstBodyMass, value);
        }
      
        public double SecondBodyMass
        {
            get => _secondBodyMass;
            set => SetProperty(ref _secondBodyMass, value);
        }       
        public double ThirdBodyMass
        {
            get => _thirdBodyMass;
            set => SetProperty(ref _thirdBodyMass, value);
        }
       
        #endregion
        public double YSlider
        {
            get => _ySlider;
            set => SetProperty(ref _ySlider, value);
        }

        public double XSlider
        {
            get => _xSlider;
            set => SetProperty(ref _xSlider, value);
        }
        public SolidColorBrush Color1 { get => _color1; set => SetProperty(ref _color1, value); }
        public SolidColorBrush Color2 { get => _color2; set => SetProperty(ref _color2, value); }
        public SolidColorBrush Color3 { get => _color3; set => SetProperty(ref _color3, value); }

        public ICommand StartCommand { get; set; }
        public ICommand ChangeColorCommand { get; set; }
        public bool FirstBodyChecked { get => _firstBodyChecked; set => SetProperty(ref _firstBodyChecked, value); }
        public bool SecondBodyChecked { get => _secondBodyChecked; set => SetProperty(ref _secondBodyChecked, value); }
        public bool ThirdBodyChecked { get => _thirdBodyChecked; set => SetProperty(ref _thirdBodyChecked, value); }

        public BodysViewModel()
        {
            Wigth = 1000;
            Height = 550;
            StratButtonHeight = 50;
            Engine = new ThreeBodyEngine();
            Engine.Dt = 10;
            StartCommand = new DelegateCommand(RunObjectsInThread);
            ChangeColorCommand = new DelegateCommand(ChangeColor);
            timeSpan = new TimeSpan(0, 0, 0, 0, 1);
            Engine.Wight = Wigth;
            Engine.Height = Height - StratButtonHeight;

            Color1 = new SolidColorBrush(Color.FromRgb(255, 255, 0));
            Color2 = new SolidColorBrush(Color.FromRgb(255, 255, 0));
            Color3 = new SolidColorBrush(Color.FromRgb(255, 255, 0));

            StartPanelVisibility = Visibility.Visible;
            StartPanelVisibilityInverse = Visibility.Collapsed;
            isStarted = false;
            InitObjectCoordinates();
        }

        private async Task InitObjectCoordinates()
        {
            new Task(() =>
            {
                while (!isStarted)
                {

                    if (FirstBodyChecked)
                    {                       
                        X1 = XSlider;                        
                        Y1 = YSlider;                      
                    }
                    if (SecondBodyChecked)
                    {                                 
                        X2 = XSlider;                                             
                        Y2 = YSlider;                     
                    }
                    if (ThirdBodyChecked)
                    {                       
                        X3 = XSlider;                       
                        Y3 = YSlider;
                    }
                }
            }).Start();
        }
        private void ChangeColor()
        {
            Random random = new Random();
            Color1 = new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256)));
            Color2 = new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256)));
            Color3 = new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256)));
        }

        private async void RunObjectsInThread()
        {
            SetStartView();
            await InitObjectCoordinates();
            InitEngine();
            await RunObjects();
        }

        private void InitEngine()
        {
            Engine.Bodys = new System.Collections.ObjectModel.ObservableCollection<MassiveBody>
            {
                new MassiveBody(1000000, new BodyVector(0.0008, 0), new BodyVector(0, 0), new BodyVector(X1, Y1)),
                new MassiveBody(5000000, new BodyVector(0, 0), new BodyVector(0, 0), new BodyVector(X2, Y2)),
                new MassiveBody(2000000, new BodyVector(-0.001, 0), new BodyVector(0, 0), new BodyVector(X3, Y3))
            };
        }

        private void SetStartView()
        {
            isStarted = !isStarted;
            if (isStarted)
            {
                StartPanelVisibility = Visibility.Collapsed;
                StartPanelVisibilityInverse = Visibility.Visible;

            }
            else
            {
                StartPanelVisibility = Visibility.Visible;
                StartPanelVisibilityInverse = Visibility.Collapsed;
            }
        }

        private async Task RunObjects()
        {
            new Task(() =>
            {

                while (isStarted)
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
