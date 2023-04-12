// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace MDSoft.Wmi.Models;

public sealed class MdWmiWinMemoryModel
{
    #region Public and private fields, properties, constructor

    public ulong FreeVirtual { get; }
    public ulong FreePhysical { get; }
    public ulong TotalVirtual { get; }
    public ulong TotalPhysical { get; }

    #endregion

    #region Constructor and destructor

    public MdWmiWinMemoryModel(ulong freeVirtual, ulong freePhysical, ulong totalVirtual, ulong totalPhysical)
    {
        FreeVirtual = freeVirtual;
        FreePhysical = freePhysical;
        TotalVirtual = totalVirtual;
        TotalPhysical = totalPhysical;
    }

    #endregion
}