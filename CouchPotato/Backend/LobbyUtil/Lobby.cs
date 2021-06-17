using CouchPotato.Backend.UserUtil;
using CouchPotato.Backend.ShowUtil;
using CouchPotato.Backend.ApiUtil;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CouchPotato.Backend.LobbyUtil
{
    public class Lobby
    {
        private long id;
        private User host;
        private ISet<User> users = new HashSet<User>();
        private IDictionary<int, (Show, int)> selectedShows = new Dictionary<int, (Show, int)>();
        private IDictionary<int, (Genre, int)> selectedGenres = new Dictionary<int, (Genre, int)>();
        private VotingEvaluation evaluation = new VotingEvaluation();
        private IApi providerApi;
        private Mode mode;
        private int sSwipes, gSwipes, highestPage;

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
                if (u.ID == id)
                {
                    return u;
                }
            }
            if (host.ID == id)
            {
                return host;
            }
            return null;
        }

        public Mode GetMode()
        {
            return mode;
        }

        public ISet<User> getAllUsers()
        {
            ISet<User> allUser = new HashSet<User>();
            allUser.Add(host);
            foreach (User u in users)
            {
                allUser.Add(u);
            }
            return allUser;
        }

        public User getHost()
        {
            return host;
        }

        public void setConfiguration(IApi api, int sSwipes, int gSwipes)
        {
            if (api != null) this.providerApi = api;

            if (sSwipes > 0) this.sSwipes = sSwipes;

            if (gSwipes > 0) this.gSwipes = gSwipes;
        }

        public int GenreSwipes
        {
            get { return gSwipes; }
        }

        public int Swipes
        {
            get { return sSwipes; }
        }

        public long ID
        {
            get { return id; }
        }

        public Image getCoverForShow(int id)
        {
            if (selectedShows.ContainsKey(id))
            {
                return providerApi.getCoverForShow(id);
            }
            return null;
        }

        public Mode nextMode()
        {
            if (mode == Mode.JOIN)
            {
                mode = Mode.GENRE_SELECTION;
                AddNewGenres(providerApi.getGenres());
                setUserSwipes(gSwipes);
                setUserUnready();
            }
            else if (mode == Mode.GENRE_SELECTION)
            {
                mode = Mode.FILM_SELECTION;
                evaluation.evaluateGenre(selectedGenres, EvaluationType.HIGHEST);

                loadNextPage();
                setUserSwipes(sSwipes);
                setUserUnready();
            }
            else if (mode == Mode.FILM_SELECTION)
            {
                evaluation.evaluateShow(selectedShows, EvaluationType.HIGHEST);
                mode = Mode.OVER;
            }
            return mode;
        }

        private void setUserSwipes(int swipes)
        {
            foreach (User u in users)
            {
                u.Swipes = swipes;
            }
            host.Swipes = swipes;
        }

        private void setUserUnready()
        {
            foreach (User u in users)
            {
                u.Ready = false;
            }
            host.Ready = false;
        }

        public bool isOpen()
        {
            return mode == Mode.JOIN;
        }

        public Genre[] Genres
        {
            get { return getGenresFromDictionary().ToArray(); }
        }

        private ISet<Genre> getGenresFromDictionary()
        {
            ISet<Genre> genres = new HashSet<Genre>();
            var tuple = selectedGenres.Values;
            foreach (var kvPair in tuple)
            {
                genres.Add(kvPair.Item1);
            }
            return genres;
        }

        public Show[] Shows
        {
            get { return getShowsFromDictionary().ToArray(); }
        }

        private ISet<Show> getShowsFromDictionary()
        {
            ISet<Show> shows = new HashSet<Show>();
            var tuple = selectedShows.Values;
            foreach (var kvPair in tuple)
            {
                shows.Add(kvPair.Item1);
            }
            return shows;
        }

        public string ApiName 
        { 
            get { return providerApi.GetType().Name; }
        }

        public bool loadNextPage()
        {
            if (mode == Mode.FILM_SELECTION)
            {
                try
                {
                    var genres = getGenresFromDictionary();
                    var newShows = providerApi.loadFilteredPage(highestPage, genres);
                    if (newShows.Length == 0)
                    {
                        highestPage++;
                        return loadNextPage();
                    }
                    AddNewShows(newShows);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }


        public Show getNextShow(int number)
        {
            if (number >= selectedShows.Count)
            {
                highestPage++;
                if (!loadNextPage())
                {
                    throw new System.ArgumentOutOfRangeException();
                }
            }
            return selectedShows.ElementAt(number).Value.Item1;
        }


        public void swipeGenre(long userId, int genreId)
        {
            if (mode == Mode.GENRE_SELECTION && getUser(userId).Swipes != 0)
            {
                if (selectedGenres.ContainsKey(genreId))
                {
                    var genre = selectedGenres[genreId];
                    genre.Item2++;
                    selectedGenres[genreId] = genre;
                    getUser(userId).Swipes--;
                }
                if (allUsersReady())
                {
                    nextMode();
                }
            }
        }
        
        public void swipeFilm(long userId, int showId)
        {
            if (mode == Mode.FILM_SELECTION && getUser(userId).Swipes != 0)
            {
                if (selectedShows.ContainsKey(showId))
                {
                    var show = selectedShows[showId];      
                    show.Item2++;
                    selectedShows[showId] = show;
                    getUser(userId).Swipes--;
                }
                if (allUsersReady())
                {
                    nextMode();
                }
            }
        }

        public int getSwipesForGenre(Genre genre)
        {
            return selectedGenres.ContainsKey(genre.Id) ? selectedGenres[genre.Id].Item2 : -1;
        }

        public int getSwipesForShow(Show show)
        {
            return selectedShows.ContainsKey(show.Id) ? selectedShows[show.Id].Item2 : -1;
        }

        public bool allUsersReady()
        {
            foreach (User user in getAllUsers())
            {
                if (!user.Ready)
                {
                    return false;
                }
            }
            return true;
        }

        public void AddNewGenres(ICollection<Genre> keys)
        {
            foreach (var key in keys)
            {
                selectedGenres.Add(key.Id, (key, 0));
            }
        }

        public void AddNewShows(ICollection<Show> keys)
        {
            foreach (var key in keys)
            {
                selectedShows.Add(key.Id, (key, 0));
            }
        }

        public IDictionary<Genre, int> getGenreResults()
        {
            IDictionary<Genre, int> result = new Dictionary<Genre, int>();
            foreach (var kvPair in selectedGenres.Values)
            {
                result.Add(kvPair.Item1, kvPair.Item2);
            }
            return result;
        }

        public IDictionary<Show, int> getShowResults()
        {
            IDictionary<Show, int> result = new Dictionary<Show, int>();
            foreach (var kvPair in selectedShows.Values)
            {
                result.Add(kvPair.Item1, kvPair.Item2);
            }
            return result;
        }
    }
}
