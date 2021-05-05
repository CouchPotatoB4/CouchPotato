using CouchPotato.Backend.ShowUtil;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CouchPotato.Backend.ApiUtil
{
    public interface IApi
    {
        Show[] getShows();

        Show[] getShows(Genre genre);

        Show[] getShows(ISet<Genre> genres);

        Show[] getShows(int page);

        Genre[] getGenres();

        Image getCoverForShow(int id);
    }
}
