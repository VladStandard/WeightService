namespace Ws.DataCore.Memory;

public sealed class MemoryModel
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

    #endregion

    #region Public and private methods

    public async Task OpenAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        IsExecute = true;
        Process? proc = Process.GetCurrentProcess();
        while (IsExecute)
        {
            if (proc is not null)
            {
                if (MemorySize.PhysicalCurrent is not null)
                    MemorySize.PhysicalCurrent.Bytes = (ulong)proc.WorkingSet64;
                if (MemorySize.VirtualCurrent is not null)
                    MemorySize.VirtualCurrent.Bytes = (ulong)proc.PrivateMemorySize64;
            }
            else
            {
                if (MemorySize.PhysicalCurrent is not null)
                    MemorySize.PhysicalCurrent.Bytes = 0;
                if (MemorySize.VirtualCurrent is not null)
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