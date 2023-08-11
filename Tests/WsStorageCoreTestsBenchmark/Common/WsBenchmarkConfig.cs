// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BenchmarkDotNet.Loggers;

namespace WsStorageCoreTestsBenchmark.Common;

public class WsBenchmarkConfig : ManualConfig
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsBenchmarkConfig _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsBenchmarkConfig Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public WsBenchmarkConfig()
    {
        // Using the WithOptions() factory method.
        WithOptions(ConfigOptions.JoinSummary);
        //WithOptions(ConfigOptions.JoinSummary | ConfigOptions.DisableLogFile);
        WithOptions(ConfigOptions.DisableOptimizationsValidator);

        ConsoleLogger logger = new();
        AddLogger(logger);
    }
}