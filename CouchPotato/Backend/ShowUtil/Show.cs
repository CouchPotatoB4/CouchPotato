using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.Backend.ShowUtil
{
    public class Show
    {
        private int id;
        private string name;
        private string description;
        private string coverStorage;
        private int votes;


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string CoverStorage
        {
            get { return coverStorage; }
            set { coverStorage = value; }
        }


        public int getVotes()
        {
            return votes;
        }

        public void vote()
        {
            votes++;
        }


        public Show(int id, string name, string description, string coverStorage)
        {
            Id = id;
            Name = name;
            Description = description;
            CoverStorage = coverStorage;
        }
    }
}
