// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL;
using DataCore.DAL.TableDirectModels;
using DataCore.Models;
using WeightCore.Print;

namespace WeightCore.Managers
{
    public class ManagerEntity : DisposableBase
    {
        #region Public and private fields and properties

        public ManagerMassa Massa { get; private set; } = new ManagerMassa();
        public ManagerMemory Memory { get; private set; } = new ManagerMemory();
        public ManagerPrint PrintMain { get; private set; } = new ManagerPrint();
        public ManagerPrint PrintShipping { get; private set; } = new ManagerPrint();

        #endregion

        #region Constructor and destructor

        public ManagerEntity()
        {
            Init(CloseMethod, ReleaseManaged, ReleaseUnmanaged);
        }

        //~ManagerEntity()
        //{
        //    Massa?.Dispose(false);
        //    Memory?.Dispose(false);
        //    PrintMain?.Dispose(false);
        //    PrintShipping?.Dispose(false);
        //}

        #endregion

        #region Public and private methods

        public void Init(ScaleDirect currentScale, PrintBrand printBrandMain, PrintBrand printBrandShipping)
        {
            Massa.Init(currentScale);
            Memory.Init();
            PrintMain.Init(printBrandMain, currentScale.PrinterMain.Name, currentScale.PrinterMain.Ip, currentScale.PrinterMain.Port);
            PrintShipping.Init(printBrandShipping, currentScale.PrinterShipping.Name, currentScale.PrinterShipping.Ip, currentScale.PrinterShipping.Port);
            PrintShipping.Init(printBrandMain, currentScale.PrinterMain.Name, currentScale.PrinterMain.Ip, currentScale.PrinterMain.Port);
        }

        public void Open(SqlViewModelEntity sqlViewModel, bool isCheckWeight)
        {
            Open();
            Massa.Open(sqlViewModel, isCheckWeight);
            Memory.Open(sqlViewModel);
            PrintMain.Open(sqlViewModel);
        }

        public void OpenMassa(SqlViewModelEntity sqlViewModel, bool isCheckWeight)
        {
            Massa.Open(sqlViewModel, isCheckWeight);
        }

        public void OpenMemory(SqlViewModelEntity sqlViewModel)
        {
            Memory.Open(sqlViewModel);
        }

        public void OpenPrint(SqlViewModelEntity sqlViewModel)
        {
            PrintMain.Open(sqlViewModel);
        }

        public void CloseMethod()
        {
            Massa.Close();
            Memory.Close();
            PrintMain.Close();
        }

        public void ReleaseManaged()
        {
            Massa.ReleaseManaged();
            Memory.ReleaseManaged();
            PrintMain.ReleaseManaged();
        }

        public void ReleaseUnmanaged()
        {
            Massa.ReleaseUnmanaged();
            Memory.ReleaseUnmanaged();
            PrintMain.ReleaseUnmanaged();
        }

        #endregion
    }
}
