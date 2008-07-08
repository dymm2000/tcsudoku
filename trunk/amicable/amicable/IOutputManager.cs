using System;

namespace amicable
{
    public interface IOutputManager
    {
        void AddAmicablePair<T>(T number1, T number2);
        void AddFriendlyPair<T>(T number1, T number2);
    }

    public class ConsoleOutputManager: IOutputManager
    {
        #region IOutputManager
        public void AddAmicablePair<T>(T number1, T number2)
        {
            Console.WriteLine("{0} and {1} are AMICABLE", number1, number2);
        }
        public void AddFriendlyPair<T>(T number1, T number2)
        {
            Console.WriteLine("{0} and {1} are FRIENDLY", number1, number2);
        }
        #endregion
    }
}