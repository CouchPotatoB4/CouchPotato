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
using CouchPotato.Backend.ShowUtil;
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
        public IActionResult Voting(string name, long userid, long lobbyid, bool host)
        {
            Lobby lobby = Control.getLobby(lobbyid);
            VotingViewModel model = new VotingViewModel();
            model.name = name;
            model.userid = userid;
            model.lobbyid = lobbyid;
            model.host = host;
            model.swipes = lobby.Swipes;
            return View(model);
        }

        public IActionResult Endscreen(long lobbyid)
        {
            Lobby lobby = Control.getLobby(lobbyid);
            EndscreenViewModel model = new EndscreenViewModel();
            model.shows = lobby.Shows;
            return View(model);
        }

        public IActionResult Lobby(string name, long userid,long lobbyid, bool host)
        {
            LobbyViewModel model = new LobbyViewModel();
            model.name = name;
            model.userid = userid;
            model.lobbyid = lobbyid;
            model.host = host;
            return View(model);
        }

        public IActionResult GenreVoting(string name, long userid, long lobbyid, bool host)
        {
            Lobby lobby = Control.getLobby(lobbyid);
            GenreVotingViewModel model = new GenreVotingViewModel();
            model.name = name;
            model.userid = userid;
            model.lobbyid = lobbyid;
            model.host = host;
            model.genres = lobby.Genres;
            model.genreSwipes = lobby.GenreSwipes;

            return View(model);
        }

        public virtual ActionResult Card(long userid, long lobbyid, int shownumber)
        {
            Lobby lobby = Control.getLobby(lobbyid);

            try
            {
                Show show = lobby.getNextShow(shownumber);
                CardViewModel model = new CardViewModel();
                model.src = show.CoverPath;
                model.title = show.Name;
                model.description = show.Description;
                model.shownumber = shownumber;
                model.showid = show.Id;
                return PartialView(model);
            }
            catch(System.ArgumentOutOfRangeException)     
            {
                setUserReady(lobbyid, userid);
                return NotFound();
            }
        }

        public ActionResult<Dictionary<string, long>> joinLobby(string name, long lobbyid)
        {
            Dictionary<string, long> returnValue = new Dictionary<string, long>();
            User user = UserFactory.build(name);
            Lobby lobby = Control.getLobby(lobbyid);
            
            if (lobby == null)
            {
                return NotFound("No such Lobby found");
            }
            if (!Control.joinLobby(user, lobby))
            {
                return NotFound("Lobby no longer active");
            }

            returnValue.Add("userid", user.ID);
            returnValue.Add("lobbyid", lobby.ID);
            return returnValue;
        }

        public ActionResult LobbyConfig(long lobbyId)
        {
            Lobby lobby = Control.getLobby(lobbyId);
            LobbyConfigViewModel model = new LobbyConfigViewModel();
            model.api = lobby.ApiName;
            model.swipes_genre = lobby.GenreSwipes;
            model.swipes_show = lobby.Swipes;

            return PartialView(model);
        }

        public Dictionary<string, long> CreateLobby(string name)
        {
            Dictionary<string, long> returnValue = new Dictionary<string, long>();
            User user = UserFactory.build(name);
            Lobby lobby = Control.createLobby(user);

            setConfig(lobby.ID , "PseudoApi", 3, 2);// set default config

            returnValue.Add("userid", user.ID);
            returnValue.Add("lobbyid", lobby.ID);
            return returnValue;
        }

        public List<string> getMembers(long lobbyid)
        {
            Lobby lobby = Control.getLobby(lobbyid);
            ISet<User> users = lobby.getAllUsers();

            List<string> returnValue = new List<string>();
            foreach (var user in users)
            {
                returnValue.Add(user.Name);
            }

            return returnValue;
        }

        public void setUserReady(long lobbyid, long userid)
        {
            Lobby lobby = Control.getLobby(lobbyid);
            lobby.getUser(userid).Ready = true;

            if (allUsersAreReady(lobbyid))
            {
                lobby.nextMode();
            }
        }      

        private bool allUsersAreReady(long lobbyid)
        {
            return Control.getLobby(lobbyid).allUsersReady();
        }

        public void setConfig(long lobbyid, string provider,int swipes, int genresCount)
        {
            Lobby lobby = Control.getLobby(lobbyid);//TODO
            IApi api;
            switch (provider)
            {   
                case "NetflixApi":
                    api = Provider.Netflix.getApi();
                    break;
                case "AmazonPrimeApi":
                    api = Provider.AmazonPrime.getApi();
                    break;
                case "AniflixApi":
                    api = Provider.Aniflix.getApi();
                    break;
                case "PseudoApi":
                    api = new PseudoApi();
                    break;
                default:
                    HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.NotFound);
                    message.Content = new StringContent("Invalid Provider");
                    throw new HttpResponseException(message);
                    break;
            }
            lobby.setConfiguration(api, swipes, genresCount);
        }

        public void startVoting(long lobbyid, long userid)
        {
            Lobby lobby = Control.getLobby(lobbyid);
            if (lobby.getHost().ID.Equals(userid))
            {
                lobby.nextMode();
            }
        }

        public Mode getMode (long lobbyid)
        {
            Lobby lobby = Control.getLobby(lobbyid);
            return lobby.GetMode();
        }

        public void swipeGenre(long userid, long lobbyid, string genre)
        {
            Lobby lobby = Control.getLobby(lobbyid);
            lobby.swipeGenre(userid, genre);
        }

        public void swipeShow(long userid, long lobbyid, int showid)
        {
            Lobby lobby = Control.getLobby(lobbyid);
            lobby.swipeFilm(userid, showid);
        }
    }
}
