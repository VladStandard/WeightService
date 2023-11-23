using System.Threading;
using TSCSDK;
using Ws.PrinterCore.Enums;

namespace Ws.PrinterCore.Tsc;

public class TscDriverHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
	private static TscDriverHelper _instance;
#pragma warning restore CS8618
	public static TscDriverHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields and properties
	public TscPrintProperties Properties { get; } = new();
    
	#endregion

	#region Public and private methods
    

	public void Setup(EnumPrintChannel channel, string ip, int port)
	{
		Properties.Channel = channel;
		Properties.PrintIp = ip;
		Properties.PrintPort = port;
	}
    
	public void SendCmdClearBuffer()
	{
		switch (Properties.Channel)
		{
			case EnumPrintChannel.Name:
				driver driver = new();
				driver.openport(Properties.PrintName);
				driver.clearbuffer();
				driver.closeport();
				break;
			case EnumPrintChannel.Ethernet:
				ethernet ethernet = new();
				ethernet.openport(Properties.PrintIp, Properties.PrintPort);
				ethernet.clearbuffer();
				ethernet.closeport();
				break;
		}
	}
    
	#endregion
}