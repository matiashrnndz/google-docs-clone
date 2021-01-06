using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest
{
    [TestClass]
    public class ContentTest
    {
        [TestMethod]
        public void TestContent()
        {
            Content content = new Content();

            Assert.IsNotNull(content);
        }
    }
}
