using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CouchPotato.ApiUtil;

namespace CouchPotato.LobbyUtil
{
    using User = CouchPotato.UserUtil.User;
    using Provider = CouchPotato.ApiUtil.Provider;
    using Film = CouchPotato.FilmUtil.Film;

    class Lobby
    {
        private long id;
        private ISet<User> users = new HashSet<User>();
        private User host;
        private Provider provider;
        private ISet<Film> films = new HashSet<Film>();
        private bool[,] votes;
        private string genre;
        private Mode mode;

        public Lobby(User host, long id)
        {
            this.host = host;
            this.id = id;
        }

        public void addUser(User user)
        {
            users.Add(user);
        }

        public void setConfiguration(Provider? provider, int swipes)
        {
            if (provider != null) this.provider = (Provider)provider;
            if (swipes > 0)
            {
                foreach (User user in users)
                {
                    user.resetSwipes(swipes);
                }
                host.resetSwipes(swipes);
            }
        }
       
        public Image getCoverForFilm(long id)
        {
            foreach (Film film in films)
            {
                if (film.ID == id) return provider.getApi().getCoverForFilm(id);
            }
            return null;
        }

        public void nextMode()
        {
            if (mode == Mode.JOIN) mode = Mode.GENRE_SELECTION;
            else if (mode == Mode.GENRE_SELECTION) mode = Mode.FILM_SELECTION;

            createVoteArray();
        }

        private void createVoteArray()
        {
            int width = users.Count;
            int height = 0;

            if (mode == Mode.FILM_SELECTION) height = provider.getApi().getGenres().Length;
            else height = provider.getApi().getFilms(genre).Length;

            votes = new bool[width, height];
        }

        public bool isOpen()
        {
            return mode == Mode.JOIN;
        }

        public string[] getGenre()
        {
            return provider.getApi().getGenres();
        }

        public ISet<Film> Films
        {
            get { return films; }
        }


        public void swipeGenre(long userId, string genre)
        {
            if (mode == Mode.GENRE_SELECTION)
            {
                int row = 0;
                foreach(string g in provider.getApi().getGenres())
                {
                    if (g.Equals(genre)) break;

                    row++;
                }

                int column = getUserNumberInSet(userId);

                votes[column, row] = true;
            }
        }


        public void swipeFilm(long userId, long filmId)
        {
            if (mode == Mode.FILM_SELECTION)
            {
                int row = 0;
                if (films.Count == 0)
                {
                    foreach (Film f in provider.getApi().getFilms(genre))
                    {
                        if (f.ID == filmId) break;

                        row++;
                    }
                }
                else
                {
                    foreach (Film f in films)
                    {
                        if (f.ID == filmId) break;

                        row++;
                    }
                }

                int column = getUserNumberInSet(userId);

                votes[column, row] = true;
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

        private string getResult()
        {
            //TODO
            return null;
        }
    }
}
