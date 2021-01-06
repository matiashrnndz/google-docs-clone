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
    public class UserManagementTest
    {
        [TestMethod]
        public void TestGetAllShouldCallTheRepositoryAndReturnAListOfUsers()
        {
            IEnumerable<User> fakeUsers = GetFakeUsers();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            IUserManagementService userLogic = new UserManagementService()
            {
                UserRepository = mockUserRepository.Object
            };            
            mockUserRepository
                .Setup(wl => wl.GetAll())
                .Returns(fakeUsers);

            IEnumerable<User> result = userLogic.GetAll();

            mockUserRepository.VerifyAll();
            Assert.IsNotNull(result);
            Assert.IsTrue(fakeUsers.SequenceEqual(result));
        }

        [TestMethod]
        public void TestAddUserShouldAddAnUnexistingUserToTheList()
        {
            User newUser = new User
            {
                Email = "completos@gmail.com",
                Name = "El Señor",
                LastName = "De Los Completos",
                UserName = "completoZZZ",
                Password = "mart1nl00ther",
                Administrator = false
            };
            IEnumerable<User> fakeUsers = GetFakeUsers();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            IUserManagementService userLogic = new UserManagementService()
            {
                UserRepository = mockUserRepository.Object
            };
            mockUserRepository
                .Setup(wl => wl.Add(newUser));
            mockUserRepository
                .Setup(wl => wl.Exists(newUser.Email))
                .Returns(false);

            User result = userLogic.Add(newUser);

            mockUserRepository.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(newUser, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ExistingUserException))]
        public void TestAddUserShouldNotAddExistingUserToTheList()
        {
            User newUser = new User
            {
                Email = "gerardo@gmail.com",
                Name = "El Señor",
                LastName = "De Los Completos",
                UserName = "completoZZZ",
                Password = "mart1nl00ther",
                Administrator = false
            };
            IEnumerable<User> fakeUsers = GetFakeUsers();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            IUserManagementService userLogic = new UserManagementService()
            {
                UserRepository = mockUserRepository.Object
            };
            mockUserRepository
                .Setup(wl => wl.Exists(newUser.Email))
                .Returns(true);

            User result = userLogic.Add(newUser);
            mockUserRepository.VerifyAll();           
        }

        [TestMethod]
        public void TestUpdateShouldModifyAnExistingUser()
        {
            User modifiedUser = new User
            {
                Email = "gerardo@gmail.com",
                Name = "El Señor",
                LastName = "De Los Completos",
                UserName = "completoZZZ",
                Password = "mart1nl00ther",
                Administrator = false
            };
            IEnumerable<User> fakeUsers = GetFakeUsers();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            IUserManagementService userLogic = new UserManagementService()
            {
                UserRepository = mockUserRepository.Object
            };
            mockUserRepository
                .Setup(wl => wl.Update(modifiedUser));
            mockUserRepository
                .Setup(wl => wl.Exists(modifiedUser.Email))
                .Returns(true);

            userLogic.Update(modifiedUser.Email, modifiedUser);

            mockUserRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingUserException))]
        public void TestUpdateShouldNotModifyUnexistingUser()
        {
            User modifiedUser = new User
            {
                Email = "completos@gmail.com",
                Name = "El Señor",
                LastName = "De Los Completos",
                UserName = "completoZZZ",
                Password = "mart1nl00ther",
                Administrator = false
            };
            IEnumerable<User> fakeUsers = GetFakeUsers();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            IUserManagementService userLogic = new UserManagementService()
            {
                UserRepository = mockUserRepository.Object
            };
            mockUserRepository
                .Setup(wl => wl.Exists(modifiedUser.Email))
                .Returns(false);

             userLogic.Update(modifiedUser.Email, modifiedUser);
            mockUserRepository.VerifyAll();
        }

        [TestMethod]
        public void TestGetByEmailShouldGetAnExistingUserByMail()
        {
            string userMail = "srbijastronk420@gmail.com";

            User fakeUser = new User
            {
                Email = "srbijastronk420@gmail.com",
                Name = "Miroslav",
                LastName = "Brukovic",
                UserName = "kebabremover777",
                Password = "tupac4lyfe"
            };
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            IUserManagementService userLogic = new UserManagementService()
            {
                UserRepository = mockUserRepository.Object
            };
            mockUserRepository
               .Setup(wl => wl.GetByEmail(userMail))
               .Returns(fakeUser);
            mockUserRepository
                .Setup(wl => wl.Exists(userMail))
                .Returns(true);

            User result = userLogic.GetByEmail(userMail);

            mockUserRepository.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeUser, result);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingUserException))]
        public void TestGetByEmailShouldFailIfUserDoesNotExist()
        {
            string userMail = "srbijastronk420@gmail.com";

            User fakeUser = new User
            {
                Email = "srbijastronk420@gmail.com",
                Name = "Miroslav",
                LastName = "Brukovic",
                UserName = "kebabremover777",
                Password = "tupac4lyfe"
            };
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            IUserManagementService userLogic = new UserManagementService()
            {
                UserRepository = mockUserRepository.Object
            };
            mockUserRepository
                .Setup(wl => wl.Exists(userMail))
                .Returns(false);

            User result = userLogic.GetByEmail(userMail);

            mockUserRepository.VerifyAll();
        }

        [TestMethod]
        public void TestDeleteShouldDeleteExistingUser()
        {
            string userMail = "srbijastronk420@gmail.com";

            User fakeUser = new User
            {
                Email = "srbijastronk420@gmail.com",
                Name = "Miroslav",
                LastName = "Brukovic",
                UserName = "kebabremover777",
                Password = "tupac4lyfe"
            };
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            IUserManagementService userLogic = new UserManagementService()
            {
                UserRepository = mockUserRepository.Object
            };
            mockUserRepository
               .Setup(wl => wl.Delete(userMail));
            mockUserRepository
                .Setup(wl => wl.Exists(userMail))
                .Returns(true);

            userLogic.Delete(userMail);

            mockUserRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingUserException))]
        public void TestDeleteShouldFailOnMissingUser()
        {
            string userMail = "srbijastronk420@gmail.com";

            User fakeUser = new User
            {
                Email = "srbijastronk420@gmail.com",
                Name = "Miroslav",
                LastName = "Brukovic",
                UserName = "kebabremover777",
                Password = "tupac4lyfe"
            };
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            IUserManagementService userLogic = new UserManagementService()
            {
                UserRepository = mockUserRepository.Object
            };
            mockUserRepository
               .Setup(wl => wl.Delete(userMail));
            mockUserRepository
                .Setup(wl => wl.Exists(userMail))
                .Returns(false);

            userLogic.Delete(userMail);

            mockUserRepository.VerifyAll();
        }

        private IEnumerable<User> GetFakeUsers()
        {
            List<User> fakeUsers = new List<User>
            {
                new User
                {
                    Email = "gerardo@gmail.com",
                    Name = "genardo",
                    LastName = "genardi",
                    UserName = "maestrogirardi",
                    Password = "uncleb0b0",
                    Administrator = true
                },
                new User
                {
                    Email = "benito@gmail.com",
                    Name = "benito",
                    LastName = "mussolini",
                    UserName = "ilduce",
                    Password = "elfaschista",
                    Administrator = true
                }
            };

            return fakeUsers.AsEnumerable();
        }
    }
}
