using WsStorageCore.Entities.SchemaDiag.Logs;
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
            _ = new WsSqlLogEntity();
    }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    public void CreateNewEnumerable()
    {
        _ = GetEnumerableLogs();
    }

    private IEnumerable<WsSqlLogEntity> GetEnumerableLogs() { for (int i = 0; i < CountRecords; i++) yield return new(); }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    public void CreateNewArray()
    {
        WsSqlLogEntity[] items = new WsSqlLogEntity[1000];
        for (int i = 0; i < CountRecords; i++)
            items[i] = new();
    }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    public void CreateNewList()
    {
        IList<WsSqlLogEntity> items = new List<WsSqlLogEntity>();
        for (int i = 0; i < CountRecords; i++)
            items.Add(new());
    }
}