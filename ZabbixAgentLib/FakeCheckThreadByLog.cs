// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using log4net;
using System;
using System.Threading;
// ReSharper disable IdentifierTypo

namespace ZabbixAgentLib
{
    public class FakeCheckThreadByLog
    {
        #region Private fields and properties

        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Thread _listenerThread;
        private readonly object _locker = new object();
        private CancellationToken _token;
        private readonly int _threadTimeOut;
        private readonly Action _reloadValuesFunc = null;

        #endregion

        #region Constructor and destructor

        public FakeCheckThreadByLog(Action reloadValuesFunc, CancellationToken token, int threadTimeOut)
        {
            _reloadValuesFunc = reloadValuesFunc;
            _token = token;
            _threadTimeOut = threadTimeOut;
        }

        ~FakeCheckThreadByLog()
        {
            try
            {
                if (_listenerThread != null && _listenerThread.IsAlive)
                {
                    _token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                    _listenerThread.Join(1000);
                    _listenerThread.Abort();
                    _listenerThread = null;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                _log.Error(ex.StackTrace);
                _log.Error(ex.InnerException);
                _log.Error(ex.Source);
            }
        }

        #endregion

        #region Public and private methods

        public void Start()
        {
            try
            {
                _listenerThread = new Thread(t =>
                {
                    while (!_token.IsCancellationRequested)
                    {
                        lock (_locker)
                        {
                            _reloadValuesFunc();
                            Thread.Sleep(_threadTimeOut);
                        }
                    }
                }
                )
                { IsBackground = true };
                _listenerThread.Start();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                _log.Error(ex.StackTrace);
                _log.Error(ex.InnerException);
                _log.Error(ex.Source);

            }
        }

        #endregion
    }
}
