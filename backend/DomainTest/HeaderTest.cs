using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest
{
    [TestClass]
    public class HeaderTest
    {
        [TestMethod]
        public void TestHeader()
        {
            Header header = new Header();

            Assert.IsNotNull(header);
        }

        [TestMethod]
        public void TestHeaderDocumentShouldBeNullOnCreation()
        {
            Header header = new Header();

            Assert.IsNull(header.DocumentThatBelongs);
        }

        [TestMethod]
        public void TestHeaderStyleClassShouldBeNullOnCreation()
        {
            Header header = new Header();

            Assert.IsNull(header.StyleClass);
        }

        [TestMethod]
        public void TestHeaderContentShouldBeNullOnCreation()
        {
            Header header = new Header();

            Assert.IsNull(header.Content);
        }
    }
}
