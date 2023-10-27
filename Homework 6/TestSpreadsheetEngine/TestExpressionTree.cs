// <copyright file="TestExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TestSpreadsheetEngine
{
    using SpreadsheetEngine;

    public class TestExpressionTree
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAdd()
        {
            ExpressionTree test = new ExpressionTree("5+3+5+4+7");
            Assert.That(test.Evaluate(), Is.EqualTo(24));
        }

        [Test]
        public void TestSubtract()
        {
            ExpressionTree test = new ExpressionTree("457-4-78-2-89");
            Assert.That(test.Evaluate(), Is.EqualTo(284));
        }

        [Test]
        public void TestMultiply()
        {
            ExpressionTree test = new ExpressionTree("5*3*4*2");
            Assert.That(test.Evaluate(), Is.EqualTo(120));
        }

        [Test]
        public void TestDivide()
        {
            ExpressionTree test = new ExpressionTree("5+3");
            Assert.That(test.Evaluate(), Is.EqualTo(8));
        }

        [Test]
        public void TestModulus()
        {
            ExpressionTree test = new ExpressionTree("5%4");
            Assert.That(test.Evaluate(), Is.EqualTo(1));
        }

        [Test]
        public void TestPower()
        {
            ExpressionTree test = new ExpressionTree("5^3^2");
            Assert.That(test.Evaluate(), Is.EqualTo(15625));
        }

        [Test]
        public void TestParenthesis()
        {
            ExpressionTree test = new ExpressionTree("(5+2)*8");
            Assert.That(test.Evaluate(), Is.EqualTo(56));
        }

        [Test]
        public void TestAddSubtract()
        {
            ExpressionTree test = new ExpressionTree("5+3-3+4-8");
            Assert.That(test.Evaluate(), Is.EqualTo(1));
        }

        [Test]
        public void TestDecimalResult()
        {
            ExpressionTree test = new ExpressionTree("(8+3)/2+4*((3+4)*3)");
            Assert.That(test.Evaluate(), Is.EqualTo(89.5));
        }

        [Test]
        public void TestComplexEquation1()
        {
            ExpressionTree test = new ExpressionTree("((45-3*5)/(4+1+1+4+5/5)+(34+49/2+3)/3+5)^2");
            Assert.That(Math.Round(test.Evaluate(), 10), Is.EqualTo(796.7789256198));
        }

        [Test]
        public void TestComplexEquation2()
        {
            ExpressionTree test = new ExpressionTree("((3^4+5^6)*(7^8-9^0))/((1^2+2^3)*(4^5-6^7))+((8^9+0^1)*(5^4-3^2))/((7^6+6^5)*(9^8-1^0))");
            Assert.That(Math.Round(test.Evaluate(), 10), Is.EqualTo(-36069.4852221974));
        }

        [Test]
        public void TestComplexEquation3()
        {
            ExpressionTree test = new ExpressionTree("4+6/(4+2*5-6)+356784567%(43+6^4)");
            Assert.That(test.Evaluate(), Is.EqualTo(1326.75));
        }

        [Test]
        public void TestSingleVariable()
        {
            ExpressionTree test = new ExpressionTree("A1+3");
            Assert.That(test.Evaluate(), Is.EqualTo(3));
            test.SetVariable("A1", 5);
            Assert.That(test.Evaluate(), Is.EqualTo(8));
        }

        [Test]
        public void TestMultiVariable()
        {
            ExpressionTree test = new ExpressionTree("A1-B5+3");
            Assert.That(test.Evaluate(), Is.EqualTo(3));
            test.SetVariable("A1", 5);
            test.SetVariable("B5", 3);
            Assert.That(test.Evaluate(), Is.EqualTo(5));
        }
    }
}