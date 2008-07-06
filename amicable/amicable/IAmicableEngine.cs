namespace amicable
{
    public interface IAmicableEngine
    {
        bool IsAmicableNumbers { get; }
        bool IsFriendlyNumbers { get; }
        void Execute(int number1, int number2);
    }
}