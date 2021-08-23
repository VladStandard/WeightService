// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL.TableModels;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class PrinterType
    {
        #region Public and private fields and properties

        private PrinterTypeEntity PrinterTypeItem => IdItem is PrinterTypeEntity idItem ? idItem : null;

        #endregion

        #region Public and private methods

        #endregion
    }
}
