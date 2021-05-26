using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Hardware
{
    /// <summary>
    /// Task memory.
    /// </summary>
    public class MemoryManagerEntity
    {
        #region Public and private fields and properties

        public int WaitWhileMiliSeconds { get; private set; }
        public int WaitExceptionMiliSeconds { get; private set; }
        public int WaitCloseMiliSeconds { get; private set; }
        public MemorySizeEntity MemorySize { get; private set; }
        public string ExceptionMsg { get; private set; }
        public delegate Task CallbackAsync(int wait);
        public bool IsExecute { get; set; }

        #endregion

        #region Constructor and destructor

        public MemoryManagerEntity(int waitWhileMiliSeconds, int waitExceptionMiliSeconds, int waitCloseMiliSeconds, ulong limitBytes)
        {
            WaitWhileMiliSeconds = waitWhileMiliSeconds;
            WaitExceptionMiliSeconds = waitExceptionMiliSeconds;
            WaitCloseMiliSeconds = waitCloseMiliSeconds;
            MemorySize = new MemorySizeEntity(limitBytes);
            IsExecute = false;
        }

        #endregion

        #region Public and private methods

        public void Open(CallbackAsync callRefresh, 
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            IsExecute = true;
            while (IsExecute)
            {
                try
                {
                    MemorySize.Physical.Bytes = (ulong)Process.GetCurrentProcess().WorkingSet64;
                    MemorySize.Virtual.Bytes = (ulong)Process.GetCurrentProcess().PrivateMemorySize64;
                    //callRefresh?.Invoke().ConfigureAwait(false);
                    callRefresh(WaitWhileMiliSeconds).ConfigureAwait(false);
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitWhileMiliSeconds));
                }
                catch (TaskCanceledException)
                {
                    // Console.WriteLine(tcex.Message);
                    // Not the problem.
                }
                catch (Exception ex)
                {
                    ExceptionMsg = ex.Message;
                    if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                        ExceptionMsg += Environment.NewLine + ex.InnerException.Message;
                    Console.WriteLine(ExceptionMsg);
                    Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitExceptionMiliSeconds));
                }
            }
        }

        public void Close([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                IsExecute = false;
            }
            catch (Exception ex)
            {
                ExceptionMsg = ex.Message;
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    ExceptionMsg += Environment.NewLine + ex.InnerException.Message;
                Console.WriteLine(ExceptionMsg);
                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
                Thread.Sleep(TimeSpan.FromMilliseconds(WaitExceptionMiliSeconds));
            }
        }

        #endregion
    }
}
