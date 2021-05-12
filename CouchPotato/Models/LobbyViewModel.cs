using System;

namespace CouchPotato.Models
{
    public class LobbyViewModel
    {
        public string name { get; set; }
        public long userId { get; set; }
        public long lobbyId { get; set; }
        public Boolean host { get; set; }
    }
}
