using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.Backend.ShowUtil
{
    public partial class Show : Votable
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string coverPath;

        public string CoverPath
        {
            get { return coverPath; }
            set { coverPath = value; }
        }

        private ISet<Genre> genres;

        public ISet<Genre> Genres
        {
            get { return genres; }     
        }

        public void AddGenre(Genre genre)
        {
            genres.Add(genre);
        }


        public Show(int id, string name, string description, string coverStorage) : base(name)
        {
            Id = id;
            Description = description;
            CoverPath = coverStorage;
        }
    }
}
