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
    public class AppSettingsHelper : LayoutComponentBase
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static AppSettingsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static AppSettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public DataAccessEntity? DataAccess { get; private set; } = null;
        public DataReferenceEntity DataReference { get; private init; } = new();
        public bool IsDebug => DataAccess?.JsonSettings.IsDebug == true;
        public MemoryEntity? Memory { get; set; } = null;
        public int FontSizeHeader { get; set; }
        public int FontSize { get; set; }
        public static int Delay => 5_000;
        public string MemoryInfo => Memory != null && Memory.MemorySize != null &&
            Memory.MemorySize.PhysicalCurrent != null
            ? $"{LocalizationCore.Strings.MemoryUsed}: {Memory.MemorySize.PhysicalCurrent.MegaBytes:N0} MB  |  {StringUtils.FormatCurDtRus(true)}"
            : $"{LocalizationCore.Strings.MemoryUsed}: - MB";
        public string MemoryInfo2 => Memory != null && Memory.MemorySize != null &&
            Memory.MemorySize.PhysicalCurrent != null && Memory.MemorySize.PhysicalTotal != null
            ? $"{Memory.MemorySize.PhysicalCurrent.MegaBytes:N0} " +
              $"{LocalizationCore.Strings.Main.From} {Memory.MemorySize.PhysicalTotal.MegaBytes:N0} MB"
            : $"{LocalizationCore.Strings.MemoryUsed}: - MB";
        public float MemoryFillSize => Memory?.MemorySize?.PhysicalCurrent == null || Memory?.MemorySize?.PhysicalTotal == null
            || Memory.MemorySize.PhysicalTotal.MegaBytes == 0 
            ? 0 : Memory.MemorySize.PhysicalCurrent.MegaBytes * 100 / Memory.MemorySize.PhysicalTotal.MegaBytes;
        public string SqlServerDescription => DataAccess?.JsonSettings is { Server: { } }
            ? DataAccess.JsonSettings.Server.Contains(LocalizationData.DeviceControl.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase)
                ? LocalizationCore.Strings.Main.ServerRelease : LocalizationCore.Strings.Main.ServerDevelop
            : LocalizationCore.Strings.Main.NotLoad;
        public bool IsSqlServerRelease => DataAccess?.JsonSettings is { Server: { } } && DataAccess.JsonSettings.Server.Contains(LocalizationData.DeviceControl.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase);
        public bool IsSqlServerDebug => DataAccess?.JsonSettings is { Server: { } } && DataAccess.JsonSettings.Server.Contains(LocalizationData.DeviceControl.SqlServerDebug, StringComparison.InvariantCultureIgnoreCase);

        #endregion

        #region Constructor and destructor

        public AppSettingsHelper()
        {
            //
        }

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
            Memory.MemorySize.Open();
        }

        public void SetupJsonSettings(JsonSettingsEntity jsonSettings)
        {
            DataAccess = new DataAccessEntity(jsonSettings);
        }

        #endregion
    }
}
