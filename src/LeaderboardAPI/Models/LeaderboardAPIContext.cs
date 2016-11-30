using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LeaderboardAPI.Models
{
    public class LeaderboardAPIContext : DbContext
    {
        public DbSet<Record>Records {get; set;}

        // Load up the local SQLite3 database file.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Leaderboard.sq3");
        }
    }
}