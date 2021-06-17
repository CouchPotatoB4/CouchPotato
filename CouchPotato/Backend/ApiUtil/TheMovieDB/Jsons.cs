using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouchPotato.Backend.ApiUtil.TheMovieDB
{
    public class GenreJsonRoot
    {
        public GenreJson[] genres { get; set; }
    }

    public class GenreJson
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class ShowJsonRoot
    {
        public int page { get; set; }
        public ShowJson[] results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
    public class ShowJson
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public int[] genre_ids { get; set; }
        public int id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
    }

    //Amazon Prime and Netflix
    public class ProviderJsonRoot
    {
        public int id { get; set; }
        public ProviderJson results { get; set; }
    }

    public class ProviderJson
    {
        //public AR AR { get; set; }
        //public AT AT { get; set; }
        //public AU AU { get; set; }
        //public BE BE { get; set; }
        //public CA CA { get; set; }
        //public CH CH { get; set; }
        //public CL CL { get; set; }
        //public CO CO { get; set; }
        public CountryJson DE { get; set; }
        //public EC EC { get; set; }
        //public EE EE { get; set; }
        //public ES ES { get; set; }
        //public FI FI { get; set; }
        //public FR FR { get; set; }
        //public GB GB { get; set; }
        //public ID ID { get; set; }
        //public IE IE { get; set; }
        //public IN IN { get; set; }
        //public IT IT { get; set; }
        //public KR KR { get; set; }
        //public LT LT { get; set; }
        //public LV LV { get; set; }
        //public MX MX { get; set; }
        //public MY MY { get; set; }
        //public NL NL { get; set; }
        //public NO NO { get; set; }
        //public NZ NZ { get; set; }
        //public PE PE { get; set; }
        //public PT PT { get; set; }
        //public RU RU { get; set; }
        //public SE SE { get; set; }
        //public SG SG { get; set; }
        //public TH TH { get; set; }
        //public US US { get; set; }
        //public VE VE { get; set; }
        //public ZA ZA { get; set; }
    }

    public class CountryJson
    {
        public string link { get; set; }
        //public RBFJson[] rent { get; set; }
        //public RBFJson[] buy { get; set; }
        public RBFJson[] flatrate { get; set; }
    }

    public class RBFJson
    {
        public int display_priority { get; set; }
        public string logo_path { get; set; }
        public int provider_id { get; set; }
        public string provider_name { get; set; }
    }
}
