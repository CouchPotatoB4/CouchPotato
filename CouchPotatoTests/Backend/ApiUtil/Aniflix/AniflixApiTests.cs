using Microsoft.VisualStudio.TestTools.UnitTesting;
using CouchPotato.Backend.ApiUtil.Aniflix;
using System;
using System.Collections.Generic;
using System.Text;

namespace CouchPotato.Backend.ApiUtil.Aniflix.Tests
{
	[TestClass()]
	public class AniflixApiTests
	{
		[TestMethod()]
		public void AniflixApiConnectionTest()
		{
			AniflixApi api = new AniflixApi();
			Assert.AreEqual(200, api.getStatusCode());
			Assert.IsNotNull(api.getGenres());
			Assert.IsNotNull(api.getShows());
		}
	}
}