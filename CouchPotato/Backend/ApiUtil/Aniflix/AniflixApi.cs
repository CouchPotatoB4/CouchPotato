﻿using CouchPotato.Backend.ShowUtil;
using CouchPotato.Backend.ApiUtil;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace CouchPotato.Backend.ApiUtil.Aniflix
{
    public class AniflixApi : AbstractApi, IApi
    {   
        private const string SHOW = "show";
        private const string HEADER_API = "api";
        private const string HEADER_SHOW = HEADER_API + "/" + SHOW + "/index";
        private const string HEADER_GENRE = HEADER_API + "/" + SHOW + "/genres";
        private const string HEADER_STORAGE = "storage";

        public AniflixApi() : base("https://www2.aniflix.tv") 
        {
            client.DefaultRequestHeaders.Add("User-Agent", "Aniflix_App");
        }

        protected override Task<HttpResponseMessage> get(string header)
        {
            return client.GetAsync(query + "/" + header);
        }

        public Image getCoverForShow(int id)
        {
            string wholeQuery = query + "/" + HEADER_STORAGE + "/";
            var headers = new List<(HttpRequestHeader, string)>();
            headers.Add((HttpRequestHeader.Authorization, "User-Agent=Aniflix_App"));
            return base.getCoverForShow(id, wholeQuery, headers);
        }

        public Show[] getShows()
        {
            if (shows.Count == 0)
            {
                if (isStatusCodeOk())
                {
                    try
                    {
                        var allShows = JsonConvert.DeserializeObject<List<ShowJson>>(getResponseBody(HEADER_SHOW));
                        shows = new Show[allShows.Count];
                    }
                    catch (Exception e)
                    {
                        throw new Exceptions.ApiChangedException(Provider.Aniflix, "Error in request GET/show.", e);
                    }
                    try
                    {
                        var genreWithShows = JsonConvert.DeserializeObject<List<GenreWithShowsJson>>(getResponseBody(HEADER_GENRE)).ToArray();
                        int showCount = 0;
                        foreach (GenreWithShowsJson gwsJson in genreWithShows)
                        {
                            var showsWithThisGenre = gwsJson.shows;
                            foreach (ShowJson sJson in showsWithThisGenre)
                            {
                                int newShow = showIsNew(sJson.id, sJson.name);
                                if (newShow == -1)
                                {
                                    shows[showCount] = VotableFactory.buildShow(sJson.id, sJson.name, sJson.description, sJson.cover_landscape);
                                    shows[showCount].AddGenre(getGenre(gwsJson.name));
                                    showCount++;
                                }
                                else
                                {
                                    shows[newShow].AddGenre(getGenre(gwsJson.name));
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exceptions.ApiChangedException(Provider.Aniflix, "Error in request GET/show.", e);
                    }
                }
            }

            return shows.ToArray<Show>();
        }

        private int showIsNew(int id, string name)
        {
            for (int i = 0; i < shows.Count; i++)
            {
                Show s = shows[i];
                if (s == null) break;
                if (s.Id == id && s.Name == name) return i;
            }
            return -1;
        }

        private Genre getGenre(string name)
        {
            foreach (Genre g in genres)
            {
                if (g.Name.Equals(name)) return g;
            }
            return null;
        }

        public Show[] getFilteredShows(ISet<Genre> genres)
        {
            if (genres == null) getGenres();
            if (shows.Count == 0) getShows();

            ISet<Show> showSet = new HashSet<Show>();
            foreach (Show s in shows)
            {
                foreach (Genre g in s.Genres)
                {
                    if (genres.Contains(g))
                    {
                        showSet.Add(s);
                        break;
                    }
                }
            }
            return showSet.ToArray();
        }

        //Beginning from 0
        public bool loadPage(int page)
        {
            if (shows.Count == 0) getShows();

            IList<Show> localShows = new List<Show>();

            int start = page * ApiConstants.PAGE_SIZE;
            int end = start + ApiConstants.PAGE_SIZE;

            if (start < shows.Count)
            {
                end = end < shows.Count ? end : shows.Count;

                for (int i = start; i < end; i++)
                {
                    shows.Add(shows[i]);
                }
            }

            return true;
        }

        public Genre[] getGenres()
        {
            if (genres == null)
            {
                if (isStatusCodeOk())
                {
                    try
                    {
                        var responseBody = getResponseBody(HEADER_GENRE);
                        var genreWithShows = JsonConvert.DeserializeObject<List<GenreWithShowsJson>>(responseBody);

                        genres = new Genre[genreWithShows.Count];
                        for (int i = 0; i < genres.Count; i++)
                        {
                            string genre = genreWithShows[i].name;
                            genres[i] = VotableFactory.buildGenre(i, genre);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exceptions.ApiChangedException(Provider.Aniflix, "Error in request GET/genre.", e);
                    }
                }
               
            }
            return genres.ToArray();
        }

        public Show[] getFilteredShows(ISet<Genre> genres, IEnumerable<Show> shows)
        {
            throw new NotImplementedException();
        }

        public Show[] loadFilteredPage(int page, ISet<Genre> genres)
        {
            throw new NotImplementedException();
        }
    }
}
