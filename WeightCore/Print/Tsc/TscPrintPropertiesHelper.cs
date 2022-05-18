// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MvvmHelpers;
using System;
using System.Globalization;
using System.Threading;

namespace WeightCore.Print.Tsc
{
    public class TscPrintPropertiesHelper : BaseViewModel
    {
        #region Design pattern "Lazy Singleton"

        private static TscPrintPropertiesHelper _instance;
        public static TscPrintPropertiesHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        private double WidthDefault => 80.0;
        private double _width;
        public double Width
        {
            get => _width;
            set
            {
                _width = value >= 0 && value <= 1000 ? value : WidthDefault;
                OnPropertyChanged();
            }
        }

        private double HeightDefault => 100.0;
        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                if (value >= 0 && value <= 1000)
                    _height = value;
                else
                    _height = HeightDefault;
                OnPropertyChanged();
            }
        }

        private PrintSpeed SpeedDefault => (PrintSpeed)4;
        private PrintSpeed _speed;
        public PrintSpeed Speed
        {
            get => _speed;
            set
            {
                if (value >= 0 && value <= (PrintSpeed)12)
                    _speed = value;
                else
                    _speed = SpeedDefault;
                OnPropertyChanged();
            }
        }

        private PrintDensity DensityDefault => (PrintDensity)6;
        private PrintDensity _density;
        public PrintDensity Density
        {
            get => _density;
            set
            {
                if (value >= 0 && value <= (PrintDensity)15)
                    _density = value;
                else
                    _density = DensityDefault;
                OnPropertyChanged();
            }
        }

        private PrintSensor SensorDefault => 0;
        private PrintSensor _sensor;
        public PrintSensor Sensor
        {
            get => _sensor;
            set
            {
                if (value >= 0 && value <= (PrintSensor)1)
                    _sensor = value;
                else
                    _sensor = SensorDefault;
                OnPropertyChanged();
            }
        }

        private int VerticalDefault => 0;
        private int _vertical;
        public int Vertical
        {
            get => _vertical;
            set
            {
                if (value >= 0 && value <= 1000)
                    _vertical = value;
                else
                    _vertical = VerticalDefault;
                OnPropertyChanged();
            }
        }

        private int OffsetDefault => 0;
        private int _offset;
        public int Offset
        {
            get => _offset;
            set
            {
                if (value >= 0 && value <= 1000)
                    _offset = value;
                else
                    _offset = OffsetDefault;
                OnPropertyChanged();
            }
        }

        private int CutterValueDefault => 0;
        private int _cutterValue;
        public int CutterValue
        {
            get => _cutterValue;
            set
            {
                if (value >= 0 && value <= 1000)
                    _cutterValue = value;
                else
                    _cutterValue = CutterValueDefault;
            }
        }

        private PrintChannel _channel;
        public PrintChannel Channel
        {
            get => _channel;
            set
            {
                _channel = value;
                OnPropertyChanged();
            }
        }
        
        private string _printName;
        public string PrintName
        {
            get => _printName;
            set
            {
                _printName = value;
                OnPropertyChanged();
            }
        }
        private string _printIp;
        public string PrintIp
        {
            get => _printIp;
            set
            {
                _printIp = value;
                OnPropertyChanged();
            }
        }
        private int _printPort;
        public int PrintPort
        {
            get => _printPort;
            set
            {
                _printPort = value;
                OnPropertyChanged();
            }
        }
        private PrintLabelSize _size;
        public PrintLabelSize Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged();
            }
        }
        private PrintDpi _dpi;
        public PrintDpi Dpi
        {
            get => _dpi;
            set
            {
                _dpi = value;
                OnPropertyChanged();
            }
        }
        private ushort _feedMm;
        public ushort FeedMm
        {
            get => _feedMm;
            set
            {
                _feedMm = value;
                OnPropertyChanged();
            }
        }
        
        #endregion

        #region Public and private methods

        public void Setup(PrintLabelSize size)
        {
            switch (size)
            {
                case PrintLabelSize.Size40x60:
                    Width = 40.0;
                    Height = 60.0;
                    break;
                case PrintLabelSize.Size60x150:
                    Width = 60.0;
                    Height = 150.0;
                    break;
                case PrintLabelSize.Size60x90:
                    Width = 60.0;
                    Height = 90.0;
                    break;
                case PrintLabelSize.Size60x100:
                    Width = 60.0;
                    Height = 100.0;
                    break;
                case PrintLabelSize.Size80x100:
                    if (CultureInfo.CurrentCulture.Name.Equals("ru-RU"))
                    {
                        Width = 83.00;
                        Height = 101.50;
                    }
                    else
                    {
                        Width = 83.00;
                        Height = 101.50;
                    }
                    break;
                case PrintLabelSize.Size100x100:
                    Width = 100.0;
                    Height = 100.0;
                    break;
                case PrintLabelSize.Size100x110:
                    Width = 100.0;
                    Height = 110.0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(size), size, null);
            }
            Speed = SpeedDefault;
            Density = DensityDefault;
            Sensor = SensorDefault;
            Vertical = VerticalDefault;
            Offset = OffsetDefault;
        }

        #endregion
    }
}