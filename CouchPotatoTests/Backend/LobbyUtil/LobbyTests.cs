using CouchPotato.Backend.UserUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CouchPotato.Backend.LobbyUtil.Tests
{
	[TestClass()]
	public class LobbyTests
	{
		[TestMethod()]
		public void getUserTest()
		{
			User hostUser = UserFactory.build("User");
			User user2 = UserFactory.build("AnotherUser");
			User user3 = UserFactory.build("FinalUser");
			Lobby lobby1 = LobbyFactory.build(hostUser);

			lobby1.addUser(user2);
			lobby1.addUser(user3);
			ISet<User> users = lobby1.getUser();

			Assert.IsNotNull(users);
			//Assert.IsTrue(users.Contains(hostUser)); TODO
			Assert.IsTrue(users.Contains(user2));
			Assert.IsTrue(users.Contains(user3));
		}

		[TestMethod()]
		public void getUserTest1()
		{
			User user1 = UserFactory.build("User");
			Lobby lobby1 = LobbyFactory.build(user1);
			Lobby lobby2 = LobbyFactory.build(user1);

			Assert.IsNotNull(lobby1.getUser(user1.ID));
			Assert.AreNotEqual(lobby1, lobby2);
			Assert.AreEqual(lobby1.getUser(user1.ID), user1);
			Assert.AreEqual(lobby1.getUser(user1.ID), lobby2.getUser(user1.ID));

			User user2 = UserFactory.build("User2");
			lobby1.addUser(user2);
			long invalidID = user1.ID + user2.ID;
			Assert.IsNull(lobby1.getUser(invalidID));
		}

		[TestMethod()]
		public void LobbyTest()
		{
			Lobby lobby = new Lobby(null, 0);
			Assert.IsNotNull(lobby);
		}

		[TestMethod()]
		public void setConfigurationTest()
		{
			LobbyFactory.build(null).setConfiguration(ApiUtil.Provider.Aniflix, 5, 5);
			LobbyFactory.build(null).setConfiguration(null, -1, -1);
		}
	}
}