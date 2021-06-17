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

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Votable(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public bool Equals(Votable v)
        {
            if (v == null) return false;
            return id == v.Id;
        }
    }
}
