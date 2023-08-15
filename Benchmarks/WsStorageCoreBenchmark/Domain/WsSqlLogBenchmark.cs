// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreBenchmark.Domain;

public class WsSqlLogBenchmark : WsBenchmarkBase
{
    private WsSqlLogRepository LogRepository { get; } = new();
    private int CountRecords { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        CountRecords = 1_000;
    }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    public void ScenarioEmpty()
    {
        // Implement your benchmark here
    }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    public void GetItemFirst()
    {
        _ = LogRepository.GetItemFirst();
    }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    public void CreateNewFor()
    {
        for (int i = 0; i < CountRecords; i++)
            _ = new WsSqlLogModel();
    }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    public void CreateNewEnumerable()
    {
        _ = GetEnumerableLogs();
    }

    private IEnumerable<WsSqlLogModel> GetEnumerableLogs() { for (int i = 0; i < CountRecords; i++) yield return new(); }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    public void CreateNewArray()
    {
        WsSqlLogModel[] items = new WsSqlLogModel[1000];
        for (int i = 0; i < CountRecords; i++)
            items[i] = new();
    }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    public void CreateNewList()
    {
        List<WsSqlLogModel> items = new();
        for (int i = 0; i < CountRecords; i++)
            items.Add(new());
    }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    public void GetEnumerable()
    {
        IEnumerable<WsSqlLogModel> items = LogRepository.GetEnumerable(CountRecords);
        if (items.Count() == 0)
            Console.WriteLine("GetEnumerable: no items!");
    }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    [Obsolete(@"Use GetEnumerable")]
    public void GetList()
    {
        List<WsSqlLogModel> items = LogRepository.GetList(CountRecords);
        if (!items.Any())
            Console.WriteLine("GetEnumerable: no items!");
    }
}