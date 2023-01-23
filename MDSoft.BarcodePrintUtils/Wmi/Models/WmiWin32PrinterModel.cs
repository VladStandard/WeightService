// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MDSoft.BarcodePrintUtils.Wmi.Enums;

namespace MDSoft.BarcodePrintUtils.Wmi.Models;

public struct WmiWin32PrinterModel
{
	#region Public and private fields and properties

    public string Name { get; }
	public string DriverName { get; }
	public string PortName { get; }
	public string PrinterState { get; }
	public string Status { get; }
	public Win32PrinterStatus PrinterStatus { get; }

	#endregion

	#region Constructor and destructor

	public WmiWin32PrinterModel(string name, string driverName, string portName, string status, string printerState, Win32PrinterStatus printerStatus)
	{
		Name = name;
		DriverName = driverName;
		PortName = portName;
		Status = status;
		PrinterState = printerState;
		PrinterStatus = printerStatus;
	}

	#endregion
}