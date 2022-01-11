// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared
{
    public partial class LoginDisplay
    {
        #region Public and private fields and properties

        public AuthenticationState Authentication { get; set; }

        #endregion

        #region Public and private methods

        private async Task BeginLogout(MouseEventArgs args)
        {
            await SignOutManager.SetSignOutState();
            Navigation.NavigateTo("authentication/logout");
        }

        #endregion
    }
}
