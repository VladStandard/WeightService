// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsBenchmarkConsoleDemo;

public class Program
{
    public static void Main(string[] args)
    {
        // If arguments are available use BenchmarkSwitcher to run benchmarks
        if (args.Length > 0)
        {
            IEnumerable<Summary> summaries = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                .Run(args, BenchmarkConfig.Get());
            return;
        }
        // Else, use BenchmarkRunner
        Summary summary = BenchmarkRunner.Run<Benchmarks>(BenchmarkConfig.Get());
    }
}