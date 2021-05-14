using CouchPotato.Backend.ApiUtil;
using CouchPotato.Backend.UserUtil;
using CouchPotatoTests.PseudoClasses;
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
		public void LobbyRuntroughTest()
		{
			User host = UserFactory.build("User");
			Lobby lobby = LobbyFactory.build(host);
			PseudoApi api = new PseudoApi();
			lobby.setConfiguration(api, 5, 5);
			Assert.AreEqual(Mode.GENRE_SELECTION, lobby.nextMode()); // New Mode: Select Genre

			for (int i = 0; i < 5; i++)
			{
				int savedVoteCount = lobby.Genres[i].Votes;
				lobby.swipeGenre(host.ID, lobby.Genres[i].Name);
				Assert.AreNotEqual(savedVoteCount, lobby.Genres[i].Votes);
			}
			int genreVoteCount = lobby.Genres[0].Votes;
			lobby.swipeGenre(host.ID, lobby.Genres[0].Name);
			Assert.AreEqual(genreVoteCount, lobby.Genres[0].Votes);

			int oldSelectedGenreCount = lobby.Genres.Length;
			Assert.AreEqual(Mode.FILM_SELECTION, lobby.nextMode()); // New Mode: Film Selection
			Assert.AreEqual(oldSelectedGenreCount, lobby.Genres.Length);

            for (int i = 0; i < 5; i++)
            {
                int savedVoteCount = lobby.Shows[i].Votes;
				lobby.swipeFilm(host.ID, lobby.Shows[i].Id);
				Assert.AreNotEqual(savedVoteCount, lobby.Shows[i].Votes);
            }
			int showVoteCount = lobby.Shows[0].Votes;
			lobby.swipeFilm(host.ID, lobby.Shows[0].Id);
			Assert.AreEqual(showVoteCount, lobby.Shows[0].Votes);

			int oldSelectedShowCount = lobby.Shows.Length;
			Assert.AreEqual(Mode.OVER, lobby.nextMode()); // New Mode: Over
			Assert.AreEqual(oldSelectedShowCount, lobby.Shows.Length);
		}

		[TestMethod()]
		public void SetConfigurationTest()
		{
			LobbyFactory.build(null).setConfiguration(Provider.Aniflix.getApi(), 5, 5);
			LobbyFactory.build(null).setConfiguration(new PseudoApi(), 5, 5);
			LobbyFactory.build(null).setConfiguration(null, -1, -1);
		}
	}
}