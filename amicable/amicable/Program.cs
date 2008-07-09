using System;
using System.Diagnostics;

namespace amicable
{
    enum Strategy
    {
        StupidPlunkAmicable,
        SmartPlunkAmicable,
        AmicableEngineByPrimary,
        TheBestStrategy
    }
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;

            Input input;
            try
            {
                input = InputServices.CreateInput(args);
                input.Validate();
            }
            catch (Exception exp)
            {
                Console.WriteLine("Input data error: " + exp.Message);
                Console.WriteLine();
                Console.WriteLine("Amicable.exe minRange maxRange");
                Console.WriteLine();
                Console.WriteLine("Search amicable and friendly numbers in defined range.");
                return;
            }

            Strategy strategy;
            try
            {
                strategy = (Strategy) Enum.Parse(typeof (Strategy), args[2]);
            }
            catch (Exception)
            {
                strategy = Strategy.TheBestStrategy;
            }

            IOutputManager outputManager = new ConsoleOutputManager();
            IAmicableEngine amicableEngine;
            switch (strategy)
            {
                case Strategy.StupidPlunkAmicable:
                    amicableEngine = new StupidPlunkAmicableEngine();
                    break;
                case Strategy.SmartPlunkAmicable:
                    amicableEngine = new SmartPlunkAmicableEngine();
                    break;
                case Strategy.AmicableEngineByPrimary:
                    amicableEngine = new AmicableEngineByPrimary();
                    break;
                default:
                    amicableEngine = new SmartPlunkAmicableEngine();
                    break;
            }
            amicableEngine.Execute(input.minRange, input.maxRange, outputManager);

            TimeSpan fullTime = DateTime.Now - startTime;
            Trace.WriteLine("fullTime: " + fullTime);
        }
    }
}
