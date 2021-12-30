// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.TableSystemModels;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Host
    {
        #region Public and private fields and properties

        public HostEntity HostItem { get => (HostEntity)Item; set => Item = value; } 

        #endregion

        #region Public and private methods

        //

        #endregion
    }
}
