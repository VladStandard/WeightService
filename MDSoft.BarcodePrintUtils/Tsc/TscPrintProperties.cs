// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using MDSoft.BarcodePrintUtils.Enums;

namespace MDSoft.BarcodePrintUtils.Tsc;

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

	private const PrintSpeed SpeedDefault = (PrintSpeed)4;
	private PrintSpeed _speed;
	public PrintSpeed Speed
	{
		get => _speed;
		set => _speed = value is >= 0 and <= (PrintSpeed)12 ? value : SpeedDefault;
	}

	private const PrintDensity DensityDefault = (PrintDensity)6;
	private PrintDensity _density;
	public PrintDensity Density
	{
		get => _density;
		set => _density = value is >= 0 and <= (PrintDensity)15 ? value : DensityDefault;
	}

	private const PrintSensor SensorDefault = 0;
	private PrintSensor _sensor;
	public PrintSensor Sensor
	{
		get => _sensor;
		set => _sensor = value is >= 0 and <= (PrintSensor)1 ? value : SensorDefault;
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

	public PrintChannel Channel { get; set; }

	public string PrintName { get; set; }

	public string PrintIp { get; set; }

	public int PrintPort { get; set; }

	public PrintLabelSize Size { get; set; }

	public PrintLabelDpi Dpi { get; set; }

	public ushort FeedMm { get; set; }

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
				//if (CultureInfo.CurrentCulture.Name.Equals("ru-RU"))
				//{
					Width = 83.00;
					Height = 101.50;
				//}
				//else
				//{
				//	Width = 83.00;
				//	Height = 101.50;
				//}
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