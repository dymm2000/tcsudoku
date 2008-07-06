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
        public void Execute(int number1, int number2)
        {
            isAmicableNumbers = false;
            isFriendlyNumbers = false;

            int summaOfNumber1 = CalculateSumma(number1);
            int summaOfNumber2 = CalculateSumma(number2);

            isAmicableNumbers = (summaOfNumber1 == number2) && (number1 == summaOfNumber2);
            isFriendlyNumbers = ((double)(summaOfNumber1 + number1)/number1) == ((double)(summaOfNumber2 + number2)/number2);
        }
        #endregion
        int CalculateSumma(int number)
        {
            int summa = 0;
            for (int i = 1; i < number; i++)
            {
                if (number % i == 0)
                    summa += i;
            }
            return summa;
        }
    }
}