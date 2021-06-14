using System;
using CouchPotato.Backend.ShowUtil;

namespace CouchPotato.Models
{
    public class GenreVotingViewModel
    {
        public string name { get; set; }
        public long userid { get; set; }
        public long lobbyid { get; set; }
        public Boolean host { get; set; }
        public Genre[] genres { get; set; }
        public int genreSwipes { get; set; }
    }
}
