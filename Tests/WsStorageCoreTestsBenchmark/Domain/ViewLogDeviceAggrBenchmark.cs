// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://benchmarkdotnet.org/articles/configs/configoptions.html

namespace WsStorageCoreTestsBenchmark.Domain;

public class ViewLogDeviceAggrBenchmark
{
    private IViewLogDeviceAggrRepository ViewLogDeviceAggrRepository { get; } = new WsSqlViewLogDeviceAggrRepository();

    [Benchmark]
    public List<WsSqlViewLogDeviceAggrModel> GetEnumerable() => ViewLogDeviceAggrRepository.GetList(10);
}