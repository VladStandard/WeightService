// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;

namespace ZabbixStubService.Zabbix
{
    public class FakeCheckThreadByLog
    {
        #region Private fields and properties

        private Thread _listenerThread;
        private readonly object _locker = new();
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
