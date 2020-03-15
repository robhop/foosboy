using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using backend.Repositories;
using backend.Types;
using HotChocolate;
using HotChocolate.Resolvers;

namespace backend.Resolvers
{
    public class PlayerResolver
    {
        public IQueryable<Player> GetPlayers([Service] PlayerRepository repository, IResolverContext ctx)
        {
            return repository.GetPlayers();
        }

        public Player CreatePlayerAsync([Service] PlayerRepository repository, PlayerInput input)
        {
            return repository.AddPlayer(input.Name, input.Avatar);
        }

        public int DeletePlayerAsync([Service] PlayerRepository repository, PlayerDelete input)
        {
            return repository.DeletePlayer(input.GetId());
        }

        public Task<Match[]> GetMatches(IResolverContext ctx, [Service] MatchRepository repository, [Parent] Player player)
        {
            return ctx.GroupDataLoader<int, Match>(nameof(repository.GetMatchesByPlayerId), repository.GetMatchesByPlayerId)
                 .LoadAsync(player.Id, new System.Threading.CancellationToken());
        }

        public Task<PlayerStats> GetPlayerStats(IResolverContext ctx, [Service] MatchRepository repository, [Parent] Player player)
        {
            return ctx.BatchDataLoader<int, PlayerStats>(nameof(repository.GetMatchStatsByPlayerId), repository.GetMatchStatsByPlayerId)
                .LoadAsync(player.Id, new System.Threading.CancellationToken());
        }
    }
}