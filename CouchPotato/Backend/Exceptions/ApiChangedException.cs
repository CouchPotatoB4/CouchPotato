using CouchPotato.Backend.ApiUtil;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.Backend.Exceptions
{
    public class ApiChangedException : Exception
    {
        private Provider provider;

        public Provider Provider
        {
            get { return provider; }
        }

        public ApiChangedException() : base() { }
       
        public ApiChangedException(Provider provider, string message) : base("Error at provider " + provider.ToString() + ".\n" + message)
        {
            this.provider = provider;
        }

        public ApiChangedException(Provider provider, string message, Exception e) : base("Error at provider " + provider.ToString() + ".\n" + message, e) 
        {
            this.provider = provider;
        }
    }
}
