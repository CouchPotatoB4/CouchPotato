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
        public TestFrontend()
        {
            InitializeComponent();
        }

        public void setBackgroundImage(Image image)
        {
            this.BackgroundImage = image;
        }
    }
}
