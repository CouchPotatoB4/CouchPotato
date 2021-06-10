﻿using System;

namespace CouchPotato.Models
{
    public class GenreVotingViewModel
    {
        public string name { get; set; }
        public long userid { get; set; }
        public long lobbyid { get; set; }
        public Boolean host { get; set; }
        public string[] genres { get; set; }
        public int genreSwipes { get; set; }
    }
}
