// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Memory;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DataCore
{
    public class MemoryEntity
    {
        #region Public and private fields and properties

        public int SleepMiliSeconds { get; }
        public int WaitCloseMiliSeconds { get; }
        public MemorySizeEntity MemorySize { get; }
        public string ExceptionMsg { get; }
        public delegate Task DelegateGuiRefreshAsync(bool continueOnCapturedContext);
        public delegate void DelegateGuiRefresh(bool continueOnCapturedContext);
        public bool IsExecute { get; set; }

        #endregion

        #region Constructor and destructor

        public MemoryEntity(int sleepMiliSeconds, int waitCloseMiliSeconds)
        {
            SleepMiliSeconds = sleepMiliSeconds;
            WaitCloseMiliSeconds = waitCloseMiliSeconds;
            MemorySize = new();
            IsExecute = false;
            ExceptionMsg = string.Empty;
        }

        public MemoryEntity() : this(1_000, 1_000) { }

        ~MemoryEntity()
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
}
