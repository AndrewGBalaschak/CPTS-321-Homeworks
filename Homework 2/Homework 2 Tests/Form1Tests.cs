using Homework_2;

namespace Homework_2
{
    public class Form1Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAllDuplicateHash()
        {
            List<int> list = new List<int> { 2, 2, 2, 2, 2, 2 };
            Assert.AreEqual(1, DistinctNumbers.HashSetCount(list));
        }

        [Test]
        public void TestAllDuplicateFalse()
        {
            List<int> list = new List<int> { 2, 2, 2, 2, 2, 2 };
            Assert.AreEqual(1, DistinctNumbers.FalsifyCount(list));
        }

        [Test]
        public void TestAllDuplicateSort()
        {
            List<int> list = new List<int> { 2, 2, 2, 2, 2, 2 };
            Assert.AreEqual(1, DistinctNumbers.SortCount(list));
        }


        [Test]
        public void TestAllUniqueHash()
        {
            List<int> list = new List<int> { 2, 3, 8, 4, 7, 6 };
            Assert.AreEqual(6, DistinctNumbers.HashSetCount(list));

        }

        [Test]
        public void TestAllUniqueFalse()
        {
            List<int> list = new List<int> { 2, 3, 8, 4, 7, 6 };
            Assert.AreEqual(6, DistinctNumbers.FalsifyCount(list));
        }

        [Test]
        public void TestAllUniqueSort()
        {
            List<int> list = new List<int> { 2, 3, 8, 4, 7, 6 };
            Assert.AreEqual(6, DistinctNumbers.SortCount(list));
        }

        [Test]
        public void TestEmptyHash()
        {
            List<int> list = new List<int> { };
            Assert.AreEqual(0, DistinctNumbers.HashSetCount(list));
        }

        [Test]
        public void TestEmptyFalse()
        {
            List<int> list = new List<int> { };
            Assert.AreEqual(0, DistinctNumbers.FalsifyCount(list));
        }

        [Test]
        public void TestEmptySort()
        {
            List<int> list = new List<int> { };
            Assert.AreEqual(0, DistinctNumbers.SortCount(list));
        }

    }
}