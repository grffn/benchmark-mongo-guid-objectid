using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace benchmark_mongo_guid_objectid
{
    public class Program
    {
        static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

    }
}
