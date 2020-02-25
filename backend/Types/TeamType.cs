
using backend.Repositories;
using HotChocolate;
using HotChocolate.Types;

namespace backend.Types
{

    public class TeamType : ObjectType<Team>
    {
        protected override void Configure(IObjectTypeDescriptor<Team> descriptor)
        {

            descriptor.AsNode()
                .IdField(t => t.Id)
                .NodeResolver((ctx, id) => ctx.Service<PlayerRepository>().GetTeamByIdAsync(id));
        }
    }
}