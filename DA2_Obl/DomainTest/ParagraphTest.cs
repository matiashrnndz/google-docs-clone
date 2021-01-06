using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest
{
    [TestClass]
    public class ParagraphTest
    {
        [TestMethod]
        public void TestParagraph()
        {
            Paragraph paragraph = new Paragraph();

            Assert.IsNotNull(paragraph);
        }

        [TestMethod]
        public void TestParagraphDocumentShouldBeNullOnCreation()
        {
            Paragraph paragraph = new Paragraph();

            Assert.IsNull(paragraph.DocumentThatBelongs);
        }

        [TestMethod]
        public void TestParagraphStyleClassShouldBeNullOnCreation()
        {
            Paragraph paragraph = new Paragraph();

            Assert.IsNull(paragraph.StyleClass);
        }

        [TestMethod]
        public void TestParagraphContentShouldBeNullOnCreation()
        {
            Paragraph paragraph = new Paragraph();

            Assert.IsNull(paragraph.Content);
        }
    }
}
