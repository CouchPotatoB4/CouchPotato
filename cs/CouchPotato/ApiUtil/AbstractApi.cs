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
        protected static int PAGE_SIZE = 20;

        protected HttpClient client = new HttpClient();
        protected string query;

        public AbstractApi(string query)
        {
            this.query = query;
        }

        //HAS TO BE PROTECTED but for testing its public

        public Task<HttpResponseMessage> get()
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
