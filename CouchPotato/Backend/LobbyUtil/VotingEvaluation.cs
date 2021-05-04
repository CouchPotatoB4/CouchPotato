using CouchPotato.Backend.ShowUtil;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CouchPotato.Backend.LobbyUtil
{
    public enum EvaluationType
    {
        ALL, VOTED, HIGHEST
    }

    public class VotingEvaluation : IComparer<Votable>
    {
        public int Compare(Votable x, Votable y)
        {
            return x.Votes.CompareTo(y.Votes);
        }

        internal ISet<Genre> evaluateGenre(ISet<Genre> genres)
        {
            return (ISet<Genre>)evaluate((ISet<Votable>)genres, EvaluationType.ALL);
        }

        internal ISet<Genre> evaluateGenre(ISet<Genre> genres, EvaluationType type)
        {
            return (ISet<Genre>)evaluate((ISet<Votable>)genres, type);
        }

        internal ISet<Show> evaluateShow(ISet<Show> shows)
        {
            return (ISet<Show>)evaluate((ISet<Votable>)shows, EvaluationType.ALL);
        }

        internal ISet<Show> evaluateShow(ISet<Show> shows, EvaluationType type)
        {
            return (ISet<Show>)evaluate((ISet<Votable>)shows, type);
        }

        internal ISet<Votable> evaluate(ISet<Votable> set, EvaluationType type)
        {
            ISet<Votable> sorted = new SortedSet<Votable>(set, this);
            return sorted;
        }
    }
}
