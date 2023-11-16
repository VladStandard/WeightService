namespace Ws.LabelCore.Helpers;

/// <summary>
/// Плагин состояния памяти.
/// </summary>
#nullable enable
public sealed class PluginMemoryHelper : PluginBaseHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static PluginMemoryHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static PluginMemoryHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private Label FieldMemory { get; set; }
    public MemorySizeModel MemorySize { get; }

    #endregion

    #region Constructor and destructor

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public PluginMemoryHelper()
    {
        PluginType = EnumPluginType.Memory;
        ResponseItem.PluginType = RequestItem.PluginType = ReopenItem.PluginType = PluginType;

        FieldMemory = new();
        MemorySize = new();
    }

    #endregion

    #region Public and private methods

    public void Init(PluginConfigModel configReopen, PluginConfigModel configRequest, PluginConfigModel configResponse, Label fieldMemory)
    {
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;

        FieldMemory = fieldMemory;
        MdInvokeControl.SetText(FieldMemory, LocaleCore.LabelPrint.Memory);
        //MdInvokeControl.SetText(FieldMemoryExt, $"{LocaleCore.Scales.Threads}: {Process.GetCurrentProcess().Threads.Count}");
    }

    public override void Execute()
    {
        base.Execute();
        RequestItem.Execute(MemorySize.Execute);
        ResponseItem.Execute(Response);
    }
    
    /// <summary>
    /// Строка состояния памяти.
    /// </summary>
    /// <returns></returns>
    private string GetMemoryStateShort() => $"{LocaleCore.LabelPrint.Memory} | " + 
        (MemorySize.PhysicalCurrent is not null ? $"{MemorySize.PhysicalCurrent.MegaBytes:N0}" : "-") + " | " +
        (MemorySize.PhysicalFree is not null ? $"{MemorySize.PhysicalFree.MegaBytes:N0} " : "-") + " | " +
        (MemorySize.PhysicalTotal is not null ? $"{MemorySize.PhysicalTotal.MegaBytes:N0} " : "-" +" MB");

    public short GetMemorySizeAppMb() => MemorySize.GetMemorySizeAppMb();

    public short GetMemorySizeFreeMb() => MemorySize.GetMemorySizeFreeMb();

    private void Response()
    {
        MdInvokeControl.SetText(FieldMemory, GetMemoryStateShort());
        //MdInvokeControl.SetText(FieldMemoryExt, $"{LocaleCore.Scales.Threads}: {Process.GetCurrentProcess().Threads.Count}");
    }

    public override void Dispose()
    {
        base.Dispose();
        MemorySize.Dispose();
    }

    #endregion
}