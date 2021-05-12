using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CouchPotato.Backend;
using CouchPotato.Backend.UserUtil;
using CouchPotato.Backend.LobbyUtil;
using CouchPotato.Models;


namespace CouchPotato.Controllers
{
    public class MainController : Controller
    {

        public MainController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Name()
        {
            return View();
        }
        public IActionResult Lobby(string name, long userId,long lobbyId, Boolean host)
        {
            LobbyViewModel model = new LobbyViewModel();
            model.name = name;
            model.userId = userId;
            model.lobbyId = lobbyId;
            model.host = host;
            return View(model);
        }

        public Dictionary<string, long> joinLobby(string name, string lobbyid)
        {
            Dictionary<string, long> returnValue = new Dictionary<string, long>();
            User user = UserFactory.build(name);
            Lobby lobby = Control.getLobby(lobbyid);
            
            if (lobby == null)
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.NotFound);
                message.Content = new StringContent("No such Lobby");
                throw new HttpResponseException(message);
            }
            if (!Control.joinLobby(user, lobby))
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.NotFound);
                message.Content = new StringContent("Lobby no longer open");
                throw new HttpResponseException(message);
            }

            returnValue.Add("userid", user.ID);
            returnValue.Add("lobbyid", lobby.ID);
            return returnValue;
        }

        public Dictionary<string, long> CreateLobby(string name)
        {
            Dictionary<string, long> returnValue = new Dictionary<string, long>();
            User user = UserFactory.build(name);
            Lobby lobby = Control.createLobby(user);
            returnValue.Add("userid", user.ID);
            returnValue.Add("lobbyid", lobby.ID);
            return returnValue;
        }
    }
}
