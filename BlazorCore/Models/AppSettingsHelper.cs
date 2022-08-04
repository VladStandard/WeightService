// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql;
using DataCore.Localizations;
using DataCore.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading;

namespace BlazorCore.Models;

public class AppSettingsHelper : LayoutComponentBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static AppSettingsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static AppSettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
    public DataAccessHelper DataAccessHelp { get; } = DataAccessHelper.Instance;
    public DataSourceDicsEntity DataSourceDics { get; } = new();
    public MemoryEntity Memory { get; set; } = new();
    public int FontSizeHeader { get; set; }
    public int FontSize { get; set; }
    public static int Delay => 5_000;
    public string MemoryInfoWithDt => Memory.MemorySize.PhysicalCurrent != null
        ? $"{LocaleCore.Memory.Memory}: {Memory.MemorySize.PhysicalCurrent.MegaBytes:N0} MB  |  {StringUtils.FormatCurDtRus(true)}"
        : $"{LocaleCore.Memory.Memory}: - MB";
    public string MemoryInfo => Memory.MemorySize.PhysicalCurrent != null
        ? $"{LocaleCore.Memory.Memory}: {Memory.MemorySize.PhysicalCurrent.MegaBytes:N0} MB"
        : $"{LocaleCore.Memory.Memory}: - MB";
    public string MemoryInfoShort => Memory.MemorySize.PhysicalCurrent != null && Memory.MemorySize.PhysicalTotal != null
        ? $"{Memory.MemorySize.PhysicalCurrent.MegaBytes:N0} " +
          $"{LocaleCore.Strings.From} {Memory.MemorySize.PhysicalTotal.MegaBytes:N0} MB"
        : $"{LocaleCore.Memory.Memory}: - MB";
    public uint MemoryFillSize => Memory.MemorySize.PhysicalCurrent == null || Memory.MemorySize.PhysicalTotal == null
        || Memory.MemorySize.PhysicalTotal.MegaBytes == 0
        ? 0 : (uint)(Memory.MemorySize.PhysicalCurrent.MegaBytes * 100 / Memory.MemorySize.PhysicalTotal.MegaBytes);
    public bool IsSqlServerRelease => DataAccess.JsonSettingsLocal.Sql is { DataSource: { } } &&
        DataAccess.JsonSettingsLocal.Sql.DataSource.Contains(LocaleData.DeviceControl.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase);
    public bool IsSqlServerDebug => DataAccess.JsonSettingsLocal.Sql is { DataSource: { } } &&
        DataAccess.JsonSettingsLocal.Sql.DataSource.Contains(LocaleData.DeviceControl.SqlServerDebug, StringComparison.InvariantCultureIgnoreCase);

    #endregion

    #region Constructor and destructor

    public AppSettingsHelper()
    {
        //
    }

    #endregion

    #region Public and private methods

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

    #endregion
}
