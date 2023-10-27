using SpreadsheetEngine;

namespace TestSpreadsheetEngine
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestEmptySpreadsheet()
        {
            Spreadsheet empty = new Spreadsheet(0, 0);
            Assert.AreEqual(0, empty.NumRows);
            Assert.AreEqual(0, empty.NumColumns);
        }

        [Test]
        public void TestLargeSpreadsheet()
        {
            Spreadsheet large = new Spreadsheet(400, 234);
            Assert.AreEqual(400, large.NumRows);
            Assert.AreEqual(234, large.NumColumns);
        }

        [Test] public void TestOutOfBounds()
        {
            Spreadsheet spread = new Spreadsheet(20, 20);
            Assert.AreEqual(null, spread.GetCell(50, 50));
        }
    }
}