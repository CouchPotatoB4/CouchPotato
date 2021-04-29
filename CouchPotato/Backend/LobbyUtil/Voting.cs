using CouchPotato.Backend.ShowUtil;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CouchPotato.Backend.LobbyUtil
{
    public class Voting : IComparer<Votable>
    {
        public int Compare(Votable x, Votable y)
        {
            return x.Votes.CompareTo(y.Votes);
        }

        internal ISet<Genre> getGenreResult(ISet<Genre> genres)
        {
            ISet<Genre> sorted = new SortedSet<Genre>(genres, this);
            return sorted;
        }

        internal ISet<Show> getShowResults(ISet<Show> shows)
        {
            ISet<Show> sorted = new SortedSet<Show>(shows, this);
            return sorted;
        }
    }
}
