// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using WeightCore.Memory;

namespace WeightCore.Managers
{
    /// <summary>
    /// Task memory.
    /// </summary>
    public class MemoryManagerHelper : ManagerEntity
    {
        #region Design pattern "Lazy Singleton"

        private static MemoryManagerHelper _instance;
        public static MemoryManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public MemorySizeEntity MemorySize { get; private set; } = new MemorySizeEntity();

        #endregion

        #region Constructor and destructor

        public new void Init(int waitReopen = 1_000, int waitResponse = 500, int waitRequest = 250, int waitClose = 2_000, int waitException = 1_000)
        {
            if (IsInit)
                return;
            IsInit = true;
            Init(waitReopen, waitResponse, waitRequest, waitClose, waitException);
        }

        #endregion

        #region Public and private methods - Manager

        public void Open()
        {
            lock (Locker)
            {
                MemorySize.Update();
            }
        }

        public void Close()
        {
            lock (Locker)
            {
                //
            }
        }

        #endregion
    }
}