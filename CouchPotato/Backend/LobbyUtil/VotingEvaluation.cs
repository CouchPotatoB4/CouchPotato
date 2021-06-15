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

    public class VotingEvaluation
    {
        public ISet<Genre> evaluateGenre(ISet<Genre> genres, EvaluationType type)
        {
            var sorted = evaluate(new HashSet<Votable>(genres), type);
            var casted = new HashSet<Genre>(); 

            foreach (Votable v in sorted)
            {
                casted.Add((Genre)v);
            }

            return casted;
        }

        public ISet<Show> evaluateShow(ISet<Show> shows, EvaluationType type)
        {
            var sorted = evaluate(new HashSet<Votable>(shows), type);
            var casted = new HashSet<Show>();

            foreach (Votable v in sorted)
            {
                casted.Add((Show)v);
            }

            return casted;
        }

        private ISet<Votable> evaluate(ISet<Votable> set, EvaluationType type)
        {
            switch (type)
            {
                case EvaluationType.ALL: 
                    return sortAll(set);
                case EvaluationType.HIGHEST: 
                    return sortAfterHighest(set);
                case EvaluationType.VOTED:
                    return sortAfterVoted(set);
                default:
                    return set;
            }
        }

        private ISet<Votable> sortAfterHighest(ISet<Votable> set)
        {
            ISet<Votable> sorted = new HashSet<Votable>();

            int highestVote = getHighestVote(set);
            foreach (Votable v in set)
            {
                if (v.Votes == highestVote)
                {
                    sorted.Add(v);
                }
            }

            return sorted;
        }

        private ISet<Votable> sortAfterVoted(ISet<Votable> set)
        {
            var copy = new HashSet<Votable>(set);
            foreach (Votable v in copy)
            {
                if (v.Votes == 0) set.Remove(v);
            }
            return sortAll(set);
        }

        private ISet<Votable> sortAll(ISet<Votable> set)
        {
            IList<Votable> list = new List<Votable>(set);

            for (int i = 1; i < list.Count; i++)
            {
                for (int j = list.Count - 1; j > 0 + i; j--)
                {
                    if (list[j].Votes > list[j - 1].Votes)
                    {
                        switchElements(list, j, j - 1);
                    }
                }
            }

            return list.ToHashSet<Votable>();
        }

        private int getHighestVote(ISet<Votable> set)
        {
            int highestVote = 0;
            foreach (Votable v in set)
            {
                if (v.Votes > highestVote)
                {
                    highestVote = v.Votes;
                }
            }
            return highestVote;
        }

        private void switchElements(IList<Votable> list, int one, int two)
        {
            Votable tmp = list[one];
            list[one] = list[two];
            list[two] = tmp;
        }
    }
}
