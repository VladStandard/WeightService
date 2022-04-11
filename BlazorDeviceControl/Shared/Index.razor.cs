// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;

namespace BlazorDeviceControl.Shared
{
    public partial class Index
    {
        #region Public and private fields and properties

        public string UserDescription => LocalizationCore.Strings.Main.AuthorizingUserName + " : " + UserSettings.Identity.Name;

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
