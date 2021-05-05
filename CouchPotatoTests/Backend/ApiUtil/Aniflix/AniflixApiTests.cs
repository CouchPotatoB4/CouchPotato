using Microsoft.VisualStudio.TestTools.UnitTesting;
using CouchPotato.Backend.ApiUtil.Aniflix;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using CouchPotato.Backend.ShowUtil;

namespace CouchPotato.Backend.ApiUtil.Aniflix.Tests
{
	[TestClass()]
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
                    testGenres[i] = VotableFactory.build("Genre");
                    testShows[i] = VotableFactory.build(1, "Show", "Description", "Path");
                }

                base.genres = testGenres;
                base.shows = testShows;
            }

            internal bool isHTTPStatusCodeOk()
            {
                return base.isStatusCodeOk();
            }
        }

        [TestMethod()]
		public void GeneralApiTest()
		{
            PseudoAniflixApi filledApi = new PseudoAniflixApi(true);
            Genre[] genres = filledApi.getGenres();
            Assert.IsNotNull(genres);
            Assert.IsNotNull(filledApi.getShows());
            Assert.IsNotNull(filledApi.getShows(VotableFactory.build("Adventure")));
            Assert.IsNotNull(filledApi.getShows(0));
            Assert.IsNotNull(filledApi.getShows(new HashSet<Genre>()));
		}

        [TestMethod()]
        public void ApiConnectionTest()
        {
            PseudoAniflixApi emptyApi = new PseudoAniflixApi(false);

            if (emptyApi.isHTTPStatusCodeOk())
            {
                Assert.IsNotNull(emptyApi.getGenres());
                Assert.IsNotNull(emptyApi.getShows());
                return;
            }
            
            try
            {
                emptyApi.getGenres();
            }
            catch(Exception)
            {
                // test passed
                return;
            }
            Assert.Fail("Denied Connection Exception expected!");
        }
	}
}