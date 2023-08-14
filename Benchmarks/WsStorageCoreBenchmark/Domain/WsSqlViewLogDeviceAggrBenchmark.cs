// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreBenchmark.Domain;

[SimpleJob(RuntimeMoniker.Net70, baseline: true)]
public class WsSqlViewLogDeviceAggrBenchmark : WsBenchmark
{
    private IViewLogDeviceAggrRepository ViewLogDeviceAggrRepository { get; } = new WsSqlViewLogDeviceAggrRepository();
    private int MaxRecords { get; set; }
    public List<WsSqlViewLogDeviceAggrModel> ListItems { get; set; } = new();

    [GlobalSetup]
    public void GlobalSetup()
    {
        MaxRecords = 0;
    }

    [Benchmark]
    public void RunGetList()
    {
        ListItems = ViewLogDeviceAggrRepository.GetList(MaxRecords);
    }
}