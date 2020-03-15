using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Repositories;
using backend.Types;
using HotChocolate;
using HotChocolate.Resolvers;

namespace backend.Resolvers
{
    public class MatchResolver
    {
        public IQueryable<Match> GetMatches([Service] MatchRepository repository, IResolverContext ctx)
        {
            return repository.GetMatches();
        }

        public Match CreateMatch([Service] MatchRepository repository, MatchInput input)
        {
            return repository.CreateMatch(input.Winners(), input.Loosers());
        }

        public int DeleteMatchAsync([Service] MatchRepository repository, MatchDelete input)
        {
            return repository.DeleteMatch(input.GetId());
        }

        public MatchEnum GetType(IResolverContext ctx)
        {
            if (ctx.Parent<Match>().Plays.Count > 2)
                return MatchEnum.DOUBLE;
            else
                return MatchEnum.SINGLE;
        }

        public IEnumerable<Player> GetWinners(IResolverContext ctx)
        {
            return ctx.Parent<Match>().Plays.Where(p => p.Result == Result.WIN).Select(p => p.Player);
        }

        public IEnumerable<Player> GetLoosers(IResolverContext ctx)
        {
            return ctx.Parent<Match>().Plays.Where(p => p.Result == Result.LOOSE).Select(p => p.Player);
        }
    }
}