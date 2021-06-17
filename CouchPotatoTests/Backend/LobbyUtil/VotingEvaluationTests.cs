using Microsoft.VisualStudio.TestTools.UnitTesting;
using CouchPotato.Backend.LobbyUtil;
using CouchPotato.Backend.ShowUtil;
using System;
using System.Collections.Generic;
using System.Text;

namespace CouchPotato.Backend.LobbyUtil.Tests
{
    [TestClass]
    public class VotingEvaluationTests
    {

        [TestMethod]
        public void CompareHighestTest()
        {
            VotingEvaluation evaluation = new VotingEvaluation();
                        
            IDictionary<int, (Genre, int)> set = new Dictionary<int, (Genre, int)>();
            Genre mostLikedGenre = VotableFactory.buildGenre(1, "Drama");
            set.Add(1, (mostLikedGenre, 5));
            set.Add(2, (VotableFactory.buildGenre(2, "Adventure"), 2));
            set.Add(3, (VotableFactory.buildGenre(3, "Comedy"), 0));

            IDictionary<int, (Genre, int)> expectedHighest = new Dictionary<int, (Genre, int)>();
            expectedHighest.Add(1, (mostLikedGenre, 5));

            Assert.IsTrue(containtsSame(expectedHighest, evaluation.evaluateGenre(set, EvaluationType.HIGHEST)));
        }

        [TestMethod]
        public void CompareVotedTest()
        {
            VotingEvaluation evaluation = new VotingEvaluation();
            IDictionary<int, (Genre, int)> set = new Dictionary<int, (Genre, int)>();
            Genre votedGenre1 = VotableFactory.buildGenre(1, "Drama");
            Genre votedGenre2 = VotableFactory.buildGenre(2, "Adventure");
            
            set.Add(1, (votedGenre1, 5));
            set.Add(2, (votedGenre2, 2));
            set.Add(3, (VotableFactory.buildGenre(3, "Comedy"), 0));

            IDictionary<int, (Genre, int)> expectedVoted = new Dictionary<int, (Genre, int)>();
            expectedVoted.Add(1, (votedGenre1, 5));
            expectedVoted.Add(2, (votedGenre2, 2));

            Assert.IsTrue(containtsSame(expectedVoted, evaluation.evaluateGenre(set, EvaluationType.VOTED)));
        }

        [TestMethod]
        public void CompareAllTest()
        {
            VotingEvaluation evaluation = new VotingEvaluation();
            IDictionary<int, (Genre, int)> set = new Dictionary<int, (Genre, int)>();
            Genre genre1 = VotableFactory.buildGenre(1, "Drama");
            Genre genre2 = VotableFactory.buildGenre(2, "Adventure");
            Genre genre3 = VotableFactory.buildGenre(3, "Comedy");

            set.Add(1, (genre1, 5));
            set.Add(2, (genre2, 2));
            set.Add(3, (genre3, 0));

            IDictionary<int, (Genre, int)> expectedAll = new Dictionary<int, (Genre, int)>();
            expectedAll.Add(1, (genre1, 5));
            expectedAll.Add(2, (genre2, 2));
            expectedAll.Add(3, (genre3, 0));

            Assert.IsTrue(containtsSame(expectedAll, evaluation.evaluateGenre(set, EvaluationType.ALL)));
        }

        private bool containtsSame(IDictionary<int, (Genre, int)> expected, IDictionary<int, (Genre, int)> actual)
        {
            if (expected.Count != actual.Count)
            {
                return false;
            }

            var it = actual.GetEnumerator();

            foreach((Genre, int) genreTupel in expected.Values)
            {
                if (genreTupel.Item1.Equals(it.Current))
                {
                    return false;
                }
                it.MoveNext();
            }

            return true;
        }
    }
}