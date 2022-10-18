using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Infrastructure.Middleware;

namespace Benchmarker
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<FileContext>();
        }
    }
}
