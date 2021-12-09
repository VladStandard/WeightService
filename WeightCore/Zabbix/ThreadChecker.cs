//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using System;
//using System.Threading;

//namespace WeightCore.Zabbix
//{
//    public class ThreadChecker
//    {
//        #region Private fields and properties

//        private Thread _thread;
//        private readonly object _locker;
//        private CancellationToken _token;
//        private readonly int _threadTimeOut;
//        public event EventHandler EventReloadValues;

//        #endregion

//        #region Constructor and destructor

//        public ThreadChecker(CancellationToken token, int threadTimeOut)
//        {
//            _locker = new object();
//            _token = token;
//            _threadTimeOut = threadTimeOut;
//            // Запуск.
//            Start();
//        }

//        ~ThreadChecker()
//        {
//            Stop();
//        }

//        #endregion

//        #region Public and private methods

//        public void Start()
//        {
//            if (_thread != null)
//                return;
//            try
//            {
//                _thread = new Thread(t =>
//                {
//                    while (!_token.IsCancellationRequested)
//                    {
//                        lock (_locker)
//                        {
//                            EventReloadValues?.Invoke(this, new EventArgs());
//                            Thread.Sleep(_threadTimeOut);
//                        }
//                    }
//                }
//                )
//                { IsBackground = true };
//                _thread.Start();
//            }
//            catch (Exception ex)
//            {
//                _log.Error(ex.Message);
//                _log.Error(ex.StackTrace);
//                _log.Error(ex.InnerException);
//                _log.Error(ex.Source);
//            }
//        }

//        public void Stop()
//        {
//            try
//            {
//                if (_thread != null && _thread.IsAlive)
//                {
//                    _token.ThrowIfCancellationRequested();
//                    Thread.Sleep(1000);
//                    _thread.Join(1000);
//                    _thread.Abort();
//                    _thread = null;
//                }
//            }
//            catch (Exception ex)
//            {
//                _log.Error(ex.Message);
//                _log.Error(ex.StackTrace);
//                _log.Error(ex.InnerException);
//                _log.Error(ex.Source);
//            }
//        }

//        #endregion
//    }
//}
