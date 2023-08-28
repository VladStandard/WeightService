namespace WsBenchmarkCore.Common;

public class WsManualConfig : ManualConfig
{
    public WsManualConfig() : this(WsEnumConfiguration.Custom) { } 

    public WsManualConfig(WsEnumConfiguration config)
    {
        switch (config)
        {
            case WsEnumConfiguration.Default:
                CreateDefaultConfig();
                break;
            case WsEnumConfiguration.Simple:
                CreateSimpleConfig();
                break;
            case WsEnumConfiguration.Custom:
                CreateCustomConfig();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(config), config, null);
        }
    }

    private void CreateDefaultConfig() => CreateEmpty()
        .WithOptions(ConfigOptions.JoinSummary | ConfigOptions.DisableLogFile)
        // Benchmark was built without optimization enabled (most probably a DEBUG configuration). Please, build it in RELEASE.
        //WithOptions(ConfigOptions.DisableOptimizationsValidator)
        ;

    private void CreateSimpleConfig() => CreateEmpty()
        .WithOptions(ConfigOptions.JoinSummary | ConfigOptions.DisableLogFile)
        .AddLogger(new ConsoleLogger());

    private void CreateCustomConfig() => CreateEmpty()
        .WithOptions(ConfigOptions.JoinSummary | ConfigOptions.DisableLogFile)
        // Jobs
        .AddJob(Job.ShortRun
            //.WithRuntime(CoreRuntime.Core60)
            .WithRuntime(CoreRuntime.Core70)
            //.WithPlatform(Platform.X86)
            .WithPlatform(Platform.X64)
            //.WithPlatform(Platform.AnyCpu)
        )
        // Configuration of diagnosers and outputs
        .AddDiagnoser(MemoryDiagnoser.Default)
        .AddColumnProvider(DefaultColumnProviders.Instance)
        .AddLogger(ConsoleLogger.Default)
        .AddExporter(CsvExporter.Default)
        .AddExporter(HtmlExporter.Default)
        .AddAnalyser(GetCustomAnalysers().ToArray());

    private IEnumerable<IAnalyser> GetCustomAnalysers()
    {
        yield return EnvironmentAnalyser.Default;
        yield return OutliersAnalyser.Default;
        yield return MinIterationTimeAnalyser.Default;
        yield return MultimodalDistributionAnalyzer.Default;
        yield return RuntimeErrorAnalyser.Default;
        yield return ZeroMeasurementAnalyser.Default;
        yield return BaselineCustomAnalyzer.Default;
    }
}
