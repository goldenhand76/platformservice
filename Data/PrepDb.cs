using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                #pragma warning disable CS8604 // Possible null reference argument.
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
                #pragma warning restore CS8604 // Possible null reference argument.
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                Console.WriteLine("--> Attampting to apply migrations...");
                try
                {
                    context.Database.Migrate();

                }
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data...");
                context.Platforms.AddRange(
                    new Platform() {Name="Dotnet", Publisher="Microsoft", Cost="Free"},
                    new Platform() {Name="SQL Server Express", Publisher="Microsoft", Cost="Free"},
                    new Platform() {Name="Kubernetes", Publisher="Cloud Native Foundation", Cost="Free"}
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }

        }
    }
}