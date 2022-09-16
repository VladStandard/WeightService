// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MDSoft.BarcodePrintUtils.Raw;
using MDSoft.BarcodePrintUtils.Zpl;

namespace MDSoft.BarcodePrintUtils.Zebra;

public class DeviceSocketRaw : IDeviceSocket
{
	private string PrinterName { get; }

	public DeviceSocketRaw(string printerName)
	{
		PrinterName = printerName;
	}

	public string SendStringToPrinter(string szString)
	{
		string zpl = ZplUtils.ConvertStringToHex(szString);
		RawPrinterHelper.SendStringToPrinter(PrinterName, zpl);
		return "";
	}
}