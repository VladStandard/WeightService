// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWeight.Plugins.Helpers;

public class PluginManagerHelper : HelperBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static PluginManagerHelper _instance;
#pragma warning restore CS8618
    public static PluginManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    public PluginLabels Labels { get; }
    public PluginMassa Massa { get; }
    public PluginMemoryHelper Memory { get; }
    public PluginPrint PrintMain { get; }
    public PluginPrint PrintShipping { get; }

    #endregion

    #region Constructor and destructor

    public PluginManagerHelper()
    {
        Labels = new();
        Massa = new();
        Memory = new();
        PrintMain = new();
        PrintShipping = new();
    }

    #endregion

    #region Public and private methods

    public override void Close()
    {
        base.Close();
        Labels.Close();
        Massa.Close();
        Memory.Close();
        PrintMain.Close();
        PrintShipping.Close();
    }

    #endregion
}