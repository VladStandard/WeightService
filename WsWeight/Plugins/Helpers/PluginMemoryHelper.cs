// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWeight.Plugins.Helpers;

public class PluginMemoryHelper : PluginHelperBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static PluginMemoryHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static PluginMemoryHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private Label FieldMemory { get; set; }
    private Label FieldMemoryExt { get; set; }
    public MemorySizeModel MemorySize { get; }

    #endregion

    #region Constructor and destructor

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public PluginMemoryHelper()
    {
        TskType = TaskType.TaskMemory;
        FieldMemory = new();
        FieldMemoryExt = new();
        MemorySize = new();
    }

    #endregion

    #region Public and private methods

    public void Init(ConfigModel configReopen, ConfigModel configRequest, ConfigModel configResponse,
        Label fieldMemory, Label fieldMemoryExt)
    {
        base.Init();
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
        //ActionUtils.ActionTryCatch(() =>
        //{
            FieldMemory = fieldMemory;
            FieldMemoryExt = fieldMemoryExt;
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMemory, LocaleCore.Scales.Memory);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMemoryExt, $"{LocaleCore.Scales.Threads}: {Process.GetCurrentProcess().Threads.Count}");
        //});
    }

    public override void Execute()
    {
        base.Execute();
        RequestItem.Execute(MemorySize.Execute);
        ResponseItem.Execute(Response);
    }

    private string GetMemoryState() =>
        $"{LocaleCore.Scales.Memory} | {LocaleCore.Scales.MemoryBusy}: " +
        (MemorySize.PhysicalCurrent is not null ? $"{MemorySize.PhysicalCurrent.MegaBytes:N0} MB" : "- MB") +
        $" | {LocaleCore.Scales.MemoryFree}: " +
        (MemorySize.PhysicalFree is not null ? $"{MemorySize.PhysicalFree.MegaBytes:N0} MB" : "- MB") +
        $" | {LocaleCore.Scales.MemoryAll}: " +
        (MemorySize.PhysicalTotal is not null ? $"{MemorySize.PhysicalTotal.MegaBytes:N0} MB" : "- MB");

    public short GetMemorySizeAppMb() => MemorySize.GetMemorySizeAppMb();

    public short GetMemorySizeFreeMb() => MemorySize.GetMemorySizeFreeMb();

    private void Response()
    {
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMemory, GetMemoryState());
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMemoryExt, $"{LocaleCore.Scales.Threads}: {Process.GetCurrentProcess().Threads.Count}");
    }

    public override void Close()
    {
        base.Close();
        MemorySize.Close();
    }

    #endregion
}