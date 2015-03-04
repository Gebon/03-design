using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using Ninject;


namespace battleships
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: {0} <ai.exe>", Process.GetCurrentProcess().ProcessName);
                return;
            }

            var kernel = new StandardKernel();
            var settings = new Settings("settings.txt");

            kernel.Bind<Settings>().ToConstant(settings);
            kernel.Bind<Random>().To<Random>().WithConstructorArgument(settings.RandomSeed);
            kernel.Bind<ProcessMonitor>()
                .To<ProcessMonitor>()
                .WithConstructorArgument(TimeSpan.FromSeconds(settings.TimeLimitSeconds*settings.GamesCount))
                .WithConstructorArgument(typeof(long), settings.MemoryLimit);
            
            var aiPath = args[0];
            var tester = kernel.Get<AiTester>();
            if (File.Exists(aiPath))
                tester.TestSingleFile(aiPath);
            else
                Console.WriteLine("No AI exe-file " + aiPath);

        }
    }
}