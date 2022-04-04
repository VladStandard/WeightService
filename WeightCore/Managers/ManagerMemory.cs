// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL;
using DataCore.Memory;

namespace WeightCore.Managers
{
    public class ManagerMemory : ManagerBase
    {
        #region Public and private fields and properties

        public MemorySizeEntity MemorySize { get; private set; } = new MemorySizeEntity();

        #endregion

        #region Constructor and destructor

        public ManagerMemory() : base()
        {
            Init(CloseMethod, ReleaseManaged, ReleaseUnmanaged);
        }

        #endregion

        #region Public and private methods

        public void Init()
        {
            Init(ProjectsEnums.TaskType.MemoryManager, null, 1_000);
        }

        public void Open(SqlViewModelEntity sqlViewModel)
        {
            Open(sqlViewModel, false,
            () =>
            {
                MemorySize.Open();
            },
            null,
            null);
        }

        public new void CloseMethod()
        {
            base.CloseMethod();
        }

        public new void ReleaseManaged()
        {
            base.ReleaseManaged();

            MemorySize?.Close();
            MemorySize?.Dispose(false);
            MemorySize = null;
        }

        public new void ReleaseUnmanaged()
        {
            base.ReleaseUnmanaged();
        }

        #endregion
    }
}
