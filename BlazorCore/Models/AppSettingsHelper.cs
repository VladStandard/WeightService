// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.Helpers;
using DataCore.Localization;
using DataCore.Models;
using DataCore.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;
using System.Threading;

namespace BlazorCore.Models
{
    public class AppSettingsHelper : LayoutComponentBase
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static AppSettingsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static AppSettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public bool IsFirstLoad { get; set; }
        public DataAccessEntity DataAccess { get; private set; }
        public DataSourceDicsEntity DataSourceDics { get; private init; } = new();
        public MemoryEntity Memory { get; set; } = new();
        public int FontSizeHeader { get; set; }
        public int FontSize { get; set; }
        public static int Delay => 5_000;
        public string MemoryInfoWithDt => Memory != null && Memory.MemorySize != null &&
            Memory.MemorySize.PhysicalCurrent != null
            ? $"{Core.Strings.Memory}: {Memory.MemorySize.PhysicalCurrent.MegaBytes:N0} MB  |  {StringUtils.FormatCurDtRus(true)}"
            : $"{Core.Strings.Memory}: - MB";
        public string MemoryInfo => Memory != null && Memory.MemorySize != null &&
            Memory.MemorySize.PhysicalCurrent != null
            ? $"{Core.Strings.Memory}: {Memory.MemorySize.PhysicalCurrent.MegaBytes:N0} MB"
            : $"{Core.Strings.Memory}: - MB";
        public string MemoryInfoShort => Memory != null && Memory.MemorySize != null &&
            Memory.MemorySize.PhysicalCurrent != null && Memory.MemorySize.PhysicalTotal != null
            ? $"{Memory.MemorySize.PhysicalCurrent.MegaBytes:N0} " +
              $"{Core.Strings.Main.From} {Memory.MemorySize.PhysicalTotal.MegaBytes:N0} MB"
            : $"{Core.Strings.Memory}: - MB";
        public float MemoryFillSize => Memory.MemorySize.PhysicalCurrent == null || Memory.MemorySize.PhysicalTotal == null
            || Memory.MemorySize.PhysicalTotal.MegaBytes == 0 
            ? 0f : (float)(Memory.MemorySize.PhysicalCurrent.MegaBytes) * 100 / Memory.MemorySize.PhysicalTotal.MegaBytes;
        public string SqlServerDescription => DataAccess.JsonSettings?.Sql is { Server: { } }
            ? DataAccess.JsonSettings.Sql.Server.Contains(LocalizationData.DeviceControl.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase)
                ? Core.Strings.Main.ServerRelease : Core.Strings.Main.ServerDevelop
            : Core.Strings.Main.NotLoad;
        public bool IsSqlServerRelease => DataAccess.JsonSettings?.Sql is { Server: { } } && 
            DataAccess.JsonSettings.Sql.Server.Contains(LocalizationData.DeviceControl.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase);
        public bool IsSqlServerDebug => DataAccess.JsonSettings?.Sql is { Server: { } } && 
            DataAccess.JsonSettings.Sql.Server.Contains(LocalizationData.DeviceControl.SqlServerDebug, StringComparison.InvariantCultureIgnoreCase);

        #endregion

        #region Constructor and destructor

        public AppSettingsHelper()
        {
            if (!IsFirstLoad)
            {
                IsFirstLoad = true;
            }
            string dir = Directory.GetCurrentDirectory();
            JsonSettingsBase? jsonSettings = DataAccessHelper.Instance.GetJsonSettings(dir);
            SetupJsonSettings(jsonSettings); 
            DataAccess = new DataAccessEntity();
        }

        #endregion

        #region Public and private methods

        //public void SetupMemory(MemoryEntity.DelegateGuiRefreshAsync callRefreshAsync)
        public void SetupMemory()
        {
            if (Memory != null)
            {
                Memory.Close();
            }
            Memory = new(1_000, 5_000);
            //Memory.OpenAsync(callRefreshAsync);
            Memory.MemorySize.Open();
        }

        public void SetupJsonSettings(JsonSettingsBase? jsonSettings)
        {
            DataAccess = new DataAccessEntity(jsonSettings);
        }

        #endregion
    }
}
