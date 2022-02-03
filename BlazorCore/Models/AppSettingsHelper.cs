// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.Models;
using DataCore.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading;

namespace BlazorCore.Models
{
    public class AppSettingsHelper : LayoutComponentBase, IDisposable
    {
        #region Design pattern "Lazy Singleton"

        private static AppSettingsHelper _instance;
        public static AppSettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public DataAccessEntity DataAccess { get; private set; }
        public DataSourceEntity DataSource { get; private init; } = new();
        public JsonSettingsEntity JsonAppSettings { get; private set; }
        public MemoryEntity Memory { get; set; }
        public bool IsDebug => JsonAppSettings != null && JsonAppSettings.IsDebug;
        public int FontSizeHeader { get; set; }
        public int FontSize { get; set; }
        public static int Delay => 5_000;
        public string MemoryInfo => Memory != null && Memory.MemorySize != null &&
            Memory.MemorySize.PhysicalCurrent != null
            ? $"{LocalizationCore.Strings.MemoryUsed}: {Memory.MemorySize.PhysicalCurrent.MegaBytes:N0} MB  |  {StringUtils.FormatCurDtRus(true)}"
            : $"{LocalizationCore.Strings.MemoryUsed}: - MB";
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
            if (jsonAppSettings == null) return;
            DataAccess = new DataAccessEntity(JsonAppSettings = jsonAppSettings);
        }

        public void Dispose()
        {
            DataAccess?.Dispose();
        }

        #endregion
    }
}
