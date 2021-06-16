using CouchPotato.Backend;
using CouchPotato.Backend.ApiUtil;
using CouchPotato.Backend.LobbyUtil;
using CouchPotato.Backend.ShowUtil;
using CouchPotato.Backend.UserUtil;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouchPotato
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //TEST
            //var u = UserFactory.build("Host");
            //var lobby = Control.createLobby(u);
            //lobby.setConfiguration(Provider.TheMovieDB.getApi(), 3, 1);

            //lobby.nextMode();

            //lobby.swipeGenre(u.ID, "Action");

            //lobby.nextMode();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
