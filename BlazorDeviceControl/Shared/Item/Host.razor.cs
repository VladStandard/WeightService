// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.TableSystemModels;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Host
    {
        #region Public and private fields and properties

        private HostEntity HostItem => IdItem is HostEntity idItem ? idItem : null;

        #endregion

        #region Public and private methods

        //

        #endregion
    }
}
