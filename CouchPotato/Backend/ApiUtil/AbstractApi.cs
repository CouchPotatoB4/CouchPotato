using CouchPotato.Backend.ShowUtil;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace CouchPotato.Backend.ApiUtil
{
    public abstract class AbstractApi
    {
        protected HttpClient client = new HttpClient();
        protected string query;

        protected Genre[] genres;
        protected IList<Show> shows = new List<Show>();

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
            HttpResponseMessage response = get().Result;
            if (!response.IsSuccessStatusCode) throw new Exceptions.DeniedConnectionException(response.StatusCode);
            return true;
        }

        protected abstract Task<HttpResponseMessage> get(string header);

        protected string getResponseBody(string header)
        {
            var content = get(header).Result.Content;
            return content.ReadAsStringAsync().Result;
        }
    }   
}
