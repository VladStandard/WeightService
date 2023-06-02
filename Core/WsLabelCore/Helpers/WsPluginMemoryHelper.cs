// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Helpers;

/// <summary>
/// Плагин состояния памяти.
/// </summary>
#nullable enable
public sealed class WsPluginMemoryHelper : WsPluginHelperBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsPluginMemoryHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsPluginMemoryHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private Label FieldMemory { get; set; }
    public MemorySizeModel MemorySize { get; }

    #endregion

    #region Constructor and destructor

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public WsPluginMemoryHelper()
    {
        PluginType = WsEnumPluginType.Memory;
        ResponseItem.PluginType = RequestItem.PluginType = ReopenItem.PluginType = PluginType;

        FieldMemory = new();
        MemorySize = new();
    }

    #endregion

    #region Public and private methods

    public void Init(WsPluginConfigModel configReopen, WsPluginConfigModel configRequest, WsPluginConfigModel configResponse, Label fieldMemory)
    {
        Init();
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;

        FieldMemory = fieldMemory;
        MdInvokeControl.SetText(FieldMemory, LocaleCore.Scales.Memory);
        //MdInvokeControl.SetText(FieldMemoryExt, $"{LocaleCore.Scales.Threads}: {Process.GetCurrentProcess().Threads.Count}");
    }

    public override void Execute()
    {
        base.Execute();
        RequestItem.Execute(MemorySize.Execute);
        ResponseItem.Execute(Response);
    }

    //private string GetMemoryState() =>
    //    $"{LocaleCore.Scales.Memory} | {LocaleCore.Scales.MemoryBusy}: " +
    //    (MemorySize.PhysicalCurrent is not null ? $"{MemorySize.PhysicalCurrent.MegaBytes:N0} MB" : "- MB") +
    //    $" | {LocaleCore.Scales.MemoryFree}: " +
    //    (MemorySize.PhysicalFree is not null ? $"{MemorySize.PhysicalFree.MegaBytes:N0} MB" : "- MB") +
    //    $" | {LocaleCore.Scales.MemoryAll}: " +
    //    (MemorySize.PhysicalTotal is not null ? $"{MemorySize.PhysicalTotal.MegaBytes:N0} MB" : "- MB");

    /// <summary>
    /// Строка состояния памяти.
    /// </summary>
    /// <returns></returns>
    private string GetMemoryStateShort() => $"{LocaleCore.Scales.Memory} | " + 
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

    public override void Close()
    {
        base.Close();
        MemorySize.Close();
    }

    #endregion
}