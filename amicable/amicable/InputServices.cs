using System;

namespace amicable
{
    class InputServices
    {
        public static Input CreateInput(string[] args)
        {
            Input input = new Input();
            input.minRange = Int32.Parse(args[0]);
            input.maxRange = Int32.Parse(args[1]);
            return input;
        }
    }
}