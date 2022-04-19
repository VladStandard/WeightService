// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using WeightCore.Helpers;
using WeightCore.Print;

namespace WeightCore.Managers
{
    public class ManagerEntity : DisposableBase
    {
        #region Public and private fields and properties

        public ManagerLabels Labels { get; private set; }
        public ManagerMassa Massa { get; private set; }
        public ManagerMemory Memory { get; private set; }
        public ManagerPrint PrintMain { get; private set; }
        public ManagerPrint PrintShipping { get; private set; }

        #endregion

        #region Constructor and destructor

        public ManagerEntity()
        {
            Labels = new();
            Massa = new();
            Memory = new();
            PrintMain = new();
            PrintShipping = new();
            Init(Close, ReleaseManaged, ReleaseUnmanaged);
        }

        ~ManagerEntity()
        {
            Labels?.Dispose();
            Massa?.Dispose(false);
            Memory?.Dispose(false);
            PrintMain?.Dispose(false);
            PrintShipping?.Dispose(false);
        }

        #endregion

        #region Public and private methods

        public void Init(ScaleEntity currentScale, PrintBrand printBrandMain, PrintBrand printBrandShipping)
        {
            Massa.Init(currentScale);
            PrintMain.Init(printBrandMain, currentScale.PrinterMain.Name, currentScale.PrinterMain.Ip, currentScale.PrinterMain.Port);
            PrintShipping.Init(printBrandShipping, currentScale.PrinterShipping.Name, currentScale.PrinterShipping.Ip, currentScale.PrinterShipping.Port);
            PrintShipping.Init(printBrandMain, currentScale.PrinterMain.Name, currentScale.PrinterMain.Ip, currentScale.PrinterMain.Port);
        }

        public new void Open()
        {
            base.Open();
            if (SessionStateHelper.Instance.IsCurrentPluCheckWeight)
                Massa?.Open();
            PrintMain?.Open();
        }

        public new void Close()
        {
            base.Close();
            Massa?.Close();
            PrintMain?.Close();
        }

        public void ReleaseManaged()
        {
            Labels?.ReleaseManaged();
            Massa?.ReleaseManaged();
            Memory?.ReleaseManaged();
            PrintMain?.ReleaseManaged();
        }

        public void ReleaseUnmanaged()
        {
            Labels?.ReleaseUnmanaged();
            Massa?.ReleaseUnmanaged();
            Memory?.ReleaseUnmanaged();
            PrintMain?.ReleaseUnmanaged();
        }

        #endregion
    }
}
