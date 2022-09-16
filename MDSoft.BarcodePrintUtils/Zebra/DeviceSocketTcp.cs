// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace MDSoft.BarcodePrintUtils.Zebra;

public class DeviceSocketTcp : IDeviceSocket
{
	private string DeviceIp { get; }
	private int DevicePort { get; }

	public DeviceSocketTcp(string deviceIp, int devicePort)
	{
		DeviceIp = deviceIp;
		DevicePort = devicePort;
	}

	public string SendStringToPrinter(string szString)
	{
		string info = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.InterplayToPrinter(DeviceIp, DevicePort, szString.Split('\n'), out string _errorMessage);
		if (_errorMessage.Length > 0)
		{
			throw new(_errorMessage);
		}
		return info;
	}
}