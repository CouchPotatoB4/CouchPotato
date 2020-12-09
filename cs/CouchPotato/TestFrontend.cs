using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CouchPotato.ApiUtil;

namespace CouchPotato
{
    using Provider = CouchPotato.ApiUtil.Provider;
    using Film = CouchPotato.ShowUtil.Show;
    using AniflixApi = CouchPotato.ApiUtil.Aniflix.AniflixApi;

    public partial class TestFrontend : Form
    {
        Provider prov = Provider.Aniflix;

        public TestFrontend()
        {
            InitializeComponent();
        }

        private void BtnGet_Click(object sender, EventArgs e)
        {
            var r = ((AniflixApi)prov.getApi()).getStatusCode();
            txtStatus.Text = r.ToString(); 
        }

        private void BtnGetFilm_Click(object sender, EventArgs e)
        {
            var shows = prov.getApi().getShows();

            foreach (var show in shows)
            {
                Console.WriteLine(show.Name);
            }
        }
    }
}
