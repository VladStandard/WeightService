// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors;

public partial class Index : BlazorCore.Models.RazorBase
{
    #region Public and private fields, properties, constructor

    private string IdDescription => $"{LocaleCore.Strings.AuthorizingId}: {UserSettings.Identity.Id}";
    private string IpAddressDescription => $"{LocaleCore.Strings.AuthorizingApAddress}: {UserSettings.Identity.IpAddress}";
    private string UserDescription => $"{LocaleCore.Strings.AuthorizingUserName}: {UserSettings.Identity.UserName}";

    #endregion

    #region Public and private methods

    //

    #endregion
}
