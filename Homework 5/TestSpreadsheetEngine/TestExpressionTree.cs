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
            Assert.AreEqual(24, test.Evaluate());
        }

        [Test]
        public void TestSubtract()
        {
            ExpressionTree test = new ExpressionTree("457-4-78-2-89");
            Assert.AreEqual(284, test.Evaluate());
        }

        [Test]
        public void TestMultiply()
        {
            ExpressionTree test = new ExpressionTree("5*3*4*2");
            Assert.AreEqual(120, test.Evaluate());
        }

        [Test]
        public void TestDivide()
        {
            ExpressionTree test = new ExpressionTree("5+3");
            Assert.AreEqual(8, test.Evaluate());
        }

        [Test]
        public void TestPower()
        {
            ExpressionTree test = new ExpressionTree("5^3^2");
            Assert.AreEqual(15625, test.Evaluate());
        }

        [Test]
        public void TestParenthesis()
        {
            ExpressionTree test = new ExpressionTree("(5+2)*8");
            Assert.AreEqual(56, test.Evaluate());
        }

        [Test]
        public void TestAddSubtract()
        {
            ExpressionTree test = new ExpressionTree("5+3-3+4-8");
            Assert.AreEqual(1, test.Evaluate());
        }

        [Test]
        public void TestDecimalResult()
        {
            ExpressionTree test = new ExpressionTree("(8+3)/2+4*((3+4)*3)");
            Assert.AreEqual(89.5, test.Evaluate());
        }
    }
}