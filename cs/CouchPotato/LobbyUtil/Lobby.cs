using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CouchPotato.ApiUtil;

namespace CouchPotato.LobbyUtil
{
    using User = UserUtil.User;
    using Provider = ApiUtil.Provider;
    using Show = ShowUtil.Show;

    class Lobby
    {
        private long id;
        private ISet<User> users = new HashSet<User>();
        private User host;
        private Provider provider;
        private ISet<Show> shows = new HashSet<Show>();
        private bool[,] votes;
        private string[] genres;
        private Mode mode;
        private int swipes, genresSwipes;

        public Lobby(User host, long id)
        {
            this.host = host;
            this.id = id;
        }

        public void addUser(User user)
        {
            users.Add(user);
        }

        public User getUser(long id)
        {
            foreach (User u in users)
            {
                if (u.ID == id) return u;
            }
            if (host.ID == id) return host;
            return null;
        }

        public void setConfiguration(Provider? provider, int swipes, int genresCount)
        {
            if (provider != null) this.provider = (Provider)provider;

            if (swipes > 0) this.swipes = swipes;

            if (genresCount > 0)
            {
                genres = new string[genresCount];
                this.genresSwipes = genresCount;
            }
        }
       
        public Image getCoverForShow(int id)
        {
            foreach (Show show in shows)
            {
                if (show.Id == id) return provider.getApi().getCoverForShow(id);
            }
            return null;
        }

        public void nextMode()
        {
            if (mode == Mode.JOIN)
            {
                mode = Mode.GENRE_SELECTION;
                provider.getApi().getGenres();
                setUserSwipes(genresSwipes);
            }
            else if (mode == Mode.GENRE_SELECTION)
            {
                genres = getGenreResult();
                provider.getApi().getShows(genres);
                mode = Mode.FILM_SELECTION;
                setUserSwipes(swipes);
            }
                
            createVoteArray();
        }

        private void createVoteArray()
        {
            int width = users.Count + 1;
            int height = 0;

            if (mode == Mode.FILM_SELECTION) height = shows.Count; 
            else height = (provider.getApi().getGenres() != null) ? provider.getApi().getGenres().Length : 0;

            votes = new bool[width, height];
        }

        private void setUserSwipes(int swipes)
        {
            foreach (User u in users)
            {
                u.Swipes = swipes;
            }
            host.Swipes = swipes;
        }

        public bool isOpen()
        {
            return mode == Mode.JOIN;
        }

        public string[] Genre
        {
            get { return provider.getApi().getGenres(); }
        }

        public ISet<Show> Shows
        {
            get { return shows; }
        }

        public void loadPage(int page)
        {
            if (mode == Mode.FILM_SELECTION)
            {
                foreach (Show s in provider.getApi().getShows(page))
                {
                    shows.Add(s);
                }
            }
        }


        public Show getNextShow(int number)
        {
            if (number >= shows.Count)
            {
                int page = number / ApiConstants.PAGE_SIZE;
                loadPage(page);
            }
            return shows.ElementAt(number);
        }


        public void swipeGenre(long userId, string genre)
        {          
            if (mode == Mode.GENRE_SELECTION)
            {
                if (getUser(userId).swipe())
                {
                    int row = 0;
                    foreach (string g in provider.getApi().getGenres())
                    {
                        if (g.Equals(genre)) break;

                        row++;
                    }

                    int column = getUserNumberInSet(userId);

                    votes[column, row] = true;
                }
            }
        }


        public void swipeFilm(long userId, int showId)
        {
            if (mode == Mode.FILM_SELECTION)
            {
                foreach (Show s in shows)
                {
                    if (s.Id == showId)
                    {
                        if (getUser(userId).swipe())
                        {
                            s.vote();
                            break;
                        }
                    }
                }
            }
        }

        private int getUserNumberInSet(long userId)
        {
            int column = 0;
            foreach (User user in users)
            {
                if (user.ID == userId) break;

                column++;
            }
            return column;
        }

        private string[] getGenreResult()
        {
            IDictionary<int, int> dicitonary = new Dictionary<int, int>();

            string[] allGenres = provider.getApi().getGenres();

            for (int r = 0; r < allGenres.Length; r++)
            {
                int count = 0;

                for (int c = 0; c < users.Count + 1; c++)
                {
                    if (votes[c, r]) count++;       
                }

                dicitonary.Add(r, count);
            }

            KeyValuePair<int, int>[] sortedDictionary = sortDicitonary(dicitonary);

            string[] result = new string[genres.Length];

            for (int i = 0; i < result.Length; i++)
            {
                int j = sortedDictionary[i].Key;
                result[i] = allGenres[j];
            } 

            return result;
        }

        private KeyValuePair<int, int>[] sortDicitonary(IDictionary<int, int> dictionary)
        {
            KeyValuePair<int, int>[] sorted = dictionary.AsEnumerable().ToArray();

            int offset = 1;
            for (int j = 0; j < sorted.Length - 1; j++)
            {
                for (int i = offset; i < sorted.Length - 1; i++)
                {
                    if (sorted[i].Value > sorted[i - 1].Value)
                    {
                        var btw = sorted[i - 1];
                        sorted[i - 1] = sorted[i];
                        sorted[i] = btw;
                    }
                }
                offset++;
            }

            return sorted;
        }
    }
}
