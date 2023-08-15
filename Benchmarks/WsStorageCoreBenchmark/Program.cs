// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if DEBUG
Console.WriteLine("--- Run Benchmarks under RELEASE config ---");
return;
#endif

Console.WriteLine("--- Benchmarks start ---");
Console.WriteLine("0. Exit");
Console.WriteLine($"1. {nameof(WsMd5VsSha256SampleBenchmark)}");
Console.WriteLine($"2. {nameof(WsTemplateBenchmark)}");
Console.WriteLine($"3. {nameof(WsSqlViewLogDeviceAggrBenchmark)}");
Console.WriteLine($"4. {nameof(WsSqlLogBenchmark)}");
string? userInput = Console.ReadLine();
Summary? summary = userInput switch
{
    "1" => RunWsMd5VsSha256SampleBenchmark(),
    "2" => RunWsTemplateBenchmark(),
    "3" => RunWsSqlViewLogDeviceAggr(),
    "4" => RunWsSqlLogBenchmark(),
    _ => null,
};
Console.WriteLine("--- Benchmarks end ---");

// Md5VsSha256SampleBenchmark.
Summary RunWsMd5VsSha256SampleBenchmark()
{
    Console.WriteLine($"--- Run {nameof(WsMd5VsSha256SampleBenchmark)} ---");
    return BenchmarkRunner.Run<WsMd5VsSha256SampleBenchmark>();
}

// Md5VsSha256SampleBenchmark.
Summary RunWsTemplateBenchmark()
{
    Console.WriteLine($"--- Run {nameof(WsTemplateBenchmark)} ---");
    return BenchmarkRunner.Run<WsTemplateBenchmark>();
}

// ViewLogDeviceAggrBenchmark.
Summary RunWsSqlViewLogDeviceAggr()
{
    Console.WriteLine($"--- Run {nameof(WsSqlViewLogDeviceAggrBenchmark)} ---");
    WsSqlViewLogDeviceAggrBenchmark viewLogDeviceAggrBenchmark = new();
    Console.WriteLine($"ConStr {viewLogDeviceAggrBenchmark.ContextManager.SqlCore.SqlConfiguration}.");
    Console.WriteLine($"Prepare {viewLogDeviceAggrBenchmark.ListItems.Count} items.");
    Summary summaryLocal = BenchmarkRunner.Run<WsSqlViewLogDeviceAggrBenchmark>(WsBenchmark.GetConfig());
    return summaryLocal;
}

// WsSqlLogBenchmark.
Summary RunWsSqlLogBenchmark()
{
    Console.WriteLine($"--- Run {nameof(WsSqlLogBenchmark)} ---");
    WsSqlLogBenchmark logBenchmark = new();
    Console.WriteLine($"ConStr {logBenchmark.ContextManager.SqlCore.SqlConfiguration}.");
    Summary summaryLocal = BenchmarkRunner.Run<WsSqlLogBenchmark>(WsBenchmark.GetConfig());
    return summaryLocal;
}
