using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CouchPotato.Backend.ShowUtil.Tests
{
	[TestClass()]
	public class ShowTests
	{
		[TestMethod()]
		public void ShowTest()
		{
			Show showFromConstructor = new Show(1, "TestShow", "TestDescription", "TestPath");
			Show showFromFactory = VotableFactory.buildShow(1, "TestShow", "TestDescription", "TestPath");

			Assert.AreEqual(showFromConstructor.Id, showFromFactory.Id);
			Assert.AreEqual(showFromConstructor.Name, showFromFactory.Name);
			Assert.AreEqual(showFromConstructor.Description, showFromFactory.Description);
			Assert.AreEqual(showFromConstructor.CoverPath, showFromFactory.CoverPath);

			int oldVotesCount = showFromConstructor.Votes;
			showFromConstructor.Vote();
			Assert.AreNotEqual(oldVotesCount, showFromConstructor.Votes);

            Genre genre = VotableFactory.buildGenre("TestGenre");
            showFromFactory.AddGenre(genre);
			Assert.IsTrue(showFromFactory.Genres.Contains(genre));

		}

		[TestMethod()]
		public void DuplicateIdTest()
		{
			Show show1 = VotableFactory.buildShow(1, "TestShow", "TestDescription", "TestPath");
			try
			{
				Show show2 = VotableFactory.buildShow(1, "TestShow", "TestDescription", "TestPath");
				Assert.Fail("Exception for id duplication expected");
			}
			catch (Exception)
			{
				// Test passed
			}
		}
	}
}