using System;
using System.Collections.Generic;

namespace amicable
{
    public class AmicableEngineByPrimary: IAmicableEngine
    {
        #region IAmicableEngine
        public void Execute(uint minNumber, uint maxNumber, IOutputManager outputManager)
        {
            uint[] primaryNumbers = CreatePrimaryNumbersArray(maxNumber);
            int primaryIndex = Array.FindIndex(primaryNumbers, delegate(uint number) { return minNumber <= number; });
            primaryIndex++;

            #region Create Summa Array
            uint[] summaArray = new uint[maxNumber - minNumber + 1];
            for (uint number = minNumber; number <= maxNumber; number++)
            {
                if (number >= primaryNumbers[primaryIndex])
                    primaryIndex = Math.Min(primaryIndex+1, primaryNumbers.Length - 1);
                
                uint summa = CalculateSumma(primaryNumbers, primaryIndex, number);
                summaArray[number - minNumber] = summa;
            }
            #endregion

            bool isAmicableNumbers;
            bool isFriendlyNumbers;
            uint summaOfNumber1;
            uint summaOfNumber2;
            for (uint number1 = minNumber; number1 < maxNumber; number1++)
            {
                summaOfNumber1 = summaArray[number1 - minNumber];
                double relOfNumber1 = ((double)(summaOfNumber1 + number1) / number1);

                for (uint number2 = number1 + 1; number2 <= maxNumber; number2++)
                {
                    summaOfNumber2 = summaArray[number2 - minNumber];

                    isAmicableNumbers = (summaOfNumber1 == number2) && (number1 == summaOfNumber2);
                    isFriendlyNumbers = relOfNumber1 == ((double)(summaOfNumber2 + number2) / number2);

                    if (isAmicableNumbers)
                        outputManager.AddAmicablePair(number1, number2);
                    if (isFriendlyNumbers)
                        outputManager.AddFriendlyPair(number1, number2);
                }
            }
        }

        uint CalculateSumma(uint[] primaryNumbers, int primaryCount, uint number)
        {
            uint summa = (uint) (number == 1 ? 0: 1);
            List<uint> actualPrimaryDividers = new List<uint>();

            for (uint i = 0; i < primaryCount; i++)
            {
                uint primaryI = primaryNumbers[i];
                uint divider = primaryI;
                while (number % divider == 0 && divider != number)
                {
                    actualPrimaryDividers.Add(primaryI);
                    divider *= primaryI;
                }
            }
            uint[] actualPrimaryDividersArray = actualPrimaryDividers.ToArray();
            List<uint> dividers = new List<uint>();

            for (uint pointer = 1; pointer < ((1 << actualPrimaryDividersArray.Length) - 1); pointer++)
            {
                int index = 0;
                uint pointerSumma = 1;
                uint counter = pointer;
                while (counter != 0)
                {
                    if (counter % 2 != 0)
                    {
                        uint nextDivider = actualPrimaryDividersArray[index];
                        pointerSumma *= nextDivider;
                    }
                    counter >>= 1;
                    index++;
                }
                if (!dividers.Contains(pointerSumma))
                {
                    dividers.Add(pointerSumma);
                    summa += pointerSumma;
                }
            }
            return summa;
        }

        #endregion
        uint[] CreatePrimaryNumbersArray(uint maxNumber)
        {
            // http://en.wikipedia.org/wiki/Sieve_of_Atkin

            #region initialize the sieve
            uint sqr_lim = (uint) Math.Sqrt(maxNumber);
            bool[] is_prime = new bool[maxNumber];
            #endregion

            #region put in candidate primes: integers which have an odd number of representations by certain quadratic forms
            uint n;
            uint y2;
            uint x2 = 0;
            for (uint i = 1; i <= sqr_lim; i++)
            {
                x2 += 2 * i - 1;
                y2 = 0;
                for (uint j = 1; j <= sqr_lim; j++)
                {
                    y2 += 2 * j - 1;

                    n = 4 * x2 + y2;
                    if ((n <= maxNumber) && (n % 12 == 1 || n % 12 == 5))
                        is_prime[n] = !is_prime[n];

                    // n = 3 * x2 + y2; 
                    n -= x2; // Optimization
                    if ((n <= maxNumber) && (n % 12 == 7))
                        is_prime[n] = !is_prime[n];

                    // n = 3 * x2 - y2;
                    n -= 2 * y2; // Optimization
                    if ((i > j) && (n <= maxNumber) && (n % 12 == 11))
                        is_prime[n] = !is_prime[n];
                }
            }
            #endregion 

            #region eliminate composites by sieving in interval [5, sqrt(limit)].
            for (uint i = 5; i <= sqr_lim; i++)
            {
                if (is_prime[i])
                {
                    n = i * i;
                    for (uint j = n; j < maxNumber; j += n)
                    {
                        is_prime[j] = false;
                    }
                }
            }
            #endregion

            #region fill primary numbers' list
            List<uint> primaryList = new List<uint>();
            primaryList.Add(2);
            primaryList.Add(3);
            for (uint i = 0; i < is_prime.Length; i++)
            {
                if (is_prime[i])
                    primaryList.Add(i);
            }
            #endregion

            return primaryList.ToArray();
        }
    }
}