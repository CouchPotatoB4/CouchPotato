﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.Backend.ShowUtil
{
    public class VotableFactory
    {
        public static Genre buildGenre(int id, string name)
        {
            return new Genre(id, name);
        }

        public static Show buildShow(int id, string name, string description, string coverStorage)
        {
            return new Show(id, name, description, coverStorage);
        }
    }
}
