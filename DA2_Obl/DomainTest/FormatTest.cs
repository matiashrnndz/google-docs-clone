using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest
{
    [TestClass]
    public class FormatTest
    {
        [TestMethod]
        public void TestFormat()
        {
            Format format = new Format();

            Assert.IsNotNull(format);
        }
    }
}
