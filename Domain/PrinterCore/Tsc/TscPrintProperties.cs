using PrinterCore.Enums;
namespace PrinterCore.Tsc;

public class TscPrintProperties
{
    #region Public and private fields and properties

    private const double WidthDefault = 80.0;
    private double _width;
    public double Width
    {
        get => _width;
        set
        {
            _width = value >= 0 && value <= 1000 ? value : WidthDefault;
        }
    }

    private const double HeightDefault = 100.0;
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
        }
    }

    private const WsEnumPrintSpeed SpeedDefault = (WsEnumPrintSpeed)4;
    private WsEnumPrintSpeed _speed;
    public WsEnumPrintSpeed Speed
    {
        get => _speed;
        set => _speed = value is >= 0 and <= (WsEnumPrintSpeed)12 ? value : SpeedDefault;
    }

    private const WsEnumPrintDensity DensityDefault = (WsEnumPrintDensity)6;
    private WsEnumPrintDensity _density;
    public WsEnumPrintDensity Density
    {
        get => _density;
        set => _density = value is >= 0 and <= (WsEnumPrintDensity)15 ? value : DensityDefault;
    }

    private const WsEnumPrintSensor SensorDefault = 0;
    private WsEnumPrintSensor _sensor;
    public WsEnumPrintSensor Sensor
    {
        get => _sensor;
        set => _sensor = value is >= 0 and <= (WsEnumPrintSensor)1 ? value : SensorDefault;
    }

    private const int VerticalDefault = 0;
    private int _vertical;
    public int Vertical
    {
        get => _vertical;
        set => _vertical = value is >= 0 and <= 1000 ? value : VerticalDefault;
    }

    private const int OffsetDefault = 0;
    private int _offset;
    public int Offset
    {
        get => _offset;
        set => _offset = value is >= 0 and <= 1000 ? value : OffsetDefault;
    }

    private const int CutterValueDefault = 0;
    private int _cutterValue;
    public int CutterValue
    {
        get => _cutterValue;
        set => _cutterValue = value is >= 0 and <= 1000 ? value : CutterValueDefault;
    }

    public WsEnumPrintChannel Channel { get; set; }

    public string PrintName { get; set; }

    public string PrintIp { get; set; }

    public int PrintPort { get; set; }

    public WsEnumPrintLabelSize Size { get; set; }

    public WsEnumPrintLabelDpi Dpi { get; set; }

    public ushort FeedMm { get; set; }

    #endregion
    
    public void Setup(WsEnumPrintLabelSize size)
    {
        switch (size)
        {
            case WsEnumPrintLabelSize.Size40x60:
                Width = 40.0;
                Height = 60.0;
                break;
            case WsEnumPrintLabelSize.Size60x150:
                Width = 60.0;
                Height = 150.0;
                break;
            case WsEnumPrintLabelSize.Size60x90:
                Width = 60.0;
                Height = 90.0;
                break;
            case WsEnumPrintLabelSize.Size60x100:
                Width = 60.0;
                Height = 100.0;
                break;
            case WsEnumPrintLabelSize.Size80x100:
                Width = 83.00;
                Height = 101.50;
                break;
            case WsEnumPrintLabelSize.Size100x100:
                Width = 100.0;
                Height = 100.0;
                break;
            case WsEnumPrintLabelSize.Size100x110:
                Width = 100.0;
                Height = 110.0;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(size), size.ToString());
        }
        Speed = SpeedDefault;
        Density = DensityDefault;
        Sensor = SensorDefault;
        Vertical = VerticalDefault;
        Offset = OffsetDefault;
    }
}