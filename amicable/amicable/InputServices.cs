using System;

namespace amicable
{
    class InputServices
    {
        public static Input CreateInput(string[] args)
        {
            Input input = new Input();
            input.minRange = UInt32.Parse(args[0]);
            input.maxRange = UInt32.Parse(args[1]);
            return input;
        }
    }
}