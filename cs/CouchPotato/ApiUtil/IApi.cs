using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CouchPotato.ApiUtil
{
    using Film = CouchPotato.FilmUtil.Film;

    interface IApi
    {
        Film[] getFilms();

        Film[] getFilms(string genre);

        Film[] getFilms(int page);

        string[] getGenres();

        Image getCoverForFilm(long id);
    }
}
