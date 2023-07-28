// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Memory;
using WsDataCore.Models;

namespace DeviceControl.Settings;

public class BlazorAppSettingsHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static BlazorAppSettingsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static BlazorAppSettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor
    
    public DataSourceDicsHelper DataSourceDics => DataSourceDicsHelper.Instance;
    public MemoryModel Memory { get; private set; } = new();
    public static int DelayInfo => 2500;
    public static int DelayError => 5000;
    
    #endregion

    #region Public and private methods

    public void SetupMemory()
    {
        Memory.Close();
        Memory = new();
        Memory.MemorySize.Execute();
        // ContextManager.ContextItem.SaveLogMemory(Memory.MemorySize.GetMemorySizeAppMb(), Memory.MemorySize.GetMemorySizeFreeMb());
    }

    #endregion
}