using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.ApiUtil.Aniflix
{
    class ShowJson
    {
        public int id { get; set; }
        public string name { get; set; }
        public string name_alt { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string cover_landscape { get; set; }
        public string cover_landscape_original { get; set; }
        public string cover_portrait { get; set; }
        public string visible_since { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string deleted_at { get; set; }
        public int howManyAbos { get; set; }
        public int seasonCount { get; set; }
        public string rating { get; set; }
        public int airing { get; set; }
    }

    class GenreWithShowsJson
    {
        public int id { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }

        public string updated_at { get; set; }
        public string deleted_at { get; set; }
        public IList<ShowJson> shows { get; set; }
    }
}
