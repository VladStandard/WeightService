#if DEBUG
Console.WriteLine("--- Run Benchmarks under RELEASE config! ---");
return;
#endif

Console.WriteLine("=== Start Benchmarks ===");
Console.WriteLine(" 0. Exit");
Console.WriteLine($" 1. {nameof(WsMd5VsSha256SampleBenchmark)}");
Console.WriteLine($" 2. {nameof(WsTemplateBenchmark)}");
Console.WriteLine($" 3. {nameof(WsSqlViewLogDeviceAggrBenchmark)}");
Console.WriteLine($" 4. {nameof(WsSqlLogBenchmark)}");
Console.Write(" User input: ");
string? userInput = Console.ReadLine();
Summary? summary = userInput switch
{
    "1" => RunWsMd5VsSha256SampleBenchmark(),
    "2" => RunWsTemplateBenchmark(),
    "3" => RunWsSqlViewLogDeviceAggr(),
    "4" => RunWsSqlLogBenchmark(),
    _ => null,
};
Console.WriteLine("=== End Benchmarks ===");

// Md5VsSha256SampleBenchmark.
Summary RunWsMd5VsSha256SampleBenchmark()
{
    Console.WriteLine($"--> Run {nameof(WsMd5VsSha256SampleBenchmark)} ---");
    return BenchmarkRunner.Run<WsMd5VsSha256SampleBenchmark>();
}

// Md5VsSha256SampleBenchmark.
Summary RunWsTemplateBenchmark()
{
    Console.WriteLine($"--> Run {nameof(WsTemplateBenchmark)} ---");
    return BenchmarkRunner.Run<WsTemplateBenchmark>();
}

// ViewLogDeviceAggrBenchmark.
Summary RunWsSqlViewLogDeviceAggr()
{
    Console.WriteLine($"--> Run {nameof(WsSqlViewLogDeviceAggrBenchmark)} ---");
    Summary summaryLocal = BenchmarkRunner.Run<WsSqlViewLogDeviceAggrBenchmark>();
    return summaryLocal;
}

// WsSqlLogBenchmark.
Summary RunWsSqlLogBenchmark()
{
    Console.WriteLine($"--> Run {nameof(WsSqlLogBenchmark)} ---");
    Summary summaryLocal = BenchmarkRunner.Run<WsSqlLogBenchmark>();
    return summaryLocal;
}
