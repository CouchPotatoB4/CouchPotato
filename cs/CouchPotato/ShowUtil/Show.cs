using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.ShowUtil
{
    class Show
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public Show(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
