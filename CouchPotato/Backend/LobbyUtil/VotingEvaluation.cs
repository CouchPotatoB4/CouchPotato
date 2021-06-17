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
        public IDictionary<int, (Genre, int)> evaluateGenre(IDictionary<int, (Genre, int)> genres, EvaluationType type)
        {
            var result = convertToVotable(genres.Values);
            var sorted = evaluate(result, type);
            genres.Clear();
            foreach (var genre in sorted)
            {
                genres.Add(genre.Item1.Id, ((Genre)genre.Item1, genre.Item2));
            }
            return genres;
        }

        private ICollection<(Votable, int)> convertToVotable(ICollection<(Genre, int)> genres)
        {
            var result = new List<(Votable, int)>();
            foreach (var genre in genres)
            {
                result.Add(genre);
            }
            return result;
        }

        public IDictionary<int, (Show, int)> evaluateShow(IDictionary<int, (Show, int)> shows, EvaluationType type)
        {
            var result = convertToVotable(shows.Values);
            var sorted = evaluate(result, type);
            shows.Clear();
            foreach (var show in sorted)
            {
                shows.Add(((Show)show.Item1).Id, ((Show)show.Item1, show.Item2));
            }
            return shows;
        }

        private ICollection<(Votable, int)> convertToVotable(ICollection<(Show, int)> shows)
        {
            var result = new List<(Votable, int)>();
            foreach (var show in shows)
            {
                result.Add(show);
            }
            return result;
        }

        private ICollection<(Votable, int)> evaluate(ICollection<(Votable, int)> dictionary, EvaluationType type)
        {
            switch (type)
            {
                case EvaluationType.ALL: 
                    return sortAll(dictionary);
                case EvaluationType.HIGHEST: 
                    return sortAfterHighest(dictionary);
                case EvaluationType.VOTED:
                    return sortAfterVoted(dictionary);
                default:
                    return null;
            }
        }

        private ICollection<(Votable, int)> sortAfterHighest(ICollection<(Votable, int)> collection)
        {
            ISet<(Votable, int)> sorted = new HashSet<(Votable, int)>();

            int highestVote = getHighestVote(collection);
            foreach (var each in collection)
            {
                if (each.Item2 == highestVote)
                {
                    sorted.Add(each);
                }
            }

            return sorted;
        }

        private ICollection<(Votable, int)> sortAfterVoted(ICollection<(Votable, int)> collection)
        {
            var copy = new HashSet<(Votable, int)>(collection);
            foreach (var each in copy)
            {
                if (each.Item2 == 0)
                {
                    collection.Remove(each);
                }
            }
            return sortAll(collection);
        }

        private ICollection<(Votable, int)> sortAll(ICollection<(Votable, int)> collection)
        {
            IList<(Votable, int)> list = new List<(Votable, int)>(collection);

            for (int i = 1; i < list.Count; i++)
            {
                for (int j = list.Count - 1; j > 0 + i; j--)
                {
                    if (list[j].Item2 > list[j - 1].Item2)
                    {
                        switchElements(list, j, j - 1);
                    }
                }
            }

            return list.ToHashSet<(Votable, int)>();
        }

        private int getHighestVote(ICollection<(Votable, int)> collection)
        {
            int highestVote = 0;
            foreach (var each in collection)
            {
                if (each.Item2 > highestVote)
                {
                    highestVote = each.Item2;
                }
            }
            return highestVote;
        }

        private void switchElements(IList<(Votable, int)> collection, int one, int two)
        {
            var tmp = collection[one];
            collection[one] = collection[two];
            collection[two] = tmp;
        }
    }
}
