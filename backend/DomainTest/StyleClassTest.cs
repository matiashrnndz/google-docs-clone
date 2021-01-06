using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest
{
    [TestClass]
    public class StyleClassTest
    {
        [TestMethod]
        public void TestStyleClass()
        {
            StyleClass styleClass = new StyleClass();

            Assert.IsNotNull(styleClass);
        }
    }
}

