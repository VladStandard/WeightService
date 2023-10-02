namespace WsStorageCoreBenchmark.Domain;

public class WsSqlLogBenchmark : WsBenchmarkBase
{
    private WsSqlLogRepository LogRepository { get; } = new();

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
        IList<WsSqlLogModel> items = new List<WsSqlLogModel>();
        for (int i = 0; i < CountRecords; i++)
            items.Add(new());
    }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    public void GetEnumerableAsync()
    {
        _ = Task.Run(async () =>
        {
            IEnumerable<WsSqlLogModel> items = await LogRepository.GetEnumerableAsync(CountRecords);
            if (!items.Any())
                Console.WriteLine("GetEnumerableAsync: no items!");
        }).ConfigureAwait(true);
    }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    public void GetLiastAsync()
    {
        _ = Task.Run(async () =>
        {
            IList<WsSqlLogModel> items = await LogRepository.GetListAsync(CountRecords);
            if (!items.Any())
                Console.WriteLine("GetEnumerable: no items!");
        }).ConfigureAwait(true);
    }
}