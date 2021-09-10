// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.Utils;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.Memory;

namespace WeightCore.Managers
{
    /// <summary>
    /// Task memory.
    /// </summary>
    public class MemoryManagerEntity
    {
        #region Public and private fields and properties - Manager

        public int WaitWhileMiliSeconds { get; private set; }
        public int WaitExceptionMiliSeconds { get; private set; }
        public int WaitCloseMiliSeconds { get; private set; }
        public string ExceptionMsg { get; private set; } = string.Empty;
        public delegate void Callback(bool isTaskEnabled);
        public bool IsExecute { get; set; } = false;

        #endregion

        #region Public and private fields and properties

        public MemorySizeEntity MemorySize { get; private set; }
        private readonly LogUtils _logUtils = LogUtils.Instance;

        #endregion

        #region Constructor and destructor

        public MemoryManagerEntity(int waitWhileMiliSeconds, int waitExceptionMiliSeconds, int waitCloseMiliSeconds)
        {
            // Manager.
            WaitWhileMiliSeconds = waitWhileMiliSeconds;
            WaitExceptionMiliSeconds = waitExceptionMiliSeconds;
            WaitCloseMiliSeconds = waitCloseMiliSeconds;
            // Other.
            MemorySize = new MemorySizeEntity();
        }

        #endregion

        #region Public and private methods - Manager

        public void Open(Callback callback, bool isTaskEnabled,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            IsExecute = true;
            while (IsExecute)
            {
                try
                {
                    MakeJob();
                    callback(isTaskEnabled);
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
                    if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                        ExceptionMsg += Environment.NewLine + ex.InnerException.Message;
                    _logUtils.Error(ExceptionMsg, filePath, memberName, lineNumber);
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitExceptionMiliSeconds));
                    throw;
                }
                //System.Windows.Forms.Application.DoEvents();
            }
        }

        public void Close([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                IsExecute = false;
                //Thread.Sleep(TimeSpan.FromMilliseconds(WaitWhileMiliSeconds));
                MakeJob();
            }
            catch (Exception ex)
            {
                ExceptionMsg = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    ExceptionMsg += Environment.NewLine + ex.InnerException.Message;
                _logUtils.Error(ExceptionMsg, filePath, memberName, lineNumber);
                Thread.Sleep(TimeSpan.FromMilliseconds(WaitExceptionMiliSeconds));
                throw;
            }
        }

        #endregion

        #region Public and private methods

        public void MakeJob()
        {
            MemorySize.Physical.Bytes = (ulong)Process.GetCurrentProcess().WorkingSet64;
            MemorySize.Virtual.Bytes = (ulong)Process.GetCurrentProcess().PrivateMemorySize64;
        }

        #endregion
    }
}