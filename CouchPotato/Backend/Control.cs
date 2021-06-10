using CouchPotato.Backend.ApiUtil;
using CouchPotato.Backend.ApiUtil.Exceptions;
using CouchPotato.Backend.LobbyUtil;
using CouchPotato.Backend.UserUtil;
using CouchPotato.Backend.ShowUtil;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Http;

namespace CouchPotato.Backend
{
    public static class Control
    {
        private static IDictionary<string, Lobby> lobbies = new Dictionary<string, Lobby>(); 

        public static Lobby createLobby(User host)
        {
            Lobby lobby = LobbyFactory.build(host);
            lobbies.Add(lobby.ID.ToString() , lobby);
            return lobby;
        }

        public static Lobby getLobby(String id)
        {
            if (lobbies.ContainsKey(id))
            {
                Lobby lobby;
                lobbies.TryGetValue(id, out lobby);
                return lobby;
            }
            return null;
        }

        public static bool joinLobby(User user, Lobby lobby)
        {
            if (lobby.isOpen())
            {
                lobby.addUser(user);
            }
            return lobby.isOpen();
        }

        private static void startSelection(Lobby lobby)
        {
            lobby.nextMode();
        }

        private static string[] getGenre(Lobby lobby)
        {
            return lobby.Genre;
        }

        private static void swipeGenre(Lobby lobby, long userId, string genre)
        {
            lobby.swipeGenre(userId, genre);
        }

        private static ISet<Show> getFilms(Lobby lobby)
        {
            return lobby.Shows;
        }

        private static void swipeFilm(Lobby lobby, long userId, int showId)
        {
            lobby.swipeFilm(userId, showId);
        }

        private static Image getCoverForShow(Lobby lobby, int showId)
        {
            return lobby.getCoverForShow(showId);
        }


        private static void setLobbyConfiguration(Lobby lobby, Provider provider, int swipes, int genresCount)
        {
            lobby.setConfiguration(provider, swipes, genresCount);
        }

    }
}
