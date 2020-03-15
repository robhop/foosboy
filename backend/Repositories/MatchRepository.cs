using System;
using System.Collections.Generic;
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
            q = q.Include(m => m.Plays).ThenInclude(p => p.Player);
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
        public Match CreateMatch(IEnumerable<int> winners, IEnumerable<int> loosers)
        {
            var winnerPlayers = winners.Select(w => dbContext.Players.Single(p => p.Id == w));
            var looserPlayers = loosers.Select(l => dbContext.Players.Single(p => p.Id == l));

            var match = new Match { Timestamp = DateTime.UtcNow };

            var winnerPlays = winnerPlayers.Select(p => new Play { Match = match, Player = p, Result = Result.WIN });
            var looserPlays = looserPlayers.Select(p => new Play { Match = match, Player = p, Result = Result.LOOSE });

            dbContext.Matches.Add(match);
            dbContext.Plays.AddRange(winnerPlays);
            dbContext.Plays.AddRange(looserPlays);

            dbContext.SaveChanges();

            return q.Single(m => m.Id == match.Id);
        }
        public IQueryable<Match> GetMatches()
        {
            return q.OrderByDescending(m => m.Timestamp);
        }
    }
}