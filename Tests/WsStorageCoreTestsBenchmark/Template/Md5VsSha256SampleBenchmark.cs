// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://benchmarkdotnet.org/articles/configs/configoptions.html

namespace WsStorageCoreTestsBenchmark.Template;

public class Md5VsSha256SampleBenchmark
{
    private const int N = 10_000;
    private readonly byte[] _data;
    private readonly SHA256 _sha256 = SHA256.Create();
    private readonly MD5 _md5 = MD5.Create();

    public Md5VsSha256SampleBenchmark()
    {
        _data = new byte[N];
        new Random(42).NextBytes(_data);
    }

    [Benchmark]
    public byte[] Sha256() => _sha256.ComputeHash(_data);

    [Benchmark]
    public byte[] Md5() => _md5.ComputeHash(_data);
}