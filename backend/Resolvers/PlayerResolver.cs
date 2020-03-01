using System;
using System.Linq;
using backend.Repositories;
using backend.Types;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;

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
    }
}