// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using System.Threading;

namespace WeightCore.Managers
{
    public class ManagerControllerHelper : DisposableBase
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static ManagerControllerHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static ManagerControllerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public ManagerLabels Labels { get; private set; }
        public ManagerMassa Massa { get; private set; }
        public ManagerMemory Memory { get; private set; }
        public ManagerPrint PrintMain { get; private set; }
        public ManagerPrint PrintShipping { get; private set; }
        private readonly object _locker = new();

        #endregion

        #region Constructor and destructor

        public ManagerControllerHelper()
        {
            lock (_locker)
            {
                Labels = new();
                Massa = new();
                Memory = new();
                PrintMain = new();
                PrintShipping = new();
                Init(Close, ReleaseManaged, ReleaseUnmanaged);
            }
        }

        ~ManagerControllerHelper()
        {
            Labels?.Dispose();
            Massa?.Dispose(false);
            Memory?.Dispose(false);
            PrintMain?.Dispose(false);
            PrintShipping?.Dispose(false);
        }

        #endregion

        #region Public and private methods

        public new void Open()
        {
            base.Open();
        }

        public new void Close()
        {
            base.Close();
        }

        public void ReleaseManaged()
        {
            Labels?.ReleaseManaged();
            Massa?.ReleaseManaged();
            Memory?.ReleaseManaged();
            PrintMain?.ReleaseManaged();
            PrintShipping?.ReleaseManaged();
        }

        public void ReleaseUnmanaged()
        {
            Labels?.ReleaseUnmanaged();
            Massa?.ReleaseUnmanaged();
            Memory?.ReleaseUnmanaged();
            PrintMain?.ReleaseUnmanaged();
            PrintShipping?.ReleaseUnmanaged();
        }

        #endregion
    }
}
