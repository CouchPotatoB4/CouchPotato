using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CouchPotato.FilmUtil;

namespace CouchPotato.ApiUtil
{
    using Film = CouchPotato.FilmUtil.Film;
    class Api : IApi
    {
        private string[] genre;

        public Image getCoverForFilm(long id)
        {
            throw new NotImplementedException();
        }

        public Film[] getFilms()
        {
            throw new NotImplementedException();
        }

        public Film[] getFilms(string genre)
        {
            throw new NotImplementedException();
        }

        public Film[] getFilms(int page)
        {
            throw new NotImplementedException();
        }

        public string[] getGenres()
        {
            if (genre == null)
            {

            }
            return genre;
        }
    }
}
