using System;

namespace amicable
{
    class Input
    {
        public uint minRange;
        public uint maxRange;

        public void Validate()
        {
            if (minRange >= maxRange)
                throw new Exception(String.Format("min range {0} should be lesser than max range {1}.", minRange, maxRange));
        }
    }
}