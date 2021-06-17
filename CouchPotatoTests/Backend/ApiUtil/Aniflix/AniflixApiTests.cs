using Microsoft.VisualStudio.TestTools.UnitTesting;
using CouchPotato.Backend.ApiUtil.Aniflix;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using CouchPotato.Backend.ShowUtil;

namespace CouchPotato.Backend.ApiUtil.Aniflix.Tests
{
	[TestClass]
	public class AniflixApiTests
	{
        private class PseudoAniflixApi : AniflixApi
        {
			internal PseudoAniflixApi(Boolean fillWithPseudoData) : base()
            {
                Genre[] testGenres = new Genre[10];
				Show[] testShows = new Show[10];
                for (int i = 0; i < 10; i++)
                {
                    testGenres[i] = VotableFactory.buildGenre("Genre");
                    testShows[i] = VotableFactory.buildShow(1, "Show", "Description", "Path");
                }

                base.genres = testGenres;
                base.shows = testShows;
            }

            internal bool isHTTPStatusCodeOk()
            {
                return base.isStatusCodeOk();
            }
        }

        [TestMethod]
        public void ApiConnectionTest()
        {
            PseudoAniflixApi emptyApi = new PseudoAniflixApi(false);

            try
            {
                if (emptyApi.isHTTPStatusCodeOk())
                {
                    Assert.IsNotNull(emptyApi.getGenres());
                    Assert.IsNotNull(emptyApi.getShows());
                    return;
                }
                Assert.Fail("Denied Connection Exception expected!");
            }
            catch(Exceptions.DeniedConnectionException e)
            {
                Assert.Fail("No Connection to API - Request Answer: " + e.Message);
            }
        }
	}
}