﻿using CouchPotato.ShowUtil;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CouchPotato.ApiUtil
{
    interface IApi
    {
        Show[] getShows();

        Show[] getShows(string genre);

        Show[] getShows(IList<string> genres);

        Show[] getShows(int page);

        string[] getGenres();

        Image getCoverForShow(int id);
    }
}
