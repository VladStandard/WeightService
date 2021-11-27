// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataShareCore.Memory;

namespace WeightCore.Managers
{
    public class ManagerFactoryMemory : ManagerBase
    {
        #region Public and private fields and properties

        public MemorySizeEntity MemorySize { get; private set; } = new MemorySizeEntity();

        #endregion

        #region Public and private methods

        public void Init()
        {
            Init(
            () =>
            {
                //
            },
            1_000, 500, 250, 2_000, 1_000);
        }

        public void Open(SqlViewModelEntity sqlViewModel)
        {
            Open(ProjectsEnums.TaskType.MemoryManager, sqlViewModel,
            () =>
            {
                //
            },
            () =>
            {
                //
            },
            () =>
            {
                //
            });
        }

        public void Close()
        {
            Close(() =>
            {
                //
            });
        }

        #endregion
    }
}
