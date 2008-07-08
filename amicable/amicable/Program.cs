using System;
using System.Diagnostics;

namespace amicable
{
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

            IOutputManager outputManager = new ConsoleOutputManager();

            IAmicableEngine smartPlunkAmicableEngine = new SmartPlunkAmicableEngine();
            smartPlunkAmicableEngine.Execute(input.minRange, input.maxRange, outputManager);

//            IAmicableEngine stupidPlunkAmicableEngine = new StupidPlunkAmicableEngine();
//            stupidPlunkAmicableEngine.Execute(input.minRange, input.maxRange, outputManager);

//            IAmicableEngine amicableEngineByPrimary = new AmicableEngineByPrimary();
//            amicableEngineByPrimary.Execute(input.minRange, input.maxRange, outputManager);

            TimeSpan fullTime = DateTime.Now - startTime;
            Trace.WriteLine("fullTime: " + fullTime);
        }
    }
}
