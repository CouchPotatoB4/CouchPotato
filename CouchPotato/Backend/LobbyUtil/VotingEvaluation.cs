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
            //This method is slightly different from the evaluate method, due to strange InvalidCastException from HashSet'1 to ISet'1
            ISet<Genre> sorted = new SortedSet<Genre>(genres, this);
            return sorted;
        }

        internal ISet<Genre> evaluateGenre(ISet<Genre> genres, EvaluationType type)
        {   
            //This method is slightly different from the evaluate method, due to strange InvalidCastException from HashSet'1 to ISet'1
            ISet<Genre> sorted = new SortedSet<Genre>(genres, this);
            return sorted;
        }

        internal ISet<Show> evaluateShow(ISet<Show> shows)
        {
            //This method is slightly different from the evaluate method, due to strange InvalidCastException from HashSet'1 to ISet'1
            ISet<Show> sorted = new SortedSet<Show>(shows, this);
            return sorted;
        }

        internal ISet<Show> evaluateShow(ISet<Show> shows, EvaluationType type)
        {
            //This method is slightly different from the evaluate method, due to strange InvalidCastException from HashSet'1 to ISet'1
            ISet<Show> sorted = new SortedSet<Show>(shows, this);
            return sorted;
        }

        internal ISet<Votable> evaluate(ISet<Votable> set, EvaluationType type)
        {
            ISet<Votable> sorted = new SortedSet<Votable>(set, this);
            return sorted;
        }
    }
}
