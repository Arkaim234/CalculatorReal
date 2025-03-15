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
            TestOperation(10, 9, new Plus(), 19);
            TestOperation(10, 9, new Minus(), 1);
            TestOperation(10, 9, new Multiplication(), 90);
            TestOperation(90, 9, new Division(), 10);
        }
        public void TestOperation(double a, double b, ISignature oper, double expected)
        {
            var input = new Context(oper);
            var newresult = input.ExecuteOperation(a,b);
            Assert.AreEqual(expected, newresult);
        }
    }
}