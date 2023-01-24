using System;

namespace DeveloperCodeAndTests
{
    public class DeveloperCode
    {
        public static int Addition(int a, int b)
        {
            return a + b;
        }

        public static float Division(int a, int b)
        {
            if (b != 0)
            {
                return a / b;
            }
            throw new DivideByZeroException();
        }
    }
}