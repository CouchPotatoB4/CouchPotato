using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.ApiUtil.Exceptions
{
    class ApiChangedException : Exception
    {
        public ApiChangedException() : base() { }
       
        public ApiChangedException(string message) : base(message) { }

        public ApiChangedException(string message, Exception e) : base(message, e) { }

        public ApiChangedException(string request, string url) : base("Error in the " + request + " request, with the URL " + url + " . ") { }

        public ApiChangedException(string request, string url, Exception e) : base("Error in the " + request + " request, with the URL " + url + " . ", e) { }
    }
}
