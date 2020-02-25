
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class FoosBoyContex : DbContext
    {
        // public FoosBoyContex(DbContextOptions<FoosBoyContex> options) : base(options)
        // { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=foosboy.db");
    }

    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public Player PlayerA { get; set; }
        public Player PlayerB { get; set; }
    }

    public class Match
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Team Winner { get; set; }
        public Team Looser { get; set; }
    }
}