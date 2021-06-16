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

        Show[] getFilteredShows(ISet<Genre> genres);

        Show[] getFilteredShows(ISet<Genre> genres, IEnumerable<Show> shows);

        Show[] loadFilteredPage(int page, ISet<Genre> genres);

        Genre[] getGenres();

        Image getCoverForShow(int id);
    }
}
