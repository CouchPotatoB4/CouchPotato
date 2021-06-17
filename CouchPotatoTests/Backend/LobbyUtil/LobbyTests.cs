using CouchPotato.Backend.ApiUtil;
using CouchPotato.Backend.ShowUtil;
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
			ISet<User> users = lobby1.getAllUsers();

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


			HashSet<Genre> votedGenres = new HashSet<Genre>();
			for (int i = 0; i < 5; i++)
			{
                var genre = lobby.Genres[i];
                int currentSwipeCount = lobby.getSwipesForGenre(genre);
				Assert.AreEqual(0, currentSwipeCount);
				lobby.swipeGenre(host.ID, genre.Name);
				Assert.AreNotEqual(currentSwipeCount, lobby.getSwipesForGenre(genre)); // Vote count should be updated & not equal to old count
				votedGenres.Add(genre);
			}

            Genre invalidVotedGenre = lobby.Genres[9];
            int genreVoteCount = lobby.getSwipesForGenre(invalidVotedGenre);
			lobby.swipeGenre(host.ID, invalidVotedGenre.Name);
            Assert.AreEqual(genreVoteCount, lobby.getSwipesForGenre(invalidVotedGenre)); // Vote count should not modify with swiping after reaching max swipe count 

			int oldSelectedGenreCount = lobby.Genres.Length;
			Assert.AreEqual(Mode.FILM_SELECTION, lobby.nextMode()); // New Mode: Film Selection
			Assert.AreNotEqual(oldSelectedGenreCount, lobby.Genres.Length); // Genre-Voting should be evaluated, therefore genre count must modify

			HashSet<Show> votedShows = new HashSet<Show>();
            for (int i = 0; i < 5; i++)
            {
                Show show = lobby.getNextShow(i);
                int savedVoteCount = lobby.getSwipesForShow(show);
				lobby.swipeFilm(host.ID, show.Id);
                Assert.AreNotEqual(savedVoteCount, lobby.getSwipesForShow(show)); // Vote count should be updated & not equal to old count
				votedShows.Add(show);
            }

			Show invalidVotedShow = lobby.getNextShow(6);
            int showVoteCount = lobby.getSwipesForShow(invalidVotedShow);
			lobby.swipeFilm(host.ID, invalidVotedShow.Id);
            Assert.AreEqual(showVoteCount, lobby.getSwipesForShow(invalidVotedShow)); // Vote count should not modify with swiping after reaching max swipe count 

			int oldSelectedShowCount = lobby.Shows.Length;
			Assert.AreEqual(Mode.OVER, lobby.nextMode()); // New Mode: Over
			Assert.AreNotEqual(oldSelectedShowCount, lobby.Shows.Length);  // Show-Voting should be evaluated, therefore genre count must modify

            foreach (var votedGenre in votedGenres)
            {
				Assert.IsTrue(lobby.getGenreResults().ContainsKey(votedGenre));
            }

			foreach (var votedShow in votedShows)
			{
				Assert.IsTrue(lobby.getShowResults().ContainsKey(votedShow));
			}
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