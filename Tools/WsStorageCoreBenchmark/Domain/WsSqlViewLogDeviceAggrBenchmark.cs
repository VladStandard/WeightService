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