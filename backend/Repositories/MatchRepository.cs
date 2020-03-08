using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class MatchRepository : BaseRepository
    {
        private IQueryable<Match> q;

        public MatchRepository(FoosBoyContex contex) : base(contex)
        {
            q = dbContext.Matches.AsQueryable();
            q = q.Include(m => m.Winner).ThenInclude(t => t.PlayerA);
            q = q.Include(m => m.Winner).ThenInclude(t => t.PlayerB);
            q = q.Include(m => m.Looser).ThenInclude(t => t.PlayerB);
            q = q.Include(m => m.Looser).ThenInclude(t => t.PlayerB);
        }

        public Task<Match> GetMatchByIdAsync(int id)
        {
            return q.SingleAsync(m => m.Id == id);
        }

        public int DeleteMatch(int id)
        {
            var match = dbContext.Matches.Single(m => m.Id == id);
            dbContext.Remove(match);
            return dbContext.SaveChanges();
        }
        public Match CreateMatch(int winnerA, int winnerB, int looserA, int looserB)
        {

            var winners = new int[] {winnerA, winnerB};
            var loosers = new int[] {looserA, looserB};
        
            var winner = dbContext.Teams.
                SingleAsync(t => t.PlayerA.Id == winners.Min() && t.PlayerB.Id == winners.Max());

            var looser = dbContext.Teams.
                SingleAsync(t => t.PlayerA.Id == loosers.Min() && t.PlayerB.Id == loosers.Max());

            var match = new Match { Winner = winner.Result, Looser = looser.Result, Timestamp = DateTime.UtcNow };

            dbContext.Matches.Add(match);
            dbContext.SaveChanges();
            return  q.Single(m => m.Id == match.Id);
        }
        public IQueryable<Match> GetMatches()
        {
            return q.OrderByDescending(m => m.Timestamp);
        }
    }
}