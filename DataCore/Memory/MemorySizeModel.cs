// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Wmi;

namespace DataCore.Memory;

public class MemorySizeModel : DisposableBase, IDisposableBase
{
	#region Public and private fields, properties, constructor

	public MemorySizeConvertModel? VirtualCurrent { get; private set; }
	public MemorySizeConvertModel? PhysicalCurrent { get; private set; }
	public MemorySizeConvertModel? VirtualFree { get; private set; }
	public MemorySizeConvertModel? PhysicalFree { get; private set; }
	public MemorySizeConvertModel? VirtualTotal { get; private set; }
	public MemorySizeConvertModel? PhysicalTotal { get; private set; }
	public MemorySizeConvertModel VirtualAllocated =>
		new(VirtualTotal is not null && VirtualFree is not null ? VirtualTotal.Bytes - VirtualFree.Bytes : 0);
	public MemorySizeConvertModel PhysicalAllocated =>
		new(PhysicalTotal is not null && PhysicalFree is not null ? PhysicalTotal.Bytes - PhysicalFree.Bytes : 0);
	private WmiHelper? Wmi { get; set; } = WmiHelper.Instance;

	#endregion

	#region Constructor and destructor

	public MemorySizeModel() : base()
	{
		Init(Close, ReleaseManaged, ReleaseUnmanaged);

		PhysicalCurrent = new MemorySizeConvertModel();
		VirtualCurrent = new MemorySizeConvertModel();
		VirtualFree = new MemorySizeConvertModel();
		VirtualTotal = new MemorySizeConvertModel();
		PhysicalFree = new MemorySizeConvertModel();
		PhysicalTotal = new MemorySizeConvertModel();
	}

	#endregion

	#region Public and private methods

	public new void Open()
	{
		base.Open();
		CheckIsDisposed();

		if (PhysicalCurrent is not null)
			PhysicalCurrent.Bytes = (ulong)Process.GetCurrentProcess().WorkingSet64;
		if (VirtualCurrent is not null)
			VirtualCurrent.Bytes = (ulong)Process.GetCurrentProcess().PrivateMemorySize64;

		if (Wmi is not null)
		{
			WmiWin32MemoryModel getWmi = Wmi.GetWin32OperatingSystemMemory();
			VirtualFree = new MemorySizeConvertModel { Bytes = getWmi.FreeVirtual };
			PhysicalFree = new MemorySizeConvertModel { Bytes = getWmi.FreePhysical };
			VirtualTotal = new MemorySizeConvertModel { Bytes = getWmi.TotalVirtual };
			PhysicalTotal = new MemorySizeConvertModel { Bytes = getWmi.TotalPhysical };
		}
	}

	public new void Close()
	{
		base.Close();
	}

	public void ReleaseManaged()
	{
		VirtualCurrent = null;
		PhysicalCurrent = null;
		VirtualFree = null;
		PhysicalFree = null;
		VirtualTotal = null;
		PhysicalTotal = null;
		Wmi = null;
	}

	public void ReleaseUnmanaged()
	{
		//
	}

	#endregion
}