// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
namespace WeightCore.Helpers
{
    public class DebugHelper
    {
        #region Design pattern "Lazy Singleton"

        private static DebugHelper _instance;
        public static DebugHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private methods

        public bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        #endregion
    }
}
