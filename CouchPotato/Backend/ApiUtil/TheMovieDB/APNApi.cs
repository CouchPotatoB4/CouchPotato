using CouchPotato.Backend.ApiUtil.TheMovieDB;
using CouchPotato.Backend.ShowUtil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouchPotato.Backend.ApiUtil.TheMovieDB
{
    public class APNApi : TheMovieDBApi
    {
        private const string HEADER_MOVIE = "/movie/";
        private const string HEADER_PROVIDER = "/watch/providers";
        public const string AMAZON_PRIME = "Amazon Prime";
        public const string NETFLIX = "Netflix";
        private string provider;

        public APNApi(string provider) : base()
        {
            this.provider = provider;
        }

        public override Show[] getFilteredShows(ISet<Genre> genres, IEnumerable<Show> shows)
        {
            var filteredShows = base.getFilteredShows(genres, shows);
            var localShows = new List<Show>();

            try
            {
                foreach (var show in filteredShows)
                {
                    var responseBody = getResponseBody(HEADER_MOVIE + show.Id + HEADER_PROVIDER);
                    var providerJsonRoot = JsonConvert.DeserializeObject<ProviderJsonRoot>(responseBody);
                    var de = providerJsonRoot.results.DE;
                    if (de != null && flatrateContainsProvider(de.flatrate))
                    {
                        localShows.Add(show);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Can't get the Provider.");
            }

            return localShows.ToArray();
        }

        private bool flatrateContainsProvider(RBFJson[] flatrate)
        {
            if (flatrate == null) return false;
            foreach (var rbf in flatrate)
            {
                Console.WriteLine(rbf.provider_name);
                if (rbf.provider_name.Contains(provider))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
