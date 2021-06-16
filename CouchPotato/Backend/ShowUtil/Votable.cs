using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouchPotato.Backend.ShowUtil
{
    public abstract class Votable
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int votes;

        public int Votes
        {
            get { return votes; }
        }

        public void Vote()
        {
            votes++;
        }

        public Votable(string name)
        {
            Name = name;
        }

        public bool Equals(Votable v)
        {
            if (v == null) return false;
            return name.Equals(v.name) && votes == v.votes;
        }
    }
}
