using backend.Resolvers;
using HotChocolate.Types;

namespace backend.Types
{
    public class QueryType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("Query");

            descriptor.Field<PlayerResolver>(t => t.GetPlayers(default, default))
                .Name("players")
                .Type<NonNullType<ListType<NonNullType<PlayerType>>>>();

            descriptor.Field<TeamResolver>(t => t.GetTeams(default, default))
                .Name("teams")
                .Type<NonNullType<ListType<NonNullType<TeamType>>>>();

            descriptor.Field<MatchResolver>(t => t.GetMatches(default, default))
                .Name("matches")
                .Type<NonNullType<ListType<NonNullType<MatchType>>>>();

        }
    }
}