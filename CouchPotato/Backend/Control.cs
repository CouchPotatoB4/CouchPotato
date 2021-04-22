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
    public class Control
    {
        private IDictionary<string, Lobby> lobbies = new Dictionary<string, Lobby>(); 

        public HttpResponseMessage post(string request)
        {
            //TODO
            try
            {

            }
            catch (ApiChangedException e)
            {
                Console.WriteLine(e);
                switch (e.Provider)
                {
                    case Provider.Aniflix:
                        break;
                    case Provider.Netflix:
                        break;
                    case Provider.AmazonPrime:
                        break;
                }
            }
            return null;
        }

        //PRIVATE
        public Lobby createLobby(User host)
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
            return lobby.Genre;
        }

        private void swipeGenre(Lobby lobby, long userId, string genre)
        {
            lobby.swipeGenre(userId, genre);
        }

        private ISet<Show> getFilms(Lobby lobby)
        {
            return lobby.Shows;
        }

        private void swipeFilm(Lobby lobby, long userId, int showId)
        {
            lobby.swipeFilm(userId, showId);
        }

        private Image getCoverForShow(Lobby lobby, int showId)
        {
            return lobby.getCoverForShow(showId);
        }


        private void setLobbyConfiguration(Lobby lobby, Provider provider, int swipes, int genresCount)
        {
            lobby.setConfiguration(provider, swipes, genresCount);
        }
    }
}
