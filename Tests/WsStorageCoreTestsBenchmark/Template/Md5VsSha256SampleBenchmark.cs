// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://benchmarkdotnet.org/articles/configs/configoptions.html

namespace WsStorageCoreTestsBenchmark.Template;

public class Md5VsSha256SampleBenchmark
{
    private const int N = 10000;
    private readonly byte[] data;
    private readonly SHA256 sha256 = SHA256.Create();
    private readonly MD5 md5 = MD5.Create();

    public Md5VsSha256SampleBenchmark()
    {
        data = new byte[N];
        new Random(42).NextBytes(data);
    }

    [Benchmark]
    public byte[] Sha256() => sha256.ComputeHash(data);

    [Benchmark]
    public byte[] Md5() => md5.ComputeHash(data);
}