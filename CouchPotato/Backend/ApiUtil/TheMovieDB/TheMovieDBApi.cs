using CouchPotato.Backend.ShowUtil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CouchPotato.Backend.ApiUtil.TheMovieDB
{
    public class TheMovieDBApi : AbstractApi, IApi
    {
        private const string KEY = "?api_key=0333faa2caaf38355723eef129425e6c";
        private const string HEADER_GENRE = "genre/movie/list";
        private const string HEADER_MOVIE_POPULAR = "movie/popular";
        private const string QUERY_IMAGES = "https://image.tmdb.org/t/p/w500/";
        private const string HEADER_PAGE = "&page=";
        private int maxPages = -1;

        private IDictionary<int, Genre> genres = new SortedDictionary<int, Genre>();

        public TheMovieDBApi() : base("https://api.themoviedb.org/3")
        {

        }

        protected override Task<HttpResponseMessage> get(string header)
        {
            string wholeQuery = query + "/" + header + KEY;
            return client.GetAsync(wholeQuery);
        }

        private Task<HttpResponseMessage> getFromPage(string header, int page)
        {
            string wholeQuery = query + "/" + header + KEY + HEADER_PAGE + page;
            return client.GetAsync(wholeQuery);
        }

        private string getResponseBodyFromPage(string header, int page)
        {
            var content = getFromPage(header, page).Result.Content;
            return content.ReadAsStringAsync().Result;
        }

        public Image getCoverForShow(int id)
        {
            foreach (Show s in shows)
            {
                if (s.Id == id)
                {
                    string url = QUERY_IMAGES + s.CoverPath;
                    try
                    {
                        HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);

                        using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponseAsync().Result)
                        {
                            var coverStream = response.GetResponseStream();
                            return new Bitmap(coverStream);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Can't load Image.");
                    }
                }
            }

            return null;
        }

        public Genre[] getGenres()
        {
            if (genres.Count == 0)
            {
                if (isStatusCodeOk())
                {
                    try
                    {
                        var responseBody = getResponseBody(HEADER_GENRE);
                        var genreJsons = JsonConvert.DeserializeObject<GenreJsonRoot>(responseBody);

                        foreach (var each in genreJsons.genres)
                        {
                            var genre = VotableFactory.buildGenre(each.name);
                            genres.Add(each.id, genre);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Error while getting the genres.");
                    }
                }
            }
            return genres.Values.ToArray<Genre>();
        }

        public Show[] getShows()
        {
            return shows.ToArray<Show>();
        }


        public Show[] getShows(ISet<Genre> genres)
        {
            if (genres == null) getGenres();
            if (shows == null) getShows();

            ISet<Show> showSet = new HashSet<Show>();
            foreach (Show s in shows)
            {
                foreach (Genre g in s.Genres)
                {
                    if (genres.Contains(g))
                    {
                        showSet.Add(s);
                        break;
                    }
                }
            }
            return showSet.ToArray();
        }

        public Show[] getShows(Genre genre)
        {
            ISet<Genre> genres = new HashSet<Genre>();
            genres.Add(genre);
            return getShows(genres);
        }

        public Show[] getShows(int page)
        {
            if (!(maxPages == -1 || maxPages > page)) throw new System.ArgumentOutOfRangeException("MaxPages: " + maxPages + " , current page: " + page);

            IList<Show> localShows = new List<Show>();

            try
            {
                var responseBody = getResponseBodyFromPage(HEADER_MOVIE_POPULAR, (page + 1));
                var showJsons = JsonConvert.DeserializeObject<ShowJsonRoot>(responseBody);
                if (maxPages == -1) maxPages = showJsons.total_pages;

                foreach (var each in showJsons.results)
                {
                    string imagePath = QUERY_IMAGES + each.poster_path;
                    var show = VotableFactory.buildShow(each.id, each.title, each.overview, imagePath);
                    foreach (int genreid in each.genre_ids)
                    {
                        show.Genres.Add(genres[genreid]);
                    }
                    localShows.Add(show);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error in getting Shows from Page: " + page);
            }

            return localShows.ToArray<Show>();
        }
    }
}
