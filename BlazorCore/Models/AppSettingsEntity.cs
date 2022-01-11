// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.Models;
using DataProjectsCore.Models;
using DataCore;
using DataShareCore;
using DataShareCore.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using Toolbelt.Blazor.HotKeys;
using DataProjectsCore.DAL;

namespace BlazorCore.Models
{
    public class AppSettingsEntity : LayoutComponentBase
    {
        #region Design pattern "Lazy Singleton"

        private static AppSettingsEntity _instance;
        public static AppSettingsEntity Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public StuntmanUsersEntity StuntmanUsers { get; set; } = new StuntmanUsersEntity();
        public CoreSettingsEntity CoreSettings { get; set; }
        public IdentityEntity Identity { get; private set; } = new IdentityEntity();
        public DataAccessEntity DataAccess { get; private set; }
        public DataSourceEntity DataSource { get; private init; } = new();
        public JsonSettingsEntity JsonAppSettings { get; private set; }
        public HotKeys HotKeysItem { get; private set; }
        public HotKeysContext HotKeysContextItem { get; set; }
        public MemoryEntity Memory { get; set; }

        public bool IsDebug => JsonAppSettings != null && JsonAppSettings.IsDebug;
        public int FontSizeHeader { get; set; }
        public int FontSize { get; set; }
        public static int Delay => 5_000;
        public string MemoryInfo => Memory != null && Memory.MemorySize != null &&
            Memory.MemorySize.PhysicalCurrent != null
            ? $"{LocalizationCore.Strings.MemoryUsed}: {Memory.MemorySize.PhysicalCurrent.MegaBytes:N0} MB  |  {StringUtils.FormatCurDtRus(true)}"
            : $"{LocalizationCore.Strings.MemoryUsed}: - MB";
        public bool IsChartSmooth { get; set; }

        public string SqlServerDescription => JsonAppSettings is { Server: { } }
            ? JsonAppSettings.Server.Contains(LocalizationData.DeviceControl.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase)
                ? LocalizationCore.Strings.Main.ServerRelease : LocalizationCore.Strings.Main.ServerDevelop
            : LocalizationCore.Strings.Main.NotLoad;
        public bool IsSqlServerRelease => JsonAppSettings is { Server: { } } && JsonAppSettings.Server.Contains(LocalizationData.DeviceControl.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase);
        public bool IsSqlServerDebug => JsonAppSettings is { Server: { } } && JsonAppSettings.Server.Contains(LocalizationData.DeviceControl.SqlServerDebug, StringComparison.InvariantCultureIgnoreCase);

        #endregion

        #region Public and private methods

        public void SetupMemory(MemoryEntity.DelegateGuiRefreshAsync callRefreshAsync)
        {
            if (Memory != null)
            {
                Memory.Close();
            }
            Memory = new MemoryEntity(1_000, 5_000);
            Memory.Open(callRefreshAsync);
        }

        public void SetupJsonSettings(JsonSettingsEntity jsonAppSettings)
        {
            if (jsonAppSettings != null && JsonAppSettings == null && CoreSettings == null && DataAccess == null)
            {
                JsonAppSettings = jsonAppSettings;
                CoreSettings = new(JsonAppSettings.Server, JsonAppSettings.Db,
                    JsonAppSettings.Trusted, JsonAppSettings.Username, JsonAppSettings.Password, JsonAppSettings.Schema);
                DataAccess = new DataAccessEntity(CoreSettings);
            }
        }

        public void SetupHotKeys(HotKeys hotKeys)
        {
            if (hotKeys != null)
            {
                _ = (HotKeysItem?.DisposeAsync().ConfigureAwait(true));
                HotKeysItem = hotKeys;
            }
        }

        public void SetupIdentity(AuthenticationStateProvider stateProvider)
        {
            Identity.Name = string.Empty;
            if (stateProvider == null)
                return;

            System.Threading.Tasks.Task<AuthenticationState> state = stateProvider.GetAuthenticationStateAsync();
            if (!state.IsCompleted)
                return;

            AuthenticationState authenticationState = state.Result;
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

        public void SetupUserAccessLevel([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (DataAccess != null)
            {
                object[] objects = DataAccess.Crud.GetEntitiesNativeObject(SqlQueries.DbServiceManaging.Tables.Access.GetAccessUser(Identity.Name),
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

        #endregion
    }
}
