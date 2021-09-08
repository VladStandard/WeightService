// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com


//using System;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Terra.Common
//{
//    /// <summary>
//    /// Task memory.
//    /// </summary>
//    public class MemoryManagerEntity
//    {
//        #region Public and private fields and properties - Manager

//        public int WaitWhileMiliSeconds { get; private set; }
//        public int WaitExceptionMiliSeconds { get; private set; }
//        public int WaitCloseMiliSeconds { get; private set; }
//        public string ExceptionMsg { get; private set; }
//        public delegate Task CallbackAsync(int wait);
//        public bool IsExecute { get; set; }

//        #endregion

//        #region Public and private fields and properties

//        public MemorySizeEntity MemorySize { get; private set; }

//        #endregion

//        #region Constructor and destructor

//        public MemoryManagerEntity(int waitWhileMiliSeconds, int waitExceptionMiliSeconds, int waitCloseMiliSeconds)
//        {
//            // Manager.
//            WaitWhileMiliSeconds = waitWhileMiliSeconds;
//            WaitExceptionMiliSeconds = waitExceptionMiliSeconds;
//            WaitCloseMiliSeconds = waitCloseMiliSeconds;
//            IsExecute = false;
//            // Other.
//            MemorySize = new MemorySizeEntity();
//        }

//        #endregion

//        #region Public and private methods - Manager

//        public void Open(CallbackAsync callback, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
//        {
//            IsExecute = true;
//            while (IsExecute)
//            {
//                try
//                {
//                    OpenJob();
//                    callback(WaitWhileMiliSeconds).ConfigureAwait(true);
//                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitWhileMiliSeconds));
//                }
//                catch (TaskCanceledException)
//                {
//                    // Console.WriteLine(tcex.Message);
//                    // Not the problem.
//                }
//                catch (Exception ex)
//                {
//                    ExceptionMsg = ex.Message;
//                    if (!string.IsNullOrEmpty(ex.InnerException?.Message))
//                        ExceptionMsg += Environment.NewLine + ex.InnerException.Message;
//                    Console.WriteLine(ExceptionMsg);
//                    Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
//                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitExceptionMiliSeconds));
//                }
//            }
//        }

//        public void Close([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
//        {
//            try
//            {
//                IsExecute = false;
//                Thread.Sleep(TimeSpan.FromMilliseconds(WaitWhileMiliSeconds));
//                CloseJob();
//            }
//            catch (Exception ex)
//            {
//                ExceptionMsg = ex.Message;
//                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
//                    ExceptionMsg += Environment.NewLine + ex.InnerException.Message;
//                Console.WriteLine(ExceptionMsg);
//                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
//                Thread.Sleep(TimeSpan.FromMilliseconds(WaitExceptionMiliSeconds));
//            }
//        }

//        #endregion

//        #region Public and private methods

//        public void OpenJob()
//        {
//            MemorySize.Physical.Bytes = (ulong)Process.GetCurrentProcess().WorkingSet64;
//            MemorySize.Virtual.Bytes = (ulong)Process.GetCurrentProcess().PrivateMemorySize64;
//        }

//        public void CloseJob()
//        {
//            MemorySize.Physical.Bytes = (ulong)Process.GetCurrentProcess().WorkingSet64;
//            MemorySize.Virtual.Bytes = (ulong)Process.GetCurrentProcess().PrivateMemorySize64;
//        }

//        #endregion
//    }
//}