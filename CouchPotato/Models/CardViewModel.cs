using System;
using CouchPotato.Backend.ShowUtil;

namespace CouchPotato.Models
{
    public class CardViewModel
    {
        public string src { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int shownumber { get; set; }
        public int showid { get; set; }
        public Genre[] showgenre { get; set; }
        public Genre[] selectedGenre { get; set; }
    }
}
