// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Shared.Sys
{
    public partial class Sql
    {
        #region Public and private fields and properties

        public BlazorCore.DAL.DataAccessEntity DataAccess { get; set; }
        public bool IsDisabled { get => DataAccess.IsDisabled; set => _ = value; }

        #endregion

        #region Public and private methods

        private void Change(string value, string name)
        {
            StateHasChanged();
        }

        #endregion
    }
}