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
			Show showFromFactory = VotableFactory.build(1, "TestShow", "TestDescription", "TestPath");

			Assert.AreEqual(showFromConstructor.Id, showFromFactory.Id);
			Assert.AreEqual(showFromConstructor.Name, showFromFactory.Name);
			Assert.AreEqual(showFromConstructor.Description, showFromFactory.Description);
			Assert.AreEqual(showFromConstructor.CoverPath, showFromFactory.CoverPath);

			int oldVotesCount = showFromConstructor.Votes;
			showFromConstructor.Vote();
			Assert.AreNotEqual(oldVotesCount, showFromConstructor.Votes);
		}

		[TestMethod()]
		public void ShowTest2()
		{
			Show show1 = VotableFactory.build(1, "TestShow", "TestDescription", "TestPath");
			try
			{
				Show show2 = VotableFactory.build(1, "TestShow", "TestDescription", "TestPath");
			}
			catch (Exception)
			{
				// Test passed
			}
			Assert.Fail("Exception for id duplication expected");
		}
	}
}