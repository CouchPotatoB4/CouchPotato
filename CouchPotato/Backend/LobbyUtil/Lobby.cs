﻿using CouchPotato.Backend.UserUtil;
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
        private ISet<Show> selectedShows = new HashSet<Show>();
        private ISet<Genre> selectedGenres = new HashSet<Genre>();
        private VotingEvaluation evaluation = new VotingEvaluation();
        private Provider provider;
        private Mode mode;
        private int sSwipes, gSwipes;

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

        public ISet<User> getUser()
        {
            ISet<User> allUser = new HashSet<User>();
            allUser.Add(host);
            foreach (User u in users)
            {
                allUser.Add(u);
            }
            return allUser;
        }

        public void setConfiguration(Provider? provider, int sSwipes, int gSwipes)
        {
            if (provider != null) this.provider = (Provider)provider;

            if (sSwipes > 0) this.sSwipes = sSwipes;

            if (gSwipes > 0) this.gSwipes = gSwipes;
        }

        public Image getCoverForShow(int id)
        {
            foreach (Show show in selectedShows)
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
                selectedGenres = new HashSet<Genre>(provider.getApi().getGenres());
                setUserSwipes(gSwipes);
            }
            else if (mode == Mode.GENRE_SELECTION)
            {
                mode = Mode.FILM_SELECTION;
                selectedGenres = evaluation.evaluateGenre(selectedGenres, EvaluationType.HIGHEST);
                selectedShows = new HashSet<Show>(provider.getApi().getShows(selectedGenres));
                setUserSwipes(sSwipes);
            }
            else if (mode == Mode.FILM_SELECTION)
            {
                mode = Mode.OVER;
            }
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
            get { return getNames((ISet<Votable>)selectedGenres); }
        }

        private string[] getNames(ISet<Votable> set)
        {
            string[] names = new string[set.Count];
            int i = 0;
            foreach (Votable v in set)
            {
                names[i] = v.Name;
                i++;
            }
            return names;
        }

        public ISet<Show> Shows
        {
            get { return selectedShows; }
        }

        public void loadPage(int page)
        {
            if (mode == Mode.FILM_SELECTION)
            {
                foreach (Show s in provider.getApi().getShows(page))
                {
                    selectedShows.Add(s);
                }
            }
        }


        public Show getNextShow(int number)
        {
            if (number >= selectedShows.Count)
            {
                int page = number / ApiConstants.PAGE_SIZE;
                loadPage(page);
            }
            return selectedShows.ElementAt(number);
        }


        public void swipeGenre(long userId, string genre)
        {
            if (mode == Mode.GENRE_SELECTION)
            {
                foreach (Genre g in selectedGenres)
                {
                    if (g.Name == genre)
                    {
                        if (getUser(userId).swipe())
                        {
                            g.Vote();
                            break;
                        }
                    }
                }
            }
        }


        public void swipeFilm(long userId, int showId)
        {
            if (mode == Mode.FILM_SELECTION)
            {
                foreach (Show s in selectedShows)
                {
                    if (s.Id == showId)
                    {
                        if (getUser(userId).swipe())
                        {
                            s.Vote();
                            break;
                        }
                    }
                }
            }
        }

        public ISet<Genre> getGenreResults()
        {
            return evaluation.evaluateGenre(selectedGenres, EvaluationType.HIGHEST);
        }

        public ISet<Show> getShowResults()
        {
            return evaluation.evaluateShow(selectedShows, EvaluationType.HIGHEST);
        }
    }
}