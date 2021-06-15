using CouchPotato.Backend.UserUtil;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.Backend.LobbyUtil
{
    public static class LobbyFactory
    {
        private static ISet<long> usedId = new SortedSet<long>();

        public static Lobby build(User host)
        {
            long id = createId();
            usedId.Add(id);

            return new Lobby(host, id);
        }

        private static long createId()
        {
            Random random = new Random();
            int returnValue = 0;
            
            do
            {
                returnValue = random.Next();
            }
            while (usedId.Contains(returnValue));
            
            return returnValue;
        }
    }
}
