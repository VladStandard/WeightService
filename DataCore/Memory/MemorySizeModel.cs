// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Memory;

public class MemorySizeModel : HelperBase
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
    private MdWmiHelper Wmi => MdWmiHelper.Instance;


    #endregion

    #region Constructor and destructor

    public MemorySizeModel() : base()
    {
        PhysicalCurrent = new();
        VirtualCurrent = new();
        VirtualFree = new();
        VirtualTotal = new();
        PhysicalFree = new();
        PhysicalTotal = new();
    }

    #endregion

    #region Public and private methods

    public override void Execute()
    {
        base.Execute();

        if (PhysicalCurrent is not null)
            PhysicalCurrent.Bytes = (ulong)Process.GetCurrentProcess().WorkingSet64;
        if (VirtualCurrent is not null)
            VirtualCurrent.Bytes = (ulong)Process.GetCurrentProcess().PrivateMemorySize64;

        MdWmiWinMemoryModel getWmi = Wmi.GetWin32OperatingSystemMemory();
        VirtualFree = new() { Bytes = getWmi.FreeVirtual };
        PhysicalFree = new() { Bytes = getWmi.FreePhysical };
        VirtualTotal = new() { Bytes = getWmi.TotalVirtual };
        PhysicalTotal = new() { Bytes = getWmi.TotalPhysical };
    }

    public override void Close()
    {
        base.Close();
        VirtualCurrent = null;
        PhysicalCurrent = null;
        VirtualFree = null;
        PhysicalFree = null;
        VirtualTotal = null;
        PhysicalTotal = null;
    }

    public short GetMemorySizeAppMb() => (short)(PhysicalCurrent?.MegaBytes ?? 0);

    public short GetMemorySizeFreeMb() => (short)(PhysicalFree?.MegaBytes ?? 0);

    #endregion
}