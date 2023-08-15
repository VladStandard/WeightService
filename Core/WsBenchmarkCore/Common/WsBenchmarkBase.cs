// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsBenchmarkCore.Common;

[Config(typeof(WsManualConfig))]
public abstract class WsBenchmarkBase
{
    #region Public and private fields, properties, constructor

    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;

    protected WsBenchmarkBase()
    {
        ContextManager.SetupJsonConsole(Directory.GetCurrentDirectory(), nameof(WsBenchmarkCore));
        Console.WriteLine(ContextManager.SqlCore.GetConnectionServer());
    }

    #endregion
}