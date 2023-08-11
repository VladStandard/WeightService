// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://benchmarkdotnet.org/articles/configs/configoptions.html

namespace WsStorageCoreTestsBenchmark.Template;

[TestFixture]
public sealed class Md5VsSha256SampleTests : WsBaseBenchmarkTests
{
    [Test]
    public void BenchmarkTests()
    {
        Assert.DoesNotThrow(() =>
        {
            Summary summary = BenchmarkRunner.Run<Md5VsSha256SampleBenchmark>(BehcnmarkConfig);
            TestContext.WriteLine(summary);
        });
    }
}