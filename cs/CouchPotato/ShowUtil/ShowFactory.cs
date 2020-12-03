using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.ShowUtil
{
    class ShowFactory
    {
        public static Show build(int id, string name, string description)
        {
            return new Show(id, name, description);
        }
    }
}
