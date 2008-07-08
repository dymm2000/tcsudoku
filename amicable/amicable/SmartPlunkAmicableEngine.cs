namespace amicable
{
    public class SmartPlunkAmicableEngine: IAmicableEngine
    {
        readonly uint minRange;
        readonly uint maxRange;
        
        public SmartPlunkAmicableEngine(uint minRange, uint maxRange)
        {
            this.minRange = minRange;    
            this.maxRange = maxRange;
        }

        #region IAmicableEngine
        public void Execute(uint minNumber, uint maxNumber, IOutputManager outputManager)
        {
            #region Create Summa Array
            uint[] summaArray = new uint[maxRange - minRange + 1];
            for (uint number = minRange; number <= maxRange; number++)
            {
                uint summa = 0;
                for (uint i = 1; i < number; i++)
                {
                    if (number % i == 0)
                        summa += i;
                }
                summaArray[number - minRange] = summa;
            }
            #endregion

            for (uint number1 = minNumber; number1 <= maxNumber; number1++)
            {
                for (uint number2 = number1 + 1; number2 <= maxNumber; number2++)
                {
                    uint summaOfNumber1 = summaArray[number1 - minRange];
                    uint summaOfNumber2 = summaArray[number2 - minRange];

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