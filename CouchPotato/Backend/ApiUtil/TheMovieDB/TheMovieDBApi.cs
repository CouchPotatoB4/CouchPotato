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
        private const string QUERY_IMAGES = "https://image.tmdb.org/t/p/w500";

        private IDictionary<int, Genre> genres = new SortedDictionary<int, Genre>();

        public TheMovieDBApi() : base("https://api.themoviedb.org/3")
        {

        }

        protected override Task<HttpResponseMessage> get(string header)
        {
            return client.GetAsync(query + "/" + header + KEY);
        }

        public Image getCoverForShow(int id)
        {
            foreach (Show s in shows)
            {
                if (s.Id == id)
                {
                    string url = QUERY_IMAGES + "/" + s.CoverPath;
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
                        var genreJsons = JsonConvert.DeserializeObject<List<GenreJson>>(responseBody);

                        foreach (var each in genreJsons)
                        {
                            var genre = VotableFactory.buildGenre(each.name);
                            genres.Add(each.id, genre);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return genres.Values.ToArray<Genre>();
        }

        public Show[] getShows()
        {
            if (shows == null)
            {
                if (isStatusCodeOk())
                {
                    try
                    {
                        var responseBody = getResponseBody(HEADER_MOVIE_POPULAR);
                        var showJsons = JsonConvert.DeserializeObject<List<ShowJson>>(responseBody);

                        shows = new Show[showJsons.Count];

                        int i = 0;
                        foreach (var each in showJsons)
                        {
                            var show = VotableFactory.buildShow(each.id, each.title, each.overview, each.poster_path);
                            foreach (int key in each.genre_ids)
                            {
                                show.AddGenre(genres[key]);
                            }
                            shows[i] = show;
                            i++;
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return shows;
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
            if (shows == null) getShows();

            Show[] localShows = new Show[ApiConstants.PAGE_SIZE];

            int start = page * ApiConstants.PAGE_SIZE;
            int end = start + ApiConstants.PAGE_SIZE;

            if (start < shows.Length)
            {
                end = end < shows.Length ? end : shows.Length;

                for (int i = start; i < end; i++)
                {
                    localShows[i - start] = shows[i];
                }
            }

            return localShows;
        }
    }
}
