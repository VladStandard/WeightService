// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreBenchmark.Domain;

[SimpleJob(RuntimeMoniker.Net70, baseline: true)]
public class WsSqlViewLogDeviceAggrBenchmark : WsBenchmarkBase
{
    private IViewLogDeviceAggrRepository ViewLogDeviceAggrRepository { get; } = new WsSqlViewLogDeviceAggrRepository();
    private int MaxRecords { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        MaxRecords = 0;
    }

    [Benchmark]
    public void RunGetList()
    {
        Console.WriteLine("--- Start ------------------------------------------------------------");
        List<WsSqlViewLogDeviceAggrModel> items = ViewLogDeviceAggrRepository.GetList(MaxRecords);
        Console.WriteLine($"Get {items.Count} items.");
        Console.WriteLine("--- End ------------------------------------------------------------");
    }
}