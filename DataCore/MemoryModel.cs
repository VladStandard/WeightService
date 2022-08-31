// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Memory;
using System.Threading.Tasks;

namespace DataCore;

public class MemoryModel
{
    #region Public and private fields, properties, constructor

    public MemorySizeModel MemorySize { get; }
    private bool IsExecute { get; set; }

    #endregion

    #region Constructor and destructor

    public MemoryModel()
    {
        MemorySize = new();
        IsExecute = false;
    }

    ~MemoryModel()
    {
        MemorySize.Dispose(false);
    }

    #endregion

    #region Public and private methods

    public async Task OpenAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        IsExecute = true;
        Process? proc = Process.GetCurrentProcess();
        while (IsExecute)
        {
            if (proc != null)
            {
                if (MemorySize.PhysicalCurrent != null)
                    MemorySize.PhysicalCurrent.Bytes = (ulong)proc.WorkingSet64;
                if (MemorySize.VirtualCurrent != null)
                    MemorySize.VirtualCurrent.Bytes = (ulong)proc.PrivateMemorySize64;
            }
            else
            {
                if (MemorySize.PhysicalCurrent != null)
                    MemorySize.PhysicalCurrent.Bytes = 0;
                if (MemorySize.VirtualCurrent != null)
                    MemorySize.VirtualCurrent.Bytes = 0;
            }
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        }
    }

    public void Close()
    {
        IsExecute = false;
    }

    #endregion
}
