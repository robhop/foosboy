using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
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

        public Task<Player[]> GetPlayers(IResolverContext ctx, [Service] PlayerRepository repository)
        {
            var id = ctx.Parent<Match>().Id;

            return ctx.GroupDataLoader<int, Player>(nameof(repository.GetPlayersByMatch), repository.GetPlayersByMatch)
                 .LoadAsync(id, new System.Threading.CancellationToken());

        }
        public Task<Player[]> GetWinners(IResolverContext ctx, [Service] PlayerRepository repository)
        {
            return GetPlayers(ctx, repository)
                .ContinueWith(a => a.Result.Where(p => p.Result == Result.WIN).ToArray());
        }

        public Task<Player[]> GetLoosers(IResolverContext ctx, [Service] PlayerRepository repository)
        {
            return GetPlayers(ctx, repository)
                .ContinueWith(a => a.Result.Where(p => p.Result == Result.LOOSE).ToArray());
        }

    }
}