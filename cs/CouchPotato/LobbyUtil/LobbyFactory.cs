using CouchPotato.UserUtil;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.LobbyUtil
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
            return usedId.Count + 1;
        }
    }
}
