
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Domain
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> ctx) : base(ctx) { }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(g => g.Title).HasMaxLength(200);
            modelBuilder.Entity<Game>().Property(g => g.GameLength).HasMaxLength(25);
            modelBuilder.Entity<Game>().Property(g => g.Description).HasMaxLength(1500);
        }
    }
}
