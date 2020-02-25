using System.Linq;
using backend.Repositories;
using HotChocolate;
using HotChocolate.Resolvers;

namespace backend.Resolvers
{
    public class TeamResolver
    {
        public IQueryable<Team> GetTeams([Service] PlayerRepository repository, IResolverContext ctx)
        {
            return repository.GetTeams();
        }
    }
}