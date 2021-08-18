// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class EntitySave
    {
        #region Public and private fields and properties

        [Parameter] public BaseEntity Item { get; set; }

        #endregion

        #region Public and private methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            //RadzenButton buttonSave = Page.;
        }

        #endregion
    }
}