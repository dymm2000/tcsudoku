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
            for (uint number = minNumber; number < maxNumber; number++)
            {
                
            }
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