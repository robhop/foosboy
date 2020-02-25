using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class MatchRepository : BaseRepository
    {
        public MatchRepository(FoosBoyContex contex) : base(contex)
        {
        }

        public Task<Match> GetMatchByIdAsync(int id)
        {
            return dbContext.Matches.SingleAsync(m => m.Id == id);
        }

        public int DeleteMatch(int id)
        {
            var match = dbContext.Matches.Single(m => m.Id == id);
            dbContext.Remove(match);
            return dbContext.SaveChanges();
        }
        public Match CreateMatch(int winnerA, int winnerB, int looserA, int looserB)
        {
            var winner = dbContext.Teams.
                SingleAsync(t => ((t.PlayerA.Id == winnerA || t.PlayerA.Id == winnerB) && (t.PlayerB.Id == winnerA || t.PlayerB.Id == winnerB)));
            var looser = dbContext.Teams.
                SingleAsync(t => ((t.PlayerA.Id == looserA || t.PlayerA.Id == looserB) && (t.PlayerB.Id == looserA || t.PlayerB.Id == looserB)));
            
            var match = new Match {Winner = winner.Result, Looser = looser.Result, Timestamp = DateTime.UtcNow};
            dbContext.Matches.Add(match);
            dbContext.SaveChanges();
            return match; 
        }
        public IQueryable<Match> GetMatches()
        {
            return dbContext.Matches.Include(t => t.Winner).Include(t => t.Looser);
        }
    }
}