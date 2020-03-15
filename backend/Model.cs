
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class FoosBoyContex : DbContext
    {
        // public FoosBoyContex(DbContextOptions<FoosBoyContex> options) : base(options)
        // { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<Match> Matches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=foosboy.db");
    }

    public class Player
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Avatar { get; set; }

    }


    public enum Result
    {
        LOOSE = 0,
        WIN = 1
    }
    public class Play
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public Match Match { get; set; }
        public Result Result { get; set; }
    }

    public class Match
    {
        public int Id { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }

        public List<Play> Plays { get; set; }
    }
}