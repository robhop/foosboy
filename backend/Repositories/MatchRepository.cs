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
            // return dbContext.Matches.SingleAsync(m => m.Id == id);
            var q = dbContext.Matches.AsQueryable();
            q = q.Include(m => m.Winner).ThenInclude(t => t.PlayerA);
            q = q.Include(m => m.Winner).ThenInclude(t => t.PlayerB);
            q = q.Include(m => m.Looser).ThenInclude(t => t.PlayerB);
            q = q.Include(m => m.Looser).ThenInclude(t => t.PlayerB);
            return q.Where(m => m.Id == id).SingleAsync();
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
                Single(t => ((t.PlayerA.Id == winnerA || t.PlayerA.Id == winnerB) && (t.PlayerB.Id == winnerA || t.PlayerB.Id == winnerB)));
            var looser = dbContext.Teams.
                Single(t => ((t.PlayerA.Id == looserA || t.PlayerA.Id == looserB) && (t.PlayerB.Id == looserA || t.PlayerB.Id == looserB)));

            var match = new Match { Winner = winner, Looser = looser, Timestamp = DateTime.UtcNow };
            dbContext.Matches.Add(match);
            dbContext.SaveChanges();
            var q = dbContext.Matches.Where(m => m.Id == match.Id);
            q = q.Include(m => m.Winner).ThenInclude(t => t.PlayerA);
            q = q.Include(m => m.Winner).ThenInclude(t => t.PlayerB);
            q = q.Include(m => m.Looser).ThenInclude(t => t.PlayerB);
            q = q.Include(m => m.Looser).ThenInclude(t => t.PlayerB);
            return q.Single();
        }
        public IQueryable<Match> GetMatches()
        {
            var q = dbContext.Matches.AsQueryable();
            q = q.Include(m => m.Winner).ThenInclude(t => t.PlayerA);
            q = q.Include(m => m.Winner).ThenInclude(t => t.PlayerB);
            q = q.Include(m => m.Looser).ThenInclude(t => t.PlayerB);
            q = q.Include(m => m.Looser).ThenInclude(t => t.PlayerB);
            return q.AsQueryable();
        }
    }
}