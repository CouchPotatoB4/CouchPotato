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
using CouchPotato.Backend.ApiUtil;
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
        public IActionResult Voting(string name, long userid, long lobbyid, Boolean host)
        {
            VotingViewModel model = new VotingViewModel();
            model.name = name;
            model.userid = userid;
            model.lobbyid = lobbyid;
            model.host = host;
            return View(model);
        }
        public IActionResult Lobby(string name, long userid,long lobbyid, Boolean host)
        {
            LobbyViewModel model = new LobbyViewModel();
            model.name = name;
            model.userid = userid;
            model.lobbyid = lobbyid;
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

            setConfig(lobby.ID.ToString() , "Aniflix", 10, 5);// set default config

            returnValue.Add("userid", user.ID);
            returnValue.Add("lobbyid", lobby.ID);
            return returnValue;
        }

        public List<String> getMembers(string lobbyid)
        {
            Lobby lobby = Control.getLobby(lobbyid);
            ISet<User> users = lobby.getUser();
            User host = lobby.getHost();

            List<String> returnValue = new List<string>();
            returnValue.Add(host.Name);
            foreach (var user in users)
            {
                returnValue.Add(user.Name);
            }

            return returnValue;
        }

        public void setConfig(string lobbyid, String provider,int swipes, int genresCount)
        {
            Lobby lobby = Control.getLobby(lobbyid);//TODO
            Provider p;
            switch (provider)
            {   
                case "Netflix":
                    p = Provider.Netflix;
                    break;
                case "AmazonPrime":
                    p = Provider.AmazonPrime;
                    break;
                case "Aniflix":
                    p = Provider.Aniflix;
                    break;
                default:
                    HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.NotFound);
                    message.Content = new StringContent("Invalid Provider");
                    throw new HttpResponseException(message);
                    break;
            }
            lobby.setConfiguration(p, swipes, genresCount);
        }

        public void startVoting(string lobbyid, long userid)
        {
            Lobby lobby = Control.getLobby(lobbyid);
            if (lobby.getHost().ID.Equals(userid))
            {
                lobby.nextMode();
            }

        }

        public Mode getMode (string lobbyid)
        {
            Lobby lobby = Control.getLobby(lobbyid);
            return lobby.GetMode();
        }
    }
}
