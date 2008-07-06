namespace amicable
{
    public class SeqAmicableEngine: IAmicableEngine
    {
        bool isAmicableNumbers;
        bool isFriendlyNumbers;
        #region IAmicableEngine
        public bool IsAmicableNumbers
        {
            get { return isAmicableNumbers; }
        }
        public bool IsFriendlyNumbers
        {
            get { return isFriendlyNumbers; }
        }
        public void Execute(uint number1, uint number2)
        {
            isAmicableNumbers = false;
            isFriendlyNumbers = false;

            uint summaOfNumber1 = CalculateSumma(number1);
            uint summaOfNumber2 = CalculateSumma(number2);

            isAmicableNumbers = (summaOfNumber1 == number2) && (number1 == summaOfNumber2);
            isFriendlyNumbers = ((double)(summaOfNumber1 + number1)/number1) == ((double)(summaOfNumber2 + number2)/number2);
        }
        #endregion
        uint CalculateSumma(uint number)
        {
            uint summa = 0;
            for (uint i = 1; i < number; i++)
            {
                if (number % i == 0)
                    summa += i;
            }
            return summa;
        }
    }
}