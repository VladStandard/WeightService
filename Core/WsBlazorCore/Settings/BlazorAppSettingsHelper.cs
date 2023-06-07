// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using Microsoft.AspNetCore.Components;

using System.Threading;
using WsDataCore.Memory;
using WsDataCore.Models;
using WsStorageCore.Helpers;

namespace WsBlazorCore.Settings;

public class BlazorAppSettingsHelper //: LayoutComponentBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static BlazorAppSettingsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static BlazorAppSettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public WsSqlAccessManagerHelper AccessManager => WsSqlAccessManagerHelper.Instance;
    public WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    public DataSourceDicsHelper DataSourceDics => DataSourceDicsHelper.Instance;
    public MemoryModel Memory { get; private set; } = new();
    public static int Delay => 2500;
    public string MemoryInfo => Memory.MemorySize.PhysicalTotal != null
        ? $"{LocaleCore.Memory.Memory}: {Memory.MemorySize.PhysicalAllocated.MegaBytes:N0} MB " +
          $"{LocaleCore.Strings.From} {Memory.MemorySize.PhysicalTotal.MegaBytes:N0} MB"
        : $"{LocaleCore.Memory.Memory}: - MB";
    public uint MemoryFillSize => Memory.MemorySize.PhysicalTotal == null || Memory.MemorySize.PhysicalTotal.MegaBytes == 0
        ? 0 : (uint)(Memory.MemorySize.PhysicalAllocated.MegaBytes * 100 / Memory.MemorySize.PhysicalTotal.MegaBytes);

    #endregion

    #region Public and private methods

    public void SetupMemory()
    {
        Memory.Close();
        Memory = new();
        //Memory.OpenAsync(callRefreshAsync);
        Memory.MemorySize.Execute();
        ContextManager.ContextItem.SaveLogMemory(Memory.MemorySize.GetMemorySizeAppMb(), Memory.MemorySize.GetMemorySizeFreeMb());
    }

    #endregion
}