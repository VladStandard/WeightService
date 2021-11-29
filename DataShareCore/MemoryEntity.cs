// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.Memory;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DataShareCore
{
    /// <summary>
    /// Task memory.
    /// </summary>
    public class MemoryEntity
    {
        #region Public and private fields and properties

        public int SleepMiliSeconds { get; private set; }
        public int WaitCloseMiliSeconds { get; private set; }
        public MemorySizeEntity MemorySize { get; private set; }
        public string ExceptionMsg { get; private set; }
        public delegate Task DelegateGuiRefreshAsync(bool continueOnCapturedContext);
        public delegate void DelegateGuiRefresh();
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

        #endregion

        #region Public and private methods

        public void Open(DelegateGuiRefreshAsync callRefreshAsync)
        {
            IsExecute = true;
            while (IsExecute)
            {
                Process proc = Process.GetCurrentProcess();
                if (proc != null)
                {
                    MemorySize.PhysicalCurrent.Bytes = (ulong)proc.WorkingSet64;
                    MemorySize.VirtualCurrent.Bytes = (ulong)proc.PrivateMemorySize64;
                }
                else {
                    MemorySize.PhysicalCurrent.Bytes = 0;
                    MemorySize.VirtualCurrent.Bytes = 0;
                }
                callRefreshAsync?.Invoke(true).ConfigureAwait(false);
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
