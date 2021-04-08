using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace CouchPotato.ApiUtil.Aniflix
{
    using Show = CouchPotato.ShowUtil.Show;
    using ShowFactory = CouchPotato.ShowUtil.ShowFactory;

    class AniflixApi : AbstractApi, IApi
    {
        private static string SHOW = "show";
        private static string HEADER_API = "api";
        private static string HEADER_SHOW = HEADER_API + "/" + SHOW + "/index";
        private static string HEADER_GENRE = HEADER_API + "/" + SHOW + "/genres";
        private static string HEADER_STORAGE = "storage";

        private string[] genres = null;
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

        public Image getCoverForShow(int id)
        {
            foreach (Show s in shows)
            {
                if (s.Id == id)
                {
                    HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(query + "/" + HEADER_STORAGE + "/" + s.CoverStorage);
                    using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponseAsync().Result)
                    {
                        var coverStream = response.GetResponseStream();
                        return new Bitmap(coverStream);
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
                    var encrypted = JsonConvert.DeserializeObject<List<ShowJson>>(getResponseBody(HEADER_SHOW));

                    shows = new Show[encrypted.Count];
                    for (int i = 0; i < shows.Length; i++)
                    {
                        ShowJson show = encrypted[i];
                        shows[i] = ShowFactory.build(show.id, show.name, show.description, show.cover_landscape);
                    }
                }
            }

            return shows;
        }

        public Show[] getShows(IList<string> genres)
        {
            if (genres == null || genres.Count == 1 && genres[0] == null)
            {
                getGenres();
            }

            if (isStatusCodeOk())
            {
                ISet<Show> showSet = new HashSet<Show>();
                foreach (string genre in genres)
                {
                    foreach (var swg in genreWithShows)
                    {
                        if (genre.Equals(swg.name))
                        {
                            var list = swg.shows;

                            foreach (var l in list)
                            {
                                showSet.Add(ShowFactory.build(l.id, l.name, l.description, l.cover_landscape));
                            }
                        }
                    }
                }
                if (showSet.Count > 0) shows = showSet.ToArray();

                return shows;
            }

            return new Show[0];
        }

        public Show[] getShows(string genre)
        {
            return getShows(new string[] { genre });
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

        public string[] getGenres()
        {
            if (genres == null)
            {
                if (isStatusCodeOk())
                {
                    genreWithShows = JsonConvert.DeserializeObject<List<GenreWithShowsJson>>(getResponseBody(HEADER_GENRE)).ToArray();

                    genres = new string[genreWithShows.Length];
                    for (int i = 0; i < genres.Length; i++)
                    {
                        string genre = genreWithShows[i].name;
                        genres[i] = genre;
                    }
                }
               
            }
            return genres;
        }
    }
}
