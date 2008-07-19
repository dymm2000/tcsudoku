using System;
using System.Threading;

namespace amicable
{
    internal class ThreadedAmicableEngine : IAmicableEngine
    {
        readonly int coresCount;
        uint[] summaArray;
        uint globalShift;
        public ThreadedAmicableEngine()
        {
            coresCount = Environment.ProcessorCount;
        }
        #region IAmicableEngine
        public void Execute(uint minNumber, uint maxNumber, IOutputManager outputManager)
        {
            Thread[] threads = new Thread[coresCount];

            #region Create Summa Array
            summaArray = new uint[maxNumber - minNumber + 1];
            globalShift = minNumber;
            uint taskSize = (uint) ((maxNumber - minNumber + 1)/coresCount);
            uint taskMin = minNumber;
            uint taskMax = taskMin + taskSize;

            for (byte threadIndex = 0; threadIndex < coresCount; threadIndex++)
            {
                ThreadedData4SummaCalculator threadedData = new ThreadedData4SummaCalculator(taskMin, taskMax);
                threads[threadIndex] = new Thread(ThreadedSummaCalculator);
                threads[threadIndex].Start(threadedData);

                taskMin = taskMax + 1;
                taskMax = threadIndex == coresCount - 2 ? maxNumber : taskMin + taskSize;                
            }
            foreach (Thread executer in threads) executer.Join();
            #endregion

            taskMin = minNumber;
            taskMax = taskMin + taskSize;
            for (byte threadIndex = 0; threadIndex < coresCount; threadIndex++)
            {
                ThreadedData4Searching threadedData = new ThreadedData4Searching(taskMin, taskMax, maxNumber, outputManager);
                threads[threadIndex] = new Thread(ThreadedCheckCalculator);
                threads[threadIndex].Start(threadedData);

                taskMin = taskMax + 1;
                taskMax = threadIndex == coresCount - 2 ? maxNumber : taskMin + taskSize;
            }
            foreach (Thread executer in threads) executer.Join();
        }
        #endregion
        public void ThreadedSummaCalculator(object transferedThreadedData)
        {
            ThreadedData4SummaCalculator threadedData = (ThreadedData4SummaCalculator)transferedThreadedData;
            uint minNumber = threadedData.minNumber;
            uint maxNumber = threadedData.maxNumber;

            for (uint number = minNumber; number <= maxNumber; number++)
            {
                uint summa = 0;
                for (uint i = 1; i <= (number >> 1); i++)
                {
                    if (number % i == 0)
                        summa += i;
                }
                summaArray[number - globalShift] = summa;
            }
        }
        public void ThreadedCheckCalculator(object transferedThreadedData)
        {
            ThreadedData4Searching threadedData = (ThreadedData4Searching)transferedThreadedData;
            uint minNumber = threadedData.minNumber;
            uint maxNumber1 = threadedData.maxNumber1;
            uint maxNumber2 = threadedData.maxNumber2;
            IOutputManager outputManager = threadedData.outputManager;

            bool isAmicableNumbers;
            bool isFriendlyNumbers;
            uint summaOfNumber1;
            uint summaOfNumber2;
            for (uint number1 = minNumber; number1 < maxNumber1; number1++)
            {
                summaOfNumber1 = summaArray[number1 - globalShift];
                double relOfNumber1 = ((double)(summaOfNumber1 + number1) / number1);

                for (uint number2 = number1 + 1; number2 <= maxNumber2; number2++)
                {
                    summaOfNumber2 = summaArray[number2 - globalShift];

                    isAmicableNumbers = (summaOfNumber1 == number2) && (number1 == summaOfNumber2);
                    isFriendlyNumbers = relOfNumber1 == ((double)(summaOfNumber2 + number2) / number2);

                    if (isAmicableNumbers)
                        outputManager.AddAmicablePair(number1, number2);
                    if (isFriendlyNumbers)
                        outputManager.AddFriendlyPair(number1, number2);
                }
            }
        }
    }
    public class ThreadedData4SummaCalculator
    {
        public uint minNumber;
        public uint maxNumber;
        public ThreadedData4SummaCalculator(uint minNumber, uint maxNumber)
        {
            this.minNumber = minNumber;
            this.maxNumber = maxNumber;
        }
    }
    public class ThreadedData4Searching
    {
        public uint minNumber;
        public uint maxNumber1;
        public uint maxNumber2;
        public IOutputManager outputManager;
        public ThreadedData4Searching(uint minNumber, uint maxNumber1, uint maxNumber2, IOutputManager outputManager)
        {
            this.minNumber = minNumber;
            this.maxNumber1 = maxNumber1;
            this.maxNumber2 = maxNumber2;
            this.outputManager = outputManager;
        }
    }
}