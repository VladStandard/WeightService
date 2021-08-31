// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL;
using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using Toolbelt.Blazor.HotKeys;

namespace BlazorCore.Models
{
    public class AppSettingsEntity : LayoutComponentBase
    {
        #region Design pattern "Lazy Singleton"

        private static AppSettingsEntity _instance;
        public static AppSettingsEntity Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public AppSettingsEntity() { }

        public void Setup(AuthenticationStateProvider stateProvider, JsonSettingsEntity jsonAppSettings, HotKeys hotKeys,
            [CallerMemberName] string memberName = "")
        {
            lock (Locker)
            {
                if (jsonAppSettings != null && JsonAppSettings == null && CoreSettingsItem == null && DataAccess == null)
                {
                    JsonAppSettings = jsonAppSettings;
                    CoreSettingsItem = new(JsonAppSettings.Server, JsonAppSettings.Db, JsonAppSettings.Trusted, JsonAppSettings.Username, JsonAppSettings.Password);
                    DataAccess = new DataAccessEntity(CoreSettingsItem);
                }
                if (hotKeys != null)
                {
                    HotKeysItem?.DisposeAsync().ConfigureAwait(true);
                    HotKeysItem = hotKeys;
                }
                
                if (stateProvider != null)
                    IdentityOpen(stateProvider);

                // Debug log.
                //if (IsDebug)
                //{
                //    Console.WriteLine("--------------------------------------------------------------------------------");
                //    Console.WriteLine($"---------- {nameof(AppSettingsEntity)}.{memberName} (for Debug mode) ---------- ");
                //    Console.WriteLine($"{nameof(Memory)}: {Memory}");
                //    Console.WriteLine($"{nameof(HotKeysItem)}: {HotKeysItem}");
                //    Console.WriteLine($"{nameof(JsonAppSettings)}: {JsonAppSettings}");
                //    Console.WriteLine($"{nameof(DataAccess.CoreSettings)}: {DataAccess.CoreSettings}");
                //    Console.WriteLine($"{nameof(DataAccess)}: {DataAccess}");
                //    Console.WriteLine($"{nameof(DataSource)}: {DataSource}");
                //    Console.WriteLine("--------------------------------------------------------------------------------");
                //}
            }
        }

        #endregion

        #region Public and private fields and properties

        public object Locker { get; set; } = new object();
        public CoreSettingsEntity CoreSettingsItem { get; set; }
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
            Memory.MemorySize.Physical != null 
            ? $"{LocalizationStrings.Share.MemoryUsed}: {Memory.MemorySize.Physical.MegaBytes:N0} MB  |  {UtilsDt.FormatCurDtRus(true)}"
            : $"{LocalizationStrings.Share.MemoryUsed}: - MB";
        public bool IsChartSmooth { get; set; }

        public string SqlServerDescription => JsonAppSettings is { Server: { } }
            ? JsonAppSettings.Server.Contains(LocalizationStrings.DeviceControl.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase)
                ? LocalizationStrings.Share.ServerRelease : LocalizationStrings.Share.ServerDevelop
            : LocalizationStrings.Share.NotLoad;
        public bool IsSqlServerRelease => JsonAppSettings is { Server: { } } && JsonAppSettings.Server.Contains(LocalizationStrings.DeviceControl.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase);
        public bool IsSqlServerDebug => JsonAppSettings is { Server: { } } && JsonAppSettings.Server.Contains(LocalizationStrings.DeviceControl.SqlServerDebug, StringComparison.InvariantCultureIgnoreCase);

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
                if (IdentityItem == null)
                    IdentityItem = new IdentityEntity(identity != null ? identity.Name : LocalizationStrings.Share.IdentityError);
                else
                    IdentityItem.Name = identity != null ? identity.Name : LocalizationStrings.Share.IdentityError;
                SetUserAccessLevel();
            }
        }

        private void SetUserAccessLevel([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            IdentityItem.IsAccess = false;
            if (DataAccess != null)
            {
                object[] objects = DataAccess.GetEntitiesNativeObject(SqlQueries.GetAccessUser(IdentityItem.Name), filePath, lineNumber, memberName);
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