// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableModels;

namespace WeightCore.Managers
{
    public class ManagerFactoryPrint : ManagerBase
    {
        #region Public and private fields and properties



        #endregion

        #region Public and private methods

        public void Init(ScaleDirect currentScale)
        {
            Init(
            () =>
            {
                PrintManager.Init(IsTscPrinter, currentScale.ZebraPrinter.Name, currentScale.ZebraPrinter.Ip, currentScale.ZebraPrinter.Port);
            },
            10_000, 500, 250, 2_000, 1_000);
        }

        public void Open(ProjectsEnums.TaskType taskType, SqlViewModelEntity sqlViewModel, ScaleDirect currentScale)
        {
            Open(taskType, sqlViewModel,
            () => {
                PrintManager.Open();
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
