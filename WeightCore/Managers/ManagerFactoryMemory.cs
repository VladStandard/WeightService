// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableModels;
using DataShareCore;

namespace WeightCore.Managers
{
    public class ManagerFactoryMemory : ManagerBase
    {
        #region Public and private fields and properties

        public MemorySizeEntity MemorySize { get; private set; } = new MemorySizeEntity();

        #endregion

        #region Public and private methods

        public void Open(ProjectsEnums.TaskType taskType, SqlViewModelEntity sqlViewModel, ScaleDirect currentScale)
        {
            Open(taskType, sqlViewModel,
            () => {
                //
            },
            () => {
                //
            },
            () => {
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
