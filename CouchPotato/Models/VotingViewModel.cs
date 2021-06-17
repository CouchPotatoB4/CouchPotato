using System;
using CouchPotato.Backend.ShowUtil;

namespace CouchPotato.Models
{
    public class VotingViewModel
    {
        public string name { get; set; }
        public long userid { get; set; }
        public long lobbyid { get; set; }
        public Boolean host { get; set; }
        public int swipes { get; set; }
        public Genre[] selectedGenre { get; set; }
    }
}
