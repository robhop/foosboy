using System;
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
            return repository.CreateMatch(input.WinnerA(), input.WinnerB(), input.LooserA(), input.LooserB());
        }

        public int DeleteMatchAsync([Service] MatchRepository repository, MatchDelete input)
        {
            return repository.DeleteMatch(input.GetId());
        }

        public MatchEnum GetType(IResolverContext ctx)
        {
            var a = ctx.Parent<Match>().Winner.PlayerA;
            var b = ctx.Parent<Match>().Winner.PlayerB;
            if (a == b || b == null)
                return MatchEnum.SINGLE;
            else
                return MatchEnum.DOUBLE;
        }
    }
}