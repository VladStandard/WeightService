// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://benchmarkdotnet.org/index.html
// https://benchmarkdotnet.org/articles/configs/configoptions.html
// https://benchmarkdotnet.org/articles/configs/columns.html

namespace WsStorageCoreBenchmark.Template;

[SimpleJob(RuntimeMoniker.Net70, baseline: true)]
public class WsMd5VsSha256SampleBenchmark
{
    private byte[] _data = Array.Empty<byte>();
    private readonly SHA256 _sha256 = SHA256.Create();
    private readonly MD5 _md5 = MD5.Create();

    [Params(10, 100)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _data = new byte[Count];
        new Random(42).NextBytes(_data);
    }

    [Benchmark]
    public byte[] Sha256() => _sha256.ComputeHash(_data);

    [Benchmark]
    public byte[] Md5() => _md5.ComputeHash(_data);

    [Benchmark]
    public void Foo1() => Thread.Sleep(10);

    [Benchmark]
    public void Foo12() => Thread.Sleep(10);

    [Benchmark]
    public void Bar3() => Thread.Sleep(10);

    [Benchmark]
    public void Bar34() => Thread.Sleep(10);
}