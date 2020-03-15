
using backend.Models;
using backend.Repositories;
using backend.Resolvers;
using HotChocolate;
using HotChocolate.Types;

namespace backend.Types
{

    public class PlayerType : ObjectType<Player>
    {
        protected override void Configure(IObjectTypeDescriptor<Player> descriptor)
        {
            descriptor.BindFieldsExplicitly();

            descriptor.Name("Player");

            descriptor.AsNode()
                .IdField(t => t.Id)
                .NodeResolver((ctx, id) => ctx.Service<PlayerRepository>()
                .GetPlayerByIdAsync(id));

            descriptor.Field(t => t.Name)
                .Type<NonNullType<StringType>>();

            descriptor.Field(t => t.Avatar)
                .Type<NonNullType<StringType>>();

            descriptor.Field<PlayerResolver>(r => r.GetMatches(null, null, null))
                .Type<NonNullType<ListType<NonNullType<MatchType>>>>();

            descriptor.Field<PlayerResolver>(r => r.GetPlayerStats(null, null, null))
                .Type<PlayerStatsType>();

        }
    }
}