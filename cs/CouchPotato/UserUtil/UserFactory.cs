﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.UserUtil
{
    static class UserFactory
    {
        private static ISet<long> usedId = new SortedSet<long>();

        public static User build(string name)
        {
            long id = createId();
            usedId.Add(id);

            return new User(id, name, 3);
        }

        private static long createId()
        {
            return usedId.Count + 1;
        }
    }
}
