// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.Memory;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DataShareCore
{
    public class MemoryEntity
    {
        #region Public and private fields and properties

        public int SleepMiliSeconds { get; private set; }
        public int WaitCloseMiliSeconds { get; private set; }
        public MemorySizeEntity MemorySize { get; private set; }
        public string ExceptionMsg { get; private set; }
        public delegate Task DelegateGuiRefreshAsync(bool continueOnCapturedContext);
        public delegate void DelegateGuiRefresh(bool continueOnCapturedContext);
        public bool IsExecute { get; set; }

        #endregion

        #region Constructor and destructor

        public MemoryEntity(int sleepMiliSeconds, int waitCloseMiliSeconds)
        {
            SleepMiliSeconds = sleepMiliSeconds;
            WaitCloseMiliSeconds = waitCloseMiliSeconds;
            MemorySize = new MemorySizeEntity();
            IsExecute = false;
            ExceptionMsg = string.Empty;
        }

        ~MemoryEntity()
        {
            MemorySize.Dispose(false);
        }

        #endregion

        #region Public and private methods

        public void Open(DelegateGuiRefreshAsync callRefreshAsync)
        {
            IsExecute = true;
            Process? proc = Process.GetCurrentProcess();
            while (IsExecute)
            {
                if (proc != null)
                {
                    if (MemorySize?.PhysicalCurrent != null)
                        MemorySize.PhysicalCurrent.Bytes = (ulong)proc.WorkingSet64;
                    if (MemorySize?.VirtualCurrent != null)
                        MemorySize.VirtualCurrent.Bytes = (ulong)proc.PrivateMemorySize64;
                }
                else
                {
                    if (MemorySize?.PhysicalCurrent != null)
                        MemorySize.PhysicalCurrent.Bytes = 0;
                    if (MemorySize?.VirtualCurrent != null)
                        MemorySize.VirtualCurrent.Bytes = 0;
                }
                //callRefreshAsync?.Invoke(false).ConfigureAwait(false);
                Thread.Sleep(TimeSpan.FromMilliseconds(SleepMiliSeconds));
            }
        }

        public void Close()
        {
            IsExecute = false;
        }

        #endregion
    }
}
