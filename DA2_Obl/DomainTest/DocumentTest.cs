using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest
{
    [TestClass]
    public class DocumentTest
    {
        [TestMethod]
        public void TestDocument()
        {
            Document document = new Document();

            Assert.IsNotNull(document);
        }

        [TestMethod]
        public void TestDocumentCreatorShouldBeNullOnCreation()
        {
            Document document = new Document();

            Assert.IsNull(document.Creator);
        }

        [TestMethod]
        public void TestDocumentStyleClassShouldBeNullOnCreation()
        {
            Document document = new Document();

            Assert.IsNull(document.StyleClass);
        }

        [TestMethod]
        public void TestDocumentCreationDateShouldExistOnCreation()
        {
            Document document = new Document();

            Assert.IsNotNull(document.CreationDate);
        }

        [TestMethod]
        public void TestDocumentLastModificationShouldExistOnCreation()
        {
            Document document = new Document();

            Assert.IsNotNull(document.LastModification);
        }
    }
}
