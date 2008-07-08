namespace amicable
{
    public interface IAmicableEngine
    {
        void Execute(uint minNumber, uint maxNumber, IOutputManager outputManager);
    }
}