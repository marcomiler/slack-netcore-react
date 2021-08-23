using System;
using Microsoft.EntityFrameworkCore;

using Domain;

namespace Persistence
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Channel> Channels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
        
    }
}