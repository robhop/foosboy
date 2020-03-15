using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
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
        public Task<ILookup<int, Player>> GetPlayersByMatch(IReadOnlyList<int> keys)
        {
            return Task<ILookup<int, Player>>.Factory.StartNew(() =>
            {
                var data = (from p in dbContext.Players
                            join pl in dbContext.Plays on p.Id equals pl.PlayerId
                            where keys.Contains(pl.MatchId)
                            select new { player = p, MatchId = pl.MatchId, result = pl.Result })
                        .AsNoTracking();
                return data.ToLookup(
                    item => item.MatchId,
                    item =>
                    {
                        item.player.Result = item.result;
                        return item.player;
                    });
            });

        }
    }
}