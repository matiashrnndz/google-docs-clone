using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest
{
    [TestClass]
    public class FooterTest
    {
        [TestMethod]
        public void TestFooter()
        {
            Footer footer = new Footer();

            Assert.IsNotNull(footer);
        }

        [TestMethod]
        public void TestFooterDocumentShouldBeNullOnCreation()
        {
            Footer footer = new Footer();

            Assert.IsNull(footer.DocumentThatBelongs);
        }

        [TestMethod]
        public void TestFooterStyleClassShouldBeNullOnCreation()
        {
            Footer footer = new Footer();

            Assert.IsNull(footer.StyleClass);
        }

        [TestMethod]
        public void TestFooterContentShouldBeNullOnCreation()
        {
            Footer footer = new Footer();

            Assert.IsNull(footer.Content);
        }
    }
}
