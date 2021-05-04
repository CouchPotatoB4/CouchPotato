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

        private Genre[] genres;
        private Show[] shows;

        private GenreWithShowsJson[] genreWithShows;

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

        //TODO
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
                        var encrypted = JsonConvert.DeserializeObject<List<ShowJson>>(getResponseBody(HEADER_SHOW));

                        shows = new Show[encrypted.Count];
                        for (int i = 0; i < shows.Length; i++)
                        {
                            ShowJson show = encrypted[i];
                            shows[i] = VotableFactory.build(show.id, show.name, show.description, show.cover_landscape);
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

        public Show[] getShows(ISet<Genre> genres)
        {
            if (genres == null)
            {
                getGenres();
            }

            if (isStatusCodeOk())
            {
                ISet<Show> showSet = new HashSet<Show>();
                foreach (Genre genre in genres)
                {
                    foreach (var swg in genreWithShows)
                    {
                        string gn = genre.Name;
                        if (gn.Equals(swg.name))
                        {
                            var list = swg.shows;

                            foreach (var l in list)
                            {
                                showSet.Add(VotableFactory.build(l.id, l.name, l.description, l.cover_landscape));
                            }
                        }
                    }
                }
                if (showSet.Count > 0) shows = showSet.ToArray();

                return shows;
            }

            return new Show[0];
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
            if (shows != null)
            {
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

            return new Show[0];
        }

        public Genre[] getGenres()
        {
            if (genres == null)
            {
                if (isStatusCodeOk())
                {
                    try
                    {
                        genreWithShows = JsonConvert.DeserializeObject<List<GenreWithShowsJson>>(getResponseBody(HEADER_GENRE)).ToArray();

                        genres = new Genre[genreWithShows.Length];
                        for (int i = 0; i < genres.Length; i++)
                        {
                            string genre = genreWithShows[i].name;
                            genres[i] = VotableFactory.build(genre);
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
