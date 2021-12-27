// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorShareCore.Models;
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

namespace BlazorProjectsCore.Models
{
    public class AppSettingsEntity : LayoutComponentBase
    {
        #region Design pattern "Lazy Singleton"

        private static AppSettingsEntity _instance;
        public static AppSettingsEntity Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public void Setup(AuthenticationStateProvider stateProvider, JsonSettingsEntity jsonAppSettings, HotKeys hotKeys)
        {
            lock (_locker)
            {
                if (jsonAppSettings != null && JsonAppSettings == null && CoreSettings == null && DataAccess == null)
                {
                    JsonAppSettings = jsonAppSettings;
                    CoreSettings = new(JsonAppSettings.Server, JsonAppSettings.Db, 
                        JsonAppSettings.Trusted, JsonAppSettings.Username, JsonAppSettings.Password, JsonAppSettings.Schema);
                    DataAccess = new DataAccessEntity(CoreSettings);
                }
                if (hotKeys != null)
                {
                    _ = (HotKeysItem?.DisposeAsync().ConfigureAwait(true));
                    HotKeysItem = hotKeys;
                }

                if (stateProvider != null)
                    IdentityOpen(stateProvider);
            }
        }

        #endregion

        #region Public and private fields and properties

        private readonly object _locker = new();
        public CoreSettingsEntity CoreSettings { get; set; }
        public IdentityEntity IdentityItem { get; private set; }
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

        #region Public and private methods - Memory manager

        public void MemoryOpen(MemoryEntity.DelegateGuiRefreshAsync callRefreshAsync)
        {
            if (Memory != null)
            {
                Memory.Close();
            }
            Memory = new MemoryEntity(1_000, 5_000);
            Memory.Open(callRefreshAsync);
        }

        #endregion

        #region Public and private methods - Authentication & identity

        private void IdentityOpen(AuthenticationStateProvider stateProvider)
        {
            if (stateProvider != null)
            {
                AuthenticationState authenticationState = stateProvider.GetAuthenticationStateAsync().Result;
                IIdentity identity = authenticationState?.User?.Identity;
                string name;
                try
                {
                    name = identity?.IsAuthenticated == true && !string.IsNullOrEmpty(identity?.Name)
                        ? identity.Name : LocalizationCore.Strings.Main.IdentityError;
                }
                catch (Exception)
                {
                    name = LocalizationCore.Strings.Main.IdentityError;
                }
                if (IdentityItem == null)
                    IdentityItem = new IdentityEntity(name);
                else
                    IdentityItem.Name = name;
                SetUserAccessLevel();
            }
        }

        private void SetUserAccessLevel([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            IdentityItem.IsAccess = false;
            if (DataAccess != null)
            {
                object[] objects = DataAccess.Crud.GetEntitiesNativeObject(SqlQueries.DbServiceManaging.Tables.Access.GetAccessUser(IdentityItem.Name), 
                    filePath, lineNumber, memberName);
                if (objects.Length == 1)
                {
                    if (objects[0] is object[] { Length: 5 } item)
                    {
                        if (Guid.TryParse(Convert.ToString(item[0]), out _))
                        {
                            if (item[4] != null)
                            {
                                IdentityItem.AccessLevel = Convert.ToBoolean(item[4]);
                            }
                        }
                    }
                }
            }
            IdentityItem.IsAccess = true;
        }

        #endregion
    }
}