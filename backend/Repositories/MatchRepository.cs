using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using backend.Types;
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

        public Task<IReadOnlyDictionary<int, PlayerStats>> GetMatchStatsByPlayerId(IReadOnlyList<int> keys)
        {
            return Task<IReadOnlyDictionary<int, PlayerStats>>.Factory.StartNew(() =>
            {
                var data = (from pl in dbContext.Plays
                            where keys.Contains(pl.PlayerId)
                            select pl).ToArray();

                return data.GroupBy(d => d.PlayerId)
                    .ToDictionary(gr => gr.Key, gr => new PlayerStats
                    {
                        GameCount = gr.Count(),
                        GameWinCount = gr.Where(p => p.Result == Result.WIN).Count(),
                        GameLooseCount = gr.Where(p => p.Result == Result.LOOSE).Count()
                    });
            });
        }

        public Task<ILookup<int, Match>> GetMatchesByPlayerId(IReadOnlyList<int> keys)
        {
            return Task<ILookup<int, Match>>.Factory.StartNew(() =>
            {
                var data = (from m in dbContext.Matches
                            join pl in dbContext.Plays on m.Id equals pl.MatchId
                            where keys.Contains(pl.PlayerId)
                            select new { Match = m, PlayerId = pl.PlayerId, result = pl.Result })
                        .AsNoTracking();
                return data.ToLookup(
                    item => item.PlayerId,
                    item =>
                    {
                        item.Match.Result = item.result;
                        return item.Match;
                    });
            });

        }

        public IQueryable<Match> GetMatches()
        {
            return q.OrderByDescending(m => m.Timestamp);
        }
    }
}