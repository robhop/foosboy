using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class PlayerRepository : BaseRepository
    {
        public PlayerRepository(FoosBoyContex contex) : base(contex)
        {
        }

        public IQueryable<Player> GetPlayers()
        {
            return dbContext.Players;
        }

        public Task<Team> GetTeamByIdAsync(int id)
        {
            return dbContext.Teams
                .Include(t => t.PlayerA)
                .Include(t => t.PlayerB)
                .SingleAsync(t => t.Id == id);
        }

        public Player AddPlayer(string name)
        {
            var player = new Player {Name = name};
            var teams = dbContext.Players
                .Select(p => new Team{PlayerA = p, PlayerB = player}).ToList()
                .Append(new Team{PlayerA = player, PlayerB = player}).ToList();
            dbContext.Players.Add(player);
            dbContext.Teams.AddRange(teams);
            dbContext.SaveChanges();
            return player;
        }

        public int DeletePlayer(int id)
        {
            var player = dbContext.Players.Single(p => p.Id == id);
            var temas = dbContext.Teams.Where(t => t.PlayerA == player || t.PlayerB == player).ToList();

            dbContext.RemoveRange(temas);
            dbContext.Remove(player);
            return dbContext.SaveChanges();
        }

        public Task<Player> GetPlayerByIdAsync(int id)
        {
            return dbContext.Players.SingleAsync(p => p.Id == id);
        }

        public IQueryable<Team> GetTeams()
        {
            return dbContext.Teams.Include(t => t.PlayerA).Include(t => t.PlayerB);
        }
    }
}