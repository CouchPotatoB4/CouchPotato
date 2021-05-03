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
			Show showFromFactory = ShowFactory.build(1, "TestShow", "TestDescription", "TestPath");

			Assert.AreEqual(showFromConstructor.Id, showFromFactory.Id);
			Assert.AreEqual(showFromConstructor.Name, showFromFactory.Name);
			Assert.AreEqual(showFromConstructor.Description, showFromFactory.Description);
			Assert.AreEqual(showFromConstructor.CoverStorage, showFromFactory.CoverStorage);

			int oldVotesCount = showFromConstructor.getVotes();
			showFromConstructor.vote();
			Assert.AreNotEqual(oldVotesCount, showFromConstructor.getVotes());
		}

		[TestMethod()]
		public void ShowTest2()
		{
			Show show1 = ShowFactory.build(1, "TestShow", "TestDescription", "TestPath");
			try
			{
				Show show2 = ShowFactory.build(1, "TestShow", "TestDescription", "TestPath");
			}
			catch (Exception)
			{
				// Test passed
			}
			Assert.Fail("Exception for id duplication expected");
		}
	}
}