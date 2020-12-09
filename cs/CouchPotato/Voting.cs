using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CouchPotato
{
    using Lobby = LobbyUtil.Lobby;
    using UserFactory = UserUtil.UserFactory;
    using User = UserUtil.User;
    using Show = ShowUtil.Show;

    public partial class FormVoting : Form
    {
        private int showNumber;

        private Lobby lobby;
        private User user;
        private Show show;


        public FormVoting()
        {
            InitializeComponent();

            Control c = new Control();

            user = UserFactory.build("The Potato");

            lobby = c.createLobby(user);

            lobby.setConfiguration(ApiUtil.Provider.Aniflix, 5, 1);

            lobby.nextMode();

            lobby.swipeGenre(user.ID, "Action");

            lobby.nextMode();

            next();
        }

        private void BtnLike_Click(object sender, EventArgs e)
        {
            if (user.Swipes > 0)
            {
                lobby.swipeFilm(user.ID, show.Id);
                next();
                if (user.Swipes == 0)
                {
                    btnLike.Enabled = false;
                }
            }
        }

        private void BtnDislike_Click(object sender, EventArgs e)
        {
            next();
        }

        private void FrmVoting_Load(object sender, EventArgs e)
        {

        }

        private void next()
        {
            show = lobby.getNextShow(showNumber);

            if (show == null)
            {
                btnDislike.Enabled = false;
                btnLike.Enabled = false;
                return;
            }
            lblSwipes.Text = "Swipes: " + user.Swipes;
            lblTitle.Text = show.Name;
            txtDescription.Text = show.Description;

            this.BackgroundImage = new Bitmap(lobby.getCoverForShow(show.Id), new Size(this.Width, this.Height));

            showNumber++;
        }
    }
}
