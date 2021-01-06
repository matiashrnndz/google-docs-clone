using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest
{
    [TestClass]
    public class TextTest
    {
        [TestMethod]
        public void TestText()
        {
            Text text = new Text();

            Assert.IsNotNull(text);
        }

        [TestMethod]
        public void TestTextStyleClassShouldBeNullOnCreation()
        {
            Text text = new Text();

            Assert.IsNull(text.StyleClass);
        }

        [TestMethod]
        public void TestTextShouldHaveBlankContentOnCreation()
        {
            Text text = new Text();

            Assert.AreEqual(text.TextContent, "");
        }


        [TestMethod]
        public void TestTextContentShouldBeNullOnCreation()
        {
            Text text = new Text();

            Assert.IsNull(text.ContentThatBelongs);
        }
    }
}
