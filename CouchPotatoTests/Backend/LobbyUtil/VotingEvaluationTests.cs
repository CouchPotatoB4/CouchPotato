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
                        
            IDictionary<string, (Genre, int)> set = new Dictionary<string, (Genre, int)>();
            Genre mostLikedGenre = VotableFactory.buildGenre("Drama");
            set.Add("Drama", (mostLikedGenre, 5));
            set.Add("Adventure", (VotableFactory.buildGenre("Adventure"), 2));
            set.Add("Comedy", (VotableFactory.buildGenre("Comedy"), 0));

            IDictionary<string, (Genre, int)> expectedHighest = new Dictionary<string, (Genre, int)>();
            expectedHighest.Add("Drama", (mostLikedGenre, 5));

            Assert.IsTrue(containtsSame(expectedHighest, evaluation.evaluateGenre(set, EvaluationType.HIGHEST)));
        }

        [TestMethod]
        public void CompareVotedTest()
        {
            VotingEvaluation evaluation = new VotingEvaluation();
            IDictionary<string, (Genre, int)> set = new Dictionary<string, (Genre, int)>();
            Genre votedGenre1 = VotableFactory.buildGenre("Drama");
            Genre votedGenre2 = VotableFactory.buildGenre("Adventure");
            
            set.Add("Drama", (votedGenre1, 5));
            set.Add("Adventure", (votedGenre2, 2));
            set.Add("Comedy", (VotableFactory.buildGenre("Comedy"), 0));

            IDictionary<string, (Genre, int)> expectedVoted = new Dictionary<string, (Genre, int)>();
            expectedVoted.Add("Drama", (votedGenre1, 5));
            expectedVoted.Add("Adventure", (votedGenre2, 2));

            Assert.IsTrue(containtsSame(expectedVoted, evaluation.evaluateGenre(set, EvaluationType.VOTED)));
        }

        [TestMethod]
        public void CompareAllTest()
        {
            VotingEvaluation evaluation = new VotingEvaluation();
            IDictionary<string, (Genre, int)> set = new Dictionary<string, (Genre, int)>();
            Genre genre1 = VotableFactory.buildGenre("Drama");
            Genre genre2 = VotableFactory.buildGenre("Adventure");
            Genre genre3 = VotableFactory.buildGenre("Comedy");

            set.Add("Drama", (genre1, 5));
            set.Add("Adventure", (genre2, 2));
            set.Add("Comedy", (genre3, 0));

            IDictionary<string, (Genre, int)> expectedVoted = new Dictionary<string, (Genre, int)>();
            expectedVoted.Add("Drama", (genre1, 5));
            expectedVoted.Add("Adventure", (genre2, 2));

            IDictionary<string, (Genre, int)> expectedAll = new Dictionary<string, (Genre, int)>();
            expectedAll.Add("Drama", (genre1, 5));
            expectedAll.Add("Adventure", (genre2, 2));
            expectedAll.Add("Comedy", (genre3, 0));

            Assert.IsTrue(containtsSame(expectedAll, evaluation.evaluateGenre(set, EvaluationType.ALL)));
        }

        private bool containtsSame(IDictionary<string, (Genre, int)> expected, IDictionary<string, (Genre, int)> actual)
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