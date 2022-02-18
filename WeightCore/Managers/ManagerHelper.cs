// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL;
using DataCore.DAL.TableDirectModels;
using DataCore.Models;

namespace WeightCore.Managers
{
    public class ManagerHelper : DisposableBase
    {
        #region Public and private fields and properties

        public ManagerMassa Massa { get; private set; } = new ManagerMassa();
        public ManagerMemory Memory { get; private set; } = new ManagerMemory();
        public ManagerPrintV2 Print { get; private set; } = new ManagerPrintV2();

        #endregion

        #region Constructor and destructor

        public ManagerHelper()
        {
            Init(CloseMethod, ReleaseManaged, ReleaseUnmanaged);
        }

        ~ManagerHelper()
        {
            Massa?.Dispose(false);
            Memory?.Dispose(false);
            Print?.Dispose(false);
        }

        #endregion

        #region Public and private methods

        public void Init(ScaleDirect currentScale, bool isTscPrinter)
        {
            Massa.Init(currentScale);
            Memory.Init();
            Print.Init(isTscPrinter, currentScale.ZebraPrinter.Name, currentScale.ZebraPrinter.Ip, currentScale.ZebraPrinter.Port);
        }

        public void Open(SqlViewModelEntity sqlViewModel)
        {
            Open();
            Massa.Open(sqlViewModel);
            Memory.Open(sqlViewModel);
            Print.Open(sqlViewModel);
        }

        public void OpenMassa(SqlViewModelEntity sqlViewModel)
        {
            Massa.Open(sqlViewModel);
        }

        public void OpenMemory(SqlViewModelEntity sqlViewModel)
        {
            Memory.Open(sqlViewModel);
        }

        public void OpenPrint(SqlViewModelEntity sqlViewModel)
        {
            Print.Open(sqlViewModel);
        }

        public void CloseMethod()
        {
            Massa.Close();
            Memory.Close();
            Print.Close();
        }

        public void ReleaseManaged()
        {
            Massa.ReleaseManaged();
            Memory.ReleaseManaged();
            Print.ReleaseManaged();
        }

        public void ReleaseUnmanaged()
        {
            Massa.ReleaseUnmanaged();
            Memory.ReleaseUnmanaged();
            Print.ReleaseUnmanaged();
        }

        #endregion
    }
}
