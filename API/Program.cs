using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using Domain;
using Persistence;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //iniciamos nuestras migraciones por codigo
            using ( var scope = host.Services.CreateScope() )//crea una instancia scope
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<AppUser>>();

                try{    

                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();

                    Seed.SeedData(userManager).Wait();

                }catch( Exception ex ){
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ocurrio un error durante la migración");
                }
            }

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
