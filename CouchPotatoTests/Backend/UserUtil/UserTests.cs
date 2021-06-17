using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CouchPotato.Backend.UserUtil.Tests
{
	[TestClass]
	public class UserTests
	{
		[TestMethod]
		public void UserTest()
		{
			User user = UserFactory.build("User");
			Assert.AreEqual("User", user.Name);
			Assert.IsFalse(user.swipe());
			user.Swipes = 5;
			int oldSwipeCount = user.Swipes;
			Assert.IsTrue(user.swipe());
			Assert.AreNotEqual(oldSwipeCount, user.Swipes);
		}
	}
}