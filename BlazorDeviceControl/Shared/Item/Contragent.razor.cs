// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.TableModels;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Contragent
    {
        #region Public and private fields and properties

        private ContragentEntity ContragentItem => IdItem is ContragentEntity idItem ? idItem : null;

        #endregion

        #region Public and private methods

        //

        #endregion
    }
}
