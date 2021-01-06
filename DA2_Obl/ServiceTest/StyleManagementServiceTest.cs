using Domain;
using Exception;
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
    public class StyleManagementServiceTest
    {

        private Mock<IStyleRepository> mockStyleRepository;
        private Mock<IFormatRepository> mockFormatRepository;
        private Mock<IStyleClassRepository> mockStyleClassRepository;
        private IStyleManagementService styleLogic;

        public StyleManagementServiceTest()
        {
            mockStyleRepository = new Mock<IStyleRepository>();
            mockFormatRepository = new Mock<IFormatRepository>();
            mockStyleClassRepository = new Mock<IStyleClassRepository>();
            styleLogic = new StyleManagementService()
            {
                StyleBuilder = new GenericStyleBuilder(),
                FormatRepository = mockFormatRepository.Object,
                StyleClassRepository = mockStyleClassRepository.Object,
                StyleRepository = mockStyleRepository.Object
            };
        }

        [TestMethod]
        public void TestGetStylesReturnsSomeStyles()
        {

            IEnumerable<Style> fakeStyles = GetFakeStyles();
            mockStyleRepository
                .Setup(wl => wl.GetStyles("Al Bacalao", "Normal"))
                .Returns(fakeStyles);

            

            IEnumerable<Style> results = styleLogic.GetAll("Normal", "Al Bacalao");

            mockStyleRepository.VerifyAll();
            Assert.IsNotNull(results);
            Assert.IsTrue(results.SequenceEqual(fakeStyles));
        }

        [TestMethod]
        public void TestAddStyleWorksOnValidStyle()
        {
            IEnumerable<Style> fakeStyles = GetFakeStyles();
            StyleClass fakeStyleClass = GetFakeStyleClass();
            Format fakeFormat = GetFakeFormat();
            Style newStyle = new Style
            {
                Id = Guid.NewGuid(),
                Format = GetFakeFormat(),
                StyleClass = GetFakeStyleClass(),
                Key = "Color",
                Value = "Blue"
            };

            mockStyleRepository
                .Setup(wl => wl.Add(newStyle));
            mockStyleRepository
                .Setup(wl => wl.Delete(fakeStyles.ElementAt(0)));
            mockStyleRepository
                .Setup(wl => wl.GetStyles(fakeStyleClass.Name, fakeFormat.Name))
                .Returns(fakeStyles);

            Style result = styleLogic.Add(fakeFormat.Name, fakeStyleClass.Name, newStyle);

            mockStyleRepository.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(newStyle, result);
        }

        

        [TestMethod]
        [ExpectedException(typeof(InvalidStyleException))]
        public void TestAddStyleFailsOnInvalidStyle()
        {
            IEnumerable<Style> fakeStyles = GetFakeStyles();
            StyleClass fakeStyleClass = GetFakeStyleClass();
            Format fakeFormat = GetFakeFormat();
            Style newStyle = new Style
            {
                Id = Guid.NewGuid(),
                Format = GetFakeFormat(),
                StyleClass = GetFakeStyleClass(),
                Key = "brbrbrbrbr",
                Value = "Blue"
            };
            Style result = styleLogic.Add(fakeFormat.Name, fakeStyleClass.Name, newStyle);
        }

        [TestMethod]
        public void TestDeleteStyleWorksOnExistingStyle()
        {
            IEnumerable<Style> fakeStyles = GetFakeStyles();

            mockStyleRepository
                .Setup(wl => wl.Delete(fakeStyles.ElementAt(1)));
            mockStyleRepository
                .Setup(wl => wl.ExistsById(fakeStyles.ElementAt(1).Id))
                .Returns(true);
            mockStyleRepository
                .Setup(wl => wl.GetById(fakeStyles.ElementAt(1).Id))
                .Returns(fakeStyles.ElementAt(1));


            styleLogic.Delete(fakeStyles.ElementAt(1).Id);

            mockStyleRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingStyleException))]
        public void TestDeleteStyleFailsOnMissingStyle()
        {
            IEnumerable<Style> fakeStyles = GetFakeStyles();

            mockStyleRepository
                .Setup(wl => wl.ExistsById(fakeStyles.ElementAt(1).Id))
                .Returns(false);

            styleLogic.Delete(fakeStyles.ElementAt(1).Id);
        }

        private static StyleClass GetFakeStyleClass()
        {
            return new StyleClass
            {
                Name = "Al Bacalao"
            };
        }

        private Format GetFakeFormat()
        {
            return new Format
            {
                Name = "Normal"
            };
        }

        private IEnumerable<Style> GetFakeStyles()
        {
            StyleClass fakeStyleClass = new StyleClass
            {
                Name = "Al Bacalao"
            };
            Format fakeFormat = new Format
            {
                Name = "Normal"
            };
            List<Style> fakeStyles = new List<Style>
            {
                new Style
                {
                    Id = Guid.NewGuid(),
                    StyleClass = fakeStyleClass,
                    Format = fakeFormat,
                    Key = "Color",
                    Value = "red"
                },
                new Style
                {
                    Id = Guid.NewGuid(),
                    StyleClass = fakeStyleClass,
                    Format = fakeFormat,
                    Key = "Font",
                    Value = "times new roman,24"
                },
                 new Style
                {
                    Id = Guid.NewGuid(),
                    StyleClass = fakeStyleClass,
                    Format = fakeFormat,
                    Key = "Bold",
                    Value = ""
                }
            };
            return fakeStyles.AsEnumerable();
        }
    }
}