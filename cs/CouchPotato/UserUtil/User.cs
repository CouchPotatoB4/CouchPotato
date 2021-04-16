using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.UserUtil
{
    public class User
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
            set { swipes = value; }
        }

        public User(long id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public bool swipe()
        {
            if (swipes > 0)
            {
                swipes--;
                return true;
            }
            return false;
        }
    }
}
