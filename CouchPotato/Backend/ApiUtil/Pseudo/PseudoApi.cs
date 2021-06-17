using CouchPotato.Backend.ApiUtil;
using CouchPotato.Backend.ShowUtil;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CouchPotato.Backend.ApiUtil
{
    public class PseudoApi : AbstractApi, IApi
    {

        public PseudoApi() : base("")
        {
            buildGenres();
            buildShows();
        }

        public Genre[] getGenres()
        {
            return genres.ToArray();
        }

        public Show[] getShows()
        {
            return shows.ToArray<Show>();
        }

        public Show[] getShows(Genre genre)
        {
            List<Show> filteredShows = new List<Show>();
            foreach (var show in shows)
            {
                if (show.Genres.Contains(genre))
                {
                    filteredShows.Add(show);
                }
            }
            return filteredShows.ToArray();
        }

        public Show[] getFilteredShows(ISet<Genre> genres, IEnumerable<Show> shows)
        {
            List<Show> filteredShows = new List<Show>();
            foreach (var show in shows)
            {
                foreach (var genre in genres)
                {
                    if (show.Genres.Contains(genre))
                    {
                        filteredShows.Add(show);
                        break;
                    }
                }
            }
            return filteredShows.ToArray();
        }

        public Show[] getFilteredShows(ISet<Genre> genres)
        {
            return getFilteredShows(genres, shows);
        }

        public Show[] loadFilteredPage(int page, ISet<Genre> genres)
        {
            return getFilteredShows(genres);
        }

        protected override Task<HttpResponseMessage> get(string header)
        {
            return null;
        }

        public Image getCoverForShow(int id)
        {
            return base.getCoverForShow(id);
        }

        private void buildGenres()
        {
            genres = new Genre[13];
            genres[0] = VotableFactory.buildGenre(0,"Horror");
            genres[1] = VotableFactory.buildGenre(1,"Fantasy");
            genres[2] = VotableFactory.buildGenre(3,"Blogbuster");
            genres[3] = VotableFactory.buildGenre(2,"Mystery & Thriller");
            genres[4] = VotableFactory.buildGenre(5,"Action & Adventure");
            genres[5] = VotableFactory.buildGenre(8678,"Romantik");
            genres[6] = VotableFactory.buildGenre(76344,"Krimi");
            genres[7] = VotableFactory.buildGenre(8868,"Drama");
            genres[8] = VotableFactory.buildGenre(867867,"Science-Fiction");
            genres[9] = VotableFactory.buildGenre(546,"Komödie");
            genres[10] = VotableFactory.buildGenre(767,"Animation");
            genres[11] = VotableFactory.buildGenre(354,"Western");
            genres[12] = VotableFactory.buildGenre(87,"Adult");
        }

        private void buildShows()
        {
            shows = new Show[10];
            shows[0] = VotableFactory.buildShow(0, "", "", "");
            shows[0].Genres.UnionWith(new Genre[] { });

            shows[0] = VotableFactory.buildShow(0, "Loki", "Der quecksilbrige Bösewicht Loki nimmt nach den Ereignissen von \"Avengers\" seine Rolle als Gott des Unheils wieder auf: Endgame.", "https://images.justwatch.com/poster/246443794/s276/Staffel-1.webp");
            shows[0].Genres.UnionWith(new Genre[] { genres[8], genres[4], genres[7], genres[6], genres[1] });

            shows[1] = VotableFactory.buildShow(1, "Ragnarök", "In einer norwegischen Kleinstadt sorgen nicht nur schmelzende Polarkappen für Endzeitstimmung. Nur ein sagenumwobener Held kann das Böse besiegen.", "https://images.justwatch.com/poster/172395521/s276/Ragnaroek.webp");
            shows[1].Genres.UnionWith(new Genre[] { genres[1], genres[7], genres[8] });

            shows[2] = VotableFactory.buildShow(2, "Love, Death & Robots", "Die einzelnen Episoden haben jeweils eine abgeschlossene Handlung, die auf den Kurzgeschichten verschiedener internationaler Schriftsteller basieren. Die Drehbücher wurden hauptsächlich von Philip Gelatt verfasst und von 15 verschiedenen Animationsstudios realisiert. Alle spielen in fiktiven Zukunfts-Utopien und -dystopien und beschäftigen sich mit Gewalt, Sex, Robotern, diversen künstlichen Intelligenzen und Katzen. Die Serie richtet sich somit an erwachsene Zuschauer. Elemente des Cyberpunks sowie des Horrorfilms finden ebenso Verwendung wie schwarzer Humor oder gesellschaftskritische Inhalte.", "https://images.justwatch.com/poster/245207620/s276/Love-Death-und-Robots.webp");
            shows[2].Genres.UnionWith(new Genre[] { genres[8], genres[0], genres[10], genres[9], genres[1]});

            shows[3] = VotableFactory.buildShow(3, "The Walking Dead", "The Walking Dead spielt bislang in der Metropolregion von Atlanta, im Südosten der Vereinigten Staaten, und erzählt vom Kampf einer kleinen Gruppe Überlebender nach einer weltweiten Zombie-Apokalypse. Unter Führung des Sheriffs Rick Grimes ist die Gruppe auf der Suche nach einer dauerhaften und vor allem sicheren Bleibe. Dabei stellen die fast überall präsenten Untoten eine permanent vorhandene Bedrohung dar, die jederzeit ohne Vorwarnung zuschlagen kann. Im Laufe der Zeit wird die Gruppe daher zwangsläufig immer kleiner, da manche Mitglieder den Zombies zum Opfer fallen, andere die Gruppe verlassen oder sich für den Tod entscheiden.", "https://images.justwatch.com/poster/8587552/s276/The-Walking-Dead.webp");
            shows[3].Genres.UnionWith(new Genre[] { genres[8], genres[4], genres[7], genres[0], genres[3] });

            shows[4] = VotableFactory.buildShow(4, "Spider-Man: Far From Home", "Nach der epischen Schlacht gegen Thanos und den schicksalshaften Ereignissen um die Avengers ist Peter zurück an seiner alten Schule. Um vom Superheldendasein etwas auszuruhen, kommt ihm eine Klassenfahrt nach Europa gerade recht, auch um seiner heimlichen Liebe \"MJ\" endlich seine Gefühle gestehen zu können. Die erste Station ist Venedig in Italien. Nach einer entspannten Gondelfahrt mit der Reisegruppe bricht aber plötzlich das Chaos los: Ein riesiges Monster kommt aus dem Wasser und greift die Stadt an. Dem stellt sich ein bisher unbekannter Superheld namens Mysterio entgegen. Und als wäre das nicht genug, taucht dann noch Nick Fury auf, um Spider-Man für eine Mission zu rekrutieren...", "https://images.justwatch.com/poster/156755327/s276/Spider-Man-Far-from-Home.webp");
            shows[4].Genres.UnionWith(new Genre[] { genres[8] , genres[4], genres[9]});

            shows[5] = VotableFactory.buildShow(5, "Rambo - Last Blood", "John Rambo hat viele große Schlachten in seinem Leben geschlagen – nun soll endlich Schluss sein. Zurückgezogen lebt der Kriegsveteran inzwischen auf einer abgelegenen Farm in Arizona. Doch der einstige Elitekämpfer kommt nicht zur Ruhe. Als die Enkeltochter seiner Haushälterin Maria verschleppt wird, begibt sich Rambo auf eine Rettungsmission nach Mexiko. Schon bald sieht er sich dort einem der mächtigsten und skrupellosesten Drogenkartelle gegenüber. Die vielen Jahre im Kampf mögen Rambo gezeichnet haben, aber sie haben ihn nicht weniger gefährlich gemacht…", "https://images.justwatch.com/poster/165682317/s276/Rambo-Last-Blood.webp");
            shows[5].Genres.UnionWith(new Genre[] { genres[3], genres[7], genres[4], genres[11]});

            shows[6] = VotableFactory.buildShow(6, "Oxygen", "Eine Frau erwacht ohne jegliche Erinnerungen in einer Kältekapsel. Der Sauerstoff wird knapp. Um zu überleben, muss sie sich irgendwie daran erinnern, wer sie ist.", "https://images.justwatch.com/poster/244791652/s276/Oxygene.webp");
            shows[6].Genres.UnionWith(new Genre[] { genres[3], genres[7], genres[1], genres[8]});

            shows[7] = VotableFactory.buildShow(7, "Rick and Morty", "Rick ist ein geistig-unausgeglicher, aber wissenschaftlich begabter alter Mann, der vor kurzem mit seiner Familie wiedervereint wurde. Er verbringt die meiste Zeit damit seinen jungen Enkel Morty in gefährliche Abenteuer im gesamten Weltraum oder alternativen Universen zu beteiligen. Zusammen mit Morty's bereits instabilen Familienleben führen diese Ereignisse zu vielen Sorgen Zuhause und in der Schule.", "https://images.justwatch.com/poster/246361950/s276/Rick-and-Morty.webp");
            shows[7].Genres.UnionWith(new Genre[] { genres[9], genres[8], genres[4], genres[10]});

            shows[8] = VotableFactory.buildShow(8, "South Park", "Die Serie konzentriert sich hauptsächlich auf die Abenteuer der vier Kinder Stan Marsh, Kyle Broflovski, Eric Cartman und Kenny McCormick in der fiktionalen Kleinstadt South Park in den Rocky Mountains in den USA der Gegenwart. Neben den vier Hauptcharakteren wird die Stadt von einer Reihe von zusätzlichen Charakteren bevölkert.", "https://images.justwatch.com/poster/246249783/s276/South-Park.webp");
            shows[8].Genres.UnionWith(new Genre[] { genres[10], genres[9]});

            shows[9] = VotableFactory.buildShow(9, "How I Met Your Mother", "Ted Mosby erzählt seinem Sohn und seiner Tochter von den Ereignissen, die dazu geführt haben, dass er ihre Mutter getroffen hat.", "https://images.justwatch.com/poster/8602772/s276/How-I-Met-Your-Mother.webp");
            shows[9].Genres.UnionWith(new Genre[] { genres[9], genres[5]});
        }
    }
}
