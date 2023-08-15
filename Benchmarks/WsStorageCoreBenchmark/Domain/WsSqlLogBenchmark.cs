// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreBenchmark.Domain;

public class WsSqlLogBenchmark : WsBenchmark
{
    private WsSqlLogRepository LogRepository { get; } = new();
    private int MaxRecords { get; set; }
    private int CountRecords { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        MaxRecords = 0;
        CountRecords = 1_000;
    }

    [Benchmark]
    public void ScenarioEmpty()
    {
        // Implement your benchmark here
    }
    
    [Benchmark]
    public void CreateNewFor()
    {
        for (int i = 0; i < CountRecords; i++) _ = new WsSqlLogModel();
    }
    
    [Benchmark]
    public void CreateNewEnumerable()
    {
        IEnumerable<WsSqlLogModel> items = GetEnumerableLogs();
    }

    private IEnumerable<WsSqlLogModel> GetEnumerableLogs() { for (int i = 0; i < CountRecords; i++) yield return new(); }

    [Benchmark]
    public void CreateNewArray()
    {
        //IEnumerable<WsSqlLogModel> items = new List<WsSqlLogModel>(1000);
        WsSqlLogModel[] items = new WsSqlLogModel[1000];
        for (int i = 0; i < CountRecords; i++) items[i] = new();
        Console.WriteLine($"Get {items.Length} items.");
    }
    
    [Benchmark]
    public void CreateNewList()
    {
        List<WsSqlLogModel> items = new();
        for (int i = 0; i < CountRecords; i++) items.Add(new());
        Console.WriteLine($"Get {items.Count} items.");
    }

    [Benchmark]
    public void RunGetEnumerable()
    {
        IEnumerable<WsSqlLogModel> items = LogRepository.GetEnumerable(MaxRecords);
        Console.WriteLine($"Get {items.Count()} items.");
    }

    [Benchmark]
    [Obsolete(@"Use RunGetEnumerable")]
    public void RunGetList()
    {
        List<WsSqlLogModel> items = LogRepository.GetList(MaxRecords);
        Console.WriteLine($"Get {items.Count} items.");
    }
}