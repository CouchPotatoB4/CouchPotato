using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouchPotato.Backend.ShowUtil
{
    public class Genre : Votable
    {
        public Genre(int id, string name) : base(name) { } 
    }
}
