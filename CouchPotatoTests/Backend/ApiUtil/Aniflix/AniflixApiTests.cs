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
		public void AniflixApiTest()
		{
			AniflixApi api = new AniflixApi();
			var shows = api.getShows();
			Assert.IsNotNull(shows);
		}
	}
}