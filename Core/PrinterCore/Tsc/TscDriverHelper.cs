using System.Threading;
using PrinterCore.Enums;
namespace PrinterCore.Tsc;

public class TscDriverHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
	private static TscDriverHelper _instance;
#pragma warning restore CS8618
	public static TscDriverHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields and properties

	public WsEnumPrintTscDll TscDll { get; set; }
	public TscPrintProperties Properties { get; } = new();
	public string Cmd { get; set; }

	#endregion

	#region Public and private methods

	private void SetupInit(WsEnumPrintLabelSize size = WsEnumPrintLabelSize.Size80x100, WsEnumPrintLabelDpi dpi = WsEnumPrintLabelDpi.Dpi300)
	{
		Properties.Size = size;
		Properties.Dpi = dpi;
		Properties.Setup(Properties.Size);
	}

	public void Setup(WsEnumPrintChannel channel, string name, WsEnumPrintLabelSize size, WsEnumPrintLabelDpi dpi)
	{
		SetupInit(size, dpi);
		Properties.Channel = channel;
		Properties.PrintName = name;
	}

	public void Setup(WsEnumPrintChannel channel, string ip, int port, WsEnumPrintLabelSize size, WsEnumPrintLabelDpi dpi)
	{
		SetupInit(size, dpi);
		Properties.Channel = channel;
		Properties.PrintIp = ip;
		Properties.PrintPort = port;
	}

	public WsEnumPrintStatus GetStatusAsEnum(byte? value)
	{
		return value switch
		{
			// Normal
			0x00 => 0x00,
			// Head opened
			0x01 => (WsEnumPrintStatus)0x01,
			// Paper Jam
			0x02 => (WsEnumPrintStatus)0x02,
			// Paper Jam and head opened
			0x03 => (WsEnumPrintStatus)0x03,
			// Out of paper
			0x04 => (WsEnumPrintStatus)0x04,
			// Out of paper and head opened
			0x05 => (WsEnumPrintStatus)0x05,
			// Out of ribbon
			0x08 => (WsEnumPrintStatus)0x08,
			// Out of ribbon and head opened
			0x09 => (WsEnumPrintStatus)0x09,
			// Out of ribbon and paper jam
			0x0A => (WsEnumPrintStatus)0x0A,
			// Out of ribbon, paper jam and head opened
			0x0B => (WsEnumPrintStatus)0x0B,
			// Out of ribbon and out of paper
			0x0C => (WsEnumPrintStatus)0x0C,
			// Out of ribbon, out of paper and head opened
			0x0D => (WsEnumPrintStatus)0x0D,
			// Pause
			0x10 => (WsEnumPrintStatus)0x10,
			// Printing
			0x20 => (WsEnumPrintStatus)0x20,
			_ => WsEnumPrintStatus.HundredTwentyEight,
		};
	}

	public bool GetDriverStatus()
	{
		bool result = false;
		switch (Properties.Channel)
		{
			case WsEnumPrintChannel.Name:
				TSCSDK.driver driver = new();
				driver.openport(Properties.PrintName);
				driver.clearbuffer();
				result = driver.driver_status(Properties.PrintName);
				driver.closeport();
				break;
			case WsEnumPrintChannel.Ethernet:
				//
				break;
		}
		return result;
	}

	public string GetStatusAsStringRus(byte? value) => value switch
	{
		// Normal
		0x00 => "Нормальный",
		// Head opened
		0x01 => "Голова открыта",
		// Paper Jam
		0x02 => "Замятие бумаги",
		// Paper Jam and head opened
		0x03 => "Замятие бумаги и открыта голова",
		// Out of paper
		0x04 => "Нет бумаги",
		// Out of paper and head opened
		0x05 => "Нет бумаги и голова открыта",
		// Out of ribbon
		0x08 => "Нет ленты",
		// Out of ribbon and head opened
		0x09 => "Нет ленты и голова открыта",
		// Out of ribbon and paper jam
		0x0A => "Закончилась лента и застряла бумага",
		// Out of ribbon, paper jam and head opened
		0x0B => "Закончилась лента, застряла бумага и голова открыта",
		// Out of ribbon and out of paper
		0x0C => "Нет ленты и нет бумаги",
		// Out of ribbon, out of paper and head opened
		0x0D => "Нет ленты, нет бумаги и голова открыта",
		// Pause
		0x10 => "Пауза",
		// Printing
		0x20 => "Печать",
		_ => "Другая ошибка",
	};

	public string GetStatusAsStringEng(byte? value) => value switch
	{
		// Normal
		0x00 => "Normal",
		// Head opened
		0x01 => "Head opened",
		// Paper Jam
		0x02 => "Paper Jam",
		// Paper Jam and head opened
		0x03 => "Paper Jam and head opened",
		// Out of paper
		0x04 => "Out of paper",
		// Out of paper and head opened
		0x05 => "Out of paper and head opened",
		// Out of ribbon
		0x08 => "Out of ribbon",
		// Out of ribbon and head opened
		0x09 => "Out of ribbon and head opened",
		// Out of ribbon and paper jam
		0x0A => "Out of ribbon and paper jam",
		// Out of ribbon, paper jam and head opened
		0x0B => "Out of ribbon, paper jam and head opened",
		// Out of ribbon and out of paper
		0x0C => "Out of ribbon and out of paper",
		// Out of ribbon, out of paper and head opened
		0x0D => "Out of ribbon, out of paper and head opened",
		// Pause
		0x10 => "Pause",
		// Printing
		0x20 => "Printing",
		_ => "Other error",
	};

	public void SendCmd(string cmd = "", bool isTest = false)
	{
		Cmd = cmd;

		switch (Properties.Channel)
		{
			case WsEnumPrintChannel.Name:
				TSCSDK.driver driver = new();
				driver.openport(Properties.PrintName);
				driver.clearbuffer();
				//driver.setup(Properties.Width.ToString(), Properties.Height.ToString(), Properties.Speed.ToString(),
				//    Properties.Density.ToString(), Properties.Sensor.ToString(), Properties.Vertical.ToString(), Properties.Offset.ToString());
				if (isTest)
				{
					driver.barcode("100", "200", "128", "100", "1", "0", "3", "3", "123456789");
					driver.printerfont("100", "100", "3", "0", "1", "1", "Printer Font Test");
					driver.sendcommand("BOX 50,50,500,400,3" + Environment.NewLine);
					driver.printlabel("1", "1");
				}
				else if (!string.IsNullOrEmpty(Cmd))
				{
					driver.sendcommand(Cmd);
				}
				driver.closeport();
				//driver.closeport_mult(9100);
				break;
			case WsEnumPrintChannel.Ethernet:
				TSCSDK.ethernet ethernet = new();
				ethernet.openport(Properties.PrintIp, Properties.PrintPort);
				ethernet.clearbuffer();
				//ethernet.setup(Properties.Width.ToString(), Properties.Height.ToString(), Properties.Speed.ToString(),
				//    Properties.Density.ToString(), Properties.Sensor.ToString(), Properties.Vertical.ToString(), Properties.Offset.ToString());
				if (isTest)
				{
					ethernet.barcode("100", "200", "128", "100", "1", "0", "3", "3", "123456789");
					ethernet.printerfont("100", "100", "3", "0", "1", "1", "Printer Font Test");
					ethernet.sendcommand("BOX 50,50,500,400,3" + Environment.NewLine);
					ethernet.printlabel("1", "1");
				}
				else if (!string.IsNullOrEmpty(Cmd))
				{
					ethernet.sendcommand(Cmd);
				}
				ethernet.closeport();
				//ethernet.closeport_mult(9100, 0);
				break;
		}
	}

	public void SendCmdClearBuffer()
	{
		switch (Properties.Channel)
		{
			case WsEnumPrintChannel.Name:
				TSCSDK.driver driver = new();
				driver.openport(Properties.PrintName);
				driver.clearbuffer();
				driver.closeport();
				break;
			case WsEnumPrintChannel.Ethernet:
				TSCSDK.ethernet ethernet = new();
				ethernet.openport(Properties.PrintIp, Properties.PrintPort);
				ethernet.clearbuffer();
				ethernet.closeport();
				break;
		}
	}

	public void SendCmdGap(double gapSize = 3.5, double gapOffset = 0.0)
	{
		string strGapSize = $"{gapSize}".Replace(',', '.');
		string strGapOffset = $"{gapOffset}".Replace(',', '.');
		SendCmd($"GAP {strGapSize} mm, {strGapOffset} mm");
	}

	public void SendCmdCutter(int value)
	{
		if (value >= 0)
			SendCmd($"SET CUTTER {value}");
	}

	public void SendCmdTest()
	{
		SendCmd("", true);
	}

	//public void ClearBuffer()
	//{
	//    //if (!Open()) return;
	//    switch (Properties.Channel)
	//    {
	//        case PrintChannel.Name:
	//            Driver.clearbuffer();
	//            break;
	//        case PrintChannel.Ethernet:
	//            Driver.closeport();
	//            break;
	//    }
	//}

	#endregion
}