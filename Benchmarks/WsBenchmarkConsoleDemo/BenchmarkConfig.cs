// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsBenchmarkConsoleDemo;

public class BenchmarkConfig
{
    /// <summary>
    /// Get a custom configuration
    /// </summary>
    /// <returns></returns>
    public static IConfig Get()
    {
        return ManualConfig.CreateEmpty()
            // Jobs
            .AddJob(Job.Default
                .WithRuntime(CoreRuntime.Core60)
                .WithPlatform(Platform.X64))
            // Configuration of diagnosers and outputs
            .AddDiagnoser(MemoryDiagnoser.Default)
            .AddColumnProvider(DefaultColumnProviders.Instance)
            .AddLogger(ConsoleLogger.Default)
            .AddExporter(CsvExporter.Default)
            .AddExporter(HtmlExporter.Default)
            .AddAnalyser(GetAnalysers().ToArray());
    }

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
}