// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;

namespace BlazorDeviceControl.Shared
{
    public partial class Index
    {
        #region Public and private fields and properties

        public string IdDescription => $"{LocaleCore.Strings.AuthorizingId}: {UserSettings.Identity.Id}";
        public string IpAddressDescription => $"{LocaleCore.Strings.AuthorizingApAddress}: {UserSettings.Identity.IpAddress}";
        public string UserDescription => $"{LocaleCore.Strings.AuthorizingUserName}: {UserSettings.Identity.UserName}";

        #endregion

        #region Constructor and destructor

        public Index() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        //

        #endregion
    }
}
