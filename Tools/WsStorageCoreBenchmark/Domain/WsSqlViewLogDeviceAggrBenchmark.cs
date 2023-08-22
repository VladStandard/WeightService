// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreBenchmark.Domain;

[SimpleJob(RuntimeMoniker.Net70, baseline: true)]
public class WsSqlViewLogDeviceAggrBenchmark : WsBenchmarkBase
{
    private IViewLogDeviceAggrRepository ViewLogDeviceAggrRepository { get; } = new WsSqlViewLogDeviceAggrRepository();

    [GlobalSetup]
    public void GlobalSetup()
    {
        CountRecords = 1_000;
    }

    [Benchmark]
    [InvocationCount(5)]
    [IterationCount(1)]
    public void GetList()
    {
        IList<WsSqlViewLogDeviceAggrModel> items = ViewLogDeviceAggrRepository.GetList(CountRecords);
        if (!items.Any())
            Console.WriteLine("GetEnumerable: no items!");
    }
}