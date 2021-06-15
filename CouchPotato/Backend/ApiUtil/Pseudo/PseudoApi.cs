using CouchPotato.Backend.ApiUtil;
using CouchPotato.Backend.ShowUtil;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.Backend.ApiUtil
{
    public class PseudoApi : AbstractApi, IApi
    {

        public PseudoApi() : base("")
        {
            genres = new Genre[10];
            for (int i = 0; i < 10; i++)
            {
                genres[i] = VotableFactory.buildGenre("Genre" + i);
            }

            shows = new Show[50];
            for (int i = 0; i < 50; i++)
            {
                Show show = VotableFactory.buildShow(i, "Show" + i, "Description", "https://assets.onlinepianist.com/songs/artists/rick_astley_bg.jpg");
                show.AddGenre(genres[i % 10]);
                shows[i] = show;
            }
        }

        public Image getCoverForShow(int id)
        {
            return Image.FromHbitmap(IntPtr.Zero);
        }

        public Genre[] getGenres()
        {
            return genres;
        }

        public Show[] getShows()
        {
            return shows;
        }

        public Show[] getShows(Genre genre)
        {
            List<Show> filteredShows = new List<Show>();
            foreach (var show in shows)
            {
                if (show.Genres.Contains(genre))
                {
                    filteredShows.Add(show);
                }
            }
            return filteredShows.ToArray();
        }

        public Show[] getShows(ISet<Genre> genres)
        {
            List<Show> filteredShows = new List<Show>();
            foreach (var show in shows)
            {
                if (show.Genres.Contains(genres.GetEnumerator().Current))
                {
                    filteredShows.Add(show);
                }
            }
            return filteredShows.ToArray();
        }

        public Show[] getShows(int page)
        {
            Show[] resultArray = new Show[10];
            for (int i = 0; i < 10; i++)
            {
                resultArray[i] = shows[i];
            }
            return resultArray;
        }

        protected override Task<HttpResponseMessage> get(string header)
        {
            return null;
        }
    }
}
