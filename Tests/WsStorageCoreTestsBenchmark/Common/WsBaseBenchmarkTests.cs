// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTestsBenchmark.Common;

public class WsBaseBenchmarkTests
{
    #region Public and private fields, properties, constructor

    protected List<WsEnumConfiguration> AllConfigurations { get; }
    protected IConfig BehcnmarkConfig { get; }

    protected WsBaseBenchmarkTests()
    {
        AllConfigurations = new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS };
        BehcnmarkConfig = ManualConfig.Create(WsBenchmarkConfig.Instance);
    }

    #endregion
}