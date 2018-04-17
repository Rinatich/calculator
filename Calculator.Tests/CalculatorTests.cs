using NUnit.Framework;
using Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests
{
    [TestFixture()]
    public class CalculatorTests
    {
        [Test]
        public void CalculateTest_Throw()
        {
            var __calculator = new Calculator();
            Assert.Throws<ArgumentNullException>(() => __calculator.Calculate(null));
            Assert.Throws<ArgumentNullException>(() => __calculator.Calculate("  "));
        }

        [Test]
        public void CalculateTest_Throw_DivideByZero()
        {
            var __calculator = new Calculator();
            Assert.Throws<DivideByZeroException>(() => __calculator.Calculate("1/(3-3)"));
        }

        [TestCase("-1+3-5", -3)]
        [TestCase("1+3-5", -1)]
        [TestCase("1 +   3-5", -1)]
        [TestCase("1+2-3", 0)]
        [TestCase("1*3.5", 3.5)]
        [TestCase("3/2", 1.5)]
        [TestCase("1+2*3", 7)]
        [TestCase("2*(2+9/3)", 10)]
        [TestCase("2*(2+9/(-2+(-1)))", -2)]
        public void CalculateTest(String input, Decimal output)
        {
            var __calculator = new Calculator();
            Decimal __result = __calculator.Calculate(input);
            Assert.AreEqual(__result, output);
        }
    }
}