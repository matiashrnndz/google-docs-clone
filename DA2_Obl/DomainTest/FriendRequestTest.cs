using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest
{
    [TestClass]
    public class FriendRequestTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestFriendRequestSenderShouldBeNullOnCreation()
        {
            FriendRequest request = new FriendRequest();

            Assert.IsNull(request.Sender);
        }

        [TestMethod]
        public void TestFriendRequestReceiverShouldBeNullOnCreation()
        {
            FriendRequest request = new FriendRequest();

            Assert.IsNull(request.Receiver);
        }
    }
}
