using Microsoft.VisualStudio.TestTools.UnitTesting;
using CouchPotato.Backend.LobbyUtil;
using CouchPotato.Backend.UserUtil;

namespace CouchPotato_TestProject
{
    [TestClass]
    public class BackendTests
    {
        [TestMethod]
        public void UserTests()
        {
            User user1 = UserFactory.build("User");
            User user2 = UserFactory.build("User");

            Assert.IsNotNull(user1.ID);
            Assert.IsNotNull(user1.Name);

            Assert.AreNotEqual(user1.ID, user2.ID);
            Assert.AreNotEqual(user1, user2);
        }

        [TestMethod]
        public void LobbyTests()
        {
            User user1 = UserFactory.build("User");
            Lobby lobby1 = LobbyFactory.build(user1);
            Lobby lobby2 = LobbyFactory.build(user1);

            Assert.IsNotNull(lobby1.getUser(user1.ID));
            Assert.AreNotEqual(lobby1, lobby2);
            Assert.AreEqual(lobby1.getUser(user1.ID), user1);
            Assert.AreEqual(lobby1.getUser(user1.ID), lobby2.getUser(user1.ID));
        }
    }
}
