using System.Diagnostics;
using TestRunner.Attributes;

namespace DeveloperCodeAndTests
{
    [TestClass]
    public class DeveloperTests
    {
        [TestInit]
        public static void SetDebugChannel()
        {
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.WriteLine("\nTest BEGIN");
            Debug.WriteLine("-------------------------------------------------------------");
        }

        [TestMethod]
        public static void AdditionTest()
        {
            bool result = DeveloperCode.Addition(1, 2) == 3;
            Debug.Assert(result, "Addition - FAILED");
            Debug.WriteLineIf(result, "Addition - PASSED");
        }

        [TestMethod]
        public static void DivisionTest()
        {
            bool result = DeveloperCode.Division(10, 2) == 5;
            Debug.Assert(result, "Division - FAILED");
            Debug.WriteLineIf(result, "Division - PASSED");
        }

        [TestMethod]
        public static void DivisionByZeroTest()
        {
            try
            {
                DeveloperCode.Division(2, 0);
                Debug.Fail("Division by zero was allowed with no exception thrown");
            }
            catch (DivideByZeroException)
            {
                Debug.Assert(true);
                Debug.WriteLineIf(true, "Division by zero was not allowed - PASSED");
            }
        }

        [TestFinal]
        public static void CloseDebugChannel()
        {
            Debug.Close();
            Debug.WriteLine("-------------------------------------------------------------");
            Debug.WriteLine("Test END\n");
        }
    }
}
