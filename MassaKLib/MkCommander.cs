// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// https://docs.microsoft.com/ru-ru/dotnet/api/system.threading.thread.abort?view=netframework-4.8

using System.Threading;
// ReSharper disable CheckNamespace

namespace WeightServices.Common.MK
{
    public class MkCommander
    {
        #region Public fields and properties

        public static readonly int CommandThreadTimeOut = 500;
        public static readonly int CommandCountPackage = 1;

        #endregion

        #region Private fields and properties

        private static readonly object Locker = new object();
        private readonly LogHelper _log = LogHelper.Instance;
        private readonly Thread _commandThread;
        private MkDeviceEntity _mkDevice;
        private bool _work;

        #endregion

        #region Constructor and destructor

        public MkCommander(MkDeviceEntity mkDeviceEntity)
        {
            _work = true;
            _mkDevice = mkDeviceEntity;
            _commandThread = new Thread(SharingSession) { IsBackground = true };
            _commandThread.Start();
            Thread.Sleep(100);
        }

        public void SharingSession()
        {
            while (_work)
            {
                lock (Locker)
                {
                    for (var i = 0; i < CommandCountPackage; i++)
                    {
                        _mkDevice.GetMassa();
                    }
                }
                Thread.Sleep(CommandThreadTimeOut);
            }
        }

        ~MkCommander()
        {
            if (_commandThread != null && _commandThread.IsAlive)
            {
                _work = false;
                Thread.Sleep(200);
                _commandThread.Abort();
                _commandThread.Join(1000);
            }
        }

        #endregion

    }
}
