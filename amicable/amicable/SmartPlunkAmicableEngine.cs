namespace amicable
{
    public class SmartPlunkAmicableEngine: IAmicableEngine
    {        
        #region IAmicableEngine
        public void Execute(uint minNumber, uint maxNumber, IOutputManager outputManager)
        {
            #region Create Summa Array
            uint[] summaArray = new uint[maxNumber - minNumber + 1];
            for (uint number = minNumber; number <= maxNumber; number++)
            {
                uint summa = 0;
                for (uint i = 1; i < number; i++)
                {
                    if (number % i == 0)
                        summa += i;
                }
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
                double relOfNumber1 = ((double) (summaOfNumber1 + number1)/number1);

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
        #endregion
    }
}