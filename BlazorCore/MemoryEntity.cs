using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
        public delegate Task DelegateGuiRefresh();
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

        public void Open(DelegateGuiRefresh callRefresh, 
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            IsExecute = true;
            while (IsExecute)
            {
                MemorySize.Physical.Bytes = (ulong)Process.GetCurrentProcess().WorkingSet64;
                MemorySize.Virtual.Bytes = (ulong)Process.GetCurrentProcess().PrivateMemorySize64;
                callRefresh?.Invoke().ConfigureAwait(false);
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
