using CouchPotato.LobbyUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CouchPotato
{
    using Lobby = LobbyUtil.Lobby;
    using User = UserUtil.User;
    using Film = FilmUtil.Film;
    using Provider = ApiUtil.Provider;

    class Control
    {
        private IDictionary<string, Lobby> lobbies = new Dictionary<string, Lobby>(); 

        public string post(string request)
        {
            //TODO


            return null;
        }

        private Lobby createLobby(User host)
        {
            return LobbyFactory.build(host);
        }

        private bool joinLobby(User user, Lobby lobby)
        {
            if (lobby.isOpen())
            {
                lobby.addUser(user);
            }
            return lobby.isOpen();
        }

        private void startSelection(Lobby lobby)
        {
            lobby.nextMode();
        }

        private string[] getGenre(Lobby lobby)
        {
            return lobby.getGenre();
        }

        private void swipeGenre(Lobby lobby, long userId, string genre)
        {
            lobby.swipeGenre(userId, genre);
        }

        private ISet<Film> getFilms(Lobby lobby)
        {
            return lobby.Films;
        }

        private void swipeFilm(Lobby lobby, long userId, long filmId)
        {
            lobby.swipeFilm(userId, filmId);
        }

        private Image getCoverForFilm(Lobby lobby, long filmId)
        {
            return lobby.getCoverForFilm(filmId);
        }


        private void setLobbyConfiguration(Lobby lobby, Provider provider, int swipes)
        {
            lobby.setConfiguration(provider, swipes);
        }
    }
}
