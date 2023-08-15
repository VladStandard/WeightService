// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsBenchmarkCore.Common;

public abstract class WsBenchmark
{
    #region Public and private fields, properties, constructor

    public WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    public List<WsEnumConfiguration> AllConfigurations { get; }

    protected WsBenchmark()
    {
        AllConfigurations = new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS };
        ContextManager.SetupJsonConsole(Directory.GetCurrentDirectory(), nameof(WsBenchmarkCore));
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get a custom configuration.
    /// </summary>
    /// <returns></returns>
    public virtual IConfig GetConfigSimple()
    {
        ConsoleLogger logger = new();
        return ManualConfig.CreateEmpty()
            // Using the WithOptions() factory method.
            //WithOptions(ConfigOptions.JoinSummary)
            .WithOptions(ConfigOptions.JoinSummary | ConfigOptions.DisableLogFile)
            // Benchmark was built without optimization enabled (most probably a DEBUG configuration). Please, build it in RELEASE.
            //WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddLogger(logger)
            ;
    }

    /// <summary>
    /// Get a custom configuration.
    /// </summary>
    /// <returns></returns>
    public static IConfig GetConfig() => ManualConfig.CreateEmpty()
        .WithOptions(ConfigOptions.JoinSummary | ConfigOptions.DisableLogFile)
        // Jobs
        .AddJob(Job.Default
            .WithRuntime(CoreRuntime.Core60)
            .WithRuntime(CoreRuntime.Core70)
            .WithPlatform(Platform.X86)
            .WithPlatform(Platform.X64)
            .WithPlatform(Platform.AnyCpu)
        )
        // Configuration of diagnosers and outputs
        .AddDiagnoser(MemoryDiagnoser.Default)
        .AddColumnProvider(DefaultColumnProviders.Instance)
        .AddLogger(ConsoleLogger.Default)
        .AddExporter(CsvExporter.Default)
        .AddExporter(HtmlExporter.Default)
        .AddAnalyser(GetAnalysers().ToArray());

    /// <summary>
    /// Get analyser for the cutom configuration
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<IAnalyser> GetAnalysers()
    {
        yield return EnvironmentAnalyser.Default;
        yield return OutliersAnalyser.Default;
        yield return MinIterationTimeAnalyser.Default;
        yield return MultimodalDistributionAnalyzer.Default;
        yield return RuntimeErrorAnalyser.Default;
        yield return ZeroMeasurementAnalyser.Default;
        yield return BaselineCustomAnalyzer.Default;
    }

    #endregion
}