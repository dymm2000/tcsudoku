using System;

namespace amicable
{
    class Input
    {
        public int minRange;
        public int maxRange;

        public void Validate()
        {
            if (minRange < 0)
                throw new Exception(String.Format("min range {0} should be positive.", minRange));
            if (maxRange < 0)
                throw new Exception(String.Format("max range {0} should be positive.", maxRange));
            if (minRange >= maxRange)
                throw new Exception(String.Format("min range {0} should be lesser than max range {1}.", minRange, maxRange));
        }
    }
}