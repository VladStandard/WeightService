namespace WsBenchmarkCore.Common;

[Config(typeof(WsManualConfig))]
public abstract class WsBenchmarkBase
{
    #region Public and private fields, properties, constructor

    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    protected int CountRecords { get; set; }

    protected WsBenchmarkBase()
    {
        ContextManager.SetupJsonConsole(Directory.GetCurrentDirectory(), nameof(WsBenchmarkCore));
        Console.WriteLine(ContextManager.SqlCore.GetConnectionServer());
    }

    #endregion
}