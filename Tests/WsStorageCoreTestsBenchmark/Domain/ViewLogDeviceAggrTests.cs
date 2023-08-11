// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTestsBenchmark.Domain;

[TestFixture]
public sealed class ViewLogDeviceAggrTests : WsBaseBenchmarkTests
{
    [Test]
    public void BenchmarkTests()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            Summary summary = BenchmarkRunner.Run<ViewLogDeviceAggrBenchmark>(BehcnmarkConfig);
            TestContext.WriteLine(summary);
        }, false, AllConfigurations);
    }
}