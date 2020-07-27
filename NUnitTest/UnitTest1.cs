using NUnit.Framework;
using Moq;
using Assignment;

namespace NUnitTest
{

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var total = 305;
            Program.total = 0;

            // Action
            Program.ActivePromotionFor_SKU("A,A,A,A,B,B,B,C,C,D");

            // Assert
            Assert.AreEqual(total, Program.total);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var total = 250;
            Program.total = 0;

            // Action
            Program.ActivePromotionFor_SKU("A,A,A,B,B,C,D,D");

            // Assert
            Assert.AreEqual(total, Program.total);
        }
    }
}