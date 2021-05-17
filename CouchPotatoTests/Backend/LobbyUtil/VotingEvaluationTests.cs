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
        public void CompareTest()
        {
            VotingEvaluation evaluation = new VotingEvaluation();
            Genre genre1 = VotableFactory.buildGenre("Adventure");
            genre1.Vote();
            genre1.Vote();
            Genre genre2 = VotableFactory.buildGenre("Love Story");
            genre2.Vote();

            Assert.AreEqual(1, evaluation.Compare(genre1, genre2));

            genre2.Vote();
            Assert.AreEqual(0, evaluation.Compare(genre1, genre2));
        }
    }
}