using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest
{
    [TestClass]
    public class StyleTest
    {
        [TestMethod]
        public void TestStyle()
        {
            Style style = new Style();

            Assert.IsNotNull(style);
        }

        [TestMethod]
        public void TestStyleStyleClassShouldBeNullOnCreation()
        {
            Style style = new Style();

            Assert.IsNull(style.StyleClass);
        }

        [TestMethod]
        public void TestStyleFormatShouldBeNullOnCreation()
        {
            Style style = new Style();

            Assert.IsNull(style.Format);
        }
    }
}
