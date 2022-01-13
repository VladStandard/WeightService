// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.Models;
using DataShareCore;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading.Tasks;
using Toolbelt.Blazor.HotKeys;

namespace BlazorCore.Models
{
    public class UserSettingsEntity
    {
        #region Public and private fields and properties

        public IdentityEntity Identity { get; private set; } = new IdentityEntity();
        public HotKeys HotKeysItem { get; private set; }
        public HotKeysContext HotKeysContextItem { get; set; }

        #endregion

        #region Public and private methods

        //public void SetupIdentity(AuthenticationStateProvider stateProvider)
        public void SetupIdentity(AuthenticationState authenticationState)
        {
            Identity.Name = string.Empty;
            //if (stateProvider == null)
            //    return;

            //System.Threading.Tasks.Task<AuthenticationState> state = stateProvider.GetAuthenticationStateAsync();
            //if (!state.IsCompleted)
            //    return;

            //AuthenticationState authenticationState = state.Result;
            if (authenticationState?.User == null)
                return;
            IIdentity identity = authenticationState.User.Identity;

            if (identity == null)
                return;
            try
            {
                Identity.Name = identity.IsAuthenticated == true && !string.IsNullOrEmpty(identity.Name)
                    ? identity.Name : LocalizationCore.Strings.Main.IdentityError;
            }
            catch (Exception)
            {
                Identity.Name = LocalizationCore.Strings.Main.IdentityError;
            }
        }

        public void SetupUserAccessLevel(DataAccessEntity dataAccess,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (dataAccess != null)
            {
                object[] objects = dataAccess.Crud.GetEntitiesNativeObject(SqlQueries.DbServiceManaging.Tables.Access.GetAccessUser(Identity.Name),
                    filePath, lineNumber, memberName);
                if (objects.Length == 1)
                {
                    if (objects[0] is object[] { Length: 5 } item)
                    {
                        if (Guid.TryParse(Convert.ToString(item[0]), out _))
                        {
                            if (item[4] != null)
                            {
                                Identity.AccessLevel = Convert.ToBoolean(item[4]);
                                return;
                            }
                        }
                    }
                }
            }
        }

        public void SetupUserAccessLevelForce()
        {
            Identity.AccessLevel = true;
        }

        public void SetupHotKeys(HotKeys hotKeys)
        {
            if (hotKeys != null)
            {
                if (HotKeysItem != null)
                {
                    _ = Task.Run(async () =>
                    {
                        await HotKeysItem.DisposeAsync().ConfigureAwait(true);
                        HotKeysItem = hotKeys;
                    }).ConfigureAwait(false);
                }
            }
        }

        #endregion
    }
}
