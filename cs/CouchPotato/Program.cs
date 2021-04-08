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
            FormVoting fV = new FormVoting(new Control());
            fV.ShowDialog();
        }


        private void UseCaseManyLobbies()
        {
            Console.WriteLine("UseCase many Lobbies");
            Control c = new Control();
            for (int  i = 0; i < 3; i++)
            {
                FormVoting fV = new FormVoting(c);
                fV.ShowDialog();
            }
        }
    }
}
