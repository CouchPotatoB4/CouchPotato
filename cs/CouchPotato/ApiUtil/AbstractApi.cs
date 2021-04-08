using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace CouchPotato.ApiUtil
{
    abstract class AbstractApi
    {
        protected HttpClient client = new HttpClient();
        protected string query;

        public AbstractApi(string query)
        {
            this.query = query;
        }

        protected Task<HttpResponseMessage> get()
        {
            return client.GetAsync(query);
        }
        
        public int getStatusCode()
        {
            return get().Result.StatusCode.GetHashCode();
        }

        protected bool isStatusCodeOk()
        {
            return get().Result.IsSuccessStatusCode;
        }

        protected abstract Task<HttpResponseMessage> get(string header);
    }   
}
