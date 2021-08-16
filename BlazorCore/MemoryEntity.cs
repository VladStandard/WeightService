using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorCore
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
        }

        #endregion

        #region Public and private methods

        [SupportedOSPlatform("windows")]
        public void Open(DelegateGuiRefreshAsync callRefreshAsync)
        {
            IsExecute = true;
            while (IsExecute)
            {
                Process proc = Process.GetCurrentProcess();
                if (proc != null)
                {
                    MemorySize.Physical.Bytes = (ulong)proc.WorkingSet64;
                    MemorySize.Virtual.Bytes = (ulong)proc.PrivateMemorySize64;
                }
                else {
                    MemorySize.Physical.Bytes = 0;
                    MemorySize.Virtual.Bytes = 0;
                }
                callRefreshAsync?.Invoke(true).ConfigureAwait(false);
                Thread.Sleep(TimeSpan.FromMilliseconds(SleepMiliSeconds));
            }
        }

        public void Close([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            IsExecute = false;
        }

        #endregion
    }
}
