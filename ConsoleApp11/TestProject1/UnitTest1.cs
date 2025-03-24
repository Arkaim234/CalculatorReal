using ConsoleApp11;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Reflection.Metadata;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestExpression("10 + 9", 19);
            TestExpression("10 - 9", 1);
            TestExpression("10 * 9", 90);
            TestExpression("90 / 9", 10);
            TestExpression("10 + 2 * 6", 22);
            TestExpression("100 * 2 + 12", 212);
            TestExpression("100 * 2 - 50 + 40 / -8", 145);
            TestExpression("10 + 3 * 2 / 2 - 1", 12);
        }

        public void TestExpression(string expression, double expected)
        {
            double result = Calculator.EvaluateExpression(expression);
            Assert.AreEqual(expected, result);
        }
    }
}