using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Domain;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating( modelBuilder ); //importante para que la migracion nos genere un nuevo id
            
            modelBuilder
                .Entity<Channel>()
                .HasData( new Channel{
                    id = Guid.NewGuid(),
                    name = "DotnetCore",
                    description = "Canal dedicado a dotnet core"
                },
                new Channel{
                    id = Guid.NewGuid(),
                    name = "Angular",
                    description = "Canal dedicado a Angular"
                },
                new Channel{
                    id = Guid.NewGuid(),
                    name = "ReactJs",
                    description = "Canal dedicado a ReactJs"
                });
                
            //definimos las relaciones de nuestras tablas y creamos una nueva migration
            modelBuilder.Entity<Message>()
                .HasOne(x => x.Sender)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.SenderId);
        }
        
    }
}