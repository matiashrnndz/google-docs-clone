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
    public class FormatManagementTest
    {
        [TestMethod]
        public void TestGetAllFormatsCallsRepositoryGetAll()
        {
            Mock<IFormatRepository> mockFormatRepository = new Mock<IFormatRepository>();
            IFormatManagementService formatLogic = new FormatManagementService
            {
                FormatRepository = mockFormatRepository.Object
            };

            IEnumerable<Format> fakeFormats = GetFakeFormats();

            mockFormatRepository
                .Setup(wl => wl.GetAll())
                .Returns(fakeFormats);

            IEnumerable<Format> results = formatLogic.GetAll();

            mockFormatRepository.VerifyAll();
            Assert.IsNotNull(results);
            Assert.IsTrue(fakeFormats.SequenceEqual(results));
        }

        [TestMethod]
        public void TestGetFormatWorksOnExistingFormat()
        {
            Mock<IFormatRepository> mockFormatRepository = new Mock<IFormatRepository>();
            IFormatManagementService formatLogic = new FormatManagementService
            {
                FormatRepository = mockFormatRepository.Object
            };

           Format fakeFormat = new Format
           {
               Name = "Normal"
           };

            mockFormatRepository
                .Setup(wl => wl.Exists("Normal"))
                .Returns(true);
            mockFormatRepository
                .Setup(wl => wl.GetByName("Normal"))
                .Returns(fakeFormat);

            Format result = formatLogic.GetByName("Normal");

            mockFormatRepository.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeFormat, result);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingFormatException))]
        public void TestGetFormatFailsOnMissingFormat()
        {
            Mock<IFormatRepository> mockFormatRepository = new Mock<IFormatRepository>();
            IFormatManagementService formatLogic = new FormatManagementService
            {
                FormatRepository = mockFormatRepository.Object
            };

            Format fakeFormat = new Format
            {
                Name = "Normal"
            };

            mockFormatRepository
                .Setup(wl => wl.Exists("Normal"))
                .Returns(false);
            mockFormatRepository
                .Setup(wl => wl.GetByName("Normal"))
                .Returns(fakeFormat);

            Format result = formatLogic.GetByName("Normal");

            mockFormatRepository.VerifyAll();
        }

        [TestMethod]
        public void TestAddFormatWorksOnMissingFormat()
        {
            Mock<IFormatRepository> mockFormatRepository = new Mock<IFormatRepository>();
            IFormatManagementService formatLogic = new FormatManagementService
            {
                FormatRepository = mockFormatRepository.Object
            };

            Format fakeFormat = new Format
            {
                Name = "Normal"
            };

            mockFormatRepository
                .Setup(wl => wl.Exists("Normal"))
                .Returns(false);
            mockFormatRepository
                .Setup(wl => wl.Add(fakeFormat));

            Format result = formatLogic.Add(fakeFormat);

            mockFormatRepository.VerifyAll();
            Assert.AreEqual(result, fakeFormat);
        }

        [TestMethod]
        [ExpectedException(typeof(ExistingFormatException))]
        public void TestAddFormatFailsOnExistingFormat()
        {
            Mock<IFormatRepository> mockFormatRepository = new Mock<IFormatRepository>();
            IFormatManagementService formatLogic = new FormatManagementService
            {
                FormatRepository = mockFormatRepository.Object
            };

            Format fakeFormat = new Format
            {
                Name = "Normal"
            };

            mockFormatRepository
                .Setup(wl => wl.Exists("Normal"))
                .Returns(true);
            mockFormatRepository
                .Setup(wl => wl.Add(fakeFormat));

            Format result = formatLogic.Add(fakeFormat);

            mockFormatRepository.VerifyAll();
            
        }

        [TestMethod]
        public void TestDeleteFormatWorksOnExistingFormat()
        {
            Mock<IFormatRepository> mockFormatRepository = new Mock<IFormatRepository>();
            IFormatManagementService formatLogic = new FormatManagementService
            {
                FormatRepository = mockFormatRepository.Object
            };

            

            mockFormatRepository
                .Setup(wl => wl.Exists("Normal"))
                .Returns(true);
            mockFormatRepository
                .Setup(wl => wl.Delete("Normal"))
                ;

            formatLogic.Delete("Normal");

            mockFormatRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingFormatException))]
        public void TestDeleteFormatFailsOnMissingFormat()
        {
            Mock<IFormatRepository> mockFormatRepository = new Mock<IFormatRepository>();
            IFormatManagementService formatLogic = new FormatManagementService
            {
                FormatRepository = mockFormatRepository.Object
            };



            mockFormatRepository
                .Setup(wl => wl.Exists("Normal"))
                .Returns(false);
            mockFormatRepository
                .Setup(wl => wl.Delete("Normal"))
                ;

            formatLogic.Delete("Normal");

            mockFormatRepository.VerifyAll();
        }
        private IEnumerable<Format> GetFakeFormats()
        {
            List<Format> fakeList = new List<Format>
            {
                new Format
                {
                    Name = "Estilizado"
                },
                new Format
                {
                    Name = "Normal"
                },
                new Format {
                    Name = "Ancestral" 
                }
            };

            return fakeList.AsEnumerable();
        }

        
    }
}
