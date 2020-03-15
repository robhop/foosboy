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


        public Player AddPlayer(string name, string avatar)
        {
            var player = new Player { Name = name, Avatar = avatar };
            dbContext.Players.Add(player);
            dbContext.SaveChanges();
            return player;
        }

        public int DeletePlayer(int id)
        {
            var player = dbContext.Players.Single(p => p.Id == id);
            dbContext.Remove(player);
            return dbContext.SaveChanges();
        }

        public Task<Player> GetPlayerByIdAsync(int id)
        {
            return dbContext.Players.SingleAsync(p => p.Id == id);
        }

    }
}