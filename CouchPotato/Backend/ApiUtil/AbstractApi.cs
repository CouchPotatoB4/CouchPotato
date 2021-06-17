using CouchPotato.Backend.ShowUtil;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace CouchPotato.Backend.ApiUtil
{
    public abstract class AbstractApi
    {
        protected HttpClient client = new HttpClient();
        protected string query;

        protected IList<Genre> genres = new List<Genre>();
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

        protected Image getCoverForShow(int id)
        {
            return getCoverForShow(id, "");
        }

        protected Image getCoverForShow(int id, string query)
        {
            return getCoverForShow(id, query, null);
        }

        protected Image getCoverForShow(int id, string query, IList<(HttpRequestHeader, string)> headers)
        {
            foreach (Show s in shows)
            {
                if (s.Id == id)
                {
                    string url = query + s.CoverPath;
                    try
                    {
                        HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                        if (headers != null)
                        {
                            foreach (var header in headers)
                            {
                                webRequest.Headers.Add(header.Item1, header.Item2);
                            }
                        }

                        using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponseAsync().Result)
                        {
                            var coverStream = response.GetResponseStream();
                            return new Bitmap(coverStream);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Can't load Image.");
                    }
                }
            }

            return null;
        }
    }   
}
