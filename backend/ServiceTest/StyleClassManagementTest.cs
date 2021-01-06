using Domain;
using Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using Service;
using ServiceImp;
using System.Collections.Generic;
using System.Linq;

namespace ServiceTest
{
    [TestClass]
    public class StyleClassManagementTest
    {
        [TestMethod]
        public void TestGetAllStyleClassesCallsRepositoryGetAll()
        {
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IStyleClassManagementService styleClassLogic = new StyleClassManagementService
            {
                StyleClassRepository = mockStyleClassRepository.Object
            };

            IEnumerable<StyleClass> fakeStyleClasses = GetFakeStyleClasses();

            mockStyleClassRepository
                .Setup(wl => wl.GetAll())
                .Returns(fakeStyleClasses);

            IEnumerable<StyleClass> results = styleClassLogic.GetAll();

            mockStyleClassRepository.VerifyAll();
            Assert.IsNotNull(results);
            Assert.IsTrue(fakeStyleClasses.SequenceEqual(results));
        }

        [TestMethod]
        public void TestAddStyleClassWorksOnMissingStyleClass()
        {
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IStyleClassManagementService styleClassLogic = new StyleClassManagementService
            {
                StyleClassRepository = mockStyleClassRepository.Object
            };

            StyleClass fakeStyleClass = new StyleClass
            {
                Name = "Al Bacalao"
            };

            mockStyleClassRepository
                .Setup(wl => wl.Exists(fakeStyleClass.Name))
                .Returns(false);

            StyleClass result = styleClassLogic.Add(fakeStyleClass);

            mockStyleClassRepository.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeStyleClass, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ExistingStyleClassException))]
        public void TestAddStyleClassFailsOnExistingStyleClass()
        {
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IStyleClassManagementService styleClassLogic = new StyleClassManagementService
            {
                StyleClassRepository = mockStyleClassRepository.Object
            };

            StyleClass fakeStyleClass = new StyleClass
            {
                Name = "Al Bacalao"
            };

            mockStyleClassRepository
                .Setup(wl => wl.Exists(fakeStyleClass.Name))
                .Returns(true);

            StyleClass result = styleClassLogic.Add(fakeStyleClass);

            mockStyleClassRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(RedundantStyleClassException))]
        public void TestAddStyleClassFailsOnRedundantDependency()
        {
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IStyleClassManagementService styleClassLogic = new StyleClassManagementService
            {
                StyleClassRepository = mockStyleClassRepository.Object
            };

            StyleClass fakeStyleClass = new StyleClass
            {
                Name = "Al Bacalao"
            };
            fakeStyleClass.BasedOn = fakeStyleClass;

            mockStyleClassRepository
                .Setup(wl => wl.Exists(fakeStyleClass.Name))
                .Returns(false);

            StyleClass result = styleClassLogic.Add(fakeStyleClass);

            mockStyleClassRepository.VerifyAll();
        }

        [TestMethod]
        public void GetStyleClassWorksOnExistingStyleClass()
        {
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IStyleClassManagementService styleClassLogic = new StyleClassManagementService
            {
                StyleClassRepository = mockStyleClassRepository.Object
            };

            StyleClass fakeStyleClass = new StyleClass
            {
                Name = "Al Bacalao"
            };

            mockStyleClassRepository
                .Setup(wl => wl.Exists(fakeStyleClass.Name))
                .Returns(true);
            mockStyleClassRepository
                .Setup(wl => wl.GetByName(fakeStyleClass.Name))
                .Returns(fakeStyleClass);

            StyleClass result = styleClassLogic.GetByName(fakeStyleClass.Name);

            mockStyleClassRepository.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeStyleClass, result);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingStyleClassException))]
        public void GetStyleClassFailsOnMissingStyleClass()
        {
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IStyleClassManagementService styleClassLogic = new StyleClassManagementService
            {
                StyleClassRepository = mockStyleClassRepository.Object
            };

            StyleClass fakeStyleClass = new StyleClass
            {
                Name = "Al Bacalao"
            };

            mockStyleClassRepository
                .Setup(wl => wl.Exists(fakeStyleClass.Name))
                .Returns(false);

            StyleClass result = styleClassLogic.GetByName(fakeStyleClass.Name);

            mockStyleClassRepository.VerifyAll();

        }

        [TestMethod]
        public void DeleteStyleClassWorksOnExistingStyleClass()
        {
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IStyleClassManagementService styleClassLogic = new StyleClassManagementService
            {
                StyleClassRepository = mockStyleClassRepository.Object
            };

            StyleClass fakeStyleClass = new StyleClass
            {
                Name = "Al Bacalao"
            };

            mockStyleClassRepository
                .Setup(wl => wl.Exists(fakeStyleClass.Name))
                .Returns(true);
            mockStyleClassRepository
                .Setup(wl => wl.GetAll())
                .Returns(GetFakeStyleClasses());


            styleClassLogic.Delete(fakeStyleClass.Name);

            mockStyleClassRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingStyleClassException))]
        public void DeleteStyleClassFailsOnMissingStyleClass()
        {
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IStyleClassManagementService styleClassLogic = new StyleClassManagementService
            {
                StyleClassRepository = mockStyleClassRepository.Object
            };

            StyleClass fakeStyleClass = new StyleClass
            {
                Name = "Al Bacalao"
            };

            mockStyleClassRepository
                .Setup(wl => wl.Exists(fakeStyleClass.Name))
                .Returns(false);


            styleClassLogic.Delete(fakeStyleClass.Name);

            mockStyleClassRepository.VerifyAll();
        }

        private IEnumerable<StyleClass> GetFakeStyleClasses()
        {
            List<StyleClass> fakeList = new List<StyleClass>
        {
            new StyleClass
            {
                Name = "A U M E N T A D O"
            },
            new StyleClass
            {
                Name = "Loco"
            },
            new StyleClass {
                Name = "Medio-Antiguo"
            }
        };

            return fakeList.AsEnumerable();
        }
    }
}
