using System;

namespace CouchPotato.Models
{
    public class LobbyViewModel
    {
        public string name { get; set; }
        public long userid { get; set; }
        public long lobbyid { get; set; }
        public bool host { get; set; }
    }
}
