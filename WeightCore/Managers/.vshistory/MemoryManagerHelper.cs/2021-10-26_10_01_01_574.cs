// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.Helpers;
using WeightCore.Memory;

namespace WeightCore.Managers
{
    /// <summary>
    /// Task memory.
    /// </summary>
    public class MemoryManagerHelper
    {
        #region Design pattern "Lazy Singleton"

        private static MemoryManagerHelper _instance;
        public static MemoryManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties - Manager

        public int WaitWhileMiliSeconds { get; private set; }
        public int WaitExceptionMiliSeconds { get; private set; }
        public int WaitCloseMiliSeconds { get; private set; }
        public string ExceptionMsg { get; private set; } = string.Empty;
        public delegate void Callback();
        public bool IsExecute { get; set; } = false;

        #endregion

        #region Public and private fields and properties

        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        public MemorySizeEntity MemorySize { get; private set; } = new MemorySizeEntity();

        #endregion

        #region Constructor and destructor

        public void Init(int waitWhileMiliSeconds, int waitExceptionMiliSeconds, int waitCloseMiliSeconds)
        {
            // Manager.
            WaitWhileMiliSeconds = waitWhileMiliSeconds;
            WaitExceptionMiliSeconds = waitExceptionMiliSeconds;
            WaitCloseMiliSeconds = waitCloseMiliSeconds;
        }

        #endregion

        #region Public and private methods - Manager

        public void Open(Callback callback)
        {
            IsExecute = true;
            while (IsExecute)
            {
                try
                {
                    MakeJob();
                    callback();
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitWhileMiliSeconds));
                }
                catch (TaskCanceledException)
                {
                    // Console.WriteLine(tcex.Message);
                    // Not the problem.
                }
                catch (Exception ex)
                {
                    _exception.Catch(null, ref ex);
                }
            }
        }

        public void Close()
        {
            try
            {
                IsExecute = false;
                MakeJob();
            }
            catch (Exception ex)
            {
                _exception.Catch(null, ref ex);
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