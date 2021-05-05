using CouchPotato.Backend.ShowUtil;
using CouchPotato.Backend.ApiUtil;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace CouchPotato.Backend.ApiUtil.Aniflix
{
    public class AniflixApi : AbstractApi, IApi
    {   
        private static string SHOW = "show";
        private static string HEADER_API = "api";
        private static string HEADER_SHOW = HEADER_API + "/" + SHOW + "/index";
        private static string HEADER_GENRE = HEADER_API + "/" + SHOW + "/genres";
        private static string HEADER_STORAGE = "storage";

        public AniflixApi() : base("https://www2.aniflix.tv") 
        {
            client.DefaultRequestHeaders.Add("User-Agent", "Aniflix_App");
        }

        protected override Task<HttpResponseMessage> get(string header)
        {
            return client.GetAsync(query + "/" + header);
        }

        private string getResponseBody(string header)
        {
            var content = get(header).Result.Content;
            return content.ReadAsStringAsync().Result;
        }

        public Image getCoverForShow(int id)
        {
            foreach (Show s in shows)
            {
                if (s.Id == id)
                {
                    string url = query + "/" + HEADER_STORAGE + "/" + s.CoverPath;
                    try
                    {
                        HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                        webRequest.Headers.Add(HttpRequestHeader.Authorization, "User-Agent=Aniflix_App");

                        using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponseAsync().Result)
                        {
                            var coverStream = response.GetResponseStream();
                            return new Bitmap(coverStream);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exceptions.ApiChangedException(Provider.Aniflix, "Error in request GET/image.", e);
                    }
                }
            }

            return null;
        }

        public Show[] getShows()
        {
            if (shows == null)
            {
                if (isStatusCodeOk())
                {
                    try
                    {
                        var allShows = JsonConvert.DeserializeObject<List<ShowJson>>(getResponseBody(HEADER_SHOW));
                        shows = new Show[allShows.Count];
                    }
                    catch (Exception e)
                    {
                        throw new Exceptions.ApiChangedException(Provider.Aniflix, "Error in request GET/show.", e);
                    }
                    try
                    {
                        var genreWithShows = JsonConvert.DeserializeObject<List<GenreWithShowsJson>>(getResponseBody(HEADER_GENRE)).ToArray();
                        int showCount = 0;
                        foreach (GenreWithShowsJson gwsJson in genreWithShows)
                        {
                            var showsWithThisGenre = gwsJson.shows;
                            foreach (ShowJson sJson in showsWithThisGenre)
                            {
                                int newShow = showIsNew(sJson.id, sJson.name);
                                if (newShow == -1)
                                {
                                    shows[showCount] = VotableFactory.buildShow(sJson.id, sJson.name, sJson.description, sJson.cover_landscape);
                                    shows[showCount].AddGenre(getGenre(gwsJson.name));
                                    showCount++;
                                }
                                else
                                {
                                    shows[newShow].AddGenre(getGenre(gwsJson.name));
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exceptions.ApiChangedException(Provider.Aniflix, "Error in request GET/show.", e);
                    }
                }
            }

            return shows;
        }

        private int showIsNew(int id, string name)
        {
            for (int i = 0; i < shows.Length; i++)
            {
                Show s = shows[i];
                if (s == null) break;
                if (s.Id == id && s.Name == name) return i;
            }
            return -1;
        }

        private Genre getGenre(string name)
        {
            foreach (Genre g in genres)
            {
                if (g.Name.Equals(name)) return g;
            }
            return null;
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

        //Beginning from 0
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

        public Genre[] getGenres()
        {
            if (genres == null)
            {
                if (isStatusCodeOk())
                {
                    try
                    {
                        var genreWithShows = JsonConvert.DeserializeObject<List<GenreWithShowsJson>>(getResponseBody(HEADER_GENRE)).ToArray();

                        genres = new Genre[genreWithShows.Length];
                        for (int i = 0; i < genres.Length; i++)
                        {
                            string genre = genreWithShows[i].name;
                            genres[i] = VotableFactory.builGenre(genre);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exceptions.ApiChangedException(Provider.Aniflix, "Error in request GET/genre.", e);
                    }
                }
               
            }
            return genres;
        }
    }
}
