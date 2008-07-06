namespace amicable
{
    public interface IAmicableEngine
    {
        bool IsAmicableNumbers { get; }
        bool IsFriendlyNumbers { get; }
        void Execute(uint number1, uint number2);
    }
}