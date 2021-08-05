using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.Models;
using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using Toolbelt.Blazor.HotKeys;

namespace BlazorDeviceControl.Data
{
    public class AppSettingsEntity : LayoutComponentBase
    {
        #region Design pattern "Lazy Singleton"

        private static AppSettingsEntity _instance;
        public static AppSettingsEntity Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public AppSettingsEntity() { }

        public void Setup(AuthenticationStateProvider stateProvider, JsonAppSettingsEntity jsonAppSettings, HotKeys hotKeys)
        {
            if (jsonAppSettings != null)
            {
                JsonAppSettings = jsonAppSettings;
                CoreSettingsEntity appSettings = new(JsonAppSettings.Server, JsonAppSettings.Db, JsonAppSettings.Trusted, 
                    JsonAppSettings.Username, JsonAppSettings.Password);
                DataAccess = new DataAccessEntity(appSettings);
            }
            if (hotKeys != null)
                HotKeysItem = hotKeys;
            if (stateProvider != null)
                IdentityOpen(stateProvider);

            // Debug log.
            if (IsDebug)
            {
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine($"---------- {nameof(AppSettingsEntity)}.{nameof(Setup)} (for Debug mode) ---------- ");
                Console.WriteLine($"{nameof(Memory)}: {Memory}");
                Console.WriteLine($"{nameof(HotKeysItem)}: {HotKeysItem}");
                Console.WriteLine($"{nameof(JsonAppSettings)}: {JsonAppSettings}");
                Console.WriteLine($"{nameof(DataAccess.CoreSettings)}: {DataAccess.CoreSettings}");
                Console.WriteLine($"{nameof(DataAccess)}: {DataAccess}");
                Console.WriteLine($"{nameof(DataSource)}: {DataSource}");
                Console.WriteLine($"{nameof(stateProvider)}: {stateProvider}");
                Console.WriteLine("--------------------------------------------------------------------------------");
            }
        }

        #endregion

        #region Public and private fields and properties

        public bool? IdentityAccessLevel { get; private set; }

        public DataAccessEntity DataAccess { get; private set; }
        public DataSourceEntity DataSource { get; private init; } = new();
        public JsonAppSettingsEntity JsonAppSettings { get; private set; }
        public HotKeys HotKeysItem { get; private set; }
        public HotKeysContext HotKeysContextItem { get; set; }
        public MemoryEntity Memory { get; set; }
        
        public bool IsDebug => JsonAppSettings.IsDebug;
        public int FontSizeHeader { get; set; }
        public int FontSize { get; set; }
        public int Delay => 5_000;
        public string MemoryInfo => Memory != null
            ? $"{LocalizationStrings.DeviceControl.MemoryUsed}: {Memory.MemorySize.Physical.MegaBytes:N0} MB  |  {UtilsDt.FormatCurDtRus(true)}"
            : $"{LocalizationStrings.DeviceControl.MemoryUsed}: - MB";
        public string IdentityMessage { get; private set; }
        public bool IsChartSmooth { get; set; }

        public string SqlServerDescription => JsonAppSettings is {Server: { }}
            ? JsonAppSettings.Server.Contains(LocalizationStrings.DeviceControl.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase)
                ? LocalizationStrings.Share.ServerRelease : LocalizationStrings.Share.ServerDevelop
            : LocalizationStrings.Share.NotLoad;
        public bool IsSqlServerRelease => JsonAppSettings is {Server: { }} && JsonAppSettings.Server.Contains(LocalizationStrings.DeviceControl.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase);
        public bool IsSqlServerDebug => JsonAppSettings is {Server: { }} && JsonAppSettings.Server.Contains(LocalizationStrings.DeviceControl.SqlServerDebug, StringComparison.InvariantCultureIgnoreCase);

        #endregion

        #region Public and private methods - Memory manager

        public void MemoryOpen(MemoryEntity.DelegateGuiRefresh callRefresh)
        {
            if (Memory != null)
            {
                Memory.Close();
            }
            Memory = new MemoryEntity(1_000, 5_000);
            Memory.Open(callRefresh);
        }

        #endregion

        #region Public and private methods - Authentication & identity

        public void IdentityOpen(AuthenticationStateProvider stateProvider)
        {
            if (stateProvider != null)
            {
                AuthenticationState authenticationState = stateProvider.GetAuthenticationStateAsync().Result;
                IIdentity identity = authenticationState?.User?.Identity;
                IdentityMessage = identity != null ? identity.Name : LocalizationStrings.Share.IdentityError;
                SetUserAccessLevel(identity?.Name);
            }
        }

        private void SetUserAccessLevel(string userName,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            IdentityAccessLevel = null;
            if (DataAccess == null)
                return;
            object[] objects = DataAccess.GetEntitiesNativeObject(SqlQueries.GetAccessUser(userName), filePath, lineNumber, memberName);
            if (objects.Length == 1)
            {
                if (objects[0] is object[] { Length: 5 } item)
                {
                    if (Guid.TryParse(Convert.ToString(item[0]), out _))
                    {
                        if (item[4] != null)
                        {
                            IdentityAccessLevel = Convert.ToBoolean(item[4]);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
