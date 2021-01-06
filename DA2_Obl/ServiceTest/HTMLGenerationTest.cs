using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using Service;
using ServiceImp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceTest
{
    [TestClass]
    public class HTMLGenerationTest
    {
        [TestMethod]
        public void TestHTMLGeneratorMakesDesiredString()
        {

            Mock<IFormatRepository> mockFormatRepository = new Mock<IFormatRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IHeaderRepository> mockHeaderRepository = new Mock<IHeaderRepository>();
            Mock<IFooterRepository> mockFooterRepository = new Mock<IFooterRepository>();
            Mock<IParagraphRepository> mockParagraphRepository = new Mock<IParagraphRepository>();
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            Mock<IStyleRepository> mockStyleRepository = new Mock<IStyleRepository>();
            Mock<IContentRepository> mockContentRepository = new Mock<IContentRepository>();
            Mock<ITextRepository> mockTextRepository = new Mock<ITextRepository>();
            ICodeGenerator generationLogic = new HTMLGenerator
            {
                FormatRepository = mockFormatRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                HeaderRepository = mockHeaderRepository.Object,
                FooterRepository = mockFooterRepository.Object,
                ParagraphRepository = mockParagraphRepository.Object,
                StyleClassRepository = mockStyleClassRepository.Object,
                StyleRepository = mockStyleRepository.Object,
                ContentRepository = mockContentRepository.Object,
                TextRepository = mockTextRepository.Object,
                StyleHTMLBuilder = new StyleHTMLBuilder()
            };
            Document fakeDocument = GetFakeDocument();
            Header fakeHeader = AssignFakeHeader(fakeDocument);
            IEnumerable<Paragraph> fakeParagraphs = AssignFakeParagraphs(fakeDocument);
            Footer fakeFooter = AssignFakeFooter(fakeDocument);
            IEnumerable<Text> fakeHeaderText = AssignTextForHeader(fakeHeader);
            IEnumerable<Text> fakeFooterText = AssignTextForFooter(fakeFooter);
            IEnumerable<Text> fakeTwoTextsForParagraph = AssignTwoTextsForParagraph(fakeParagraphs.ElementAt(0));
            IEnumerable<Text> fakeThreeTextsForParagraph = AssignThreeTextsForParagraph(fakeParagraphs.ElementAt(1));

            Format fakeFormat = GetFakeFormat();
            mockHeaderRepository
                .Setup(wl => wl.ExistsForDocument(fakeDocument.Id))
                .Returns(true);
            mockFooterRepository
                .Setup(wl => wl.ExistsForDocument(fakeDocument.Id))
                .Returns(true);
            mockHeaderRepository
                .Setup(wl => wl.GetByDocument(fakeDocument.Id))
                .Returns(fakeHeader);
            mockParagraphRepository
                .Setup(wl => wl.GetAllByDocument(fakeDocument.Id))
                .Returns(fakeParagraphs);
            mockFooterRepository
                .Setup(wl => wl.GetByDocument(fakeDocument.Id))
                .Returns(fakeFooter);
            mockTextRepository
                .Setup(wl => wl.GetByContent(fakeHeader.Content))
                .Returns(fakeHeaderText);
            mockTextRepository
                .Setup(wl => wl.GetByContent(fakeParagraphs.ElementAt(0).Content))
                .Returns(fakeTwoTextsForParagraph);
            mockTextRepository
                .Setup(wl => wl.GetByContent(fakeParagraphs.ElementAt(1).Content))
                .Returns(fakeThreeTextsForParagraph);
            mockTextRepository
                .Setup(wl => wl.GetByContent(fakeFooter.Content))
                .Returns(fakeFooterText);
            mockStyleRepository
                .Setup(wl => wl.GetStyles(GetStyleClassNormal().Name, fakeFormat.Name))
                .Returns(GetFakeNormalStyles());
            mockStyleRepository
                .Setup(wl => wl.GetStyles(GetStyleClassFormal().Name, fakeFormat.Name))
                .Returns(GetFakeFormalStyles());
            mockStyleRepository
                .Setup(wl => wl.GetStyles(GetStyleClassTitle().Name, fakeFormat.Name))
                .Returns(GetFakeTitleStyles());




            string result = generationLogic.GenerateHTML(fakeDocument, fakeFormat);


            mockHeaderRepository.VerifyAll();
            mockParagraphRepository.VerifyAll();
            mockFooterRepository.VerifyAll();
            mockTextRepository.VerifyAll();
            mockStyleRepository.VerifyAll();
            Assert.AreEqual(result, GetFakeResultForFirstTest());
        }

        private Document GetFakeDocument()
        {
            return new Document
            {
                Id = Guid.NewGuid(),
                Creator = new User(),
                StyleClass = GetStyleClassNormal(),
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now
            };
        }
        private Header AssignFakeHeader(Document document)
        {
            Header head = new Header()
            {
                Id = Guid.NewGuid(),
                DocumentThatBelongs = document,
                Content = new Content()
                {
                    Id = Guid.NewGuid()
                },
                StyleClass = GetStyleClassTitle()
            };
            return head;
        }
        private Footer AssignFakeFooter(Document document)
        {
            Footer foot = new Footer()
            {
                Id = Guid.NewGuid(),
                DocumentThatBelongs = document,
                Content = new Content()
                {
                    Id = Guid.NewGuid()
                },
                StyleClass = null
            };
            return foot;
        }
        private IEnumerable<Paragraph> AssignFakeParagraphs(Document document)
        {
            List<Paragraph> fakeParagraphs = new List<Paragraph>
            {
                new Paragraph
                {
                    Id = Guid.NewGuid(),
                    DocumentThatBelongs = document,
                    Content = new Content()
                    {
                        Id = Guid.NewGuid()
                    },
                    StyleClass = GetStyleClassFormal(),
                    Position = 0
                },
                new Paragraph
                {
                    Id = Guid.NewGuid(),
                    DocumentThatBelongs = document,
                    Content = new Content()
                    {
                        Id = Guid.NewGuid()
                    },
                    StyleClass = null,
                    Position = 1
                }
            };
            return fakeParagraphs.AsEnumerable();
        }
        private IEnumerable<Text> AssignTextForHeader(Header header)
        {
            return new List<Text> {
                    new Text
                {
                    Id = Guid.NewGuid(),
                    ContentThatBelongs = header.Content,
                    StyleClass = null,
                    TextContent = "Gualeguaychú"
                }
            }.AsEnumerable();
        }
        private IEnumerable<Text> AssignTextForFooter(Footer footer)
        {
            return new List<Text> {
                new Text
                {
                    Id = Guid.NewGuid(),
                    ContentThatBelongs = footer.Content,
                    StyleClass = GetStyleClassNormal(),
                    TextContent = "Boris 2017"
                }
            }.AsEnumerable();
        }
        private IEnumerable<Text> AssignTwoTextsForParagraph(Paragraph paragraph)
        {
            List<Text> fakeTwoTests = new List<Text>
            {
                new Text
                {
                    Id = Guid.NewGuid(),
                    ContentThatBelongs = paragraph.Content,
                    StyleClass = null,
                    TextContent = "Me gusta la fiesta la danza la chuza"
                },
                new Text
                {
                    Id = Guid.NewGuid(),
                    ContentThatBelongs = paragraph.Content,
                    StyleClass = GetStyleClassNormal(),
                    TextContent = "Pero cuidado que viene la yusta"
                }
            };
            return fakeTwoTests.AsEnumerable();
        }
        private IEnumerable<Text> AssignThreeTextsForParagraph(Paragraph paragraph)
        {
            List<Text> fakeThreeTests = new List<Text>
            {
                new Text
                {
                    Id = Guid.NewGuid(),
                    ContentThatBelongs = paragraph.Content,
                    StyleClass = GetStyleClassTitle(),
                    TextContent = "Do you know da wey"
                },
                new Text
                {
                    Id = Guid.NewGuid(),
                    ContentThatBelongs = paragraph.Content,
                    StyleClass = null,
                    TextContent = "Click click click click click"
                },
                new Text
                {
                    Id = Guid.NewGuid(),
                    ContentThatBelongs = paragraph.Content,
                    StyleClass = GetStyleClassFormal(),
                    TextContent = "GOD SAVE THE QUEEN"
                }
            };
            return fakeThreeTests.AsEnumerable();
        }
        private StyleClass GetStyleClassNormal()
        {
            return new StyleClass
            {
                Name = "Normal",
                BasedOn = null
            };
        }
        private StyleClass GetStyleClassTitle()
        {
            return new StyleClass
            {
                Name = "Title",
                BasedOn = null
            };
        }
        private StyleClass GetStyleClassFormal()
        {
            return new StyleClass
            {
                Name = "Formal",
                BasedOn = GetStyleClassTitle()
            };
        }
        private Format GetFakeFormat()
        {
            return new Format
            {
                Name = "Prueba1"
            };
        }
        private IEnumerable<Style> GetFakeNormalStyles()
        {
            List<Style> fakeStyles = new List<Style>
            {
                new Style
                {
                Id = Guid.NewGuid(),
                Format = GetFakeFormat(),
                StyleClass = GetStyleClassNormal(),
                Key = "Font",
                Value = "Arial,12"
                }
            };
            return fakeStyles.AsEnumerable();
        }
        private IEnumerable<Style> GetFakeFormalStyles()
        {
            List<Style> fakeStyles = new List<Style>
            {
                new Style
                {
                Id = Guid.NewGuid(),
                Format = GetFakeFormat(),
                StyleClass = GetStyleClassFormal(),
                Key = "Font",
                Value = "Courier,12"
                },
                new Style
                {
                    Id = Guid.NewGuid(),
                    Format = GetFakeFormat(),
                    StyleClass = GetStyleClassFormal(),
                    Key = "Italics",
                    Value = ""
                }
            };
            return fakeStyles.AsEnumerable();
        }
        private IEnumerable<Style> GetFakeTitleStyles()
        {
            List<Style> fakeStyles = new List<Style>
            {
                new Style
                {
                Id = Guid.NewGuid(),
                Format = GetFakeFormat(),
                StyleClass = GetStyleClassTitle(),
                Key = "Font",
                Value = "Arial,24"
                },
                new Style
                {
                    Id = Guid.NewGuid(),
                    Format = GetFakeFormat(),
                    StyleClass = GetStyleClassFormal(),
                    Key = "Bold",
                    Value = ""
                }
            };
            return fakeStyles.AsEnumerable();
        }

        private string GetFakeResultForFirstTest()
        {
            return "<br><strong><p style='font-family:Arial; font-size:24pt;'>Gualeguaychú</p></strong><br><br><br><em><strong><p style='font-family:Courier; font-size:12pt;'>Me gusta la fiesta la danza la chuza</p></strong></em><br><em><strong><p style='font-family:Arial; font-size:12pt;'>Pero cuidado que viene la yusta</p></strong></em><br><br><strong><p style='font-family:Arial; font-size:24pt;'>Do you know da wey</p></strong><br><p style='font-family:Arial; font-size:12pt;'>Click click click click click</p><br><em><strong><p style='font-family:Courier; font-size:12pt;'>GOD SAVE THE QUEEN</p></strong></em><br><br><p style='font-family:Arial; font-size:12pt;'>Boris 2017</p>";
        }


    }
}
