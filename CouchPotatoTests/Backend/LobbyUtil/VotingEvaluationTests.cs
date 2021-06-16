using Microsoft.VisualStudio.TestTools.UnitTesting;
using CouchPotato.Backend.LobbyUtil;
using CouchPotato.Backend.ShowUtil;
using System;
using System.Collections.Generic;
using System.Text;

namespace CouchPotato.Backend.LobbyUtil.Tests
{
    [TestClass()]
    public class VotingEvaluationTests
    {

        [TestMethod()]
        public void CompareHighestTest()
        {
            VotingEvaluation evaluation = new VotingEvaluation();
            Genre genre1 = VotableFactory.buildGenre("Adventure");
            genre1.Vote();
            genre1.Vote();
            Genre genre2 = VotableFactory.buildGenre("Love Story");
            genre2.Vote();
            Genre genre3 = VotableFactory.buildGenre("Horror");

            ISet<Genre> set = new HashSet<Genre>();
            set.Add(genre1);
            set.Add(genre2);
            set.Add(genre3);

            ISet<Genre> expectedHighest = new HashSet<Genre>();
            expectedHighest.Add(genre1);

            Assert.IsTrue(containtsSame(expectedHighest, evaluation.evaluateGenre(set, EvaluationType.HIGHEST)));
        }

        [TestMethod()]
        public void CompareVotedTest()
        {
            VotingEvaluation evaluation = new VotingEvaluation();
            Genre genre1 = VotableFactory.buildGenre("Adventure");
            genre1.Vote();
            genre1.Vote();
            Genre genre2 = VotableFactory.buildGenre("Love Story");
            genre2.Vote();
            Genre genre3 = VotableFactory.buildGenre("Horror");

            ISet<Genre> set = new HashSet<Genre>();
            set.Add(genre1);
            set.Add(genre2);
            set.Add(genre3);

            ISet<Genre> expectedVoted = new HashSet<Genre>();
            expectedVoted.Add(genre1);
            expectedVoted.Add(genre2);

            Assert.IsTrue(containtsSame(expectedVoted, evaluation.evaluateGenre(set, EvaluationType.VOTED)));
        }

        [TestMethod()]
        public void CompareAllTest()
        {
            VotingEvaluation evaluation = new VotingEvaluation();
            Genre genre1 = VotableFactory.buildGenre("Adventure");
            genre1.Vote();
            genre1.Vote();
            Genre genre2 = VotableFactory.buildGenre("Love Story");
            genre2.Vote();
            Genre genre3 = VotableFactory.buildGenre("Horror");

            ISet<Genre> set = new HashSet<Genre>();
            set.Add(genre1);
            set.Add(genre2);
            set.Add(genre3);

            ISet<Genre> expectedAll = new HashSet<Genre>();
            expectedAll.Add(genre1);
            expectedAll.Add(genre2);
            expectedAll.Add(genre3);

            Assert.IsTrue(containtsSame(expectedAll, evaluation.evaluateGenre(set, EvaluationType.ALL)));
        }

        private bool containtsSame(ISet<Genre> expected, ISet<Genre> actual)
        {
            if (expected.Count != actual.Count) return false;

            var it = actual.GetEnumerator();

            foreach(Genre v in expected)
            {
                if (v.Equals(it.Current)) return false;
                it.MoveNext();
            }

            return true;
        }
    }
}