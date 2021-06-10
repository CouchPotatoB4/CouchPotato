using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace CouchPotato.Backend.Exceptions
{
    public class DeniedConnectionException : Exception
    {
        private HttpStatusCode statusCode;

        public HttpStatusCode StatusCode
        {
            get { return statusCode; }
        }

        public DeniedConnectionException(HttpStatusCode statusCode) : base(statusCode.ToString())
        {
            this.statusCode = statusCode;
        }
    }
}
