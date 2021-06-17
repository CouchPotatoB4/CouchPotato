using System;
using CouchPotato.Backend.ShowUtil;

namespace CouchPotato.Models
{
    public class EndscreenViewModel
    {
        public Show[] shows { get; set; }
        public int anzUser { get; set; }
        public int votes { get; set; }
    }
}
