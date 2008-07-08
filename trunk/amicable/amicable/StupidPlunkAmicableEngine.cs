namespace amicable
{
    public class StupidPlunkAmicableEngine : IAmicableEngine
    {
        #region IAmicableEngine
        public void Execute(uint minNumber, uint maxNumber, IOutputManager outputManager)
        {
            for (uint number1 = minNumber; number1 <= maxNumber; number1++)
            {
                for (uint number2 = number1 + 1; number2 <= maxNumber; number2++)
                {
                    #region calculate summa of number1
                    uint summa = 0;
                    for (uint i = 1; i < number1; i++)
                    {
                        if (number1 % i == 0)
                            summa += i;
                    }
                    #endregion
                    uint summaOfNumber1 = summa;

                    #region calculate summa of number2
                    summa = 0;
                    for (uint i = 1; i < number2; i++)
                    {
                        if (number2 % i == 0)
                            summa += i;
                    }
                    #endregion
                    uint summaOfNumber2 = summa;

                    bool isAmicableNumbers = (summaOfNumber1 == number2) && (number1 == summaOfNumber2);
                    bool isFriendlyNumbers = ((double)(summaOfNumber1 + number1) / number1) == ((double)(summaOfNumber2 + number2) / number2);

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