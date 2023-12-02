// <copyright file="TestSpreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TestSpreadsheetEngine
{
    using SpreadsheetEngine;

    public class TestSpreadsheet
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestEmptySpreadsheet()
        {
            Spreadsheet empty = new Spreadsheet(0, 0);
            Assert.That(empty.NumRows, Is.EqualTo(0));
            Assert.That(empty.NumColumns, Is.EqualTo(0));
        }

        [Test]
        public void TestLargeSpreadsheet()
        {
            Spreadsheet large = new Spreadsheet(400, 234);
            Assert.That(large.NumRows, Is.EqualTo(400));
            Assert.That(large.NumColumns, Is.EqualTo(234));
        }

        [Test]
        public void TestOutOfBounds()
        {
            Spreadsheet spread = new Spreadsheet(20, 20);
            Assert.That(spread.GetCell(50, 50), Is.EqualTo(null));
        }

        [Test]
        public void TestDependentCells()
        {
            Spreadsheet spread = new Spreadsheet(20, 20);
            spread.SetCell(0, 0, "5");
            spread.SetCell(0, 1, "=A1");
            Assert.That(spread.GetCell(0, 1).Value, Is.EqualTo("5"));
            spread.SetCell(0, 0, "8");
            Assert.That(spread.GetCell(0, 1).Value, Is.EqualTo("8"));
        }

        [Test]
        public void TestDependentDependentCells()
        {
            Spreadsheet spread = new Spreadsheet(20, 20);
            spread.SetCell(0, 0, "5");
            spread.SetCell(0, 1, "=A1");
            Assert.That(spread.GetCell(0, 1).Value, Is.EqualTo("5"));

            spread.SetCell(5, 5, "=B1*6");
            spread.SetCell(0, 0, "8");

            Assert.That(spread.GetCell(0, 1).Value, Is.EqualTo("8"));
            Assert.That(spread.GetCell(5, 5).Value, Is.EqualTo("48"));
        }

    }
}