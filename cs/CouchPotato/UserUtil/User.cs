using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.UserUtil
{
    class User
    {
        private long id;
        private string name;
        private int swipes;

        public long ID
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public int Swipes
        {
            get { return swipes; }
        }

        public User(long id, string name, int swipes)
        {
            this.id = id;
            this.name = name;
            this.swipes = swipes;
        }

        public void resetSwipes(int swipes)
        {
            this.swipes = swipes;
        }
    }
}
