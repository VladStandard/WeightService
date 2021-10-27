// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.Helpers;

namespace WeightCore.Managers
{
    /// <summary>
    /// Task memory.
    /// </summary>
    public class DeviceManagerHelper
    {
        #region Design pattern "Lazy Singleton"

        private static DeviceManagerHelper _instance;
        public static DeviceManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        private readonly LogHelper _log = LogHelper.Instance;
        public int WaitWhileMiliSeconds { get; private set; }
        public int WaitExceptionMiliSeconds { get; private set; }
        public int WaitCloseMiliSeconds { get; private set; }
        public string ExceptionMsg { get; private set; }
        public delegate void Callback();
        public bool IsExecute { get; set; }

        #endregion

        #region Constructor and destructor

        public void Init(int waitWhileMiliSeconds, int waitExceptionMiliSeconds, int waitCloseMiliSeconds)
        {
            // Manager.
            WaitWhileMiliSeconds = waitWhileMiliSeconds;
            WaitExceptionMiliSeconds = waitExceptionMiliSeconds;
            WaitCloseMiliSeconds = waitCloseMiliSeconds;
            IsExecute = false;
        }

        #endregion

        #region Public and private methods - Manager

        public void Open(Callback callback)
        {
            if (WaitWhileMiliSeconds == 0)
                return;
            IsExecute = true;
            while (IsExecute)
            {
                try
                {
                    OpenJob();
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
                    throw;
                }
            }
        }

        public void Close()
        {
            try
            {
                IsExecute = false;
                CloseJob();
            }
            catch (Exception ex)
            {
                _exception.Catch(null, ref ex);
            }
        }

        #endregion

        #region Public and private methods

        public void OpenJob()
        {
            //
        }

        public void CloseJob()
        {
            //
        }

        #endregion
    }
}
