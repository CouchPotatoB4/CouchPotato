using CouchPotato.Backend.ShowUtil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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

        public TheMovieDBApi() : base("https://api.themoviedb.org/3")
        {
            getGenres();
        }

        protected override Task<HttpResponseMessage> get(string header)
        {
            return getFromPage(header, -1);
        }

        private Task<HttpResponseMessage> getFromPage(string header, int page)
        {
            var wholeQuery = new StringBuilder(query + "/" + header + KEY);
            if (page != -1)
            {
                wholeQuery.Append(HEADER_PAGE + page);
            }
            return client.GetAsync(wholeQuery.ToString());
        }

        private string getResponseBodyFromPage(string header, int page)
        {
            var content = getFromPage(header, page).Result.Content;
            return content.ReadAsStringAsync().Result;
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
                            var genre = VotableFactory.buildGenre(each.id, each.name);
                            genres.Add(genre);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Error while getting the genres.");
                    }
                }
            }
            return genres.ToArray();
        }

        public Show[] getShows()
        {
            return shows.ToArray();
        }


        public Show[] getFilteredShows(ISet<Genre> genres)
        {
            return getFilteredShows(genres, shows);
        }

        public virtual Show[] getFilteredShows(ISet<Genre> genres, IEnumerable<Show> shows)
        {
            if (genres == null) getGenres();
            if (shows.Count() == 0) getShows();

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

        public Show[] loadFilteredPage(int page, ISet<Genre> genresSet)
        {
            if (!(maxPages == -1 || maxPages > page)) throw new System.ArgumentOutOfRangeException("MaxPages: " + maxPages + " , current page: " + page);
            
            ISet<Show> localShows = new HashSet<Show>(); 

            try
            {
                var responseBody = getResponseBodyFromPage(HEADER_MOVIE_POPULAR, (page + 1));
                var showJsons = JsonConvert.DeserializeObject<ShowJsonRoot>(responseBody);
                if (maxPages == -1) maxPages = showJsons.total_pages;

                foreach (var each in showJsons.results)
                {
                    string imagePath = QUERY_IMAGES + each.poster_path;
                    var show = VotableFactory.buildShow(each.id, each.title, each.overview, imagePath);

                    foreach (int genreId in each.genre_ids)
                    {
                        foreach (var genre in genres)
                        {
                            if (genreId == genre.Id)
                            {
                                show.Genres.Add(genre);
                            }
                        }
                    }
                    localShows.Add(show);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error in getting Shows from Page: " + page);
            }

            var filteredShows = getFilteredShows(genresSet, localShows);
            ((List<Show>)shows).AddRange(filteredShows);        
            return filteredShows;
        }

        public Image getCoverForShow(int id)
        {
            return base.getCoverForShow(id, QUERY_IMAGES);
        }
    }
}
