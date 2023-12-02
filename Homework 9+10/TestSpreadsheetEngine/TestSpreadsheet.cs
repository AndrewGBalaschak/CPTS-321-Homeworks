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
            var ex = Assert.Throws<IndexOutOfRangeException>(() => spread.GetCell(50, 50));
            Assert.That(ex.Message, Is.EqualTo("#BAD REFERENCE"));
        }

        [Test]
        public void TestDependentCells()
        {
            Spreadsheet spread = new Spreadsheet(20, 20);
            spread.SetCell("A1", "5");
            spread.SetCell("A2", "=A1");
            Assert.That(spread.GetCell("A2").Value, Is.EqualTo("5"));
            spread.SetCell("A1", "8");
            Assert.That(spread.GetCell("A2").Value, Is.EqualTo("8"));
        }

        [Test]
        public void TestDependentDependentCells()
        {
            Spreadsheet spread = new Spreadsheet(20, 20);
            spread.SetCell("A1", "5");
            spread.SetCell("B1", "=A1");
            Assert.That(spread.GetCell(0, 1).Value, Is.EqualTo("5"));

            spread.SetCell("E5", "=B1*6");
            spread.SetCell("A1", "8");

            Assert.That(spread.GetCell("B1").Value, Is.EqualTo("8"));
            Assert.That(spread.GetCell("E5").Value, Is.EqualTo("48"));
        }

        [Test]
        public void TestUndoRedo()
        {
            Spreadsheet spread = new Spreadsheet(20, 20);
            spread.SetCell("A1", "5");
            Assert.That(spread.GetCell("A1").Value, Is.EqualTo("5"));
            spread.SetCell("A1", "8");
            Assert.That(spread.GetCell("A1").Value, Is.EqualTo("8"));

            spread.Undo();
            Assert.That(spread.GetCell("A1").Value, Is.EqualTo("5"));
        }

        [Test]
        public void TestSaveOpen()
        {
            Spreadsheet spread = new Spreadsheet(20, 20);
            spread.SetCell("A1", "=B1*C2");
            spread.SetCell("B1", "7");
            spread.SetCell("C2", "8");

            string directory = System.IO.Directory.GetCurrentDirectory();

            spread.Save(directory + "\\test.xml");
            Console.WriteLine(directory);
            spread.Clear();

            spread.Open(directory + "\\test.xml");

            Assert.That(spread.GetCell("A1").Value, Is.EqualTo("56"));
            Assert.That(spread.GetCell("B1").Value, Is.EqualTo("7"));
            Assert.That(spread.GetCell("C2").Value, Is.EqualTo("8"));
        }

        [Test]
        public void TestSelfReference()
        {
            Spreadsheet spread = new Spreadsheet(20, 20);
            spread.SetCell("A1", "5");
            spread.SetCell("A2", "=100/(A2*3)");
            Assert.That(spread.GetCell("A2").Value, Is.EqualTo("#SELF REFERENCE"));
        }

        [Test]
        public void TestCircularReference()
        {
            Spreadsheet spread = new Spreadsheet(20, 20);
            spread.SetCell("A1", "=B1*2");
            spread.SetCell("A2", "=A1*5");
            spread.SetCell("B1", "=B2*3");
            spread.SetCell("B2", "=A2*4");
            Assert.That(spread.GetCell("B2").Value, Is.EqualTo("#CIRCULAR REFERENCE"));
        }
    }
}