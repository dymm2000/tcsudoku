using System;

namespace amicable
{
    class Program
    {
        static void Main(string[] args)
        {
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

            //Console.WriteLine("Amicable range: {0} .. {1}", input.minRange, input.maxRange);

            IAmicableEngine amicableEngine = new SeqAmicableEngine();

            for (uint number1 = input.minRange; number1 <= input.maxRange; number1++)
            {
                for (uint number2 = number1 + 1; number2 <= input.maxRange; number2++)
                {
                    amicableEngine.Execute(number1, number2);
                    if (amicableEngine.IsAmicableNumbers)
                        Console.WriteLine("{0} and {1} are AMICABLE", number1, number2);
                    if (amicableEngine.IsFriendlyNumbers)
                        Console.WriteLine("{0} and {1} are FRIENDLY", number1, number2);
                }
            }            
        }
    }
}
