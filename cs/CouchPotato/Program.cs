using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }


        public Program()
        {
            UseCaseMovieVotingAndSwiping();
        }


        private void UseCaseMovieVotingAndSwiping()
        {
            Console.WriteLine("UseCase Movie Voting and Swiping.");
            FormVoting fV = new FormVoting();
            fV.ShowDialog();
        }
    }
}
